using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisBidBloodType
{
    public partial class HisBidBloodTypeDAO : EntityBase
    {
        public List<V_HIS_BID_BLOOD_TYPE> GetView(HisBidBloodTypeSO search, CommonParam param)
        {
            List<V_HIS_BID_BLOOD_TYPE> result = new List<V_HIS_BID_BLOOD_TYPE>();

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

        public HIS_BID_BLOOD_TYPE GetByCode(string code, HisBidBloodTypeSO search)
        {
            HIS_BID_BLOOD_TYPE result = null;

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
        
        public V_HIS_BID_BLOOD_TYPE GetViewById(long id, HisBidBloodTypeSO search)
        {
            V_HIS_BID_BLOOD_TYPE result = null;

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

        public V_HIS_BID_BLOOD_TYPE GetViewByCode(string code, HisBidBloodTypeSO search)
        {
            V_HIS_BID_BLOOD_TYPE result = null;

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

        public Dictionary<string, HIS_BID_BLOOD_TYPE> GetDicByCode(HisBidBloodTypeSO search, CommonParam param)
        {
            Dictionary<string, HIS_BID_BLOOD_TYPE> result = new Dictionary<string, HIS_BID_BLOOD_TYPE>();
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
