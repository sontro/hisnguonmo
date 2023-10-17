using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisSuimIndexUnit
{
    public partial class HisSuimIndexUnitDAO : EntityBase
    {
        private HisSuimIndexUnitCreate CreateWorker
        {
            get
            {
                return (HisSuimIndexUnitCreate)Worker.Get<HisSuimIndexUnitCreate>();
            }
        }
        private HisSuimIndexUnitUpdate UpdateWorker
        {
            get
            {
                return (HisSuimIndexUnitUpdate)Worker.Get<HisSuimIndexUnitUpdate>();
            }
        }
        private HisSuimIndexUnitDelete DeleteWorker
        {
            get
            {
                return (HisSuimIndexUnitDelete)Worker.Get<HisSuimIndexUnitDelete>();
            }
        }
        private HisSuimIndexUnitTruncate TruncateWorker
        {
            get
            {
                return (HisSuimIndexUnitTruncate)Worker.Get<HisSuimIndexUnitTruncate>();
            }
        }
        private HisSuimIndexUnitGet GetWorker
        {
            get
            {
                return (HisSuimIndexUnitGet)Worker.Get<HisSuimIndexUnitGet>();
            }
        }
        private HisSuimIndexUnitCheck CheckWorker
        {
            get
            {
                return (HisSuimIndexUnitCheck)Worker.Get<HisSuimIndexUnitCheck>();
            }
        }

        public bool Create(HIS_SUIM_INDEX_UNIT data)
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

        public bool CreateList(List<HIS_SUIM_INDEX_UNIT> listData)
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

        public bool Update(HIS_SUIM_INDEX_UNIT data)
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

        public bool UpdateList(List<HIS_SUIM_INDEX_UNIT> listData)
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

        public bool Delete(HIS_SUIM_INDEX_UNIT data)
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

        public bool DeleteList(List<HIS_SUIM_INDEX_UNIT> listData)
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

        public bool Truncate(HIS_SUIM_INDEX_UNIT data)
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

        public bool TruncateList(List<HIS_SUIM_INDEX_UNIT> listData)
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

        public List<HIS_SUIM_INDEX_UNIT> Get(HisSuimIndexUnitSO search, CommonParam param)
        {
            List<HIS_SUIM_INDEX_UNIT> result = new List<HIS_SUIM_INDEX_UNIT>();
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

        public HIS_SUIM_INDEX_UNIT GetById(long id, HisSuimIndexUnitSO search)
        {
            HIS_SUIM_INDEX_UNIT result = null;
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
