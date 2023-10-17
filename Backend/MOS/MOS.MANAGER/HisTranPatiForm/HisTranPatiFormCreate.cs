using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTranPatiForm
{
    class HisTranPatiFormCreate : BusinessBase
    {
        internal HisTranPatiFormCreate()
            : base()
        {

        }

        internal HisTranPatiFormCreate(Inventec.Core.CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_TRAN_PATI_FORM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTranPatiFormCheck checker = new HisTranPatiFormCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.TRAN_PATI_FORM_CODE, null);
                if (valid)
                {
                    result = DAOWorker.HisTranPatiFormDAO.Create(data);
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

        internal bool CreateList(List<HIS_TRAN_PATI_FORM> listData)
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
                    valid = valid && checker.ExistsCode(data.TRAN_PATI_FORM_CODE, null);
                }
                if (valid)
                {
                    result = DAOWorker.HisTranPatiFormDAO.CreateList(listData);
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
