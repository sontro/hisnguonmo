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
    internal class HisExecuteRoleUserCopyByLoginname : BusinessBase
    {
        internal HisExecuteRoleUserCopyByLoginname()
            : base()
        {

        }

        internal HisExecuteRoleUserCopyByLoginname(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(HisExecuteRoleUserCopyByLoginnameSDO data, ref List<HIS_EXECUTE_ROLE_USER> resultData)
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
                    List<HIS_EXECUTE_ROLE_USER> newExecuteRoleUsers = new List<HIS_EXECUTE_ROLE_USER>();
                    List<HIS_EXECUTE_ROLE_USER> copyExecuteRoleUsers = DAOWorker.SqlDAO.GetSql<HIS_EXECUTE_ROLE_USER>("SELECT * FROM HIS_EXECUTE_ROLE_USER WHERE LOGINNAME = :param1", data.CopyLoginname);
                    List<HIS_EXECUTE_ROLE_USER> pasteExecuteRoleUsers = DAOWorker.SqlDAO.GetSql<HIS_EXECUTE_ROLE_USER>("SELECT * FROM HIS_EXECUTE_ROLE_USER WHERE LOGINNAME = :param1", data.PasteLoginname);
                    if (!IsNotNullOrEmpty(copyExecuteRoleUsers))
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisUserRoom_TaiKhoanChuaCoDuLieuThietLap, data.CopyLoginname);
                        throw new Exception("Khong co du lieu CopyExecuteRoleUser");
                    }

                    foreach (HIS_EXECUTE_ROLE_USER copyData in copyExecuteRoleUsers)
                    {
                        if (pasteExecuteRoleUsers == null || pasteExecuteRoleUsers.Exists(e => e.EXECUTE_ROLE_ID == copyData.EXECUTE_ROLE_ID))
                        {
                            continue;
                        }
                        HIS_EXECUTE_ROLE_USER pasteData = new HIS_EXECUTE_ROLE_USER();
                        pasteData.LOGINNAME = data.PasteLoginname;
                        pasteData.EXECUTE_ROLE_ID = copyData.EXECUTE_ROLE_ID;
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
