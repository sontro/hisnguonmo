using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisBidMedicineType
{
    partial class HisBidMedicineTypeGet : EntityBase
    {
        public HIS_BID_MEDICINE_TYPE GetByCode(string code, HisBidMedicineTypeSO search)
        {
            HIS_BID_MEDICINE_TYPE result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_BID_MEDICINE_TYPE.AsQueryable().Where(p => p.BID_MEDICINE_TYPE_CODE == code);
                        if (search.listHisBidMedicineTypeExpression != null && search.listHisBidMedicineTypeExpression.Count > 0)
                        {
                            foreach (var item in search.listHisBidMedicineTypeExpression)
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
