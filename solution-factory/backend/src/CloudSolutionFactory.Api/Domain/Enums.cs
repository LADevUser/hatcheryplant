namespace CloudSolutionFactory.Api.Domain;

public enum UserStatus
{
    PendingEmailVerification,
    Active,
    VerificationExpired,
    Disabled
}

public enum AuthProvider
{
    EmailPassword,
    Google,
    Microsoft,
    GitHub
}

public enum Role
{
    Owner,
    Admin,
    Member
}
