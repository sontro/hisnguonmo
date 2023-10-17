using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisSereServExt;
using MOS.MANAGER.HisServiceReq;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisSereServ.ConfirmNoExcute
{
    class HisSereServConfirmNoExcuteCheck: BusinessBase
    {
        internal HisSereServConfirmNoExcuteCheck()
            : base()
        {

        }

        internal HisSereServConfirmNoExcuteCheck(CommonParam param)
            : base(param)
        {

        }

        /// <summary>
        /// kiểm tra dữ liệu Xác nhận không thực hiện
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool IsAllow(HisSereServConfirmNoExcuteSDO sdo, HIS_SERE_SERV sereServ, WorkPlaceSDO workPlaceSdo)
        {
            bool valid = true;
            try
            {
                if (sereServ == null)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("HisSereServ ko hop le");
                    return false;
                }

                if (sereServ.IS_NO_EXECUTE == Constant.IS_TRUE)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSereServ_DichVuDaDuocDanhDauKhongThucHien);
                    return false;
                }

                HIS_SERVICE_REQ serviceReq = new HisServiceReqGet().GetById(sereServ.SERVICE_REQ_ID ?? 0);
                if (serviceReq.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__HT)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSereServ_YLenhDaHoanThanh);
                    return false;
                }

                if (workPlaceSdo.RoomId != serviceReq.EXECUTE_ROOM_ID)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSereServ_BanKhongLamViecTaiPhongThucHienCuaDichVu);
                    return false;
                }

                HIS_SERE_SERV_EXT sereServExt = new HisSereServExtGet().GetBySereServId(sereServ.ID);
                if (sereServExt != null)
                {
                    if (sereServExt.BEGIN_TIME.HasValue || sereServExt.NOTE != null || sereServExt.CONCLUDE != null)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSereServ_DichVuDaXuLy);
                        return false;
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

        internal bool VerifyRequireField(HisSereServConfirmNoExcuteSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (data.SereServId <= 0) throw new ArgumentNullException("data.SereServId");
                if (data.WorkingRoomId <= 0) throw new ArgumentNullException("data.WorkingRoomId");
                if (string.IsNullOrWhiteSpace(data.ConfirmNoExcuteReason)) throw new ArgumentNullException("data.ConfirmNoExcuteReason");
            }
            catch (ArgumentNullException ex)
            {
                MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                Inventec.Common.Logging.LogSystem.Warn(ex);
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
    }
}
