using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisTranPatiTemp
{
    public partial class HisTranPatiTempDAO : EntityBase
    {
        private HisTranPatiTempCreate CreateWorker
        {
            get
            {
                return (HisTranPatiTempCreate)Worker.Get<HisTranPatiTempCreate>();
            }
        }
        private HisTranPatiTempUpdate UpdateWorker
        {
            get
            {
                return (HisTranPatiTempUpdate)Worker.Get<HisTranPatiTempUpdate>();
            }
        }
        private HisTranPatiTempDelete DeleteWorker
        {
            get
            {
                return (HisTranPatiTempDelete)Worker.Get<HisTranPatiTempDelete>();
            }
        }
        private HisTranPatiTempTruncate TruncateWorker
        {
            get
            {
                return (HisTranPatiTempTruncate)Worker.Get<HisTranPatiTempTruncate>();
            }
        }
        private HisTranPatiTempGet GetWorker
        {
            get
            {
                return (HisTranPatiTempGet)Worker.Get<HisTranPatiTempGet>();
            }
        }
        private HisTranPatiTempCheck CheckWorker
        {
            get
            {
                return (HisTranPatiTempCheck)Worker.Get<HisTranPatiTempCheck>();
            }
        }

        public bool Create(HIS_TRAN_PATI_TEMP data)
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

        public bool CreateList(List<HIS_TRAN_PATI_TEMP> listData)
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

        public bool Update(HIS_TRAN_PATI_TEMP data)
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

        public bool UpdateList(List<HIS_TRAN_PATI_TEMP> listData)
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

        public bool Delete(HIS_TRAN_PATI_TEMP data)
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

        public bool DeleteList(List<HIS_TRAN_PATI_TEMP> listData)
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

        public bool Truncate(HIS_TRAN_PATI_TEMP data)
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

        public bool TruncateList(List<HIS_TRAN_PATI_TEMP> listData)
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

        public List<HIS_TRAN_PATI_TEMP> Get(HisTranPatiTempSO search, CommonParam param)
        {
            List<HIS_TRAN_PATI_TEMP> result = new List<HIS_TRAN_PATI_TEMP>();
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

        public HIS_TRAN_PATI_TEMP GetById(long id, HisTranPatiTempSO search)
        {
            HIS_TRAN_PATI_TEMP result = null;
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
