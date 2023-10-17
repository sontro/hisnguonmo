using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisContactPoint
{
    public partial class HisContactPointDAO : EntityBase
    {
        public List<V_HIS_CONTACT_POINT> GetView(HisContactPointSO search, CommonParam param)
        {
            List<V_HIS_CONTACT_POINT> result = new List<V_HIS_CONTACT_POINT>();

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

        public HIS_CONTACT_POINT GetByCode(string code, HisContactPointSO search)
        {
            HIS_CONTACT_POINT result = null;

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
        
        public V_HIS_CONTACT_POINT GetViewById(long id, HisContactPointSO search)
        {
            V_HIS_CONTACT_POINT result = null;

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

        public V_HIS_CONTACT_POINT GetViewByCode(string code, HisContactPointSO search)
        {
            V_HIS_CONTACT_POINT result = null;

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

        public Dictionary<string, HIS_CONTACT_POINT> GetDicByCode(HisContactPointSO search, CommonParam param)
        {
            Dictionary<string, HIS_CONTACT_POINT> result = new Dictionary<string, HIS_CONTACT_POINT>();
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
