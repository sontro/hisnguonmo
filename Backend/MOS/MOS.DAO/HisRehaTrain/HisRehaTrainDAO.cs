using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisRehaTrain
{
    public partial class HisRehaTrainDAO : EntityBase
    {
        private HisRehaTrainCreate CreateWorker
        {
            get
            {
                return (HisRehaTrainCreate)Worker.Get<HisRehaTrainCreate>();
            }
        }
        private HisRehaTrainUpdate UpdateWorker
        {
            get
            {
                return (HisRehaTrainUpdate)Worker.Get<HisRehaTrainUpdate>();
            }
        }
        private HisRehaTrainDelete DeleteWorker
        {
            get
            {
                return (HisRehaTrainDelete)Worker.Get<HisRehaTrainDelete>();
            }
        }
        private HisRehaTrainTruncate TruncateWorker
        {
            get
            {
                return (HisRehaTrainTruncate)Worker.Get<HisRehaTrainTruncate>();
            }
        }
        private HisRehaTrainGet GetWorker
        {
            get
            {
                return (HisRehaTrainGet)Worker.Get<HisRehaTrainGet>();
            }
        }
        private HisRehaTrainCheck CheckWorker
        {
            get
            {
                return (HisRehaTrainCheck)Worker.Get<HisRehaTrainCheck>();
            }
        }

        public bool Create(HIS_REHA_TRAIN data)
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

        public bool CreateList(List<HIS_REHA_TRAIN> listData)
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

        public bool Update(HIS_REHA_TRAIN data)
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

        public bool UpdateList(List<HIS_REHA_TRAIN> listData)
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

        public bool Delete(HIS_REHA_TRAIN data)
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

        public bool DeleteList(List<HIS_REHA_TRAIN> listData)
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

        public bool Truncate(HIS_REHA_TRAIN data)
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

        public bool TruncateList(List<HIS_REHA_TRAIN> listData)
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

        public List<HIS_REHA_TRAIN> Get(HisRehaTrainSO search, CommonParam param)
        {
            List<HIS_REHA_TRAIN> result = new List<HIS_REHA_TRAIN>();
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

        public HIS_REHA_TRAIN GetById(long id, HisRehaTrainSO search)
        {
            HIS_REHA_TRAIN result = null;
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
