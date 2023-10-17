using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.DAO.HisSereServPttt
{
    partial class HisSereServPtttGet : EntityBase
    {
        public V_HIS_SERE_SERV_PTTT_1 GetView1ById(long id, HisSereServPtttSO search)
        {
            V_HIS_SERE_SERV_PTTT_1 result = null;
            try
            {
                bool valid = true;
                valid = valid && IsGreaterThanZero(id);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_SERE_SERV_PTTT_1.AsQueryable().Where(p => p.ID == id);
                        if (search.listVHisSereServPttt1Expression != null && search.listVHisSereServPttt1Expression.Count > 0)
                        {
                            foreach (var item in search.listVHisSereServPttt1Expression)
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
                Logging(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => id), id) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => search), search), LogType.Error);
                LogSystem.Error(ex);
                result = null;
            }
            return result;
        }
    }
}
