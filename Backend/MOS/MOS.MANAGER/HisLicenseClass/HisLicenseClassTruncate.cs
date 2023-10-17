using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisRestRetrType;
using MOS.MANAGER.HisKskPeriodDriver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisLicenseClass
{
    partial class HisLicenseClassTruncate : BusinessBase
    {
        internal HisLicenseClassTruncate()
            : base()
        {

        }

        internal HisLicenseClassTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(long id)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisLicenseClassCheck checker = new HisLicenseClassCheck(param);
                HIS_LICENSE_CLASS raw = null;
                valid = valid && checker.VerifyId(id, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.CheckConstraint(id);
                if (valid)
                {
                    var kskperiodDriver = new HisKskPeriodDriverGet().GetByLicenseClassId(raw.ID);
                    if (IsNotNullOrEmpty(kskperiodDriver))
                    {
                        if (!DAOWorker.HisKskPeriodDriverDAO.TruncateList(kskperiodDriver))
                        {
                            throw new Exception("Xoa thong tin HisKskPeriodDriver that bai." + LogUtil.TraceData("data", kskperiodDriver));
                        }
                    }
                    result = DAOWorker.HisLicenseClassDAO.Truncate(raw);
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

        internal bool TruncateList(List<HIS_LICENSE_CLASS> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisLicenseClassCheck checker = new HisLicenseClassCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
					valid = valid && checker.CheckConstraint(data.ID);
                }
                if (valid)
                {
                    var kskperiodDriver = new HisKskPeriodDriverGet().GetByLicenseClassIds(listData.Select(o => o.ID).ToList());
                    if (IsNotNullOrEmpty(kskperiodDriver))
                    {
                        if (!DAOWorker.HisKskPeriodDriverDAO.TruncateList(kskperiodDriver))
                        {
                            throw new Exception("Xoa danh sach thong tin HisKskPeriodDriver that bai." + LogUtil.TraceData("data", kskperiodDriver));
                        }
                    }
                    result = DAOWorker.HisLicenseClassDAO.TruncateList(listData);
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
