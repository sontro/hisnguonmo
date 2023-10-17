using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisDocumentBook
{
    partial class HisDocumentBookDelete : BusinessBase
    {
        internal HisDocumentBookDelete()
            : base()
        {

        }

        internal HisDocumentBookDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_DOCUMENT_BOOK data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisDocumentBookCheck checker = new HisDocumentBookCheck(param);
                valid = valid && IsNotNull(data);
                HIS_DOCUMENT_BOOK raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisDocumentBookDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_DOCUMENT_BOOK> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisDocumentBookCheck checker = new HisDocumentBookCheck(param);
                List<HIS_DOCUMENT_BOOK> listRaw = new List<HIS_DOCUMENT_BOOK>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisDocumentBookDAO.DeleteList(listData);
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
