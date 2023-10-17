using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVaccinationReact
{
    partial class HisVaccinationReactCreate : BusinessBase
    {
		private List<HIS_VACCINATION_REACT> recentHisVaccinationReacts = new List<HIS_VACCINATION_REACT>();
		
        internal HisVaccinationReactCreate()
            : base()
        {

        }

        internal HisVaccinationReactCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_VACCINATION_REACT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisVaccinationReactCheck checker = new HisVaccinationReactCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisVaccinationReactDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisVaccinationReact_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisVaccinationReact that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisVaccinationReacts.Add(data);
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
		
		internal bool CreateList(List<HIS_VACCINATION_REACT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisVaccinationReactCheck checker = new HisVaccinationReactCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisVaccinationReactDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisVaccinationReact_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisVaccinationReact that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisVaccinationReacts.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisVaccinationReacts))
            {
                if (!DAOWorker.HisVaccinationReactDAO.TruncateList(this.recentHisVaccinationReacts))
                {
                    LogSystem.Warn("Rollback du lieu HisVaccinationReact that bai, can kiem tra lai." + LogUtil.TraceData("recentHisVaccinationReacts", this.recentHisVaccinationReacts));
                }
				this.recentHisVaccinationReacts = null;
            }
        }
    }
}
