using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisKskAccess
{
    partial class HisKskAccessGet : BusinessBase
    {
        internal HisKskAccessGet()
            : base()
        {

        }

        internal HisKskAccessGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_KSK_ACCESS> Get(HisKskAccessFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisKskAccessDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_KSK_ACCESS GetById(long id)
        {
            try
            {
                return GetById(id, new HisKskAccessFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_KSK_ACCESS GetById(long id, HisKskAccessFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisKskAccessDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_KSK_ACCESS> GetByKskContract(long kskContractId)
        {
            try
            {
                HisKskAccessFilterQuery filter = new HisKskAccessFilterQuery();
                filter.KSK_CONTRACT_ID = kskContractId;
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
