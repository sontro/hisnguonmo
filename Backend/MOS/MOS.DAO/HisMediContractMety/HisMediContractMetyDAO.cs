using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMediContractMety
{
    public partial class HisMediContractMetyDAO : EntityBase
    {
        private HisMediContractMetyCreate CreateWorker
        {
            get
            {
                return (HisMediContractMetyCreate)Worker.Get<HisMediContractMetyCreate>();
            }
        }
        private HisMediContractMetyUpdate UpdateWorker
        {
            get
            {
                return (HisMediContractMetyUpdate)Worker.Get<HisMediContractMetyUpdate>();
            }
        }
        private HisMediContractMetyDelete DeleteWorker
        {
            get
            {
                return (HisMediContractMetyDelete)Worker.Get<HisMediContractMetyDelete>();
            }
        }
        private HisMediContractMetyTruncate TruncateWorker
        {
            get
            {
                return (HisMediContractMetyTruncate)Worker.Get<HisMediContractMetyTruncate>();
            }
        }
        private HisMediContractMetyGet GetWorker
        {
            get
            {
                return (HisMediContractMetyGet)Worker.Get<HisMediContractMetyGet>();
            }
        }
        private HisMediContractMetyCheck CheckWorker
        {
            get
            {
                return (HisMediContractMetyCheck)Worker.Get<HisMediContractMetyCheck>();
            }
        }

        public bool Create(HIS_MEDI_CONTRACT_METY data)
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

        public bool CreateList(List<HIS_MEDI_CONTRACT_METY> listData)
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

        public bool Update(HIS_MEDI_CONTRACT_METY data)
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

        public bool UpdateList(List<HIS_MEDI_CONTRACT_METY> listData)
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

        public bool Delete(HIS_MEDI_CONTRACT_METY data)
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

        public bool DeleteList(List<HIS_MEDI_CONTRACT_METY> listData)
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

        public bool Truncate(HIS_MEDI_CONTRACT_METY data)
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

        public bool TruncateList(List<HIS_MEDI_CONTRACT_METY> listData)
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

        public List<HIS_MEDI_CONTRACT_METY> Get(HisMediContractMetySO search, CommonParam param)
        {
            List<HIS_MEDI_CONTRACT_METY> result = new List<HIS_MEDI_CONTRACT_METY>();
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

        public HIS_MEDI_CONTRACT_METY GetById(long id, HisMediContractMetySO search)
        {
            HIS_MEDI_CONTRACT_METY result = null;
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
