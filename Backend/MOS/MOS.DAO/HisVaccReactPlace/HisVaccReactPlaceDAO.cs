using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisVaccReactPlace
{
    public partial class HisVaccReactPlaceDAO : EntityBase
    {
        private HisVaccReactPlaceCreate CreateWorker
        {
            get
            {
                return (HisVaccReactPlaceCreate)Worker.Get<HisVaccReactPlaceCreate>();
            }
        }
        private HisVaccReactPlaceUpdate UpdateWorker
        {
            get
            {
                return (HisVaccReactPlaceUpdate)Worker.Get<HisVaccReactPlaceUpdate>();
            }
        }
        private HisVaccReactPlaceDelete DeleteWorker
        {
            get
            {
                return (HisVaccReactPlaceDelete)Worker.Get<HisVaccReactPlaceDelete>();
            }
        }
        private HisVaccReactPlaceTruncate TruncateWorker
        {
            get
            {
                return (HisVaccReactPlaceTruncate)Worker.Get<HisVaccReactPlaceTruncate>();
            }
        }
        private HisVaccReactPlaceGet GetWorker
        {
            get
            {
                return (HisVaccReactPlaceGet)Worker.Get<HisVaccReactPlaceGet>();
            }
        }
        private HisVaccReactPlaceCheck CheckWorker
        {
            get
            {
                return (HisVaccReactPlaceCheck)Worker.Get<HisVaccReactPlaceCheck>();
            }
        }

        public bool Create(HIS_VACC_REACT_PLACE data)
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

        public bool CreateList(List<HIS_VACC_REACT_PLACE> listData)
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

        public bool Update(HIS_VACC_REACT_PLACE data)
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

        public bool UpdateList(List<HIS_VACC_REACT_PLACE> listData)
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

        public bool Delete(HIS_VACC_REACT_PLACE data)
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

        public bool DeleteList(List<HIS_VACC_REACT_PLACE> listData)
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

        public bool Truncate(HIS_VACC_REACT_PLACE data)
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

        public bool TruncateList(List<HIS_VACC_REACT_PLACE> listData)
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

        public List<HIS_VACC_REACT_PLACE> Get(HisVaccReactPlaceSO search, CommonParam param)
        {
            List<HIS_VACC_REACT_PLACE> result = new List<HIS_VACC_REACT_PLACE>();
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

        public HIS_VACC_REACT_PLACE GetById(long id, HisVaccReactPlaceSO search)
        {
            HIS_VACC_REACT_PLACE result = null;
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
