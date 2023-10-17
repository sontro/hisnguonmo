using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisPatientCase
{
    public partial class HisPatientCaseDAO : EntityBase
    {
        public List<V_HIS_PATIENT_CASE> GetView(HisPatientCaseSO search, CommonParam param)
        {
            List<V_HIS_PATIENT_CASE> result = new List<V_HIS_PATIENT_CASE>();
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

        public V_HIS_PATIENT_CASE GetViewById(long id, HisPatientCaseSO search)
        {
            V_HIS_PATIENT_CASE result = null;

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
