using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisSaroExro
{
    public partial class HisSaroExroDAO : EntityBase
    {
        private HisSaroExroCreate CreateWorker
        {
            get
            {
                return (HisSaroExroCreate)Worker.Get<HisSaroExroCreate>();
            }
        }
        private HisSaroExroUpdate UpdateWorker
        {
            get
            {
                return (HisSaroExroUpdate)Worker.Get<HisSaroExroUpdate>();
            }
        }
        private HisSaroExroDelete DeleteWorker
        {
            get
            {
                return (HisSaroExroDelete)Worker.Get<HisSaroExroDelete>();
            }
        }
        private HisSaroExroTruncate TruncateWorker
        {
            get
            {
                return (HisSaroExroTruncate)Worker.Get<HisSaroExroTruncate>();
            }
        }
        private HisSaroExroGet GetWorker
        {
            get
            {
                return (HisSaroExroGet)Worker.Get<HisSaroExroGet>();
            }
        }
        private HisSaroExroCheck CheckWorker
        {
            get
            {
                return (HisSaroExroCheck)Worker.Get<HisSaroExroCheck>();
            }
        }

        public bool Create(HIS_SARO_EXRO data)
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

        public bool CreateList(List<HIS_SARO_EXRO> listData)
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

        public bool Update(HIS_SARO_EXRO data)
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

        public bool UpdateList(List<HIS_SARO_EXRO> listData)
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

        public bool Delete(HIS_SARO_EXRO data)
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

        public bool DeleteList(List<HIS_SARO_EXRO> listData)
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

        public bool Truncate(HIS_SARO_EXRO data)
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

        public bool TruncateList(List<HIS_SARO_EXRO> listData)
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

        public List<HIS_SARO_EXRO> Get(HisSaroExroSO search, CommonParam param)
        {
            List<HIS_SARO_EXRO> result = new List<HIS_SARO_EXRO>();
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

        public HIS_SARO_EXRO GetById(long id, HisSaroExroSO search)
        {
            HIS_SARO_EXRO result = null;
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
