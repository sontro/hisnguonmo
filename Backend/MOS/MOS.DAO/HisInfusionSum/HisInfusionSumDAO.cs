using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisInfusionSum
{
    public partial class HisInfusionSumDAO : EntityBase
    {
        private HisInfusionSumCreate CreateWorker
        {
            get
            {
                return (HisInfusionSumCreate)Worker.Get<HisInfusionSumCreate>();
            }
        }
        private HisInfusionSumUpdate UpdateWorker
        {
            get
            {
                return (HisInfusionSumUpdate)Worker.Get<HisInfusionSumUpdate>();
            }
        }
        private HisInfusionSumDelete DeleteWorker
        {
            get
            {
                return (HisInfusionSumDelete)Worker.Get<HisInfusionSumDelete>();
            }
        }
        private HisInfusionSumTruncate TruncateWorker
        {
            get
            {
                return (HisInfusionSumTruncate)Worker.Get<HisInfusionSumTruncate>();
            }
        }
        private HisInfusionSumGet GetWorker
        {
            get
            {
                return (HisInfusionSumGet)Worker.Get<HisInfusionSumGet>();
            }
        }
        private HisInfusionSumCheck CheckWorker
        {
            get
            {
                return (HisInfusionSumCheck)Worker.Get<HisInfusionSumCheck>();
            }
        }

        public bool Create(HIS_INFUSION_SUM data)
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

        public bool CreateList(List<HIS_INFUSION_SUM> listData)
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

        public bool Update(HIS_INFUSION_SUM data)
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

        public bool UpdateList(List<HIS_INFUSION_SUM> listData)
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

        public bool Delete(HIS_INFUSION_SUM data)
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

        public bool DeleteList(List<HIS_INFUSION_SUM> listData)
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

        public bool Truncate(HIS_INFUSION_SUM data)
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

        public bool TruncateList(List<HIS_INFUSION_SUM> listData)
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

        public List<HIS_INFUSION_SUM> Get(HisInfusionSumSO search, CommonParam param)
        {
            List<HIS_INFUSION_SUM> result = new List<HIS_INFUSION_SUM>();
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

        public HIS_INFUSION_SUM GetById(long id, HisInfusionSumSO search)
        {
            HIS_INFUSION_SUM result = null;
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
