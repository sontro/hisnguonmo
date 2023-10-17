using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMedicineUseForm
{
    class HisMedicineUseFormUpdate : BusinessBase
    {
        internal HisMedicineUseFormUpdate()
            : base()
        {

        }

        internal HisMedicineUseFormUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_MEDICINE_USE_FORM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMedicineUseFormCheck checker = new HisMedicineUseFormCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && IsGreaterThanZero(data.ID);
                valid = valid && checker.IsUnLock(data.ID);
                valid = valid && checker.ExistsCode(data.MEDICINE_USE_FORM_CODE, data.ID);
                if (valid)
                {
                    result = DAOWorker.HisMedicineUseFormDAO.Update(data);
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

        internal bool UpdateList(List<HIS_MEDICINE_USE_FORM> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMedicineUseFormCheck checker = new HisMedicineUseFormCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
                    valid = valid && checker.ExistsCode(data.MEDICINE_USE_FORM_CODE, data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisMedicineUseFormDAO.UpdateList(listData);
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
