using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSereServDebt
{
    partial class HisSereServDebtGet : BusinessBase
    {
        internal HisSereServDebtGet()
            : base()
        {

        }

        internal HisSereServDebtGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_SERE_SERV_DEBT> Get(HisSereServDebtFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSereServDebtDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_SERE_SERV_DEBT> GetByDebtIds(List<long> debtIds)
        {
            if (IsNotNullOrEmpty(debtIds))
            {
                HisSereServDebtFilterQuery filter = new HisSereServDebtFilterQuery();
                filter.DEBT_IDs = debtIds;
                return this.Get(filter);
            }
            return null;
        }

        internal List<HIS_SERE_SERV_DEBT> GetNoCancelBySereServIds(List<long> sereServIds)
        {
            try
            {
                if (IsNotNullOrEmpty(sereServIds))
                {
                    HisSereServDebtFilterQuery filter = new HisSereServDebtFilterQuery();
                    filter.SERE_SERV_IDs = sereServIds;
                    filter.IS_CANCEL = false;
                    return this.Get(filter);
                }
                return null;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_SERE_SERV_DEBT> GetNoCancelBySereServId(long sereServId)
        {
            try
            {
                if (sereServId != null)
                {
                    HisSereServDebtFilterQuery filter = new HisSereServDebtFilterQuery();
                    filter.SERE_SERV_ID = sereServId;
                    filter.IS_CANCEL = false;
                    return this.Get(filter);
                }
                return null;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_SERE_SERV_DEBT GetById(long id)
        {
            try
            {
                return GetById(id, new HisSereServDebtFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_SERE_SERV_DEBT GetById(long id, HisSereServDebtFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSereServDebtDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_SERE_SERV_DEBT> GetByDebtId(long debtId)
        {
            if (debtId != null)
            {
                HisSereServDebtFilterQuery filter = new HisSereServDebtFilterQuery();
                filter.DEBT_ID = debtId;
                return this.Get(filter);
            }
            return null;
        }
    }
}
