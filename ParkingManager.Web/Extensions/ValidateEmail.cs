using System.Text.RegularExpressions;

namespace ParkingManager.Web.Extensions
{
    public static class ValidateEmail
    {
        public static Match Validate(string Email)
        {
            string email = Email;

            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

            Match match = regex.Match(email);

            return match;
        }
    }
}
