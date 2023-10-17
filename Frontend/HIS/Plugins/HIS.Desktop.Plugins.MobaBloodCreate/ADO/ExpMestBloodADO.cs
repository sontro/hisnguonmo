using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MobaBloodCreate.ADO
{
    public class ExpMestBloodADO : V_HIS_EXP_MEST_BLOOD
    {
        public bool IsCheck { get; set; }
        public ExpMestBloodADO()
        {
        }

        public ExpMestBloodADO(V_HIS_EXP_MEST_BLOOD data)
        {
            try
            {
                if (data != null)
                {
                    Inventec.Common.Mapper.DataObjectMapper.Map<ExpMestBloodADO>(this, data);
                    this.BLOOD_TYPE_CODE = data.BLOOD_TYPE_CODE;
                    this.BLOOD_TYPE_NAME = data.BLOOD_TYPE_NAME;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
