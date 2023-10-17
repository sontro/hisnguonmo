using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDocumentBook
{
    partial class HisDocumentBookGet : BusinessBase
    {
        internal HIS_DOCUMENT_BOOK GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisDocumentBookFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_DOCUMENT_BOOK GetByCode(string code, HisDocumentBookFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisDocumentBookDAO.GetByCode(code, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }
    }
}
