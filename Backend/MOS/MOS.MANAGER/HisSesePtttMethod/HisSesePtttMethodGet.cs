using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSesePtttMethod
{
    partial class HisSesePtttMethodGet : BusinessBase
    {
        internal HisSesePtttMethodGet()
            : base()
        {

        }

        internal HisSesePtttMethodGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_SESE_PTTT_METHOD> Get(HisSesePtttMethodFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSesePtttMethodDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_SESE_PTTT_METHOD GetById(long id)
        {
            try
            {
                return GetById(id, new HisSesePtttMethodFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_SESE_PTTT_METHOD GetById(long id, HisSesePtttMethodFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSesePtttMethodDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_SESE_PTTT_METHOD> GetBySereServPtttIds(List<long> ids)
        {
            try
            {
                HisSesePtttMethodFilterQuery filter = new HisSesePtttMethodFilterQuery();
                filter.SERE_SERV_PTTT_IDs = ids;
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
