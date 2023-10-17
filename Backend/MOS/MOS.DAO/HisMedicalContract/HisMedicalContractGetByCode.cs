using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMedicalContract
{
    partial class HisMedicalContractGet : EntityBase
    {
        public HIS_MEDICAL_CONTRACT GetByCode(string code, HisMedicalContractSO search)
        {
            HIS_MEDICAL_CONTRACT result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_MEDICAL_CONTRACT.AsQueryable().Where(p => p.MEDICAL_CONTRACT_CODE == code);
                        if (search.listHisMedicalContractExpression != null && search.listHisMedicalContractExpression.Count > 0)
                        {
                            foreach (var item in search.listHisMedicalContractExpression)
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
