using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServicePackage
{
    class HisServicePackageTruncate : BusinessBase
    {
        internal HisServicePackageTruncate()
            : base()
        {

        }

        internal HisServicePackageTruncate(Inventec.Core.CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_SERVICE_PACKAGE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisServicePackageCheck checker = new HisServicePackageCheck(param);
                valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                valid = valid && checker.IsUnLock(data.ID);
                if (valid)
                {
                    result = DAOWorker.HisServicePackageDAO.Truncate(data);
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

        internal bool TruncateList(List<HIS_SERVICE_PACKAGE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisServicePackageCheck checker = new HisServicePackageCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisServicePackageDAO.TruncateList(listData);
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

        internal bool TruncateByServiceId(long serviceId)
        {
            bool result = false;
            try
            {
                List<HIS_SERVICE_PACKAGE> servicePackages = new HisServicePackageGet().GetByServiceId(serviceId);
                if (IsNotNullOrEmpty(servicePackages))
                {
                    result = this.TruncateList(servicePackages);
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
