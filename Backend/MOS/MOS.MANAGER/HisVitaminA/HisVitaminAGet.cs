using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVitaminA
{
    partial class HisVitaminAGet : BusinessBase
    {
        internal HisVitaminAGet()
            : base()
        {

        }

        internal HisVitaminAGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_VITAMIN_A> Get(HisVitaminAFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisVitaminADAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_VITAMIN_A GetById(long id)
        {
            try
            {
                return GetById(id, new HisVitaminAFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_VITAMIN_A GetById(long id, HisVitaminAFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisVitaminADAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_VITAMIN_A> GetByExpMestId(long expMestId)
        {
            try
            {
                HisVitaminAFilterQuery filter = new HisVitaminAFilterQuery();
                filter.EXP_MEST_ID = expMestId;
                return this.Get(filter);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return null;
        }
    }
}
