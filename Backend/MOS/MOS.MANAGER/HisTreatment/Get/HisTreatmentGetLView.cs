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
        internal List<L_HIS_TREATMENT> GetLView(HisTreatmentLViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTreatmentDAO.GetLView(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal L_HIS_TREATMENT GetLViewById(long id)
        {
            try
            {
                return GetLViewById(id, new HisTreatmentLViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal L_HIS_TREATMENT GetLViewById(long id, HisTreatmentLViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTreatmentDAO.GetLViewById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<L_HIS_TREATMENT_1> GetLView1(HisTreatmentLView1FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTreatmentDAO.GetLView1(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal L_HIS_TREATMENT_1 GetLView1ById(long id)
        {
            try
            {
                return GetLView1ById(id, new HisTreatmentLView1FilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal L_HIS_TREATMENT_1 GetLView1ById(long id, HisTreatmentLView1FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTreatmentDAO.GetLView1ById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<L_HIS_TREATMENT_2> GetLView2(HisTreatmentLView2FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTreatmentDAO.GetLView2(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
                
            }
        }

        internal List<L_HIS_TREATMENT_3> GetLView3(HisTreatmentLView3FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTreatmentDAO.GetLView3(filter.Query(), param);
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
