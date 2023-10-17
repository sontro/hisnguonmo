using Inventec.Common.Repository;
using Inventec.Core;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisAnticipateMety
{
    public partial class HisAnticipateMetyDAO : EntityBase
    {
        private HisAnticipateMetyCreate CreateWorker
        {
            get
            {
                return (HisAnticipateMetyCreate)Worker.Get<HisAnticipateMetyCreate>();
            }
        }
        private HisAnticipateMetyUpdate UpdateWorker
        {
            get
            {
                return (HisAnticipateMetyUpdate)Worker.Get<HisAnticipateMetyUpdate>();
            }
        }
        private HisAnticipateMetyDelete DeleteWorker
        {
            get
            {
                return (HisAnticipateMetyDelete)Worker.Get<HisAnticipateMetyDelete>();
            }
        }
        private HisAnticipateMetyTruncate TruncateWorker
        {
            get
            {
                return (HisAnticipateMetyTruncate)Worker.Get<HisAnticipateMetyTruncate>();
            }
        }
        private HisAnticipateMetyGet GetWorker
        {
            get
            {
                return (HisAnticipateMetyGet)Worker.Get<HisAnticipateMetyGet>();
            }
        }
        private HisAnticipateMetyCheck CheckWorker
        {
            get
            {
                return (HisAnticipateMetyCheck)Worker.Get<HisAnticipateMetyCheck>();
            }
        }

        public bool Create(HIS_ANTICIPATE_METY data)
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

        public bool CreateList(List<HIS_ANTICIPATE_METY> listData)
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

        public bool Update(HIS_ANTICIPATE_METY data)
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

        public bool UpdateList(List<HIS_ANTICIPATE_METY> listData)
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

        public bool Delete(HIS_ANTICIPATE_METY data)
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

        public bool DeleteList(List<HIS_ANTICIPATE_METY> listData)
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

        public bool Truncate(HIS_ANTICIPATE_METY data)
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

        public bool TruncateList(List<HIS_ANTICIPATE_METY> listData)
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

        public List<HIS_ANTICIPATE_METY> Get(HisAnticipateMetySO search, CommonParam param)
        {
            List<HIS_ANTICIPATE_METY> result = new List<HIS_ANTICIPATE_METY>();
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

        public HIS_ANTICIPATE_METY GetById(long id, HisAnticipateMetySO search)
        {
            HIS_ANTICIPATE_METY result = null;
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
