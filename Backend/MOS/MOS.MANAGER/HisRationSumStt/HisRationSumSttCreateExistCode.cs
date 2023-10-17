using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRationSumStt
{
    partial class HisRationSumSttCreate : BusinessBase
    {
        private List<HIS_RATION_SUM_STT> recentHisRationSumStts = new List<HIS_RATION_SUM_STT>();

        internal HisRationSumSttCreate()
            : base()
        {

        }

        internal HisRationSumSttCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_RATION_SUM_STT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisRationSumSttCheck checker = new HisRationSumSttCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.RATION_SUM_STT_CODE, null);
                if (valid)
                {
                    if (!DAOWorker.HisRationSumSttDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisRationSumStt_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisRationSumStt that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisRationSumStts.Add(data);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal void RollbackData()
        {
            if (IsNotNullOrEmpty(this.recentHisRationSumStts))
            {
                if (!DAOWorker.HisRationSumSttDAO.TruncateList(this.recentHisRationSumStts))
                {
                    LogSystem.Warn("Rollback du lieu HisRationSumStt that bai, can kiem tra lai." + LogUtil.TraceData("recentHisRationSumStts", this.recentHisRationSumStts));
                }
                this.recentHisRationSumStts = null;
            }
        }
    }
}
