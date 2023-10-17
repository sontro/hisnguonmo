using SDA.LibraryMessage;
using Inventec.Common.Logging;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Base
{
    public class MessageUtil
    {
        public static void SetMessage(CommonParam param, SDA.LibraryMessage.Message.Enum en, params string[] extraMessage)
        {
            try
            {
                string message = GetMessage(en);
                if (extraMessage != null && extraMessage.Length > 0)
                {
                    message = String.Format(message, extraMessage);
                }
                AddMessage(param, message);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        private static string GetMessage(SDA.LibraryMessage.Message.Enum en)
        {
            Message message = SDA.LibraryMessage.DatabaseMessage.Get(null, en);
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
