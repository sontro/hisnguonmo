using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisVaccReactPlace
{
    partial class HisVaccReactPlaceDelete : BusinessBase
    {
        internal HisVaccReactPlaceDelete()
            : base()
        {

        }

        internal HisVaccReactPlaceDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_VACC_REACT_PLACE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisVaccReactPlaceCheck checker = new HisVaccReactPlaceCheck(param);
                valid = valid && IsNotNull(data);
                HIS_VACC_REACT_PLACE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisVaccReactPlaceDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_VACC_REACT_PLACE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisVaccReactPlaceCheck checker = new HisVaccReactPlaceCheck(param);
                List<HIS_VACC_REACT_PLACE> listRaw = new List<HIS_VACC_REACT_PLACE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisVaccReactPlaceDAO.DeleteList(listData);
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
