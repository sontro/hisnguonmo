using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisRehaTrainType
{
    public partial class HisRehaTrainTypeDAO : EntityBase
    {
        private HisRehaTrainTypeCreate CreateWorker
        {
            get
            {
                return (HisRehaTrainTypeCreate)Worker.Get<HisRehaTrainTypeCreate>();
            }
        }
        private HisRehaTrainTypeUpdate UpdateWorker
        {
            get
            {
                return (HisRehaTrainTypeUpdate)Worker.Get<HisRehaTrainTypeUpdate>();
            }
        }
        private HisRehaTrainTypeDelete DeleteWorker
        {
            get
            {
                return (HisRehaTrainTypeDelete)Worker.Get<HisRehaTrainTypeDelete>();
            }
        }
        private HisRehaTrainTypeTruncate TruncateWorker
        {
            get
            {
                return (HisRehaTrainTypeTruncate)Worker.Get<HisRehaTrainTypeTruncate>();
            }
        }
        private HisRehaTrainTypeGet GetWorker
        {
            get
            {
                return (HisRehaTrainTypeGet)Worker.Get<HisRehaTrainTypeGet>();
            }
        }
        private HisRehaTrainTypeCheck CheckWorker
        {
            get
            {
                return (HisRehaTrainTypeCheck)Worker.Get<HisRehaTrainTypeCheck>();
            }
        }

        public bool Create(HIS_REHA_TRAIN_TYPE data)
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

        public bool CreateList(List<HIS_REHA_TRAIN_TYPE> listData)
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

        public bool Update(HIS_REHA_TRAIN_TYPE data)
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

        public bool UpdateList(List<HIS_REHA_TRAIN_TYPE> listData)
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

        public bool Delete(HIS_REHA_TRAIN_TYPE data)
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

        public bool DeleteList(List<HIS_REHA_TRAIN_TYPE> listData)
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

        public bool Truncate(HIS_REHA_TRAIN_TYPE data)
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

        public bool TruncateList(List<HIS_REHA_TRAIN_TYPE> listData)
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

        public List<HIS_REHA_TRAIN_TYPE> Get(HisRehaTrainTypeSO search, CommonParam param)
        {
            List<HIS_REHA_TRAIN_TYPE> result = new List<HIS_REHA_TRAIN_TYPE>();
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

        public HIS_REHA_TRAIN_TYPE GetById(long id, HisRehaTrainTypeSO search)
        {
            HIS_REHA_TRAIN_TYPE result = null;
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
