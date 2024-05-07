namespace SocialNetwork.Domain.Enum
{

    public enum StatusCode
    {
        UserNotFound = 1,
        UserAlreadyExists = 2,

        MessageNotFound = 11,
        MessageAlreadyExists = 12,

        Ok = 200,
        InternalServerError = 500,
    }
}
