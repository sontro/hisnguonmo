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

namespace MOS.MANAGER.HisExpMest.Base.BaseHandler.Unapprove
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
            try
            {
                if (expMest != null)
                {
                    Mapper.CreateMap<HIS_EXP_MEST, HIS_EXP_MEST>();
                    HIS_EXP_MEST beforeUpdate = Mapper.Map<HIS_EXP_MEST>(expMest);

                    expMest.EXP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REQUEST;
                    expMest.IS_EXPORT_EQUAL_APPROVE = null;
                    expMest.IS_EXPORT_EQUAL_REQUEST = null;
                    expMest.LAST_APPROVAL_DATE = null;
                    expMest.LAST_APPROVAL_LOGINNAME = null;
                    expMest.LAST_APPROVAL_TIME = null;
                    expMest.LAST_APPROVAL_USERNAME = null;

                    if (!this.hisExpMestUpdate.Update(expMest, beforeUpdate))
                    {
                        throw new Exception("Rollback du lieu");
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return false;
            }
        }


        internal void Rollback()
        {
            this.hisExpMestUpdate.RollbackData();
        }
    }
}
