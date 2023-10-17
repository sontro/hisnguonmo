using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisHospitalizeReason
{
    partial class HisHospitalizeReasonCreate : BusinessBase
    {
		private List<HIS_HOSPITALIZE_REASON> recentHisHospitalizeReasons = new List<HIS_HOSPITALIZE_REASON>();
		
        internal HisHospitalizeReasonCreate()
            : base()
        {

        }

        internal HisHospitalizeReasonCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_HOSPITALIZE_REASON data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisHospitalizeReasonCheck checker = new HisHospitalizeReasonCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.HOSPITALIZE_REASON_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisHospitalizeReasonDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisHospitalizeReason_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisHospitalizeReason that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisHospitalizeReasons.Add(data);
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
		
		internal bool CreateList(List<HIS_HOSPITALIZE_REASON> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisHospitalizeReasonCheck checker = new HisHospitalizeReasonCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.HOSPITALIZE_REASON_CODE, null);
                }
                if (valid)
                {
                    if (!DAOWorker.HisHospitalizeReasonDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisHospitalizeReason_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisHospitalizeReason that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisHospitalizeReasons.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisHospitalizeReasons))
            {
                if (!DAOWorker.HisHospitalizeReasonDAO.TruncateList(this.recentHisHospitalizeReasons))
                {
                    LogSystem.Warn("Rollback du lieu HisHospitalizeReason that bai, can kiem tra lai." + LogUtil.TraceData("recentHisHospitalizeReasons", this.recentHisHospitalizeReasons));
                }
				this.recentHisHospitalizeReasons = null;
            }
        }
    }
}
