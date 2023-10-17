using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisFileType
{
    partial class HisFileTypeGet : BusinessBase
    {
        internal HIS_FILE_TYPE GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisFileTypeFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_FILE_TYPE GetByCode(string code, HisFileTypeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisFileTypeDAO.GetByCode(code, filter.Query());
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
