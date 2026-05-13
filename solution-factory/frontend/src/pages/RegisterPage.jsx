import { useNavigate } from 'react-router-dom';
import { api } from '../api/client';

export default function RegisterPage() {
  const navigate = useNavigate();

  async function onSubmit(event) {
    event.preventDefault();
    const form = new FormData(event.currentTarget);

    const response = await api('/auth/register/email', {
      method: 'POST',
      body: JSON.stringify({
        tenantId: form.get('tenantId'),
        email: form.get('email'),
        password: form.get('password')
      })
    });

    alert(response.message);
    navigate('/verification-pending');
  }

  return (
    <main>
      <h2>Register</h2>
      <form onSubmit={onSubmit}>
        <input name="tenantId" placeholder="Tenant/Workspace" required />
        <input name="email" type="email" placeholder="Email" required />
        <input name="password" type="password" placeholder="Password" required />
        <button type="submit">Create account</button>
      </form>
      <p>Google / Microsoft / GitHub sign-in will be connected in a later iteration.</p>
    </main>
  );
}
