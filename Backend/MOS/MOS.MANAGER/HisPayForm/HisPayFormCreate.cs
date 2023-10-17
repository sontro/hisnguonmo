using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPayForm
{
    class HisPayFormCreate : BusinessBase
    {
        internal HisPayFormCreate()
            : base()
        {

        }

        internal HisPayFormCreate(Inventec.Core.CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_PAY_FORM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPayFormCheck checker = new HisPayFormCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.PAY_FORM_CODE, null);
                if (valid)
                {
                    result = DAOWorker.HisPayFormDAO.Create(data);
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

        internal bool CreateList(List<HIS_PAY_FORM> listData)
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
                    valid = valid && checker.ExistsCode(data.PAY_FORM_CODE, null);
                }
                if (valid)
                {
                    result = DAOWorker.HisPayFormDAO.CreateList(listData);
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
