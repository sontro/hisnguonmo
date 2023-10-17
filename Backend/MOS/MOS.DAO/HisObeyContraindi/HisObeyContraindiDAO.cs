using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisObeyContraindi
{
    public partial class HisObeyContraindiDAO : EntityBase
    {
        private HisObeyContraindiCreate CreateWorker
        {
            get
            {
                return (HisObeyContraindiCreate)Worker.Get<HisObeyContraindiCreate>();
            }
        }
        private HisObeyContraindiUpdate UpdateWorker
        {
            get
            {
                return (HisObeyContraindiUpdate)Worker.Get<HisObeyContraindiUpdate>();
            }
        }
        private HisObeyContraindiDelete DeleteWorker
        {
            get
            {
                return (HisObeyContraindiDelete)Worker.Get<HisObeyContraindiDelete>();
            }
        }
        private HisObeyContraindiTruncate TruncateWorker
        {
            get
            {
                return (HisObeyContraindiTruncate)Worker.Get<HisObeyContraindiTruncate>();
            }
        }
        private HisObeyContraindiGet GetWorker
        {
            get
            {
                return (HisObeyContraindiGet)Worker.Get<HisObeyContraindiGet>();
            }
        }
        private HisObeyContraindiCheck CheckWorker
        {
            get
            {
                return (HisObeyContraindiCheck)Worker.Get<HisObeyContraindiCheck>();
            }
        }

        public bool Create(HIS_OBEY_CONTRAINDI data)
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

        public bool CreateList(List<HIS_OBEY_CONTRAINDI> listData)
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

        public bool Update(HIS_OBEY_CONTRAINDI data)
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

        public bool UpdateList(List<HIS_OBEY_CONTRAINDI> listData)
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

        public bool Delete(HIS_OBEY_CONTRAINDI data)
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

        public bool DeleteList(List<HIS_OBEY_CONTRAINDI> listData)
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

        public bool Truncate(HIS_OBEY_CONTRAINDI data)
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

        public bool TruncateList(List<HIS_OBEY_CONTRAINDI> listData)
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

        public List<HIS_OBEY_CONTRAINDI> Get(HisObeyContraindiSO search, CommonParam param)
        {
            List<HIS_OBEY_CONTRAINDI> result = new List<HIS_OBEY_CONTRAINDI>();
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

        public HIS_OBEY_CONTRAINDI GetById(long id, HisObeyContraindiSO search)
        {
            HIS_OBEY_CONTRAINDI result = null;
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
