using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAppointmentServ
{
    partial class HisAppointmentServCreate : BusinessBase
    {
		private List<HIS_APPOINTMENT_SERV> recentHisAppointmentServs = new List<HIS_APPOINTMENT_SERV>();
		
        internal HisAppointmentServCreate()
            : base()
        {

        }

        internal HisAppointmentServCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_APPOINTMENT_SERV data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAppointmentServCheck checker = new HisAppointmentServCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisAppointmentServDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAppointmentServ_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisAppointmentServ that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisAppointmentServs.Add(data);
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
		
		internal bool CreateList(List<HIS_APPOINTMENT_SERV> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisAppointmentServCheck checker = new HisAppointmentServCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisAppointmentServDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAppointmentServ_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisAppointmentServ that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisAppointmentServs.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisAppointmentServs))
            {
                if (!DAOWorker.HisAppointmentServDAO.TruncateList(this.recentHisAppointmentServs))
                {
                    LogSystem.Warn("Rollback du lieu HisAppointmentServ that bai, can kiem tra lai." + LogUtil.TraceData("recentHisAppointmentServs", this.recentHisAppointmentServs));
                }
				this.recentHisAppointmentServs = null;
            }
        }
    }
}
