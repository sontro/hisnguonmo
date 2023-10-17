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
    class HisMedicineTypeRoomCopyByMety : BusinessBase
    {
        internal HisMedicineTypeRoomCopyByMety()
            : base()
        {

        }

        internal HisMedicineTypeRoomCopyByMety(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(HisMetyRoomCopyByMedicineTypeSDO data, ref List<HIS_MEDICINE_TYPE_ROOM> resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(data);
                valid = valid && IsGreaterThanZero(data.CopyMedicineTypeId);
                valid = valid && IsGreaterThanZero(data.PasteMedicineTypeId);
                if (valid)
                {
                    List<HIS_MEDICINE_TYPE_ROOM> newMedicineTypeRooms = new List<HIS_MEDICINE_TYPE_ROOM>();
                    List<HIS_MEDICINE_TYPE_ROOM> copyMedicineTypeRooms = DAOWorker.SqlDAO.GetSql<HIS_MEDICINE_TYPE_ROOM>("SELECT * FROM HIS_MEDICINE_TYPE_ROOM WHERE MEDICINE_TYPE_ID = :param1", data.CopyMedicineTypeId);
                    List<HIS_MEDICINE_TYPE_ROOM> pasteMedicineTypeRooms = DAOWorker.SqlDAO.GetSql<HIS_MEDICINE_TYPE_ROOM>("SELECT * FROM HIS_MEDICINE_TYPE_ROOM WHERE MEDICINE_TYPE_ID = :param1", data.PasteMedicineTypeId);
                    if (!IsNotNullOrEmpty(copyMedicineTypeRooms))
                    {
                        HIS_MEDICINE_TYPE medicineType = Config.HisMedicineTypeCFG.DATA.FirstOrDefault(o => o.ID == data.CopyMedicineTypeId);
                        string name = medicineType != null ? medicineType.MEDICINE_TYPE_NAME : null;
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisMedicineType_ThuocChuaCoDuLieuThietLap, name);
                        throw new Exception("Khong co du lieu copyMedicineTypeRooms");
                    }

                    foreach (HIS_MEDICINE_TYPE_ROOM copyData in copyMedicineTypeRooms)
                    {
                        HIS_MEDICINE_TYPE_ROOM serviceMaty = pasteMedicineTypeRooms != null ? pasteMedicineTypeRooms.FirstOrDefault(o => o.ROOM_ID == copyData.ROOM_ID) : null;
                        if (serviceMaty != null)
                        {
                            continue;
                        }
                        else
                        {
                            serviceMaty = new HIS_MEDICINE_TYPE_ROOM();
                            serviceMaty.MEDICINE_TYPE_ID = data.PasteMedicineTypeId;
                            serviceMaty.ROOM_ID = copyData.ROOM_ID;
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
