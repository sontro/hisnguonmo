using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMediContractMaty
{
    public partial class HisMediContractMatyDAO : EntityBase
    {
        private HisMediContractMatyCreate CreateWorker
        {
            get
            {
                return (HisMediContractMatyCreate)Worker.Get<HisMediContractMatyCreate>();
            }
        }
        private HisMediContractMatyUpdate UpdateWorker
        {
            get
            {
                return (HisMediContractMatyUpdate)Worker.Get<HisMediContractMatyUpdate>();
            }
        }
        private HisMediContractMatyDelete DeleteWorker
        {
            get
            {
                return (HisMediContractMatyDelete)Worker.Get<HisMediContractMatyDelete>();
            }
        }
        private HisMediContractMatyTruncate TruncateWorker
        {
            get
            {
                return (HisMediContractMatyTruncate)Worker.Get<HisMediContractMatyTruncate>();
            }
        }
        private HisMediContractMatyGet GetWorker
        {
            get
            {
                return (HisMediContractMatyGet)Worker.Get<HisMediContractMatyGet>();
            }
        }
        private HisMediContractMatyCheck CheckWorker
        {
            get
            {
                return (HisMediContractMatyCheck)Worker.Get<HisMediContractMatyCheck>();
            }
        }

        public bool Create(HIS_MEDI_CONTRACT_MATY data)
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

        public bool CreateList(List<HIS_MEDI_CONTRACT_MATY> listData)
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

        public bool Update(HIS_MEDI_CONTRACT_MATY data)
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

        public bool UpdateList(List<HIS_MEDI_CONTRACT_MATY> listData)
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

        public bool Delete(HIS_MEDI_CONTRACT_MATY data)
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

        public bool DeleteList(List<HIS_MEDI_CONTRACT_MATY> listData)
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

        public bool Truncate(HIS_MEDI_CONTRACT_MATY data)
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

        public bool TruncateList(List<HIS_MEDI_CONTRACT_MATY> listData)
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

        public List<HIS_MEDI_CONTRACT_MATY> Get(HisMediContractMatySO search, CommonParam param)
        {
            List<HIS_MEDI_CONTRACT_MATY> result = new List<HIS_MEDI_CONTRACT_MATY>();
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

        public HIS_MEDI_CONTRACT_MATY GetById(long id, HisMediContractMatySO search)
        {
            HIS_MEDI_CONTRACT_MATY result = null;
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
