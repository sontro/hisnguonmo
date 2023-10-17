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
    class HisMestRoomCopyByMediStock : BusinessBase
    {
        private List<HIS_MEST_ROOM> recentMestRooms;

        internal HisMestRoomCopyByMediStock()
            : base()
        {

        }

        internal HisMestRoomCopyByMediStock(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(HisMestRoomCopyByMediStockSDO data, ref List<HIS_MEST_ROOM> resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(data);
                valid = valid && IsGreaterThanZero(data.CopyMediStockId);
                valid = valid && IsGreaterThanZero(data.PasteMediStockId);
                if (valid)
                {
                    List<HIS_MEST_ROOM> newMestRooms = new List<HIS_MEST_ROOM>();
                    List<HIS_MEST_ROOM> oldMestRooms = new List<HIS_MEST_ROOM>();
                    List<HIS_MEST_ROOM> copyMestRooms = DAOWorker.SqlDAO.GetSql<HIS_MEST_ROOM>("SELECT * FROM HIS_MEST_ROOM WHERE MEDI_STOCK_ID = :param1", data.CopyMediStockId);
                    List<HIS_MEST_ROOM> pasteMestRooms = DAOWorker.SqlDAO.GetSql<HIS_MEST_ROOM>("SELECT * FROM HIS_MEST_ROOM WHERE MEDI_STOCK_ID = :param1", data.PasteMediStockId);
                    if (!IsNotNullOrEmpty(copyMestRooms))
                    {
                        V_HIS_MEDI_STOCK stock = Config.HisMediStockCFG.DATA.FirstOrDefault(o => o.ID == data.CopyMediStockId);
                        string name = stock != null ? stock.MEDI_STOCK_NAME : null;
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisMediStock_KhoChuaCoDuLieuThietLap, name);
                        throw new Exception("Khong co du lieu copyMestRooms");
                    }

                    foreach (HIS_MEST_ROOM copyData in copyMestRooms)
                    {
                        HIS_MEST_ROOM mestMety = pasteMestRooms != null ? pasteMestRooms.FirstOrDefault(o => o.ROOM_ID == copyData.ROOM_ID) : null;
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
                            mestMety.MEDI_STOCK_ID = data.PasteMediStockId;
                            mestMety.ROOM_ID = copyData.ROOM_ID;
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
