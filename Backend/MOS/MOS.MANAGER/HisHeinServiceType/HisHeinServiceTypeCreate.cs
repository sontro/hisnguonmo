using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisHeinServiceType
{
    class HisHeinServiceTypeCreate : BusinessBase
    {
        internal HisHeinServiceTypeCreate()
            : base()
        {

        }

        internal HisHeinServiceTypeCreate(Inventec.Core.CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_HEIN_SERVICE_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisHeinServiceTypeCheck checker = new HisHeinServiceTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.HEIN_SERVICE_TYPE_CODE, null);
                if (valid)
                {
                    result = DAOWorker.HisHeinServiceTypeDAO.Create(data);
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

        internal bool CreateList(List<HIS_HEIN_SERVICE_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisHeinServiceTypeCheck checker = new HisHeinServiceTypeCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.HEIN_SERVICE_TYPE_CODE, null);
                }
                if (valid)
                {
                    result = DAOWorker.HisHeinServiceTypeDAO.CreateList(listData);
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
