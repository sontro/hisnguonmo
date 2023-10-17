using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisSereServPttt
{
    public partial class HisSereServPtttDAO : EntityBase
    {
        private HisSereServPtttCreate CreateWorker
        {
            get
            {
                return (HisSereServPtttCreate)Worker.Get<HisSereServPtttCreate>();
            }
        }
        private HisSereServPtttUpdate UpdateWorker
        {
            get
            {
                return (HisSereServPtttUpdate)Worker.Get<HisSereServPtttUpdate>();
            }
        }
        private HisSereServPtttDelete DeleteWorker
        {
            get
            {
                return (HisSereServPtttDelete)Worker.Get<HisSereServPtttDelete>();
            }
        }
        private HisSereServPtttTruncate TruncateWorker
        {
            get
            {
                return (HisSereServPtttTruncate)Worker.Get<HisSereServPtttTruncate>();
            }
        }
        private HisSereServPtttGet GetWorker
        {
            get
            {
                return (HisSereServPtttGet)Worker.Get<HisSereServPtttGet>();
            }
        }
        private HisSereServPtttCheck CheckWorker
        {
            get
            {
                return (HisSereServPtttCheck)Worker.Get<HisSereServPtttCheck>();
            }
        }

        public bool Create(HIS_SERE_SERV_PTTT data)
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

        public bool CreateList(List<HIS_SERE_SERV_PTTT> listData)
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

        public bool Update(HIS_SERE_SERV_PTTT data)
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

        public bool UpdateList(List<HIS_SERE_SERV_PTTT> listData)
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

        public bool Delete(HIS_SERE_SERV_PTTT data)
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

        public bool DeleteList(List<HIS_SERE_SERV_PTTT> listData)
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

        public bool Truncate(HIS_SERE_SERV_PTTT data)
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

        public bool TruncateList(List<HIS_SERE_SERV_PTTT> listData)
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

        public List<HIS_SERE_SERV_PTTT> Get(HisSereServPtttSO search, CommonParam param)
        {
            List<HIS_SERE_SERV_PTTT> result = new List<HIS_SERE_SERV_PTTT>();
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

        public HIS_SERE_SERV_PTTT GetById(long id, HisSereServPtttSO search)
        {
            HIS_SERE_SERV_PTTT result = null;
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
