using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisImpMest
{
    partial class HisImpMestGet : GetBase
    {
        internal List<V_HIS_IMP_MEST_2> GetView2(HisImpMestView2FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisImpMestDAO.GetView2(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_IMP_MEST_2 GetView2ById(long id)
        {
            try
            {
                return GetView2ById(id, new HisImpMestView2FilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_IMP_MEST_2 GetView2ById(long id, HisImpMestView2FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisImpMestDAO.GetView2ById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_IMP_MEST_2 GetView2ByCode(string code)
        {
            try
            {
                return GetView2ByCode(code, new HisImpMestView2FilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_IMP_MEST_2 GetView2ByCode(string code, HisImpMestView2FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisImpMestDAO.GetView2ByCode(code, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<V_HIS_IMP_MEST_2> GetView2ByTreatmentId(long treatmentId)
        {
            try
            {
                HisImpMestView2FilterQuery filter = new HisImpMestView2FilterQuery();
                filter.TDL_TREATMENT_ID = treatmentId;
                return this.GetView2(filter);
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
