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
        internal List<V_HIS_MEDICINE_TYPE_ROOM> GetView(HisMedicineTypeRoomViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMedicineTypeRoomDAO.GetView(filter.Query(), param);
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
