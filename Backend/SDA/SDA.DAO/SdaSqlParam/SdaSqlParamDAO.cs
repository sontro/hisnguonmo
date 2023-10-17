using SDA.DAO.StagingObject;
using SDA.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.DAO.SdaSqlParam
{
    public partial class SdaSqlParamDAO : EntityBase
    {
        private SdaSqlParamCreate CreateWorker
        {
            get
            {
                return (SdaSqlParamCreate)Worker.Get<SdaSqlParamCreate>();
            }
        }
        private SdaSqlParamUpdate UpdateWorker
        {
            get
            {
                return (SdaSqlParamUpdate)Worker.Get<SdaSqlParamUpdate>();
            }
        }
        private SdaSqlParamDelete DeleteWorker
        {
            get
            {
                return (SdaSqlParamDelete)Worker.Get<SdaSqlParamDelete>();
            }
        }
        private SdaSqlParamTruncate TruncateWorker
        {
            get
            {
                return (SdaSqlParamTruncate)Worker.Get<SdaSqlParamTruncate>();
            }
        }
        private SdaSqlParamGet GetWorker
        {
            get
            {
                return (SdaSqlParamGet)Worker.Get<SdaSqlParamGet>();
            }
        }
        private SdaSqlParamCheck CheckWorker
        {
            get
            {
                return (SdaSqlParamCheck)Worker.Get<SdaSqlParamCheck>();
            }
        }

        public bool Create(SDA_SQL_PARAM data)
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

        public bool CreateList(List<SDA_SQL_PARAM> listData)
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

        public bool Update(SDA_SQL_PARAM data)
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

        public bool UpdateList(List<SDA_SQL_PARAM> listData)
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

        public bool Delete(SDA_SQL_PARAM data)
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

        public bool DeleteList(List<SDA_SQL_PARAM> listData)
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

        public bool Truncate(SDA_SQL_PARAM data)
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

        public bool TruncateList(List<SDA_SQL_PARAM> listData)
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

        public List<SDA_SQL_PARAM> Get(SdaSqlParamSO search, CommonParam param)
        {
            List<SDA_SQL_PARAM> result = new List<SDA_SQL_PARAM>();
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

        public SDA_SQL_PARAM GetById(long id, SdaSqlParamSO search)
        {
            SDA_SQL_PARAM result = null;
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
