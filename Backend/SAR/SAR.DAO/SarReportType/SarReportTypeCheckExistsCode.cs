using SAR.DAO.Base;
using SAR.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace SAR.DAO.SarReportType
{
    partial class SarReportTypeCheck : EntityBase
    {
        public bool ExistsCode(string code, long? id)
        {
            bool result = false;
            try
            {
                code = (code == null ? "" : code);
                id = id.HasValue ? id.Value : -1;
                long count = 0;
                using (var ctx = new AppContext())
                {
                    count = ctx.SAR_REPORT_TYPE.AsQueryable().Where(p => p.REPORT_TYPE_CODE.Equals(code) && p.ID != id && (p.IS_DELETE.HasValue || p.IS_DELETE.Value != (short)1)).Count();
                }
                result = (count > 0);
            }
            catch (Exception)
            {
                Logging(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => code), code) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => id), id), LogType.Error);
                throw;
            }
            return result;
        }
    }
}
