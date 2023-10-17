using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVitaminA
{
    partial class HisVitaminAGet : BusinessBase
    {
        internal V_HIS_VITAMIN_A GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisVitaminAViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_VITAMIN_A GetViewByCode(string code, HisVitaminAViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisVitaminADAO.GetViewByCode(code, filter.Query());
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
