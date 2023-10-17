using Inventec.Common.Repository;
using Inventec.Core;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisAccidentHelmet
{
    public partial class HisAccidentHelmetDAO : EntityBase
    {
        private HisAccidentHelmetCreate CreateWorker
        {
            get
            {
                return (HisAccidentHelmetCreate)Worker.Get<HisAccidentHelmetCreate>();
            }
        }
        private HisAccidentHelmetUpdate UpdateWorker
        {
            get
            {
                return (HisAccidentHelmetUpdate)Worker.Get<HisAccidentHelmetUpdate>();
            }
        }
        private HisAccidentHelmetDelete DeleteWorker
        {
            get
            {
                return (HisAccidentHelmetDelete)Worker.Get<HisAccidentHelmetDelete>();
            }
        }
        private HisAccidentHelmetTruncate TruncateWorker
        {
            get
            {
                return (HisAccidentHelmetTruncate)Worker.Get<HisAccidentHelmetTruncate>();
            }
        }
        private HisAccidentHelmetGet GetWorker
        {
            get
            {
                return (HisAccidentHelmetGet)Worker.Get<HisAccidentHelmetGet>();
            }
        }
        private HisAccidentHelmetCheck CheckWorker
        {
            get
            {
                return (HisAccidentHelmetCheck)Worker.Get<HisAccidentHelmetCheck>();
            }
        }

        public bool Create(HIS_ACCIDENT_HELMET data)
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

        public bool CreateList(List<HIS_ACCIDENT_HELMET> listData)
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

        public bool Update(HIS_ACCIDENT_HELMET data)
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

        public bool UpdateList(List<HIS_ACCIDENT_HELMET> listData)
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

        public bool Delete(HIS_ACCIDENT_HELMET data)
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

        public bool DeleteList(List<HIS_ACCIDENT_HELMET> listData)
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

        public bool Truncate(HIS_ACCIDENT_HELMET data)
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

        public bool TruncateList(List<HIS_ACCIDENT_HELMET> listData)
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

        public List<HIS_ACCIDENT_HELMET> Get(HisAccidentHelmetSO search, CommonParam param)
        {
            List<HIS_ACCIDENT_HELMET> result = new List<HIS_ACCIDENT_HELMET>();
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

        public HIS_ACCIDENT_HELMET GetById(long id, HisAccidentHelmetSO search)
        {
            HIS_ACCIDENT_HELMET result = null;
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
