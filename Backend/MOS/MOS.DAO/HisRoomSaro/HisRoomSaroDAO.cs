using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisRoomSaro
{
    public partial class HisRoomSaroDAO : EntityBase
    {
        private HisRoomSaroCreate CreateWorker
        {
            get
            {
                return (HisRoomSaroCreate)Worker.Get<HisRoomSaroCreate>();
            }
        }
        private HisRoomSaroUpdate UpdateWorker
        {
            get
            {
                return (HisRoomSaroUpdate)Worker.Get<HisRoomSaroUpdate>();
            }
        }
        private HisRoomSaroDelete DeleteWorker
        {
            get
            {
                return (HisRoomSaroDelete)Worker.Get<HisRoomSaroDelete>();
            }
        }
        private HisRoomSaroTruncate TruncateWorker
        {
            get
            {
                return (HisRoomSaroTruncate)Worker.Get<HisRoomSaroTruncate>();
            }
        }
        private HisRoomSaroGet GetWorker
        {
            get
            {
                return (HisRoomSaroGet)Worker.Get<HisRoomSaroGet>();
            }
        }
        private HisRoomSaroCheck CheckWorker
        {
            get
            {
                return (HisRoomSaroCheck)Worker.Get<HisRoomSaroCheck>();
            }
        }

        public bool Create(HIS_ROOM_SARO data)
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

        public bool CreateList(List<HIS_ROOM_SARO> listData)
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

        public bool Update(HIS_ROOM_SARO data)
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

        public bool UpdateList(List<HIS_ROOM_SARO> listData)
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

        public bool Delete(HIS_ROOM_SARO data)
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

        public bool DeleteList(List<HIS_ROOM_SARO> listData)
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

        public bool Truncate(HIS_ROOM_SARO data)
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

        public bool TruncateList(List<HIS_ROOM_SARO> listData)
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

        public List<HIS_ROOM_SARO> Get(HisRoomSaroSO search, CommonParam param)
        {
            List<HIS_ROOM_SARO> result = new List<HIS_ROOM_SARO>();
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

        public HIS_ROOM_SARO GetById(long id, HisRoomSaroSO search)
        {
            HIS_ROOM_SARO result = null;
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
