using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisKskUneiVaty
{
    public partial class HisKskUneiVatyDAO : EntityBase
    {
        private HisKskUneiVatyCreate CreateWorker
        {
            get
            {
                return (HisKskUneiVatyCreate)Worker.Get<HisKskUneiVatyCreate>();
            }
        }
        private HisKskUneiVatyUpdate UpdateWorker
        {
            get
            {
                return (HisKskUneiVatyUpdate)Worker.Get<HisKskUneiVatyUpdate>();
            }
        }
        private HisKskUneiVatyDelete DeleteWorker
        {
            get
            {
                return (HisKskUneiVatyDelete)Worker.Get<HisKskUneiVatyDelete>();
            }
        }
        private HisKskUneiVatyTruncate TruncateWorker
        {
            get
            {
                return (HisKskUneiVatyTruncate)Worker.Get<HisKskUneiVatyTruncate>();
            }
        }
        private HisKskUneiVatyGet GetWorker
        {
            get
            {
                return (HisKskUneiVatyGet)Worker.Get<HisKskUneiVatyGet>();
            }
        }
        private HisKskUneiVatyCheck CheckWorker
        {
            get
            {
                return (HisKskUneiVatyCheck)Worker.Get<HisKskUneiVatyCheck>();
            }
        }

        public bool Create(HIS_KSK_UNEI_VATY data)
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

        public bool CreateList(List<HIS_KSK_UNEI_VATY> listData)
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

        public bool Update(HIS_KSK_UNEI_VATY data)
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

        public bool UpdateList(List<HIS_KSK_UNEI_VATY> listData)
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

        public bool Delete(HIS_KSK_UNEI_VATY data)
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

        public bool DeleteList(List<HIS_KSK_UNEI_VATY> listData)
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

        public bool Truncate(HIS_KSK_UNEI_VATY data)
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

        public bool TruncateList(List<HIS_KSK_UNEI_VATY> listData)
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

        public List<HIS_KSK_UNEI_VATY> Get(HisKskUneiVatySO search, CommonParam param)
        {
            List<HIS_KSK_UNEI_VATY> result = new List<HIS_KSK_UNEI_VATY>();
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

        public HIS_KSK_UNEI_VATY GetById(long id, HisKskUneiVatySO search)
        {
            HIS_KSK_UNEI_VATY result = null;
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
