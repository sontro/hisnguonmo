using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ACS.UTILITY
{
    public class GenerateToken
    {
        internal const string HASH_SALT = "!@韦#$엃%^穸&*D#127^)*&!졻⇨蹋@#$%件$%^捺^GFTaqm᷻臏R閳�䛹⷏첯瀞湘mdaj멟qp䡷qja.n퐕aq菱1tq3囀t43kz萯z8";

        public static string GenerateRenewCode(string tokenCode)
        {
            if (!String.IsNullOrWhiteSpace(tokenCode))
            {
                string input = string.Format("{0}-{1}", tokenCode, Guid.NewGuid().ToString());
                return HashSha512(input);
            }
            return null;
        }

        private static string HashSha512(string input)
        {
            SHA512CryptoServiceProvider provider = new SHA512CryptoServiceProvider();
            byte[] buffer = provider.ComputeHash(Encoding.UTF8.GetBytes(input + HASH_SALT));
            StringBuilder builder = new StringBuilder();
            foreach (byte num in buffer)
            {
                builder.AppendFormat("{0:x2}", num);
            }
            return builder.ToString();
        }

        public static string GenerateTokenCode(string loginName, string loginAddress)
        {
            if (!String.IsNullOrWhiteSpace(loginAddress) && !String.IsNullOrWhiteSpace(loginName))
            {
                string input = string.Format("{0}-{1}-{2}", loginName, Guid.NewGuid().ToString(), loginAddress);
                return HashSha512(input);
            }
            return null;
        }

    }
}
