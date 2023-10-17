using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Pacs
{
    interface IPacsProcessor
    {
        bool SendOrder(PacsOrderData data, ref List<string> sqls);

        void UpdateStatus(List<PacsOrderData> listData, List<string> sqls);

        bool UpdatePatientInfo(HIS_PATIENT data, ref List<string> messages);
    }
}
