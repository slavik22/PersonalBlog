namespace BuisnessLogicLayer.Validation;

public class PersonalBlogException: Exception
{
    public PersonalBlogException()
    {
    }

    public PersonalBlogException(string? message) : base(message)
    {
    }

    public PersonalBlogException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}