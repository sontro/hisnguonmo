using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisVaccAppointment
{
    partial class HisVaccAppointmentUpdate : BusinessBase
    {
		private List<HIS_VACC_APPOINTMENT> beforeUpdateHisVaccAppointments = new List<HIS_VACC_APPOINTMENT>();
		
        internal HisVaccAppointmentUpdate()
            : base()
        {

        }

        internal HisVaccAppointmentUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_VACC_APPOINTMENT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisVaccAppointmentCheck checker = new HisVaccAppointmentCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_VACC_APPOINTMENT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisVaccAppointmentDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisVaccAppointment_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisVaccAppointment that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisVaccAppointments.Add(raw);
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

        internal bool UpdateList(List<HIS_VACC_APPOINTMENT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisVaccAppointmentCheck checker = new HisVaccAppointmentCheck(param);
                List<HIS_VACC_APPOINTMENT> listRaw = new List<HIS_VACC_APPOINTMENT>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisVaccAppointmentDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisVaccAppointment_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisVaccAppointment that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisVaccAppointments.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisVaccAppointments))
            {
                if (!DAOWorker.HisVaccAppointmentDAO.UpdateList(this.beforeUpdateHisVaccAppointments))
                {
                    LogSystem.Warn("Rollback du lieu HisVaccAppointment that bai, can kiem tra lai." + LogUtil.TraceData("HisVaccAppointments", this.beforeUpdateHisVaccAppointments));
                }
				this.beforeUpdateHisVaccAppointments = null;
            }
        }
    }
}
