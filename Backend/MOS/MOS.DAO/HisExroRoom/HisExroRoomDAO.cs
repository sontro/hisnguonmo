using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisExroRoom
{
    public partial class HisExroRoomDAO : EntityBase
    {
        private HisExroRoomCreate CreateWorker
        {
            get
            {
                return (HisExroRoomCreate)Worker.Get<HisExroRoomCreate>();
            }
        }
        private HisExroRoomUpdate UpdateWorker
        {
            get
            {
                return (HisExroRoomUpdate)Worker.Get<HisExroRoomUpdate>();
            }
        }
        private HisExroRoomDelete DeleteWorker
        {
            get
            {
                return (HisExroRoomDelete)Worker.Get<HisExroRoomDelete>();
            }
        }
        private HisExroRoomTruncate TruncateWorker
        {
            get
            {
                return (HisExroRoomTruncate)Worker.Get<HisExroRoomTruncate>();
            }
        }
        private HisExroRoomGet GetWorker
        {
            get
            {
                return (HisExroRoomGet)Worker.Get<HisExroRoomGet>();
            }
        }
        private HisExroRoomCheck CheckWorker
        {
            get
            {
                return (HisExroRoomCheck)Worker.Get<HisExroRoomCheck>();
            }
        }

        public bool Create(HIS_EXRO_ROOM data)
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

        public bool CreateList(List<HIS_EXRO_ROOM> listData)
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

        public bool Update(HIS_EXRO_ROOM data)
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

        public bool UpdateList(List<HIS_EXRO_ROOM> listData)
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

        public bool Delete(HIS_EXRO_ROOM data)
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

        public bool DeleteList(List<HIS_EXRO_ROOM> listData)
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

        public bool Truncate(HIS_EXRO_ROOM data)
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

        public bool TruncateList(List<HIS_EXRO_ROOM> listData)
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

        public List<HIS_EXRO_ROOM> Get(HisExroRoomSO search, CommonParam param)
        {
            List<HIS_EXRO_ROOM> result = new List<HIS_EXRO_ROOM>();
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

        public HIS_EXRO_ROOM GetById(long id, HisExroRoomSO search)
        {
            HIS_EXRO_ROOM result = null;
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
