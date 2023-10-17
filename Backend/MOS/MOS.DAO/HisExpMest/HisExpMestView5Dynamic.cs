using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using MOS.DynamicDTO;

namespace MOS.DAO.HisExpMest
{
    partial class HisExpMestGet : EntityBase
    {
        public List<HisExpMestView5DTO> GetView5Dynamic(HisExpMestSO search, CommonParam param)
        {
            List<HisExpMestView5DTO> list = new List<HisExpMestView5DTO>();
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_EXP_MEST_5.AsNoTracking().AsQueryable();
                        if (search.listVHisExpMest5Expression != null && search.listVHisExpMest5Expression.Count > 0)
                        {
                            foreach (var item in search.listVHisExpMest5Expression)
                            {
                                query = query.Where(item);
                            }
                        }

                        if (!IsNotNullOrEmpty(search.DynamicColumns))
                        {
                            throw new Exception("DynamicColumns is null");
                        }

                        int? start = null;
                        int? limit = null;

                        if (!string.IsNullOrWhiteSpace(search.OrderField) && !string.IsNullOrWhiteSpace(search.OrderDirection))
                        {
                            if (param.Start.HasValue && param.Limit.HasValue)
                            {
                                start = param.Start.Value;
                                limit = param.Limit.Value;
                                param.Count = (from r in query select r).Count();
                            }
                            //Mac dinh luon thuc hien theo order_field, order_direction de tranh loi o ham Skip o phia duoi
                            query = query.OrderByProperty(search.OrderField, search.OrderDirection);
                        }

                        //Xu ly sau order trang truong hop loi trong DynamicColumns khong co truong order
                        var selector = new DynamicQueryProcessor<V_HIS_EXP_MEST_5>();
                        selector.SetMemberInfo(search.DynamicColumns);
                        var q = selector.Select<HisExpMestView5DTO>(query);
                        if (start.HasValue && limit.HasValue)
                        {
                            list = q.Skip(start.Value).Take(limit.Value).ToList();
                        }
                        else
                        {
                            list = q.ToList();
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
