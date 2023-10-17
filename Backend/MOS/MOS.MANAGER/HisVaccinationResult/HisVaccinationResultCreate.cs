using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVaccinationResult
{
    partial class HisVaccinationResultCreate : BusinessBase
    {
		private List<HIS_VACCINATION_RESULT> recentHisVaccinationResults = new List<HIS_VACCINATION_RESULT>();
		
        internal HisVaccinationResultCreate()
            : base()
        {

        }

        internal HisVaccinationResultCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_VACCINATION_RESULT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisVaccinationResultCheck checker = new HisVaccinationResultCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisVaccinationResultDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisVaccinationResult_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisVaccinationResult that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisVaccinationResults.Add(data);
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
		
		internal bool CreateList(List<HIS_VACCINATION_RESULT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisVaccinationResultCheck checker = new HisVaccinationResultCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisVaccinationResultDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisVaccinationResult_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisVaccinationResult that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisVaccinationResults.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisVaccinationResults))
            {
                if (!DAOWorker.HisVaccinationResultDAO.TruncateList(this.recentHisVaccinationResults))
                {
                    LogSystem.Warn("Rollback du lieu HisVaccinationResult that bai, can kiem tra lai." + LogUtil.TraceData("recentHisVaccinationResults", this.recentHisVaccinationResults));
                }
				this.recentHisVaccinationResults = null;
            }
        }
    }
}
