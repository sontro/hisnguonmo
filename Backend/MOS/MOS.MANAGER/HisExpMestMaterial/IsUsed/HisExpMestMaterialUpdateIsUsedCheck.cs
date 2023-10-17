using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.UTILITY;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExpMestMaterial
{
    class HisExpMestMaterialUpdateIsUsedCheck : BusinessBase
    {
        internal HisExpMestMaterialUpdateIsUsedCheck()
            : base()
        {

        }

        internal HisExpMestMaterialUpdateIsUsedCheck(CommonParam paramCheck)
            : base(paramCheck)
        {

        }

        internal bool IsExported(HIS_EXP_MEST_MATERIAL data)
        {
            bool valid = true;
            try
            {
                if (data.IS_EXPORT != Constant.IS_TRUE)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMestMaterial_VatTuChuaThucXuat);
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }
    }
}
