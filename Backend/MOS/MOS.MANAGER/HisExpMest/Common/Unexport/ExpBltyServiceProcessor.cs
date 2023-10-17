using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpBltyService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Common.Unexport
{
    class ExpBltyServiceProcessor : BusinessBase
    {
        internal ExpBltyServiceProcessor(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(HIS_EXP_MEST expMest)
        {
            bool result = false;
            try
            {
                if (expMest != null && expMest.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DM)
                {
                    if (!new HisExpBltyServiceTruncate(param).TruncateByExpMestId(expMest.ID))
                    {
                        throw new Exception("HisExpBltyServiceTruncate. Ket thuc nghiep vu");
                    }
                }

                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

    }
}
