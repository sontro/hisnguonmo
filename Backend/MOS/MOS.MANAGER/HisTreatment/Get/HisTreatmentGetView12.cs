using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisTreatment.Get;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTreatment
{
    partial class HisTreatmentGet : GetBase
    {
        internal List<V_HIS_TREATMENT_12> GetView12(HisTreatmentView12FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTreatmentDAO.GetView12(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_TREATMENT_12 GetView12ById(long id)
        {
            try
            {
                return GetView12ById(id, new HisTreatmentView12FilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_TREATMENT_12 GetView12ById(long id, HisTreatmentView12FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTreatmentDAO.GetView12ById(id, filter.Query());
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
