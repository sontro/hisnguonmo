using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisTransfusionSum
{
    public partial class HisTransfusionSumDAO : EntityBase
    {
        private HisTransfusionSumCreate CreateWorker
        {
            get
            {
                return (HisTransfusionSumCreate)Worker.Get<HisTransfusionSumCreate>();
            }
        }
        private HisTransfusionSumUpdate UpdateWorker
        {
            get
            {
                return (HisTransfusionSumUpdate)Worker.Get<HisTransfusionSumUpdate>();
            }
        }
        private HisTransfusionSumDelete DeleteWorker
        {
            get
            {
                return (HisTransfusionSumDelete)Worker.Get<HisTransfusionSumDelete>();
            }
        }
        private HisTransfusionSumTruncate TruncateWorker
        {
            get
            {
                return (HisTransfusionSumTruncate)Worker.Get<HisTransfusionSumTruncate>();
            }
        }
        private HisTransfusionSumGet GetWorker
        {
            get
            {
                return (HisTransfusionSumGet)Worker.Get<HisTransfusionSumGet>();
            }
        }
        private HisTransfusionSumCheck CheckWorker
        {
            get
            {
                return (HisTransfusionSumCheck)Worker.Get<HisTransfusionSumCheck>();
            }
        }

        public bool Create(HIS_TRANSFUSION_SUM data)
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

        public bool CreateList(List<HIS_TRANSFUSION_SUM> listData)
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

        public bool Update(HIS_TRANSFUSION_SUM data)
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

        public bool UpdateList(List<HIS_TRANSFUSION_SUM> listData)
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

        public bool Delete(HIS_TRANSFUSION_SUM data)
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

        public bool DeleteList(List<HIS_TRANSFUSION_SUM> listData)
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

        public bool Truncate(HIS_TRANSFUSION_SUM data)
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

        public bool TruncateList(List<HIS_TRANSFUSION_SUM> listData)
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

        public List<HIS_TRANSFUSION_SUM> Get(HisTransfusionSumSO search, CommonParam param)
        {
            List<HIS_TRANSFUSION_SUM> result = new List<HIS_TRANSFUSION_SUM>();
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

        public HIS_TRANSFUSION_SUM GetById(long id, HisTransfusionSumSO search)
        {
            HIS_TRANSFUSION_SUM result = null;
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
