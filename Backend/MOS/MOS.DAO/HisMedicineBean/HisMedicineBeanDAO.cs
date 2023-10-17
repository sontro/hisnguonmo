using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMedicineBean
{
    public partial class HisMedicineBeanDAO : EntityBase
    {
        private HisMedicineBeanCreate CreateWorker
        {
            get
            {
                return (HisMedicineBeanCreate)Worker.Get<HisMedicineBeanCreate>();
            }
        }
        private HisMedicineBeanUpdate UpdateWorker
        {
            get
            {
                return (HisMedicineBeanUpdate)Worker.Get<HisMedicineBeanUpdate>();
            }
        }
        private HisMedicineBeanDelete DeleteWorker
        {
            get
            {
                return (HisMedicineBeanDelete)Worker.Get<HisMedicineBeanDelete>();
            }
        }
        private HisMedicineBeanTruncate TruncateWorker
        {
            get
            {
                return (HisMedicineBeanTruncate)Worker.Get<HisMedicineBeanTruncate>();
            }
        }
        private HisMedicineBeanGet GetWorker
        {
            get
            {
                return (HisMedicineBeanGet)Worker.Get<HisMedicineBeanGet>();
            }
        }
        private HisMedicineBeanCheck CheckWorker
        {
            get
            {
                return (HisMedicineBeanCheck)Worker.Get<HisMedicineBeanCheck>();
            }
        }

        public bool Create(HIS_MEDICINE_BEAN data)
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

        public bool CreateList(List<HIS_MEDICINE_BEAN> listData)
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

        public bool Update(HIS_MEDICINE_BEAN data)
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

        public bool UpdateList(List<HIS_MEDICINE_BEAN> listData)
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

        public bool Delete(HIS_MEDICINE_BEAN data)
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

        public bool DeleteList(List<HIS_MEDICINE_BEAN> listData)
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

        public bool Truncate(HIS_MEDICINE_BEAN data)
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

        public bool TruncateList(List<HIS_MEDICINE_BEAN> listData)
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

        public List<HIS_MEDICINE_BEAN> Get(HisMedicineBeanSO search, CommonParam param)
        {
            List<HIS_MEDICINE_BEAN> result = new List<HIS_MEDICINE_BEAN>();
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

        public HIS_MEDICINE_BEAN GetById(long id, HisMedicineBeanSO search)
        {
            HIS_MEDICINE_BEAN result = null;
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
