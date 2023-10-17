using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisSereServBill
{
    public partial class HisSereServBillDAO : EntityBase
    {
        private HisSereServBillCreate CreateWorker
        {
            get
            {
                return (HisSereServBillCreate)Worker.Get<HisSereServBillCreate>();
            }
        }
        private HisSereServBillUpdate UpdateWorker
        {
            get
            {
                return (HisSereServBillUpdate)Worker.Get<HisSereServBillUpdate>();
            }
        }
        private HisSereServBillDelete DeleteWorker
        {
            get
            {
                return (HisSereServBillDelete)Worker.Get<HisSereServBillDelete>();
            }
        }
        private HisSereServBillTruncate TruncateWorker
        {
            get
            {
                return (HisSereServBillTruncate)Worker.Get<HisSereServBillTruncate>();
            }
        }
        private HisSereServBillGet GetWorker
        {
            get
            {
                return (HisSereServBillGet)Worker.Get<HisSereServBillGet>();
            }
        }
        private HisSereServBillCheck CheckWorker
        {
            get
            {
                return (HisSereServBillCheck)Worker.Get<HisSereServBillCheck>();
            }
        }

        public bool Create(HIS_SERE_SERV_BILL data)
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

        public bool CreateList(List<HIS_SERE_SERV_BILL> listData)
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

        public bool Update(HIS_SERE_SERV_BILL data)
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

        public bool UpdateList(List<HIS_SERE_SERV_BILL> listData)
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

        public bool Delete(HIS_SERE_SERV_BILL data)
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

        public bool DeleteList(List<HIS_SERE_SERV_BILL> listData)
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

        public bool Truncate(HIS_SERE_SERV_BILL data)
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

        public bool TruncateList(List<HIS_SERE_SERV_BILL> listData)
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

        public List<HIS_SERE_SERV_BILL> Get(HisSereServBillSO search, CommonParam param)
        {
            List<HIS_SERE_SERV_BILL> result = new List<HIS_SERE_SERV_BILL>();
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

        public HIS_SERE_SERV_BILL GetById(long id, HisSereServBillSO search)
        {
            HIS_SERE_SERV_BILL result = null;
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
