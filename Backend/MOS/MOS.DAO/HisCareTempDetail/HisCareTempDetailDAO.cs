using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisCareTempDetail
{
    public partial class HisCareTempDetailDAO : EntityBase
    {
        private HisCareTempDetailCreate CreateWorker
        {
            get
            {
                return (HisCareTempDetailCreate)Worker.Get<HisCareTempDetailCreate>();
            }
        }
        private HisCareTempDetailUpdate UpdateWorker
        {
            get
            {
                return (HisCareTempDetailUpdate)Worker.Get<HisCareTempDetailUpdate>();
            }
        }
        private HisCareTempDetailDelete DeleteWorker
        {
            get
            {
                return (HisCareTempDetailDelete)Worker.Get<HisCareTempDetailDelete>();
            }
        }
        private HisCareTempDetailTruncate TruncateWorker
        {
            get
            {
                return (HisCareTempDetailTruncate)Worker.Get<HisCareTempDetailTruncate>();
            }
        }
        private HisCareTempDetailGet GetWorker
        {
            get
            {
                return (HisCareTempDetailGet)Worker.Get<HisCareTempDetailGet>();
            }
        }
        private HisCareTempDetailCheck CheckWorker
        {
            get
            {
                return (HisCareTempDetailCheck)Worker.Get<HisCareTempDetailCheck>();
            }
        }

        public bool Create(HIS_CARE_TEMP_DETAIL data)
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

        public bool CreateList(List<HIS_CARE_TEMP_DETAIL> listData)
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

        public bool Update(HIS_CARE_TEMP_DETAIL data)
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

        public bool UpdateList(List<HIS_CARE_TEMP_DETAIL> listData)
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

        public bool Delete(HIS_CARE_TEMP_DETAIL data)
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

        public bool DeleteList(List<HIS_CARE_TEMP_DETAIL> listData)
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

        public bool Truncate(HIS_CARE_TEMP_DETAIL data)
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

        public bool TruncateList(List<HIS_CARE_TEMP_DETAIL> listData)
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

        public List<HIS_CARE_TEMP_DETAIL> Get(HisCareTempDetailSO search, CommonParam param)
        {
            List<HIS_CARE_TEMP_DETAIL> result = new List<HIS_CARE_TEMP_DETAIL>();
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

        public HIS_CARE_TEMP_DETAIL GetById(long id, HisCareTempDetailSO search)
        {
            HIS_CARE_TEMP_DETAIL result = null;
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
