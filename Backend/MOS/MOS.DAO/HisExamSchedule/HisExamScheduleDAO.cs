using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisExamSchedule
{
    public partial class HisExamScheduleDAO : EntityBase
    {
        private HisExamScheduleCreate CreateWorker
        {
            get
            {
                return (HisExamScheduleCreate)Worker.Get<HisExamScheduleCreate>();
            }
        }
        private HisExamScheduleUpdate UpdateWorker
        {
            get
            {
                return (HisExamScheduleUpdate)Worker.Get<HisExamScheduleUpdate>();
            }
        }
        private HisExamScheduleDelete DeleteWorker
        {
            get
            {
                return (HisExamScheduleDelete)Worker.Get<HisExamScheduleDelete>();
            }
        }
        private HisExamScheduleTruncate TruncateWorker
        {
            get
            {
                return (HisExamScheduleTruncate)Worker.Get<HisExamScheduleTruncate>();
            }
        }
        private HisExamScheduleGet GetWorker
        {
            get
            {
                return (HisExamScheduleGet)Worker.Get<HisExamScheduleGet>();
            }
        }
        private HisExamScheduleCheck CheckWorker
        {
            get
            {
                return (HisExamScheduleCheck)Worker.Get<HisExamScheduleCheck>();
            }
        }

        public bool Create(HIS_EXAM_SCHEDULE data)
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

        public bool CreateList(List<HIS_EXAM_SCHEDULE> listData)
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

        public bool Update(HIS_EXAM_SCHEDULE data)
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

        public bool UpdateList(List<HIS_EXAM_SCHEDULE> listData)
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

        public bool Delete(HIS_EXAM_SCHEDULE data)
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

        public bool DeleteList(List<HIS_EXAM_SCHEDULE> listData)
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

        public bool Truncate(HIS_EXAM_SCHEDULE data)
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

        public bool TruncateList(List<HIS_EXAM_SCHEDULE> listData)
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

        public List<HIS_EXAM_SCHEDULE> Get(HisExamScheduleSO search, CommonParam param)
        {
            List<HIS_EXAM_SCHEDULE> result = new List<HIS_EXAM_SCHEDULE>();
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

        public HIS_EXAM_SCHEDULE GetById(long id, HisExamScheduleSO search)
        {
            HIS_EXAM_SCHEDULE result = null;
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
