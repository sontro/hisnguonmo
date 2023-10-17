using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExecuteRoleUser
{
    internal class HisExecuteRoleUserCopyByRole : BusinessBase
    {
        internal HisExecuteRoleUserCopyByRole()
            : base()
        {

        }

        internal HisExecuteRoleUserCopyByRole(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(HisExecuteRoleUserCopyByRoleSDO data, ref List<HIS_EXECUTE_ROLE_USER> resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(data);
                valid = valid && IsGreaterThanZero(data.CopyExecuteRoleId);
                valid = valid && IsGreaterThanZero(data.PasteExecuteRoleId);
                if (valid)
                {
                    List<HIS_EXECUTE_ROLE_USER> newExecuteRoleUsers = new List<HIS_EXECUTE_ROLE_USER>();
                    List<HIS_EXECUTE_ROLE_USER> copyExecuteRoleUsers = DAOWorker.SqlDAO.GetSql<HIS_EXECUTE_ROLE_USER>("SELECT * FROM HIS_EXECUTE_ROLE_USER WHERE EXECUTE_ROLE_ID = :param1", data.CopyExecuteRoleId);
                    List<HIS_EXECUTE_ROLE_USER> pasteExecuteRoleUsers = DAOWorker.SqlDAO.GetSql<HIS_EXECUTE_ROLE_USER>("SELECT * FROM HIS_EXECUTE_ROLE_USER WHERE EXECUTE_ROLE_ID = :param1", data.PasteExecuteRoleId);
                    if (!IsNotNullOrEmpty(copyExecuteRoleUsers))
                    {
                        V_HIS_EXECUTE_ROOM room = Config.HisExecuteRoomCFG.DATA.FirstOrDefault(o => o.ID == data.CopyExecuteRoleId);
                        string name = room != null ? room.EXECUTE_ROOM_NAME : null;
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisUserRoom_PhongChuaCoDuLieuThietLap, name);
                        throw new Exception("Khong co du lieu CopyExecuteRoleUser");
                    }

                    foreach (HIS_EXECUTE_ROLE_USER copyData in copyExecuteRoleUsers)
                    {
                        if (pasteExecuteRoleUsers == null || pasteExecuteRoleUsers.Exists(e => e.LOGINNAME == copyData.LOGINNAME))
                        {
                            continue;
                        }
                        HIS_EXECUTE_ROLE_USER pasteData = new HIS_EXECUTE_ROLE_USER();
                        pasteData.LOGINNAME = copyData.LOGINNAME;
                        pasteData.EXECUTE_ROLE_ID = data.PasteExecuteRoleId;
                        newExecuteRoleUsers.Add(pasteData);
                    }
                    if (IsNotNullOrEmpty(newExecuteRoleUsers))
                    {
                        if (!DAOWorker.HisExecuteRoleUserDAO.CreateList(newExecuteRoleUsers))
                        {
                            throw new Exception("Khong tao duoc thiet lap tai khoan phong");
                        }
                    }
                    result = true;
                    resultData = new List<HIS_EXECUTE_ROLE_USER>();
                    if (IsNotNullOrEmpty(newExecuteRoleUsers))
                    {
                        resultData.AddRange(newExecuteRoleUsers);
                    }
                    if (IsNotNullOrEmpty(pasteExecuteRoleUsers))
                    {
                        resultData.AddRange(pasteExecuteRoleUsers);
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
