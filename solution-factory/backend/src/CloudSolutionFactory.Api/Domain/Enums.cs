namespace CloudSolutionFactory.Api.Domain;

public enum UserStatus
{
    PendingEmailVerification,
    Active,
    Suspended
}

public enum AuthProvider
{
    EmailPassword,
    Google,
    Microsoft,
    GitHub
}

public enum PlatformRole
{
    Owner,
    Admin,
    Member
}
