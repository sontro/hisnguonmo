using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisEmteMaterialType;
using MOS.MANAGER.HisEmteMedicineType;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExpMestTemplate
{
    class HisExpMestTemplateTruncate : BusinessBase
    {
        internal HisExpMestTemplateTruncate()
            : base()
        {

        }

        internal HisExpMestTemplateTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(long id)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_EXP_MEST_TEMPLATE raw = null;
                HisExpMestTemplateCheck checker = new HisExpMestTemplateCheck(param);
                valid = valid && checker.VerifyId(id, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    //Xoa het du lieu cu ve thuoc
                    if (!new HisEmteMedicineTypeTruncate(param).TruncateByExpMestTemplateId(id))
                    {
                        throw new Exception("Xoa du lieu HisEmteMedicineType cu that bai");
                    }

                    //Xoa het du lieu cu ve vat tu
                    if (!new HisEmteMaterialTypeTruncate(param).TruncateByExpMestTemplateId(id))
                    {
                        throw new Exception("Xoa du lieu HisEmteMaterialType cu that bai");
                    }
                    result = DAOWorker.HisExpMestTemplateDAO.Truncate(raw);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
    }
}
