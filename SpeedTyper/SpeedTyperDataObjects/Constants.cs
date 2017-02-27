namespace SpeedTyper.DataObjects
{
    public class Constants
    {
        public static int MAXLEVEL = 15;

        // username must be 4-20 characters long and can only contain letters, numbers, and underscores
        public static string NAMEREGEX = @"^(?=.{4,20}$)(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$"; // http://stackoverflow.com/a/12019115/7124631
        // password  - Minimum 8 characters at least 1 Alphabet and 1 Number with Optional Special Chars
        public static string PASSWORDREGEX = @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d!$%@#£€*?&]{8,}$"; // http://stackoverflow.com/a/21456918/7124631
    }
}
