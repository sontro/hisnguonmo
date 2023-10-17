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
        internal List<V_HIS_TREATMENT_11> GetView11(HisTreatmentView11FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTreatmentDAO.GetView11(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_TREATMENT_11 GetView11ById(long id)
        {
            try
            {
                return GetView11ById(id, new HisTreatmentView11FilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_TREATMENT_11 GetView11ById(long id, HisTreatmentView11FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTreatmentDAO.GetView11ById(id, filter.Query());
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
