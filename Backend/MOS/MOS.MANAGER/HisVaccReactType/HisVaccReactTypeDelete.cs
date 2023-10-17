using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisVaccReactType
{
    partial class HisVaccReactTypeDelete : BusinessBase
    {
        internal HisVaccReactTypeDelete()
            : base()
        {

        }

        internal HisVaccReactTypeDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_VACC_REACT_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisVaccReactTypeCheck checker = new HisVaccReactTypeCheck(param);
                valid = valid && IsNotNull(data);
                HIS_VACC_REACT_TYPE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisVaccReactTypeDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_VACC_REACT_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisVaccReactTypeCheck checker = new HisVaccReactTypeCheck(param);
                List<HIS_VACC_REACT_TYPE> listRaw = new List<HIS_VACC_REACT_TYPE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisVaccReactTypeDAO.DeleteList(listData);
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
