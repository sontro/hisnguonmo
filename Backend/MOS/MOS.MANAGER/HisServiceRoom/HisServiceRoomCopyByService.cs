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
    class HisServiceRoomCopyByService : BusinessBase
    {
        internal HisServiceRoomCopyByService()
            : base()
        {

        }

        internal HisServiceRoomCopyByService(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(HisServiceRoomCopyByServiceSDO data, ref List<HIS_SERVICE_ROOM> resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(data);
                valid = valid && IsGreaterThanZero(data.CopyServiceId);
                valid = valid && IsGreaterThanZero(data.PasteServiceId);
                if (valid)
                {
                    List<HIS_SERVICE_ROOM> newMestMatys = new List<HIS_SERVICE_ROOM>();

                    List<HIS_SERVICE_ROOM> copyMestMatys = DAOWorker.SqlDAO.GetSql<HIS_SERVICE_ROOM>("SELECT * FROM HIS_SERVICE_ROOM WHERE SERVICE_ID = :param1", data.CopyServiceId);
                    List<HIS_SERVICE_ROOM> pasteMestMatys = DAOWorker.SqlDAO.GetSql<HIS_SERVICE_ROOM>("SELECT * FROM HIS_SERVICE_ROOM WHERE SERVICE_ID = :param1", data.PasteServiceId);
                    if (!IsNotNullOrEmpty(copyMestMatys))
                    {
                        V_HIS_SERVICE stock = Config.HisServiceCFG.DATA_VIEW.FirstOrDefault(o => o.ID == data.CopyServiceId);
                        string name = stock != null ? stock.SERVICE_NAME : null;
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisService_DichVuChuaCoDuLieuThietLap, name);
                        throw new Exception("Khong co du lieu copyServiceRooms");
                    }

                    foreach (HIS_SERVICE_ROOM copyData in copyMestMatys)
                    {
                        HIS_SERVICE_ROOM mestMaty = pasteMestMatys != null ? pasteMestMatys.FirstOrDefault(o => o.ROOM_ID
                            == copyData.ROOM_ID) : null;
                        if (mestMaty != null)
                        {
                            continue;
                        }
                        else
                        {
                            mestMaty = new HIS_SERVICE_ROOM();
                            mestMaty.SERVICE_ID = data.PasteServiceId;
                            mestMaty.ROOM_ID = copyData.ROOM_ID;
                            mestMaty.IS_PRIORITY = copyData.IS_PRIORITY;
                            newMestMatys.Add(mestMaty);
                        }
                    }
                    if (IsNotNullOrEmpty(newMestMatys))
                    {
                        if (!DAOWorker.HisServiceRoomDAO.CreateList(newMestMatys))
                        {
                            throw new Exception("Khong tao duoc HIS_SERVICE_ROOM");
                        }
                    }

                    result = true;
                    resultData = new List<HIS_SERVICE_ROOM>();
                    if (IsNotNullOrEmpty(newMestMatys))
                    {
                        resultData.AddRange(newMestMatys);
                    }
                    if (IsNotNullOrEmpty(pasteMestMatys))
                    {
                        resultData.AddRange(pasteMestMatys);
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
