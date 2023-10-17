using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisRestRetrType;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMetyMety
{
    partial class HisMetyMetyTruncate : BusinessBase
    {
        internal HisMetyMetyTruncate()
            : base()
        {

        }

        internal HisMetyMetyTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(long id)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMetyMetyCheck checker = new HisMetyMetyCheck(param);
                HIS_METY_METY raw = null;
                valid = valid && checker.VerifyId(id, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.CheckConstraint(id);
                if (valid)
                {
                    result = DAOWorker.HisMetyMetyDAO.Truncate(raw);
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


        internal bool TruncateList(List<long> ids)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(ids);
                HisMetyMetyCheck checker = new HisMetyMetyCheck(param);
                List<HIS_METY_METY> listRaw = new List<HIS_METY_METY>();
                valid = valid && checker.VerifyIds(ids, listRaw);
                foreach (var data in listRaw)
                {
                    valid = valid && checker.IsUnLock(data);
                    valid = valid && checker.CheckConstraint(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisMetyMetyDAO.TruncateList(listRaw);
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
