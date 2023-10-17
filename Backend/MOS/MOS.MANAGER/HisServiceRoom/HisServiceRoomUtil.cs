using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceRoom
{
    class HisServiceRoomUtil
    {
        //Kiem tra xem phong co xu ly duoc dich vu hay khong
        public static bool IsProcessable(long roomId, long serviceId, CommonParam param)
        {
            V_HIS_SERVICE service = HisServiceCFG.DATA_VIEW.Where(o => o.ID == serviceId).FirstOrDefault();
            if (service == null || service.IS_ACTIVE != Constant.IS_TRUE)
            {
                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisService_DichVuDangBiKhoa);
                return false;
            }

            V_HIS_ROOM room = HisRoomCFG.DATA.Where(o => o.ID == roomId).FirstOrDefault();
            if (room == null || room.IS_ACTIVE != Constant.IS_TRUE)
            {
                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisRoom_PhongXuLyDangBiKhoa);
                return false;
            }


            //Kiem tra xem phong tiep theo co duoc phep xu ly dich vu hay khong
            if (HisServiceRoomCFG.DATA_VIEW == null || !HisServiceRoomCFG.DATA_VIEW.Exists(o => o.ROOM_ID == roomId && o.SERVICE_ID == serviceId && o.IS_ACTIVE == Constant.IS_TRUE))
            {
                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceRoom_KhongChoPhepThucHienDichVuTaiPhong, service.SERVICE_NAME, room.ROOM_NAME);
                return false;
            }
            return true;
        }

        public static bool VerifyExecuteRoom(long roomId, CommonParam param)
        {
            V_HIS_EXECUTE_ROOM room = HisExecuteRoomCFG.DATA.Where(o => o.ROOM_ID == roomId).FirstOrDefault();
            if (room == null || room.IS_ACTIVE != Constant.IS_TRUE)
            {
                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisRoom_PhongXuLyDangBiKhoa);
                return false;
            }

            if (room.ALLOW_NOT_CHOOSE_SERVICE != Constant.IS_TRUE)
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                LogSystem.Error("HIS_EXECUTE_ROOM.ALLOW_NOT_CHOOSE_SERVICE <> 1");
                return false;
            }

            return true;
        }
    }
}
