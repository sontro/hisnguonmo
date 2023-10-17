using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.ElectronicBill.Base
{
    public class ElectronicBillResultUtil
    {
        public static void Set(ref ElectronicBillResult electronicBillResult, bool success, string message)
        {
            try
            {
                if (electronicBillResult == null)
                {
                    electronicBillResult = new ElectronicBillResult();
                }

                if (electronicBillResult.Messages == null)
                {
                    electronicBillResult.Messages = new List<string>();
                }
                electronicBillResult.Success = success;
                if (!String.IsNullOrEmpty(message))
                {
                    electronicBillResult.Messages.Add(message);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        public static void Set(ref ElectronicBillResult electronicBillResult, bool success, List<string> messages)
        {
            try
            {
                if (electronicBillResult == null)
                {
                    electronicBillResult = new ElectronicBillResult();
                }

                if (electronicBillResult.Messages == null)
                {
                    electronicBillResult.Messages = new List<string>();
                }
                electronicBillResult.Success = success;
                if (messages != null && messages.Count > 0)
                {
                    electronicBillResult.Messages.AddRange(messages);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
