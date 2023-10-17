using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.UTILITY;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExpMestMedicine
{
    class HisExpMestMedicineUpdateIsUsedCheck : BusinessBase
    {
        internal HisExpMestMedicineUpdateIsUsedCheck()
            : base()
        {

        }

        internal HisExpMestMedicineUpdateIsUsedCheck(CommonParam paramCheck)
            : base(paramCheck)
        {

        }

        internal bool IsExported(HIS_EXP_MEST_MEDICINE data)
        {
            bool valid = true;
            try
            {
                if (data.IS_EXPORT != Constant.IS_TRUE)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMestMedicine_ThuocChuaThucXuat);
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
