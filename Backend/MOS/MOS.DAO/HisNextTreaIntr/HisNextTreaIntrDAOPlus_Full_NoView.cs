using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisNextTreaIntr
{
    public partial class HisNextTreaIntrDAO : EntityBase
    {
        public HIS_NEXT_TREA_INTR GetByCode(string code, HisNextTreaIntrSO search)
        {
            HIS_NEXT_TREA_INTR result = null;

            try
            {
                result = GetWorker.GetByCode(code, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }

            return result;
        }

        public Dictionary<string, HIS_NEXT_TREA_INTR> GetDicByCode(HisNextTreaIntrSO search, CommonParam param)
        {
            Dictionary<string, HIS_NEXT_TREA_INTR> result = new Dictionary<string, HIS_NEXT_TREA_INTR>();
            try
            {
                result = GetWorker.GetDicByCode(search, param);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result.Clear();
            }

            return result;
        }

        public bool ExistsCode(string code, long? id)
        {

            try
            {
                return CheckWorker.ExistsCode(code, id);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                throw;
            }
        }
    }
}
