using Inventec.Core;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Test.LisIntegreateProcessor
{
    class LisProcessor
    {
        public static bool RequestOrder(CommonParam param, OrderData data, ref List<string> messages)
        {
            ILisProcessor processor = LisFactory.GetProcessor(param);
            if (processor != null)
            {
                return processor.RequestOrder(data, ref messages);
            }
            return false;
        }

        public static bool DeleteOrder(CommonParam param, OrderData data, ref List<string> messages)
        {
            ILisProcessor processor = LisFactory.GetProcessor(param);
            if (processor != null)
            {
                return processor.DeleteOrder(data, ref messages);
            }
            return false;
        }

        public static bool UpdatePatientInfo(CommonParam param, HIS_PATIENT data, ref List<string> messages)
        {
            ILisProcessor processor = LisFactory.GetProcessor(param);
            if (processor != null)
            {
                return processor.UpdatePatientInfo(data, ref messages);
            }
            return false;
        }
    }
}
