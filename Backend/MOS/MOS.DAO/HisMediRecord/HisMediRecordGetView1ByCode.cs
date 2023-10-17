using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMediRecord
{
    partial class HisMediRecordGet : EntityBase
    {
        public V_HIS_MEDI_RECORD_1 GetView1ByCode(string code, HisMediRecordSO search)
        {
            V_HIS_MEDI_RECORD_1 result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_MEDI_RECORD_1.AsQueryable().Where(p => p.STORE_CODE == code);
                        if (search.listVHisMediRecord1Expression != null && search.listVHisMediRecord1Expression.Count > 0)
                        {
                            foreach (var item in search.listVHisMediRecord1Expression)
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
