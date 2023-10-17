using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisExportMestMedicine.ADO
{
    class ExpMestBloodADO : V_HIS_EXP_MEST_BLOOD
    {
        public decimal AMOUNT { get; set; }

        public ExpMestBloodADO() { }

        public ExpMestBloodADO(V_HIS_EXP_MEST_BLOOD _data)
        {
            try
            {
                if (_data != null)
                {
                    Inventec.Common.Mapper.DataObjectMapper.Map<ExpMestBloodADO>(this, _data);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
