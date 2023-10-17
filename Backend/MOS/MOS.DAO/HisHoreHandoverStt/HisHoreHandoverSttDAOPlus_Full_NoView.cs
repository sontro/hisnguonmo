using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisHoreHandoverStt
{
    public partial class HisHoreHandoverSttDAO : EntityBase
    {
        public HIS_HORE_HANDOVER_STT GetByCode(string code, HisHoreHandoverSttSO search)
        {
            HIS_HORE_HANDOVER_STT result = null;

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

        public Dictionary<string, HIS_HORE_HANDOVER_STT> GetDicByCode(HisHoreHandoverSttSO search, CommonParam param)
        {
            Dictionary<string, HIS_HORE_HANDOVER_STT> result = new Dictionary<string, HIS_HORE_HANDOVER_STT>();
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
