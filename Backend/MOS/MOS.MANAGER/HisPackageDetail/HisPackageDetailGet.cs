using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPackageDetail
{
    partial class HisPackageDetailGet : BusinessBase
    {
        internal HisPackageDetailGet()
            : base()
        {

        }

        internal HisPackageDetailGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_PACKAGE_DETAIL> Get(HisPackageDetailFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisPackageDetailDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_PACKAGE_DETAIL GetById(long id)
        {
            try
            {
                return GetById(id, new HisPackageDetailFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_PACKAGE_DETAIL GetById(long id, HisPackageDetailFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisPackageDetailDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_PACKAGE_DETAIL> GetByPackageId(long packageId)
        {
            try
            {
                HisPackageDetailFilterQuery filter = new HisPackageDetailFilterQuery();
                filter.PACKAGE_ID = packageId;
                return this.Get(filter);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }
    }
}
