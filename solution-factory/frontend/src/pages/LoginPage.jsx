import { useNavigate } from 'react-router-dom';
import { api } from '../api/client';

export default function LoginPage() {
  const navigate = useNavigate();

  async function onSubmit(event) {
    event.preventDefault();
    const form = new FormData(event.currentTarget);
    const response = await api('/auth/login/email', {
      method: 'POST',
      body: JSON.stringify({
        tenantId: form.get('tenantId'),
        email: form.get('email'),
        password: form.get('password')
      })
    });

    if (!response.accessToken) {
      alert(response.message || 'Login failed');
      return;
    }

    localStorage.setItem('accessToken', response.accessToken);
    navigate('/dashboard');
  }

  return (
    <main>
      <h2>Login</h2>
      <form onSubmit={onSubmit}>
        <input name="tenantId" placeholder="Tenant/Workspace" required />
        <input name="email" type="email" placeholder="Email" required />
        <input name="password" type="password" placeholder="Password" required />
        <button type="submit">Login</button>
      </form>
    </main>
  );
}
