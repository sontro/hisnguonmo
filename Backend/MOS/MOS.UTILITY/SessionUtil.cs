using Inventec.Token.ResourceSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.UTILITY
{
    public class SessionUtil
    {
        /// <summary>
        /// Tao sesssion-key dua vao client-session-key do client gui len bang cach cong them token-code
        /// </summary>
        /// <param name="clientSessionKey"></param>
        /// <returns></returns>
        public static string SessionKey(string clientSessionKey)
        {
            return string.Format("{0}|{1}", ResourceTokenManager.GetTokenCode(), clientSessionKey);
        }
    }
}
