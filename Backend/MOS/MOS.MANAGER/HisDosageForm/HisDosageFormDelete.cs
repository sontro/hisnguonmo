using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisDosageForm
{
    partial class HisDosageFormDelete : BusinessBase
    {
        internal HisDosageFormDelete()
            : base()
        {

        }

        internal HisDosageFormDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_DOSAGE_FORM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisDosageFormCheck checker = new HisDosageFormCheck(param);
                valid = valid && IsNotNull(data);
                HIS_DOSAGE_FORM raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisDosageFormDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_DOSAGE_FORM> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisDosageFormCheck checker = new HisDosageFormCheck(param);
                List<HIS_DOSAGE_FORM> listRaw = new List<HIS_DOSAGE_FORM>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisDosageFormDAO.DeleteList(listData);
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
