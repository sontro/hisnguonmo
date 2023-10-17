using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisEmotionlessResult
{
    public partial class HisEmotionlessResultDAO : EntityBase
    {
        private HisEmotionlessResultCreate CreateWorker
        {
            get
            {
                return (HisEmotionlessResultCreate)Worker.Get<HisEmotionlessResultCreate>();
            }
        }
        private HisEmotionlessResultUpdate UpdateWorker
        {
            get
            {
                return (HisEmotionlessResultUpdate)Worker.Get<HisEmotionlessResultUpdate>();
            }
        }
        private HisEmotionlessResultDelete DeleteWorker
        {
            get
            {
                return (HisEmotionlessResultDelete)Worker.Get<HisEmotionlessResultDelete>();
            }
        }
        private HisEmotionlessResultTruncate TruncateWorker
        {
            get
            {
                return (HisEmotionlessResultTruncate)Worker.Get<HisEmotionlessResultTruncate>();
            }
        }
        private HisEmotionlessResultGet GetWorker
        {
            get
            {
                return (HisEmotionlessResultGet)Worker.Get<HisEmotionlessResultGet>();
            }
        }
        private HisEmotionlessResultCheck CheckWorker
        {
            get
            {
                return (HisEmotionlessResultCheck)Worker.Get<HisEmotionlessResultCheck>();
            }
        }

        public bool Create(HIS_EMOTIONLESS_RESULT data)
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

        public bool CreateList(List<HIS_EMOTIONLESS_RESULT> listData)
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

        public bool Update(HIS_EMOTIONLESS_RESULT data)
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

        public bool UpdateList(List<HIS_EMOTIONLESS_RESULT> listData)
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

        public bool Delete(HIS_EMOTIONLESS_RESULT data)
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

        public bool DeleteList(List<HIS_EMOTIONLESS_RESULT> listData)
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

        public bool Truncate(HIS_EMOTIONLESS_RESULT data)
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

        public bool TruncateList(List<HIS_EMOTIONLESS_RESULT> listData)
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

        public List<HIS_EMOTIONLESS_RESULT> Get(HisEmotionlessResultSO search, CommonParam param)
        {
            List<HIS_EMOTIONLESS_RESULT> result = new List<HIS_EMOTIONLESS_RESULT>();
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

        public HIS_EMOTIONLESS_RESULT GetById(long id, HisEmotionlessResultSO search)
        {
            HIS_EMOTIONLESS_RESULT result = null;
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
