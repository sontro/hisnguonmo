using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisExamServiceTemp
{
    public partial class HisExamServiceTempDAO : EntityBase
    {
        private HisExamServiceTempCreate CreateWorker
        {
            get
            {
                return (HisExamServiceTempCreate)Worker.Get<HisExamServiceTempCreate>();
            }
        }
        private HisExamServiceTempUpdate UpdateWorker
        {
            get
            {
                return (HisExamServiceTempUpdate)Worker.Get<HisExamServiceTempUpdate>();
            }
        }
        private HisExamServiceTempDelete DeleteWorker
        {
            get
            {
                return (HisExamServiceTempDelete)Worker.Get<HisExamServiceTempDelete>();
            }
        }
        private HisExamServiceTempTruncate TruncateWorker
        {
            get
            {
                return (HisExamServiceTempTruncate)Worker.Get<HisExamServiceTempTruncate>();
            }
        }
        private HisExamServiceTempGet GetWorker
        {
            get
            {
                return (HisExamServiceTempGet)Worker.Get<HisExamServiceTempGet>();
            }
        }
        private HisExamServiceTempCheck CheckWorker
        {
            get
            {
                return (HisExamServiceTempCheck)Worker.Get<HisExamServiceTempCheck>();
            }
        }

        public bool Create(HIS_EXAM_SERVICE_TEMP data)
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

        public bool CreateList(List<HIS_EXAM_SERVICE_TEMP> listData)
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

        public bool Update(HIS_EXAM_SERVICE_TEMP data)
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

        public bool UpdateList(List<HIS_EXAM_SERVICE_TEMP> listData)
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

        public bool Delete(HIS_EXAM_SERVICE_TEMP data)
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

        public bool DeleteList(List<HIS_EXAM_SERVICE_TEMP> listData)
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

        public bool Truncate(HIS_EXAM_SERVICE_TEMP data)
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

        public bool TruncateList(List<HIS_EXAM_SERVICE_TEMP> listData)
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

        public List<HIS_EXAM_SERVICE_TEMP> Get(HisExamServiceTempSO search, CommonParam param)
        {
            List<HIS_EXAM_SERVICE_TEMP> result = new List<HIS_EXAM_SERVICE_TEMP>();
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

        public HIS_EXAM_SERVICE_TEMP GetById(long id, HisExamServiceTempSO search)
        {
            HIS_EXAM_SERVICE_TEMP result = null;
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
