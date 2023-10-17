using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExroRoom
{
    class HisExroRoomCopyByRoom : BusinessBase
    {
        private List<HIS_EXRO_ROOM> recentExroRooms;

        internal HisExroRoomCopyByRoom()
            : base()
        {

        }

        internal HisExroRoomCopyByRoom(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(HisExroRoomCopyByRoomSDO data, ref List<HIS_EXRO_ROOM> resultData)
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
                    List<HIS_EXRO_ROOM> newExroRooms = new List<HIS_EXRO_ROOM>();
                    List<HIS_EXRO_ROOM> oldExroRooms = new List<HIS_EXRO_ROOM>();
                    List<HIS_EXRO_ROOM> copyExroRooms = DAOWorker.SqlDAO.GetSql<HIS_EXRO_ROOM>("SELECT * FROM HIS_EXRO_ROOM WHERE ROOM_ID = :param1", data.CopyRoomId);
                    List<HIS_EXRO_ROOM> pasteExroRooms = DAOWorker.SqlDAO.GetSql<HIS_EXRO_ROOM>("SELECT * FROM HIS_EXRO_ROOM WHERE ROOM_ID = :param1", data.PasteRoomId);
                    if (!IsNotNullOrEmpty(copyExroRooms))
                    {
                        V_HIS_ROOM room = Config.HisRoomCFG.DATA.FirstOrDefault(o => o.ID == data.CopyRoomId);
                        string name = room != null ? room.ROOM_NAME : null;
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisUserRoom_PhongChuaCoDuLieuThietLap, name);
                        throw new Exception("Khong co du lieu copyExroRooms");
                    }

                    foreach (HIS_EXRO_ROOM copyData in copyExroRooms)
                    {
                        HIS_EXRO_ROOM serviceRoom = pasteExroRooms != null ? pasteExroRooms.FirstOrDefault(o => o.EXECUTE_ROOM_ID == copyData.EXECUTE_ROOM_ID) : null;
                        if (serviceRoom != null)
                        {
                            serviceRoom.IS_ALLOW_REQUEST = copyData.IS_ALLOW_REQUEST;
                            serviceRoom.IS_HOLD_ORDER = copyData.IS_HOLD_ORDER;
                            oldExroRooms.Add(serviceRoom);
                        }
                        else
                        {
                            serviceRoom = new HIS_EXRO_ROOM();
                            serviceRoom.ROOM_ID = data.PasteRoomId;
                            serviceRoom.EXECUTE_ROOM_ID = copyData.EXECUTE_ROOM_ID;
                            serviceRoom.IS_ALLOW_REQUEST = copyData.IS_ALLOW_REQUEST;
                            serviceRoom.IS_HOLD_ORDER = copyData.IS_HOLD_ORDER;
                            newExroRooms.Add(serviceRoom);
                        }
                    }
                    if (IsNotNullOrEmpty(newExroRooms))
                    {
                        if (!DAOWorker.HisExroRoomDAO.CreateList(newExroRooms))
                        {
                            throw new Exception("Khong tao duoc HIS_EXRO_ROOM");
                        }
                        this.recentExroRooms = newExroRooms;
                    }

                    if (IsNotNullOrEmpty(oldExroRooms))
                    {
                        if (!DAOWorker.HisExroRoomDAO.UpdateList(oldExroRooms))
                        {
                            throw new Exception("Khong sua duoc HIS_EXRO_ROOM");
                        }
                    }

                    result = true;
                    resultData = new List<HIS_EXRO_ROOM>();
                    if (IsNotNullOrEmpty(newExroRooms))
                    {
                        resultData.AddRange(newExroRooms);
                    }
                    if (IsNotNullOrEmpty(pasteExroRooms))
                    {
                        resultData.AddRange(pasteExroRooms);
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                this.RollbackData();
                result = false;
                resultData = null;
            }
            return result;
        }

        private void RollbackData()
        {
            try
            {
                if (IsNotNullOrEmpty(this.recentExroRooms))
                {
                    if (!DAOWorker.HisExroRoomDAO.TruncateList(this.recentExroRooms))
                    {
                        Logging("Rollback HIS_EXRO_ROOM that bai. Kiem tra lai du lieu", LogType.Warn);
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
