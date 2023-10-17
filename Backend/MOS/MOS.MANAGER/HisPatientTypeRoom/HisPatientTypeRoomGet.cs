using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPatientTypeRoom
{
    partial class HisPatientTypeRoomGet : BusinessBase
    {
        internal HisPatientTypeRoomGet()
            : base()
        {

        }

        internal HisPatientTypeRoomGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_PATIENT_TYPE_ROOM> Get(HisPatientTypeRoomFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisPatientTypeRoomDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_PATIENT_TYPE_ROOM GetById(long id)
        {
            try
            {
                return GetById(id, new HisPatientTypeRoomFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_PATIENT_TYPE_ROOM GetById(long id, HisPatientTypeRoomFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisPatientTypeRoomDAO.GetById(id, filter.Query());
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
