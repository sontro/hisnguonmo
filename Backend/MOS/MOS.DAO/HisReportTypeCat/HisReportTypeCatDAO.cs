using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisReportTypeCat
{
    public partial class HisReportTypeCatDAO : EntityBase
    {
        private HisReportTypeCatCreate CreateWorker
        {
            get
            {
                return (HisReportTypeCatCreate)Worker.Get<HisReportTypeCatCreate>();
            }
        }
        private HisReportTypeCatUpdate UpdateWorker
        {
            get
            {
                return (HisReportTypeCatUpdate)Worker.Get<HisReportTypeCatUpdate>();
            }
        }
        private HisReportTypeCatDelete DeleteWorker
        {
            get
            {
                return (HisReportTypeCatDelete)Worker.Get<HisReportTypeCatDelete>();
            }
        }
        private HisReportTypeCatTruncate TruncateWorker
        {
            get
            {
                return (HisReportTypeCatTruncate)Worker.Get<HisReportTypeCatTruncate>();
            }
        }
        private HisReportTypeCatGet GetWorker
        {
            get
            {
                return (HisReportTypeCatGet)Worker.Get<HisReportTypeCatGet>();
            }
        }
        private HisReportTypeCatCheck CheckWorker
        {
            get
            {
                return (HisReportTypeCatCheck)Worker.Get<HisReportTypeCatCheck>();
            }
        }

        public bool Create(HIS_REPORT_TYPE_CAT data)
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

        public bool CreateList(List<HIS_REPORT_TYPE_CAT> listData)
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

        public bool Update(HIS_REPORT_TYPE_CAT data)
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

        public bool UpdateList(List<HIS_REPORT_TYPE_CAT> listData)
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

        public bool Delete(HIS_REPORT_TYPE_CAT data)
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

        public bool DeleteList(List<HIS_REPORT_TYPE_CAT> listData)
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

        public bool Truncate(HIS_REPORT_TYPE_CAT data)
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

        public bool TruncateList(List<HIS_REPORT_TYPE_CAT> listData)
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

        public List<HIS_REPORT_TYPE_CAT> Get(HisReportTypeCatSO search, CommonParam param)
        {
            List<HIS_REPORT_TYPE_CAT> result = new List<HIS_REPORT_TYPE_CAT>();
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

        public HIS_REPORT_TYPE_CAT GetById(long id, HisReportTypeCatSO search)
        {
            HIS_REPORT_TYPE_CAT result = null;
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
