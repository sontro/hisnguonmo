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
    class HisExroRoomCopyByExro : BusinessBase
    {
        private List<HIS_EXRO_ROOM> recentExroRooms;

        internal HisExroRoomCopyByExro()
            : base()
        {

        }

        internal HisExroRoomCopyByExro(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(HisExroRoomCopyByExroSDO data, ref List<HIS_EXRO_ROOM> resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(data);
                valid = valid && IsGreaterThanZero(data.CopyExecuteRoomId);
                valid = valid && IsGreaterThanZero(data.PasteExecuteRoomId);
                if (valid)
                {
                    List<HIS_EXRO_ROOM> newExroRooms = new List<HIS_EXRO_ROOM>();
                    List<HIS_EXRO_ROOM> oldExroRooms = new List<HIS_EXRO_ROOM>();
                    List<HIS_EXRO_ROOM> copyExroRooms = DAOWorker.SqlDAO.GetSql<HIS_EXRO_ROOM>("SELECT * FROM HIS_EXRO_ROOM WHERE EXECUTE_ROOM_ID = :param1", data.CopyExecuteRoomId);
                    List<HIS_EXRO_ROOM> pasteExroRooms = DAOWorker.SqlDAO.GetSql<HIS_EXRO_ROOM>("SELECT * FROM HIS_EXRO_ROOM WHERE EXECUTE_ROOM_ID = :param1", data.PasteExecuteRoomId);
                    if (!IsNotNullOrEmpty(copyExroRooms))
                    {
                        V_HIS_EXECUTE_ROOM exeRoom = Config.HisExecuteRoomCFG.DATA.FirstOrDefault(o => o.ID == data.CopyExecuteRoomId);
                        string name = exeRoom != null ? exeRoom.EXECUTE_ROOM_NAME : null;
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisUserRoom_PhongChuaCoDuLieuThietLap, name);
                        throw new Exception("Khong co du lieu copyExroRooms");
                    }

                    foreach (HIS_EXRO_ROOM copyData in copyExroRooms)
                    {
                        HIS_EXRO_ROOM serviceRoom = pasteExroRooms != null ? pasteExroRooms.FirstOrDefault(o => o.ROOM_ID == copyData.ROOM_ID) : null;
                        if (serviceRoom != null)
                        {
                            serviceRoom.IS_ALLOW_REQUEST = copyData.IS_ALLOW_REQUEST;
                            serviceRoom.IS_HOLD_ORDER = copyData.IS_HOLD_ORDER;
                            oldExroRooms.Add(serviceRoom);
                        }
                        else
                        {
                            serviceRoom = new HIS_EXRO_ROOM();
                            serviceRoom.EXECUTE_ROOM_ID = data.PasteExecuteRoomId;
                            serviceRoom.ROOM_ID = copyData.ROOM_ID;
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
