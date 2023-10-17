using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMachineServMaty
{
    partial class HisMachineServMatyGet : BusinessBase
    {
        internal HisMachineServMatyGet()
            : base()
        {

        }

        internal HisMachineServMatyGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_MACHINE_SERV_MATY> Get(HisMachineServMatyFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMachineServMatyDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MACHINE_SERV_MATY GetById(long id)
        {
            try
            {
                return GetById(id, new HisMachineServMatyFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MACHINE_SERV_MATY GetById(long id, HisMachineServMatyFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMachineServMatyDAO.GetById(id, filter.Query());
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
