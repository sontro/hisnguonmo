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
        internal List<V_HIS_DOCUMENT_BOOK> GetView(HisDocumentBookViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisDocumentBookDAO.GetView(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }


        internal V_HIS_DOCUMENT_BOOK GetViewById(long id)
        {
            try
            {
                return GetViewById(id, new HisDocumentBookViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_DOCUMENT_BOOK GetViewById(long id, HisDocumentBookViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisDocumentBookDAO.GetViewById(id, filter.Query());
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
