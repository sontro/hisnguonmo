using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisBirthCertBook
{
    partial class HisBirthCertBookDelete : BusinessBase
    {
        internal HisBirthCertBookDelete()
            : base()
        {

        }

        internal HisBirthCertBookDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_BIRTH_CERT_BOOK data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBirthCertBookCheck checker = new HisBirthCertBookCheck(param);
                valid = valid && IsNotNull(data);
                HIS_BIRTH_CERT_BOOK raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisBirthCertBookDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_BIRTH_CERT_BOOK> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBirthCertBookCheck checker = new HisBirthCertBookCheck(param);
                List<HIS_BIRTH_CERT_BOOK> listRaw = new List<HIS_BIRTH_CERT_BOOK>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisBirthCertBookDAO.DeleteList(listData);
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
