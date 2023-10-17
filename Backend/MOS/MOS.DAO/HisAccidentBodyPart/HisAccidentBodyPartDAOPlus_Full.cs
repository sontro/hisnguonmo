using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisAccidentBodyPart
{
    public partial class HisAccidentBodyPartDAO : EntityBase
    {
        public List<V_HIS_ACCIDENT_BODY_PART> GetView(HisAccidentBodyPartSO search, CommonParam param)
        {
            List<V_HIS_ACCIDENT_BODY_PART> result = new List<V_HIS_ACCIDENT_BODY_PART>();

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

        public HIS_ACCIDENT_BODY_PART GetByCode(string code, HisAccidentBodyPartSO search)
        {
            HIS_ACCIDENT_BODY_PART result = null;

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
        
        public V_HIS_ACCIDENT_BODY_PART GetViewById(long id, HisAccidentBodyPartSO search)
        {
            V_HIS_ACCIDENT_BODY_PART result = null;

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

        public V_HIS_ACCIDENT_BODY_PART GetViewByCode(string code, HisAccidentBodyPartSO search)
        {
            V_HIS_ACCIDENT_BODY_PART result = null;

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

        public Dictionary<string, HIS_ACCIDENT_BODY_PART> GetDicByCode(HisAccidentBodyPartSO search, CommonParam param)
        {
            Dictionary<string, HIS_ACCIDENT_BODY_PART> result = new Dictionary<string, HIS_ACCIDENT_BODY_PART>();
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
