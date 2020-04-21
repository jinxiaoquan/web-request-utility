using System.Security;

namespace Web_Request_Utility.ConvertExtensions
{
    public static class StringExtensions
    {

        public static SecureString ConvertToSecureString(this string password)
        {
            char[] chArray = password.ToCharArray();
            SecureString secureString = new SecureString();
            foreach (char c in chArray)
            {
                secureString.AppendChar(c);
            } 
            return secureString;
        }

    }
}
