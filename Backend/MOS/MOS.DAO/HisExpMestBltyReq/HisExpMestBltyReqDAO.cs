using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisExpMestBltyReq
{
    public partial class HisExpMestBltyReqDAO : EntityBase
    {
        private HisExpMestBltyReqCreate CreateWorker
        {
            get
            {
                return (HisExpMestBltyReqCreate)Worker.Get<HisExpMestBltyReqCreate>();
            }
        }
        private HisExpMestBltyReqUpdate UpdateWorker
        {
            get
            {
                return (HisExpMestBltyReqUpdate)Worker.Get<HisExpMestBltyReqUpdate>();
            }
        }
        private HisExpMestBltyReqDelete DeleteWorker
        {
            get
            {
                return (HisExpMestBltyReqDelete)Worker.Get<HisExpMestBltyReqDelete>();
            }
        }
        private HisExpMestBltyReqTruncate TruncateWorker
        {
            get
            {
                return (HisExpMestBltyReqTruncate)Worker.Get<HisExpMestBltyReqTruncate>();
            }
        }
        private HisExpMestBltyReqGet GetWorker
        {
            get
            {
                return (HisExpMestBltyReqGet)Worker.Get<HisExpMestBltyReqGet>();
            }
        }
        private HisExpMestBltyReqCheck CheckWorker
        {
            get
            {
                return (HisExpMestBltyReqCheck)Worker.Get<HisExpMestBltyReqCheck>();
            }
        }

        public bool Create(HIS_EXP_MEST_BLTY_REQ data)
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

        public bool CreateList(List<HIS_EXP_MEST_BLTY_REQ> listData)
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

        public bool Update(HIS_EXP_MEST_BLTY_REQ data)
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

        public bool UpdateList(List<HIS_EXP_MEST_BLTY_REQ> listData)
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

        public bool Delete(HIS_EXP_MEST_BLTY_REQ data)
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

        public bool DeleteList(List<HIS_EXP_MEST_BLTY_REQ> listData)
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

        public bool Truncate(HIS_EXP_MEST_BLTY_REQ data)
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

        public bool TruncateList(List<HIS_EXP_MEST_BLTY_REQ> listData)
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

        public List<HIS_EXP_MEST_BLTY_REQ> Get(HisExpMestBltyReqSO search, CommonParam param)
        {
            List<HIS_EXP_MEST_BLTY_REQ> result = new List<HIS_EXP_MEST_BLTY_REQ>();
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

        public HIS_EXP_MEST_BLTY_REQ GetById(long id, HisExpMestBltyReqSO search)
        {
            HIS_EXP_MEST_BLTY_REQ result = null;
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
