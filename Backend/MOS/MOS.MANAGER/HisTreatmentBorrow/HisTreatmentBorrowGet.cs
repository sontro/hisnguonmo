using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTreatmentBorrow
{
    partial class HisTreatmentBorrowGet : BusinessBase
    {
        internal HisTreatmentBorrowGet()
            : base()
        {

        }

        internal HisTreatmentBorrowGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_TREATMENT_BORROW> Get(HisTreatmentBorrowFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTreatmentBorrowDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_TREATMENT_BORROW GetById(long id)
        {
            try
            {
                return GetById(id, new HisTreatmentBorrowFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_TREATMENT_BORROW GetById(long id, HisTreatmentBorrowFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTreatmentBorrowDAO.GetById(id, filter.Query());
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
