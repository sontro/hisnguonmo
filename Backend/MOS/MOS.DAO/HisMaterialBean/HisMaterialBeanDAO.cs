using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMaterialBean
{
    public partial class HisMaterialBeanDAO : EntityBase
    {
        private HisMaterialBeanCreate CreateWorker
        {
            get
            {
                return (HisMaterialBeanCreate)Worker.Get<HisMaterialBeanCreate>();
            }
        }
        private HisMaterialBeanUpdate UpdateWorker
        {
            get
            {
                return (HisMaterialBeanUpdate)Worker.Get<HisMaterialBeanUpdate>();
            }
        }
        private HisMaterialBeanDelete DeleteWorker
        {
            get
            {
                return (HisMaterialBeanDelete)Worker.Get<HisMaterialBeanDelete>();
            }
        }
        private HisMaterialBeanTruncate TruncateWorker
        {
            get
            {
                return (HisMaterialBeanTruncate)Worker.Get<HisMaterialBeanTruncate>();
            }
        }
        private HisMaterialBeanGet GetWorker
        {
            get
            {
                return (HisMaterialBeanGet)Worker.Get<HisMaterialBeanGet>();
            }
        }
        private HisMaterialBeanCheck CheckWorker
        {
            get
            {
                return (HisMaterialBeanCheck)Worker.Get<HisMaterialBeanCheck>();
            }
        }

        public bool Create(HIS_MATERIAL_BEAN data)
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

        public bool CreateList(List<HIS_MATERIAL_BEAN> listData)
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

        public bool Update(HIS_MATERIAL_BEAN data)
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

        public bool UpdateList(List<HIS_MATERIAL_BEAN> listData)
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

        public bool Delete(HIS_MATERIAL_BEAN data)
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

        public bool DeleteList(List<HIS_MATERIAL_BEAN> listData)
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

        public bool Truncate(HIS_MATERIAL_BEAN data)
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

        public bool TruncateList(List<HIS_MATERIAL_BEAN> listData)
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

        public List<HIS_MATERIAL_BEAN> Get(HisMaterialBeanSO search, CommonParam param)
        {
            List<HIS_MATERIAL_BEAN> result = new List<HIS_MATERIAL_BEAN>();
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

        public HIS_MATERIAL_BEAN GetById(long id, HisMaterialBeanSO search)
        {
            HIS_MATERIAL_BEAN result = null;
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
