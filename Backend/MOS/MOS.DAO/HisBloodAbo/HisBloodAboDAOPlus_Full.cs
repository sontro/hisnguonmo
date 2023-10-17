using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisBloodAbo
{
    public partial class HisBloodAboDAO : EntityBase
    {
        public List<V_HIS_BLOOD_ABO> GetView(HisBloodAboSO search, CommonParam param)
        {
            List<V_HIS_BLOOD_ABO> result = new List<V_HIS_BLOOD_ABO>();

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

        public HIS_BLOOD_ABO GetByCode(string code, HisBloodAboSO search)
        {
            HIS_BLOOD_ABO result = null;

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
        
        public V_HIS_BLOOD_ABO GetViewById(long id, HisBloodAboSO search)
        {
            V_HIS_BLOOD_ABO result = null;

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

        public V_HIS_BLOOD_ABO GetViewByCode(string code, HisBloodAboSO search)
        {
            V_HIS_BLOOD_ABO result = null;

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

        public Dictionary<string, HIS_BLOOD_ABO> GetDicByCode(HisBloodAboSO search, CommonParam param)
        {
            Dictionary<string, HIS_BLOOD_ABO> result = new Dictionary<string, HIS_BLOOD_ABO>();
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
