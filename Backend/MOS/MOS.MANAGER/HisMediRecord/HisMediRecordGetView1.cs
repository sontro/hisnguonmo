using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMediRecord
{
    partial class HisMediRecordGet : BusinessBase
    {
        internal List<V_HIS_MEDI_RECORD_1> GetView1(HisMediRecordView1FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMediRecordDAO.GetView1(filter.Query(), param);
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
