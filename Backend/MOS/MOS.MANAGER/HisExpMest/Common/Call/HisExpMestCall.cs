using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMest.Common.Update;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Common.Call
{
    class HisExpMestCall : BusinessBase
    {
        private HisExpMestUpdate expMestUpdateProcessor;

        internal HisExpMestCall()
            : base()
        {
            this.Init();
        }

        internal HisExpMestCall(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.expMestUpdateProcessor = new HisExpMestUpdate(param);
        }

        internal bool Run(ExpMestCallSDO data)
        {
            bool result = false;
            try
            {
                HIS_EXP_MEST expMest = null;
                HisExpMestCheck commonChecker = new HisExpMestCheck(param);

                bool valid = true;
                valid = valid && commonChecker.VerifyId(data.ExpMestId, ref expMest);
                if (valid)
                {
                    Mapper.CreateMap<HIS_EXP_MEST, HIS_EXP_MEST>();
                    HIS_EXP_MEST beforeExpMest = Mapper.Map<HIS_EXP_MEST>(expMest);

                    expMest.GATE_CODE = data.GateCode;
                    expMest.CALL_TIME = Inventec.Common.DateTime.Get.Now();

                    if (!this.expMestUpdateProcessor.Update(expMest, beforeExpMest))
                    {
                        throw new Exception("Cap nhat gate_code trong phieu xuat that bai. Rollback du lieu");
                    }

                    result = true;
                }
            }
            catch (Exception ex)
            {
                this.RollbackData();
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }

            return result;
        }

        private void RollbackData()
        {
            this.expMestUpdateProcessor.RollbackData();
        }
    }
}
