using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisSuimIndex
{
    public partial class HisSuimIndexDAO : EntityBase
    {
        private HisSuimIndexCreate CreateWorker
        {
            get
            {
                return (HisSuimIndexCreate)Worker.Get<HisSuimIndexCreate>();
            }
        }
        private HisSuimIndexUpdate UpdateWorker
        {
            get
            {
                return (HisSuimIndexUpdate)Worker.Get<HisSuimIndexUpdate>();
            }
        }
        private HisSuimIndexDelete DeleteWorker
        {
            get
            {
                return (HisSuimIndexDelete)Worker.Get<HisSuimIndexDelete>();
            }
        }
        private HisSuimIndexTruncate TruncateWorker
        {
            get
            {
                return (HisSuimIndexTruncate)Worker.Get<HisSuimIndexTruncate>();
            }
        }
        private HisSuimIndexGet GetWorker
        {
            get
            {
                return (HisSuimIndexGet)Worker.Get<HisSuimIndexGet>();
            }
        }
        private HisSuimIndexCheck CheckWorker
        {
            get
            {
                return (HisSuimIndexCheck)Worker.Get<HisSuimIndexCheck>();
            }
        }

        public bool Create(HIS_SUIM_INDEX data)
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

        public bool CreateList(List<HIS_SUIM_INDEX> listData)
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

        public bool Update(HIS_SUIM_INDEX data)
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

        public bool UpdateList(List<HIS_SUIM_INDEX> listData)
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

        public bool Delete(HIS_SUIM_INDEX data)
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

        public bool DeleteList(List<HIS_SUIM_INDEX> listData)
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

        public bool Truncate(HIS_SUIM_INDEX data)
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

        public bool TruncateList(List<HIS_SUIM_INDEX> listData)
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

        public List<HIS_SUIM_INDEX> Get(HisSuimIndexSO search, CommonParam param)
        {
            List<HIS_SUIM_INDEX> result = new List<HIS_SUIM_INDEX>();
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

        public HIS_SUIM_INDEX GetById(long id, HisSuimIndexSO search)
        {
            HIS_SUIM_INDEX result = null;
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
