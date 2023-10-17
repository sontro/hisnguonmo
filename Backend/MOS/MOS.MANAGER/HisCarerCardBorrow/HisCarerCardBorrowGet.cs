using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisCarerCardBorrow
{
    partial class HisCarerCardBorrowGet : BusinessBase
    {
        internal HisCarerCardBorrowGet()
            : base()
        {

        }

        internal HisCarerCardBorrowGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_CARER_CARD_BORROW> Get(HisCarerCardBorrowFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisCarerCardBorrowDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_CARER_CARD_BORROW GetById(long id)
        {
            try
            {
                return GetById(id, new HisCarerCardBorrowFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_CARER_CARD_BORROW GetById(long id, HisCarerCardBorrowFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisCarerCardBorrowDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_CARER_CARD_BORROW> GetByIds(List<long> ids)
        {
            try
            {
                HisCarerCardBorrowFilterQuery filter = new HisCarerCardBorrowFilterQuery();
                filter.IDs = ids;
                return this.Get(filter);
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
