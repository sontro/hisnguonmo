using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisEkip;
using MOS.MANAGER.HisEkipUser;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisPtttCalendar;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServExt;
using MOS.MANAGER.HisSereServPttt;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Surg.Calendar
{
    /// <summary>
    /// Gan phau thuat vao lich
    /// </summary>
    class HisServiceReqSurgCalendar : BusinessBase
    {
        private HisServiceReqUpdate hisServiceReqUpdate;

        internal HisServiceReqSurgCalendar()
            : base()
        {
            this.hisServiceReqUpdate = new HisServiceReqUpdate(param);
        }

        internal HisServiceReqSurgCalendar(CommonParam paramUpdate)
            : base(paramUpdate)
        {
            this.hisServiceReqUpdate = new HisServiceReqUpdate(param);
        }

        internal bool Add(HisServiceReqCalendarSDO data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPtttCalendarCheck calendarCheck = new HisPtttCalendarCheck(param);
                HisServiceReqSurgCalendarCheck checker = new HisServiceReqSurgCalendarCheck(param);

                List<HIS_SERVICE_REQ> serviceReqs = null;
                HIS_PTTT_CALENDAR calendar = null;
                WorkPlaceSDO workPlace = null;
                valid = valid && checker.IsValidAddData(data, ref serviceReqs, ref calendar);
                valid = valid && this.HasWorkPlaceInfo(data.WorkingRoomId, ref workPlace);
                valid = valid && this.IsWorkingAtDepartment(calendar.DEPARTMENT_ID, workPlace.DepartmentId);
                valid = valid && checker.IsExecuteAtDepartment(serviceReqs, workPlace.DepartmentId);
                valid = valid && calendarCheck.IsNotApproved(calendar);
                if (valid)
                {
                    Mapper.CreateMap<HIS_SERVICE_REQ, HIS_SERVICE_REQ>();
                    List<HIS_SERVICE_REQ> befores = Mapper.Map<List<HIS_SERVICE_REQ>>(serviceReqs);
                    serviceReqs.ForEach(o => o.PTTT_CALENDAR_ID = calendar.ID);
                    if (this.hisServiceReqUpdate.UpdateList(serviceReqs, befores))
                    {
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                this.RollbackData();
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal bool Remove(HisServiceReqCalendarSDO data)
        {
            bool result = false;
            try
            {
                WorkPlaceSDO workPlace = null;
                bool valid = true;
                HisPtttCalendarCheck calendarCheck = new HisPtttCalendarCheck(param);
                HisServiceReqSurgCalendarCheck checker = new HisServiceReqSurgCalendarCheck(param);
                List<HIS_SERVICE_REQ> serviceReqs = null;
                valid = valid && this.HasWorkPlaceInfo(data.WorkingRoomId, ref workPlace);
                valid = valid && checker.IsValidRemoveData(data, workPlace, ref serviceReqs);
                if (valid)
                {
                    Mapper.CreateMap<HIS_SERVICE_REQ, HIS_SERVICE_REQ>();
                    List<HIS_SERVICE_REQ> befores = Mapper.Map<List<HIS_SERVICE_REQ>>(serviceReqs);
                    serviceReqs.ForEach(o => o.PTTT_CALENDAR_ID = null);

                    if (this.hisServiceReqUpdate.UpdateList(serviceReqs, befores))
                    {
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                this.RollbackData();
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
        
        private void RollbackData()
        {
            this.hisServiceReqUpdate.RollbackData();
        }
    }
}
