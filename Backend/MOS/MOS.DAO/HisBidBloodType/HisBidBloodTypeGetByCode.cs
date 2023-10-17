using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisBidBloodType
{
    partial class HisBidBloodTypeGet : EntityBase
    {
        public HIS_BID_BLOOD_TYPE GetByCode(string code, HisBidBloodTypeSO search)
        {
            HIS_BID_BLOOD_TYPE result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_BID_BLOOD_TYPE.AsQueryable().Where(p => p.BID_BLOOD_TYPE_CODE == code);
                        if (search.listHisBidBloodTypeExpression != null && search.listHisBidBloodTypeExpression.Count > 0)
                        {
                            foreach (var item in search.listHisBidBloodTypeExpression)
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
