using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AssignPrescriptionYHCT.ADO
{
    public class threadMedicineADO
    {
        public HIS_EXP_MEST hisExpMest { get; set; }
        public V_HIS_SERVICE_REQ_5 Prescription { get; set; }
        public List<MPS.ADO.ExeExpMestMedicineSDO> lstMedicineExpmestTypeADO { get; set; }

        public threadMedicineADO() { }

        public threadMedicineADO(HIS_EXP_MEST data)
        {
            try
            {
                if (data != null)
                {
                    this.hisExpMest = new HIS_EXP_MEST();
                    hisExpMest.ID = data.ID;
                    hisExpMest.SERVICE_REQ_ID = data.SERVICE_REQ_ID;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
