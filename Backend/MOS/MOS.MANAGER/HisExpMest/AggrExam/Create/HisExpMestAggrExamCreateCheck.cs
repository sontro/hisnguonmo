using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.Token;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.AggrExam.Create
{
    class HisExpMestAggrExamCreateCheck : BusinessBase
    {
        internal HisExpMestAggrExamCreateCheck()
            : base()
        {

        }

        internal HisExpMestAggrExamCreateCheck(CommonParam param)
            : base(param)
        {

        }


        internal bool VerifyRequireField(HisExpMestAggrSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (data.ReqRoomId <= 0) throw new ArgumentNullException("data.ReqRoomId null");
                if (!IsNotNullOrEmpty(data.ExpMestIds)) throw new ArgumentNullException("data.ExpMestIds null");
            }
            catch (ArgumentNullException ex)
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                LogSystem.Error(ex);
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

        internal bool IsAllowed(HisExpMestAggrSDO data, ref List<HIS_EXP_MEST> expMests)
        {
            try
            {
                WorkPlaceSDO workPlace = TokenManager.GetWorkPlace(data.ReqRoomId);
                if (workPlace == null)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.KhongCoThongTinPhongLamViec);
                    return false;
                }

                List<HIS_EXP_MEST> tmp = new HisExpMestGet().GetByIds(data.ExpMestIds);
                if (tmp == null)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("ExpMestIds ko hop le");
                    return false;
                }

                //Kiem tra xem cac phieu xuat da thuoc cac phieu linh nao chua
                List<string> inAggrs = tmp.Where(o => o.AGGR_EXP_MEST_ID.HasValue).Select(o => o.EXP_MEST_CODE).ToList();
                if (IsNotNullOrEmpty(inAggrs))
                {
                    string inAggrStr = string.Join(",", inAggrs);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_CacPhieuDaDuocTongHop, inAggrStr);
                    return false;
                }

                //Ko cho tao phieu tong hop kham voi cac phieu xuat ko phai la don phong kham
                List<string> invalidTypes = tmp.Where(o => o.EXP_MEST_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DPK).Select(o => o.EXP_MEST_CODE).ToList();
                if (IsNotNullOrEmpty(invalidTypes))
                {
                    string invalidTypeStr = string.Join(",", invalidTypes);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_CacPhieuKhongPhaiDonPhongKham, invalidTypeStr);
                    return false;
                }

                //Kiem tra xem cac phieu xuat co phieu nao ko o trang thai y/c ko
                List<string> notRequests = tmp
                    .Where(o => o.EXP_MEST_STT_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REQUEST)
                    .Select(o => o.EXP_MEST_CODE).ToList();
                if (IsNotNullOrEmpty(notRequests))
                {
                    string notRequestStr = string.Join(",", notRequests);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_CacPhieuKhongOTrangThaiYeuCau, notRequestStr);
                    return false;
                }

                List<string> notTaken = tmp.Where(o => o.IS_NOT_TAKEN == Constant.IS_TRUE).Select(s => s.EXP_MEST_CODE).ToList();
                if (IsNotNullOrEmpty(notTaken))
                {
                    string notTakenStr = string.Join(", ", notTaken);
                    MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_PhieuXuatDaDuocDanhDauKhongLay, notTakenStr);
                    return false;
                }

                expMests = tmp;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return false;
            }
            return true;
        }
    }
}
