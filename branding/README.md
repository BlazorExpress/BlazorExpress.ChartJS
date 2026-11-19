# BlazorExpress Heritage branding

The family uses one badge-free **Be** mark in .NET purple (`#512BD4`). Use the full identity name in wordmarks, link cards, and other placements with room for text.

| Identity | Ready-to-use kit |
| --- | --- |
| BlazorExpress | [BlazorExpress](exports/blazorexpress/) |
| BlazorExpress.Bulma | [Bulma](exports/bulma/) |
| BlazorExpress.ChartJS | [ChartJS](exports/chartjs/) |

| Use | File | Placement |
| --- | --- | --- |
| Logo without text | `logo.svg` | The same color mark works on light and dark surfaces. Prefer a 24 CSS px or larger display box outside the browser tab. |
| Logo with the project name | `wordmark-light.svg`, `wordmark-dark.svg` | Choose the version for the actual surrounding surface. Keep the full name visible or use the compact mark with a clear text label when space is limited. |
| One-color rendering | `logo-black.svg`, `logo-white.svg` | Use black on light surfaces and white on dark surfaces. Identify the project in adjacent text where applicable. |
| Browser tab | `favicon-16.png`, `favicon-32.png` | Declare 16 × 16 and 32 × 32 PNG icons in the document head. |
| Avatar | `avatar.png` | Upload to the appropriate profile or organization avatar setting. The 500 × 500 canvas leaves space for circular crops. |
| Package icon | `package-icon.png` | Bulma and ChartJS each have a transparent 128 × 128 PNG for their NuGet package. Include the image in the package and point the existing icon metadata to it. |
| Website link card | `website-card.png` | Serve the 1200 × 630 PNG at a public URL and use that URL in the Open Graph and Twitter image tags. |
| Repository link preview | `github-preview.png` | Upload the 1280 × 640 PNG in **Settings → Social preview** for the Bulma or ChartJS repository. Use the separate website card for site links. |

ChartJS serves the purple demo logo and loader, uses the favicon at the existing demo URL with the 16 px variant alongside it, and uses the purple card as the demo's default share image. Its README and NuGet packaging share the existing `nuget/logo.png` path. The BlazorExpress and Bulma kits are ready for their maintainers to apply.

## Edit and regenerate

The editable SVGs in [src](src/) define the Be geometry, Sora letter outlines, and card layout. Palette and target sizes live in [assets.json](assets.json). The Be letterforms and sweep are derived from the preexisting ChartJS demo artwork. The named wordmarks use outlined Sora glyphs. See the [upstream Sora license](https://github.com/google/fonts/blob/main/ofl/sora/OFL.txt). The kit includes outlines rather than font binaries.

From `branding/`, use Node.js and the checked-in renderer lockfile:

```sh
npm ci --ignore-scripts
npm run export
npm run check
```

Update the consuming ChartJS copies together from the verified ChartJS exports. The demo shared SVGs use `logo.svg`; its shared fallback PNG uses `package-icon.png`. The WebAssembly files `logo.png`, `favicon.png`, and `favicon-16.png` use `loader.png`, `favicon-32.png`, and `favicon-16.png` respectively. The demo RCL card uses `website-card.png`, and `nuget/logo.png` uses `package-icon.png`. The regular .NET build and package use the checked-in outputs and do not run Node.
