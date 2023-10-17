using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisExecuteRoom
{
    public partial class HisExecuteRoomDAO : EntityBase
    {
        private HisExecuteRoomCreate CreateWorker
        {
            get
            {
                return (HisExecuteRoomCreate)Worker.Get<HisExecuteRoomCreate>();
            }
        }
        private HisExecuteRoomUpdate UpdateWorker
        {
            get
            {
                return (HisExecuteRoomUpdate)Worker.Get<HisExecuteRoomUpdate>();
            }
        }
        private HisExecuteRoomDelete DeleteWorker
        {
            get
            {
                return (HisExecuteRoomDelete)Worker.Get<HisExecuteRoomDelete>();
            }
        }
        private HisExecuteRoomTruncate TruncateWorker
        {
            get
            {
                return (HisExecuteRoomTruncate)Worker.Get<HisExecuteRoomTruncate>();
            }
        }
        private HisExecuteRoomGet GetWorker
        {
            get
            {
                return (HisExecuteRoomGet)Worker.Get<HisExecuteRoomGet>();
            }
        }
        private HisExecuteRoomCheck CheckWorker
        {
            get
            {
                return (HisExecuteRoomCheck)Worker.Get<HisExecuteRoomCheck>();
            }
        }

        public bool Create(HIS_EXECUTE_ROOM data)
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

        public bool CreateList(List<HIS_EXECUTE_ROOM> listData)
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

        public bool Update(HIS_EXECUTE_ROOM data)
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

        public bool UpdateList(List<HIS_EXECUTE_ROOM> listData)
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

        public bool Delete(HIS_EXECUTE_ROOM data)
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

        public bool DeleteList(List<HIS_EXECUTE_ROOM> listData)
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

        public bool Truncate(HIS_EXECUTE_ROOM data)
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

        public bool TruncateList(List<HIS_EXECUTE_ROOM> listData)
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

        public List<HIS_EXECUTE_ROOM> Get(HisExecuteRoomSO search, CommonParam param)
        {
            List<HIS_EXECUTE_ROOM> result = new List<HIS_EXECUTE_ROOM>();
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

        public HIS_EXECUTE_ROOM GetById(long id, HisExecuteRoomSO search)
        {
            HIS_EXECUTE_ROOM result = null;
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
