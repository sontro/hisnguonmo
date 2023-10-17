using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMestMetyUnit
{
    public partial class HisMestMetyUnitDAO : EntityBase
    {
        private HisMestMetyUnitCreate CreateWorker
        {
            get
            {
                return (HisMestMetyUnitCreate)Worker.Get<HisMestMetyUnitCreate>();
            }
        }
        private HisMestMetyUnitUpdate UpdateWorker
        {
            get
            {
                return (HisMestMetyUnitUpdate)Worker.Get<HisMestMetyUnitUpdate>();
            }
        }
        private HisMestMetyUnitDelete DeleteWorker
        {
            get
            {
                return (HisMestMetyUnitDelete)Worker.Get<HisMestMetyUnitDelete>();
            }
        }
        private HisMestMetyUnitTruncate TruncateWorker
        {
            get
            {
                return (HisMestMetyUnitTruncate)Worker.Get<HisMestMetyUnitTruncate>();
            }
        }
        private HisMestMetyUnitGet GetWorker
        {
            get
            {
                return (HisMestMetyUnitGet)Worker.Get<HisMestMetyUnitGet>();
            }
        }
        private HisMestMetyUnitCheck CheckWorker
        {
            get
            {
                return (HisMestMetyUnitCheck)Worker.Get<HisMestMetyUnitCheck>();
            }
        }

        public bool Create(HIS_MEST_METY_UNIT data)
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

        public bool CreateList(List<HIS_MEST_METY_UNIT> listData)
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

        public bool Update(HIS_MEST_METY_UNIT data)
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

        public bool UpdateList(List<HIS_MEST_METY_UNIT> listData)
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

        public bool Delete(HIS_MEST_METY_UNIT data)
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

        public bool DeleteList(List<HIS_MEST_METY_UNIT> listData)
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

        public bool Truncate(HIS_MEST_METY_UNIT data)
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

        public bool TruncateList(List<HIS_MEST_METY_UNIT> listData)
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

        public List<HIS_MEST_METY_UNIT> Get(HisMestMetyUnitSO search, CommonParam param)
        {
            List<HIS_MEST_METY_UNIT> result = new List<HIS_MEST_METY_UNIT>();
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

        public HIS_MEST_METY_UNIT GetById(long id, HisMestMetyUnitSO search)
        {
            HIS_MEST_METY_UNIT result = null;
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
