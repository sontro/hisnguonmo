using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.BaseCompensationCreate.ADO
{
    public class MetyMatyADO
    {
        public long METY_MATY_ID { get; set; }
        public long TYPE { get; set; }
        public string METY_MATY_CODE { get; set; }
        public string METY_MATY_NAME { get; set; }
        public string SERVICE_UNIT_NAME { get; set; }
        public decimal AMOUNT { get; set; }
        public decimal REQ_AMOUNT { get; set; }
        public bool IsCheck { get; set; }
        public string MEDI_STOCK_NAME { get; set; }
        public long? MEDI_STOCK_ID { get; set; }
        public List<HIS_EXP_MEST_MEDICINE> ExpMestMedicines { get; set; }
        public List<HIS_EXP_MEST_MATERIAL> ExpMestMaterials { get; set; }
        public List<HIS_EXP_MEST_METY_REQ> ExpMestMetyReqs { get; set; }
        public List<HIS_EXP_MEST_MATY_REQ> ExpMestMatyReqs { get; set; }

    }
}
