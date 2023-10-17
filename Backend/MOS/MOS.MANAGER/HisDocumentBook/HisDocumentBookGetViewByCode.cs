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
        internal V_HIS_DOCUMENT_BOOK GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisDocumentBookViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_DOCUMENT_BOOK GetViewByCode(string code, HisDocumentBookViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisDocumentBookDAO.GetViewByCode(code, filter.Query());
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
