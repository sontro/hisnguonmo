using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.Filter;
using MOS.SDO;
using AutoMapper;
using MOS.MANAGER.Config;

namespace MOS.MANAGER.HisTreatment
{
    partial class HisTreatmentGet : GetBase
    {
        internal List<V_HIS_TREATMENT_5> GetView5(HisTreatmentView5FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTreatmentDAO.GetView5(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_TREATMENT_5 GetView5ById(long id)
        {
            try
            {
                return GetView5ById(id, new HisTreatmentView5FilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_TREATMENT_5 GetView5ById(long id, HisTreatmentView5FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTreatmentDAO.GetView5ById(id, filter.Query());
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
