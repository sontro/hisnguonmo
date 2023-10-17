using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisContraindication
{
    partial class HisContraindicationDelete : BusinessBase
    {
        internal HisContraindicationDelete()
            : base()
        {

        }

        internal HisContraindicationDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_CONTRAINDICATION data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisContraindicationCheck checker = new HisContraindicationCheck(param);
                valid = valid && IsNotNull(data);
                HIS_CONTRAINDICATION raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisContraindicationDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_CONTRAINDICATION> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisContraindicationCheck checker = new HisContraindicationCheck(param);
                List<HIS_CONTRAINDICATION> listRaw = new List<HIS_CONTRAINDICATION>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisContraindicationDAO.DeleteList(listData);
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
