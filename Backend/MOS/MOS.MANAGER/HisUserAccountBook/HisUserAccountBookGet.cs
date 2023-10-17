using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisUserAccountBook
{
    partial class HisUserAccountBookGet : BusinessBase
    {
        internal HisUserAccountBookGet()
            : base()
        {

        }

        internal HisUserAccountBookGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_USER_ACCOUNT_BOOK> Get(HisUserAccountBookFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisUserAccountBookDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_USER_ACCOUNT_BOOK GetById(long id)
        {
            try
            {
                return GetById(id, new HisUserAccountBookFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_USER_ACCOUNT_BOOK GetById(long id, HisUserAccountBookFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisUserAccountBookDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_USER_ACCOUNT_BOOK> GetByAccountBookId(long id)
        {
            HisUserAccountBookFilterQuery filter = new HisUserAccountBookFilterQuery();
            filter.ACCOUNT_BOOK_ID = id;
            return this.Get(filter);
        }
    }
}
