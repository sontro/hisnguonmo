using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ApprovalExportPrescription.ADO
{
    public class ThreadDataADO
    {
        public List<MOS.EFMODEL.DataModels.V_HIS_EXP_MEST> ListHisExpMest { get; set; }//in
        public List<MOS.EFMODEL.DataModels.HIS_SERVICE_REQ> ListHisServiceReq { get; set; }//Out
        public List<MOS.EFMODEL.DataModels.V_HIS_EXP_MEST_MATERIAL> ListExpMestMaterial { get; set; }//out
        public List<MOS.EFMODEL.DataModels.V_HIS_EXP_MEST_MEDICINE> ListExpMestMedicine { get; set; }//out

        public ThreadDataADO(List<MOS.EFMODEL.DataModels.V_HIS_EXP_MEST> data)
        {
            if (data != null)
            {
                this.ListHisExpMest = data;
            }
        }
    }
}
