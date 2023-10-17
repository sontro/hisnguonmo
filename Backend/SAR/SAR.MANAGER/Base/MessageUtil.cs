using SAR.LibraryMessage;
using Inventec.Common.Logging;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Base
{
    public class MessageUtil
    {
        public static void SetMessage(CommonParam param, SAR.LibraryMessage.Message.Enum en, params string[] extraMessage)
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

        private static string GetMessage(SAR.LibraryMessage.Message.Enum en)
        {
            Message message = SAR.LibraryMessage.DatabaseMessage.Get(null, en);
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
