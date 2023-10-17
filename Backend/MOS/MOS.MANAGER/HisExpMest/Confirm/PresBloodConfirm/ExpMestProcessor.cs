using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMest.Common.Update;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Confirm.PresBloodConfirm
{
    class ExpMestProcessor : BusinessBase
    {
        private HisExpMestUpdate hisExpMestUpdate;

        internal ExpMestProcessor(CommonParam param)
            : base(param)
        {
            this.hisExpMestUpdate = new HisExpMestUpdate(param);
        }

        internal bool Run(HIS_EXP_MEST expMest)
        {
            bool result = false;
            try
            {
                Mapper.CreateMap<HIS_EXP_MEST, HIS_EXP_MEST>();
                HIS_EXP_MEST before = Mapper.Map<HIS_EXP_MEST>(expMest);
                expMest.IS_CONFIRM = Constant.IS_TRUE;
                expMest.CONFIRM_TIME = Inventec.Common.DateTime.Get.Now().Value;
                expMest.CONFIRM_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                expMest.CONFIRM_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();

                if (!this.hisExpMestUpdate.Update(expMest,before))
                {
                    throw new Exception("hisExpMestUpdate. Ket thuc nghiep vu");
                }
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
            try
            {
                this.hisExpMestUpdate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
