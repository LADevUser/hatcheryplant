import { useEffect, useState } from 'react';
import { api } from '../api/client';

export default function WelcomeDashboardPage() {
  const [profile, setProfile] = useState(null);

  useEffect(() => {
    api('/me', {
      headers: { Authorization: `Bearer ${localStorage.getItem('accessToken') || ''}` }
    }).then(setProfile);
  }, []);

  return (
    <main>
      <h2>Welcome Dashboard</h2>
      <p>You are authenticated.</p>
      <pre>{JSON.stringify(profile, null, 2)}</pre>
    </main>
  );
}
