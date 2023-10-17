using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisStentConclude
{
    partial class HisStentConcludeGet : EntityBase
    {
        public List<HIS_STENT_CONCLUDE> Get(HisStentConcludeSO search, CommonParam param)
        {
            List<HIS_STENT_CONCLUDE> list = new List<HIS_STENT_CONCLUDE>();
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_STENT_CONCLUDE.AsNoTracking().AsQueryable();
                        if (search.listHisStentConcludeExpression != null && search.listHisStentConcludeExpression.Count > 0)
                        {
                            foreach (var item in search.listHisStentConcludeExpression)
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

        public HIS_STENT_CONCLUDE GetById(long id, HisStentConcludeSO search)
        {
            HIS_STENT_CONCLUDE result = null;
            try
            {
                bool valid = true;
                valid = valid && IsGreaterThanZero(id);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_STENT_CONCLUDE.AsQueryable().Where(p => p.ID == id);
                        if (search.listHisStentConcludeExpression != null && search.listHisStentConcludeExpression.Count > 0)
                        {
                            foreach (var item in search.listHisStentConcludeExpression)
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
