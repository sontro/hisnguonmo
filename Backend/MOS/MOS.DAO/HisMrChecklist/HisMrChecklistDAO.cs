using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMrChecklist
{
    public partial class HisMrChecklistDAO : EntityBase
    {
        private HisMrChecklistCreate CreateWorker
        {
            get
            {
                return (HisMrChecklistCreate)Worker.Get<HisMrChecklistCreate>();
            }
        }
        private HisMrChecklistUpdate UpdateWorker
        {
            get
            {
                return (HisMrChecklistUpdate)Worker.Get<HisMrChecklistUpdate>();
            }
        }
        private HisMrChecklistDelete DeleteWorker
        {
            get
            {
                return (HisMrChecklistDelete)Worker.Get<HisMrChecklistDelete>();
            }
        }
        private HisMrChecklistTruncate TruncateWorker
        {
            get
            {
                return (HisMrChecklistTruncate)Worker.Get<HisMrChecklistTruncate>();
            }
        }
        private HisMrChecklistGet GetWorker
        {
            get
            {
                return (HisMrChecklistGet)Worker.Get<HisMrChecklistGet>();
            }
        }
        private HisMrChecklistCheck CheckWorker
        {
            get
            {
                return (HisMrChecklistCheck)Worker.Get<HisMrChecklistCheck>();
            }
        }

        public bool Create(HIS_MR_CHECKLIST data)
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

        public bool CreateList(List<HIS_MR_CHECKLIST> listData)
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

        public bool Update(HIS_MR_CHECKLIST data)
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

        public bool UpdateList(List<HIS_MR_CHECKLIST> listData)
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

        public bool Delete(HIS_MR_CHECKLIST data)
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

        public bool DeleteList(List<HIS_MR_CHECKLIST> listData)
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

        public bool Truncate(HIS_MR_CHECKLIST data)
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

        public bool TruncateList(List<HIS_MR_CHECKLIST> listData)
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

        public List<HIS_MR_CHECKLIST> Get(HisMrChecklistSO search, CommonParam param)
        {
            List<HIS_MR_CHECKLIST> result = new List<HIS_MR_CHECKLIST>();
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

        public HIS_MR_CHECKLIST GetById(long id, HisMrChecklistSO search)
        {
            HIS_MR_CHECKLIST result = null;
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
