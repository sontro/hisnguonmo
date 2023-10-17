using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMedicineMaterial
{
    public partial class HisMedicineMaterialDAO : EntityBase
    {
        private HisMedicineMaterialCreate CreateWorker
        {
            get
            {
                return (HisMedicineMaterialCreate)Worker.Get<HisMedicineMaterialCreate>();
            }
        }
        private HisMedicineMaterialUpdate UpdateWorker
        {
            get
            {
                return (HisMedicineMaterialUpdate)Worker.Get<HisMedicineMaterialUpdate>();
            }
        }
        private HisMedicineMaterialDelete DeleteWorker
        {
            get
            {
                return (HisMedicineMaterialDelete)Worker.Get<HisMedicineMaterialDelete>();
            }
        }
        private HisMedicineMaterialTruncate TruncateWorker
        {
            get
            {
                return (HisMedicineMaterialTruncate)Worker.Get<HisMedicineMaterialTruncate>();
            }
        }
        private HisMedicineMaterialGet GetWorker
        {
            get
            {
                return (HisMedicineMaterialGet)Worker.Get<HisMedicineMaterialGet>();
            }
        }
        private HisMedicineMaterialCheck CheckWorker
        {
            get
            {
                return (HisMedicineMaterialCheck)Worker.Get<HisMedicineMaterialCheck>();
            }
        }

        public bool Create(HIS_MEDICINE_MATERIAL data)
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

        public bool CreateList(List<HIS_MEDICINE_MATERIAL> listData)
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

        public bool Update(HIS_MEDICINE_MATERIAL data)
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

        public bool UpdateList(List<HIS_MEDICINE_MATERIAL> listData)
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

        public bool Delete(HIS_MEDICINE_MATERIAL data)
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

        public bool DeleteList(List<HIS_MEDICINE_MATERIAL> listData)
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

        public bool Truncate(HIS_MEDICINE_MATERIAL data)
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

        public bool TruncateList(List<HIS_MEDICINE_MATERIAL> listData)
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

        public List<HIS_MEDICINE_MATERIAL> Get(HisMedicineMaterialSO search, CommonParam param)
        {
            List<HIS_MEDICINE_MATERIAL> result = new List<HIS_MEDICINE_MATERIAL>();
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

        public HIS_MEDICINE_MATERIAL GetById(long id, HisMedicineMaterialSO search)
        {
            HIS_MEDICINE_MATERIAL result = null;
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
