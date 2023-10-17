using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisSereServPtttTemp
{
    public partial class HisSereServPtttTempDAO : EntityBase
    {
        public HIS_SERE_SERV_PTTT_TEMP GetByCode(string code, HisSereServPtttTempSO search)
        {
            HIS_SERE_SERV_PTTT_TEMP result = null;

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

        public Dictionary<string, HIS_SERE_SERV_PTTT_TEMP> GetDicByCode(HisSereServPtttTempSO search, CommonParam param)
        {
            Dictionary<string, HIS_SERE_SERV_PTTT_TEMP> result = new Dictionary<string, HIS_SERE_SERV_PTTT_TEMP>();
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
