using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMemaGroup
{
    public partial class HisMemaGroupDAO : EntityBase
    {
        public List<V_HIS_MEMA_GROUP> GetView(HisMemaGroupSO search, CommonParam param)
        {
            List<V_HIS_MEMA_GROUP> result = new List<V_HIS_MEMA_GROUP>();

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

        public HIS_MEMA_GROUP GetByCode(string code, HisMemaGroupSO search)
        {
            HIS_MEMA_GROUP result = null;

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
        
        public V_HIS_MEMA_GROUP GetViewById(long id, HisMemaGroupSO search)
        {
            V_HIS_MEMA_GROUP result = null;

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

        public V_HIS_MEMA_GROUP GetViewByCode(string code, HisMemaGroupSO search)
        {
            V_HIS_MEMA_GROUP result = null;

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

        public Dictionary<string, HIS_MEMA_GROUP> GetDicByCode(HisMemaGroupSO search, CommonParam param)
        {
            Dictionary<string, HIS_MEMA_GROUP> result = new Dictionary<string, HIS_MEMA_GROUP>();
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
