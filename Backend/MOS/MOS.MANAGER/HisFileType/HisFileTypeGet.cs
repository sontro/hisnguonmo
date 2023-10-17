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
        internal HisFileTypeGet()
            : base()
        {

        }

        internal HisFileTypeGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_FILE_TYPE> Get(HisFileTypeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisFileTypeDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_FILE_TYPE GetById(long id)
        {
            try
            {
                return GetById(id, new HisFileTypeFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_FILE_TYPE GetById(long id, HisFileTypeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisFileTypeDAO.GetById(id, filter.Query());
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
