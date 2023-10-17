using SAR.DAO.StagingObject;
using SAR.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SAR.DAO.SarReportTypeGroup
{
    public partial class SarReportTypeGroupDAO : EntityBase
    {
        private SarReportTypeGroupCreate CreateWorker
        {
            get
            {
                return (SarReportTypeGroupCreate)Worker.Get<SarReportTypeGroupCreate>();
            }
        }
        private SarReportTypeGroupUpdate UpdateWorker
        {
            get
            {
                return (SarReportTypeGroupUpdate)Worker.Get<SarReportTypeGroupUpdate>();
            }
        }
        private SarReportTypeGroupDelete DeleteWorker
        {
            get
            {
                return (SarReportTypeGroupDelete)Worker.Get<SarReportTypeGroupDelete>();
            }
        }
        private SarReportTypeGroupTruncate TruncateWorker
        {
            get
            {
                return (SarReportTypeGroupTruncate)Worker.Get<SarReportTypeGroupTruncate>();
            }
        }
        private SarReportTypeGroupGet GetWorker
        {
            get
            {
                return (SarReportTypeGroupGet)Worker.Get<SarReportTypeGroupGet>();
            }
        }
        private SarReportTypeGroupCheck CheckWorker
        {
            get
            {
                return (SarReportTypeGroupCheck)Worker.Get<SarReportTypeGroupCheck>();
            }
        }

        public bool Create(SAR_REPORT_TYPE_GROUP data)
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

        public bool CreateList(List<SAR_REPORT_TYPE_GROUP> listData)
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

        public bool Update(SAR_REPORT_TYPE_GROUP data)
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

        public bool UpdateList(List<SAR_REPORT_TYPE_GROUP> listData)
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

        public bool Delete(SAR_REPORT_TYPE_GROUP data)
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

        public bool DeleteList(List<SAR_REPORT_TYPE_GROUP> listData)
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

        public bool Truncate(SAR_REPORT_TYPE_GROUP data)
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

        public bool TruncateList(List<SAR_REPORT_TYPE_GROUP> listData)
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

        public List<SAR_REPORT_TYPE_GROUP> Get(SarReportTypeGroupSO search, CommonParam param)
        {
            List<SAR_REPORT_TYPE_GROUP> result = new List<SAR_REPORT_TYPE_GROUP>();
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

        public SAR_REPORT_TYPE_GROUP GetById(long id, SarReportTypeGroupSO search)
        {
            SAR_REPORT_TYPE_GROUP result = null;
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
