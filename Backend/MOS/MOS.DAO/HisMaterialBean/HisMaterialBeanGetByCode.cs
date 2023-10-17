using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMaterialBean
{
    partial class HisMaterialBeanGet : EntityBase
    {
        public HIS_MATERIAL_BEAN GetByCode(string code, HisMaterialBeanSO search)
        {
            HIS_MATERIAL_BEAN result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_MATERIAL_BEAN.AsQueryable().Where(p => p.MATERIAL_BEAN_CODE == code);
                        if (search.listHisMaterialBeanExpression != null && search.listHisMaterialBeanExpression.Count > 0)
                        {
                            foreach (var item in search.listHisMaterialBeanExpression)
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
                Logging(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => code), code) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => search), search), LogType.Error);
                LogSystem.Error(ex);
                result = null;
            }
            return result;
        }
    }
}
