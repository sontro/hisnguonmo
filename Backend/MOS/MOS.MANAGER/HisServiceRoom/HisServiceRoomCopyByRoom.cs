using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceRoom
{
    class HisServiceRoomCopyByRoom : BusinessBase
    {
        internal HisServiceRoomCopyByRoom()
            : base()
        {

        }

        internal HisServiceRoomCopyByRoom(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(HisServiceRoomCopyByRoomSDO data, ref List<HIS_SERVICE_ROOM> resultData)
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
                    List<HIS_SERVICE_ROOM> newServiceRooms = new List<HIS_SERVICE_ROOM>();

                    List<HIS_SERVICE_ROOM> copyServiceRooms = DAOWorker.SqlDAO.GetSql<HIS_SERVICE_ROOM>("SELECT * FROM HIS_SERVICE_ROOM WHERE ROOM_ID = :param1", data.CopyRoomId);
                    List<HIS_SERVICE_ROOM> pasteServiceRooms = DAOWorker.SqlDAO.GetSql<HIS_SERVICE_ROOM>("SELECT * FROM HIS_SERVICE_ROOM WHERE ROOM_ID = :param1", data.PasteRoomId);
                    if (!IsNotNullOrEmpty(copyServiceRooms))
                    {
                        V_HIS_ROOM room = Config.HisRoomCFG.DATA.FirstOrDefault(o => o.ID == data.CopyRoomId);
                        string name = room != null ? room.ROOM_NAME : null;
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisUserRoom_PhongChuaCoDuLieuThietLap, name);
                        throw new Exception("Khong co du lieu copyServiceRooms");
                    }

                    foreach (HIS_SERVICE_ROOM copyData in copyServiceRooms)
                    {
                        HIS_SERVICE_ROOM mestMaty = pasteServiceRooms != null ? pasteServiceRooms.FirstOrDefault(o => o.SERVICE_ID
                            == copyData.SERVICE_ID) : null;
                        if (mestMaty != null)
                        {
                            continue;
                        }
                        else
                        {
                            mestMaty = new HIS_SERVICE_ROOM();
                            mestMaty.ROOM_ID = data.PasteRoomId;
                            mestMaty.SERVICE_ID = copyData.SERVICE_ID;
                            mestMaty.IS_PRIORITY = copyData.IS_PRIORITY;
                            newServiceRooms.Add(mestMaty);
                        }
                    }
                    if (IsNotNullOrEmpty(newServiceRooms))
                    {
                        if (!DAOWorker.HisServiceRoomDAO.CreateList(newServiceRooms))
                        {
                            throw new Exception("Khong tao duoc HIS_SERVICE_ROOM");
                        }
                    }

                    result = true;
                    resultData = new List<HIS_SERVICE_ROOM>();
                    if (IsNotNullOrEmpty(newServiceRooms))
                    {
                        resultData.AddRange(newServiceRooms);
                    }
                    if (IsNotNullOrEmpty(pasteServiceRooms))
                    {
                        resultData.AddRange(pasteServiceRooms);
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
