using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisSevereIllnessInfo
{
    public partial class HisSevereIllnessInfoDAO : EntityBase
    {
        public List<V_HIS_SEVERE_ILLNESS_INFO> GetView(HisSevereIllnessInfoSO search, CommonParam param)
        {
            List<V_HIS_SEVERE_ILLNESS_INFO> result = new List<V_HIS_SEVERE_ILLNESS_INFO>();

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

        public HIS_SEVERE_ILLNESS_INFO GetByCode(string code, HisSevereIllnessInfoSO search)
        {
            HIS_SEVERE_ILLNESS_INFO result = null;

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
        
        public V_HIS_SEVERE_ILLNESS_INFO GetViewById(long id, HisSevereIllnessInfoSO search)
        {
            V_HIS_SEVERE_ILLNESS_INFO result = null;

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

        public V_HIS_SEVERE_ILLNESS_INFO GetViewByCode(string code, HisSevereIllnessInfoSO search)
        {
            V_HIS_SEVERE_ILLNESS_INFO result = null;

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

        public Dictionary<string, HIS_SEVERE_ILLNESS_INFO> GetDicByCode(HisSevereIllnessInfoSO search, CommonParam param)
        {
            Dictionary<string, HIS_SEVERE_ILLNESS_INFO> result = new Dictionary<string, HIS_SEVERE_ILLNESS_INFO>();
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
