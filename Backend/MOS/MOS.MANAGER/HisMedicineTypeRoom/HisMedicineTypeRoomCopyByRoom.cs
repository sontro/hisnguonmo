using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisMedicineTypeRoom
{
    class HisMedicineTypeRoomCopyByRoom : BusinessBase
    {
        internal HisMedicineTypeRoomCopyByRoom()
            : base()
        {

        }

        internal HisMedicineTypeRoomCopyByRoom(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(HisMetyRoomCopyByRoomSDO data, ref List<HIS_MEDICINE_TYPE_ROOM> resultData)
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
                    List<HIS_MEDICINE_TYPE_ROOM> newMedicineTypeRooms = new List<HIS_MEDICINE_TYPE_ROOM>();
                    List<HIS_MEDICINE_TYPE_ROOM> copyMedicineTypeRooms = DAOWorker.SqlDAO.GetSql<HIS_MEDICINE_TYPE_ROOM>("SELECT * FROM HIS_MEDICINE_TYPE_ROOM WHERE ROOM_ID = :param1", data.CopyRoomId);
                    List<HIS_MEDICINE_TYPE_ROOM> pasteMedicineTypeRooms = DAOWorker.SqlDAO.GetSql<HIS_MEDICINE_TYPE_ROOM>("SELECT * FROM HIS_MEDICINE_TYPE_ROOM WHERE ROOM_ID = :param1", data.PasteRoomId);
                    if (!IsNotNullOrEmpty(copyMedicineTypeRooms))
                    {
                        V_HIS_ROOM room = Config.HisRoomCFG.DATA.FirstOrDefault(o => o.ID == data.CopyRoomId);
                        string name = room != null ? room.ROOM_NAME : null;
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisUserRoom_PhongChuaCoDuLieuThietLap, name);
                        throw new Exception("Khong co du lieu copyMedicineTypeRooms");
                    }

                    foreach (HIS_MEDICINE_TYPE_ROOM copyData in copyMedicineTypeRooms)
                    {
                        HIS_MEDICINE_TYPE_ROOM serviceMaty = pasteMedicineTypeRooms != null ? pasteMedicineTypeRooms.FirstOrDefault(o => o.MEDICINE_TYPE_ID == copyData.MEDICINE_TYPE_ID) : null;
                        if (serviceMaty != null)
                        {
                            continue;
                        }
                        else
                        {
                            serviceMaty = new HIS_MEDICINE_TYPE_ROOM();
                            serviceMaty.ROOM_ID = data.PasteRoomId;
                            serviceMaty.MEDICINE_TYPE_ID = copyData.MEDICINE_TYPE_ID;
                            newMedicineTypeRooms.Add(serviceMaty);
                        }
                    }
                    if (IsNotNullOrEmpty(newMedicineTypeRooms))
                    {
                        if (!DAOWorker.HisMedicineTypeRoomDAO.CreateList(newMedicineTypeRooms))
                        {
                            throw new Exception("Khong tao duoc HIS_MEDICINE_TYPE_ROOM");
                        }
                    }

                    result = true;
                    resultData = new List<HIS_MEDICINE_TYPE_ROOM>();
                    if (IsNotNullOrEmpty(newMedicineTypeRooms))
                    {
                        resultData.AddRange(newMedicineTypeRooms);
                    }
                    if (IsNotNullOrEmpty(pasteMedicineTypeRooms))
                    {
                        resultData.AddRange(pasteMedicineTypeRooms);
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
