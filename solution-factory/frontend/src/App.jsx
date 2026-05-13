import { BrowserRouter, Route, Routes } from 'react-router-dom';
import LandingPage from './pages/LandingPage';
import RegisterPage from './pages/RegisterPage';
import LoginPage from './pages/LoginPage';
import VerificationPendingPage from './pages/VerificationPendingPage';
import VerifyEmailResultPage from './pages/VerifyEmailResultPage';
import WelcomeDashboardPage from './pages/WelcomeDashboardPage';

export default function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<LandingPage />} />
        <Route path="/register" element={<RegisterPage />} />
        <Route path="/login" element={<LoginPage />} />
        <Route path="/verification-pending" element={<VerificationPendingPage />} />
        <Route path="/verify-email" element={<VerifyEmailResultPage />} />
        <Route path="/dashboard" element={<WelcomeDashboardPage />} />
      </Routes>
    </BrowserRouter>
  );
}
