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
        internal HisDocumentBookGet()
            : base()
        {

        }

        internal HisDocumentBookGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_DOCUMENT_BOOK> Get(HisDocumentBookFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisDocumentBookDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_DOCUMENT_BOOK GetById(long id)
        {
            try
            {
                return GetById(id, new HisDocumentBookFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_DOCUMENT_BOOK GetById(long id, HisDocumentBookFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisDocumentBookDAO.GetById(id, filter.Query());
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
