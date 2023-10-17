using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisImpMestPropose
{
    public partial class HisImpMestProposeDAO : EntityBase
    {
        private HisImpMestProposeCreate CreateWorker
        {
            get
            {
                return (HisImpMestProposeCreate)Worker.Get<HisImpMestProposeCreate>();
            }
        }
        private HisImpMestProposeUpdate UpdateWorker
        {
            get
            {
                return (HisImpMestProposeUpdate)Worker.Get<HisImpMestProposeUpdate>();
            }
        }
        private HisImpMestProposeDelete DeleteWorker
        {
            get
            {
                return (HisImpMestProposeDelete)Worker.Get<HisImpMestProposeDelete>();
            }
        }
        private HisImpMestProposeTruncate TruncateWorker
        {
            get
            {
                return (HisImpMestProposeTruncate)Worker.Get<HisImpMestProposeTruncate>();
            }
        }
        private HisImpMestProposeGet GetWorker
        {
            get
            {
                return (HisImpMestProposeGet)Worker.Get<HisImpMestProposeGet>();
            }
        }
        private HisImpMestProposeCheck CheckWorker
        {
            get
            {
                return (HisImpMestProposeCheck)Worker.Get<HisImpMestProposeCheck>();
            }
        }

        public bool Create(HIS_IMP_MEST_PROPOSE data)
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

        public bool CreateList(List<HIS_IMP_MEST_PROPOSE> listData)
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

        public bool Update(HIS_IMP_MEST_PROPOSE data)
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

        public bool UpdateList(List<HIS_IMP_MEST_PROPOSE> listData)
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

        public bool Delete(HIS_IMP_MEST_PROPOSE data)
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

        public bool DeleteList(List<HIS_IMP_MEST_PROPOSE> listData)
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

        public bool Truncate(HIS_IMP_MEST_PROPOSE data)
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

        public bool TruncateList(List<HIS_IMP_MEST_PROPOSE> listData)
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

        public List<HIS_IMP_MEST_PROPOSE> Get(HisImpMestProposeSO search, CommonParam param)
        {
            List<HIS_IMP_MEST_PROPOSE> result = new List<HIS_IMP_MEST_PROPOSE>();
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

        public HIS_IMP_MEST_PROPOSE GetById(long id, HisImpMestProposeSO search)
        {
            HIS_IMP_MEST_PROPOSE result = null;
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
