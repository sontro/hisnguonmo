using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisRoomType;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisRoomTypeModule
{
    class HisRoomTypeModuleCopyByRoomType : BusinessBase
    {
        internal HisRoomTypeModuleCopyByRoomType()
            :base()
        {

        }

        internal HisRoomTypeModuleCopyByRoomType(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(HisRotyModuleCopyByRoomTypeSDO data, ref List<HIS_ROOM_TYPE_MODULE> resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(data);
                valid = valid && IsGreaterThanZero(data.CopyRoomTypeId);
                valid = valid && IsGreaterThanZero(data.PasteRoomTypeId);
                if (valid)
                {
                    List<HIS_ROOM_TYPE_MODULE> newRoomTypeModules = new List<HIS_ROOM_TYPE_MODULE>();
                    List<HIS_ROOM_TYPE_MODULE> copyRoomTypeModules = DAOWorker.SqlDAO.GetSql<HIS_ROOM_TYPE_MODULE>("SELECT * FROM HIS_ROOM_TYPE_MODULE WHERE ROOM_TYPE_ID = :param1", data.CopyRoomTypeId);
                    List<HIS_ROOM_TYPE_MODULE> pasteRoomTypeModules = DAOWorker.SqlDAO.GetSql<HIS_ROOM_TYPE_MODULE>("SELECT * FROM HIS_ROOM_TYPE_MODULE WHERE ROOM_TYPE_ID = :param1", data.PasteRoomTypeId);
                    if (!IsNotNullOrEmpty(copyRoomTypeModules))
                    {
                        HIS_ROOM_TYPE roomType = new HisRoomTypeGet().GetById(data.CopyRoomTypeId);
                        string name = roomType != null ? roomType.ROOM_TYPE_NAME : null;
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisRoomType_LoaiPhongChuaCoDuLieuThietLap, name);
                        throw new Exception("Khong co du lieu CopyRoomTypeModule");
                    }

                    foreach (HIS_ROOM_TYPE_MODULE copyData in copyRoomTypeModules)
                    {
                        if (pasteRoomTypeModules == null || pasteRoomTypeModules.Exists(e => e.MODULE_LINK == copyData.MODULE_LINK))
                        {
                            continue;
                        }
                        HIS_ROOM_TYPE_MODULE pasteData = new HIS_ROOM_TYPE_MODULE();
                        pasteData.MODULE_LINK = copyData.MODULE_LINK;
                        pasteData.ROOM_TYPE_ID = data.PasteRoomTypeId;
                        newRoomTypeModules.Add(pasteData);
                    }
                    if (IsNotNullOrEmpty(newRoomTypeModules))
                    {
                        if (!DAOWorker.HisRoomTypeModuleDAO.CreateList(newRoomTypeModules))
                        {
                            throw new Exception("Khong tao duoc thiet lap tai khoan phong");
                        }
                    }
                    result = true;
                    resultData = new List<HIS_ROOM_TYPE_MODULE>();
                    if (IsNotNullOrEmpty(newRoomTypeModules))
                    {
                        resultData.AddRange(newRoomTypeModules);
                    }
                    if (IsNotNullOrEmpty(pasteRoomTypeModules))
                    {
                        resultData.AddRange(pasteRoomTypeModules);
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
