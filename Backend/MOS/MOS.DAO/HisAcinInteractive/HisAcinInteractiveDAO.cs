using Inventec.Common.Repository;
using Inventec.Core;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisAcinInteractive
{
    public partial class HisAcinInteractiveDAO : EntityBase
    {
        private HisAcinInteractiveCreate CreateWorker
        {
            get
            {
                return (HisAcinInteractiveCreate)Worker.Get<HisAcinInteractiveCreate>();
            }
        }
        private HisAcinInteractiveUpdate UpdateWorker
        {
            get
            {
                return (HisAcinInteractiveUpdate)Worker.Get<HisAcinInteractiveUpdate>();
            }
        }
        private HisAcinInteractiveDelete DeleteWorker
        {
            get
            {
                return (HisAcinInteractiveDelete)Worker.Get<HisAcinInteractiveDelete>();
            }
        }
        private HisAcinInteractiveTruncate TruncateWorker
        {
            get
            {
                return (HisAcinInteractiveTruncate)Worker.Get<HisAcinInteractiveTruncate>();
            }
        }
        private HisAcinInteractiveGet GetWorker
        {
            get
            {
                return (HisAcinInteractiveGet)Worker.Get<HisAcinInteractiveGet>();
            }
        }
        private HisAcinInteractiveCheck CheckWorker
        {
            get
            {
                return (HisAcinInteractiveCheck)Worker.Get<HisAcinInteractiveCheck>();
            }
        }

        public bool Create(HIS_ACIN_INTERACTIVE data)
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

        public bool CreateList(List<HIS_ACIN_INTERACTIVE> listData)
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

        public bool Update(HIS_ACIN_INTERACTIVE data)
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

        public bool UpdateList(List<HIS_ACIN_INTERACTIVE> listData)
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

        public bool Delete(HIS_ACIN_INTERACTIVE data)
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

        public bool DeleteList(List<HIS_ACIN_INTERACTIVE> listData)
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

        public bool Truncate(HIS_ACIN_INTERACTIVE data)
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

        public bool TruncateList(List<HIS_ACIN_INTERACTIVE> listData)
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

        public List<HIS_ACIN_INTERACTIVE> Get(HisAcinInteractiveSO search, CommonParam param)
        {
            List<HIS_ACIN_INTERACTIVE> result = new List<HIS_ACIN_INTERACTIVE>();
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

        public HIS_ACIN_INTERACTIVE GetById(long id, HisAcinInteractiveSO search)
        {
            HIS_ACIN_INTERACTIVE result = null;
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
