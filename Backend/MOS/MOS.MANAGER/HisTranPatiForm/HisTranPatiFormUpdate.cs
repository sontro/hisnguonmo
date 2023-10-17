using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTranPatiForm
{
    class HisTranPatiFormUpdate : BusinessBase
    {
        internal HisTranPatiFormUpdate()
            : base()
        {

        }

        internal HisTranPatiFormUpdate(Inventec.Core.CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_TRAN_PATI_FORM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTranPatiFormCheck checker = new HisTranPatiFormCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && IsGreaterThanZero(data.ID);
                valid = valid && checker.IsUnLock(data);
                valid = valid && checker.ExistsCode(data.TRAN_PATI_FORM_CODE, data.ID);
                if (valid)
                {
                    result = DAOWorker.HisTranPatiFormDAO.Update(data);
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

        internal bool UpdateList(List<HIS_TRAN_PATI_FORM> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisTranPatiFormCheck checker = new HisTranPatiFormCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data);
                    valid = valid && checker.ExistsCode(data.TRAN_PATI_FORM_CODE, data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisTranPatiFormDAO.UpdateList(listData);
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
