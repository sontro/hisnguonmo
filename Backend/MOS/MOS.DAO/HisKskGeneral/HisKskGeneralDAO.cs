using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisKskGeneral
{
    public partial class HisKskGeneralDAO : EntityBase
    {
        private HisKskGeneralCreate CreateWorker
        {
            get
            {
                return (HisKskGeneralCreate)Worker.Get<HisKskGeneralCreate>();
            }
        }
        private HisKskGeneralUpdate UpdateWorker
        {
            get
            {
                return (HisKskGeneralUpdate)Worker.Get<HisKskGeneralUpdate>();
            }
        }
        private HisKskGeneralDelete DeleteWorker
        {
            get
            {
                return (HisKskGeneralDelete)Worker.Get<HisKskGeneralDelete>();
            }
        }
        private HisKskGeneralTruncate TruncateWorker
        {
            get
            {
                return (HisKskGeneralTruncate)Worker.Get<HisKskGeneralTruncate>();
            }
        }
        private HisKskGeneralGet GetWorker
        {
            get
            {
                return (HisKskGeneralGet)Worker.Get<HisKskGeneralGet>();
            }
        }
        private HisKskGeneralCheck CheckWorker
        {
            get
            {
                return (HisKskGeneralCheck)Worker.Get<HisKskGeneralCheck>();
            }
        }

        public bool Create(HIS_KSK_GENERAL data)
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

        public bool CreateList(List<HIS_KSK_GENERAL> listData)
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

        public bool Update(HIS_KSK_GENERAL data)
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

        public bool UpdateList(List<HIS_KSK_GENERAL> listData)
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

        public bool Delete(HIS_KSK_GENERAL data)
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

        public bool DeleteList(List<HIS_KSK_GENERAL> listData)
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

        public bool Truncate(HIS_KSK_GENERAL data)
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

        public bool TruncateList(List<HIS_KSK_GENERAL> listData)
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

        public List<HIS_KSK_GENERAL> Get(HisKskGeneralSO search, CommonParam param)
        {
            List<HIS_KSK_GENERAL> result = new List<HIS_KSK_GENERAL>();
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

        public HIS_KSK_GENERAL GetById(long id, HisKskGeneralSO search)
        {
            HIS_KSK_GENERAL result = null;
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
