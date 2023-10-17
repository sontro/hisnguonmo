using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSevereIllnessInfo
{
    partial class HisSevereIllnessInfoGet : BusinessBase
    {
        internal HisSevereIllnessInfoGet()
            : base()
        {

        }

        internal HisSevereIllnessInfoGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_SEVERE_ILLNESS_INFO> Get(HisSevereIllnessInfoFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSevereIllnessInfoDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_SEVERE_ILLNESS_INFO GetById(long id)
        {
            try
            {
                return GetById(id, new HisSevereIllnessInfoFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_SEVERE_ILLNESS_INFO GetById(long id, HisSevereIllnessInfoFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSevereIllnessInfoDAO.GetById(id, filter.Query());
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
