using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisRoomTypeModule
{
    class HisRoomTypeModuleCopyByModule : BusinessBase
    {
        internal HisRoomTypeModuleCopyByModule()
            : base()
        {

        }

        internal HisRoomTypeModuleCopyByModule(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(HisRotyModuleCopyByModuleSDO data, ref List<HIS_ROOM_TYPE_MODULE> resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(data);
                valid = valid && !String.IsNullOrEmpty(data.CopyModuleLink);
                valid = valid && !String.IsNullOrEmpty(data.PasteModuleLink);
                if (valid)
                {
                    List<HIS_ROOM_TYPE_MODULE> newRoomTypeModules = new List<HIS_ROOM_TYPE_MODULE>();
                    List<HIS_ROOM_TYPE_MODULE> copyRoomTypeModules = DAOWorker.SqlDAO.GetSql<HIS_ROOM_TYPE_MODULE>("SELECT * FROM HIS_ROOM_TYPE_MODULE WHERE MODULE_LINK = :param1", data.CopyModuleLink);
                    List<HIS_ROOM_TYPE_MODULE> pasteRoomTypeModules = DAOWorker.SqlDAO.GetSql<HIS_ROOM_TYPE_MODULE>("SELECT * FROM HIS_ROOM_TYPE_MODULE WHERE MODULE_LINK = :param1", data.PasteModuleLink);
                    if (!IsNotNullOrEmpty(copyRoomTypeModules))
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisRoomTypeModule_ChucNangChuaCoDuLieuThietLap);
                        throw new Exception("Khong co du lieu CopyRoomTypeModule");
                    }

                    foreach (HIS_ROOM_TYPE_MODULE copyData in copyRoomTypeModules)
                    {
                        if (pasteRoomTypeModules == null || pasteRoomTypeModules.Exists(e => e.ROOM_TYPE_ID == copyData.ROOM_TYPE_ID))
                        {
                            continue;
                        }
                        HIS_ROOM_TYPE_MODULE pasteData = new HIS_ROOM_TYPE_MODULE();
                        pasteData.MODULE_LINK = data.PasteModuleLink;
                        pasteData.ROOM_TYPE_ID = copyData.ROOM_TYPE_ID;
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
                resultData = null;
                result = false;
            }
            return result;
        }
    }
}
