using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceReqStt
{
    class HisServiceReqSttCreate : BusinessBase
    {
        internal HisServiceReqSttCreate()
            : base()
        {

        }

        internal HisServiceReqSttCreate(Inventec.Core.CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_SERVICE_REQ_STT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisServiceReqSttCheck checker = new HisServiceReqSttCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.SERVICE_REQ_STT_CODE, null);
                if (valid)
                {
                    result = DAOWorker.HisServiceReqSttDAO.Create(data);
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

        internal bool CreateList(List<HIS_SERVICE_REQ_STT> listData)
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
                    valid = valid && checker.ExistsCode(data.SERVICE_REQ_STT_CODE, null);
                }
                if (valid)
                {
                    result = DAOWorker.HisServiceReqSttDAO.CreateList(listData);
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
