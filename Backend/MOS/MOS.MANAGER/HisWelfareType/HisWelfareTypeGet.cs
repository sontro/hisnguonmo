using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisWelfareType
{
    partial class HisWelfareTypeGet : BusinessBase
    {
        internal HisWelfareTypeGet()
            : base()
        {

        }

        internal HisWelfareTypeGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_WELFARE_TYPE> Get(HisWelfareTypeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisWelfareTypeDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_WELFARE_TYPE GetById(long id)
        {
            try
            {
                return GetById(id, new HisWelfareTypeFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_WELFARE_TYPE GetById(long id, HisWelfareTypeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisWelfareTypeDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_WELFARE_TYPE GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisWelfareTypeFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_WELFARE_TYPE GetByCode(string code, HisWelfareTypeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisWelfareTypeDAO.GetByCode(code, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }
    }
}
