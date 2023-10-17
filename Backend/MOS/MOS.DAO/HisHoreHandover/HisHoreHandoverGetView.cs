using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisHoreHandover
{
    partial class HisHoreHandoverGet : EntityBase
    {
        public List<V_HIS_HORE_HANDOVER> GetView(HisHoreHandoverSO search, CommonParam param)
        {
            List<V_HIS_HORE_HANDOVER> list = new List<V_HIS_HORE_HANDOVER>();
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_HORE_HANDOVER.AsNoTracking().AsQueryable();
                        if (search.listVHisHoreHandoverExpression != null && search.listVHisHoreHandoverExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisHoreHandoverExpression)
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
    }
}
