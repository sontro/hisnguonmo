using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisPrepareMaty
{
    public partial class HisPrepareMatyDAO : EntityBase
    {
        private HisPrepareMatyCreate CreateWorker
        {
            get
            {
                return (HisPrepareMatyCreate)Worker.Get<HisPrepareMatyCreate>();
            }
        }
        private HisPrepareMatyUpdate UpdateWorker
        {
            get
            {
                return (HisPrepareMatyUpdate)Worker.Get<HisPrepareMatyUpdate>();
            }
        }
        private HisPrepareMatyDelete DeleteWorker
        {
            get
            {
                return (HisPrepareMatyDelete)Worker.Get<HisPrepareMatyDelete>();
            }
        }
        private HisPrepareMatyTruncate TruncateWorker
        {
            get
            {
                return (HisPrepareMatyTruncate)Worker.Get<HisPrepareMatyTruncate>();
            }
        }
        private HisPrepareMatyGet GetWorker
        {
            get
            {
                return (HisPrepareMatyGet)Worker.Get<HisPrepareMatyGet>();
            }
        }
        private HisPrepareMatyCheck CheckWorker
        {
            get
            {
                return (HisPrepareMatyCheck)Worker.Get<HisPrepareMatyCheck>();
            }
        }

        public bool Create(HIS_PREPARE_MATY data)
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

        public bool CreateList(List<HIS_PREPARE_MATY> listData)
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

        public bool Update(HIS_PREPARE_MATY data)
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

        public bool UpdateList(List<HIS_PREPARE_MATY> listData)
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

        public bool Delete(HIS_PREPARE_MATY data)
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

        public bool DeleteList(List<HIS_PREPARE_MATY> listData)
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

        public bool Truncate(HIS_PREPARE_MATY data)
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

        public bool TruncateList(List<HIS_PREPARE_MATY> listData)
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

        public List<HIS_PREPARE_MATY> Get(HisPrepareMatySO search, CommonParam param)
        {
            List<HIS_PREPARE_MATY> result = new List<HIS_PREPARE_MATY>();
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

        public HIS_PREPARE_MATY GetById(long id, HisPrepareMatySO search)
        {
            HIS_PREPARE_MATY result = null;
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
