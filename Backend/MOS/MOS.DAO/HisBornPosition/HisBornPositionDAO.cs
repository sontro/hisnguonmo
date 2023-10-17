using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisBornPosition
{
    public partial class HisBornPositionDAO : EntityBase
    {
        private HisBornPositionCreate CreateWorker
        {
            get
            {
                return (HisBornPositionCreate)Worker.Get<HisBornPositionCreate>();
            }
        }
        private HisBornPositionUpdate UpdateWorker
        {
            get
            {
                return (HisBornPositionUpdate)Worker.Get<HisBornPositionUpdate>();
            }
        }
        private HisBornPositionDelete DeleteWorker
        {
            get
            {
                return (HisBornPositionDelete)Worker.Get<HisBornPositionDelete>();
            }
        }
        private HisBornPositionTruncate TruncateWorker
        {
            get
            {
                return (HisBornPositionTruncate)Worker.Get<HisBornPositionTruncate>();
            }
        }
        private HisBornPositionGet GetWorker
        {
            get
            {
                return (HisBornPositionGet)Worker.Get<HisBornPositionGet>();
            }
        }
        private HisBornPositionCheck CheckWorker
        {
            get
            {
                return (HisBornPositionCheck)Worker.Get<HisBornPositionCheck>();
            }
        }

        public bool Create(HIS_BORN_POSITION data)
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

        public bool CreateList(List<HIS_BORN_POSITION> listData)
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

        public bool Update(HIS_BORN_POSITION data)
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

        public bool UpdateList(List<HIS_BORN_POSITION> listData)
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

        public bool Delete(HIS_BORN_POSITION data)
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

        public bool DeleteList(List<HIS_BORN_POSITION> listData)
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

        public bool Truncate(HIS_BORN_POSITION data)
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

        public bool TruncateList(List<HIS_BORN_POSITION> listData)
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

        public List<HIS_BORN_POSITION> Get(HisBornPositionSO search, CommonParam param)
        {
            List<HIS_BORN_POSITION> result = new List<HIS_BORN_POSITION>();
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

        public HIS_BORN_POSITION GetById(long id, HisBornPositionSO search)
        {
            HIS_BORN_POSITION result = null;
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
