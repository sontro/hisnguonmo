using Inventec.Common.Logging;
using System;
using System.Collections.Generic;

namespace ACS.UTILITY
{
    public class Password
    {
        public static string GeneratePasswordTemp()
        {
            try
            {
                return System.Web.Security.Membership.GeneratePassword(16, 0);
            }
            catch (Exception ex)
            {
                LogSystem.Error("Co exception khi generate pin_temp cho khach hang ca nhan. Tam thoi tra ve chuoi mac dinh Acs123456@.");
                LogSystem.Error(ex);
                return "Acs123456@";
            }
        }

        private readonly static Random _rng = new Random();
        private const string _chars = "1234567890";//"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";
        public static string GeneratePassword()
        {
            try
            {
                return RandomString(16);
            }
            catch (Exception ex)
            {
                LogSystem.Error("Co exception khi generate pin_temp cho khach hang ca nhan. Tam thoi tra ve chuoi mac dinh Acs123456@.");
                LogSystem.Error(ex);
                return "Acs123456@";
            }
        }

        public static string RandomString(int size)
        {
            char[] buffer = new char[size];

            for (int i = 0; i < size; i++)
            {
                buffer[i] = _chars[_rng.Next(_chars.Length)];
            }
            return new string(buffer);
        }
    }
}
