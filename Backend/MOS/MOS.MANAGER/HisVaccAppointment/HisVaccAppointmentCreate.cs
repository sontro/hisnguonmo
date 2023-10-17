using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVaccAppointment
{
    partial class HisVaccAppointmentCreate : BusinessBase
    {
		private List<HIS_VACC_APPOINTMENT> recentHisVaccAppointments = new List<HIS_VACC_APPOINTMENT>();
		
        internal HisVaccAppointmentCreate()
            : base()
        {

        }

        internal HisVaccAppointmentCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_VACC_APPOINTMENT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisVaccAppointmentCheck checker = new HisVaccAppointmentCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisVaccAppointmentDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisVaccAppointment_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisVaccAppointment that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisVaccAppointments.Add(data);
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
		
		internal bool CreateList(List<HIS_VACC_APPOINTMENT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisVaccAppointmentCheck checker = new HisVaccAppointmentCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisVaccAppointmentDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisVaccAppointment_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisVaccAppointment that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisVaccAppointments.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisVaccAppointments))
            {
                if (!DAOWorker.HisVaccAppointmentDAO.TruncateList(this.recentHisVaccAppointments))
                {
                    LogSystem.Warn("Rollback du lieu HisVaccAppointment that bai, can kiem tra lai." + LogUtil.TraceData("recentHisVaccAppointments", this.recentHisVaccAppointments));
                }
				this.recentHisVaccAppointments = null;
            }
        }
    }
}
