using Inventec.Common.Repository;
using Inventec.Core;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisAnticipateMaty
{
    public partial class HisAnticipateMatyDAO : EntityBase
    {
        private HisAnticipateMatyCreate CreateWorker
        {
            get
            {
                return (HisAnticipateMatyCreate)Worker.Get<HisAnticipateMatyCreate>();
            }
        }
        private HisAnticipateMatyUpdate UpdateWorker
        {
            get
            {
                return (HisAnticipateMatyUpdate)Worker.Get<HisAnticipateMatyUpdate>();
            }
        }
        private HisAnticipateMatyDelete DeleteWorker
        {
            get
            {
                return (HisAnticipateMatyDelete)Worker.Get<HisAnticipateMatyDelete>();
            }
        }
        private HisAnticipateMatyTruncate TruncateWorker
        {
            get
            {
                return (HisAnticipateMatyTruncate)Worker.Get<HisAnticipateMatyTruncate>();
            }
        }
        private HisAnticipateMatyGet GetWorker
        {
            get
            {
                return (HisAnticipateMatyGet)Worker.Get<HisAnticipateMatyGet>();
            }
        }
        private HisAnticipateMatyCheck CheckWorker
        {
            get
            {
                return (HisAnticipateMatyCheck)Worker.Get<HisAnticipateMatyCheck>();
            }
        }

        public bool Create(HIS_ANTICIPATE_MATY data)
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

        public bool CreateList(List<HIS_ANTICIPATE_MATY> listData)
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

        public bool Update(HIS_ANTICIPATE_MATY data)
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

        public bool UpdateList(List<HIS_ANTICIPATE_MATY> listData)
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

        public bool Delete(HIS_ANTICIPATE_MATY data)
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

        public bool DeleteList(List<HIS_ANTICIPATE_MATY> listData)
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

        public bool Truncate(HIS_ANTICIPATE_MATY data)
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

        public bool TruncateList(List<HIS_ANTICIPATE_MATY> listData)
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

        public List<HIS_ANTICIPATE_MATY> Get(HisAnticipateMatySO search, CommonParam param)
        {
            List<HIS_ANTICIPATE_MATY> result = new List<HIS_ANTICIPATE_MATY>();
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

        public HIS_ANTICIPATE_MATY GetById(long id, HisAnticipateMatySO search)
        {
            HIS_ANTICIPATE_MATY result = null;
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
