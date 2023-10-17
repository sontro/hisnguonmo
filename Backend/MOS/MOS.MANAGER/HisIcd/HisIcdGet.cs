using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisIcd
{
    partial class HisIcdGet : GetBase
    {
        internal HisIcdGet()
            : base()
        {

        }

        internal HisIcdGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_ICD> Get(HisIcdFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisIcdDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_ICD GetById(long id)
        {
            try
            {
                return GetById(id, new HisIcdFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_ICD GetById(long id, HisIcdFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisIcdDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_ICD GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisIcdFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_ICD> GetByCodes(List<string> codes)
        {
            try
            {
                HisIcdFilterQuery filter = new HisIcdFilterQuery();
                filter.ICD_CODEs = codes;
                return this.Get(filter);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_ICD GetByCode(string code, HisIcdFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisIcdDAO.GetByCode(code, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_ICD> GetByIcdGroupId(long id)
        {
            HisIcdFilterQuery filter = new HisIcdFilterQuery();
            filter.ICD_GROUP_ID = id;
            return this.Get(filter);
        }

        internal List<V_HIS_ICD> GetView(HisIcdViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisIcdDAO.GetView(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_ICD GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisIcdViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_ICD GetViewByCode(string code, HisIcdViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisIcdDAO.GetViewByCode(code, filter.Query());
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
