using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMest.Common.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Base.BaseCompensation.Create
{
    class ExpMestProcessor : BusinessBase
    {
        private HisExpMestCreate hisExpMestCreate;

        internal ExpMestProcessor()
            : base()
        {
            this.hisExpMestCreate = new HisExpMestCreate(param);
        }

        internal ExpMestProcessor(CommonParam param)
            : base(param)
        {
            this.hisExpMestCreate = new HisExpMestCreate(param);
        }

        internal bool Run(Dictionary<HIS_EXP_MEST, ExpMestDetail> dicExpMest, ref List<HIS_EXP_MEST> expMests)
        {
            bool result = false;
            try
            {
                List<HIS_EXP_MEST> toInserts = dicExpMest.Keys.ToList();
                if (!this.hisExpMestCreate.CreateList(toInserts))
                {
                    throw new Exception("hisExpMestCreate. Ket thuc nghiep vu");
                }
                expMests = toInserts;
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
                param.HasException = true;
            }
            return result;
        }

        internal void Rollback()
        {
            this.hisExpMestCreate.RollbackData();
        }
    }
}
