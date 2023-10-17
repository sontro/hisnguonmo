using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisLicenseClass
{
    partial class HisLicenseClassGet : EntityBase
    {
        public V_HIS_LICENSE_CLASS GetViewByCode(string code, HisLicenseClassSO search)
        {
            V_HIS_LICENSE_CLASS result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_LICENSE_CLASS.AsQueryable().Where(p => p.LICENSE_CLASS_CODE == code);
                        if (search.listVHisLicenseClassExpression != null && search.listVHisLicenseClassExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisLicenseClassExpression)
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
