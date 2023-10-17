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
        internal List<V_HIS_TREATMENT_10> GetView10(HisTreatmentView10FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTreatmentDAO.GetView10(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_TREATMENT_10 GetView10ById(long id)
        {
            try
            {
                return GetView10ById(id, new HisTreatmentView10FilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_TREATMENT_10 GetView10ById(long id, HisTreatmentView10FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTreatmentDAO.GetView10ById(id, filter.Query());
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
