using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisBloodGiver
{
    public partial class HisBloodGiverDAO : EntityBase
    {
        private HisBloodGiverCreate CreateWorker
        {
            get
            {
                return (HisBloodGiverCreate)Worker.Get<HisBloodGiverCreate>();
            }
        }
        private HisBloodGiverUpdate UpdateWorker
        {
            get
            {
                return (HisBloodGiverUpdate)Worker.Get<HisBloodGiverUpdate>();
            }
        }
        private HisBloodGiverDelete DeleteWorker
        {
            get
            {
                return (HisBloodGiverDelete)Worker.Get<HisBloodGiverDelete>();
            }
        }
        private HisBloodGiverTruncate TruncateWorker
        {
            get
            {
                return (HisBloodGiverTruncate)Worker.Get<HisBloodGiverTruncate>();
            }
        }
        private HisBloodGiverGet GetWorker
        {
            get
            {
                return (HisBloodGiverGet)Worker.Get<HisBloodGiverGet>();
            }
        }
        private HisBloodGiverCheck CheckWorker
        {
            get
            {
                return (HisBloodGiverCheck)Worker.Get<HisBloodGiverCheck>();
            }
        }

        public bool Create(HIS_BLOOD_GIVER data)
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

        public bool CreateList(List<HIS_BLOOD_GIVER> listData)
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

        public bool Update(HIS_BLOOD_GIVER data)
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

        public bool UpdateList(List<HIS_BLOOD_GIVER> listData)
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

        public bool Delete(HIS_BLOOD_GIVER data)
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

        public bool DeleteList(List<HIS_BLOOD_GIVER> listData)
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

        public bool Truncate(HIS_BLOOD_GIVER data)
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

        public bool TruncateList(List<HIS_BLOOD_GIVER> listData)
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

        public List<HIS_BLOOD_GIVER> Get(HisBloodGiverSO search, CommonParam param)
        {
            List<HIS_BLOOD_GIVER> result = new List<HIS_BLOOD_GIVER>();
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

        public HIS_BLOOD_GIVER GetById(long id, HisBloodGiverSO search)
        {
            HIS_BLOOD_GIVER result = null;
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
