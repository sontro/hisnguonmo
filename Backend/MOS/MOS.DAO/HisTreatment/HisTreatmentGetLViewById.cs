using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisTreatment
{
    partial class HisTreatmentGet : EntityBase
    {
        public L_HIS_TREATMENT GetLViewById(long id, HisTreatmentSO search)
        {
            L_HIS_TREATMENT result = null;
            try
            {
                bool valid = true;
                valid = valid && IsGreaterThanZero(id);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.L_HIS_TREATMENT.AsQueryable().Where(p => p.ID == id);
                        if (search.listLHisTreatmentExpression != null && search.listLHisTreatmentExpression.Count > 0)
                        {
                            foreach (var item in search.listLHisTreatmentExpression)
                            {
                                query = query.Where(item);
                            }
                        }
                        result = query.SingleOrDefault();
                    }
                }
            }
            catch (Exception ex)
            {
                Logging(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => id), id) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => search), search), LogType.Error);
                LogSystem.Error(ex);
                result = null;
            }
            return result;
        }

        public L_HIS_TREATMENT_1 GetLView1ById(long id, HisTreatmentSO search)
        {
            L_HIS_TREATMENT_1 result = null;
            try
            {
                bool valid = true;
                valid = valid && IsGreaterThanZero(id);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.L_HIS_TREATMENT_1.AsQueryable().Where(p => p.ID == id);
                        if (search.listLHisTreatment1Expression != null && search.listLHisTreatment1Expression.Count > 0)
                        {
                            foreach (var item in search.listLHisTreatment1Expression)
                            {
                                query = query.Where(item);
                            }
                        }
                        result = query.SingleOrDefault();
                    }
                }
            }
            catch (Exception ex)
            {
                Logging(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => id), id) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => search), search), LogType.Error);
                LogSystem.Error(ex);
                result = null;
            }
            return result;
        }

        public L_HIS_TREATMENT_2 GetLView2ById(long id, HisTreatmentSO search)
        {
            L_HIS_TREATMENT_2 result = null;
            try
            {
                bool valid = true;
                valid = valid && IsGreaterThanZero(id);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.L_HIS_TREATMENT_2.AsQueryable().Where(p => p.ID == id);
                        if (search.listLHisTreatment2Expression != null && search.listLHisTreatment2Expression.Count > 0)
                        {
                            foreach (var item in search.listLHisTreatment2Expression)
                            {
                                query = query.Where(item);
                            }
                        }
                        result = query.SingleOrDefault();
                    }
                }
            }
            catch (Exception ex)
            {
                Logging(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => id), id) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => search), search), LogType.Error);
                LogSystem.Error(ex);
                result = null;
            }
            return result;
        }

        public L_HIS_TREATMENT_3 GetLView3ById(long id, HisTreatmentSO search)
        {
            L_HIS_TREATMENT_3 result = null;
            try
            {
                bool valid = true;
                valid = valid && IsGreaterThanZero(id);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.L_HIS_TREATMENT_3.AsQueryable().Where(p => p.ID == id);
                        if (search.listLHisTreatment3Expression != null && search.listLHisTreatment3Expression.Count > 0)
                        {
                            foreach (var item in search.listLHisTreatment3Expression)
                            {
                                query = query.Where(item);
                            }
                        }
                        result = query.SingleOrDefault();
                    }
                }
            }
            catch (Exception ex)
            {
                Logging(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => id), id) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => search), search), LogType.Error);
                LogSystem.Error(ex);
                result = null;
            }
            return result;
        }
    }
}
