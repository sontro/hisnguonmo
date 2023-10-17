using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisHealthExamRank
{
    public partial class HisHealthExamRankDAO : EntityBase
    {
        private HisHealthExamRankCreate CreateWorker
        {
            get
            {
                return (HisHealthExamRankCreate)Worker.Get<HisHealthExamRankCreate>();
            }
        }
        private HisHealthExamRankUpdate UpdateWorker
        {
            get
            {
                return (HisHealthExamRankUpdate)Worker.Get<HisHealthExamRankUpdate>();
            }
        }
        private HisHealthExamRankDelete DeleteWorker
        {
            get
            {
                return (HisHealthExamRankDelete)Worker.Get<HisHealthExamRankDelete>();
            }
        }
        private HisHealthExamRankTruncate TruncateWorker
        {
            get
            {
                return (HisHealthExamRankTruncate)Worker.Get<HisHealthExamRankTruncate>();
            }
        }
        private HisHealthExamRankGet GetWorker
        {
            get
            {
                return (HisHealthExamRankGet)Worker.Get<HisHealthExamRankGet>();
            }
        }
        private HisHealthExamRankCheck CheckWorker
        {
            get
            {
                return (HisHealthExamRankCheck)Worker.Get<HisHealthExamRankCheck>();
            }
        }

        public bool Create(HIS_HEALTH_EXAM_RANK data)
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

        public bool CreateList(List<HIS_HEALTH_EXAM_RANK> listData)
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

        public bool Update(HIS_HEALTH_EXAM_RANK data)
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

        public bool UpdateList(List<HIS_HEALTH_EXAM_RANK> listData)
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

        public bool Delete(HIS_HEALTH_EXAM_RANK data)
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

        public bool DeleteList(List<HIS_HEALTH_EXAM_RANK> listData)
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

        public bool Truncate(HIS_HEALTH_EXAM_RANK data)
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

        public bool TruncateList(List<HIS_HEALTH_EXAM_RANK> listData)
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

        public List<HIS_HEALTH_EXAM_RANK> Get(HisHealthExamRankSO search, CommonParam param)
        {
            List<HIS_HEALTH_EXAM_RANK> result = new List<HIS_HEALTH_EXAM_RANK>();
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

        public HIS_HEALTH_EXAM_RANK GetById(long id, HisHealthExamRankSO search)
        {
            HIS_HEALTH_EXAM_RANK result = null;
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
