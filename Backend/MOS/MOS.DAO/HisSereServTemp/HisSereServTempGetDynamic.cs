using Inventec.Common.Logging;
using Inventec.Core;
using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.DynamicDTO;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.DAO.HisSereServTemp
{
    partial class HisSereServTempGet : EntityBase
    {
        public List<HisSereServTempDTO> GetDynamic(HisSereServTempSO search, CommonParam param)
        {
            List<HisSereServTempDTO> list = new List<HisSereServTempDTO>();
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_SERE_SERV_TEMP.AsNoTracking().AsQueryable();
                        if (search.listHisSereServTempExpression != null && search.listHisSereServTempExpression.Count > 0)
                        {
                            foreach (var item in search.listHisSereServTempExpression)
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

                            //Cac extra_order chi thuc hien khi co truyen vao
                            if (IsNotNullOrEmpty(search.ExtraOrderDirection1) && IsNotNullOrEmpty(search.ExtraOrderField1))
                            {
                                query = query.ThenByProperty(search.ExtraOrderField1, search.ExtraOrderDirection1);
                            }
                            if (IsNotNullOrEmpty(search.ExtraOrderDirection2) && IsNotNullOrEmpty(search.ExtraOrderField2))
                            {
                                query = query.ThenByProperty(search.ExtraOrderField2, search.ExtraOrderDirection2);
                            }
                            if (IsNotNullOrEmpty(search.ExtraOrderDirection3) && IsNotNullOrEmpty(search.ExtraOrderField3))
                            {
                                query = query.ThenByProperty(search.ExtraOrderField3, search.ExtraOrderDirection3);
                            }
                            if (IsNotNullOrEmpty(search.ExtraOrderDirection4) && IsNotNullOrEmpty(search.ExtraOrderField4))
                            {
                                query = query.ThenByProperty(search.ExtraOrderField4, search.ExtraOrderDirection4);
                            }
                        }

                        //Xu ly sau order trang truong hop loi trong DynamicColumns khong co truong order
                        var selector = new DynamicQueryProcessor<HIS_SERE_SERV_TEMP>();
                        selector.SetMemberInfo(search.DynamicColumns);
                        var q = selector.Select<HisSereServTempDTO>(query);
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
