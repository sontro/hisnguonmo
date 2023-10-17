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
        internal HisMediRecordTypeGet()
            : base()
        {

        }

        internal HisMediRecordTypeGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_MEDI_RECORD_TYPE> Get(HisMediRecordTypeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMediRecordTypeDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MEDI_RECORD_TYPE GetById(long id)
        {
            try
            {
                return GetById(id, new HisMediRecordTypeFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MEDI_RECORD_TYPE GetById(long id, HisMediRecordTypeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMediRecordTypeDAO.GetById(id, filter.Query());
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
