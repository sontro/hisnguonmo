using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisSereServDebt
{
    public partial class HisSereServDebtDAO : EntityBase
    {
        private HisSereServDebtCreate CreateWorker
        {
            get
            {
                return (HisSereServDebtCreate)Worker.Get<HisSereServDebtCreate>();
            }
        }
        private HisSereServDebtUpdate UpdateWorker
        {
            get
            {
                return (HisSereServDebtUpdate)Worker.Get<HisSereServDebtUpdate>();
            }
        }
        private HisSereServDebtDelete DeleteWorker
        {
            get
            {
                return (HisSereServDebtDelete)Worker.Get<HisSereServDebtDelete>();
            }
        }
        private HisSereServDebtTruncate TruncateWorker
        {
            get
            {
                return (HisSereServDebtTruncate)Worker.Get<HisSereServDebtTruncate>();
            }
        }
        private HisSereServDebtGet GetWorker
        {
            get
            {
                return (HisSereServDebtGet)Worker.Get<HisSereServDebtGet>();
            }
        }
        private HisSereServDebtCheck CheckWorker
        {
            get
            {
                return (HisSereServDebtCheck)Worker.Get<HisSereServDebtCheck>();
            }
        }

        public bool Create(HIS_SERE_SERV_DEBT data)
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

        public bool CreateList(List<HIS_SERE_SERV_DEBT> listData)
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

        public bool Update(HIS_SERE_SERV_DEBT data)
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

        public bool UpdateList(List<HIS_SERE_SERV_DEBT> listData)
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

        public bool Delete(HIS_SERE_SERV_DEBT data)
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

        public bool DeleteList(List<HIS_SERE_SERV_DEBT> listData)
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

        public bool Truncate(HIS_SERE_SERV_DEBT data)
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

        public bool TruncateList(List<HIS_SERE_SERV_DEBT> listData)
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

        public List<HIS_SERE_SERV_DEBT> Get(HisSereServDebtSO search, CommonParam param)
        {
            List<HIS_SERE_SERV_DEBT> result = new List<HIS_SERE_SERV_DEBT>();
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

        public HIS_SERE_SERV_DEBT GetById(long id, HisSereServDebtSO search)
        {
            HIS_SERE_SERV_DEBT result = null;
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
