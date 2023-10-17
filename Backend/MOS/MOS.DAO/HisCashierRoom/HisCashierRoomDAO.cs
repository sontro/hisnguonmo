using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisCashierRoom
{
    public partial class HisCashierRoomDAO : EntityBase
    {
        private HisCashierRoomCreate CreateWorker
        {
            get
            {
                return (HisCashierRoomCreate)Worker.Get<HisCashierRoomCreate>();
            }
        }
        private HisCashierRoomUpdate UpdateWorker
        {
            get
            {
                return (HisCashierRoomUpdate)Worker.Get<HisCashierRoomUpdate>();
            }
        }
        private HisCashierRoomDelete DeleteWorker
        {
            get
            {
                return (HisCashierRoomDelete)Worker.Get<HisCashierRoomDelete>();
            }
        }
        private HisCashierRoomTruncate TruncateWorker
        {
            get
            {
                return (HisCashierRoomTruncate)Worker.Get<HisCashierRoomTruncate>();
            }
        }
        private HisCashierRoomGet GetWorker
        {
            get
            {
                return (HisCashierRoomGet)Worker.Get<HisCashierRoomGet>();
            }
        }
        private HisCashierRoomCheck CheckWorker
        {
            get
            {
                return (HisCashierRoomCheck)Worker.Get<HisCashierRoomCheck>();
            }
        }

        public bool Create(HIS_CASHIER_ROOM data)
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

        public bool CreateList(List<HIS_CASHIER_ROOM> listData)
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

        public bool Update(HIS_CASHIER_ROOM data)
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

        public bool UpdateList(List<HIS_CASHIER_ROOM> listData)
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

        public bool Delete(HIS_CASHIER_ROOM data)
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

        public bool DeleteList(List<HIS_CASHIER_ROOM> listData)
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

        public bool Truncate(HIS_CASHIER_ROOM data)
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

        public bool TruncateList(List<HIS_CASHIER_ROOM> listData)
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

        public List<HIS_CASHIER_ROOM> Get(HisCashierRoomSO search, CommonParam param)
        {
            List<HIS_CASHIER_ROOM> result = new List<HIS_CASHIER_ROOM>();
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

        public HIS_CASHIER_ROOM GetById(long id, HisCashierRoomSO search)
        {
            HIS_CASHIER_ROOM result = null;
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
