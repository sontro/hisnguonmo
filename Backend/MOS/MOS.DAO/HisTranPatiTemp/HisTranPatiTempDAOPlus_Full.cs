using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisTranPatiTemp
{
    public partial class HisTranPatiTempDAO : EntityBase
    {
        public List<V_HIS_TRAN_PATI_TEMP> GetView(HisTranPatiTempSO search, CommonParam param)
        {
            List<V_HIS_TRAN_PATI_TEMP> result = new List<V_HIS_TRAN_PATI_TEMP>();

            try
            {
                result = GetWorker.GetView(search, param);
            }
            catch (Exception ex)
            {
                param.HasException = true;
                Inventec.Common.Logging.LogSystem.Error(ex);
                result.Clear();
            }

            return result;
        }

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
        
        public V_HIS_TRAN_PATI_TEMP GetViewById(long id, HisTranPatiTempSO search)
        {
            V_HIS_TRAN_PATI_TEMP result = null;

            try
            {
                result = GetWorker.GetViewById(id, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }

            return result;
        }

        public V_HIS_TRAN_PATI_TEMP GetViewByCode(string code, HisTranPatiTempSO search)
        {
            V_HIS_TRAN_PATI_TEMP result = null;

            try
            {
                result = GetWorker.GetViewByCode(code, search);
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
