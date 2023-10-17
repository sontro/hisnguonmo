using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSkinSurgeryDesc
{
    partial class HisSkinSurgeryDescGet : BusinessBase
    {
        internal HisSkinSurgeryDescGet()
            : base()
        {

        }

        internal HisSkinSurgeryDescGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_SKIN_SURGERY_DESC> Get(HisSkinSurgeryDescFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSkinSurgeryDescDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_SKIN_SURGERY_DESC GetById(long id)
        {
            try
            {
                return GetById(id, new HisSkinSurgeryDescFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_SKIN_SURGERY_DESC GetById(long id, HisSkinSurgeryDescFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSkinSurgeryDescDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_SKIN_SURGERY_DESC> GetByIds(List<long> ids)
        {
            try
            {
                HisSkinSurgeryDescFilterQuery filter = new HisSkinSurgeryDescFilterQuery();
                filter.IDs = ids;
                return this.Get(filter);
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
