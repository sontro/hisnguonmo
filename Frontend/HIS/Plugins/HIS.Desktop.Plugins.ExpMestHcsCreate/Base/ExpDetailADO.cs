using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExpMestHcsCreate.Base
{
    public class ExpDetailADO
    {
        public long ID { get; set; }
        public decimal AMOUNT { get; set; }
        public bool IsMedicine { get; set; }
        public string DESCRIPTION { get; set; }


        public ExpDetailADO()
        {
        }

        public ExpDetailADO(V_HIS_EXP_MEST_METY_REQ data)
        {
            if (data != null)
            {
                this.ID = data.MEDICINE_TYPE_ID;
                this.AMOUNT = data.AMOUNT;
                this.IsMedicine = true;
                this.DESCRIPTION = data.DESCRIPTION;
            }
        }

        public ExpDetailADO(V_HIS_EXP_MEST_MATY_REQ data)
        {
            if (data != null)
            {
                this.ID = data.MATERIAL_TYPE_ID;
                this.AMOUNT = data.AMOUNT;
                this.IsMedicine = false;
                this.DESCRIPTION = data.DESCRIPTION;
            }
        }
    }
}
