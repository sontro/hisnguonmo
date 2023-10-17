using SAR.DAO.Base;
using SAR.DAO.StagingObject;
using SAR.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SAR.DAO.SarReportTypeGroup
{
    partial class SarReportTypeGroupGet : EntityBase
    {
        public List<SAR_REPORT_TYPE_GROUP> Get(SarReportTypeGroupSO search, CommonParam param)
        {
            List<SAR_REPORT_TYPE_GROUP> list = new List<SAR_REPORT_TYPE_GROUP>();
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.SAR_REPORT_TYPE_GROUP.AsQueryable();
                        if (search.listSarReportTypeGroupExpression != null && search.listSarReportTypeGroupExpression.Count > 0)
                        {
                            foreach (var item in search.listSarReportTypeGroupExpression)
                            {
                                query = query.Where(item);
                            }
                        }
                        int start = param.Start.HasValue ? param.Start.Value : 0;
                        int limit = param.Limit.HasValue ? param.Limit.Value : Int32.MaxValue;
                        list = query.OrderByProperty(search.OrderField, search.OrderDirection).Skip(start).Take(limit).ToList();
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

        public SAR_REPORT_TYPE_GROUP GetById(long id, SarReportTypeGroupSO search)
        {
            SAR_REPORT_TYPE_GROUP result = null;
            try
            {
                bool valid = true;
                valid = valid && IsGreaterThanZero(id);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.SAR_REPORT_TYPE_GROUP.AsQueryable().Where(p => p.ID == id);
                        if (search.listSarReportTypeGroupExpression != null && search.listSarReportTypeGroupExpression.Count > 0)
                        {
                            foreach (var item in search.listSarReportTypeGroupExpression)
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
