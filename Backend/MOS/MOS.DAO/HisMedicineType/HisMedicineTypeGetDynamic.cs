using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using MOS.DynamicDTO;

namespace MOS.DAO.HisMedicineType
{
    partial class HisMedicineTypeGet : EntityBase
    {
        public List<HisMedicineTypeView1DTO> GetView1Dynamic(HisMedicineTypeSO search, CommonParam param)
        {
            List<HisMedicineTypeView1DTO> list = new List<HisMedicineTypeView1DTO>();
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_MEDICINE_TYPE_1.AsNoTracking().AsQueryable();
                        if (search.listVHisMedicineType1Expression != null && search.listVHisMedicineType1Expression.Count > 0)
                        {
                            foreach (var item in search.listVHisMedicineType1Expression)
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

                            query = query.OrderByProperty(search.OrderField, search.OrderDirection);
                        }

                        //Xu ly sau order trang truong hop loi trong DynamicColumns khong co truong order
                        var selector = new DynamicQueryProcessor<V_HIS_MEDICINE_TYPE_1>();
                        selector.SetMemberInfo(search.DynamicColumns);
                        var q = selector.Select<HisMedicineTypeView1DTO>(query);
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
