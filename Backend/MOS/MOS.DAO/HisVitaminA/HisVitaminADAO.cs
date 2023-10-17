using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisVitaminA
{
    public partial class HisVitaminADAO : EntityBase
    {
        private HisVitaminACreate CreateWorker
        {
            get
            {
                return (HisVitaminACreate)Worker.Get<HisVitaminACreate>();
            }
        }
        private HisVitaminAUpdate UpdateWorker
        {
            get
            {
                return (HisVitaminAUpdate)Worker.Get<HisVitaminAUpdate>();
            }
        }
        private HisVitaminADelete DeleteWorker
        {
            get
            {
                return (HisVitaminADelete)Worker.Get<HisVitaminADelete>();
            }
        }
        private HisVitaminATruncate TruncateWorker
        {
            get
            {
                return (HisVitaminATruncate)Worker.Get<HisVitaminATruncate>();
            }
        }
        private HisVitaminAGet GetWorker
        {
            get
            {
                return (HisVitaminAGet)Worker.Get<HisVitaminAGet>();
            }
        }
        private HisVitaminACheck CheckWorker
        {
            get
            {
                return (HisVitaminACheck)Worker.Get<HisVitaminACheck>();
            }
        }

        public bool Create(HIS_VITAMIN_A data)
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

        public bool CreateList(List<HIS_VITAMIN_A> listData)
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

        public bool Update(HIS_VITAMIN_A data)
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

        public bool UpdateList(List<HIS_VITAMIN_A> listData)
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

        public bool Delete(HIS_VITAMIN_A data)
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

        public bool DeleteList(List<HIS_VITAMIN_A> listData)
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

        public bool Truncate(HIS_VITAMIN_A data)
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

        public bool TruncateList(List<HIS_VITAMIN_A> listData)
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

        public List<HIS_VITAMIN_A> Get(HisVitaminASO search, CommonParam param)
        {
            List<HIS_VITAMIN_A> result = new List<HIS_VITAMIN_A>();
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

        public HIS_VITAMIN_A GetById(long id, HisVitaminASO search)
        {
            HIS_VITAMIN_A result = null;
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
