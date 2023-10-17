using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisAccidentPoison
{
    partial class HisAccidentPoisonGet : EntityBase
    {
        public List<V_HIS_ACCIDENT_POISON> GetView(HisAccidentPoisonSO search, CommonParam param)
        {
            List<V_HIS_ACCIDENT_POISON> list = new List<V_HIS_ACCIDENT_POISON>();
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_ACCIDENT_POISON.AsQueryable();
                        if (search.listVHisAccidentPoisonExpression != null && search.listVHisAccidentPoisonExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisAccidentPoisonExpression)
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
    }
}
