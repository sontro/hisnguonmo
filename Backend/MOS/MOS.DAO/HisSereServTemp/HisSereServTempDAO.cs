using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisSereServTemp
{
    public partial class HisSereServTempDAO : EntityBase
    {
        private HisSereServTempCreate CreateWorker
        {
            get
            {
                return (HisSereServTempCreate)Worker.Get<HisSereServTempCreate>();
            }
        }
        private HisSereServTempUpdate UpdateWorker
        {
            get
            {
                return (HisSereServTempUpdate)Worker.Get<HisSereServTempUpdate>();
            }
        }
        private HisSereServTempDelete DeleteWorker
        {
            get
            {
                return (HisSereServTempDelete)Worker.Get<HisSereServTempDelete>();
            }
        }
        private HisSereServTempTruncate TruncateWorker
        {
            get
            {
                return (HisSereServTempTruncate)Worker.Get<HisSereServTempTruncate>();
            }
        }
        private HisSereServTempGet GetWorker
        {
            get
            {
                return (HisSereServTempGet)Worker.Get<HisSereServTempGet>();
            }
        }
        private HisSereServTempCheck CheckWorker
        {
            get
            {
                return (HisSereServTempCheck)Worker.Get<HisSereServTempCheck>();
            }
        }

        public bool Create(HIS_SERE_SERV_TEMP data)
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

        public bool CreateList(List<HIS_SERE_SERV_TEMP> listData)
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

        public bool Update(HIS_SERE_SERV_TEMP data)
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

        public bool UpdateList(List<HIS_SERE_SERV_TEMP> listData)
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

        public bool Delete(HIS_SERE_SERV_TEMP data)
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

        public bool DeleteList(List<HIS_SERE_SERV_TEMP> listData)
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

        public bool Truncate(HIS_SERE_SERV_TEMP data)
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

        public bool TruncateList(List<HIS_SERE_SERV_TEMP> listData)
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

        public List<HIS_SERE_SERV_TEMP> Get(HisSereServTempSO search, CommonParam param)
        {
            List<HIS_SERE_SERV_TEMP> result = new List<HIS_SERE_SERV_TEMP>();
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

        public HIS_SERE_SERV_TEMP GetById(long id, HisSereServTempSO search)
        {
            HIS_SERE_SERV_TEMP result = null;
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
