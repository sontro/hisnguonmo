using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisAntigenMety
{
    public partial class HisAntigenMetyDAO : EntityBase
    {
        private HisAntigenMetyCreate CreateWorker
        {
            get
            {
                return (HisAntigenMetyCreate)Worker.Get<HisAntigenMetyCreate>();
            }
        }
        private HisAntigenMetyUpdate UpdateWorker
        {
            get
            {
                return (HisAntigenMetyUpdate)Worker.Get<HisAntigenMetyUpdate>();
            }
        }
        private HisAntigenMetyDelete DeleteWorker
        {
            get
            {
                return (HisAntigenMetyDelete)Worker.Get<HisAntigenMetyDelete>();
            }
        }
        private HisAntigenMetyTruncate TruncateWorker
        {
            get
            {
                return (HisAntigenMetyTruncate)Worker.Get<HisAntigenMetyTruncate>();
            }
        }
        private HisAntigenMetyGet GetWorker
        {
            get
            {
                return (HisAntigenMetyGet)Worker.Get<HisAntigenMetyGet>();
            }
        }
        private HisAntigenMetyCheck CheckWorker
        {
            get
            {
                return (HisAntigenMetyCheck)Worker.Get<HisAntigenMetyCheck>();
            }
        }

        public bool Create(HIS_ANTIGEN_METY data)
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

        public bool CreateList(List<HIS_ANTIGEN_METY> listData)
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

        public bool Update(HIS_ANTIGEN_METY data)
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

        public bool UpdateList(List<HIS_ANTIGEN_METY> listData)
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

        public bool Delete(HIS_ANTIGEN_METY data)
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

        public bool DeleteList(List<HIS_ANTIGEN_METY> listData)
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

        public bool Truncate(HIS_ANTIGEN_METY data)
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

        public bool TruncateList(List<HIS_ANTIGEN_METY> listData)
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

        public List<HIS_ANTIGEN_METY> Get(HisAntigenMetySO search, CommonParam param)
        {
            List<HIS_ANTIGEN_METY> result = new List<HIS_ANTIGEN_METY>();
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

        public HIS_ANTIGEN_METY GetById(long id, HisAntigenMetySO search)
        {
            HIS_ANTIGEN_METY result = null;
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
