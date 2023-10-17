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
        public List<L_HIS_TREATMENT> GetLView(HisTreatmentSO search, CommonParam param)
        {
            List<L_HIS_TREATMENT> list = new List<L_HIS_TREATMENT>();
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.L_HIS_TREATMENT.AsQueryable();
                        if (search.listLHisTreatmentExpression != null && search.listLHisTreatmentExpression.Count > 0)
                        {
                            foreach (var item in search.listLHisTreatmentExpression)
                            {
                                query = query.Where(item);
                            }
                        }
                        int start = param.Start.HasValue ? param.Start.Value : 0;
                        int limit = param.Limit.HasValue ? param.Limit.Value : Int32.MaxValue;
                        list = (!param.Start.HasValue && !param.Limit.HasValue) ? query.OrderByProperty(search.OrderField, search.OrderDirection).ToList() : query.OrderByProperty(search.OrderField, search.OrderDirection).Skip(start).Take(limit).ToList();
                        param.Count = (from r in query select r).Count();
                    }
                }
            }
            catch (Exception ex)
            {
                Logging(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => search), search) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => param), param), LogType.Error);
                LogSystem.Error(ex);
                list.Clear();
            }
            return list;
        }

        public List<L_HIS_TREATMENT_1> GetLView1(HisTreatmentSO search, CommonParam param)
        {
            List<L_HIS_TREATMENT_1> list = new List<L_HIS_TREATMENT_1>();
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.L_HIS_TREATMENT_1.AsQueryable();
                        if (search.listLHisTreatment1Expression != null && search.listLHisTreatment1Expression.Count > 0)
                        {
                            foreach (var item in search.listLHisTreatment1Expression)
                            {
                                query = query.Where(item);
                            }
                        }
                        int start = param.Start.HasValue ? param.Start.Value : 0;
                        int limit = param.Limit.HasValue ? param.Limit.Value : Int32.MaxValue;
                        list = (!param.Start.HasValue && !param.Limit.HasValue) ? query.OrderByProperty(search.OrderField, search.OrderDirection).ToList() : query.OrderByProperty(search.OrderField, search.OrderDirection).Skip(start).Take(limit).ToList();
                        param.Count = (from r in query select r).Count();
                    }
                }
            }
            catch (Exception ex)
            {
                Logging(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => search), search) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => param), param), LogType.Error);
                LogSystem.Error(ex);
                list.Clear();
            }
            return list;
        }

        public List<L_HIS_TREATMENT_2> GetLView2(HisTreatmentSO search, CommonParam param)
        {
            List<L_HIS_TREATMENT_2> list = new List<L_HIS_TREATMENT_2>();
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.L_HIS_TREATMENT_2.AsQueryable();
                        if (search.listLHisTreatment2Expression != null && search.listLHisTreatment2Expression.Count > 0)
                        {
                            foreach (var item in search.listLHisTreatment2Expression)
                            {
                                query = query.Where(item);
                            }
                        }
                        int start = param.Start.HasValue ? param.Start.Value : 0;
                        int limit = param.Limit.HasValue ? param.Limit.Value : Int32.MaxValue;
                        list = (!param.Start.HasValue && !param.Limit.HasValue) ? query.OrderByProperty(search.OrderField, search.OrderDirection).ToList() : query.OrderByProperty(search.OrderField, search.OrderDirection).Skip(start).Take(limit).ToList();
                        param.Count = (from r in query select r).Count();
                    }
                }
            }
            catch (Exception ex)
            {
                Logging(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => search), search) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => param), param), LogType.Error);
                LogSystem.Error(ex);
                list.Clear();
            }
            return list;
        }

        public List<L_HIS_TREATMENT_3> GetLView3(HisTreatmentSO search, CommonParam param)
        {
            List<L_HIS_TREATMENT_3> list = new List<L_HIS_TREATMENT_3>();
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.L_HIS_TREATMENT_3.AsQueryable();
                        if (search.listLHisTreatment3Expression != null && search.listLHisTreatment3Expression.Count > 0)
                        {
                            foreach (var item in search.listLHisTreatment3Expression)
                            {
                                query = query.Where(item);
                            }
                        }
                        int start = param.Start.HasValue ? param.Start.Value : 0;
                        int limit = param.Limit.HasValue ? param.Limit.Value : Int32.MaxValue;
                        list = (!param.Start.HasValue && !param.Limit.HasValue) ? query.OrderByProperty(search.OrderField, search.OrderDirection).ToList() : query.OrderByProperty(search.OrderField, search.OrderDirection).Skip(start).Take(limit).ToList();
                        param.Count = (from r in query select r).Count();
                    }
                }
            }
            catch (Exception ex)
            {
                Logging(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => search), search) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => param), param), LogType.Error);
                LogSystem.Error(ex);
                list.Clear();
            }
            return list;
        }
    }
}
