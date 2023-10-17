using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMediRecordType
{
    partial class HisMediRecordTypeDelete : BusinessBase
    {
        internal HisMediRecordTypeDelete()
            : base()
        {

        }

        internal HisMediRecordTypeDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_MEDI_RECORD_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMediRecordTypeCheck checker = new HisMediRecordTypeCheck(param);
                valid = valid && IsNotNull(data);
                HIS_MEDI_RECORD_TYPE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisMediRecordTypeDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_MEDI_RECORD_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMediRecordTypeCheck checker = new HisMediRecordTypeCheck(param);
                List<HIS_MEDI_RECORD_TYPE> listRaw = new List<HIS_MEDI_RECORD_TYPE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisMediRecordTypeDAO.DeleteList(listData);
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
