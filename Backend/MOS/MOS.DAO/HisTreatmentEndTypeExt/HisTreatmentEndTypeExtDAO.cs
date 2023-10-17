using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisTreatmentEndTypeExt
{
    public partial class HisTreatmentEndTypeExtDAO : EntityBase
    {
        private HisTreatmentEndTypeExtCreate CreateWorker
        {
            get
            {
                return (HisTreatmentEndTypeExtCreate)Worker.Get<HisTreatmentEndTypeExtCreate>();
            }
        }
        private HisTreatmentEndTypeExtUpdate UpdateWorker
        {
            get
            {
                return (HisTreatmentEndTypeExtUpdate)Worker.Get<HisTreatmentEndTypeExtUpdate>();
            }
        }
        private HisTreatmentEndTypeExtDelete DeleteWorker
        {
            get
            {
                return (HisTreatmentEndTypeExtDelete)Worker.Get<HisTreatmentEndTypeExtDelete>();
            }
        }
        private HisTreatmentEndTypeExtTruncate TruncateWorker
        {
            get
            {
                return (HisTreatmentEndTypeExtTruncate)Worker.Get<HisTreatmentEndTypeExtTruncate>();
            }
        }
        private HisTreatmentEndTypeExtGet GetWorker
        {
            get
            {
                return (HisTreatmentEndTypeExtGet)Worker.Get<HisTreatmentEndTypeExtGet>();
            }
        }
        private HisTreatmentEndTypeExtCheck CheckWorker
        {
            get
            {
                return (HisTreatmentEndTypeExtCheck)Worker.Get<HisTreatmentEndTypeExtCheck>();
            }
        }

        public bool Create(HIS_TREATMENT_END_TYPE_EXT data)
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

        public bool CreateList(List<HIS_TREATMENT_END_TYPE_EXT> listData)
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

        public bool Update(HIS_TREATMENT_END_TYPE_EXT data)
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

        public bool UpdateList(List<HIS_TREATMENT_END_TYPE_EXT> listData)
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

        public bool Delete(HIS_TREATMENT_END_TYPE_EXT data)
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

        public bool DeleteList(List<HIS_TREATMENT_END_TYPE_EXT> listData)
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

        public bool Truncate(HIS_TREATMENT_END_TYPE_EXT data)
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

        public bool TruncateList(List<HIS_TREATMENT_END_TYPE_EXT> listData)
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

        public List<HIS_TREATMENT_END_TYPE_EXT> Get(HisTreatmentEndTypeExtSO search, CommonParam param)
        {
            List<HIS_TREATMENT_END_TYPE_EXT> result = new List<HIS_TREATMENT_END_TYPE_EXT>();
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

        public HIS_TREATMENT_END_TYPE_EXT GetById(long id, HisTreatmentEndTypeExtSO search)
        {
            HIS_TREATMENT_END_TYPE_EXT result = null;
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
