using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisVaccReactPlace
{
    partial class HisVaccReactPlaceGet : EntityBase
    {
        public HIS_VACC_REACT_PLACE GetByCode(string code, HisVaccReactPlaceSO search)
        {
            HIS_VACC_REACT_PLACE result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_VACC_REACT_PLACE.AsQueryable().Where(p => p.VACC_REACT_PLACE_CODE == code);
                        if (search.listHisVaccReactPlaceExpression != null && search.listHisVaccReactPlaceExpression.Count > 0)
                        {
                            foreach (var item in search.listHisVaccReactPlaceExpression)
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
