using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSaroExro
{
    partial class HisSaroExroGet : EntityBase
    {
        public V_HIS_SARO_EXRO GetViewByCode(string code, HisSaroExroSO search)
        {
            V_HIS_SARO_EXRO result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_SARO_EXRO.AsQueryable().Where(p => p.SARO_EXRO_CODE == code);
                        if (search.listVHisSaroExroExpression != null && search.listVHisSaroExroExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisSaroExroExpression)
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
