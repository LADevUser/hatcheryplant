import { useEffect, useState } from 'react';
import { Link, useSearchParams } from 'react-router-dom';
import { api } from '../api/client';

export default function VerifyEmailResultPage() {
  const [searchParams] = useSearchParams();
  const [message, setMessage] = useState('Verifying token...');

  useEffect(() => {
    const token = searchParams.get('token');
    api(`/auth/verify-email?token=${token}`).then((result) => setMessage(result.message));
  }, [searchParams]);

  return (
    <main>
      <h2>Email Verification</h2>
      <p>{message}</p>
      <Link to="/login">Go to login</Link>
    </main>
  );
}
