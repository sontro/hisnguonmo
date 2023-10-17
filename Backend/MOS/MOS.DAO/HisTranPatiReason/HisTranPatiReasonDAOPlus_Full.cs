using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisTranPatiReason
{
    public partial class HisTranPatiReasonDAO : EntityBase
    {
        public List<V_HIS_TRAN_PATI_REASON> GetView(HisTranPatiReasonSO search, CommonParam param)
        {
            List<V_HIS_TRAN_PATI_REASON> result = new List<V_HIS_TRAN_PATI_REASON>();

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

        public HIS_TRAN_PATI_REASON GetByCode(string code, HisTranPatiReasonSO search)
        {
            HIS_TRAN_PATI_REASON result = null;

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
        
        public V_HIS_TRAN_PATI_REASON GetViewById(long id, HisTranPatiReasonSO search)
        {
            V_HIS_TRAN_PATI_REASON result = null;

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

        public V_HIS_TRAN_PATI_REASON GetViewByCode(string code, HisTranPatiReasonSO search)
        {
            V_HIS_TRAN_PATI_REASON result = null;

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

        public Dictionary<string, HIS_TRAN_PATI_REASON> GetDicByCode(HisTranPatiReasonSO search, CommonParam param)
        {
            Dictionary<string, HIS_TRAN_PATI_REASON> result = new Dictionary<string, HIS_TRAN_PATI_REASON>();
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
