using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.MANAGER.HisUserRoom;
using MOS.UTILITY;

namespace MOS.MANAGER.HisRoom.UpdateResponsibleUser
{
    class HisRoomUpdateResponsibleUserCheck : BusinessBase
    {
        internal HisRoomUpdateResponsibleUserCheck()
            : base()
        {

        }

        internal HisRoomUpdateResponsibleUserCheck(CommonParam paramCheck)
            : base(paramCheck)
        {

        }

        internal bool IsValid(List<UpdateResponsibleUserSDO> data, ref List<HIS_ROOM> rooms)
        {
            bool valid = true;
            try
            {
                if (IsNotNullOrEmpty(data))
                {
                    List<long> roomIds = data.Select(o => o.RoomId).Distinct().ToList();

                    if (roomIds == null || roomIds.Count != data.Count)
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        LogSystem.Warn("Ton tai roomId trung nhau");
                        return false;
                    }

                    List<HIS_ROOM> r = new HisRoomGet().GetByIds(roomIds);

                    if (r == null || r.Count != roomIds.Count)
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        LogSystem.Warn("Ton tai roomId khong hop le");
                        return false;
                    }

                    rooms = r;

                    List<UpdateResponsibleUserSDO> invalids = data
                        .Where(o => !string.IsNullOrWhiteSpace(o.ResponsibleLoginName))
                        .Where(o => HisUserRoomCFG.DATA == null || !HisUserRoomCFG.DATA.Exists(t => t.ROOM_ID == o.RoomId && t.LOGINNAME == o.ResponsibleLoginName)).ToList();

                    if (IsNotNullOrEmpty(invalids))
                    {
                        var group = invalids.GroupBy(o => o.ResponsibleLoginName);
                        foreach (var g in group)
                        {
                            List<long> ids = g.Select(o => o.RoomId).ToList();
                            List<string> roomNames = HisRoomCFG.DATA.Where(o => ids.Contains(o.ID)).Select(o => o.ROOM_NAME).ToList();
                            string roomNameStr = string.Join(",", roomNames);
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisRoom_ChuaDuocGanQuyenVaoCacPhong, g.Key, roomNameStr);
                        }
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
    }
}
