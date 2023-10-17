using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisSereServRation
{
    public partial class HisSereServRationDAO : EntityBase
    {
        private HisSereServRationCreate CreateWorker
        {
            get
            {
                return (HisSereServRationCreate)Worker.Get<HisSereServRationCreate>();
            }
        }
        private HisSereServRationUpdate UpdateWorker
        {
            get
            {
                return (HisSereServRationUpdate)Worker.Get<HisSereServRationUpdate>();
            }
        }
        private HisSereServRationDelete DeleteWorker
        {
            get
            {
                return (HisSereServRationDelete)Worker.Get<HisSereServRationDelete>();
            }
        }
        private HisSereServRationTruncate TruncateWorker
        {
            get
            {
                return (HisSereServRationTruncate)Worker.Get<HisSereServRationTruncate>();
            }
        }
        private HisSereServRationGet GetWorker
        {
            get
            {
                return (HisSereServRationGet)Worker.Get<HisSereServRationGet>();
            }
        }
        private HisSereServRationCheck CheckWorker
        {
            get
            {
                return (HisSereServRationCheck)Worker.Get<HisSereServRationCheck>();
            }
        }

        public bool Create(HIS_SERE_SERV_RATION data)
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

        public bool CreateList(List<HIS_SERE_SERV_RATION> listData)
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

        public bool Update(HIS_SERE_SERV_RATION data)
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

        public bool UpdateList(List<HIS_SERE_SERV_RATION> listData)
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

        public bool Delete(HIS_SERE_SERV_RATION data)
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

        public bool DeleteList(List<HIS_SERE_SERV_RATION> listData)
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

        public bool Truncate(HIS_SERE_SERV_RATION data)
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

        public bool TruncateList(List<HIS_SERE_SERV_RATION> listData)
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

        public List<HIS_SERE_SERV_RATION> Get(HisSereServRationSO search, CommonParam param)
        {
            List<HIS_SERE_SERV_RATION> result = new List<HIS_SERE_SERV_RATION>();
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

        public HIS_SERE_SERV_RATION GetById(long id, HisSereServRationSO search)
        {
            HIS_SERE_SERV_RATION result = null;
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
