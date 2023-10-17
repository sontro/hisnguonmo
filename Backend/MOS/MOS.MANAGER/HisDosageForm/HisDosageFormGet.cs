using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDosageForm
{
    partial class HisDosageFormGet : BusinessBase
    {
        internal HisDosageFormGet()
            : base()
        {

        }

        internal HisDosageFormGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_DOSAGE_FORM> Get(HisDosageFormFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisDosageFormDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_DOSAGE_FORM GetById(long id)
        {
            try
            {
                return GetById(id, new HisDosageFormFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_DOSAGE_FORM GetById(long id, HisDosageFormFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisDosageFormDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }
        internal HIS_DOSAGE_FORM GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisDosageFormFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_DOSAGE_FORM GetByCode(string code, HisDosageFormFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisDosageFormDAO.GetByCode(code, filter.Query());
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
