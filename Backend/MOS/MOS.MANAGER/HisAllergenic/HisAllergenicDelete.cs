using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisAllergenic
{
    partial class HisAllergenicDelete : BusinessBase
    {
        internal HisAllergenicDelete()
            : base()
        {

        }

        internal HisAllergenicDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_ALLERGENIC data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAllergenicCheck checker = new HisAllergenicCheck(param);
                valid = valid && IsNotNull(data);
                HIS_ALLERGENIC raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisAllergenicDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_ALLERGENIC> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisAllergenicCheck checker = new HisAllergenicCheck(param);
                List<HIS_ALLERGENIC> listRaw = new List<HIS_ALLERGENIC>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisAllergenicDAO.DeleteList(listData);
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
