using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisTreatmentEndType
{
    public partial class HisTreatmentEndTypeDAO : EntityBase
    {
        private HisTreatmentEndTypeCreate CreateWorker
        {
            get
            {
                return (HisTreatmentEndTypeCreate)Worker.Get<HisTreatmentEndTypeCreate>();
            }
        }
        private HisTreatmentEndTypeUpdate UpdateWorker
        {
            get
            {
                return (HisTreatmentEndTypeUpdate)Worker.Get<HisTreatmentEndTypeUpdate>();
            }
        }
        private HisTreatmentEndTypeDelete DeleteWorker
        {
            get
            {
                return (HisTreatmentEndTypeDelete)Worker.Get<HisTreatmentEndTypeDelete>();
            }
        }
        private HisTreatmentEndTypeTruncate TruncateWorker
        {
            get
            {
                return (HisTreatmentEndTypeTruncate)Worker.Get<HisTreatmentEndTypeTruncate>();
            }
        }
        private HisTreatmentEndTypeGet GetWorker
        {
            get
            {
                return (HisTreatmentEndTypeGet)Worker.Get<HisTreatmentEndTypeGet>();
            }
        }
        private HisTreatmentEndTypeCheck CheckWorker
        {
            get
            {
                return (HisTreatmentEndTypeCheck)Worker.Get<HisTreatmentEndTypeCheck>();
            }
        }

        public bool Create(HIS_TREATMENT_END_TYPE data)
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

        public bool CreateList(List<HIS_TREATMENT_END_TYPE> listData)
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

        public bool Update(HIS_TREATMENT_END_TYPE data)
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

        public bool UpdateList(List<HIS_TREATMENT_END_TYPE> listData)
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

        public bool Delete(HIS_TREATMENT_END_TYPE data)
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

        public bool DeleteList(List<HIS_TREATMENT_END_TYPE> listData)
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

        public bool Truncate(HIS_TREATMENT_END_TYPE data)
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

        public bool TruncateList(List<HIS_TREATMENT_END_TYPE> listData)
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

        public List<HIS_TREATMENT_END_TYPE> Get(HisTreatmentEndTypeSO search, CommonParam param)
        {
            List<HIS_TREATMENT_END_TYPE> result = new List<HIS_TREATMENT_END_TYPE>();
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

        public HIS_TREATMENT_END_TYPE GetById(long id, HisTreatmentEndTypeSO search)
        {
            HIS_TREATMENT_END_TYPE result = null;
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
