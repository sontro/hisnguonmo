using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMestTypeUser
{
    internal class HisImpMestTypeUserCopyByLoginname : BusinessBase
    {
        internal HisImpMestTypeUserCopyByLoginname()
            : base()
        {

        }

        internal HisImpMestTypeUserCopyByLoginname(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(HisImpMestTypeUserCopyByLoginnameSDO data, ref List<HIS_IMP_MEST_TYPE_USER> resultData)
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
                    List<HIS_IMP_MEST_TYPE_USER> newImpMestTypeUsers = new List<HIS_IMP_MEST_TYPE_USER>();
                    List<HIS_IMP_MEST_TYPE_USER> copyImpMestTypeUsers = DAOWorker.SqlDAO.GetSql<HIS_IMP_MEST_TYPE_USER>("SELECT * FROM HIS_IMP_MEST_TYPE_USER WHERE LOGINNAME = :param1", data.CopyLoginname);
                    List<HIS_IMP_MEST_TYPE_USER> pasteImpMestTypeUsers = DAOWorker.SqlDAO.GetSql<HIS_IMP_MEST_TYPE_USER>("SELECT * FROM HIS_IMP_MEST_TYPE_USER WHERE LOGINNAME = :param1", data.PasteLoginname);
                    if (!IsNotNullOrEmpty(copyImpMestTypeUsers))
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisUserRoom_TaiKhoanChuaCoDuLieuThietLap, data.CopyLoginname);
                        throw new Exception("Khong co du lieu CopyImpMestTypeUser");
                    }

                    foreach (HIS_IMP_MEST_TYPE_USER copyData in copyImpMestTypeUsers)
                    {
                        if (pasteImpMestTypeUsers == null || pasteImpMestTypeUsers.Exists(e => e.IMP_MEST_TYPE_ID == copyData.IMP_MEST_TYPE_ID))
                        {
                            continue;
                        }
                        HIS_IMP_MEST_TYPE_USER pasteData = new HIS_IMP_MEST_TYPE_USER();
                        pasteData.LOGINNAME = data.PasteLoginname;
                        pasteData.IMP_MEST_TYPE_ID = copyData.IMP_MEST_TYPE_ID;
                        newImpMestTypeUsers.Add(pasteData);
                    }
                    if (IsNotNullOrEmpty(newImpMestTypeUsers))
                    {
                        if (!DAOWorker.HisImpMestTypeUserDAO.CreateList(newImpMestTypeUsers))
                        {
                            throw new Exception("Khong tao duoc thiet lap tai khoan phong");
                        }
                    }
                    result = true;
                    resultData = new List<HIS_IMP_MEST_TYPE_USER>();
                    if (IsNotNullOrEmpty(newImpMestTypeUsers))
                    {
                        resultData.AddRange(newImpMestTypeUsers);
                    }
                    if (IsNotNullOrEmpty(pasteImpMestTypeUsers))
                    {
                        resultData.AddRange(pasteImpMestTypeUsers);
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
