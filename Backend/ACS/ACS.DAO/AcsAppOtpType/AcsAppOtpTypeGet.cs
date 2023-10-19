using ACS.DAO.Base;
using ACS.DAO.StagingObject;
using ACS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ACS.DAO.AcsAppOtpType
{
    partial class AcsAppOtpTypeGet : EntityBase
    {
        public List<ACS_APP_OTP_TYPE> Get(AcsAppOtpTypeSO search, CommonParam param)
        {
            List<ACS_APP_OTP_TYPE> list = new List<ACS_APP_OTP_TYPE>();
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.ACS_APP_OTP_TYPE.AsQueryable();
                        if (search.listAcsAppOtpTypeExpression != null && search.listAcsAppOtpTypeExpression.Count > 0)
                        {
                            foreach (var item in search.listAcsAppOtpTypeExpression)
                            {
                                query = query.Where(item);
                            }
                        }
                        int start = param.Start.HasValue ? param.Start.Value : 0;
                        int limit = param.Limit.HasValue ? param.Limit.Value : Int32.MaxValue;
                        list = query.OrderByProperty(search.OrderField, search.OrderDirection).Skip(start).Take(limit).ToList();
                        param.Count = (from r in query select r).Count();
                    }
                }
            }
            catch (Exception ex)
            {
                Logging(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => search), search) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => param), param), LogType.Error);
                LogSystem.Error(ex);
                list.Clear();
            }
            return list;
        }

        public ACS_APP_OTP_TYPE GetById(long id, AcsAppOtpTypeSO search)
        {
            ACS_APP_OTP_TYPE result = null;
            try
            {
                bool valid = true;
                valid = valid && IsGreaterThanZero(id);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.ACS_APP_OTP_TYPE.AsQueryable().Where(p => p.ID == id);
                        if (search.listAcsAppOtpTypeExpression != null && search.listAcsAppOtpTypeExpression.Count > 0)
                        {
                            foreach (var item in search.listAcsAppOtpTypeExpression)
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
