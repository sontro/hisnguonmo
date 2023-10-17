using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMedicineInteractive
{
    partial class HisMedicineInteractiveGet : BusinessBase
    {
        internal HisMedicineInteractiveGet()
            : base()
        {

        }

        internal HisMedicineInteractiveGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_MEDICINE_INTERACTIVE> Get(HisMedicineInteractiveFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMedicineInteractiveDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MEDICINE_INTERACTIVE GetById(long id)
        {
            try
            {
                return GetById(id, new HisMedicineInteractiveFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MEDICINE_INTERACTIVE GetById(long id, HisMedicineInteractiveFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMedicineInteractiveDAO.GetById(id, filter.Query());
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
