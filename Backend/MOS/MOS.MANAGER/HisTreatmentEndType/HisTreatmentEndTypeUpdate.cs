using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisTreatmentEndType
{
    partial class HisTreatmentEndTypeUpdate : BusinessBase
    {
        internal HisTreatmentEndTypeUpdate()
            : base()
        {

        }

        internal HisTreatmentEndTypeUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_TREATMENT_END_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTreatmentEndTypeCheck checker = new HisTreatmentEndTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_TREATMENT_END_TYPE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.TREATMENT_END_TYPE_CODE, data.ID);
                valid = valid && checker.IsAllowUpdate(data, raw);
                if (valid)
                {
                    result = DAOWorker.HisTreatmentEndTypeDAO.Update(data);
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

        internal bool UpdateList(List<HIS_TREATMENT_END_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisTreatmentEndTypeCheck checker = new HisTreatmentEndTypeCheck(param);
                List<HIS_TREATMENT_END_TYPE> listRaw = new List<HIS_TREATMENT_END_TYPE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                valid = valid && checker.CheckAllowUpdateOrDelete(listData);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.TREATMENT_END_TYPE_CODE, data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisTreatmentEndTypeDAO.UpdateList(listData);
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
