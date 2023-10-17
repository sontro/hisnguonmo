using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisDeathCertBook
{
    partial class HisDeathCertBookDelete : BusinessBase
    {
        internal HisDeathCertBookDelete()
            : base()
        {

        }

        internal HisDeathCertBookDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_DEATH_CERT_BOOK data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisDeathCertBookCheck checker = new HisDeathCertBookCheck(param);
                valid = valid && IsNotNull(data);
                HIS_DEATH_CERT_BOOK raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisDeathCertBookDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_DEATH_CERT_BOOK> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisDeathCertBookCheck checker = new HisDeathCertBookCheck(param);
                List<HIS_DEATH_CERT_BOOK> listRaw = new List<HIS_DEATH_CERT_BOOK>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisDeathCertBookDAO.DeleteList(listData);
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
