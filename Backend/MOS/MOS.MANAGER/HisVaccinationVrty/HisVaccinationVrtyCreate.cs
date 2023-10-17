using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVaccinationVrty
{
    partial class HisVaccinationVrtyCreate : BusinessBase
    {
		private List<HIS_VACCINATION_VRTY> recentHisVaccinationVrtys = new List<HIS_VACCINATION_VRTY>();
		
        internal HisVaccinationVrtyCreate()
            : base()
        {

        }

        internal HisVaccinationVrtyCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_VACCINATION_VRTY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisVaccinationVrtyCheck checker = new HisVaccinationVrtyCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisVaccinationVrtyDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisVaccinationVrty_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisVaccinationVrty that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisVaccinationVrtys.Add(data);
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
		
		internal bool CreateList(List<HIS_VACCINATION_VRTY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisVaccinationVrtyCheck checker = new HisVaccinationVrtyCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisVaccinationVrtyDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisVaccinationVrty_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisVaccinationVrty that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisVaccinationVrtys.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisVaccinationVrtys))
            {
                if (!DAOWorker.HisVaccinationVrtyDAO.TruncateList(this.recentHisVaccinationVrtys))
                {
                    LogSystem.Warn("Rollback du lieu HisVaccinationVrty that bai, can kiem tra lai." + LogUtil.TraceData("recentHisVaccinationVrtys", this.recentHisVaccinationVrtys));
                }
				this.recentHisVaccinationVrtys = null;
            }
        }
    }
}
