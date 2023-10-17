using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisSeseDepoRepay
{
    public partial class HisSeseDepoRepayDAO : EntityBase
    {
        private HisSeseDepoRepayCreate CreateWorker
        {
            get
            {
                return (HisSeseDepoRepayCreate)Worker.Get<HisSeseDepoRepayCreate>();
            }
        }
        private HisSeseDepoRepayUpdate UpdateWorker
        {
            get
            {
                return (HisSeseDepoRepayUpdate)Worker.Get<HisSeseDepoRepayUpdate>();
            }
        }
        private HisSeseDepoRepayDelete DeleteWorker
        {
            get
            {
                return (HisSeseDepoRepayDelete)Worker.Get<HisSeseDepoRepayDelete>();
            }
        }
        private HisSeseDepoRepayTruncate TruncateWorker
        {
            get
            {
                return (HisSeseDepoRepayTruncate)Worker.Get<HisSeseDepoRepayTruncate>();
            }
        }
        private HisSeseDepoRepayGet GetWorker
        {
            get
            {
                return (HisSeseDepoRepayGet)Worker.Get<HisSeseDepoRepayGet>();
            }
        }
        private HisSeseDepoRepayCheck CheckWorker
        {
            get
            {
                return (HisSeseDepoRepayCheck)Worker.Get<HisSeseDepoRepayCheck>();
            }
        }

        public bool Create(HIS_SESE_DEPO_REPAY data)
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

        public bool CreateList(List<HIS_SESE_DEPO_REPAY> listData)
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

        public bool Update(HIS_SESE_DEPO_REPAY data)
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

        public bool UpdateList(List<HIS_SESE_DEPO_REPAY> listData)
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

        public bool Delete(HIS_SESE_DEPO_REPAY data)
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

        public bool DeleteList(List<HIS_SESE_DEPO_REPAY> listData)
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

        public bool Truncate(HIS_SESE_DEPO_REPAY data)
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

        public bool TruncateList(List<HIS_SESE_DEPO_REPAY> listData)
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

        public List<HIS_SESE_DEPO_REPAY> Get(HisSeseDepoRepaySO search, CommonParam param)
        {
            List<HIS_SESE_DEPO_REPAY> result = new List<HIS_SESE_DEPO_REPAY>();
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

        public HIS_SESE_DEPO_REPAY GetById(long id, HisSeseDepoRepaySO search)
        {
            HIS_SESE_DEPO_REPAY result = null;
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
