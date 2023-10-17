using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisHivTreatment
{
    public partial class HisHivTreatmentDAO : EntityBase
    {
        public List<V_HIS_HIV_TREATMENT> GetView(HisHivTreatmentSO search, CommonParam param)
        {
            List<V_HIS_HIV_TREATMENT> result = new List<V_HIS_HIV_TREATMENT>();

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

        //public HIS_HIV_TREATMENT GetByCode(string code, HisHivTreatmentSO search)
        //{
        //    HIS_HIV_TREATMENT result = null;

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
        
        public V_HIS_HIV_TREATMENT GetViewById(long id, HisHivTreatmentSO search)
        {
            V_HIS_HIV_TREATMENT result = null;

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

        public V_HIS_HIV_TREATMENT GetViewByCode(string code, HisHivTreatmentSO search)
        {
            V_HIS_HIV_TREATMENT result = null;

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

    //    public Dictionary<string, HIS_HIV_TREATMENT> GetDicByCode(HisHivTreatmentSO search, CommonParam param)
    //    {
    //        Dictionary<string, HIS_HIV_TREATMENT> result = new Dictionary<string, HIS_HIV_TREATMENT>();
    //        try
    //        {
    //            result = GetWorker.GetDicByCode(search, param);
    //        }
    //        catch (Exception ex)
    //        {
    //            Inventec.Common.Logging.LogSystem.Error(ex);
    //            result.Clear();
    //        }

    //        return result;
    //    }

    //    public bool ExistsCode(string code, long? id)
    //    {
    //        try
    //        {
    //            return CheckWorker.ExistsCode(code, id);
    //        }
    //        catch (Exception ex)
    //        {
    //            Inventec.Common.Logging.LogSystem.Error(ex);
    //            throw;
    //        }
    //    }
    }
}
