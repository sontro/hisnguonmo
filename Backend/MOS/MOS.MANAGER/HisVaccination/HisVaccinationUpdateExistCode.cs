using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisVaccination
{
    partial class HisVaccinationUpdate : BusinessBase
    {
		private List<HIS_VACCINATION> beforeUpdateHisVaccinations = new List<HIS_VACCINATION>();
		
        internal HisVaccinationUpdate()
            : base()
        {

        }

        internal HisVaccinationUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_VACCINATION data)
        {
            HIS_VACCINATION before = new HisVaccinationGet().GetById(data.ID);
            return this.Update(data, before);
        }

        internal bool Update(HIS_VACCINATION data, HIS_VACCINATION before)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisVaccinationCheck checker = new HisVaccinationCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.IsUnLock(before);
                if (valid)
                {
					if (!DAOWorker.HisVaccinationDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisVaccination_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisVaccination that bai." + LogUtil.TraceData("data", data));
                    }

                    this.beforeUpdateHisVaccinations.Add(before);
                    
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

        internal bool UpdateList(List<HIS_VACCINATION> listData, List<HIS_VACCINATION> befores)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisVaccinationCheck checker = new HisVaccinationCheck(param);
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.IsUnLock(befores);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.VACCINATION_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisVaccinationDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisVaccination_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisVaccination that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.beforeUpdateHisVaccinations.AddRange(befores);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisVaccinations))
            {
                if (!DAOWorker.HisVaccinationDAO.UpdateList(this.beforeUpdateHisVaccinations))
                {
                    LogSystem.Warn("Rollback du lieu HisVaccination that bai, can kiem tra lai." + LogUtil.TraceData("HisVaccinations", this.beforeUpdateHisVaccinations));
                }
				this.beforeUpdateHisVaccinations = null;
            }
        }
    }
}
