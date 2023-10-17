using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisRationSchedule
{
    public partial class HisRationScheduleDAO : EntityBase
    {
        public List<V_HIS_RATION_SCHEDULE> GetView(HisRationScheduleSO search, CommonParam param)
        {
            List<V_HIS_RATION_SCHEDULE> result = new List<V_HIS_RATION_SCHEDULE>();

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

        //public HIS_RATION_SCHEDULE GetByCode(string code, HisRationScheduleSO search)
        //{
        //    HIS_RATION_SCHEDULE result = null;

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
        
        public V_HIS_RATION_SCHEDULE GetViewById(long id, HisRationScheduleSO search)
        {
            V_HIS_RATION_SCHEDULE result = null;

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

        //public V_HIS_RATION_SCHEDULE GetViewByCode(string code, HisRationScheduleSO search)
        //{
        //    V_HIS_RATION_SCHEDULE result = null;

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

        //public Dictionary<string, HIS_RATION_SCHEDULE> GetDicByCode(HisRationScheduleSO search, CommonParam param)
        //{
        //    Dictionary<string, HIS_RATION_SCHEDULE> result = new Dictionary<string, HIS_RATION_SCHEDULE>();
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
