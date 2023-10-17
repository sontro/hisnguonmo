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
        
        internal List<V_HIS_TREATMENT_FEE_4> GetFeeView4(HisTreatmentFeeView4FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTreatmentDAO.GetFeeView4(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<V_HIS_TREATMENT_FEE_4> GetFeeView4ByIds(List<long> ids)
        {
            if (ids != null)
            {
                HisTreatmentFeeView4FilterQuery filter = new HisTreatmentFeeView4FilterQuery();
                filter.IDs = ids;
                return this.GetFeeView4(filter);
            }
            return null;
        }

        internal V_HIS_TREATMENT_FEE_4 GetFeeView4ById(long id)
        {
            try
            {
                return GetFeeView4ById(id, new HisTreatmentFeeView4FilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_TREATMENT_FEE_4 GetFeeView4ById(long id, HisTreatmentFeeView4FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTreatmentDAO.GetFeeView4ById(id, filter.Query());
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
