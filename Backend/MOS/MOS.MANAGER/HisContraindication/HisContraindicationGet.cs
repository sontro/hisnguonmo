using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisContraindication
{
    partial class HisContraindicationGet : BusinessBase
    {
        internal HisContraindicationGet()
            : base()
        {

        }

        internal HisContraindicationGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_CONTRAINDICATION> Get(HisContraindicationFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisContraindicationDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_CONTRAINDICATION GetById(long id)
        {
            try
            {
                return GetById(id, new HisContraindicationFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_CONTRAINDICATION GetById(long id, HisContraindicationFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisContraindicationDAO.GetById(id, filter.Query());
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
