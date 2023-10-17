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
        
        internal List<V_HIS_TREATMENT_FEE_2> GetFeeView2(HisTreatmentFeeView2FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTreatmentDAO.GetFeeView2(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<V_HIS_TREATMENT_FEE_2> GetFeeView2ByIds(List<long> ids)
        {
            if (ids != null)
            {
                HisTreatmentFeeView2FilterQuery filter = new HisTreatmentFeeView2FilterQuery();
                filter.IDs = ids;
                return this.GetFeeView2(filter);
            }
            return null;
        }

        internal V_HIS_TREATMENT_FEE_2 GetFeeView2ById(long id)
        {
            try
            {
                return GetFeeView2ById(id, new HisTreatmentFeeView2FilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_TREATMENT_FEE_2 GetFeeView2ById(long id, HisTreatmentFeeView2FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTreatmentDAO.GetFeeView2ById(id, filter.Query());
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
