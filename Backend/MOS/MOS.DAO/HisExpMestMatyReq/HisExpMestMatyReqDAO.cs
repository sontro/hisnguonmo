using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisExpMestMatyReq
{
    public partial class HisExpMestMatyReqDAO : EntityBase
    {
        private HisExpMestMatyReqCreate CreateWorker
        {
            get
            {
                return (HisExpMestMatyReqCreate)Worker.Get<HisExpMestMatyReqCreate>();
            }
        }
        private HisExpMestMatyReqUpdate UpdateWorker
        {
            get
            {
                return (HisExpMestMatyReqUpdate)Worker.Get<HisExpMestMatyReqUpdate>();
            }
        }
        private HisExpMestMatyReqDelete DeleteWorker
        {
            get
            {
                return (HisExpMestMatyReqDelete)Worker.Get<HisExpMestMatyReqDelete>();
            }
        }
        private HisExpMestMatyReqTruncate TruncateWorker
        {
            get
            {
                return (HisExpMestMatyReqTruncate)Worker.Get<HisExpMestMatyReqTruncate>();
            }
        }
        private HisExpMestMatyReqGet GetWorker
        {
            get
            {
                return (HisExpMestMatyReqGet)Worker.Get<HisExpMestMatyReqGet>();
            }
        }
        private HisExpMestMatyReqCheck CheckWorker
        {
            get
            {
                return (HisExpMestMatyReqCheck)Worker.Get<HisExpMestMatyReqCheck>();
            }
        }

        public bool Create(HIS_EXP_MEST_MATY_REQ data)
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

        public bool CreateList(List<HIS_EXP_MEST_MATY_REQ> listData)
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

        public bool Update(HIS_EXP_MEST_MATY_REQ data)
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

        public bool UpdateList(List<HIS_EXP_MEST_MATY_REQ> listData)
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

        public bool Delete(HIS_EXP_MEST_MATY_REQ data)
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

        public bool DeleteList(List<HIS_EXP_MEST_MATY_REQ> listData)
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

        public bool Truncate(HIS_EXP_MEST_MATY_REQ data)
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

        public bool TruncateList(List<HIS_EXP_MEST_MATY_REQ> listData)
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

        public List<HIS_EXP_MEST_MATY_REQ> Get(HisExpMestMatyReqSO search, CommonParam param)
        {
            List<HIS_EXP_MEST_MATY_REQ> result = new List<HIS_EXP_MEST_MATY_REQ>();
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

        public HIS_EXP_MEST_MATY_REQ GetById(long id, HisExpMestMatyReqSO search)
        {
            HIS_EXP_MEST_MATY_REQ result = null;
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
