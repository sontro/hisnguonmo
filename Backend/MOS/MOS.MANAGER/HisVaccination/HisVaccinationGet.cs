using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVaccination
{
    partial class HisVaccinationGet : BusinessBase
    {
        internal HisVaccinationGet()
            : base()
        {

        }

        internal HisVaccinationGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_VACCINATION> Get(HisVaccinationFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisVaccinationDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_VACCINATION GetById(long id)
        {
            try
            {
                return GetById(id, new HisVaccinationFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_VACCINATION GetById(long id, HisVaccinationFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisVaccinationDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_VACCINATION> GetByBillId(long billId)
        {
            try
            {
                HisVaccinationFilterQuery filter = new HisVaccinationFilterQuery();
                filter.BILL_ID = billId;
                return this.Get(filter);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_VACCINATION> GetByVaccinationExamId(long vaccinationExmId)
        {
            try
            {
                HisVaccinationFilterQuery filter = new HisVaccinationFilterQuery();
                filter.VACCINATION_EXAM_ID = vaccinationExmId;
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
