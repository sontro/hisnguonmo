using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisImpMestType
{
    public partial class HisImpMestTypeDAO : EntityBase
    {
        private HisImpMestTypeCreate CreateWorker
        {
            get
            {
                return (HisImpMestTypeCreate)Worker.Get<HisImpMestTypeCreate>();
            }
        }
        private HisImpMestTypeUpdate UpdateWorker
        {
            get
            {
                return (HisImpMestTypeUpdate)Worker.Get<HisImpMestTypeUpdate>();
            }
        }
        private HisImpMestTypeDelete DeleteWorker
        {
            get
            {
                return (HisImpMestTypeDelete)Worker.Get<HisImpMestTypeDelete>();
            }
        }
        private HisImpMestTypeTruncate TruncateWorker
        {
            get
            {
                return (HisImpMestTypeTruncate)Worker.Get<HisImpMestTypeTruncate>();
            }
        }
        private HisImpMestTypeGet GetWorker
        {
            get
            {
                return (HisImpMestTypeGet)Worker.Get<HisImpMestTypeGet>();
            }
        }
        private HisImpMestTypeCheck CheckWorker
        {
            get
            {
                return (HisImpMestTypeCheck)Worker.Get<HisImpMestTypeCheck>();
            }
        }

        public bool Create(HIS_IMP_MEST_TYPE data)
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

        public bool CreateList(List<HIS_IMP_MEST_TYPE> listData)
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

        public bool Update(HIS_IMP_MEST_TYPE data)
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

        public bool UpdateList(List<HIS_IMP_MEST_TYPE> listData)
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

        public bool Delete(HIS_IMP_MEST_TYPE data)
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

        public bool DeleteList(List<HIS_IMP_MEST_TYPE> listData)
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

        public bool Truncate(HIS_IMP_MEST_TYPE data)
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

        public bool TruncateList(List<HIS_IMP_MEST_TYPE> listData)
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

        public List<HIS_IMP_MEST_TYPE> Get(HisImpMestTypeSO search, CommonParam param)
        {
            List<HIS_IMP_MEST_TYPE> result = new List<HIS_IMP_MEST_TYPE>();
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

        public HIS_IMP_MEST_TYPE GetById(long id, HisImpMestTypeSO search)
        {
            HIS_IMP_MEST_TYPE result = null;
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
