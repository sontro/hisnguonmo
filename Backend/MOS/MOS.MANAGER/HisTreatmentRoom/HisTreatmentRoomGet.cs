using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTreatmentRoom
{
    partial class HisTreatmentRoomGet : BusinessBase
    {
        internal HisTreatmentRoomGet()
            : base()
        {

        }

        internal HisTreatmentRoomGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_TREATMENT_ROOM> Get(HisTreatmentRoomFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTreatmentRoomDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_TREATMENT_ROOM GetById(long id)
        {
            try
            {
                return GetById(id, new HisTreatmentRoomFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_TREATMENT_ROOM GetById(long id, HisTreatmentRoomFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTreatmentRoomDAO.GetById(id, filter.Query());
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
