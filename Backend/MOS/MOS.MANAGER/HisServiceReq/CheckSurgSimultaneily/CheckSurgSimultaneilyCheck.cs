using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisSereServExt;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.CheckSurgSimultaneily
{
    class CheckSurgSimultaneilyCheck : BusinessBase
    {
        internal CheckSurgSimultaneilyCheck()
            : base()
        {
        }

        internal CheckSurgSimultaneilyCheck(CommonParam param)
            : base(param)
        {
        }

        internal bool VerifyRequireField(HisSurgServiceReqUpdateListSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (!IsNotNullOrEmpty(data.SurgUpdateSDOs)) throw new ArgumentNullException("data.SurgUpdateSDOs");
            }
            catch (ArgumentNullException ex)
            {
                MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                LogSystem.Warn(ex);
                valid = false;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        //internal bool IsValidData(HIS_SERVICE_REQ data, HIS_SERVICE_REQ old)
        //{
        //    bool result = true;

        //    try
        //    {
        //        //Neu bo tick thi kiem tra xem y lenh cha co tick "ko huong BHYT" ko
        //        if (old.IS_NOT_USE_BHYT == Constant.IS_TRUE && data.IS_NOT_USE_BHYT != Constant.IS_TRUE && old.PARENT_ID.HasValue)
        //        {
        //            HIS_SERVICE_REQ parent = new HisServiceReqGet().GetById(old.PARENT_ID.Value);
        //            if (parent.IS_NOT_USE_BHYT == Constant.IS_TRUE)
        //            {
        //                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_YLenhChaKhongHuongBHYT, parent.SERVICE_REQ_CODE);
        //                return false;
        //            }
        //        }

        //        //Neu tick thi kiem tra xem trong y lenh co dich vu nao co doi tuong thanh toan la BHYT ko
        //        if (old.IS_NOT_USE_BHYT != Constant.IS_TRUE && data.IS_NOT_USE_BHYT == Constant.IS_TRUE)
        //        {
        //            HisSereServFilterQuery filter = new HisSereServFilterQuery();
        //            filter.PATIENT_TYPE_ID = HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT;
        //            filter.SERVICE_REQ_ID = old.ID;

        //            List<HIS_SERE_SERV> hisSereServs = new HisSereServGet().Get(filter);
        //            if (IsNotNullOrEmpty(hisSereServs))
        //            {
        //                List<string> serviceCodes = hisSereServs.Select(o => o.TDL_SERVICE_CODE).ToList();
        //                string codeStr = string.Join(",", serviceCodes);
        //                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_CacDichVuCoDoiTuongThanhToanLaBHYT, codeStr);
        //                return false;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogSystem.Error(ex);
        //        result = false;
        //    }

        //    return result;
        //}

        internal bool IsValidServiceReq(List<HIS_SERE_SERV> sereServs, ref HIS_SERVICE_REQ serviceReq)
        {
            bool result = true;

            try
            {
                if (IsNotNullOrEmpty(sereServs))
                {
                    List<long> serviceReqIds = sereServs.Where(o => o.SERVICE_REQ_ID.HasValue).Select(o => o.SERVICE_REQ_ID.Value).Distinct().ToList();
                    if (serviceReqIds == null || serviceReqIds.Count > 1)
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        LogSystem.Warn("sere_serv da bi xoa hoac 2 sere_serv khong cung thuoc 1 y lenh");
                        return false;
                    }
                    result = new HisServiceReqCheck(param).VerifyId(serviceReqIds[0], ref serviceReq);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                result = false;
            }

            return result;
        }

        internal bool IsValidSereServExt(List<HIS_SERE_SERV> sereServs,ref List<HIS_SERE_SERV_EXT> ssExts)
        {
            bool result = true;

            try
            {
                if (IsNotNullOrEmpty(sereServs))
                {
                    ssExts = new HisSereServExtGet().GetBySereServIds(sereServs.Select(o => o.ID).ToList());
                    if (ssExts == null || ssExts.Count != sereServs.Count)
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        LogSystem.Warn("sere_serv da bi xoa hoac 2 sere_serv khong cung thuoc 1 y lenh");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                result = false;
            }

            return result;
        }
    }
}
