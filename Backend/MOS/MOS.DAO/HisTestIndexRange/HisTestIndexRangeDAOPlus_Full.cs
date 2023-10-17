using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisTestIndexRange
{
    public partial class HisTestIndexRangeDAO : EntityBase
    {
        public List<V_HIS_TEST_INDEX_RANGE> GetView(HisTestIndexRangeSO search, CommonParam param)
        {
            List<V_HIS_TEST_INDEX_RANGE> result = new List<V_HIS_TEST_INDEX_RANGE>();

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

        public HIS_TEST_INDEX_RANGE GetByCode(string code, HisTestIndexRangeSO search)
        {
            HIS_TEST_INDEX_RANGE result = null;

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
        
        public V_HIS_TEST_INDEX_RANGE GetViewById(long id, HisTestIndexRangeSO search)
        {
            V_HIS_TEST_INDEX_RANGE result = null;

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

        public V_HIS_TEST_INDEX_RANGE GetViewByCode(string code, HisTestIndexRangeSO search)
        {
            V_HIS_TEST_INDEX_RANGE result = null;

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

        public Dictionary<string, HIS_TEST_INDEX_RANGE> GetDicByCode(HisTestIndexRangeSO search, CommonParam param)
        {
            Dictionary<string, HIS_TEST_INDEX_RANGE> result = new Dictionary<string, HIS_TEST_INDEX_RANGE>();
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
