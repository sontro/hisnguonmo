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
    class HisSaroExroCopyByExecuteRoom : BusinessBase
    {
        internal HisSaroExroCopyByExecuteRoom()
            : base()
        {

        }

        internal HisSaroExroCopyByExecuteRoom(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(HisSaroExroCopyByExecuteRoomSDO data, ref List<HIS_SARO_EXRO> resultData)
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
                    List<HIS_SARO_EXRO> newSaroExros = new List<HIS_SARO_EXRO>();

                    List<HIS_SARO_EXRO> copySaroExros = DAOWorker.SqlDAO.GetSql<HIS_SARO_EXRO>("SELECT * FROM HIS_SARO_EXRO WHERE EXECUTE_ROOM_ID = :param1", data.CopyExecuteRoomId);
                    List<HIS_SARO_EXRO> pasteSaroExros = DAOWorker.SqlDAO.GetSql<HIS_SARO_EXRO>("SELECT * FROM HIS_SARO_EXRO WHERE EXECUTE_ROOM_ID = :param1", data.PasteExecuteRoomId);
                    if (!IsNotNullOrEmpty(copySaroExros))
                    {
                        V_HIS_EXECUTE_ROOM stock = Config.HisExecuteRoomCFG.DATA.FirstOrDefault(o => o.ID == data.CopyExecuteRoomId);
                        string name = stock != null ? stock.EXECUTE_ROOM_NAME : null;
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisUserRoom_PhongChuaCoDuLieuThietLap, name);
                        throw new Exception("Khong co du lieu copySaroExros");
                    }

                    foreach (HIS_SARO_EXRO copyData in copySaroExros)
                    {
                        HIS_SARO_EXRO mestMaty = pasteSaroExros != null ? pasteSaroExros.FirstOrDefault(o => o.SAMPLE_ROOM_ID == copyData.SAMPLE_ROOM_ID) : null;
                        if (mestMaty != null)
                        {
                            continue;
                        }
                        else
                        {
                            mestMaty = new HIS_SARO_EXRO();
                            mestMaty.EXECUTE_ROOM_ID = data.PasteExecuteRoomId;
                            mestMaty.SAMPLE_ROOM_ID = copyData.SAMPLE_ROOM_ID;
                            newSaroExros.Add(mestMaty);
                        }
                    }
                    if (IsNotNullOrEmpty(newSaroExros))
                    {
                        if (!DAOWorker.HisSaroExroDAO.CreateList(newSaroExros))
                        {
                            throw new Exception("Khong tao duoc HIS_SARO_EXRO");
                        }
                    }

                    result = true;
                    resultData = new List<HIS_SARO_EXRO>();
                    if (IsNotNullOrEmpty(newSaroExros))
                    {
                        resultData.AddRange(newSaroExros);
                    }
                    if (IsNotNullOrEmpty(pasteSaroExros))
                    {
                        resultData.AddRange(pasteSaroExros);
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
