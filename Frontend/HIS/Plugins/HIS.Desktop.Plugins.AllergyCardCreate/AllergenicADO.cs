using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AllergyCardCreate
{
    public class AllergenicADO : HIS_ALLERGENIC
    {
        public bool NghiNgo { get; set; }
        public bool ChacChan { get; set; }

        public AllergenicADO() { }

        public AllergenicADO(HIS_ALLERGENIC data)
        {
            if (data != null)
            {
                Inventec.Common.Mapper.DataObjectMapper.Map<AllergenicADO>(this, data);
                if (data.IS_DOUBT == 1)
                    this.NghiNgo = true;
                if (data.IS_SURE == 1)
                    this.ChacChan = true;
            }
        }
    }
}
