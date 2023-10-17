using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisBloodVolume
{
    public partial class HisBloodVolumeDAO : EntityBase
    {
        private HisBloodVolumeCreate CreateWorker
        {
            get
            {
                return (HisBloodVolumeCreate)Worker.Get<HisBloodVolumeCreate>();
            }
        }
        private HisBloodVolumeUpdate UpdateWorker
        {
            get
            {
                return (HisBloodVolumeUpdate)Worker.Get<HisBloodVolumeUpdate>();
            }
        }
        private HisBloodVolumeDelete DeleteWorker
        {
            get
            {
                return (HisBloodVolumeDelete)Worker.Get<HisBloodVolumeDelete>();
            }
        }
        private HisBloodVolumeTruncate TruncateWorker
        {
            get
            {
                return (HisBloodVolumeTruncate)Worker.Get<HisBloodVolumeTruncate>();
            }
        }
        private HisBloodVolumeGet GetWorker
        {
            get
            {
                return (HisBloodVolumeGet)Worker.Get<HisBloodVolumeGet>();
            }
        }
        private HisBloodVolumeCheck CheckWorker
        {
            get
            {
                return (HisBloodVolumeCheck)Worker.Get<HisBloodVolumeCheck>();
            }
        }

        public bool Create(HIS_BLOOD_VOLUME data)
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

        public bool CreateList(List<HIS_BLOOD_VOLUME> listData)
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

        public bool Update(HIS_BLOOD_VOLUME data)
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

        public bool UpdateList(List<HIS_BLOOD_VOLUME> listData)
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

        public bool Delete(HIS_BLOOD_VOLUME data)
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

        public bool DeleteList(List<HIS_BLOOD_VOLUME> listData)
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

        public bool Truncate(HIS_BLOOD_VOLUME data)
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

        public bool TruncateList(List<HIS_BLOOD_VOLUME> listData)
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

        public List<HIS_BLOOD_VOLUME> Get(HisBloodVolumeSO search, CommonParam param)
        {
            List<HIS_BLOOD_VOLUME> result = new List<HIS_BLOOD_VOLUME>();
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

        public HIS_BLOOD_VOLUME GetById(long id, HisBloodVolumeSO search)
        {
            HIS_BLOOD_VOLUME result = null;
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
