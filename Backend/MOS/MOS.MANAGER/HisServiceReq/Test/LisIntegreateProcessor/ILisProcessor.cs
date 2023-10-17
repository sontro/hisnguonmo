using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Test.LisIntegreateProcessor
{
    interface ILisProcessor
    {
        bool RequestOrder(OrderData data, ref List<string> messages);
        bool DeleteOrder(OrderData data, ref List<string> messages);
        bool UpdatePatientInfo(HIS_PATIENT data, ref List<string> messages);
    }
}
