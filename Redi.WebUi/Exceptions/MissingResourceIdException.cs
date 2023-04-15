namespace Redi.WebUi.Exceptions
{
    public class MissingResourceIdException : Exception
    {
        public MissingResourceIdException() 
            : base("Please provide resource Id as a URL route parameter.\n"
                    + "Example:    /path/to/resource/0a12bc34-d5e6-7fgh-i8jk-91l2m3nop4q5")
        { }
    }
}
