using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMedicineTypeRoom
{
    partial class HisMedicineTypeRoomGet : BusinessBase
    {
        internal HisMedicineTypeRoomGet()
            : base()
        {

        }

        internal HisMedicineTypeRoomGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_MEDICINE_TYPE_ROOM> Get(HisMedicineTypeRoomFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMedicineTypeRoomDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MEDICINE_TYPE_ROOM GetById(long id)
        {
            try
            {
                return GetById(id, new HisMedicineTypeRoomFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MEDICINE_TYPE_ROOM GetById(long id, HisMedicineTypeRoomFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMedicineTypeRoomDAO.GetById(id, filter.Query());
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
