import { render, screen, waitFor } from '@testing-library/react';
import { MemoryRouter } from 'react-router-dom';
import { AppRoutes } from '../App';

const mockApi = {
  registerEmail: vi.fn().mockResolvedValue({ verificationLink: '/auth/verify-email?token=abc' }),
  loginEmail: vi.fn().mockResolvedValue({ accessToken: 'demo-token' }),
  verifyEmail: vi.fn().mockResolvedValue({ message: 'Email verification successful.' }),
  me: vi.fn().mockResolvedValue({ email: 'tester@example.com' })
};

vi.mock('../api/client', () => ({ api: mockApi }));

function renderAt(path) {
  return render(
    <MemoryRouter initialEntries={[path]}>
      <AppRoutes />
    </MemoryRouter>
  );
}

describe('App route smoke tests', () => {
  beforeEach(() => {
    localStorage.clear();
    vi.clearAllMocks();
  });

  it('renders landing page', () => {
    renderAt('/');
    expect(screen.getByRole('heading', { name: 'Cloud Solution Factory' })).toBeInTheDocument();
  });

  it('renders register page', () => {
    renderAt('/register');
    expect(screen.getByRole('heading', { name: 'Create account' })).toBeInTheDocument();
  });

  it('renders login page', () => {
    renderAt('/login');
    expect(screen.getByRole('heading', { name: 'Login' })).toBeInTheDocument();
  });

  it('renders verification pending page', () => {
    localStorage.setItem('verificationLink', '/auth/verify-email?token=abc');
    renderAt('/verification-pending');
    expect(screen.getByText(/Check your email/i)).toBeInTheDocument();
  });

  it('renders verify email result page', async () => {
    renderAt('/verify-email?token=abc');
    await waitFor(() => {
      expect(screen.getByText('Email verification successful.')).toBeInTheDocument();
    });
  });

  it('renders welcome dashboard page', async () => {
    localStorage.setItem('demoToken', 'demo-token');
    renderAt('/dashboard');
    await waitFor(() => {
      expect(screen.getByRole('heading', { name: 'Welcome Dashboard' })).toBeInTheDocument();
      expect(screen.getByText('Welcome tester@example.com')).toBeInTheDocument();
    });
  });
});
