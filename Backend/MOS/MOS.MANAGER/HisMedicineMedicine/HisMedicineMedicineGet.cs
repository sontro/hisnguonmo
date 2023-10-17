using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMedicineMedicine
{
    partial class HisMedicineMedicineGet : BusinessBase
    {
        internal HisMedicineMedicineGet()
            : base()
        {

        }

        internal HisMedicineMedicineGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_MEDICINE_MEDICINE> Get(HisMedicineMedicineFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMedicineMedicineDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MEDICINE_MEDICINE GetById(long id)
        {
            try
            {
                return GetById(id, new HisMedicineMedicineFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MEDICINE_MEDICINE GetById(long id, HisMedicineMedicineFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMedicineMedicineDAO.GetById(id, filter.Query());
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
