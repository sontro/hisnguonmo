using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisMixedMedicine;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisInfusion
{
    partial class HisInfusionTruncate : BusinessBase
    {
        internal HisInfusionTruncate()
            : base()
        {
        }

        internal HisInfusionTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {
        }

        internal bool Truncate(long id)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisInfusionCheck checker = new HisInfusionCheck(param);
                HIS_INFUSION raw = null;
                valid = valid && checker.VerifyId(id, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    var mixedMedicines = new HisMixedMedicineGet().GetByInfusionId(id);
                    if (IsNotNullOrEmpty(mixedMedicines))
                    {
                        if (!new HisMixedMedicineTruncate().TruncateList(mixedMedicines))
                        {
                            throw new Exception("Xoa thong tin his mixed medicines theo infusionId that bai." + LogUtil.TraceData("id", id));
                        }
                    }

                    result = DAOWorker.HisInfusionDAO.Truncate(raw);
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

        internal bool TruncateList(List<HIS_INFUSION> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisInfusionCheck checker = new HisInfusionCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
                }
                if (valid)
                {
                    var ids = IsNotNullOrEmpty(listData) ? listData.Select(o => o.ID).ToList() : null;
                    var mixedMedicines = IsNotNullOrEmpty(ids) ? new HisMixedMedicineGet().GetByInfusionIds(ids) : null;
                    if (IsNotNullOrEmpty(mixedMedicines))
                    {
                        if (!new HisMixedMedicineTruncate().TruncateList(mixedMedicines))
                        {
                            throw new Exception("Xoa thong tin his mixed medicines theo infusionIds that bai." + LogUtil.TraceData("id", ids));
                        }
                    }
                    result = DAOWorker.HisInfusionDAO.TruncateList(listData);
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
