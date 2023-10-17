using SDA.DAO.StagingObject;
using SDA.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.DAO.SdaEventLog
{
    public partial class SdaEventLogDAO : EntityBase
    {
        private SdaEventLogCreate CreateWorker
        {
            get
            {
                return (SdaEventLogCreate)Worker.Get<SdaEventLogCreate>();
            }
        }
        private SdaEventLogUpdate UpdateWorker
        {
            get
            {
                return (SdaEventLogUpdate)Worker.Get<SdaEventLogUpdate>();
            }
        }
        private SdaEventLogDelete DeleteWorker
        {
            get
            {
                return (SdaEventLogDelete)Worker.Get<SdaEventLogDelete>();
            }
        }
        private SdaEventLogTruncate TruncateWorker
        {
            get
            {
                return (SdaEventLogTruncate)Worker.Get<SdaEventLogTruncate>();
            }
        }
        private SdaEventLogGet GetWorker
        {
            get
            {
                return (SdaEventLogGet)Worker.Get<SdaEventLogGet>();
            }
        }
        private SdaEventLogCheck CheckWorker
        {
            get
            {
                return (SdaEventLogCheck)Worker.Get<SdaEventLogCheck>();
            }
        }

        public bool Create(SDA_EVENT_LOG data)
        {
            bool result = false;
            
            #region Logging Input Data
            try
            {
                Input = Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => data), data);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            #endregion

            try
            {
                result = CreateWorker.Create(data);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }

            #region Logging
            if (!result)
            {
                LogInOut(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => result), result) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => data), data));
            }
            #endregion

            return result;
        }

        public bool CreateList(List<SDA_EVENT_LOG> listData)
        {
            bool result = false;

            #region Logging Input Data
            try
            {
                Input = Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => listData), listData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            #endregion

            try
            {
                result = CreateWorker.CreateList(listData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }

            #region Logging
            if (!result)
            {
                LogInOut(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => result), result) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => listData), listData));
            }
            #endregion

            return result;
        }

        public bool Update(SDA_EVENT_LOG data)
        {
            bool result = false;
            
            #region Logging Input Data
            try
            {
                Input = Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => data), data);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            #endregion

            try
            {
                result = UpdateWorker.Update(data);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }

            #region Logging
            if (!result)
            {
                LogInOut(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => result), result) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => data), data));
            }
            #endregion

            return result;
        }

        public bool UpdateList(List<SDA_EVENT_LOG> listData)
        {
            bool result = false;

            #region Logging Input Data
            try
            {
                Input = Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => listData), listData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            #endregion

            try
            {
                result = UpdateWorker.UpdateList(listData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }

            #region Logging
            if (!result)
            {
                LogInOut(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => result), result) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => listData), listData));
            }
            #endregion

            return result;
        }

        public bool Delete(SDA_EVENT_LOG data)
        {
            bool result = false;
            
            #region Logging Input Data
            try
            {
                Input = Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => data), data);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            #endregion

            try
            {
                result = DeleteWorker.Delete(data);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }

            #region Logging
            if (!result)
            {
                LogInOut(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => result), result) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => data), data));
            }
            #endregion

            return result;
        }

        public bool DeleteList(List<SDA_EVENT_LOG> listData)
        {
            bool result = false;

            #region Logging Input Data
            try
            {
                Input = Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => listData), listData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            #endregion

            try
            {
                result = DeleteWorker.DeleteList(listData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }

            #region Logging
            if (!result)
            {
                LogInOut(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => result), result) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => listData), listData));
            }
            #endregion

            return result;
        }

        public bool Truncate(SDA_EVENT_LOG data)
        {
            bool result = false;
            
            #region Logging Input Data
            try
            {
                Input = Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => data), data);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            #endregion

            try
            {
                result = TruncateWorker.Truncate(data);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }

            #region Logging
            if (!result)
            {
                LogInOut(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => result), result) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => data), data));
            }
            #endregion

            return result;
        }

        public bool TruncateList(List<SDA_EVENT_LOG> listData)
        {
            bool result = false;

            #region Logging Input Data
            try
            {
                Input = Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => listData), listData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            #endregion

            try
            {
                result = TruncateWorker.TruncateList(listData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }

            #region Logging
            if (!result)
            {
                LogInOut(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => result), result) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => listData), listData));
            }
            #endregion

            return result;
        }

        public List<SDA_EVENT_LOG> Get(SdaEventLogSO search, CommonParam param)
        {
            List<SDA_EVENT_LOG> result = new List<SDA_EVENT_LOG>();

            #region Logging Input Data
            try
            {
                Input = Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => param), param) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => search), search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            #endregion

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

            #region Logging
            if (param.HasException)
            {
                LogInOut();
            }
            #endregion

            return result;
        }

        public SDA_EVENT_LOG GetById(long id, SdaEventLogSO search)
        {
            SDA_EVENT_LOG result = null;

            #region Logging Input Data
            try
            {
                Input = Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => id), id) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => search), search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            #endregion

            try
            {
                result = GetWorker.GetById(id, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
                LogInOut();
            }

            return result;
        }

        public bool IsUnLock(long id)
        {
            #region Logging Input Data
            try
            {
                Input = Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => id), id);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            #endregion

            try
            {
                return CheckWorker.IsUnLock(id);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                LogInOut();
                throw;
            }
        }
    }
}
