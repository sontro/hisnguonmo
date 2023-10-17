using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TreatmentFinish.ADO
{
    class MediOrgADO : HIS_MEDI_ORG
    {
        public string MEDI_ORG_NAME_UNSIGN { get; set; }

        public MediOrgADO(HIS_MEDI_ORG data)
        {
            Inventec.Common.Mapper.DataObjectMapper.Map<MediOrgADO>(this, data);
            this.MEDI_ORG_NAME_UNSIGN = Inventec.Common.String.Convert.UnSignVNese(data.MEDI_ORG_NAME);
        }
    }
}
