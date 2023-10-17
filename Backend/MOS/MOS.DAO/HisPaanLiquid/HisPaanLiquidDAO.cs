using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisPaanLiquid
{
    public partial class HisPaanLiquidDAO : EntityBase
    {
        private HisPaanLiquidCreate CreateWorker
        {
            get
            {
                return (HisPaanLiquidCreate)Worker.Get<HisPaanLiquidCreate>();
            }
        }
        private HisPaanLiquidUpdate UpdateWorker
        {
            get
            {
                return (HisPaanLiquidUpdate)Worker.Get<HisPaanLiquidUpdate>();
            }
        }
        private HisPaanLiquidDelete DeleteWorker
        {
            get
            {
                return (HisPaanLiquidDelete)Worker.Get<HisPaanLiquidDelete>();
            }
        }
        private HisPaanLiquidTruncate TruncateWorker
        {
            get
            {
                return (HisPaanLiquidTruncate)Worker.Get<HisPaanLiquidTruncate>();
            }
        }
        private HisPaanLiquidGet GetWorker
        {
            get
            {
                return (HisPaanLiquidGet)Worker.Get<HisPaanLiquidGet>();
            }
        }
        private HisPaanLiquidCheck CheckWorker
        {
            get
            {
                return (HisPaanLiquidCheck)Worker.Get<HisPaanLiquidCheck>();
            }
        }

        public bool Create(HIS_PAAN_LIQUID data)
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

        public bool CreateList(List<HIS_PAAN_LIQUID> listData)
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

        public bool Update(HIS_PAAN_LIQUID data)
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

        public bool UpdateList(List<HIS_PAAN_LIQUID> listData)
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

        public bool Delete(HIS_PAAN_LIQUID data)
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

        public bool DeleteList(List<HIS_PAAN_LIQUID> listData)
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

        public bool Truncate(HIS_PAAN_LIQUID data)
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

        public bool TruncateList(List<HIS_PAAN_LIQUID> listData)
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

        public List<HIS_PAAN_LIQUID> Get(HisPaanLiquidSO search, CommonParam param)
        {
            List<HIS_PAAN_LIQUID> result = new List<HIS_PAAN_LIQUID>();
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

        public HIS_PAAN_LIQUID GetById(long id, HisPaanLiquidSO search)
        {
            HIS_PAAN_LIQUID result = null;
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
