using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisLicenseClass
{
    partial class HisLicenseClassGet : BusinessBase
    {
        internal HisLicenseClassGet()
            : base()
        {

        }

        internal HisLicenseClassGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_LICENSE_CLASS> Get(HisLicenseClassFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisLicenseClassDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_LICENSE_CLASS GetById(long id)
        {
            try
            {
                return GetById(id, new HisLicenseClassFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_LICENSE_CLASS GetById(long id, HisLicenseClassFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisLicenseClassDAO.GetById(id, filter.Query());
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
