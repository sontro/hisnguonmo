using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisCareTempDetail
{
    partial class HisCareTempDetailGet : BusinessBase
    {
        internal HisCareTempDetailGet()
            : base()
        {

        }

        internal HisCareTempDetailGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_CARE_TEMP_DETAIL> Get(HisCareTempDetailFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisCareTempDetailDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_CARE_TEMP_DETAIL GetById(long id)
        {
            try
            {
                return GetById(id, new HisCareTempDetailFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_CARE_TEMP_DETAIL GetById(long id, HisCareTempDetailFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisCareTempDetailDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_CARE_TEMP_DETAIL> GetByCareTempId(long careTempId)
        {
            try
            {
                HisCareTempDetailFilterQuery filter = new HisCareTempDetailFilterQuery();
                filter.CARE_TEMP_ID = careTempId;
                return this.Get(filter);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }


        internal List<HIS_CARE_TEMP_DETAIL> GetByCareTypeId(long careTypeId)
        {
            try
            {
                HisCareTempDetailFilterQuery filter = new HisCareTempDetailFilterQuery();
                filter.CARE_TYPE_ID = careTypeId;
                return this.Get(filter);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }
    }
}
