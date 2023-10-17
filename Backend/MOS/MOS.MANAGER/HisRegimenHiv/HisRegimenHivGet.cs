using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRegimenHiv
{
    partial class HisRegimenHivGet : BusinessBase
    {
        internal HisRegimenHivGet()
            : base()
        {

        }

        internal HisRegimenHivGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_REGIMEN_HIV> Get(HisRegimenHivFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisRegimenHivDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_REGIMEN_HIV GetById(long id)
        {
            try
            {
                return GetById(id, new HisRegimenHivFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_REGIMEN_HIV GetById(long id, HisRegimenHivFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisRegimenHivDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_REGIMEN_HIV GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisRegimenHivFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_REGIMEN_HIV GetByCode(string code, HisRegimenHivFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisRegimenHivDAO.GetByCode(code, filter.Query());
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
