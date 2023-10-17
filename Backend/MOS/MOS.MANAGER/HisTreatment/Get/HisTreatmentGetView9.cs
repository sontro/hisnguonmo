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
        internal List<V_HIS_TREATMENT_9> GetView9(HisTreatmentView9FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTreatmentDAO.GetView9(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_TREATMENT_9 GetView9ById(long id)
        {
            try
            {
                return GetView9ById(id, new HisTreatmentView9FilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_TREATMENT_9 GetView9ById(long id, HisTreatmentView9FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTreatmentDAO.GetView9ById(id, filter.Query());
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
