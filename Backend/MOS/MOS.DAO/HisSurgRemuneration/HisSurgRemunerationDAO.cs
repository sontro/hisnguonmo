using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisSurgRemuneration
{
    public partial class HisSurgRemunerationDAO : EntityBase
    {
        private HisSurgRemunerationCreate CreateWorker
        {
            get
            {
                return (HisSurgRemunerationCreate)Worker.Get<HisSurgRemunerationCreate>();
            }
        }
        private HisSurgRemunerationUpdate UpdateWorker
        {
            get
            {
                return (HisSurgRemunerationUpdate)Worker.Get<HisSurgRemunerationUpdate>();
            }
        }
        private HisSurgRemunerationDelete DeleteWorker
        {
            get
            {
                return (HisSurgRemunerationDelete)Worker.Get<HisSurgRemunerationDelete>();
            }
        }
        private HisSurgRemunerationTruncate TruncateWorker
        {
            get
            {
                return (HisSurgRemunerationTruncate)Worker.Get<HisSurgRemunerationTruncate>();
            }
        }
        private HisSurgRemunerationGet GetWorker
        {
            get
            {
                return (HisSurgRemunerationGet)Worker.Get<HisSurgRemunerationGet>();
            }
        }
        private HisSurgRemunerationCheck CheckWorker
        {
            get
            {
                return (HisSurgRemunerationCheck)Worker.Get<HisSurgRemunerationCheck>();
            }
        }

        public bool Create(HIS_SURG_REMUNERATION data)
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

        public bool CreateList(List<HIS_SURG_REMUNERATION> listData)
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

        public bool Update(HIS_SURG_REMUNERATION data)
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

        public bool UpdateList(List<HIS_SURG_REMUNERATION> listData)
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

        public bool Delete(HIS_SURG_REMUNERATION data)
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

        public bool DeleteList(List<HIS_SURG_REMUNERATION> listData)
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

        public bool Truncate(HIS_SURG_REMUNERATION data)
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

        public bool TruncateList(List<HIS_SURG_REMUNERATION> listData)
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

        public List<HIS_SURG_REMUNERATION> Get(HisSurgRemunerationSO search, CommonParam param)
        {
            List<HIS_SURG_REMUNERATION> result = new List<HIS_SURG_REMUNERATION>();
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

        public HIS_SURG_REMUNERATION GetById(long id, HisSurgRemunerationSO search)
        {
            HIS_SURG_REMUNERATION result = null;
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
