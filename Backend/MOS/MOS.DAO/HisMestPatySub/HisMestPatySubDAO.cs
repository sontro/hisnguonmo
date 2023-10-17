using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMestPatySub
{
    public partial class HisMestPatySubDAO : EntityBase
    {
        private HisMestPatySubCreate CreateWorker
        {
            get
            {
                return (HisMestPatySubCreate)Worker.Get<HisMestPatySubCreate>();
            }
        }
        private HisMestPatySubUpdate UpdateWorker
        {
            get
            {
                return (HisMestPatySubUpdate)Worker.Get<HisMestPatySubUpdate>();
            }
        }
        private HisMestPatySubDelete DeleteWorker
        {
            get
            {
                return (HisMestPatySubDelete)Worker.Get<HisMestPatySubDelete>();
            }
        }
        private HisMestPatySubTruncate TruncateWorker
        {
            get
            {
                return (HisMestPatySubTruncate)Worker.Get<HisMestPatySubTruncate>();
            }
        }
        private HisMestPatySubGet GetWorker
        {
            get
            {
                return (HisMestPatySubGet)Worker.Get<HisMestPatySubGet>();
            }
        }
        private HisMestPatySubCheck CheckWorker
        {
            get
            {
                return (HisMestPatySubCheck)Worker.Get<HisMestPatySubCheck>();
            }
        }

        public bool Create(HIS_MEST_PATY_SUB data)
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

        public bool CreateList(List<HIS_MEST_PATY_SUB> listData)
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

        public bool Update(HIS_MEST_PATY_SUB data)
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

        public bool UpdateList(List<HIS_MEST_PATY_SUB> listData)
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

        public bool Delete(HIS_MEST_PATY_SUB data)
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

        public bool DeleteList(List<HIS_MEST_PATY_SUB> listData)
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

        public bool Truncate(HIS_MEST_PATY_SUB data)
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

        public bool TruncateList(List<HIS_MEST_PATY_SUB> listData)
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

        public List<HIS_MEST_PATY_SUB> Get(HisMestPatySubSO search, CommonParam param)
        {
            List<HIS_MEST_PATY_SUB> result = new List<HIS_MEST_PATY_SUB>();
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

        public HIS_MEST_PATY_SUB GetById(long id, HisMestPatySubSO search)
        {
            HIS_MEST_PATY_SUB result = null;
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
