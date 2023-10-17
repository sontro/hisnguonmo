using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.ADO
{
    public class HoldReturnADO
    {
        public HIS_TREATMENT Treatment { get; set; }
        public List<HIS_DOC_HOLD_TYPE> DocHoldType { get; set; }
        
        public HoldReturnADO()
        {

        }

        public HoldReturnADO(HIS_TREATMENT treatment, List<HIS_DOC_HOLD_TYPE> docHoldType)
        {
            this.Treatment = treatment;
            this.DocHoldType = docHoldType;
        }
    }
}
