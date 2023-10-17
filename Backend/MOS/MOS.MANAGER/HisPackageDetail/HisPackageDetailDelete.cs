using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisPackageDetail
{
    partial class HisPackageDetailDelete : BusinessBase
    {
        internal HisPackageDetailDelete()
            : base()
        {

        }

        internal HisPackageDetailDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_PACKAGE_DETAIL data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPackageDetailCheck checker = new HisPackageDetailCheck(param);
                valid = valid && IsNotNull(data);
                HIS_PACKAGE_DETAIL raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisPackageDetailDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_PACKAGE_DETAIL> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisPackageDetailCheck checker = new HisPackageDetailCheck(param);
                List<HIS_PACKAGE_DETAIL> listRaw = new List<HIS_PACKAGE_DETAIL>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisPackageDetailDAO.DeleteList(listData);
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
