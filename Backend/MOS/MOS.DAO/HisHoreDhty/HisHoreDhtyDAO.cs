using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisHoreDhty
{
    public partial class HisHoreDhtyDAO : EntityBase
    {
        private HisHoreDhtyCreate CreateWorker
        {
            get
            {
                return (HisHoreDhtyCreate)Worker.Get<HisHoreDhtyCreate>();
            }
        }
        private HisHoreDhtyUpdate UpdateWorker
        {
            get
            {
                return (HisHoreDhtyUpdate)Worker.Get<HisHoreDhtyUpdate>();
            }
        }
        private HisHoreDhtyDelete DeleteWorker
        {
            get
            {
                return (HisHoreDhtyDelete)Worker.Get<HisHoreDhtyDelete>();
            }
        }
        private HisHoreDhtyTruncate TruncateWorker
        {
            get
            {
                return (HisHoreDhtyTruncate)Worker.Get<HisHoreDhtyTruncate>();
            }
        }
        private HisHoreDhtyGet GetWorker
        {
            get
            {
                return (HisHoreDhtyGet)Worker.Get<HisHoreDhtyGet>();
            }
        }
        private HisHoreDhtyCheck CheckWorker
        {
            get
            {
                return (HisHoreDhtyCheck)Worker.Get<HisHoreDhtyCheck>();
            }
        }

        public bool Create(HIS_HORE_DHTY data)
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

        public bool CreateList(List<HIS_HORE_DHTY> listData)
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

        public bool Update(HIS_HORE_DHTY data)
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

        public bool UpdateList(List<HIS_HORE_DHTY> listData)
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

        public bool Delete(HIS_HORE_DHTY data)
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

        public bool DeleteList(List<HIS_HORE_DHTY> listData)
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

        public bool Truncate(HIS_HORE_DHTY data)
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

        public bool TruncateList(List<HIS_HORE_DHTY> listData)
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

        public List<HIS_HORE_DHTY> Get(HisHoreDhtySO search, CommonParam param)
        {
            List<HIS_HORE_DHTY> result = new List<HIS_HORE_DHTY>();
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

        public HIS_HORE_DHTY GetById(long id, HisHoreDhtySO search)
        {
            HIS_HORE_DHTY result = null;
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
