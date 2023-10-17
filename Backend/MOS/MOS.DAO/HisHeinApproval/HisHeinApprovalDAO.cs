using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisHeinApproval
{
    public partial class HisHeinApprovalDAO : EntityBase
    {
        private HisHeinApprovalCreate CreateWorker
        {
            get
            {
                return (HisHeinApprovalCreate)Worker.Get<HisHeinApprovalCreate>();
            }
        }
        private HisHeinApprovalUpdate UpdateWorker
        {
            get
            {
                return (HisHeinApprovalUpdate)Worker.Get<HisHeinApprovalUpdate>();
            }
        }
        private HisHeinApprovalDelete DeleteWorker
        {
            get
            {
                return (HisHeinApprovalDelete)Worker.Get<HisHeinApprovalDelete>();
            }
        }
        private HisHeinApprovalTruncate TruncateWorker
        {
            get
            {
                return (HisHeinApprovalTruncate)Worker.Get<HisHeinApprovalTruncate>();
            }
        }
        private HisHeinApprovalGet GetWorker
        {
            get
            {
                return (HisHeinApprovalGet)Worker.Get<HisHeinApprovalGet>();
            }
        }
        private HisHeinApprovalCheck CheckWorker
        {
            get
            {
                return (HisHeinApprovalCheck)Worker.Get<HisHeinApprovalCheck>();
            }
        }

        public bool Create(HIS_HEIN_APPROVAL data)
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

        public bool CreateList(List<HIS_HEIN_APPROVAL> listData)
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

        public bool Update(HIS_HEIN_APPROVAL data)
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

        public bool UpdateList(List<HIS_HEIN_APPROVAL> listData)
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

        public bool Delete(HIS_HEIN_APPROVAL data)
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

        public bool DeleteList(List<HIS_HEIN_APPROVAL> listData)
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

        public bool Truncate(HIS_HEIN_APPROVAL data)
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

        public bool TruncateList(List<HIS_HEIN_APPROVAL> listData)
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

        public List<HIS_HEIN_APPROVAL> Get(HisHeinApprovalSO search, CommonParam param)
        {
            List<HIS_HEIN_APPROVAL> result = new List<HIS_HEIN_APPROVAL>();
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

        public HIS_HEIN_APPROVAL GetById(long id, HisHeinApprovalSO search)
        {
            HIS_HEIN_APPROVAL result = null;
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
