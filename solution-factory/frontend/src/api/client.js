const API_BASE = import.meta.env.VITE_API_BASE_URL ?? 'http://localhost:5000';
export async function api(path, options = {}) {
  const response = await fetch(`${API_BASE}${path}`, { headers: { 'Content-Type': 'application/json', ...(options.headers || {}) }, ...options });
  return response.json();
}
