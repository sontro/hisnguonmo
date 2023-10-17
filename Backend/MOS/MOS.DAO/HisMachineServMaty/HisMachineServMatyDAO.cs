using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMachineServMaty
{
    public partial class HisMachineServMatyDAO : EntityBase
    {
        private HisMachineServMatyCreate CreateWorker
        {
            get
            {
                return (HisMachineServMatyCreate)Worker.Get<HisMachineServMatyCreate>();
            }
        }
        private HisMachineServMatyUpdate UpdateWorker
        {
            get
            {
                return (HisMachineServMatyUpdate)Worker.Get<HisMachineServMatyUpdate>();
            }
        }
        private HisMachineServMatyDelete DeleteWorker
        {
            get
            {
                return (HisMachineServMatyDelete)Worker.Get<HisMachineServMatyDelete>();
            }
        }
        private HisMachineServMatyTruncate TruncateWorker
        {
            get
            {
                return (HisMachineServMatyTruncate)Worker.Get<HisMachineServMatyTruncate>();
            }
        }
        private HisMachineServMatyGet GetWorker
        {
            get
            {
                return (HisMachineServMatyGet)Worker.Get<HisMachineServMatyGet>();
            }
        }
        private HisMachineServMatyCheck CheckWorker
        {
            get
            {
                return (HisMachineServMatyCheck)Worker.Get<HisMachineServMatyCheck>();
            }
        }

        public bool Create(HIS_MACHINE_SERV_MATY data)
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

        public bool CreateList(List<HIS_MACHINE_SERV_MATY> listData)
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

        public bool Update(HIS_MACHINE_SERV_MATY data)
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

        public bool UpdateList(List<HIS_MACHINE_SERV_MATY> listData)
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

        public bool Delete(HIS_MACHINE_SERV_MATY data)
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

        public bool DeleteList(List<HIS_MACHINE_SERV_MATY> listData)
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

        public bool Truncate(HIS_MACHINE_SERV_MATY data)
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

        public bool TruncateList(List<HIS_MACHINE_SERV_MATY> listData)
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

        public List<HIS_MACHINE_SERV_MATY> Get(HisMachineServMatySO search, CommonParam param)
        {
            List<HIS_MACHINE_SERV_MATY> result = new List<HIS_MACHINE_SERV_MATY>();
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

        public HIS_MACHINE_SERV_MATY GetById(long id, HisMachineServMatySO search)
        {
            HIS_MACHINE_SERV_MATY result = null;
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
