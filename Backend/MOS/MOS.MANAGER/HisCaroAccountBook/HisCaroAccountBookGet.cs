using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisCaroAccountBook
{
    partial class HisCaroAccountBookGet : BusinessBase
    {
        internal HisCaroAccountBookGet()
            : base()
        {

        }

        internal HisCaroAccountBookGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_CARO_ACCOUNT_BOOK> Get(HisCaroAccountBookFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisCaroAccountBookDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_CARO_ACCOUNT_BOOK GetById(long id)
        {
            try
            {
                return GetById(id, new HisCaroAccountBookFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_CARO_ACCOUNT_BOOK GetById(long id, HisCaroAccountBookFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisCaroAccountBookDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_CARO_ACCOUNT_BOOK> GetByAccountBookId(long accountBookId)
        {
            HisCaroAccountBookFilterQuery filter = new HisCaroAccountBookFilterQuery();
            filter.ACCOUNT_BOOK_ID = accountBookId;
            return this.Get(filter);
        }
    }
}
