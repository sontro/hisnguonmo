using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisService;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.HisServiceReq.CheckSurgSimultaneily;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Surg.Process
{
    class HisServiceReqSurgProcessCheck : BusinessBase
    {
        internal HisServiceReqSurgProcessCheck()
            : base()
        {

        }

        internal HisServiceReqSurgProcessCheck(CommonParam paramCheck)
            : base(paramCheck)
        {

        }

        internal bool IsValidUpdateInstructionTime(bool updateInstructionTimeByStartTime, HIS_SERE_SERV_EXT sereServExt, HIS_TREATMENT treatment)
        {
            bool valid = true;
            try
            {
                if (updateInstructionTimeByStartTime)
                {
                    if (sereServExt == null || !sereServExt.BEGIN_TIME.HasValue)
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        LogSystem.Warn("Y/c cap nhat thoi gian y lenh theo thoi gian bat dau nhung ko co thong tin thoi gian bat dau");
                        return false;
                    }
                    valid = new HisServiceReqCheck(param).IsValidInstructionTime(sereServExt.BEGIN_TIME.Value, treatment);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool IsValidTime(HIS_SERE_SERV_EXT sereServExt)
        {
            bool valid = true;
            try
            {
                if (sereServExt.BEGIN_TIME.HasValue && sereServExt.END_TIME.HasValue && sereServExt.BEGIN_TIME.Value > sereServExt.END_TIME.Value)
                {
                    string endTime = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(sereServExt.END_TIME.Value);
                    string beginTime = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(sereServExt.BEGIN_TIME.Value);

                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_ThoiGianBatDauLonHonThoiGianKetThuc, beginTime, endTime);
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool IsValidEyeSurgDescData(HIS_EYE_SURGRY_DESC data)
        {
            bool valid = true;
            try
            {
                List<long> validEyeSurgDescLoaiPtMat = new List<long>()
                {
                    IMSys.DbConfig.HIS_RS.HIS_EYE_SURGRY_DESC.LOAI_PT_MAT__GLOCOM,
                    IMSys.DbConfig.HIS_RS.HIS_EYE_SURGRY_DESC.LOAI_PT_MAT__PT_MONG,
                    IMSys.DbConfig.HIS_RS.HIS_EYE_SURGRY_DESC.LOAI_PT_MAT__PT_SUP_MI,
                    IMSys.DbConfig.HIS_RS.HIS_EYE_SURGRY_DESC.LOAI_PT_MAT__PT_TAI_TAO_LE_QUAN,
                    IMSys.DbConfig.HIS_RS.HIS_EYE_SURGRY_DESC.LOAI_PT_MAT__PT_TTT,
                    IMSys.DbConfig.HIS_RS.HIS_EYE_SURGRY_DESC.LOAI_PT_MAT__TT_LASER_YAG,
                    IMSys.DbConfig.HIS_RS.HIS_EYE_SURGRY_DESC.LOAI_PT_MAT__TT_MONG_MAT
                };
                if (data != null && !validEyeSurgDescLoaiPtMat.Contains(data.LOAI_PT_MAT))
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("LOAI_PT_MAT ko hop le");
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool IsValidData(HisSurgServiceReqUpdateListSDO sdo, ref List<HIS_SERE_SERV> sereServs, ref HIS_SERVICE_REQ serviceReq, ref HIS_TREATMENT treatment)
        {
            bool valid = true;
            try
            {
                if (sdo != null && IsNotNullOrEmpty(sdo.SurgUpdateSDOs))
                {
                    List<long> ids = sdo.SurgUpdateSDOs.Select(o => o.SereServId).Distinct().ToList();
                    sereServs = new HisSereServGet().GetByIds(ids);
                    if (sereServs == null || sereServs.Count != ids.Count)
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        LogSystem.Warn("sere_serv_id khong hop le");
                        return false;
                    }

                    List<long> serviceReqIds = sereServs.Where(o => o.SERVICE_REQ_ID.HasValue).Select(o => o.SERVICE_REQ_ID.Value).Distinct().ToList();
                    if (serviceReqIds == null || serviceReqIds.Count > 1)
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        LogSystem.Warn("sere_serv da bi xoa hoac 2 sere_serv khong cung thuoc 1 y lenh");
                        return false;
                    }

                    serviceReq = new HisServiceReqGet().GetById(serviceReqIds[0]);
                    treatment = new HisTreatmentGet().GetById(serviceReq.TREATMENT_ID);
                    HisServiceCheck svChecker = new HisServiceCheck(param);
                    foreach (SurgUpdateSDO s in sdo.SurgUpdateSDOs)
                    {
                        if (s != null && s.SereServExt != null)
                            s.SereServExt.SERE_SERV_ID = s.SereServId; // gan lai sere_serv_id
                        valid = valid && this.IsValidUpdateInstructionTime(sdo.UpdateInstructionTimeByStartTime, s.SereServExt, treatment);
                        valid = valid && this.IsValidTime(s.SereServExt);
                        valid = valid && this.IsNotDuplicateEkipUser(s.EkipUsers);
                        valid = valid && this.IsValidEyeSurgDescData(s.EyeSurgryDesc);
                    }
                    valid = valid && svChecker.IsValidMinMaxServiceTime(sdo.SurgUpdateSDOs.Select(o => o.SereServExt).ToList(), sereServs);

                    CheckSurgSimultaneilyProcessor simultaneilyChecker = new CheckSurgSimultaneilyProcessor(param);
                    foreach (SurgUpdateSDO s in sdo.SurgUpdateSDOs)
                    {
                        if (HisServiceReqCFG.IS_CHECK_SIMULTANEITY && s.SereServExt != null && s.SereServExt.END_TIME.HasValue && s.SereServExt.END_TIME.HasValue)
                        {
                            var ss = sereServs.FirstOrDefault(o => o.ID == s.SereServId);
                            var service = HisServiceCFG.DATA_VIEW.FirstOrDefault(o => o.ID == ss.SERVICE_ID);
                            if (service != null && (!service.ALLOW_SIMULTANEITY.HasValue || service.ALLOW_SIMULTANEITY.Value != Constant.IS_TRUE))
                            {
                                valid = simultaneilyChecker.CheckPatient(ss, treatment.ID, s.SereServExt.BEGIN_TIME, s.SereServExt.END_TIME, ids) && valid;
                                valid = simultaneilyChecker.CheckDoctor(s.EkipUsers, ss, s.SereServExt.BEGIN_TIME, s.SereServExt.END_TIME, ids) && valid;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        private bool IsValidService(List<HIS_SERE_SERV> sereServs, SurgUpdateSDO sdo)
        {
            bool valid = true;
            try
            {
                if (IsNotNullOrEmpty(sereServs) && IsNotNull(sdo))
                {
                    HIS_SERE_SERV ss = sereServs.FirstOrDefault(o => o.ID == sdo.SereServId);
                    if (IsNotNull(ss))
                    {
                        V_HIS_SERVICE sv = HisServiceCFG.DATA_VIEW.FirstOrDefault(o => o.ID == ss.SERVICE_ID);
                        if (IsNotNull(sv))
                        {
                            if (sv.MIN_PROCESS_TIME.HasValue &&  sv.MAX_PROCESS_TIME.Value > 0)
                            {

                            }
                        }
                    }
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("LOAI_PT_MAT ko hop le");
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool IsNotDuplicateEkipUser(List<HIS_EKIP_USER> ekipUsers)
        {
            try
            {
                if (IsNotNullOrEmpty(ekipUsers))
                {
                    bool check = ekipUsers.GroupBy(o => new { o.LOGINNAME, o.EXECUTE_ROLE_ID }).Where(grp => grp.Count() > 1).Any();
                    if (check)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSurgServiceReq_TonTaiHaiDongDuLieuTrungNhau);
                        return false;
                    }
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
