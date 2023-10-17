using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceReqStt
{
    class HisServiceReqSttUpdate : BusinessBase
    {
        internal HisServiceReqSttUpdate()
            : base()
        {

        }

        internal HisServiceReqSttUpdate(Inventec.Core.CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_SERVICE_REQ_STT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisServiceReqSttCheck checker = new HisServiceReqSttCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && IsGreaterThanZero(data.ID);
                valid = valid && checker.IsUnLock(data.ID);
                valid = valid && checker.ExistsCode(data.SERVICE_REQ_STT_CODE, data.ID);
                if (valid)
                {
                    result = DAOWorker.HisServiceReqSttDAO.Update(data);
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

        internal bool UpdateList(List<HIS_SERVICE_REQ_STT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisServiceReqSttCheck checker = new HisServiceReqSttCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
                    valid = valid && checker.ExistsCode(data.SERVICE_REQ_STT_CODE, data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisServiceReqSttDAO.UpdateList(listData);
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
