using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisAppointmentServ
{
    partial class HisAppointmentServUpdate : BusinessBase
    {
		private List<HIS_APPOINTMENT_SERV> beforeUpdateHisAppointmentServs = new List<HIS_APPOINTMENT_SERV>();
		
        internal HisAppointmentServUpdate()
            : base()
        {

        }

        internal HisAppointmentServUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_APPOINTMENT_SERV data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAppointmentServCheck checker = new HisAppointmentServCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_APPOINTMENT_SERV raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisAppointmentServDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAppointmentServ_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisAppointmentServ that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisAppointmentServs.Add(raw);
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

        internal bool UpdateList(List<HIS_APPOINTMENT_SERV> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisAppointmentServCheck checker = new HisAppointmentServCheck(param);
                List<HIS_APPOINTMENT_SERV> listRaw = new List<HIS_APPOINTMENT_SERV>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisAppointmentServDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAppointmentServ_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisAppointmentServ that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisAppointmentServs.AddRange(listRaw);
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

        internal bool UpdateList(List<HIS_APPOINTMENT_SERV> listData, List<HIS_APPOINTMENT_SERV> befores)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisAppointmentServCheck checker = new HisAppointmentServCheck(param);
                valid = valid && checker.IsUnLock(befores);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisAppointmentServDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAppointmentServ_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisAppointmentServ that bai." + LogUtil.TraceData("listData", listData));
                    }

                    this.beforeUpdateHisAppointmentServs.AddRange(befores);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisAppointmentServs))
            {
                if (!DAOWorker.HisAppointmentServDAO.UpdateList(this.beforeUpdateHisAppointmentServs))
                {
                    LogSystem.Warn("Rollback du lieu HisAppointmentServ that bai, can kiem tra lai." + LogUtil.TraceData("HisAppointmentServs", this.beforeUpdateHisAppointmentServs));
                }
				this.beforeUpdateHisAppointmentServs = null;
            }
        }
    }
}
