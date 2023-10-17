﻿using Inventec.Common.Logging;
using Inventec.Core;
using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace MOS.DAO.HisSereServTein
{
    partial class HisSereServTeinGet : EntityBase
    {
        public List<V_HIS_SERE_SERV_TEIN_1> GetView1(HisSereServTeinSO search, CommonParam param)
        {
            List<V_HIS_SERE_SERV_TEIN_1> list = new List<V_HIS_SERE_SERV_TEIN_1>();
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_SERE_SERV_TEIN_1.AsNoTracking().AsQueryable();
                        if (search.listVHisSereServTein1Expression != null && search.listVHisSereServTein1Expression.Count > 0)
                        {
                            foreach (var item in search.listVHisSereServTein1Expression)
                            {
                                query = query.Where(item);
                            }
                        }


                        if (!string.IsNullOrWhiteSpace(search.OrderField) && !string.IsNullOrWhiteSpace(search.OrderDirection)) { if (!param.Start.HasValue || !param.Limit.HasValue) { list = query.OrderByProperty(search.OrderField, search.OrderDirection).ToList(); } else { param.Count = (from r in query select r).Count(); query = query.OrderByProperty(search.OrderField, search.OrderDirection); if (param.Count <= param.Limit.Value && param.Start.Value == 0) { list = query.ToList(); } else { list = query.Skip(param.Start.Value).Take(param.Limit.Value).ToList(); } } } else { list = query.ToList(); }

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