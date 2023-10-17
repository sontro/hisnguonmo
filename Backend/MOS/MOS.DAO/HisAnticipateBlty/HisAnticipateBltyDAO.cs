using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisAnticipateBlty
{
    public partial class HisAnticipateBltyDAO : EntityBase
    {
        private HisAnticipateBltyCreate CreateWorker
        {
            get
            {
                return (HisAnticipateBltyCreate)Worker.Get<HisAnticipateBltyCreate>();
            }
        }
        private HisAnticipateBltyUpdate UpdateWorker
        {
            get
            {
                return (HisAnticipateBltyUpdate)Worker.Get<HisAnticipateBltyUpdate>();
            }
        }
        private HisAnticipateBltyDelete DeleteWorker
        {
            get
            {
                return (HisAnticipateBltyDelete)Worker.Get<HisAnticipateBltyDelete>();
            }
        }
        private HisAnticipateBltyTruncate TruncateWorker
        {
            get
            {
                return (HisAnticipateBltyTruncate)Worker.Get<HisAnticipateBltyTruncate>();
            }
        }
        private HisAnticipateBltyGet GetWorker
        {
            get
            {
                return (HisAnticipateBltyGet)Worker.Get<HisAnticipateBltyGet>();
            }
        }
        private HisAnticipateBltyCheck CheckWorker
        {
            get
            {
                return (HisAnticipateBltyCheck)Worker.Get<HisAnticipateBltyCheck>();
            }
        }

        public bool Create(HIS_ANTICIPATE_BLTY data)
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

        public bool CreateList(List<HIS_ANTICIPATE_BLTY> listData)
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

        public bool Update(HIS_ANTICIPATE_BLTY data)
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

        public bool UpdateList(List<HIS_ANTICIPATE_BLTY> listData)
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

        public bool Delete(HIS_ANTICIPATE_BLTY data)
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

        public bool DeleteList(List<HIS_ANTICIPATE_BLTY> listData)
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

        public bool Truncate(HIS_ANTICIPATE_BLTY data)
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

        public bool TruncateList(List<HIS_ANTICIPATE_BLTY> listData)
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

        public List<HIS_ANTICIPATE_BLTY> Get(HisAnticipateBltySO search, CommonParam param)
        {
            List<HIS_ANTICIPATE_BLTY> result = new List<HIS_ANTICIPATE_BLTY>();
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

        public HIS_ANTICIPATE_BLTY GetById(long id, HisAnticipateBltySO search)
        {
            HIS_ANTICIPATE_BLTY result = null;
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
