using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisExpMestMetyReq
{
    public partial class HisExpMestMetyReqDAO : EntityBase
    {
        private HisExpMestMetyReqCreate CreateWorker
        {
            get
            {
                return (HisExpMestMetyReqCreate)Worker.Get<HisExpMestMetyReqCreate>();
            }
        }
        private HisExpMestMetyReqUpdate UpdateWorker
        {
            get
            {
                return (HisExpMestMetyReqUpdate)Worker.Get<HisExpMestMetyReqUpdate>();
            }
        }
        private HisExpMestMetyReqDelete DeleteWorker
        {
            get
            {
                return (HisExpMestMetyReqDelete)Worker.Get<HisExpMestMetyReqDelete>();
            }
        }
        private HisExpMestMetyReqTruncate TruncateWorker
        {
            get
            {
                return (HisExpMestMetyReqTruncate)Worker.Get<HisExpMestMetyReqTruncate>();
            }
        }
        private HisExpMestMetyReqGet GetWorker
        {
            get
            {
                return (HisExpMestMetyReqGet)Worker.Get<HisExpMestMetyReqGet>();
            }
        }
        private HisExpMestMetyReqCheck CheckWorker
        {
            get
            {
                return (HisExpMestMetyReqCheck)Worker.Get<HisExpMestMetyReqCheck>();
            }
        }

        public bool Create(HIS_EXP_MEST_METY_REQ data)
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

        public bool CreateList(List<HIS_EXP_MEST_METY_REQ> listData)
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

        public bool Update(HIS_EXP_MEST_METY_REQ data)
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

        public bool UpdateList(List<HIS_EXP_MEST_METY_REQ> listData)
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

        public bool Delete(HIS_EXP_MEST_METY_REQ data)
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

        public bool DeleteList(List<HIS_EXP_MEST_METY_REQ> listData)
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

        public bool Truncate(HIS_EXP_MEST_METY_REQ data)
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

        public bool TruncateList(List<HIS_EXP_MEST_METY_REQ> listData)
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

        public List<HIS_EXP_MEST_METY_REQ> Get(HisExpMestMetyReqSO search, CommonParam param)
        {
            List<HIS_EXP_MEST_METY_REQ> result = new List<HIS_EXP_MEST_METY_REQ>();
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

        public HIS_EXP_MEST_METY_REQ GetById(long id, HisExpMestMetyReqSO search)
        {
            HIS_EXP_MEST_METY_REQ result = null;
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
