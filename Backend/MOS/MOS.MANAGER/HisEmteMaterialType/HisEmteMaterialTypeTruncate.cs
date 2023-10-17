using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEmteMaterialType
{
    class HisEmteMaterialTypeTruncate : BusinessBase
    {
        internal HisEmteMaterialTypeTruncate()
            : base()
        {

        }

        internal HisEmteMaterialTypeTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_EMTE_MATERIAL_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisEmteMaterialTypeCheck checker = new HisEmteMaterialTypeCheck(param);
                valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                valid = valid && checker.IsUnLock(data.ID);
                if (valid)
                {
                    result = DAOWorker.HisEmteMaterialTypeDAO.Truncate(data);
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

        internal bool TruncateList(List<HIS_EMTE_MATERIAL_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisEmteMaterialTypeCheck checker = new HisEmteMaterialTypeCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisEmteMaterialTypeDAO.TruncateList(listData);
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
                List<HIS_EMTE_MATERIAL_TYPE> hisEmteMaterialTypes = new HisEmteMaterialTypeGet().GetByExpMestTemplateId(id);
                if (IsNotNullOrEmpty(hisEmteMaterialTypes))
                {
                    result = this.TruncateList(hisEmteMaterialTypes);
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
