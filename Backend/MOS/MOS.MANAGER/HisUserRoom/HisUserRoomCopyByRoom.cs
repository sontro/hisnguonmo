using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisUserRoom
{
    internal class HisUserRoomCopyByRoom : BusinessBase
    {
        internal HisUserRoomCopyByRoom()
            : base()
        {

        }

        internal HisUserRoomCopyByRoom(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(HisUserRoomCopyByRoomSDO data, ref List<HIS_USER_ROOM> resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(data);
                valid = valid && IsGreaterThanZero(data.CopyRoomId);
                valid = valid && IsGreaterThanZero(data.PasteRoomId);
                if (valid)
                {
                    List<HIS_USER_ROOM> newUserRooms = new List<HIS_USER_ROOM>();
                    List<HIS_USER_ROOM> copyUserRooms = DAOWorker.SqlDAO.GetSql<HIS_USER_ROOM>("SELECT * FROM HIS_USER_ROOM WHERE ROOM_ID = :param1", data.CopyRoomId);
                    List<HIS_USER_ROOM> pasteUserRooms = DAOWorker.SqlDAO.GetSql<HIS_USER_ROOM>("SELECT * FROM HIS_USER_ROOM WHERE ROOM_ID = :param1", data.PasteRoomId);
                    if (!IsNotNullOrEmpty(copyUserRooms))
                    {
                        V_HIS_ROOM room = Config.HisRoomCFG.DATA.FirstOrDefault(o => o.ID == data.CopyRoomId);
                        string name = room != null ? room.ROOM_NAME : null;
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisUserRoom_PhongChuaCoDuLieuThietLap, name);
                        throw new Exception("Khong co du lieu CopyUserRoom");
                    }

                    foreach (HIS_USER_ROOM copyData in copyUserRooms)
                    {
                        if (pasteUserRooms == null || pasteUserRooms.Exists(e => e.LOGINNAME == copyData.LOGINNAME))
                        {
                            continue;
                        }
                        HIS_USER_ROOM pasteData = new HIS_USER_ROOM();
                        pasteData.LOGINNAME = copyData.LOGINNAME;
                        pasteData.ROOM_ID = data.PasteRoomId;
                        newUserRooms.Add(pasteData);
                    }
                    if (IsNotNullOrEmpty(newUserRooms))
                    {
                        if (!DAOWorker.HisUserRoomDAO.CreateList(newUserRooms))
                        {
                            throw new Exception("Khong tao duoc thiet lap tai khoan phong");
                        }
                    }
                    result = true;
                    resultData = new List<HIS_USER_ROOM>();
                    if (IsNotNullOrEmpty(newUserRooms))
                    {
                        resultData.AddRange(newUserRooms);
                    }
                    if (IsNotNullOrEmpty(pasteUserRooms))
                    {
                        resultData.AddRange(pasteUserRooms);
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
                resultData = null;
            }
            return result;
        }
    }
}
