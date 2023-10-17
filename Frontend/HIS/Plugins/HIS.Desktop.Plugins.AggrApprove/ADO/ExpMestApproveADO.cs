using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AggrApprove.ADO
{
    public class ExpMestApproveADO : V_HIS_EXP_MEST
    {
        public string CONCRETE_ID__IN_SETY { get; set; }
        public string PARENT_ID__IN_SETY { get; set; }
        public string EXP_MEST_PARENT { get; set; }
        public List<long> EXP_MEST_IDs { get; set; }
        public bool CHECK { get; set; }

         public ExpMestApproveADO() { }

         public ExpMestApproveADO(V_HIS_EXP_MEST data)
        {
            try
            {
                if (data != null)
                {
                    Inventec.Common.Mapper.DataObjectMapper.Map<ExpMestApproveADO>(this, data);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
