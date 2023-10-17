using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisServSegr
{
    public partial class HisServSegrDAO : EntityBase
    {
        private HisServSegrCreate CreateWorker
        {
            get
            {
                return (HisServSegrCreate)Worker.Get<HisServSegrCreate>();
            }
        }
        private HisServSegrUpdate UpdateWorker
        {
            get
            {
                return (HisServSegrUpdate)Worker.Get<HisServSegrUpdate>();
            }
        }
        private HisServSegrDelete DeleteWorker
        {
            get
            {
                return (HisServSegrDelete)Worker.Get<HisServSegrDelete>();
            }
        }
        private HisServSegrTruncate TruncateWorker
        {
            get
            {
                return (HisServSegrTruncate)Worker.Get<HisServSegrTruncate>();
            }
        }
        private HisServSegrGet GetWorker
        {
            get
            {
                return (HisServSegrGet)Worker.Get<HisServSegrGet>();
            }
        }
        private HisServSegrCheck CheckWorker
        {
            get
            {
                return (HisServSegrCheck)Worker.Get<HisServSegrCheck>();
            }
        }

        public bool Create(HIS_SERV_SEGR data)
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

        public bool CreateList(List<HIS_SERV_SEGR> listData)
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

        public bool Update(HIS_SERV_SEGR data)
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

        public bool UpdateList(List<HIS_SERV_SEGR> listData)
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

        public bool Delete(HIS_SERV_SEGR data)
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

        public bool DeleteList(List<HIS_SERV_SEGR> listData)
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

        public bool Truncate(HIS_SERV_SEGR data)
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

        public bool TruncateList(List<HIS_SERV_SEGR> listData)
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

        public List<HIS_SERV_SEGR> Get(HisServSegrSO search, CommonParam param)
        {
            List<HIS_SERV_SEGR> result = new List<HIS_SERV_SEGR>();
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

        public HIS_SERV_SEGR GetById(long id, HisServSegrSO search)
        {
            HIS_SERV_SEGR result = null;
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
