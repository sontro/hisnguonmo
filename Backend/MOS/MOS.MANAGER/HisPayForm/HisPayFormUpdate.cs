using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPayForm
{
    class HisPayFormUpdate : BusinessBase
    {
        internal HisPayFormUpdate()
            : base()
        {

        }

        internal HisPayFormUpdate(Inventec.Core.CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_PAY_FORM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPayFormCheck checker = new HisPayFormCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && IsGreaterThanZero(data.ID);
                valid = valid && checker.IsUnLock(data.ID);
                valid = valid && checker.ExistsCode(data.PAY_FORM_CODE, data.ID);
                if (valid)
                {
                    result = DAOWorker.HisPayFormDAO.Update(data);
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

        internal bool UpdateList(List<HIS_PAY_FORM> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisPayFormCheck checker = new HisPayFormCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
                    valid = valid && checker.ExistsCode(data.PAY_FORM_CODE, data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisPayFormDAO.UpdateList(listData);
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
