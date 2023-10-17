using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisTreatment
{
    public partial class HisTreatmentDAO : EntityBase
    {
        public List<L_HIS_TREATMENT> GetLView(HisTreatmentSO search, CommonParam param)
        {
            List<L_HIS_TREATMENT> result = new List<L_HIS_TREATMENT>();

            #region Logging Input Data
            try
            {
                Input = Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => param), param) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => search), search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            #endregion

            try
            {
                result = GetWorker.GetLView(search, param);
            }
            catch (Exception ex)
            {
                param.HasException = true;
                Inventec.Common.Logging.LogSystem.Error(ex);
                result.Clear();
            }

            #region Logging
            if (param.HasException)
            {
                LogInOut();
            }
            #endregion

            return result;
        }

        public L_HIS_TREATMENT GetLViewById(long id, HisTreatmentSO search)
        {
            L_HIS_TREATMENT result = null;

            #region Logging Input Data
            try
            {
                Input = Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => id), id) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => search), search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            #endregion

            try
            {
                result = GetWorker.GetLViewById(id, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
                LogInOut();
            }

            return result;
        }

        public List<L_HIS_TREATMENT_1> GetLView1(HisTreatmentSO search, CommonParam param)
        {
            List<L_HIS_TREATMENT_1> result = new List<L_HIS_TREATMENT_1>();

            #region Logging Input Data
            try
            {
                Input = Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => param), param) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => search), search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            #endregion

            try
            {
                result = GetWorker.GetLView1(search, param);
            }
            catch (Exception ex)
            {
                param.HasException = true;
                Inventec.Common.Logging.LogSystem.Error(ex);
                result.Clear();
            }

            #region Logging
            if (param.HasException)
            {
                LogInOut();
            }
            #endregion

            return result;
        }

        public L_HIS_TREATMENT_1 GetLView1ById(long id, HisTreatmentSO search)
        {
            L_HIS_TREATMENT_1 result = null;

            #region Logging Input Data
            try
            {
                Input = Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => id), id) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => search), search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            #endregion

            try
            {
                result = GetWorker.GetLView1ById(id, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
                LogInOut();
            }

            return result;
        }

        public List<L_HIS_TREATMENT_2> GetLView2(HisTreatmentSO search, CommonParam param)
        {
            List<L_HIS_TREATMENT_2> result = new List<L_HIS_TREATMENT_2>();

            #region Logging Input Data
            try
            {
                Input = Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => param), param) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => search), search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            #endregion

            try
            {
                result = GetWorker.GetLView2(search, param);
            }
            catch (Exception ex)
            {
                param.HasException = true;
                Inventec.Common.Logging.LogSystem.Error(ex);
                result.Clear();
            }

            #region Logging
            if (param.HasException)
            {
                LogInOut();
            }
            #endregion

            return result;
        }

        public L_HIS_TREATMENT_2 GetLView2ById(long id, HisTreatmentSO search)
        {
            L_HIS_TREATMENT_2 result = null;

            #region Logging Input Data
            try
            {
                Input = Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => id), id) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => search), search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            #endregion

            try
            {
                result = GetWorker.GetLView2ById(id, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
                LogInOut();
            }

            return result;
        }

        public List<L_HIS_TREATMENT_3> GetLView3(HisTreatmentSO search, CommonParam param)
        {
            List<L_HIS_TREATMENT_3> result = new List<L_HIS_TREATMENT_3>();

            #region Logging Input Data
            try
            {
                Input = Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => param), param) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => search), search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            #endregion

            try
            {
                result = GetWorker.GetLView3(search, param);
            }
            catch (Exception ex)
            {
                param.HasException = true;
                Inventec.Common.Logging.LogSystem.Error(ex);
                result.Clear();
            }

            #region Logging
            if (param.HasException)
            {
                LogInOut();
            }
            #endregion

            return result;
        }

        public L_HIS_TREATMENT_3 GetLView3ById(long id, HisTreatmentSO search)
        {
            L_HIS_TREATMENT_3 result = null;

            #region Logging Input Data
            try
            {
                Input = Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => id), id) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => search), search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            #endregion

            try
            {
                result = GetWorker.GetLView3ById(id, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
                LogInOut();
            }

            return result;
        }
    }
}
