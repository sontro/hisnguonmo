using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisVaccinationVrpl
{
    partial class HisVaccinationVrplUpdate : BusinessBase
    {
		private List<HIS_VACCINATION_VRPL> beforeUpdateHisVaccinationVrpls = new List<HIS_VACCINATION_VRPL>();
		
        internal HisVaccinationVrplUpdate()
            : base()
        {

        }

        internal HisVaccinationVrplUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_VACCINATION_VRPL data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisVaccinationVrplCheck checker = new HisVaccinationVrplCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_VACCINATION_VRPL raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisVaccinationVrplDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisVaccinationVrpl_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisVaccinationVrpl that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisVaccinationVrpls.Add(raw);
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

        internal bool UpdateList(List<HIS_VACCINATION_VRPL> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisVaccinationVrplCheck checker = new HisVaccinationVrplCheck(param);
                List<HIS_VACCINATION_VRPL> listRaw = new List<HIS_VACCINATION_VRPL>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisVaccinationVrplDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisVaccinationVrpl_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisVaccinationVrpl that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisVaccinationVrpls.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisVaccinationVrpls))
            {
                if (!DAOWorker.HisVaccinationVrplDAO.UpdateList(this.beforeUpdateHisVaccinationVrpls))
                {
                    LogSystem.Warn("Rollback du lieu HisVaccinationVrpl that bai, can kiem tra lai." + LogUtil.TraceData("HisVaccinationVrpls", this.beforeUpdateHisVaccinationVrpls));
                }
				this.beforeUpdateHisVaccinationVrpls = null;
            }
        }
    }
}
