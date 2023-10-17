using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisTreatmentEndTypeExt
{
    partial class HisTreatmentEndTypeExtCheck : EntityBase
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
                    count = ctx.HIS_TREATMENT_END_TYPE_EXT.AsQueryable().Where(p => p.TREATMENT_END_TYPE_EXT_CODE.Equals(code) && p.ID != id && (p.IS_DELETE.HasValue || p.IS_DELETE.Value != (short)1)).Count();
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
