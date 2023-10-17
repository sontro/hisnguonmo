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

namespace MOS.MANAGER.HisDispense.Handler.Confirm
{
    class ExpMestProcessor : BusinessBase
    {
        private HisExpMestUpdate hisExpMestUpdate;

        internal ExpMestProcessor()
            : base()
        {
            this.hisExpMestUpdate = new HisExpMestUpdate(param);
        }

        internal ExpMestProcessor(CommonParam param)
            : base(param)
        {
            this.hisExpMestUpdate = new HisExpMestUpdate(param);
        }

        internal bool Run(HIS_DISPENSE dispense, HIS_EXP_MEST expMest, string loginname, string username)
        {
            bool result = false;
            try
            {
                Mapper.CreateMap<HIS_EXP_MEST, HIS_EXP_MEST>();
                HIS_EXP_MEST before = Mapper.Map<HIS_EXP_MEST>(expMest);
                expMest.EXP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__DONE;
                expMest.FINISH_TIME = dispense.DISPENSE_TIME;
                expMest.LAST_EXP_LOGINNAME = loginname;
                expMest.LAST_EXP_TIME = dispense.DISPENSE_TIME;
                expMest.LAST_EXP_USERNAME = username;
                expMest.IS_EXPORT_EQUAL_APPROVE = Constant.IS_TRUE;
                expMest.IS_EXPORT_EQUAL_REQUEST = Constant.IS_TRUE;
                if (!this.hisExpMestUpdate.Update(expMest, before))
                {
                    throw new Exception("hisExpMestUpdate. ket thuc nghiep vu");
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

        internal void RollbackData()
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
