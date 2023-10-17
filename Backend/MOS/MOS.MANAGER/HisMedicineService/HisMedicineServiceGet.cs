using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMedicineService
{
    partial class HisMedicineServiceGet : BusinessBase
    {
        internal HisMedicineServiceGet()
            : base()
        {

        }

        internal HisMedicineServiceGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_MEDICINE_SERVICE> Get(HisMedicineServiceFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMedicineServiceDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MEDICINE_SERVICE GetById(long id)
        {
            try
            {
                return GetById(id, new HisMedicineServiceFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MEDICINE_SERVICE GetById(long id, HisMedicineServiceFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMedicineServiceDAO.GetById(id, filter.Query());
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
