using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisKskDriver
{
    public partial class HisKskDriverDAO : EntityBase
    {
        public List<V_HIS_KSK_DRIVER> GetView(HisKskDriverSO search, CommonParam param)
        {
            List<V_HIS_KSK_DRIVER> result = new List<V_HIS_KSK_DRIVER>();

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

        public HIS_KSK_DRIVER GetByCode(string code, HisKskDriverSO search)
        {
            HIS_KSK_DRIVER result = null;

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
        
        public V_HIS_KSK_DRIVER GetViewById(long id, HisKskDriverSO search)
        {
            V_HIS_KSK_DRIVER result = null;

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

        public V_HIS_KSK_DRIVER GetViewByCode(string code, HisKskDriverSO search)
        {
            V_HIS_KSK_DRIVER result = null;

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

        public Dictionary<string, HIS_KSK_DRIVER> GetDicByCode(HisKskDriverSO search, CommonParam param)
        {
            Dictionary<string, HIS_KSK_DRIVER> result = new Dictionary<string, HIS_KSK_DRIVER>();
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
