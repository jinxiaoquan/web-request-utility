using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Web_Request_Utility.String
{
    public class StringHelper
    {

        public static SecureString ConvertToSecureString(string password)
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
