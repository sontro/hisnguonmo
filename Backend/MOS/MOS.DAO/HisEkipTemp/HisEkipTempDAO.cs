using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisEkipTemp
{
    public partial class HisEkipTempDAO : EntityBase
    {
        private HisEkipTempCreate CreateWorker
        {
            get
            {
                return (HisEkipTempCreate)Worker.Get<HisEkipTempCreate>();
            }
        }
        private HisEkipTempUpdate UpdateWorker
        {
            get
            {
                return (HisEkipTempUpdate)Worker.Get<HisEkipTempUpdate>();
            }
        }
        private HisEkipTempDelete DeleteWorker
        {
            get
            {
                return (HisEkipTempDelete)Worker.Get<HisEkipTempDelete>();
            }
        }
        private HisEkipTempTruncate TruncateWorker
        {
            get
            {
                return (HisEkipTempTruncate)Worker.Get<HisEkipTempTruncate>();
            }
        }
        private HisEkipTempGet GetWorker
        {
            get
            {
                return (HisEkipTempGet)Worker.Get<HisEkipTempGet>();
            }
        }
        private HisEkipTempCheck CheckWorker
        {
            get
            {
                return (HisEkipTempCheck)Worker.Get<HisEkipTempCheck>();
            }
        }

        public bool Create(HIS_EKIP_TEMP data)
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

        public bool CreateList(List<HIS_EKIP_TEMP> listData)
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

        public bool Update(HIS_EKIP_TEMP data)
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

        public bool UpdateList(List<HIS_EKIP_TEMP> listData)
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

        public bool Delete(HIS_EKIP_TEMP data)
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

        public bool DeleteList(List<HIS_EKIP_TEMP> listData)
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

        public bool Truncate(HIS_EKIP_TEMP data)
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

        public bool TruncateList(List<HIS_EKIP_TEMP> listData)
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

        public List<HIS_EKIP_TEMP> Get(HisEkipTempSO search, CommonParam param)
        {
            List<HIS_EKIP_TEMP> result = new List<HIS_EKIP_TEMP>();
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

        public HIS_EKIP_TEMP GetById(long id, HisEkipTempSO search)
        {
            HIS_EKIP_TEMP result = null;
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
