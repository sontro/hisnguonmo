using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDeathWithin
{
    class HisDeathWithinCreate : BusinessBase
    {
        internal HisDeathWithinCreate()
            : base()
        {

        }

        internal HisDeathWithinCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_DEATH_WITHIN data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisDeathWithinCheck checker = new HisDeathWithinCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.DEATH_WITHIN_CODE, null);
                if (valid)
                {
                    result = DAOWorker.HisDeathWithinDAO.Create(data);
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

        internal bool CreateList(List<HIS_DEATH_WITHIN> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisDeathWithinCheck checker = new HisDeathWithinCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.DEATH_WITHIN_CODE, null);
                }
                if (valid)
                {
                    result = DAOWorker.HisDeathWithinDAO.CreateList(listData);
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
