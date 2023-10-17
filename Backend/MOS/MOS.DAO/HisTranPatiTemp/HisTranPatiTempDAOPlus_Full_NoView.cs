using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisTranPatiTemp
{
    public partial class HisTranPatiTempDAO : EntityBase
    {
        public HIS_TRAN_PATI_TEMP GetByCode(string code, HisTranPatiTempSO search)
        {
            HIS_TRAN_PATI_TEMP result = null;

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

        public Dictionary<string, HIS_TRAN_PATI_TEMP> GetDicByCode(HisTranPatiTempSO search, CommonParam param)
        {
            Dictionary<string, HIS_TRAN_PATI_TEMP> result = new Dictionary<string, HIS_TRAN_PATI_TEMP>();
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
