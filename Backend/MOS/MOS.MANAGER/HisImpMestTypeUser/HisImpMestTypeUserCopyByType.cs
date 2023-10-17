using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisImpMestType;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMestTypeUser
{
    internal class HisImpMestTypeUserCopyByType : BusinessBase
    {
        internal HisImpMestTypeUserCopyByType()
            : base()
        {

        }

        internal HisImpMestTypeUserCopyByType(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(HisImpMestTypeUserCopyByTypeSDO data, ref List<HIS_IMP_MEST_TYPE_USER> resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(data);
                valid = valid && IsGreaterThanZero(data.CopyImpMestTypeId);
                valid = valid && IsGreaterThanZero(data.PasteImpMestTypeId);
                if (valid)
                {
                    List<HIS_IMP_MEST_TYPE_USER> newImpMestTypeUsers = new List<HIS_IMP_MEST_TYPE_USER>();
                    List<HIS_IMP_MEST_TYPE_USER> copyImpMestTypeUsers = DAOWorker.SqlDAO.GetSql<HIS_IMP_MEST_TYPE_USER>("SELECT * FROM HIS_IMP_MEST_TYPE_USER WHERE IMP_MEST_TYPE_ID = :param1", data.CopyImpMestTypeId);
                    List<HIS_IMP_MEST_TYPE_USER> pasteImpMestTypeUsers = DAOWorker.SqlDAO.GetSql<HIS_IMP_MEST_TYPE_USER>("SELECT * FROM HIS_IMP_MEST_TYPE_USER WHERE IMP_MEST_TYPE_ID = :param1", data.PasteImpMestTypeId);
                    if (!IsNotNullOrEmpty(copyImpMestTypeUsers))
                    {
                        HIS_IMP_MEST_TYPE type = new HisImpMestTypeGet().GetById(data.CopyImpMestTypeId);
                        string name = type != null ? type.IMP_MEST_TYPE_NAME : null;
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisImpMestType_LoaiNhapChuaCoDuLieuThietLap, name);
                        throw new Exception("Khong co du lieu CopyImpMestTypeUser");
                    }

                    foreach (HIS_IMP_MEST_TYPE_USER copyData in copyImpMestTypeUsers)
                    {
                        if (pasteImpMestTypeUsers == null || pasteImpMestTypeUsers.Exists(e => e.LOGINNAME == copyData.LOGINNAME))
                        {
                            continue;
                        }
                        HIS_IMP_MEST_TYPE_USER pasteData = new HIS_IMP_MEST_TYPE_USER();
                        pasteData.LOGINNAME = copyData.LOGINNAME;
                        pasteData.IMP_MEST_TYPE_ID = data.PasteImpMestTypeId;
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
