using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisRemuneration
{
    public partial class HisRemunerationDAO : EntityBase
    {
        private HisRemunerationCreate CreateWorker
        {
            get
            {
                return (HisRemunerationCreate)Worker.Get<HisRemunerationCreate>();
            }
        }
        private HisRemunerationUpdate UpdateWorker
        {
            get
            {
                return (HisRemunerationUpdate)Worker.Get<HisRemunerationUpdate>();
            }
        }
        private HisRemunerationDelete DeleteWorker
        {
            get
            {
                return (HisRemunerationDelete)Worker.Get<HisRemunerationDelete>();
            }
        }
        private HisRemunerationTruncate TruncateWorker
        {
            get
            {
                return (HisRemunerationTruncate)Worker.Get<HisRemunerationTruncate>();
            }
        }
        private HisRemunerationGet GetWorker
        {
            get
            {
                return (HisRemunerationGet)Worker.Get<HisRemunerationGet>();
            }
        }
        private HisRemunerationCheck CheckWorker
        {
            get
            {
                return (HisRemunerationCheck)Worker.Get<HisRemunerationCheck>();
            }
        }

        public bool Create(HIS_REMUNERATION data)
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

        public bool CreateList(List<HIS_REMUNERATION> listData)
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

        public bool Update(HIS_REMUNERATION data)
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

        public bool UpdateList(List<HIS_REMUNERATION> listData)
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

        public bool Delete(HIS_REMUNERATION data)
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

        public bool DeleteList(List<HIS_REMUNERATION> listData)
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

        public bool Truncate(HIS_REMUNERATION data)
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

        public bool TruncateList(List<HIS_REMUNERATION> listData)
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

        public List<HIS_REMUNERATION> Get(HisRemunerationSO search, CommonParam param)
        {
            List<HIS_REMUNERATION> result = new List<HIS_REMUNERATION>();
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

        public HIS_REMUNERATION GetById(long id, HisRemunerationSO search)
        {
            HIS_REMUNERATION result = null;
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
