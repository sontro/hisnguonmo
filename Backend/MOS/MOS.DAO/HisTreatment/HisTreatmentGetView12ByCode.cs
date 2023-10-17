using Inventec.Common.Logging;
using Inventec.Core;
using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.DAO.HisTreatment
{
    partial class HisTreatmentGet : EntityBase
    {
        public V_HIS_TREATMENT_12 GetView12ByCode(string code, HisTreatmentSO search)
        {
            V_HIS_TREATMENT_12 result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_TREATMENT_12.AsQueryable().Where(p => p.TREATMENT_CODE == code);
                        if (search.listVHisTreatment12Expression != null && search.listVHisTreatment12Expression.Count > 0)
                        {
                            foreach (var item in search.listVHisTreatment12Expression)
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
