using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMest.Common.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Base.BaseHandler.Unexport
{
    class ExpMestProcessor : BusinessBase
    {
        private HisExpMestUpdate hisExpMestUpdate;

        internal ExpMestProcessor(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisExpMestUpdate = new HisExpMestUpdate(param);
        }

        internal bool Run(HIS_EXP_MEST expMest)
        {
            bool result = false;
            try
            {
                if (expMest != null)
                {
                    Mapper.CreateMap<HIS_EXP_MEST, HIS_EXP_MEST>();
                    HIS_EXP_MEST beforeUpdate = Mapper.Map<HIS_EXP_MEST>(expMest);

                    expMest.IS_HTX = MOS.UTILITY.Constant.IS_TRUE;
                    expMest.IS_EXPORT_EQUAL_APPROVE = null;
                    expMest.IS_EXPORT_EQUAL_REQUEST = null;

                    if (!this.hisExpMestUpdate.Update(expMest, beforeUpdate))
                    {
                        throw new Exception("Rollback du lieu");
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

        internal void Rollback()
        {
            this.hisExpMestUpdate.RollbackData();
        }
    }
}
