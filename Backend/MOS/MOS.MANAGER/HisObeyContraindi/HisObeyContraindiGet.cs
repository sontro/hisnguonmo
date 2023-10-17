using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisObeyContraindi
{
    partial class HisObeyContraindiGet : BusinessBase
    {
        internal HisObeyContraindiGet()
            : base()
        {

        }

        internal HisObeyContraindiGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_OBEY_CONTRAINDI> Get(HisObeyContraindiFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisObeyContraindiDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_OBEY_CONTRAINDI GetById(long id)
        {
            try
            {
                return GetById(id, new HisObeyContraindiFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_OBEY_CONTRAINDI GetById(long id, HisObeyContraindiFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisObeyContraindiDAO.GetById(id, filter.Query());
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
