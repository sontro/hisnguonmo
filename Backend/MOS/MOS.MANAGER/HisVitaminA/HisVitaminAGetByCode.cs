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
        internal HIS_VITAMIN_A GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisVitaminAFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_VITAMIN_A GetByCode(string code, HisVitaminAFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisVitaminADAO.GetByCode(code, filter.Query());
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
