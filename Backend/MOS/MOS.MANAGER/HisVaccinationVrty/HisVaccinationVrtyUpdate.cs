using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisVaccinationVrty
{
    partial class HisVaccinationVrtyUpdate : BusinessBase
    {
		private List<HIS_VACCINATION_VRTY> beforeUpdateHisVaccinationVrtys = new List<HIS_VACCINATION_VRTY>();
		
        internal HisVaccinationVrtyUpdate()
            : base()
        {

        }

        internal HisVaccinationVrtyUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_VACCINATION_VRTY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisVaccinationVrtyCheck checker = new HisVaccinationVrtyCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_VACCINATION_VRTY raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisVaccinationVrtyDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisVaccinationVrty_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisVaccinationVrty that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisVaccinationVrtys.Add(raw);
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

        internal bool UpdateList(List<HIS_VACCINATION_VRTY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisVaccinationVrtyCheck checker = new HisVaccinationVrtyCheck(param);
                List<HIS_VACCINATION_VRTY> listRaw = new List<HIS_VACCINATION_VRTY>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisVaccinationVrtyDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisVaccinationVrty_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisVaccinationVrty that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisVaccinationVrtys.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisVaccinationVrtys))
            {
                if (!DAOWorker.HisVaccinationVrtyDAO.UpdateList(this.beforeUpdateHisVaccinationVrtys))
                {
                    LogSystem.Warn("Rollback du lieu HisVaccinationVrty that bai, can kiem tra lai." + LogUtil.TraceData("HisVaccinationVrtys", this.beforeUpdateHisVaccinationVrtys));
                }
				this.beforeUpdateHisVaccinationVrtys = null;
            }
        }
    }
}
