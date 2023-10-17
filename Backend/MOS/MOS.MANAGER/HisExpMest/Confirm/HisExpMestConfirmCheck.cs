using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMestBltyReq;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Confirm
{
    class HisExpMestConfirmCheck : BusinessBase
    {
        internal HisExpMestConfirmCheck()
            : base()
        {

        }

        internal HisExpMestConfirmCheck(CommonParam param)
            : base(param)
        {

        }


        internal bool VerifyData(HisExpMestConfirmSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (data.ExpMestId <= 0) throw new ArgumentNullException("data.ExpMestId null");
                if (data.ReqRoomId <= 0) throw new ArgumentNullException("data.ReqRoomId null");
                if (!IsNotNullOrEmpty(data.ExpBltyReqs)) throw new ArgumentNullException("data.ExpBltyReqs null or empty");
                if (data.ExpBltyReqs.Any(a => a.ExpMestBltyReqId <= 0)) throw new ArgumentNullException("data.ExpBltyReqs.ExpMestBltyReqId null");
                if (data.ExpBltyReqs.Any(a => a.BloodTypeId <= 0)) throw new ArgumentNullException("data.ExpBltyReqs.BloodTypeId null");
                if (data.ExpBltyReqs.Any(a => a.Amount <= 0)) throw new ArgumentNullException("data.ExpBltyReqs.Amount null");
            }
            catch (ArgumentNullException ex)
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool ValidData(HisExpMestConfirmSDO data, ref List<HIS_EXP_MEST_BLTY_REQ> bltyReqs)
        {
            bool valid = true;
            try
            {
                List<HIS_EXP_MEST_BLTY_REQ> reqs = new HisExpMestBltyReqGet().GetByExpMestId(data.ExpMestId);
                if (data.ExpBltyReqs.Any(a => reqs == null || !reqs.Exists(e => e.ID == a.ExpMestBltyReqId)))
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Ton tai ExpBltyReqSdo khong thuoc phieu xuat");
                }

                if (reqs.Any(a => !data.ExpBltyReqs.Exists(e => e.ExpMestBltyReqId == a.ID)))
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Ton tai HIS_EXP_MEST_BLTY_REQ khong co trong SDO");
                }

                if (reqs.Count != data.ExpBltyReqs.Count)
                {
                    throw new Exception("Ton tai Req.Count != Sdo.Count");
                }
                bltyReqs = reqs;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool IsNotConfirm(HIS_EXP_MEST expMest)
        {
            bool valid = true;
            try
            {
                valid = this.IsNotConfirm(new List<HIS_EXP_MEST>() { expMest });
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool IsNotConfirm(List<HIS_EXP_MEST> lstExpMest)
        {
            bool valid = true;
            try
            {
                List<string> confirmCodes = lstExpMest != null ? lstExpMest.Where(o => o.IS_CONFIRM == Constant.IS_TRUE).Select(s => s.EXP_MEST_CODE).ToList() : null;
                if (IsNotNullOrEmpty(confirmCodes))
                {
                    string code = String.Join(",", confirmCodes);
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest__PhieuXuatDaDuocDuyetChot, code);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool IsConfirm(HIS_EXP_MEST expMest)
        {
            bool valid = true;
            try
            {
                valid = this.IsConfirm(new List<HIS_EXP_MEST>() { expMest });
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool IsConfirm(List<HIS_EXP_MEST> lstExpMest)
        {
            bool valid = true;
            try
            {
                List<string> notConfirmCodes = lstExpMest != null ? lstExpMest.Where(o => o.IS_CONFIRM != Constant.IS_TRUE).Select(s => s.EXP_MEST_CODE).ToList() : null;
                if (IsNotNullOrEmpty(notConfirmCodes))
                {
                    string code = String.Join(",", notConfirmCodes);
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest__PhieuXuatChuaDuocDuyetChot, code);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool IsStatusRequest(HIS_EXP_MEST expMest)
        {
            bool valid = true;
            try
            {
                valid = this.IsStatusRequest(new List<HIS_EXP_MEST>() { expMest });
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                valid = false;
            }
            return valid;
        }

        internal bool IsStatusRequest(List<HIS_EXP_MEST> lstExpMest)
        {
            bool valid = true;
            try
            {
                List<string> notRequestCodes = lstExpMest != null ? lstExpMest.Where(o => o.EXP_MEST_STT_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REQUEST).Select(s => s.EXP_MEST_CODE).ToList() : null;
                if (IsNotNullOrEmpty(notRequestCodes))
                {
                    string code = String.Join(",", notRequestCodes);
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest__PhieuXuatKhongOTrangThaiYeuCau, code);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                valid = false;
            }
            return valid;
        }

        internal bool IsPresBlood(HIS_EXP_MEST expMest)
        {
            bool valid = true;
            try
            {
                valid = this.IsPresBlood(new List<HIS_EXP_MEST>() { expMest });
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                valid = false;
            }
            return valid;
        }

        internal bool IsPresBlood(List<HIS_EXP_MEST> lstExpMest)
        {
            bool valid = true;
            try
            {
                List<string> notPresBloodCodes = lstExpMest != null ? lstExpMest.Where(o => o.EXP_MEST_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DM).Select(s => s.EXP_MEST_CODE).ToList() : null;
                if (IsNotNullOrEmpty(notPresBloodCodes))
                {
                    string code = String.Join(",", notPresBloodCodes);
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest__PhieuXuatKhongPhaiDonMau, code);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                valid = false;
            }
            return valid;
        }

        internal bool WorkingInMediStock(HIS_EXP_MEST expMest, WorkPlaceSDO workPlace)
        {
            bool valid = true;
            try
            {
                if (!workPlace.MediStockId.HasValue || expMest.MEDI_STOCK_ID != workPlace.MediStockId.Value)
                {
                    V_HIS_MEDI_STOCK mediStock = HisMediStockCFG.DATA.FirstOrDefault(o => o.ID == expMest.MEDI_STOCK_ID);
                    string name = mediStock != null ? mediStock.MEDI_STOCK_NAME : "";
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.BanDangKhongLamViecTaiKho, name);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                valid = false;
            }
            return valid;
        }

    }
}
