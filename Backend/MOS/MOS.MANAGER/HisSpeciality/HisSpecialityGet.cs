using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSpeciality
{
    partial class HisSpecialityGet : BusinessBase
    {
        internal HisSpecialityGet()
            : base()
        {

        }

        internal HisSpecialityGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_SPECIALITY> Get(HisSpecialityFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSpecialityDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_SPECIALITY GetById(long id)
        {
            try
            {
                return GetById(id, new HisSpecialityFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_SPECIALITY GetById(long id, HisSpecialityFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSpecialityDAO.GetById(id, filter.Query());
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
