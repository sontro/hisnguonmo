using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMetyMaty
{
    public partial class HisMetyMatyDAO : EntityBase
    {
        private HisMetyMatyCreate CreateWorker
        {
            get
            {
                return (HisMetyMatyCreate)Worker.Get<HisMetyMatyCreate>();
            }
        }
        private HisMetyMatyUpdate UpdateWorker
        {
            get
            {
                return (HisMetyMatyUpdate)Worker.Get<HisMetyMatyUpdate>();
            }
        }
        private HisMetyMatyDelete DeleteWorker
        {
            get
            {
                return (HisMetyMatyDelete)Worker.Get<HisMetyMatyDelete>();
            }
        }
        private HisMetyMatyTruncate TruncateWorker
        {
            get
            {
                return (HisMetyMatyTruncate)Worker.Get<HisMetyMatyTruncate>();
            }
        }
        private HisMetyMatyGet GetWorker
        {
            get
            {
                return (HisMetyMatyGet)Worker.Get<HisMetyMatyGet>();
            }
        }
        private HisMetyMatyCheck CheckWorker
        {
            get
            {
                return (HisMetyMatyCheck)Worker.Get<HisMetyMatyCheck>();
            }
        }

        public bool Create(HIS_METY_MATY data)
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

        public bool CreateList(List<HIS_METY_MATY> listData)
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

        public bool Update(HIS_METY_MATY data)
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

        public bool UpdateList(List<HIS_METY_MATY> listData)
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

        public bool Delete(HIS_METY_MATY data)
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

        public bool DeleteList(List<HIS_METY_MATY> listData)
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

        public bool Truncate(HIS_METY_MATY data)
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

        public bool TruncateList(List<HIS_METY_MATY> listData)
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

        public List<HIS_METY_MATY> Get(HisMetyMatySO search, CommonParam param)
        {
            List<HIS_METY_MATY> result = new List<HIS_METY_MATY>();
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

        public HIS_METY_MATY GetById(long id, HisMetyMatySO search)
        {
            HIS_METY_MATY result = null;
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
