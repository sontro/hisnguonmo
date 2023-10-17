using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisSereServPtttTemp
{
    public partial class HisSereServPtttTempDAO : EntityBase
    {
        private HisSereServPtttTempCreate CreateWorker
        {
            get
            {
                return (HisSereServPtttTempCreate)Worker.Get<HisSereServPtttTempCreate>();
            }
        }
        private HisSereServPtttTempUpdate UpdateWorker
        {
            get
            {
                return (HisSereServPtttTempUpdate)Worker.Get<HisSereServPtttTempUpdate>();
            }
        }
        private HisSereServPtttTempDelete DeleteWorker
        {
            get
            {
                return (HisSereServPtttTempDelete)Worker.Get<HisSereServPtttTempDelete>();
            }
        }
        private HisSereServPtttTempTruncate TruncateWorker
        {
            get
            {
                return (HisSereServPtttTempTruncate)Worker.Get<HisSereServPtttTempTruncate>();
            }
        }
        private HisSereServPtttTempGet GetWorker
        {
            get
            {
                return (HisSereServPtttTempGet)Worker.Get<HisSereServPtttTempGet>();
            }
        }
        private HisSereServPtttTempCheck CheckWorker
        {
            get
            {
                return (HisSereServPtttTempCheck)Worker.Get<HisSereServPtttTempCheck>();
            }
        }

        public bool Create(HIS_SERE_SERV_PTTT_TEMP data)
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

        public bool CreateList(List<HIS_SERE_SERV_PTTT_TEMP> listData)
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

        public bool Update(HIS_SERE_SERV_PTTT_TEMP data)
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

        public bool UpdateList(List<HIS_SERE_SERV_PTTT_TEMP> listData)
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

        public bool Delete(HIS_SERE_SERV_PTTT_TEMP data)
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

        public bool DeleteList(List<HIS_SERE_SERV_PTTT_TEMP> listData)
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

        public bool Truncate(HIS_SERE_SERV_PTTT_TEMP data)
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

        public bool TruncateList(List<HIS_SERE_SERV_PTTT_TEMP> listData)
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

        public List<HIS_SERE_SERV_PTTT_TEMP> Get(HisSereServPtttTempSO search, CommonParam param)
        {
            List<HIS_SERE_SERV_PTTT_TEMP> result = new List<HIS_SERE_SERV_PTTT_TEMP>();
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

        public HIS_SERE_SERV_PTTT_TEMP GetById(long id, HisSereServPtttTempSO search)
        {
            HIS_SERE_SERV_PTTT_TEMP result = null;
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
