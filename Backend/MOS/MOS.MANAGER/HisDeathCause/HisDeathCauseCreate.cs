using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDeathCause
{
    class HisDeathCauseCreate : BusinessBase
    {
        internal HisDeathCauseCreate()
            : base()
        {

        }

        internal HisDeathCauseCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_DEATH_CAUSE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisDeathCauseCheck checker = new HisDeathCauseCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.DEATH_CAUSE_CODE, null);
                if (valid)
                {
                    result = DAOWorker.HisDeathCauseDAO.Create(data);
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

        internal bool CreateList(List<HIS_DEATH_CAUSE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisDeathCauseCheck checker = new HisDeathCauseCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.DEATH_CAUSE_CODE, null);
                }
                if (valid)
                {
                    result = DAOWorker.HisDeathCauseDAO.CreateList(listData);
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
