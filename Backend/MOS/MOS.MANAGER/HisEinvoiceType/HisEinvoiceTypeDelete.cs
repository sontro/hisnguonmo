using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisEinvoiceType
{
    partial class HisEinvoiceTypeDelete : BusinessBase
    {
        internal HisEinvoiceTypeDelete()
            : base()
        {

        }

        internal HisEinvoiceTypeDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_EINVOICE_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisEinvoiceTypeCheck checker = new HisEinvoiceTypeCheck(param);
                valid = valid && IsNotNull(data);
                HIS_EINVOICE_TYPE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisEinvoiceTypeDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_EINVOICE_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisEinvoiceTypeCheck checker = new HisEinvoiceTypeCheck(param);
                List<HIS_EINVOICE_TYPE> listRaw = new List<HIS_EINVOICE_TYPE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisEinvoiceTypeDAO.DeleteList(listData);
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
