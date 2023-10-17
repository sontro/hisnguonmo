using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVaccination
{
    partial class HisVaccinationCreate : BusinessBase
    {
		private List<HIS_VACCINATION> recentHisVaccinations = new List<HIS_VACCINATION>();
		
        internal HisVaccinationCreate()
            : base()
        {

        }

        internal HisVaccinationCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_VACCINATION data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisVaccinationCheck checker = new HisVaccinationCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisVaccinationDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisVaccination_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisVaccination that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisVaccinations.Add(data);
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

        internal bool CreateList(List<HIS_VACCINATION> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisVaccinationCheck checker = new HisVaccinationCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisVaccinationDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisVaccination_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisVaccination that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisVaccinations.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisVaccinations))
            {
                if (!DAOWorker.HisVaccinationDAO.TruncateList(this.recentHisVaccinations))
                {
                    LogSystem.Warn("Rollback du lieu HisVaccination that bai, can kiem tra lai." + LogUtil.TraceData("recentHisVaccinations", this.recentHisVaccinations));
                }
				this.recentHisVaccinations = null;
            }
        }
    }
}
