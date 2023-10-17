using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMest;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.MANAGER.HisExpMestBltyReq;
using MOS.MANAGER.HisExpMest.Common.Create;
using MOS.MANAGER.HisExpMest.Common.Update;
using MOS.MANAGER.HisExpMest.Common;
using MOS.MANAGER.EventLogUtil;
using MOS.LibraryEventLog;

namespace MOS.MANAGER.HisExpMest.AggrExam.Remove
{
    /// <summary>
    /// Remove 1 phiếu nội trú ra khỏi phiếu lĩnh:
	/// - Phải làm việc tại kho hoặc tại khoa yêu cầu
	/// - Phiếu lĩnh phải chưa được duyệt
    /// </summary>
    partial class HisExpMestAggrExamRemove : BusinessBase
    {
        private HisExpMestUpdate hisExpMestUpdate;

        internal HisExpMestAggrExamRemove()
            : base()
        {
            this.Init();
        }

        internal HisExpMestAggrExamRemove(CommonParam paramUpdate)
            : base(paramUpdate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisExpMestUpdate = new HisExpMestUpdate(param);
        }

        internal bool Run(HisExpMestSDO data, ref HIS_EXP_MEST resultData)
        {
            bool result = false;
            try
            {
                HIS_EXP_MEST expMest = null;
                bool valid = true;
                HisExpMestAggrExamRemoveCheck checker = new HisExpMestAggrExamRemoveCheck(param);
                HisExpMestCheck commonChecker = new HisExpMestCheck(param);
                valid = valid && commonChecker.VerifyRequireField(data);
                valid = valid && checker.IsAllowed(data, ref expMest);
                if (valid)
                {
                    Mapper.CreateMap<HIS_EXP_MEST, HIS_EXP_MEST>();
                    HIS_EXP_MEST before = Mapper.Map<HIS_EXP_MEST>(expMest);
                    expMest.AGGR_EXP_MEST_ID = null;
                    expMest.TDL_AGGR_EXP_MEST_CODE = null;

                    if (!this.hisExpMestUpdate.Update(expMest, before))
                    {
                        throw new Exception("Cap nhat aggr_exp_mest_id ==> null that bai");
                    }
                    resultData = expMest;

                    new EventLogGenerator(EventLog.Enum.HisExpMest_XoaKhoiPhieuLinh).AggrExpMestCode(before.TDL_AGGR_EXP_MEST_CODE).ExpMestCode(resultData.EXP_MEST_CODE).Run();

                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                resultData = null;
                this.RollBack();
                result = false;
            }
            return result;
        }

        private void RollBack()
        {
            this.hisExpMestUpdate.RollbackData();
        }
    }
}
