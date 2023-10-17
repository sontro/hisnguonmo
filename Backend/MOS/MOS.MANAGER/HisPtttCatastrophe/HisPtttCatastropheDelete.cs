using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisPtttCatastrophe
{
    partial class HisPtttCatastropheDelete : BusinessBase
    {
        internal HisPtttCatastropheDelete()
            : base()
        {

        }

        internal HisPtttCatastropheDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_PTTT_CATASTROPHE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPtttCatastropheCheck checker = new HisPtttCatastropheCheck(param);
                valid = valid && IsNotNull(data);
                HIS_PTTT_CATASTROPHE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisPtttCatastropheDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_PTTT_CATASTROPHE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisPtttCatastropheCheck checker = new HisPtttCatastropheCheck(param);
                List<HIS_PTTT_CATASTROPHE> listRaw = new List<HIS_PTTT_CATASTROPHE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisPtttCatastropheDAO.DeleteList(listData);
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
