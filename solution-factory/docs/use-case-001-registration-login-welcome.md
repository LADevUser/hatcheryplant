# Use case 001: Registration, Login, Welcome

Flow: landing -> register/login -> (email verification for email/password) -> login -> welcome dashboard.

Rules:
- Email/password accounts must verify within 24h.
- OAuth providers may be treated verified when `email_verified=true` (documented only in v1, no real OAuth integration yet).
