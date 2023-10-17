using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTranPatiForm
{
    class HisTranPatiFormDelete : BusinessBase
    {
        internal HisTranPatiFormDelete()
            : base()
        {

        }

        internal HisTranPatiFormDelete(Inventec.Core.CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_TRAN_PATI_FORM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTranPatiFormCheck checker = new HisTranPatiFormCheck(param);
                valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                valid = valid && checker.IsUnLock(data);
                if (valid)
                {
                    result = DAOWorker.HisTranPatiFormDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_TRAN_PATI_FORM> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisTranPatiFormCheck checker = new HisTranPatiFormCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data);
                }
                if (valid)
                {
                    result = DAOWorker.HisTranPatiFormDAO.DeleteList(listData);
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
