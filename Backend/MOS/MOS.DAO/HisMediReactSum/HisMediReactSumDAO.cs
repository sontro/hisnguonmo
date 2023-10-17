using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMediReactSum
{
    public partial class HisMediReactSumDAO : EntityBase
    {
        private HisMediReactSumCreate CreateWorker
        {
            get
            {
                return (HisMediReactSumCreate)Worker.Get<HisMediReactSumCreate>();
            }
        }
        private HisMediReactSumUpdate UpdateWorker
        {
            get
            {
                return (HisMediReactSumUpdate)Worker.Get<HisMediReactSumUpdate>();
            }
        }
        private HisMediReactSumDelete DeleteWorker
        {
            get
            {
                return (HisMediReactSumDelete)Worker.Get<HisMediReactSumDelete>();
            }
        }
        private HisMediReactSumTruncate TruncateWorker
        {
            get
            {
                return (HisMediReactSumTruncate)Worker.Get<HisMediReactSumTruncate>();
            }
        }
        private HisMediReactSumGet GetWorker
        {
            get
            {
                return (HisMediReactSumGet)Worker.Get<HisMediReactSumGet>();
            }
        }
        private HisMediReactSumCheck CheckWorker
        {
            get
            {
                return (HisMediReactSumCheck)Worker.Get<HisMediReactSumCheck>();
            }
        }

        public bool Create(HIS_MEDI_REACT_SUM data)
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

        public bool CreateList(List<HIS_MEDI_REACT_SUM> listData)
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

        public bool Update(HIS_MEDI_REACT_SUM data)
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

        public bool UpdateList(List<HIS_MEDI_REACT_SUM> listData)
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

        public bool Delete(HIS_MEDI_REACT_SUM data)
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

        public bool DeleteList(List<HIS_MEDI_REACT_SUM> listData)
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

        public bool Truncate(HIS_MEDI_REACT_SUM data)
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

        public bool TruncateList(List<HIS_MEDI_REACT_SUM> listData)
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

        public List<HIS_MEDI_REACT_SUM> Get(HisMediReactSumSO search, CommonParam param)
        {
            List<HIS_MEDI_REACT_SUM> result = new List<HIS_MEDI_REACT_SUM>();
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

        public HIS_MEDI_REACT_SUM GetById(long id, HisMediReactSumSO search)
        {
            HIS_MEDI_REACT_SUM result = null;
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
