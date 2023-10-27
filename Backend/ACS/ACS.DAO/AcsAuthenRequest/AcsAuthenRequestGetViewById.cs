using ACS.DAO.Base;
using ACS.DAO.StagingObject;
using ACS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ACS.DAO.AcsAuthenRequest
{
    partial class AcsAuthenRequestGet : EntityBase
    {
        public V_ACS_AUTHEN_REQUEST GetViewById(long id, AcsAuthenRequestSO search)
        {
            V_ACS_AUTHEN_REQUEST result = null;
            try
            {
                bool valid = true;
                valid = valid && IsGreaterThanZero(id);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_ACS_AUTHEN_REQUEST.AsQueryable().Where(p => p.ID == id);
                        if (search.listVAcsAuthenRequestExpression != null && search.listVAcsAuthenRequestExpression.Count > 0)
                        {
                            foreach (var item in search.listVAcsAuthenRequestExpression)
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
