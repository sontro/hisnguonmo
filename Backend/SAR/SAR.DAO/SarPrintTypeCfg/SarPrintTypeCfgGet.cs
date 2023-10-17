using SAR.DAO.Base;
using SAR.DAO.StagingObject;
using SAR.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SAR.DAO.SarPrintTypeCfg
{
    partial class SarPrintTypeCfgGet : EntityBase
    {
        public List<SAR_PRINT_TYPE_CFG> Get(SarPrintTypeCfgSO search, CommonParam param)
        {
            List<SAR_PRINT_TYPE_CFG> list = new List<SAR_PRINT_TYPE_CFG>();
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.SAR_PRINT_TYPE_CFG.AsQueryable();
                        if (search.listSarPrintTypeCfgExpression != null && search.listSarPrintTypeCfgExpression.Count > 0)
                        {
                            foreach (var item in search.listSarPrintTypeCfgExpression)
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

        public SAR_PRINT_TYPE_CFG GetById(long id, SarPrintTypeCfgSO search)
        {
            SAR_PRINT_TYPE_CFG result = null;
            try
            {
                bool valid = true;
                valid = valid && IsGreaterThanZero(id);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.SAR_PRINT_TYPE_CFG.AsQueryable().Where(p => p.ID == id);
                        if (search.listSarPrintTypeCfgExpression != null && search.listSarPrintTypeCfgExpression.Count > 0)
                        {
                            foreach (var item in search.listSarPrintTypeCfgExpression)
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
