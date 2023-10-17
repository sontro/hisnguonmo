using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisVaexVaer
{
    public partial class HisVaexVaerDAO : EntityBase
    {
        private HisVaexVaerCreate CreateWorker
        {
            get
            {
                return (HisVaexVaerCreate)Worker.Get<HisVaexVaerCreate>();
            }
        }
        private HisVaexVaerUpdate UpdateWorker
        {
            get
            {
                return (HisVaexVaerUpdate)Worker.Get<HisVaexVaerUpdate>();
            }
        }
        private HisVaexVaerDelete DeleteWorker
        {
            get
            {
                return (HisVaexVaerDelete)Worker.Get<HisVaexVaerDelete>();
            }
        }
        private HisVaexVaerTruncate TruncateWorker
        {
            get
            {
                return (HisVaexVaerTruncate)Worker.Get<HisVaexVaerTruncate>();
            }
        }
        private HisVaexVaerGet GetWorker
        {
            get
            {
                return (HisVaexVaerGet)Worker.Get<HisVaexVaerGet>();
            }
        }
        private HisVaexVaerCheck CheckWorker
        {
            get
            {
                return (HisVaexVaerCheck)Worker.Get<HisVaexVaerCheck>();
            }
        }

        public bool Create(HIS_VAEX_VAER data)
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

        public bool CreateList(List<HIS_VAEX_VAER> listData)
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

        public bool Update(HIS_VAEX_VAER data)
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

        public bool UpdateList(List<HIS_VAEX_VAER> listData)
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

        public bool Delete(HIS_VAEX_VAER data)
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

        public bool DeleteList(List<HIS_VAEX_VAER> listData)
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

        public bool Truncate(HIS_VAEX_VAER data)
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

        public bool TruncateList(List<HIS_VAEX_VAER> listData)
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

        public List<HIS_VAEX_VAER> Get(HisVaexVaerSO search, CommonParam param)
        {
            List<HIS_VAEX_VAER> result = new List<HIS_VAEX_VAER>();
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

        public HIS_VAEX_VAER GetById(long id, HisVaexVaerSO search)
        {
            HIS_VAEX_VAER result = null;
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
