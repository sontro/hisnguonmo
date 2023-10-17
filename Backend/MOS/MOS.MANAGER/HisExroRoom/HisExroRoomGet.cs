using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExroRoom
{
    partial class HisExroRoomGet : BusinessBase
    {
        internal HisExroRoomGet()
            : base()
        {

        }

        internal HisExroRoomGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_EXRO_ROOM> Get(HisExroRoomFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisExroRoomDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_EXRO_ROOM GetById(long id)
        {
            try
            {
                return GetById(id, new HisExroRoomFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_EXRO_ROOM GetById(long id, HisExroRoomFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisExroRoomDAO.GetById(id, filter.Query());
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
