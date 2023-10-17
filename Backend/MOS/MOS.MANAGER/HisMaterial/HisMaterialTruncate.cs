using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisMaterialPaty;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMaterial
{
    class HisMaterialTruncate : BusinessBase
    {
        internal HisMaterialTruncate()
            : base()
        {

        }

        internal HisMaterialTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_MATERIAL data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMaterialCheck checker = new HisMaterialCheck(param);
                valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                valid = valid && checker.IsUnLock(data);
                valid = valid && checker.CheckConstraint(data.ID);
                if (valid)
                {
                    new HisMaterialPatyTruncate(param).TruncateByMaterialId(data.ID);
                    result = DAOWorker.HisMaterialDAO.Truncate(data);
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

        internal bool TruncateList(List<HIS_MATERIAL> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMaterialCheck checker = new HisMaterialCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data);
                    valid = valid && checker.CheckConstraint(data.ID);
                }
                if (valid)
                {
                    foreach (var data in listData)
                    {
                        new HisMaterialPatyTruncate(param).TruncateByMaterialId(data.ID); ;
                    }
                    result = DAOWorker.HisMaterialDAO.TruncateList(listData);
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
