using Inventec.Common.Repository;
using Inventec.Core;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisAccidentHurtType
{
    public partial class HisAccidentHurtTypeDAO : EntityBase
    {
        private HisAccidentHurtTypeCreate CreateWorker
        {
            get
            {
                return (HisAccidentHurtTypeCreate)Worker.Get<HisAccidentHurtTypeCreate>();
            }
        }
        private HisAccidentHurtTypeUpdate UpdateWorker
        {
            get
            {
                return (HisAccidentHurtTypeUpdate)Worker.Get<HisAccidentHurtTypeUpdate>();
            }
        }
        private HisAccidentHurtTypeDelete DeleteWorker
        {
            get
            {
                return (HisAccidentHurtTypeDelete)Worker.Get<HisAccidentHurtTypeDelete>();
            }
        }
        private HisAccidentHurtTypeTruncate TruncateWorker
        {
            get
            {
                return (HisAccidentHurtTypeTruncate)Worker.Get<HisAccidentHurtTypeTruncate>();
            }
        }
        private HisAccidentHurtTypeGet GetWorker
        {
            get
            {
                return (HisAccidentHurtTypeGet)Worker.Get<HisAccidentHurtTypeGet>();
            }
        }
        private HisAccidentHurtTypeCheck CheckWorker
        {
            get
            {
                return (HisAccidentHurtTypeCheck)Worker.Get<HisAccidentHurtTypeCheck>();
            }
        }

        public bool Create(HIS_ACCIDENT_HURT_TYPE data)
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

        public bool CreateList(List<HIS_ACCIDENT_HURT_TYPE> listData)
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

        public bool Update(HIS_ACCIDENT_HURT_TYPE data)
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

        public bool UpdateList(List<HIS_ACCIDENT_HURT_TYPE> listData)
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

        public bool Delete(HIS_ACCIDENT_HURT_TYPE data)
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

        public bool DeleteList(List<HIS_ACCIDENT_HURT_TYPE> listData)
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

        public bool Truncate(HIS_ACCIDENT_HURT_TYPE data)
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

        public bool TruncateList(List<HIS_ACCIDENT_HURT_TYPE> listData)
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

        public List<HIS_ACCIDENT_HURT_TYPE> Get(HisAccidentHurtTypeSO search, CommonParam param)
        {
            List<HIS_ACCIDENT_HURT_TYPE> result = new List<HIS_ACCIDENT_HURT_TYPE>();
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

        public HIS_ACCIDENT_HURT_TYPE GetById(long id, HisAccidentHurtTypeSO search)
        {
            HIS_ACCIDENT_HURT_TYPE result = null;
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
