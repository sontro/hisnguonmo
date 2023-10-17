using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisImpMestPay
{
    public partial class HisImpMestPayDAO : EntityBase
    {
        private HisImpMestPayCreate CreateWorker
        {
            get
            {
                return (HisImpMestPayCreate)Worker.Get<HisImpMestPayCreate>();
            }
        }
        private HisImpMestPayUpdate UpdateWorker
        {
            get
            {
                return (HisImpMestPayUpdate)Worker.Get<HisImpMestPayUpdate>();
            }
        }
        private HisImpMestPayDelete DeleteWorker
        {
            get
            {
                return (HisImpMestPayDelete)Worker.Get<HisImpMestPayDelete>();
            }
        }
        private HisImpMestPayTruncate TruncateWorker
        {
            get
            {
                return (HisImpMestPayTruncate)Worker.Get<HisImpMestPayTruncate>();
            }
        }
        private HisImpMestPayGet GetWorker
        {
            get
            {
                return (HisImpMestPayGet)Worker.Get<HisImpMestPayGet>();
            }
        }
        private HisImpMestPayCheck CheckWorker
        {
            get
            {
                return (HisImpMestPayCheck)Worker.Get<HisImpMestPayCheck>();
            }
        }

        public bool Create(HIS_IMP_MEST_PAY data)
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

        public bool CreateList(List<HIS_IMP_MEST_PAY> listData)
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

        public bool Update(HIS_IMP_MEST_PAY data)
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

        public bool UpdateList(List<HIS_IMP_MEST_PAY> listData)
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

        public bool Delete(HIS_IMP_MEST_PAY data)
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

        public bool DeleteList(List<HIS_IMP_MEST_PAY> listData)
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

        public bool Truncate(HIS_IMP_MEST_PAY data)
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

        public bool TruncateList(List<HIS_IMP_MEST_PAY> listData)
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

        public List<HIS_IMP_MEST_PAY> Get(HisImpMestPaySO search, CommonParam param)
        {
            List<HIS_IMP_MEST_PAY> result = new List<HIS_IMP_MEST_PAY>();
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

        public HIS_IMP_MEST_PAY GetById(long id, HisImpMestPaySO search)
        {
            HIS_IMP_MEST_PAY result = null;
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
