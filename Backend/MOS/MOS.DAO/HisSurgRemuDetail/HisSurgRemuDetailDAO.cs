using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisSurgRemuDetail
{
    public partial class HisSurgRemuDetailDAO : EntityBase
    {
        private HisSurgRemuDetailCreate CreateWorker
        {
            get
            {
                return (HisSurgRemuDetailCreate)Worker.Get<HisSurgRemuDetailCreate>();
            }
        }
        private HisSurgRemuDetailUpdate UpdateWorker
        {
            get
            {
                return (HisSurgRemuDetailUpdate)Worker.Get<HisSurgRemuDetailUpdate>();
            }
        }
        private HisSurgRemuDetailDelete DeleteWorker
        {
            get
            {
                return (HisSurgRemuDetailDelete)Worker.Get<HisSurgRemuDetailDelete>();
            }
        }
        private HisSurgRemuDetailTruncate TruncateWorker
        {
            get
            {
                return (HisSurgRemuDetailTruncate)Worker.Get<HisSurgRemuDetailTruncate>();
            }
        }
        private HisSurgRemuDetailGet GetWorker
        {
            get
            {
                return (HisSurgRemuDetailGet)Worker.Get<HisSurgRemuDetailGet>();
            }
        }
        private HisSurgRemuDetailCheck CheckWorker
        {
            get
            {
                return (HisSurgRemuDetailCheck)Worker.Get<HisSurgRemuDetailCheck>();
            }
        }

        public bool Create(HIS_SURG_REMU_DETAIL data)
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

        public bool CreateList(List<HIS_SURG_REMU_DETAIL> listData)
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

        public bool Update(HIS_SURG_REMU_DETAIL data)
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

        public bool UpdateList(List<HIS_SURG_REMU_DETAIL> listData)
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

        public bool Delete(HIS_SURG_REMU_DETAIL data)
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

        public bool DeleteList(List<HIS_SURG_REMU_DETAIL> listData)
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

        public bool Truncate(HIS_SURG_REMU_DETAIL data)
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

        public bool TruncateList(List<HIS_SURG_REMU_DETAIL> listData)
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

        public List<HIS_SURG_REMU_DETAIL> Get(HisSurgRemuDetailSO search, CommonParam param)
        {
            List<HIS_SURG_REMU_DETAIL> result = new List<HIS_SURG_REMU_DETAIL>();
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

        public HIS_SURG_REMU_DETAIL GetById(long id, HisSurgRemuDetailSO search)
        {
            HIS_SURG_REMU_DETAIL result = null;
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
