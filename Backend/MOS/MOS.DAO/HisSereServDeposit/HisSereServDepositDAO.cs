using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisSereServDeposit
{
    public partial class HisSereServDepositDAO : EntityBase
    {
        private HisSereServDepositCreate CreateWorker
        {
            get
            {
                return (HisSereServDepositCreate)Worker.Get<HisSereServDepositCreate>();
            }
        }
        private HisSereServDepositUpdate UpdateWorker
        {
            get
            {
                return (HisSereServDepositUpdate)Worker.Get<HisSereServDepositUpdate>();
            }
        }
        private HisSereServDepositDelete DeleteWorker
        {
            get
            {
                return (HisSereServDepositDelete)Worker.Get<HisSereServDepositDelete>();
            }
        }
        private HisSereServDepositTruncate TruncateWorker
        {
            get
            {
                return (HisSereServDepositTruncate)Worker.Get<HisSereServDepositTruncate>();
            }
        }
        private HisSereServDepositGet GetWorker
        {
            get
            {
                return (HisSereServDepositGet)Worker.Get<HisSereServDepositGet>();
            }
        }
        private HisSereServDepositCheck CheckWorker
        {
            get
            {
                return (HisSereServDepositCheck)Worker.Get<HisSereServDepositCheck>();
            }
        }

        public bool Create(HIS_SERE_SERV_DEPOSIT data)
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

        public bool CreateList(List<HIS_SERE_SERV_DEPOSIT> listData)
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

        public bool Update(HIS_SERE_SERV_DEPOSIT data)
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

        public bool UpdateList(List<HIS_SERE_SERV_DEPOSIT> listData)
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

        public bool Delete(HIS_SERE_SERV_DEPOSIT data)
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

        public bool DeleteList(List<HIS_SERE_SERV_DEPOSIT> listData)
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

        public bool Truncate(HIS_SERE_SERV_DEPOSIT data)
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

        public bool TruncateList(List<HIS_SERE_SERV_DEPOSIT> listData)
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

        public List<HIS_SERE_SERV_DEPOSIT> Get(HisSereServDepositSO search, CommonParam param)
        {
            List<HIS_SERE_SERV_DEPOSIT> result = new List<HIS_SERE_SERV_DEPOSIT>();
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

        public HIS_SERE_SERV_DEPOSIT GetById(long id, HisSereServDepositSO search)
        {
            HIS_SERE_SERV_DEPOSIT result = null;
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
