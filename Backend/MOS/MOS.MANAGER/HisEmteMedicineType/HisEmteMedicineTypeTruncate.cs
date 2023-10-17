using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEmteMedicineType
{
    class HisEmteMedicineTypeTruncate : BusinessBase
    {
        internal HisEmteMedicineTypeTruncate()
            : base()
        {

        }

        internal HisEmteMedicineTypeTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_EMTE_MEDICINE_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisEmteMedicineTypeCheck checker = new HisEmteMedicineTypeCheck(param);
                valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                valid = valid && checker.IsUnLock(data.ID);
                if (valid)
                {
                    result = DAOWorker.HisEmteMedicineTypeDAO.Truncate(data);
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

        internal bool TruncateList(List<HIS_EMTE_MEDICINE_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisEmteMedicineTypeCheck checker = new HisEmteMedicineTypeCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisEmteMedicineTypeDAO.TruncateList(listData);
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

        internal bool TruncateByExpMestTemplateId(long id)
        {
            bool result = true;
            try
            {
                List<HIS_EMTE_MEDICINE_TYPE> hisEmteMedicineTypes = new HisEmteMedicineTypeGet().GetByExpMestTemplateId(id);
                if (IsNotNullOrEmpty(hisEmteMedicineTypes))
                {
                    result = this.TruncateList(hisEmteMedicineTypes);
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
