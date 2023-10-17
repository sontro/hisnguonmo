using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceReqType
{
    class HisServiceReqTypeUpdate : BusinessBase
    {
        internal HisServiceReqTypeUpdate()
            : base()
        {

        }

        internal HisServiceReqTypeUpdate(Inventec.Core.CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_SERVICE_REQ_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisServiceReqTypeCheck checker = new HisServiceReqTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && IsGreaterThanZero(data.ID);
                valid = valid && checker.IsUnLock(data.ID);
                valid = valid && checker.ExistsCode(data.SERVICE_REQ_TYPE_CODE, data.ID);
                if (valid)
                {
                    result = DAOWorker.HisServiceReqTypeDAO.Update(data);
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

        internal bool UpdateList(List<HIS_SERVICE_REQ_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisServiceReqTypeCheck checker = new HisServiceReqTypeCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
                    valid = valid && checker.ExistsCode(data.SERVICE_REQ_TYPE_CODE, data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisServiceReqTypeDAO.UpdateList(listData);
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
