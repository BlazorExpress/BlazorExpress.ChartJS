import { readFile, mkdir, mkdtemp, rename, rm, rmdir, writeFile } from 'node:fs/promises';
import { join } from 'node:path';
import { fileURLToPath } from 'node:url';
import { Resvg } from '@resvg/resvg-js';

const root = fileURLToPath(new URL('.', import.meta.url));
const source = join(root, 'src');
const destination = join(root, 'exports');
const manifest = JSON.parse(await readFile(join(root, 'assets.json'), 'utf8'));
const master = await readFile(join(source, 'be.svg'), 'utf8');
const cardLayout = await readFile(join(source, 'cards.svg'), 'utf8');
const outputs = new Map();

function svg(width, height, label, content) {
    return `<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 ${width} ${height}" aria-label="${label}" role="img">\n${content}\n</svg>\n`;
}

function mark(color, monochrome = false) {
    const defs = master.match(/<defs>[\s\S]*?<\/defs>/)?.[0];
    if (!defs) throw new Error('The Be master is missing its geometry.');
    const tile = `<use href="#heritage-tile" fill="${color}" stroke="${color}" stroke-width="5" stroke-miterlimit="10"`;
    if (monochrome) return `  ${defs}\n  ${tile} mask="url(#heritage-ink)"/>`;
    return `  ${defs}\n  ${tile}/>\n  <use href="#heritage-sweep" fill="#FFFFFF"/>\n  <use href="#heritage-letterforms" fill="${color}"/>`;
}

function place(content, x, y, scale = 1) {
    return `  <g transform="translate(${x} ${y}) scale(${scale})">\n${content}\n  </g>`;
}

function add(name, content) {
    if (!/^[a-z]+\/[a-z0-9-]+\.(svg|png)$/.test(name) || outputs.has(name)) {
        throw new Error(`Invalid or repeated export path: ${name}`);
    }
    outputs.set(name, Buffer.from(content));
}

function addPng(name, artwork, width, height, limit = Infinity) {
    const image = new Resvg(artwork, {
        fitTo: { mode: 'width', value: width },
        font: { loadSystemFonts: false }
    }).render();
    const png = image.asPng();
    if (image.width !== width || image.height !== height || png.length >= limit) {
        throw new Error(`Unexpected dimensions or file size for ${name}`);
    }
    add(name, png);
}

function card(brand, artwork, lettering, width, height) {
    const scaleX = width / 1200;
    const scaleY = height / 630;
    const scale = Math.min(scaleX, scaleY);
    const offsetX = (width - 1200 * scale) / 2;
    const wordScale = Math.min(1, 700 / lettering.width);
    const background = cardLayout
        .replace(/^[\s\S]*?<svg[^>]*>/, '')
        .replace(/<\/svg>\s*$/, '')
        .replaceAll('#5C2D91', brand.color)
        .replace(/  <!-- (mark|wordmark) -->\r?\n/g, '');
    const foreground = cardLayout
        .split('\n')
        .filter(line => line.includes('<!--'))
        .join('\n')
        .replace('<!-- mark -->', place(artwork, 101, 225, 180 / 512))
        .replace('<!-- wordmark -->', place(lettering.paths, 317, 315 - 48 * wordScale, wordScale));
    const backdrop = `  <g transform="scale(${scaleX} ${scaleY})">${background}  </g>`;
    return svg(width, height, brand.name, backdrop + '\n' + place(foreground, offsetX, 0, scale));
}

for (const [id, brand] of Object.entries(manifest.identities)) {
    const wordSource = await readFile(join(source, `wordmark-${id}.svg`), 'utf8');
    const width = Number(wordSource.match(/viewBox="0 0 ([\d.]+) 96"/)?.[1]);
    const paths = wordSource.match(/<g>[\s\S]*<\/g>/)?.[0];
    if (!width || !paths) throw new Error(`Missing outlined wordmark for ${brand.name}`);
    const lettering = { width, paths };
    const artwork = mark(brand.color);
    const logo = svg(512, 512, `${brand.name} logo`, artwork);

    add(`${id}/logo.svg`, logo);
    for (const [name, ink] of [['black', '#000000'], ['white', '#FFFFFF']]) {
        add(`${id}/logo-${name}.svg`, svg(512, 512, `${brand.name} logo`, mark(ink, true)));
    }
    for (const [theme, ink] of [['light', '#171323'], ['dark', '#FFFFFF']]) {
        const lockup = place(artwork, 0, 0, 96 / 512) + '\n'
            + place(paths.replaceAll('#171323', ink), 120, 0);
        add(`${id}/wordmark-${theme}.svg`, svg(120 + width, 96, brand.name, lockup));
    }
    for (const size of manifest.favicons) {
        addPng(`${id}/favicon-${size}.png`, logo, size, size);
    }
    const avatar = manifest.avatar;
    const avatarSvg = svg(avatar.size, avatar.size, `${brand.name} avatar`, place(artwork, 76, 76, 348 / 512));
    addPng(`${id}/avatar.png`, avatarSvg, avatar.size, avatar.size, avatar.limit);
    const site = manifest.websiteCard;
    addPng(`${id}/website-card.png`, card(brand, artwork, lettering, site.width, site.height), site.width, site.height);
    if (brand.github) {
        const preview = manifest.githubPreview;
        addPng(`${id}/github-preview.png`, card(brand, artwork, lettering, preview.width, preview.height), preview.width, preview.height, preview.limit);
    }
    if (brand.package) {
        const icon = manifest.packageIcon;
        addPng(`${id}/package-icon.png`, logo, icon.size, icon.size, icon.limit);
    }
    if (brand.loader) {
        const { size } = manifest.loader;
        addPng(`${id}/loader.png`, logo, size, size);
    }
}

if (process.argv.includes('--check')) {
    for (const [name, content] of outputs) {
        const current = await readFile(join(destination, name));
        if (!current.equals(content)) throw new Error(`Stale export: ${name}`);
    }
    console.log(`Verified ${outputs.size} reproducible Heritage exports.`);
} else {
    const stage = await mkdtemp(join(root, '.exports-'));
    try {
        for (const [name, content] of outputs) {
            const [id] = name.split('/');
            await mkdir(join(stage, id), { recursive: true });
            await writeFile(join(stage, name), content);
        }
        for (const id of Object.keys(manifest.identities)) {
            await mkdir(join(destination, id), { recursive: true });
        }
        for (const name of outputs.keys()) {
            await rename(join(stage, name), join(destination, name));
        }
        console.log(`Wrote ${outputs.size} Heritage exports.`);
    } finally {
        for (const name of outputs.keys()) await rm(join(stage, name), { force: true });
        for (const id of Object.keys(manifest.identities)) {
            await rmdir(join(stage, id)).catch(error => {
                if (error.code !== 'ENOENT') throw error;
            });
        }
        await rmdir(stage);
    }
}
