using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisImpMestBlood
{
    public partial class HisImpMestBloodDAO : EntityBase
    {
        private HisImpMestBloodCreate CreateWorker
        {
            get
            {
                return (HisImpMestBloodCreate)Worker.Get<HisImpMestBloodCreate>();
            }
        }
        private HisImpMestBloodUpdate UpdateWorker
        {
            get
            {
                return (HisImpMestBloodUpdate)Worker.Get<HisImpMestBloodUpdate>();
            }
        }
        private HisImpMestBloodDelete DeleteWorker
        {
            get
            {
                return (HisImpMestBloodDelete)Worker.Get<HisImpMestBloodDelete>();
            }
        }
        private HisImpMestBloodTruncate TruncateWorker
        {
            get
            {
                return (HisImpMestBloodTruncate)Worker.Get<HisImpMestBloodTruncate>();
            }
        }
        private HisImpMestBloodGet GetWorker
        {
            get
            {
                return (HisImpMestBloodGet)Worker.Get<HisImpMestBloodGet>();
            }
        }
        private HisImpMestBloodCheck CheckWorker
        {
            get
            {
                return (HisImpMestBloodCheck)Worker.Get<HisImpMestBloodCheck>();
            }
        }

        public bool Create(HIS_IMP_MEST_BLOOD data)
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

        public bool CreateList(List<HIS_IMP_MEST_BLOOD> listData)
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

        public bool Update(HIS_IMP_MEST_BLOOD data)
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

        public bool UpdateList(List<HIS_IMP_MEST_BLOOD> listData)
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

        public bool Delete(HIS_IMP_MEST_BLOOD data)
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

        public bool DeleteList(List<HIS_IMP_MEST_BLOOD> listData)
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

        public bool Truncate(HIS_IMP_MEST_BLOOD data)
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

        public bool TruncateList(List<HIS_IMP_MEST_BLOOD> listData)
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

        public List<HIS_IMP_MEST_BLOOD> Get(HisImpMestBloodSO search, CommonParam param)
        {
            List<HIS_IMP_MEST_BLOOD> result = new List<HIS_IMP_MEST_BLOOD>();
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

        public HIS_IMP_MEST_BLOOD GetById(long id, HisImpMestBloodSO search)
        {
            HIS_IMP_MEST_BLOOD result = null;
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
