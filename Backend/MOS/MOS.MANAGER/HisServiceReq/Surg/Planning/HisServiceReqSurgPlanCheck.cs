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

namespace MOS.MANAGER.HisServiceReq.Surg.Planning
{
    /// <summary>
    /// Lap ke hoach cho phau thuat
    /// </summary>
    class HisServiceReqSurgPlanCheck : BusinessBase
    {
        internal HisServiceReqSurgPlanCheck()
            : base()
        {
        }

        internal HisServiceReqSurgPlanCheck(CommonParam paramUpdate)
            : base(paramUpdate)
        {
        }

        internal bool IsValidData(HisServiceReqPlanApproveSDO data)
        {
            bool result = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (data.ServiceReqId <= 0) throw new ArgumentNullException("data.ServiceReqId");
                if (data.Time <= 0) throw new ArgumentNullException("data.Time");
                if (data.WorkingRoomId <= 0) throw new ArgumentNullException("data.WorkingRoomId");
                if (string.IsNullOrWhiteSpace(data.Loginname)) throw new ArgumentNullException("data.Loginname");
                if (string.IsNullOrWhiteSpace(data.Username)) throw new ArgumentNullException("data.Loginname");
            }
            catch (ArgumentNullException ex)
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                LogSystem.Warn(ex);
                result = false;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        internal bool IsValidTime(HisServiceReqPlanSDO data)
        {
            bool result = true;
            try
            {
                if (data.PlanTimeFrom > data.PlanTimeTo)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("PlanTimeFrom > PlanTimeTo");
                    result = false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return result;
        }

        internal bool IsValidType(HIS_SERVICE_REQ serviceReq)
        {
            bool result = true;
            try
            {
                if (serviceReq.SERVICE_REQ_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__PT)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_YLenhKhongPhaiLaChiDinhPhauThuat);
                    result = false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return result;
        }

        internal bool IsNotApproved(HIS_SERVICE_REQ serviceReq)
        {
            bool result = true;
            try
            {
                if (serviceReq != null && serviceReq.PTTT_APPROVAL_STT_ID == IMSys.DbConfig.HIS_RS.HIS_PTTT_APPROVAL_STT.ID__APPROVED)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_KeHoachPhauThuatDaDuocDuyet, serviceReq.SERVICE_REQ_CODE);
                    result = false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return result;
        }

        internal bool IsNotApprovedCalendar(HIS_SERVICE_REQ serviceReq)
        {
            bool result = true;
            try
            {
                if (serviceReq.PTTT_CALENDAR_ID.HasValue)
                {
                    HIS_PTTT_CALENDAR calendar = null;
                    HisPtttCalendarCheck calendarCheck = new HisPtttCalendarCheck(param);
                    result = calendarCheck.VerifyId(serviceReq.PTTT_CALENDAR_ID.Value, ref calendar)
                        && calendarCheck.IsNotApproved(calendar);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return result;
        }

        internal bool IsNotDuplicateEkipUser(List<EkipSDO> planEkip)
        {
            bool result = true;
            try
            {
                if (IsNotNullOrEmpty(planEkip))
                {
                    bool check = planEkip.GroupBy(o => new { o.LoginName, o.ExecuteRoleId }).Where(grp => grp.Count() > 1).Any();
                    if (check)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSurgServiceReq_TonTaiHaiDongDuLieuTrungNhau);
                        result = false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return result;
        }
    }
}