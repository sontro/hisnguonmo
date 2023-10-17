using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisBirthCertBook
{
    partial class HisBirthCertBookGet : EntityBase
    {
        public HIS_BIRTH_CERT_BOOK GetByCode(string code, HisBirthCertBookSO search)
        {
            HIS_BIRTH_CERT_BOOK result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_BIRTH_CERT_BOOK.AsQueryable().Where(p => p.BIRTH_CERT_BOOK_CODE == code);
                        if (search.listHisBirthCertBookExpression != null && search.listHisBirthCertBookExpression.Count > 0)
                        {
                            foreach (var item in search.listHisBirthCertBookExpression)
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
