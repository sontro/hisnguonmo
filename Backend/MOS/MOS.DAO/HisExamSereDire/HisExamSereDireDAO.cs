using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisExamSereDire
{
    public partial class HisExamSereDireDAO : EntityBase
    {
        private HisExamSereDireCreate CreateWorker
        {
            get
            {
                return (HisExamSereDireCreate)Worker.Get<HisExamSereDireCreate>();
            }
        }
        private HisExamSereDireUpdate UpdateWorker
        {
            get
            {
                return (HisExamSereDireUpdate)Worker.Get<HisExamSereDireUpdate>();
            }
        }
        private HisExamSereDireDelete DeleteWorker
        {
            get
            {
                return (HisExamSereDireDelete)Worker.Get<HisExamSereDireDelete>();
            }
        }
        private HisExamSereDireTruncate TruncateWorker
        {
            get
            {
                return (HisExamSereDireTruncate)Worker.Get<HisExamSereDireTruncate>();
            }
        }
        private HisExamSereDireGet GetWorker
        {
            get
            {
                return (HisExamSereDireGet)Worker.Get<HisExamSereDireGet>();
            }
        }
        private HisExamSereDireCheck CheckWorker
        {
            get
            {
                return (HisExamSereDireCheck)Worker.Get<HisExamSereDireCheck>();
            }
        }

        public bool Create(HIS_EXAM_SERE_DIRE data)
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

        public bool CreateList(List<HIS_EXAM_SERE_DIRE> listData)
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

        public bool Update(HIS_EXAM_SERE_DIRE data)
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

        public bool UpdateList(List<HIS_EXAM_SERE_DIRE> listData)
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

        public bool Delete(HIS_EXAM_SERE_DIRE data)
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

        public bool DeleteList(List<HIS_EXAM_SERE_DIRE> listData)
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

        public bool Truncate(HIS_EXAM_SERE_DIRE data)
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

        public bool TruncateList(List<HIS_EXAM_SERE_DIRE> listData)
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

        public List<HIS_EXAM_SERE_DIRE> Get(HisExamSereDireSO search, CommonParam param)
        {
            List<HIS_EXAM_SERE_DIRE> result = new List<HIS_EXAM_SERE_DIRE>();
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

        public HIS_EXAM_SERE_DIRE GetById(long id, HisExamSereDireSO search)
        {
            HIS_EXAM_SERE_DIRE result = null;
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
