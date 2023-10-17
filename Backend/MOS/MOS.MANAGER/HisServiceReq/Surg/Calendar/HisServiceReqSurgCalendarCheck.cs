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
    /// Lap ke hoach cho phau thuat
    /// </summary>
    class HisServiceReqSurgCalendarCheck : BusinessBase
    {
        internal HisServiceReqSurgCalendarCheck()
            : base()
        {
        }

        internal HisServiceReqSurgCalendarCheck(CommonParam paramUpdate)
            : base(paramUpdate)
        {
        }

        internal bool IsValidAddData(HisServiceReqCalendarSDO data, ref List<HIS_SERVICE_REQ> serviceReqs, ref HIS_PTTT_CALENDAR calendar)
        {
            bool result = true;
            try
            {
                if (!IsNotNullOrEmpty(data.ServiceReqIds))
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("data.ServiceReqIds ko co du lieu");
                    return false;
                }

                if (!data.PtttCalendarId.HasValue)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("data.PtttCalendarId ko co du lieu");
                    return false;
                }

                var srs = new HisServiceReqGet().GetByIds(data.ServiceReqIds);
                if (srs == null || srs.Count != data.ServiceReqIds.Count)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("Ton tai service_req_id ko hop le hoac co 2 id trung nhau");
                    return false;
                }

                var cal = new HisPtttCalendarGet().GetById(data.PtttCalendarId.Value);
                if (cal == null)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("PtttCalendarId ko hop le");
                    return false;
                }
                calendar = cal;
                serviceReqs = srs;
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return result;
        }

        internal bool IsValidRemoveData(HisServiceReqCalendarSDO data, WorkPlaceSDO workPlace, ref List<HIS_SERVICE_REQ> serviceReqs)
        {
            bool result = true;
            try
            {
                if (!IsNotNullOrEmpty(data.ServiceReqIds))
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("data.ServiceReqIds ko co du lieu");
                    return false;
                }

                var srs = new HisServiceReqGet().GetByIds(data.ServiceReqIds);
                if (srs == null || srs.Count != data.ServiceReqIds.Count)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("Ton tai service_req_id ko hop le hoac co 2 id trung nhau");
                    return false;
                }

                List<string> approveSrCodes = srs.Where(o => o.PTTT_APPROVAL_STT_ID == IMSys.DbConfig.HIS_RS.HIS_PTTT_APPROVAL_STT.ID__APPROVED).Select(s => s.SERVICE_REQ_CODE).ToList();
                if (IsNotNullOrEmpty(approveSrCodes))
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisServiceReq_CacYeuCauDaDuocDuyetMo, String.Join(",", approveSrCodes));
                    return false;
                }

                List<long> ptttCalendarIds = srs.Where(o => o.PTTT_CALENDAR_ID.HasValue).Select(o => o.PTTT_CALENDAR_ID.Value).ToList();
                List<HIS_PTTT_CALENDAR> ptttCalendars = new HisPtttCalendarGet().GetById(ptttCalendarIds);
                if (IsNotNullOrEmpty(ptttCalendars))
                {
                    HisPtttCalendarCheck calendarCheck = new HisPtttCalendarCheck(param);
                    foreach (HIS_PTTT_CALENDAR cal in ptttCalendars)
                    {
                        result = result && calendarCheck.IsNotApproved(cal);
                    }
                }

                if (IsNotNullOrEmpty(srs))
                {
                    foreach (HIS_SERVICE_REQ sr in srs)
                    {
                        result = result && this.IsWorkingAtDepartment(sr.EXECUTE_DEPARTMENT_ID, workPlace.DepartmentId);
                    }
                }
                serviceReqs = srs;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return result;
        }

        internal bool IsExecuteAtDepartment(List<HIS_SERVICE_REQ> serviceReqs, long departmentId)
        {
            try
            {
                List<HIS_SERVICE_REQ> notInDepartments = serviceReqs.Where(o => o.EXECUTE_DEPARTMENT_ID != departmentId).ToList();
                if (IsNotNullOrEmpty(notInDepartments))
                {
                    HIS_DEPARTMENT department = HisDepartmentCFG.DATA.Where(o => o.ID == departmentId).FirstOrDefault();
                    List<string> serviceReqCodes = notInDepartments.Select(o => o.SERVICE_REQ_CODE).ToList();
                    string serviceReqCodeStr = string.Join(",", serviceReqCodes);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_CacYLenhKhongThucHienTaiKhoa, serviceReqCodeStr, department.DEPARTMENT_NAME);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }
    }
}
