using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSereServPtttTemp
{
    partial class HisSereServPtttTempGet : BusinessBase
    {
        internal HisSereServPtttTempGet()
            : base()
        {

        }

        internal HisSereServPtttTempGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_SERE_SERV_PTTT_TEMP> Get(HisSereServPtttTempFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSereServPtttTempDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_SERE_SERV_PTTT_TEMP GetById(long id)
        {
            try
            {
                return GetById(id, new HisSereServPtttTempFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_SERE_SERV_PTTT_TEMP GetById(long id, HisSereServPtttTempFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSereServPtttTempDAO.GetById(id, filter.Query());
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
