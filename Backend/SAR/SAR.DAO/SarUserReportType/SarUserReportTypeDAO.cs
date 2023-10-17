using SAR.DAO.StagingObject;
using SAR.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SAR.DAO.SarUserReportType
{
    public partial class SarUserReportTypeDAO : EntityBase
    {
        private SarUserReportTypeCreate CreateWorker
        {
            get
            {
                return (SarUserReportTypeCreate)Worker.Get<SarUserReportTypeCreate>();
            }
        }
        private SarUserReportTypeUpdate UpdateWorker
        {
            get
            {
                return (SarUserReportTypeUpdate)Worker.Get<SarUserReportTypeUpdate>();
            }
        }
        private SarUserReportTypeDelete DeleteWorker
        {
            get
            {
                return (SarUserReportTypeDelete)Worker.Get<SarUserReportTypeDelete>();
            }
        }
        private SarUserReportTypeTruncate TruncateWorker
        {
            get
            {
                return (SarUserReportTypeTruncate)Worker.Get<SarUserReportTypeTruncate>();
            }
        }
        private SarUserReportTypeGet GetWorker
        {
            get
            {
                return (SarUserReportTypeGet)Worker.Get<SarUserReportTypeGet>();
            }
        }
        private SarUserReportTypeCheck CheckWorker
        {
            get
            {
                return (SarUserReportTypeCheck)Worker.Get<SarUserReportTypeCheck>();
            }
        }

        public bool Create(SAR_USER_REPORT_TYPE data)
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

        public bool CreateList(List<SAR_USER_REPORT_TYPE> listData)
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

        public bool Update(SAR_USER_REPORT_TYPE data)
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

        public bool UpdateList(List<SAR_USER_REPORT_TYPE> listData)
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

        public bool Delete(SAR_USER_REPORT_TYPE data)
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

        public bool DeleteList(List<SAR_USER_REPORT_TYPE> listData)
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

        public bool Truncate(SAR_USER_REPORT_TYPE data)
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

        public bool TruncateList(List<SAR_USER_REPORT_TYPE> listData)
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

        public List<SAR_USER_REPORT_TYPE> Get(SarUserReportTypeSO search, CommonParam param)
        {
            List<SAR_USER_REPORT_TYPE> result = new List<SAR_USER_REPORT_TYPE>();
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

        public SAR_USER_REPORT_TYPE GetById(long id, SarUserReportTypeSO search)
        {
            SAR_USER_REPORT_TYPE result = null;
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
