using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMediReact
{
    class HisMediReactTruncate : BusinessBase
    {
        internal HisMediReactTruncate()
            : base()
        {

        }

        internal HisMediReactTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_MEDI_REACT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMediReactCheck checker = new HisMediReactCheck(param);
                valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                HIS_MEDI_REACT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisMediReactDAO.Truncate(data);
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

        internal bool TruncateList(List<HIS_MEDI_REACT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMediReactCheck checker = new HisMediReactCheck(param);
                List<HIS_MEDI_REACT> listRaw = new List<HIS_MEDI_REACT>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisMediReactDAO.TruncateList(listData);
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
