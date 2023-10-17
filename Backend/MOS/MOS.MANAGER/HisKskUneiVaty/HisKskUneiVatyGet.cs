using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisKskUneiVaty
{
    partial class HisKskUneiVatyGet : BusinessBase
    {
        internal HisKskUneiVatyGet()
            : base()
        {

        }

        internal HisKskUneiVatyGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_KSK_UNEI_VATY> Get(HisKskUneiVatyFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisKskUneiVatyDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_KSK_UNEI_VATY GetById(long id)
        {
            try
            {
                return GetById(id, new HisKskUneiVatyFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_KSK_UNEI_VATY GetById(long id, HisKskUneiVatyFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisKskUneiVatyDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_KSK_UNEI_VATY> GetByKskUnderEighteenId(long kskUnderEighteenId)
        {
            try
            {
                HisKskUneiVatyFilterQuery filter = new HisKskUneiVatyFilterQuery();
                filter.KSK_UNDER_EIGHTEEN_ID = kskUnderEighteenId;
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
