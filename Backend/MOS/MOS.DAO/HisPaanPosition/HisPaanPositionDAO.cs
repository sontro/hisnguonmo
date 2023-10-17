using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisPaanPosition
{
    public partial class HisPaanPositionDAO : EntityBase
    {
        private HisPaanPositionCreate CreateWorker
        {
            get
            {
                return (HisPaanPositionCreate)Worker.Get<HisPaanPositionCreate>();
            }
        }
        private HisPaanPositionUpdate UpdateWorker
        {
            get
            {
                return (HisPaanPositionUpdate)Worker.Get<HisPaanPositionUpdate>();
            }
        }
        private HisPaanPositionDelete DeleteWorker
        {
            get
            {
                return (HisPaanPositionDelete)Worker.Get<HisPaanPositionDelete>();
            }
        }
        private HisPaanPositionTruncate TruncateWorker
        {
            get
            {
                return (HisPaanPositionTruncate)Worker.Get<HisPaanPositionTruncate>();
            }
        }
        private HisPaanPositionGet GetWorker
        {
            get
            {
                return (HisPaanPositionGet)Worker.Get<HisPaanPositionGet>();
            }
        }
        private HisPaanPositionCheck CheckWorker
        {
            get
            {
                return (HisPaanPositionCheck)Worker.Get<HisPaanPositionCheck>();
            }
        }

        public bool Create(HIS_PAAN_POSITION data)
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

        public bool CreateList(List<HIS_PAAN_POSITION> listData)
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

        public bool Update(HIS_PAAN_POSITION data)
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

        public bool UpdateList(List<HIS_PAAN_POSITION> listData)
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

        public bool Delete(HIS_PAAN_POSITION data)
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

        public bool DeleteList(List<HIS_PAAN_POSITION> listData)
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

        public bool Truncate(HIS_PAAN_POSITION data)
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

        public bool TruncateList(List<HIS_PAAN_POSITION> listData)
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

        public List<HIS_PAAN_POSITION> Get(HisPaanPositionSO search, CommonParam param)
        {
            List<HIS_PAAN_POSITION> result = new List<HIS_PAAN_POSITION>();
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

        public HIS_PAAN_POSITION GetById(long id, HisPaanPositionSO search)
        {
            HIS_PAAN_POSITION result = null;
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
