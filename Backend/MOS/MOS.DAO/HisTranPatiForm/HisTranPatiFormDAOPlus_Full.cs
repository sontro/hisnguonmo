using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisTranPatiForm
{
    public partial class HisTranPatiFormDAO : EntityBase
    {
        public List<V_HIS_TRAN_PATI_FORM> GetView(HisTranPatiFormSO search, CommonParam param)
        {
            List<V_HIS_TRAN_PATI_FORM> result = new List<V_HIS_TRAN_PATI_FORM>();

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

        public HIS_TRAN_PATI_FORM GetByCode(string code, HisTranPatiFormSO search)
        {
            HIS_TRAN_PATI_FORM result = null;

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
        
        public V_HIS_TRAN_PATI_FORM GetViewById(long id, HisTranPatiFormSO search)
        {
            V_HIS_TRAN_PATI_FORM result = null;

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

        public V_HIS_TRAN_PATI_FORM GetViewByCode(string code, HisTranPatiFormSO search)
        {
            V_HIS_TRAN_PATI_FORM result = null;

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

        public Dictionary<string, HIS_TRAN_PATI_FORM> GetDicByCode(HisTranPatiFormSO search, CommonParam param)
        {
            Dictionary<string, HIS_TRAN_PATI_FORM> result = new Dictionary<string, HIS_TRAN_PATI_FORM>();
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
