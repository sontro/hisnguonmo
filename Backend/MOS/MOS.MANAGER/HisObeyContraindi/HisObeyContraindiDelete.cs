using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisObeyContraindi
{
    partial class HisObeyContraindiDelete : BusinessBase
    {
        internal HisObeyContraindiDelete()
            : base()
        {

        }

        internal HisObeyContraindiDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_OBEY_CONTRAINDI data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisObeyContraindiCheck checker = new HisObeyContraindiCheck(param);
                valid = valid && IsNotNull(data);
                HIS_OBEY_CONTRAINDI raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisObeyContraindiDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_OBEY_CONTRAINDI> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisObeyContraindiCheck checker = new HisObeyContraindiCheck(param);
                List<HIS_OBEY_CONTRAINDI> listRaw = new List<HIS_OBEY_CONTRAINDI>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisObeyContraindiDAO.DeleteList(listData);
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
