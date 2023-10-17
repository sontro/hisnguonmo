using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisAccidentHelmet
{
    public partial class HisAccidentHelmetDAO : EntityBase
    {
        public List<V_HIS_ACCIDENT_HELMET> GetView(HisAccidentHelmetSO search, CommonParam param)
        {
            List<V_HIS_ACCIDENT_HELMET> result = new List<V_HIS_ACCIDENT_HELMET>();

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

        public HIS_ACCIDENT_HELMET GetByCode(string code, HisAccidentHelmetSO search)
        {
            HIS_ACCIDENT_HELMET result = null;

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
        
        public V_HIS_ACCIDENT_HELMET GetViewById(long id, HisAccidentHelmetSO search)
        {
            V_HIS_ACCIDENT_HELMET result = null;

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

        public V_HIS_ACCIDENT_HELMET GetViewByCode(string code, HisAccidentHelmetSO search)
        {
            V_HIS_ACCIDENT_HELMET result = null;

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

        public Dictionary<string, HIS_ACCIDENT_HELMET> GetDicByCode(HisAccidentHelmetSO search, CommonParam param)
        {
            Dictionary<string, HIS_ACCIDENT_HELMET> result = new Dictionary<string, HIS_ACCIDENT_HELMET>();
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
