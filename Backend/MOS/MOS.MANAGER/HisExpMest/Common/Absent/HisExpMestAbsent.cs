using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.MANAGER.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisExpMest.Common;
using MOS.MANAGER.HisExpMest.Common.Update;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Common.Absent
{
    class HisExpMestAbsent : BusinessBase
    {
        private HisExpMestUpdate expMestUpdateProcessor;

        internal HisExpMestAbsent()
            : base()
        {
            this.Init();
        }

        internal HisExpMestAbsent(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.expMestUpdateProcessor = new HisExpMestUpdate(param);
        }

        internal bool Run(HisExpMestSDO data)
        {
            bool result = false;
            try
            {
                HIS_EXP_MEST expMest = null;
                WorkPlaceSDO workPlace = null;
                HisExpMestCheck commonChecker = new HisExpMestCheck(param);

                bool valid = true;
                valid = valid && commonChecker.VerifyId(data.ExpMestId, ref expMest);
                valid = valid && this.HasWorkPlaceInfo(data.ReqRoomId, ref workPlace);

                if (valid)
                {
                    Mapper.CreateMap<HIS_EXP_MEST, HIS_EXP_MEST>();
                    HIS_EXP_MEST beforeExpMest = Mapper.Map<HIS_EXP_MEST>(expMest);

                    expMest.IS_ABSENT = Constant.IS_TRUE;

                    if (!this.expMestUpdateProcessor.Update(expMest, beforeExpMest))
                    {
                        throw new Exception("Cap nhat vang mat phieu tong hop that bai. Rollback du lieu");
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
