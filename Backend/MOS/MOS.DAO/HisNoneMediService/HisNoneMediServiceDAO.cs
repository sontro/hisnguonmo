using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisNoneMediService
{
    public partial class HisNoneMediServiceDAO : EntityBase
    {
        private HisNoneMediServiceCreate CreateWorker
        {
            get
            {
                return (HisNoneMediServiceCreate)Worker.Get<HisNoneMediServiceCreate>();
            }
        }
        private HisNoneMediServiceUpdate UpdateWorker
        {
            get
            {
                return (HisNoneMediServiceUpdate)Worker.Get<HisNoneMediServiceUpdate>();
            }
        }
        private HisNoneMediServiceDelete DeleteWorker
        {
            get
            {
                return (HisNoneMediServiceDelete)Worker.Get<HisNoneMediServiceDelete>();
            }
        }
        private HisNoneMediServiceTruncate TruncateWorker
        {
            get
            {
                return (HisNoneMediServiceTruncate)Worker.Get<HisNoneMediServiceTruncate>();
            }
        }
        private HisNoneMediServiceGet GetWorker
        {
            get
            {
                return (HisNoneMediServiceGet)Worker.Get<HisNoneMediServiceGet>();
            }
        }
        private HisNoneMediServiceCheck CheckWorker
        {
            get
            {
                return (HisNoneMediServiceCheck)Worker.Get<HisNoneMediServiceCheck>();
            }
        }

        public bool Create(HIS_NONE_MEDI_SERVICE data)
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

        public bool CreateList(List<HIS_NONE_MEDI_SERVICE> listData)
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

        public bool Update(HIS_NONE_MEDI_SERVICE data)
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

        public bool UpdateList(List<HIS_NONE_MEDI_SERVICE> listData)
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

        public bool Delete(HIS_NONE_MEDI_SERVICE data)
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

        public bool DeleteList(List<HIS_NONE_MEDI_SERVICE> listData)
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

        public bool Truncate(HIS_NONE_MEDI_SERVICE data)
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

        public bool TruncateList(List<HIS_NONE_MEDI_SERVICE> listData)
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

        public List<HIS_NONE_MEDI_SERVICE> Get(HisNoneMediServiceSO search, CommonParam param)
        {
            List<HIS_NONE_MEDI_SERVICE> result = new List<HIS_NONE_MEDI_SERVICE>();
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

        public HIS_NONE_MEDI_SERVICE GetById(long id, HisNoneMediServiceSO search)
        {
            HIS_NONE_MEDI_SERVICE result = null;
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
