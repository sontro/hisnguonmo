using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisEmrCoverConfig
{
    public partial class HisEmrCoverConfigDAO : EntityBase
    {
        private HisEmrCoverConfigCreate CreateWorker
        {
            get
            {
                return (HisEmrCoverConfigCreate)Worker.Get<HisEmrCoverConfigCreate>();
            }
        }
        private HisEmrCoverConfigUpdate UpdateWorker
        {
            get
            {
                return (HisEmrCoverConfigUpdate)Worker.Get<HisEmrCoverConfigUpdate>();
            }
        }
        private HisEmrCoverConfigDelete DeleteWorker
        {
            get
            {
                return (HisEmrCoverConfigDelete)Worker.Get<HisEmrCoverConfigDelete>();
            }
        }
        private HisEmrCoverConfigTruncate TruncateWorker
        {
            get
            {
                return (HisEmrCoverConfigTruncate)Worker.Get<HisEmrCoverConfigTruncate>();
            }
        }
        private HisEmrCoverConfigGet GetWorker
        {
            get
            {
                return (HisEmrCoverConfigGet)Worker.Get<HisEmrCoverConfigGet>();
            }
        }
        private HisEmrCoverConfigCheck CheckWorker
        {
            get
            {
                return (HisEmrCoverConfigCheck)Worker.Get<HisEmrCoverConfigCheck>();
            }
        }

        public bool Create(HIS_EMR_COVER_CONFIG data)
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

        public bool CreateList(List<HIS_EMR_COVER_CONFIG> listData)
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

        public bool Update(HIS_EMR_COVER_CONFIG data)
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

        public bool UpdateList(List<HIS_EMR_COVER_CONFIG> listData)
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

        public bool Delete(HIS_EMR_COVER_CONFIG data)
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

        public bool DeleteList(List<HIS_EMR_COVER_CONFIG> listData)
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

        public bool Truncate(HIS_EMR_COVER_CONFIG data)
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

        public bool TruncateList(List<HIS_EMR_COVER_CONFIG> listData)
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

        public List<HIS_EMR_COVER_CONFIG> Get(HisEmrCoverConfigSO search, CommonParam param)
        {
            List<HIS_EMR_COVER_CONFIG> result = new List<HIS_EMR_COVER_CONFIG>();
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

        public HIS_EMR_COVER_CONFIG GetById(long id, HisEmrCoverConfigSO search)
        {
            HIS_EMR_COVER_CONFIG result = null;
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
