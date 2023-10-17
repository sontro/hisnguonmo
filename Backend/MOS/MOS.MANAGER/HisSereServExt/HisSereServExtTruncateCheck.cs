using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisSereServExt;
using MOS.MANAGER.HisServiceReq;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisSereServExt
{
    class HisSereServExtTruncateCheck: BusinessBase
    {
        internal HisSereServExtTruncateCheck()
            : base()
        {

        }

        internal HisSereServExtTruncateCheck(CommonParam param)
            : base(param)
        {

        }

        /// <summary>
        /// kiểm tra dữ liệu Hủy xác nhận không thực hiện
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool IsAllow(HisSereServDeleteConfirmNoExcuteSDO sdo, HIS_SERE_SERV_EXT sereServExt, WorkPlaceSDO workPlaceSdo)
        {
            bool valid = true;
            try
            {
                if (sereServExt == null)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("HisSereServExt ko hop le");
                    return false;
                }

                HIS_SERVICE_REQ serviceReq = new HisServiceReqGet().GetById(sereServExt.TDL_SERVICE_REQ_ID ?? 0);
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
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool VerifyRequireField(HisSereServDeleteConfirmNoExcuteSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (data.SereServId <= 0) throw new ArgumentNullException("data.SereServId");
                if (data.WorkingRoomId <= 0) throw new ArgumentNullException("data.WorkingRoomId");
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
