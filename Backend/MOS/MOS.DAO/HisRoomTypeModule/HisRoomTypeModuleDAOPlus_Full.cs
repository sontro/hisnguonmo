using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisRoomTypeModule
{
    public partial class HisRoomTypeModuleDAO : EntityBase
    {
        public List<V_HIS_ROOM_TYPE_MODULE> GetView(HisRoomTypeModuleSO search, CommonParam param)
        {
            List<V_HIS_ROOM_TYPE_MODULE> result = new List<V_HIS_ROOM_TYPE_MODULE>();

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

        public HIS_ROOM_TYPE_MODULE GetByCode(string code, HisRoomTypeModuleSO search)
        {
            HIS_ROOM_TYPE_MODULE result = null;

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
        
        public V_HIS_ROOM_TYPE_MODULE GetViewById(long id, HisRoomTypeModuleSO search)
        {
            V_HIS_ROOM_TYPE_MODULE result = null;

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

        public V_HIS_ROOM_TYPE_MODULE GetViewByCode(string code, HisRoomTypeModuleSO search)
        {
            V_HIS_ROOM_TYPE_MODULE result = null;

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

        public Dictionary<string, HIS_ROOM_TYPE_MODULE> GetDicByCode(HisRoomTypeModuleSO search, CommonParam param)
        {
            Dictionary<string, HIS_ROOM_TYPE_MODULE> result = new Dictionary<string, HIS_ROOM_TYPE_MODULE>();
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
