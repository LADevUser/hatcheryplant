# Frontend - Cloud Solution Factory Portal

React app with pages for landing, registration, login, verification, and welcome dashboard.

Set API base URL with `VITE_API_BASE_URL` (default `http://localhost:5254`).

## Commands
```bash
npm install
npm run dev
npm run build
npm run test
npm run test:run
npm run test:coverage
```


## Troubleshooting
- If `npm run build` says `Could not resolve entry module "index.html"`, confirm you are in `solution-factory/frontend` and that `frontend/index.html` exists.
- If `npm run test:run` says missing script, run `git pull` and confirm `package.json` includes `test:run`.
- If `vitest: not found`, install dev dependencies with `npm install --include=dev`.
