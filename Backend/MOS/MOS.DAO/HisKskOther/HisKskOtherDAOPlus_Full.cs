using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisKskOther
{
    public partial class HisKskOtherDAO : EntityBase
    {
        public List<V_HIS_KSK_OTHER> GetView(HisKskOtherSO search, CommonParam param)
        {
            List<V_HIS_KSK_OTHER> result = new List<V_HIS_KSK_OTHER>();

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

        public HIS_KSK_OTHER GetByCode(string code, HisKskOtherSO search)
        {
            HIS_KSK_OTHER result = null;

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
        
        public V_HIS_KSK_OTHER GetViewById(long id, HisKskOtherSO search)
        {
            V_HIS_KSK_OTHER result = null;

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

        public V_HIS_KSK_OTHER GetViewByCode(string code, HisKskOtherSO search)
        {
            V_HIS_KSK_OTHER result = null;

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

        public Dictionary<string, HIS_KSK_OTHER> GetDicByCode(HisKskOtherSO search, CommonParam param)
        {
            Dictionary<string, HIS_KSK_OTHER> result = new Dictionary<string, HIS_KSK_OTHER>();
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
