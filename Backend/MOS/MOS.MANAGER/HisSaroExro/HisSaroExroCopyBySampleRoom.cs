using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisSaroExro
{
    class HisSaroExroCopyBySampleRoom : BusinessBase
    {
        internal HisSaroExroCopyBySampleRoom()
            : base()
        {

        }

        internal HisSaroExroCopyBySampleRoom(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(HisSaroExroCopyBySampleRoomSDO data, ref List<HIS_SARO_EXRO> resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(data);
                valid = valid && IsGreaterThanZero(data.CopySampleRoomId);
                valid = valid && IsGreaterThanZero(data.PasteSampleRoomId);
                if (valid)
                {
                    List<HIS_SARO_EXRO> newMestMatys = new List<HIS_SARO_EXRO>();

                    List<HIS_SARO_EXRO> copyMestMatys = DAOWorker.SqlDAO.GetSql<HIS_SARO_EXRO>("SELECT * FROM HIS_SARO_EXRO WHERE SAMPLE_ROOM_ID = :param1", data.CopySampleRoomId);
                    List<HIS_SARO_EXRO> pasteMestMatys = DAOWorker.SqlDAO.GetSql<HIS_SARO_EXRO>("SELECT * FROM HIS_SARO_EXRO WHERE SAMPLE_ROOM_ID = :param1", data.PasteSampleRoomId);
                    if (!IsNotNullOrEmpty(copyMestMatys))
                    {
                        V_HIS_SAMPLE_ROOM room = Config.HisSampleRoomCFG.DATA.FirstOrDefault(o => o.ID == data.CopySampleRoomId);
                        string name = room != null ? room.SAMPLE_ROOM_NAME : null;
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisUserRoom_PhongChuaCoDuLieuThietLap, name);
                        throw new Exception("Khong co du lieu copySaroExros");
                    }

                    foreach (HIS_SARO_EXRO copyData in copyMestMatys)
                    {
                        HIS_SARO_EXRO mestMaty = pasteMestMatys != null ? pasteMestMatys.FirstOrDefault(o => o.EXECUTE_ROOM_ID == copyData.EXECUTE_ROOM_ID) : null;
                        if (mestMaty != null)
                        {
                            continue;
                        }
                        else
                        {
                            mestMaty = new HIS_SARO_EXRO();
                            mestMaty.SAMPLE_ROOM_ID = data.PasteSampleRoomId;
                            mestMaty.EXECUTE_ROOM_ID = copyData.EXECUTE_ROOM_ID;
                            newMestMatys.Add(mestMaty);
                        }
                    }
                    if (IsNotNullOrEmpty(newMestMatys))
                    {
                        if (!DAOWorker.HisSaroExroDAO.CreateList(newMestMatys))
                        {
                            throw new Exception("Khong tao duoc HIS_SARO_EXRO");
                        }
                    }

                    result = true;
                    resultData = new List<HIS_SARO_EXRO>();
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
