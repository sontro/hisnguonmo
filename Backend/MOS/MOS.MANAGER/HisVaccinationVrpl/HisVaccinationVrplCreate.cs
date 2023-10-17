using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVaccinationVrpl
{
    partial class HisVaccinationVrplCreate : BusinessBase
    {
		private List<HIS_VACCINATION_VRPL> recentHisVaccinationVrpls = new List<HIS_VACCINATION_VRPL>();
		
        internal HisVaccinationVrplCreate()
            : base()
        {

        }

        internal HisVaccinationVrplCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_VACCINATION_VRPL data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisVaccinationVrplCheck checker = new HisVaccinationVrplCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisVaccinationVrplDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisVaccinationVrpl_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisVaccinationVrpl that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisVaccinationVrpls.Add(data);
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
		
		internal bool CreateList(List<HIS_VACCINATION_VRPL> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisVaccinationVrplCheck checker = new HisVaccinationVrplCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisVaccinationVrplDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisVaccinationVrpl_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisVaccinationVrpl that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisVaccinationVrpls.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisVaccinationVrpls))
            {
                if (!DAOWorker.HisVaccinationVrplDAO.TruncateList(this.recentHisVaccinationVrpls))
                {
                    LogSystem.Warn("Rollback du lieu HisVaccinationVrpl that bai, can kiem tra lai." + LogUtil.TraceData("recentHisVaccinationVrpls", this.recentHisVaccinationVrpls));
                }
				this.recentHisVaccinationVrpls = null;
            }
        }
    }
}
