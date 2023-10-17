﻿using Inventec.Common.Logging;
using Inventec.Core;
using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.DAO.HisServiceReq
{
    partial class HisServiceReqGet : EntityBase
    {
        public V_HIS_SERVICE_REQ_12 GetView12ByCode(string code, HisServiceReqSO search)
        {
            V_HIS_SERVICE_REQ_12 result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_SERVICE_REQ_12.AsQueryable().Where(p => p.SERVICE_REQ_CODE == code);
                        if (search.listVHisServiceReq12Expression != null && search.listVHisServiceReq12Expression.Count > 0)
                        {
                            foreach (var item in search.listVHisServiceReq12Expression)
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