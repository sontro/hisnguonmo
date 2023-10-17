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
        internal HisMediRecordGet()
            : base()
        {

        }

        internal HisMediRecordGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_MEDI_RECORD> Get(HisMediRecordFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMediRecordDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MEDI_RECORD GetById(long id)
        {
            try
            {
                return GetById(id, new HisMediRecordFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MEDI_RECORD GetById(long id, HisMediRecordFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMediRecordDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_MEDI_RECORD> GetByProgramId(long programId)
        {
            HisMediRecordFilterQuery filter = new HisMediRecordFilterQuery();
            filter.PROGRAM_ID = programId;
            return this.Get(filter);
        }

        internal List<HIS_MEDI_RECORD> GetByIds(List<long> ids)
        {
            try
            {
                if (ids != null)
                {
                    HisMediRecordFilterQuery filter = new HisMediRecordFilterQuery();
                    filter.IDs = ids;
                    return this.Get(filter);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }
            return null;
        }

    }
}
