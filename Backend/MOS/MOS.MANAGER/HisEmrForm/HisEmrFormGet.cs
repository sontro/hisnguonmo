using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEmrForm
{
    partial class HisEmrFormGet : BusinessBase
    {
        internal HisEmrFormGet()
            : base()
        {

        }

        internal HisEmrFormGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_EMR_FORM> Get(HisEmrFormFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisEmrFormDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_EMR_FORM GetById(long id)
        {
            try
            {
                return GetById(id, new HisEmrFormFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_EMR_FORM GetById(long id, HisEmrFormFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisEmrFormDAO.GetById(id, filter.Query());
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
