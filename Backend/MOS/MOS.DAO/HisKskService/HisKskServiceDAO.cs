using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisKskService
{
    public partial class HisKskServiceDAO : EntityBase
    {
        private HisKskServiceCreate CreateWorker
        {
            get
            {
                return (HisKskServiceCreate)Worker.Get<HisKskServiceCreate>();
            }
        }
        private HisKskServiceUpdate UpdateWorker
        {
            get
            {
                return (HisKskServiceUpdate)Worker.Get<HisKskServiceUpdate>();
            }
        }
        private HisKskServiceDelete DeleteWorker
        {
            get
            {
                return (HisKskServiceDelete)Worker.Get<HisKskServiceDelete>();
            }
        }
        private HisKskServiceTruncate TruncateWorker
        {
            get
            {
                return (HisKskServiceTruncate)Worker.Get<HisKskServiceTruncate>();
            }
        }
        private HisKskServiceGet GetWorker
        {
            get
            {
                return (HisKskServiceGet)Worker.Get<HisKskServiceGet>();
            }
        }
        private HisKskServiceCheck CheckWorker
        {
            get
            {
                return (HisKskServiceCheck)Worker.Get<HisKskServiceCheck>();
            }
        }

        public bool Create(HIS_KSK_SERVICE data)
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

        public bool CreateList(List<HIS_KSK_SERVICE> listData)
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

        public bool Update(HIS_KSK_SERVICE data)
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

        public bool UpdateList(List<HIS_KSK_SERVICE> listData)
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

        public bool Delete(HIS_KSK_SERVICE data)
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

        public bool DeleteList(List<HIS_KSK_SERVICE> listData)
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

        public bool Truncate(HIS_KSK_SERVICE data)
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

        public bool TruncateList(List<HIS_KSK_SERVICE> listData)
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

        public List<HIS_KSK_SERVICE> Get(HisKskServiceSO search, CommonParam param)
        {
            List<HIS_KSK_SERVICE> result = new List<HIS_KSK_SERVICE>();
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

        public HIS_KSK_SERVICE GetById(long id, HisKskServiceSO search)
        {
            HIS_KSK_SERVICE result = null;
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
