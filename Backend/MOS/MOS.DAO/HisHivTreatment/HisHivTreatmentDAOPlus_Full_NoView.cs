using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisHivTreatment
{
    public partial class HisHivTreatmentDAO : EntityBase
    {
        public HIS_HIV_TREATMENT GetByCode(string code, HisHivTreatmentSO search)
        {
            HIS_HIV_TREATMENT result = null;

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

        public Dictionary<string, HIS_HIV_TREATMENT> GetDicByCode(HisHivTreatmentSO search, CommonParam param)
        {
            Dictionary<string, HIS_HIV_TREATMENT> result = new Dictionary<string, HIS_HIV_TREATMENT>();
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
    }
}
