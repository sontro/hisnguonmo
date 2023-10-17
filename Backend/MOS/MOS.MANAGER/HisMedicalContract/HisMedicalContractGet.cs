using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMedicalContract
{
    partial class HisMedicalContractGet : BusinessBase
    {
        internal HisMedicalContractGet()
            : base()
        {

        }

        internal HisMedicalContractGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_MEDICAL_CONTRACT> Get(HisMedicalContractFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMedicalContractDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MEDICAL_CONTRACT GetById(long id)
        {
            try
            {
                return GetById(id, new HisMedicalContractFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MEDICAL_CONTRACT GetById(long id, HisMedicalContractFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMedicalContractDAO.GetById(id, filter.Query());
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
