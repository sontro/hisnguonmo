using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisSesePtttMethod
{
    public partial class HisSesePtttMethodDAO : EntityBase
    {
        private HisSesePtttMethodCreate CreateWorker
        {
            get
            {
                return (HisSesePtttMethodCreate)Worker.Get<HisSesePtttMethodCreate>();
            }
        }
        private HisSesePtttMethodUpdate UpdateWorker
        {
            get
            {
                return (HisSesePtttMethodUpdate)Worker.Get<HisSesePtttMethodUpdate>();
            }
        }
        private HisSesePtttMethodDelete DeleteWorker
        {
            get
            {
                return (HisSesePtttMethodDelete)Worker.Get<HisSesePtttMethodDelete>();
            }
        }
        private HisSesePtttMethodTruncate TruncateWorker
        {
            get
            {
                return (HisSesePtttMethodTruncate)Worker.Get<HisSesePtttMethodTruncate>();
            }
        }
        private HisSesePtttMethodGet GetWorker
        {
            get
            {
                return (HisSesePtttMethodGet)Worker.Get<HisSesePtttMethodGet>();
            }
        }
        private HisSesePtttMethodCheck CheckWorker
        {
            get
            {
                return (HisSesePtttMethodCheck)Worker.Get<HisSesePtttMethodCheck>();
            }
        }

        public bool Create(HIS_SESE_PTTT_METHOD data)
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

        public bool CreateList(List<HIS_SESE_PTTT_METHOD> listData)
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

        public bool Update(HIS_SESE_PTTT_METHOD data)
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

        public bool UpdateList(List<HIS_SESE_PTTT_METHOD> listData)
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

        public bool Delete(HIS_SESE_PTTT_METHOD data)
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

        public bool DeleteList(List<HIS_SESE_PTTT_METHOD> listData)
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

        public bool Truncate(HIS_SESE_PTTT_METHOD data)
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

        public bool TruncateList(List<HIS_SESE_PTTT_METHOD> listData)
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

        public List<HIS_SESE_PTTT_METHOD> Get(HisSesePtttMethodSO search, CommonParam param)
        {
            List<HIS_SESE_PTTT_METHOD> result = new List<HIS_SESE_PTTT_METHOD>();
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

        public HIS_SESE_PTTT_METHOD GetById(long id, HisSesePtttMethodSO search)
        {
            HIS_SESE_PTTT_METHOD result = null;
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
