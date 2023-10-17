using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisRoomTypeModule
{
    public partial class HisRoomTypeModuleDAO : EntityBase
    {
        private HisRoomTypeModuleCreate CreateWorker
        {
            get
            {
                return (HisRoomTypeModuleCreate)Worker.Get<HisRoomTypeModuleCreate>();
            }
        }
        private HisRoomTypeModuleUpdate UpdateWorker
        {
            get
            {
                return (HisRoomTypeModuleUpdate)Worker.Get<HisRoomTypeModuleUpdate>();
            }
        }
        private HisRoomTypeModuleDelete DeleteWorker
        {
            get
            {
                return (HisRoomTypeModuleDelete)Worker.Get<HisRoomTypeModuleDelete>();
            }
        }
        private HisRoomTypeModuleTruncate TruncateWorker
        {
            get
            {
                return (HisRoomTypeModuleTruncate)Worker.Get<HisRoomTypeModuleTruncate>();
            }
        }
        private HisRoomTypeModuleGet GetWorker
        {
            get
            {
                return (HisRoomTypeModuleGet)Worker.Get<HisRoomTypeModuleGet>();
            }
        }
        private HisRoomTypeModuleCheck CheckWorker
        {
            get
            {
                return (HisRoomTypeModuleCheck)Worker.Get<HisRoomTypeModuleCheck>();
            }
        }

        public bool Create(HIS_ROOM_TYPE_MODULE data)
        {
            bool result = false;
            try
            {
                result = CreateWorker.Create(data);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        public bool CreateList(List<HIS_ROOM_TYPE_MODULE> listData)
        {
            bool result = false;
            try
            {
                result = CreateWorker.CreateList(listData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        public bool Update(HIS_ROOM_TYPE_MODULE data)
        {
            bool result = false;
            try
            {
                result = UpdateWorker.Update(data);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        public bool UpdateList(List<HIS_ROOM_TYPE_MODULE> listData)
        {
            bool result = false;
            try
            {
                result = UpdateWorker.UpdateList(listData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        public bool Delete(HIS_ROOM_TYPE_MODULE data)
        {
            bool result = false;
            try
            {
                result = DeleteWorker.Delete(data);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        public bool DeleteList(List<HIS_ROOM_TYPE_MODULE> listData)
        {
            bool result = false;

            try
            {
                result = DeleteWorker.DeleteList(listData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        public bool Truncate(HIS_ROOM_TYPE_MODULE data)
        {
            bool result = false;
            try
            {
                result = TruncateWorker.Truncate(data);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        public bool TruncateList(List<HIS_ROOM_TYPE_MODULE> listData)
        {
            bool result = false;
            try
            {
                result = TruncateWorker.TruncateList(listData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        public List<HIS_ROOM_TYPE_MODULE> Get(HisRoomTypeModuleSO search, CommonParam param)
        {
            List<HIS_ROOM_TYPE_MODULE> result = new List<HIS_ROOM_TYPE_MODULE>();
            try
            {
                result = GetWorker.Get(search, param);
            }
            catch (Exception ex)
            {
                param.HasException = true;
                Inventec.Common.Logging.LogSystem.Error(ex);
                result.Clear();
            }
            return result;
        }

        public HIS_ROOM_TYPE_MODULE GetById(long id, HisRoomTypeModuleSO search)
        {
            HIS_ROOM_TYPE_MODULE result = null;
            try
            {
                result = GetWorker.GetById(id, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }

            return result;
        }

        public bool IsUnLock(long id)
        {
            try
            {
                return CheckWorker.IsUnLock(id);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                throw;
            }
        }
    }
}
