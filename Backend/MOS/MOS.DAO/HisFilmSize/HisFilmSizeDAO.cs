using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisFilmSize
{
    public partial class HisFilmSizeDAO : EntityBase
    {
        private HisFilmSizeCreate CreateWorker
        {
            get
            {
                return (HisFilmSizeCreate)Worker.Get<HisFilmSizeCreate>();
            }
        }
        private HisFilmSizeUpdate UpdateWorker
        {
            get
            {
                return (HisFilmSizeUpdate)Worker.Get<HisFilmSizeUpdate>();
            }
        }
        private HisFilmSizeDelete DeleteWorker
        {
            get
            {
                return (HisFilmSizeDelete)Worker.Get<HisFilmSizeDelete>();
            }
        }
        private HisFilmSizeTruncate TruncateWorker
        {
            get
            {
                return (HisFilmSizeTruncate)Worker.Get<HisFilmSizeTruncate>();
            }
        }
        private HisFilmSizeGet GetWorker
        {
            get
            {
                return (HisFilmSizeGet)Worker.Get<HisFilmSizeGet>();
            }
        }
        private HisFilmSizeCheck CheckWorker
        {
            get
            {
                return (HisFilmSizeCheck)Worker.Get<HisFilmSizeCheck>();
            }
        }

        public bool Create(HIS_FILM_SIZE data)
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

        public bool CreateList(List<HIS_FILM_SIZE> listData)
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

        public bool Update(HIS_FILM_SIZE data)
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

        public bool UpdateList(List<HIS_FILM_SIZE> listData)
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

        public bool Delete(HIS_FILM_SIZE data)
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

        public bool DeleteList(List<HIS_FILM_SIZE> listData)
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

        public bool Truncate(HIS_FILM_SIZE data)
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

        public bool TruncateList(List<HIS_FILM_SIZE> listData)
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

        public List<HIS_FILM_SIZE> Get(HisFilmSizeSO search, CommonParam param)
        {
            List<HIS_FILM_SIZE> result = new List<HIS_FILM_SIZE>();
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

        public HIS_FILM_SIZE GetById(long id, HisFilmSizeSO search)
        {
            HIS_FILM_SIZE result = null;
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
