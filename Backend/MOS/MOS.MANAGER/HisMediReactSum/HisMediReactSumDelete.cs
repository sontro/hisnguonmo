using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMediReactSum
{
    partial class HisMediReactSumDelete : BusinessBase
    {
        internal HisMediReactSumDelete()
            : base()
        {

        }

        internal HisMediReactSumDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_MEDI_REACT_SUM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMediReactSumCheck checker = new HisMediReactSumCheck(param);
                valid = valid && IsNotNull(data);
                HIS_MEDI_REACT_SUM raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisMediReactSumDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_MEDI_REACT_SUM> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMediReactSumCheck checker = new HisMediReactSumCheck(param);
                List<HIS_MEDI_REACT_SUM> listRaw = new List<HIS_MEDI_REACT_SUM>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisMediReactSumDAO.DeleteList(listData);
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
