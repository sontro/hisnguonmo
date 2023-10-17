using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisKskDriverCar
{
    public partial class HisKskDriverCarDAO : EntityBase
    {
        private HisKskDriverCarCreate CreateWorker
        {
            get
            {
                return (HisKskDriverCarCreate)Worker.Get<HisKskDriverCarCreate>();
            }
        }
        private HisKskDriverCarUpdate UpdateWorker
        {
            get
            {
                return (HisKskDriverCarUpdate)Worker.Get<HisKskDriverCarUpdate>();
            }
        }
        private HisKskDriverCarDelete DeleteWorker
        {
            get
            {
                return (HisKskDriverCarDelete)Worker.Get<HisKskDriverCarDelete>();
            }
        }
        private HisKskDriverCarTruncate TruncateWorker
        {
            get
            {
                return (HisKskDriverCarTruncate)Worker.Get<HisKskDriverCarTruncate>();
            }
        }
        private HisKskDriverCarGet GetWorker
        {
            get
            {
                return (HisKskDriverCarGet)Worker.Get<HisKskDriverCarGet>();
            }
        }
        private HisKskDriverCarCheck CheckWorker
        {
            get
            {
                return (HisKskDriverCarCheck)Worker.Get<HisKskDriverCarCheck>();
            }
        }

        public bool Create(HIS_KSK_DRIVER_CAR data)
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

        public bool CreateList(List<HIS_KSK_DRIVER_CAR> listData)
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

        public bool Update(HIS_KSK_DRIVER_CAR data)
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

        public bool UpdateList(List<HIS_KSK_DRIVER_CAR> listData)
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

        public bool Delete(HIS_KSK_DRIVER_CAR data)
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

        public bool DeleteList(List<HIS_KSK_DRIVER_CAR> listData)
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

        public bool Truncate(HIS_KSK_DRIVER_CAR data)
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

        public bool TruncateList(List<HIS_KSK_DRIVER_CAR> listData)
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

        public List<HIS_KSK_DRIVER_CAR> Get(HisKskDriverCarSO search, CommonParam param)
        {
            List<HIS_KSK_DRIVER_CAR> result = new List<HIS_KSK_DRIVER_CAR>();
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

        public HIS_KSK_DRIVER_CAR GetById(long id, HisKskDriverCarSO search)
        {
            HIS_KSK_DRIVER_CAR result = null;
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
