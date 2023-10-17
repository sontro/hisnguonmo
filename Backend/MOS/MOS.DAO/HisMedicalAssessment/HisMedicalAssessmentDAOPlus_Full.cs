using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMedicalAssessment
{
    public partial class HisMedicalAssessmentDAO : EntityBase
    {
        public List<V_HIS_MEDICAL_ASSESSMENT> GetView(HisMedicalAssessmentSO search, CommonParam param)
        {
            List<V_HIS_MEDICAL_ASSESSMENT> result = new List<V_HIS_MEDICAL_ASSESSMENT>();

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

        //public HIS_MEDICAL_ASSESSMENT GetByCode(string code, HisMedicalAssessmentSO search)
        //{
        //    HIS_MEDICAL_ASSESSMENT result = null;

        //    try
        //    {
        //        result = GetWorker.GetByCode(code, search);
        //    }
        //    catch (Exception ex)
        //    {
        //        Inventec.Common.Logging.LogSystem.Error(ex);
        //        result = null;
        //    }

        //    return result;
        //}
        
        public V_HIS_MEDICAL_ASSESSMENT GetViewById(long id, HisMedicalAssessmentSO search)
        {
            V_HIS_MEDICAL_ASSESSMENT result = null;

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

        //public V_HIS_MEDICAL_ASSESSMENT GetViewByCode(string code, HisMedicalAssessmentSO search)
        //{
        //    V_HIS_MEDICAL_ASSESSMENT result = null;

        //    try
        //    {
        //        result = GetWorker.GetViewByCode(code, search);
        //    }
        //    catch (Exception ex)
        //    {
        //        Inventec.Common.Logging.LogSystem.Error(ex);
        //        result = null;
        //    }
        //    return result;
        //}

        //public Dictionary<string, HIS_MEDICAL_ASSESSMENT> GetDicByCode(HisMedicalAssessmentSO search, CommonParam param)
        //{
        //    Dictionary<string, HIS_MEDICAL_ASSESSMENT> result = new Dictionary<string, HIS_MEDICAL_ASSESSMENT>();
        //    try
        //    {
        //        result = GetWorker.GetDicByCode(search, param);
        //    }
        //    catch (Exception ex)
        //    {
        //        Inventec.Common.Logging.LogSystem.Error(ex);
        //        result.Clear();
        //    }

        //    return result;
        //}

        //public bool ExistsCode(string code, long? id)
        //{
        //    try
        //    {
        //        return CheckWorker.ExistsCode(code, id);
        //    }
        //    catch (Exception ex)
        //    {
        //        Inventec.Common.Logging.LogSystem.Error(ex);
        //        throw;
        //    }
        //}
    }
}
