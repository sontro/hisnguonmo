using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBedBsty
{
    partial class HisBedBstyGet : BusinessBase
    {
        internal HIS_BED_BSTY GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisBedBstyFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_BED_BSTY GetByCode(string code, HisBedBstyFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisBedBstyDAO.GetByCode(code, filter.Query());
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
