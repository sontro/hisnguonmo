using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisTransfusion
{
    public partial class HisTransfusionDAO : EntityBase
    {
        private HisTransfusionCreate CreateWorker
        {
            get
            {
                return (HisTransfusionCreate)Worker.Get<HisTransfusionCreate>();
            }
        }
        private HisTransfusionUpdate UpdateWorker
        {
            get
            {
                return (HisTransfusionUpdate)Worker.Get<HisTransfusionUpdate>();
            }
        }
        private HisTransfusionDelete DeleteWorker
        {
            get
            {
                return (HisTransfusionDelete)Worker.Get<HisTransfusionDelete>();
            }
        }
        private HisTransfusionTruncate TruncateWorker
        {
            get
            {
                return (HisTransfusionTruncate)Worker.Get<HisTransfusionTruncate>();
            }
        }
        private HisTransfusionGet GetWorker
        {
            get
            {
                return (HisTransfusionGet)Worker.Get<HisTransfusionGet>();
            }
        }
        private HisTransfusionCheck CheckWorker
        {
            get
            {
                return (HisTransfusionCheck)Worker.Get<HisTransfusionCheck>();
            }
        }

        public bool Create(HIS_TRANSFUSION data)
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

        public bool CreateList(List<HIS_TRANSFUSION> listData)
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

        public bool Update(HIS_TRANSFUSION data)
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

        public bool UpdateList(List<HIS_TRANSFUSION> listData)
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

        public bool Delete(HIS_TRANSFUSION data)
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

        public bool DeleteList(List<HIS_TRANSFUSION> listData)
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

        public bool Truncate(HIS_TRANSFUSION data)
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

        public bool TruncateList(List<HIS_TRANSFUSION> listData)
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

        public List<HIS_TRANSFUSION> Get(HisTransfusionSO search, CommonParam param)
        {
            List<HIS_TRANSFUSION> result = new List<HIS_TRANSFUSION>();
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

        public HIS_TRANSFUSION GetById(long id, HisTransfusionSO search)
        {
            HIS_TRANSFUSION result = null;
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
