using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisMestRoom
{
    class HisMestRoomCopyByRoom : BusinessBase
    {
        private List<HIS_MEST_ROOM> recentMestRooms;

        internal HisMestRoomCopyByRoom()
            : base()
        {

        }

        internal HisMestRoomCopyByRoom(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(HisMestRoomCopyByRoomSDO data, ref List<HIS_MEST_ROOM> resultData)
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
                    List<HIS_MEST_ROOM> newMestRooms = new List<HIS_MEST_ROOM>();
                    List<HIS_MEST_ROOM> oldMestRooms = new List<HIS_MEST_ROOM>();
                    List<HIS_MEST_ROOM> copyMestRooms = DAOWorker.SqlDAO.GetSql<HIS_MEST_ROOM>("SELECT * FROM HIS_MEST_ROOM WHERE ROOM_ID = :param1", data.CopyRoomId);
                    List<HIS_MEST_ROOM> pasteMestRooms = DAOWorker.SqlDAO.GetSql<HIS_MEST_ROOM>("SELECT * FROM HIS_MEST_ROOM WHERE ROOM_ID = :param1", data.PasteRoomId);
                    if (!IsNotNullOrEmpty(copyMestRooms))
                    {
                        V_HIS_ROOM room = Config.HisRoomCFG.DATA.FirstOrDefault(o => o.ID == data.CopyRoomId);
                        string name = room != null ? room.ROOM_NAME : null;
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisUserRoom_PhongChuaCoDuLieuThietLap, name);
                        throw new Exception("Khong co du lieu copyMestRooms");
                    }

                    foreach (HIS_MEST_ROOM copyData in copyMestRooms)
                    {
                        HIS_MEST_ROOM mestMety = pasteMestRooms != null ? pasteMestRooms.FirstOrDefault(o => o.MEDI_STOCK_ID == copyData.MEDI_STOCK_ID) : null;
                        if (mestMety != null)
                        {
                            if (mestMety.PRIORITY == copyData.PRIORITY)
                            {
                                continue;
                            }
                            mestMety.PRIORITY = copyData.PRIORITY;
                            oldMestRooms.Add(mestMety);
                        }
                        else
                        {
                            mestMety = new HIS_MEST_ROOM();
                            mestMety.ROOM_ID = data.PasteRoomId;
                            mestMety.MEDI_STOCK_ID = copyData.MEDI_STOCK_ID;
                            mestMety.PRIORITY = copyData.PRIORITY;
                            newMestRooms.Add(mestMety);
                        }
                    }
                    if (IsNotNullOrEmpty(newMestRooms))
                    {
                        if (!DAOWorker.HisMestRoomDAO.CreateList(newMestRooms))
                        {
                            throw new Exception("Khong tao duoc HIS_MEST_ROOM");
                        }
                        this.recentMestRooms = newMestRooms;
                    }

                    if (IsNotNullOrEmpty(oldMestRooms))
                    {
                        if (!DAOWorker.HisMestRoomDAO.UpdateList(oldMestRooms))
                        {
                            throw new Exception("Khong sua duoc HIS_MEST_ROOM");
                        }
                    }

                    result = true;
                    resultData = new List<HIS_MEST_ROOM>();
                    if (IsNotNullOrEmpty(newMestRooms))
                    {
                        resultData.AddRange(newMestRooms);
                    }
                    if (IsNotNullOrEmpty(pasteMestRooms))
                    {
                        resultData.AddRange(pasteMestRooms);
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
                if (IsNotNullOrEmpty(this.recentMestRooms))
                {
                    if (!DAOWorker.HisMestRoomDAO.TruncateList(this.recentMestRooms))
                    {
                        Logging("Rollback HIS_MEST_ROOM that bai. Kiem tra lai du lieu", LogType.Warn);
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
