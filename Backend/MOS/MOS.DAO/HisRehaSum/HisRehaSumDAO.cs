using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisRehaSum
{
    public partial class HisRehaSumDAO : EntityBase
    {
        private HisRehaSumCreate CreateWorker
        {
            get
            {
                return (HisRehaSumCreate)Worker.Get<HisRehaSumCreate>();
            }
        }
        private HisRehaSumUpdate UpdateWorker
        {
            get
            {
                return (HisRehaSumUpdate)Worker.Get<HisRehaSumUpdate>();
            }
        }
        private HisRehaSumDelete DeleteWorker
        {
            get
            {
                return (HisRehaSumDelete)Worker.Get<HisRehaSumDelete>();
            }
        }
        private HisRehaSumTruncate TruncateWorker
        {
            get
            {
                return (HisRehaSumTruncate)Worker.Get<HisRehaSumTruncate>();
            }
        }
        private HisRehaSumGet GetWorker
        {
            get
            {
                return (HisRehaSumGet)Worker.Get<HisRehaSumGet>();
            }
        }
        private HisRehaSumCheck CheckWorker
        {
            get
            {
                return (HisRehaSumCheck)Worker.Get<HisRehaSumCheck>();
            }
        }

        public bool Create(HIS_REHA_SUM data)
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

        public bool CreateList(List<HIS_REHA_SUM> listData)
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

        public bool Update(HIS_REHA_SUM data)
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

        public bool UpdateList(List<HIS_REHA_SUM> listData)
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

        public bool Delete(HIS_REHA_SUM data)
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

        public bool DeleteList(List<HIS_REHA_SUM> listData)
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

        public bool Truncate(HIS_REHA_SUM data)
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

        public bool TruncateList(List<HIS_REHA_SUM> listData)
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

        public List<HIS_REHA_SUM> Get(HisRehaSumSO search, CommonParam param)
        {
            List<HIS_REHA_SUM> result = new List<HIS_REHA_SUM>();
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

        public HIS_REHA_SUM GetById(long id, HisRehaSumSO search)
        {
            HIS_REHA_SUM result = null;
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
