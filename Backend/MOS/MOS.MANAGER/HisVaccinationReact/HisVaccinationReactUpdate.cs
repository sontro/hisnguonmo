using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisVaccinationReact
{
    partial class HisVaccinationReactUpdate : BusinessBase
    {
		private List<HIS_VACCINATION_REACT> beforeUpdateHisVaccinationReacts = new List<HIS_VACCINATION_REACT>();
		
        internal HisVaccinationReactUpdate()
            : base()
        {

        }

        internal HisVaccinationReactUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_VACCINATION_REACT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisVaccinationReactCheck checker = new HisVaccinationReactCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_VACCINATION_REACT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisVaccinationReactDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisVaccinationReact_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisVaccinationReact that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisVaccinationReacts.Add(raw);
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

        internal bool UpdateList(List<HIS_VACCINATION_REACT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisVaccinationReactCheck checker = new HisVaccinationReactCheck(param);
                List<HIS_VACCINATION_REACT> listRaw = new List<HIS_VACCINATION_REACT>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisVaccinationReactDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisVaccinationReact_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisVaccinationReact that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisVaccinationReacts.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisVaccinationReacts))
            {
                if (!DAOWorker.HisVaccinationReactDAO.UpdateList(this.beforeUpdateHisVaccinationReacts))
                {
                    LogSystem.Warn("Rollback du lieu HisVaccinationReact that bai, can kiem tra lai." + LogUtil.TraceData("HisVaccinationReacts", this.beforeUpdateHisVaccinationReacts));
                }
				this.beforeUpdateHisVaccinationReacts = null;
            }
        }
    }
}
