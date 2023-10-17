using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMediRecordType
{
    partial class HisMediRecordTypeGet : BusinessBase
    {
        internal HIS_MEDI_RECORD_TYPE GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisMediRecordTypeFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MEDI_RECORD_TYPE GetByCode(string code, HisMediRecordTypeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMediRecordTypeDAO.GetByCode(code, filter.Query());
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
