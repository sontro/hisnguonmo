using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisSpeedUnit
{
    public partial class HisSpeedUnitDAO : EntityBase
    {
        public List<V_HIS_SPEED_UNIT> GetView(HisSpeedUnitSO search, CommonParam param)
        {
            List<V_HIS_SPEED_UNIT> result = new List<V_HIS_SPEED_UNIT>();

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

        public HIS_SPEED_UNIT GetByCode(string code, HisSpeedUnitSO search)
        {
            HIS_SPEED_UNIT result = null;

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
        
        public V_HIS_SPEED_UNIT GetViewById(long id, HisSpeedUnitSO search)
        {
            V_HIS_SPEED_UNIT result = null;

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

        public V_HIS_SPEED_UNIT GetViewByCode(string code, HisSpeedUnitSO search)
        {
            V_HIS_SPEED_UNIT result = null;

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

        public Dictionary<string, HIS_SPEED_UNIT> GetDicByCode(HisSpeedUnitSO search, CommonParam param)
        {
            Dictionary<string, HIS_SPEED_UNIT> result = new Dictionary<string, HIS_SPEED_UNIT>();
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
