using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisTranPatiTech
{
    public partial class HisTranPatiTechDAO : EntityBase
    {
        private HisTranPatiTechCreate CreateWorker
        {
            get
            {
                return (HisTranPatiTechCreate)Worker.Get<HisTranPatiTechCreate>();
            }
        }
        private HisTranPatiTechUpdate UpdateWorker
        {
            get
            {
                return (HisTranPatiTechUpdate)Worker.Get<HisTranPatiTechUpdate>();
            }
        }
        private HisTranPatiTechDelete DeleteWorker
        {
            get
            {
                return (HisTranPatiTechDelete)Worker.Get<HisTranPatiTechDelete>();
            }
        }
        private HisTranPatiTechTruncate TruncateWorker
        {
            get
            {
                return (HisTranPatiTechTruncate)Worker.Get<HisTranPatiTechTruncate>();
            }
        }
        private HisTranPatiTechGet GetWorker
        {
            get
            {
                return (HisTranPatiTechGet)Worker.Get<HisTranPatiTechGet>();
            }
        }
        private HisTranPatiTechCheck CheckWorker
        {
            get
            {
                return (HisTranPatiTechCheck)Worker.Get<HisTranPatiTechCheck>();
            }
        }

        public bool Create(HIS_TRAN_PATI_TECH data)
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

        public bool CreateList(List<HIS_TRAN_PATI_TECH> listData)
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

        public bool Update(HIS_TRAN_PATI_TECH data)
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

        public bool UpdateList(List<HIS_TRAN_PATI_TECH> listData)
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

        public bool Delete(HIS_TRAN_PATI_TECH data)
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

        public bool DeleteList(List<HIS_TRAN_PATI_TECH> listData)
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

        public bool Truncate(HIS_TRAN_PATI_TECH data)
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

        public bool TruncateList(List<HIS_TRAN_PATI_TECH> listData)
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

        public List<HIS_TRAN_PATI_TECH> Get(HisTranPatiTechSO search, CommonParam param)
        {
            List<HIS_TRAN_PATI_TECH> result = new List<HIS_TRAN_PATI_TECH>();
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

        public HIS_TRAN_PATI_TECH GetById(long id, HisTranPatiTechSO search)
        {
            HIS_TRAN_PATI_TECH result = null;
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
