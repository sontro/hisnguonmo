using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisUserRoom
{
    internal class HisUserRoomCopyByLoginname : BusinessBase
    {
        internal HisUserRoomCopyByLoginname()
            : base()
        {

        }

        internal HisUserRoomCopyByLoginname(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(HisUserRoomCopyByLoginnameSDO data, ref List<HIS_USER_ROOM> resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(data);
                valid = valid && !String.IsNullOrEmpty(data.CopyLoginname);
                valid = valid && !String.IsNullOrEmpty(data.PasteLoginname);
                if (valid)
                {
                    List<HIS_USER_ROOM> newUserRooms = new List<HIS_USER_ROOM>();
                    List<HIS_USER_ROOM> copyUserRooms = DAOWorker.SqlDAO.GetSql<HIS_USER_ROOM>("SELECT * FROM HIS_USER_ROOM WHERE LOGINNAME = :param1", data.CopyLoginname);
                    List<HIS_USER_ROOM> pasteUserRooms = DAOWorker.SqlDAO.GetSql<HIS_USER_ROOM>("SELECT * FROM HIS_USER_ROOM WHERE LOGINNAME = :param1", data.PasteLoginname);
                    if (!IsNotNullOrEmpty(copyUserRooms))
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisUserRoom_TaiKhoanChuaCoDuLieuThietLap, data.CopyLoginname);
                        throw new Exception("Khong co du lieu CopyUserRoom");
                    }

                    foreach (HIS_USER_ROOM copyData in copyUserRooms)
                    {
                        if (pasteUserRooms == null || pasteUserRooms.Exists(e => e.ROOM_ID == copyData.ROOM_ID))
                        {
                            continue;
                        }
                        HIS_USER_ROOM pasteData = new HIS_USER_ROOM();
                        pasteData.LOGINNAME = data.PasteLoginname;
                        pasteData.ROOM_ID = copyData.ROOM_ID;
                        newUserRooms.Add(pasteData);
                    }
                    if (IsNotNullOrEmpty(newUserRooms))
                    {
                        if (!DAOWorker.HisUserRoomDAO.CreateList(newUserRooms))
                        {
                            throw new Exception("Khong tao duoc thiet lap tai khoan phong");
                        }
                    }
                    result = true;
                    resultData = new List<HIS_USER_ROOM>();
                    if (IsNotNullOrEmpty(newUserRooms))
                    {
                        resultData.AddRange(newUserRooms);
                    }
                    if (IsNotNullOrEmpty(pasteUserRooms))
                    {
                        resultData.AddRange(pasteUserRooms);
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
