using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisTextLib
{
    public partial class HisTextLibDAO : EntityBase
    {
        public List<V_HIS_TEXT_LIB> GetView(HisTextLibSO search, CommonParam param)
        {
            List<V_HIS_TEXT_LIB> result = new List<V_HIS_TEXT_LIB>();

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

        public HIS_TEXT_LIB GetByCode(string code, HisTextLibSO search)
        {
            HIS_TEXT_LIB result = null;

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
        
        public V_HIS_TEXT_LIB GetViewById(long id, HisTextLibSO search)
        {
            V_HIS_TEXT_LIB result = null;

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

        public V_HIS_TEXT_LIB GetViewByCode(string code, HisTextLibSO search)
        {
            V_HIS_TEXT_LIB result = null;

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

        public Dictionary<string, HIS_TEXT_LIB> GetDicByCode(HisTextLibSO search, CommonParam param)
        {
            Dictionary<string, HIS_TEXT_LIB> result = new Dictionary<string, HIS_TEXT_LIB>();
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
