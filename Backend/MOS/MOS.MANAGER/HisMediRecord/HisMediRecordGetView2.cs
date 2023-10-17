﻿using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMediRecord
{
    partial class HisMediRecordGet : BusinessBase
    {
        internal List<V_HIS_MEDI_RECORD_2> GetView2(HisMediRecordView2FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMediRecordDAO.GetView2(filter.Query(), param);
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
