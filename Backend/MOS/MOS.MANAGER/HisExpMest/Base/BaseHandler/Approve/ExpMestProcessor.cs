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

namespace MOS.MANAGER.HisExpMest.Base.BaseHandler.Approve
{
    class ExpMestProcessor : BusinessBase
    {
        private HisExpMestUpdate hisExpMestUpdate;

        internal ExpMestProcessor(CommonParam param)
            : base(param)
        {
            this.hisExpMestUpdate = new HisExpMestUpdate(param);
        }

        internal bool Run(HIS_EXP_MEST expMest, string description, string loginname, string username, long time)
        {
            bool result = false;
            try
            {

                //Cap nhat trang thai cua exp_mest
                Mapper.CreateMap<HIS_EXP_MEST, HIS_EXP_MEST>();
                HIS_EXP_MEST beforeUpdate = Mapper.Map<HIS_EXP_MEST>(expMest);//phuc vu rollback
                expMest.EXP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__EXECUTE;
                expMest.IS_EXPORT_EQUAL_APPROVE = null;
                expMest.LAST_APPROVAL_TIME = time;
                expMest.LAST_APPROVAL_LOGINNAME = loginname;
                expMest.LAST_APPROVAL_USERNAME = username;
                expMest.LAST_APPROVAL_DATE = expMest.LAST_APPROVAL_TIME - expMest.LAST_APPROVAL_TIME % 1000000;
                expMest.IS_BEING_APPROVED = Constant.IS_TRUE;

                if (!this.hisExpMestUpdate.Update(expMest, beforeUpdate))
                {
                    throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                }
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
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
