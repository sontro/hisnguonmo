using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisLicenseClass
{
    partial class HisLicenseClassDelete : BusinessBase
    {
        internal HisLicenseClassDelete()
            : base()
        {

        }

        internal HisLicenseClassDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_LICENSE_CLASS data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisLicenseClassCheck checker = new HisLicenseClassCheck(param);
                valid = valid && IsNotNull(data);
                HIS_LICENSE_CLASS raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisLicenseClassDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_LICENSE_CLASS> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisLicenseClassCheck checker = new HisLicenseClassCheck(param);
                List<HIS_LICENSE_CLASS> listRaw = new List<HIS_LICENSE_CLASS>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisLicenseClassDAO.DeleteList(listData);
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
