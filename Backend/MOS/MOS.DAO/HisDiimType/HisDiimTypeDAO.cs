using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisDiimType
{
    public partial class HisDiimTypeDAO : EntityBase
    {
        private HisDiimTypeCreate CreateWorker
        {
            get
            {
                return (HisDiimTypeCreate)Worker.Get<HisDiimTypeCreate>();
            }
        }
        private HisDiimTypeUpdate UpdateWorker
        {
            get
            {
                return (HisDiimTypeUpdate)Worker.Get<HisDiimTypeUpdate>();
            }
        }
        private HisDiimTypeDelete DeleteWorker
        {
            get
            {
                return (HisDiimTypeDelete)Worker.Get<HisDiimTypeDelete>();
            }
        }
        private HisDiimTypeTruncate TruncateWorker
        {
            get
            {
                return (HisDiimTypeTruncate)Worker.Get<HisDiimTypeTruncate>();
            }
        }
        private HisDiimTypeGet GetWorker
        {
            get
            {
                return (HisDiimTypeGet)Worker.Get<HisDiimTypeGet>();
            }
        }
        private HisDiimTypeCheck CheckWorker
        {
            get
            {
                return (HisDiimTypeCheck)Worker.Get<HisDiimTypeCheck>();
            }
        }

        public bool Create(HIS_DIIM_TYPE data)
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

        public bool CreateList(List<HIS_DIIM_TYPE> listData)
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

        public bool Update(HIS_DIIM_TYPE data)
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

        public bool UpdateList(List<HIS_DIIM_TYPE> listData)
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

        public bool Delete(HIS_DIIM_TYPE data)
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

        public bool DeleteList(List<HIS_DIIM_TYPE> listData)
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

        public bool Truncate(HIS_DIIM_TYPE data)
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

        public bool TruncateList(List<HIS_DIIM_TYPE> listData)
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

        public List<HIS_DIIM_TYPE> Get(HisDiimTypeSO search, CommonParam param)
        {
            List<HIS_DIIM_TYPE> result = new List<HIS_DIIM_TYPE>();
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

        public HIS_DIIM_TYPE GetById(long id, HisDiimTypeSO search)
        {
            HIS_DIIM_TYPE result = null;
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
