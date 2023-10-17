using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceReqType
{
    class HisServiceReqTypeCreate : BusinessBase
    {
        internal HisServiceReqTypeCreate()
            : base()
        {

        }

        internal HisServiceReqTypeCreate(Inventec.Core.CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_SERVICE_REQ_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisServiceReqTypeCheck checker = new HisServiceReqTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.SERVICE_REQ_TYPE_CODE, null);
                if (valid)
                {
                    result = DAOWorker.HisServiceReqTypeDAO.Create(data);
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

        internal bool CreateList(List<HIS_SERVICE_REQ_TYPE> listData)
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
                    valid = valid && checker.ExistsCode(data.SERVICE_REQ_TYPE_CODE, null);
                }
                if (valid)
                {
                    result = DAOWorker.HisServiceReqTypeDAO.CreateList(listData);
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
