using Inventec.Common.Logging;
using Inventec.Core;
using MOS.LibraryMessage;
using System;

namespace MOS.Dashboard.Base
{
    public class MessageUtil
    {
        public static void SetMessage(CommonParam param, MOS.LibraryMessage.Message.Enum en, params string[] extraMessage)
        {
            string message = null;
            try
            {
                message = GetMessage(en, param.LanguageCode);
                if (extraMessage != null && extraMessage.Length > 0)
                {
                    message = String.Format(message, extraMessage);
                }
                AddMessage(param, message);
            }
            catch (Exception ex)
            {
                LogSystem.Error("Co exception khi SetParam co tham so phu." + message, ex);
            }
        }

        internal static string GetMessage(MOS.LibraryMessage.Message.Enum en, string languageCode)
        {
            languageCode = string.IsNullOrWhiteSpace(languageCode) ? Message.LanguageCode.VI : languageCode;
            Message message = MOS.LibraryMessage.DatabaseMessage.Get(languageCode, en);
            return message != null ? message.message : null;
        }

        private static void AddMessage(CommonParam param, string message)
        {
            if (message != null)
            {
                if (!param.Messages.Contains(message))
                {
                    param.Messages.Add(message);
                }
            }
        }
    }
}
