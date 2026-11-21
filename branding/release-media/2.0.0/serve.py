"""Serve branding/ on http://127.0.0.1:5098 and save what generator.html posts.

Run from anywhere:  python branding/release-media/2.0.0/serve.py
Then open http://127.0.0.1:5098/release-media/2.0.0/generator.html and press a button.
Only the file names in ALLOWED are written, and only into this folder.
"""
import http.server
import pathlib
import urllib.parse

HERE = pathlib.Path(__file__).resolve().parent
ROOT = HERE.parent.parent  # branding/
ALLOWED = {"release-video.mp4", "poster.png", "thumbnail.png"}


class Handler(http.server.SimpleHTTPRequestHandler):
    def __init__(self, *args, **kwargs):
        super().__init__(*args, directory=str(ROOT), **kwargs)

    def do_POST(self):
        query = urllib.parse.parse_qs(urllib.parse.urlparse(self.path).query)
        name = (query.get("name") or [""])[0]
        if not self.path.startswith("/save") or name not in ALLOWED:
            self.send_error(400, "unknown file name")
            return
        length = int(self.headers.get("Content-Length", "0"))
        data = self.rfile.read(length)
        (HERE / name).write_bytes(data)
        self.send_response(200)
        self.end_headers()
        self.wfile.write(f"saved {name} ({len(data)} bytes)".encode())


if __name__ == "__main__":
    http.server.ThreadingHTTPServer(("127.0.0.1", 5098), Handler).serve_forever()
