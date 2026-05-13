import { Link } from 'react-router-dom';

export default function LandingPage() {
  return (
    <main>
      <h1>Cloud Solution Factory Platform</h1>
      <p>Start by creating an account or signing in.</p>
      <Link to="/register">Create account</Link> | <Link to="/login">Login</Link>
    </main>
  );
}
