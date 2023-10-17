using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisEmotionlessMethod
{
    public partial class HisEmotionlessMethodDAO : EntityBase
    {
        private HisEmotionlessMethodCreate CreateWorker
        {
            get
            {
                return (HisEmotionlessMethodCreate)Worker.Get<HisEmotionlessMethodCreate>();
            }
        }
        private HisEmotionlessMethodUpdate UpdateWorker
        {
            get
            {
                return (HisEmotionlessMethodUpdate)Worker.Get<HisEmotionlessMethodUpdate>();
            }
        }
        private HisEmotionlessMethodDelete DeleteWorker
        {
            get
            {
                return (HisEmotionlessMethodDelete)Worker.Get<HisEmotionlessMethodDelete>();
            }
        }
        private HisEmotionlessMethodTruncate TruncateWorker
        {
            get
            {
                return (HisEmotionlessMethodTruncate)Worker.Get<HisEmotionlessMethodTruncate>();
            }
        }
        private HisEmotionlessMethodGet GetWorker
        {
            get
            {
                return (HisEmotionlessMethodGet)Worker.Get<HisEmotionlessMethodGet>();
            }
        }
        private HisEmotionlessMethodCheck CheckWorker
        {
            get
            {
                return (HisEmotionlessMethodCheck)Worker.Get<HisEmotionlessMethodCheck>();
            }
        }

        public bool Create(HIS_EMOTIONLESS_METHOD data)
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

        public bool CreateList(List<HIS_EMOTIONLESS_METHOD> listData)
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

        public bool Update(HIS_EMOTIONLESS_METHOD data)
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

        public bool UpdateList(List<HIS_EMOTIONLESS_METHOD> listData)
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

        public bool Delete(HIS_EMOTIONLESS_METHOD data)
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

        public bool DeleteList(List<HIS_EMOTIONLESS_METHOD> listData)
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

        public bool Truncate(HIS_EMOTIONLESS_METHOD data)
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

        public bool TruncateList(List<HIS_EMOTIONLESS_METHOD> listData)
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

        public List<HIS_EMOTIONLESS_METHOD> Get(HisEmotionlessMethodSO search, CommonParam param)
        {
            List<HIS_EMOTIONLESS_METHOD> result = new List<HIS_EMOTIONLESS_METHOD>();
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

        public HIS_EMOTIONLESS_METHOD GetById(long id, HisEmotionlessMethodSO search)
        {
            HIS_EMOTIONLESS_METHOD result = null;
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
