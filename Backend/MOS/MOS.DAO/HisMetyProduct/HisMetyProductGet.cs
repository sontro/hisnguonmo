using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMetyProduct
{
    partial class HisMetyProductGet : EntityBase
    {
        public List<HIS_METY_PRODUCT> Get(HisMetyProductSO search, CommonParam param)
        {
            List<HIS_METY_PRODUCT> list = new List<HIS_METY_PRODUCT>();
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_METY_PRODUCT.AsNoTracking().AsQueryable();
                        if (search.listHisMetyProductExpression != null && search.listHisMetyProductExpression.Count > 0)
                        {
                            foreach (var item in search.listHisMetyProductExpression)
                            {
                                query = query.Where(item);
                            }
                        }

                        if (!string.IsNullOrWhiteSpace(search.OrderField) && !string.IsNullOrWhiteSpace(search.OrderDirection))
                        {
                            if (!param.Start.HasValue || !param.Limit.HasValue)
                            {
                                list = query.OrderByProperty(search.OrderField, search.OrderDirection).ToList();
                            }
                            else
                            {
                                param.Count = (from r in query select r).Count();
                                query = query.OrderByProperty(search.OrderField, search.OrderDirection);
                                if (param.Count <= param.Limit.Value && param.Start.Value == 0)
                                {
                                    list = query.ToList();
                                }
                                else
                                {
                                    list = query.Skip(param.Start.Value).Take(param.Limit.Value).ToList();
                                }
                            }
                        }
                        else
                        {
                            list = query.ToList();
                        }
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

        public HIS_METY_PRODUCT GetById(long id, HisMetyProductSO search)
        {
            HIS_METY_PRODUCT result = null;
            try
            {
                bool valid = true;
                valid = valid && IsGreaterThanZero(id);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_METY_PRODUCT.AsQueryable().Where(p => p.ID == id);
                        if (search.listHisMetyProductExpression != null && search.listHisMetyProductExpression.Count > 0)
                        {
                            foreach (var item in search.listHisMetyProductExpression)
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
