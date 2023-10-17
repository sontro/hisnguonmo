using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisUserRoom
{
    public partial class HisUserRoomDAO : EntityBase
    {
        public List<V_HIS_USER_ROOM> GetView(HisUserRoomSO search, CommonParam param)
        {
            List<V_HIS_USER_ROOM> result = new List<V_HIS_USER_ROOM>();

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

        public HIS_USER_ROOM GetByCode(string code, HisUserRoomSO search)
        {
            HIS_USER_ROOM result = null;

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
        
        public V_HIS_USER_ROOM GetViewById(long id, HisUserRoomSO search)
        {
            V_HIS_USER_ROOM result = null;

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

        public V_HIS_USER_ROOM GetViewByCode(string code, HisUserRoomSO search)
        {
            V_HIS_USER_ROOM result = null;

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

        public Dictionary<string, HIS_USER_ROOM> GetDicByCode(HisUserRoomSO search, CommonParam param)
        {
            Dictionary<string, HIS_USER_ROOM> result = new Dictionary<string, HIS_USER_ROOM>();
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
