using SAR.DAO.StagingObject;
using SAR.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SAR.DAO.SarPrintType
{
    public partial class SarPrintTypeDAO : EntityBase
    {
        private SarPrintTypeCreate CreateWorker
        {
            get
            {
                return (SarPrintTypeCreate)Worker.Get<SarPrintTypeCreate>();
            }
        }
        private SarPrintTypeUpdate UpdateWorker
        {
            get
            {
                return (SarPrintTypeUpdate)Worker.Get<SarPrintTypeUpdate>();
            }
        }
        private SarPrintTypeDelete DeleteWorker
        {
            get
            {
                return (SarPrintTypeDelete)Worker.Get<SarPrintTypeDelete>();
            }
        }
        private SarPrintTypeTruncate TruncateWorker
        {
            get
            {
                return (SarPrintTypeTruncate)Worker.Get<SarPrintTypeTruncate>();
            }
        }
        private SarPrintTypeGet GetWorker
        {
            get
            {
                return (SarPrintTypeGet)Worker.Get<SarPrintTypeGet>();
            }
        }
        private SarPrintTypeCheck CheckWorker
        {
            get
            {
                return (SarPrintTypeCheck)Worker.Get<SarPrintTypeCheck>();
            }
        }

        public bool Create(SAR_PRINT_TYPE data)
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

        public bool CreateList(List<SAR_PRINT_TYPE> listData)
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

        public bool Update(SAR_PRINT_TYPE data)
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

        public bool UpdateList(List<SAR_PRINT_TYPE> listData)
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

        public bool Delete(SAR_PRINT_TYPE data)
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

        public bool DeleteList(List<SAR_PRINT_TYPE> listData)
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

        public bool Truncate(SAR_PRINT_TYPE data)
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

        public bool TruncateList(List<SAR_PRINT_TYPE> listData)
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

        public List<SAR_PRINT_TYPE> Get(SarPrintTypeSO search, CommonParam param)
        {
            List<SAR_PRINT_TYPE> result = new List<SAR_PRINT_TYPE>();
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

        public SAR_PRINT_TYPE GetById(long id, SarPrintTypeSO search)
        {
            SAR_PRINT_TYPE result = null;
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
