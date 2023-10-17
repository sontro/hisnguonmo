using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMedicineUseForm
{
    class HisMedicineUseFormCreate : BusinessBase
    {
        internal HisMedicineUseFormCreate()
            : base()
        {

        }

        internal HisMedicineUseFormCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_MEDICINE_USE_FORM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMedicineUseFormCheck checker = new HisMedicineUseFormCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.MEDICINE_USE_FORM_CODE, null);
                if (valid)
                {
                    result = DAOWorker.HisMedicineUseFormDAO.Create(data);
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

        internal bool CreateList(List<HIS_MEDICINE_USE_FORM> listData)
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
                    valid = valid && checker.ExistsCode(data.MEDICINE_USE_FORM_CODE, null);
                }
                if (valid)
                {
                    result = DAOWorker.HisMedicineUseFormDAO.CreateList(listData);
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
