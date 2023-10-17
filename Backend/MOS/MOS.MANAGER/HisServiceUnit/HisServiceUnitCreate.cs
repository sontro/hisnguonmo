using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceUnit
{
    class HisServiceUnitCreate : BusinessBase
    {
        internal HisServiceUnitCreate()
            : base()
        {

        }

        internal HisServiceUnitCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_SERVICE_UNIT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisServiceUnitCheck checker = new HisServiceUnitCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.SERVICE_UNIT_CODE, null);
                if (valid)
                {
                    result = DAOWorker.HisServiceUnitDAO.Create(data);
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

        internal bool CreateList(List<HIS_SERVICE_UNIT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisServiceUnitCheck checker = new HisServiceUnitCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.SERVICE_UNIT_CODE, null);
                }
                if (valid)
                {
                    result = DAOWorker.HisServiceUnitDAO.CreateList(listData);
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
