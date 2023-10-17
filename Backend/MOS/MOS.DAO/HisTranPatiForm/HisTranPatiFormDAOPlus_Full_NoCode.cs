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
    }
}
