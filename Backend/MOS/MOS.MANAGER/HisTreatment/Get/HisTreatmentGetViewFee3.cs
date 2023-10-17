using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.Filter;
using MOS.MANAGER.HisDepartmentTran;
using MOS.SDO;
using AutoMapper;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisHeinApproval;
using MOS.UTILITY;

namespace MOS.MANAGER.HisTreatment
{
    partial class HisTreatmentGet : GetBase
    {
        
        internal List<V_HIS_TREATMENT_FEE_3> GetFeeView3(HisTreatmentFeeView3FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTreatmentDAO.GetFeeView3(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<V_HIS_TREATMENT_FEE_3> GetFeeView3ByIds(List<long> ids)
        {
            if (ids != null)
            {
                HisTreatmentFeeView3FilterQuery filter = new HisTreatmentFeeView3FilterQuery();
                filter.IDs = ids;
                return this.GetFeeView3(filter);
            }
            return null;
        }

        internal V_HIS_TREATMENT_FEE_3 GetFeeView3ById(long id)
        {
            try
            {
                return GetFeeView3ById(id, new HisTreatmentFeeView3FilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_TREATMENT_FEE_3 GetFeeView3ById(long id, HisTreatmentFeeView3FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTreatmentDAO.GetFeeView3ById(id, filter.Query());
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
