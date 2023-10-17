using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisBltyVolume
{
    public partial class HisBltyVolumeDAO : EntityBase
    {
        private HisBltyVolumeCreate CreateWorker
        {
            get
            {
                return (HisBltyVolumeCreate)Worker.Get<HisBltyVolumeCreate>();
            }
        }
        private HisBltyVolumeUpdate UpdateWorker
        {
            get
            {
                return (HisBltyVolumeUpdate)Worker.Get<HisBltyVolumeUpdate>();
            }
        }
        private HisBltyVolumeDelete DeleteWorker
        {
            get
            {
                return (HisBltyVolumeDelete)Worker.Get<HisBltyVolumeDelete>();
            }
        }
        private HisBltyVolumeTruncate TruncateWorker
        {
            get
            {
                return (HisBltyVolumeTruncate)Worker.Get<HisBltyVolumeTruncate>();
            }
        }
        private HisBltyVolumeGet GetWorker
        {
            get
            {
                return (HisBltyVolumeGet)Worker.Get<HisBltyVolumeGet>();
            }
        }
        private HisBltyVolumeCheck CheckWorker
        {
            get
            {
                return (HisBltyVolumeCheck)Worker.Get<HisBltyVolumeCheck>();
            }
        }

        public bool Create(HIS_BLTY_VOLUME data)
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

        public bool CreateList(List<HIS_BLTY_VOLUME> listData)
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

        public bool Update(HIS_BLTY_VOLUME data)
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

        public bool UpdateList(List<HIS_BLTY_VOLUME> listData)
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

        public bool Delete(HIS_BLTY_VOLUME data)
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

        public bool DeleteList(List<HIS_BLTY_VOLUME> listData)
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

        public bool Truncate(HIS_BLTY_VOLUME data)
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

        public bool TruncateList(List<HIS_BLTY_VOLUME> listData)
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

        public List<HIS_BLTY_VOLUME> Get(HisBltyVolumeSO search, CommonParam param)
        {
            List<HIS_BLTY_VOLUME> result = new List<HIS_BLTY_VOLUME>();
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

        public HIS_BLTY_VOLUME GetById(long id, HisBltyVolumeSO search)
        {
            HIS_BLTY_VOLUME result = null;
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
