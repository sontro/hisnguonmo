using SAR.DAO.StagingObject;
using SAR.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SAR.DAO.SarRetyFofi
{
    public partial class SarRetyFofiDAO : EntityBase
    {
        private SarRetyFofiCreate CreateWorker
        {
            get
            {
                return (SarRetyFofiCreate)Worker.Get<SarRetyFofiCreate>();
            }
        }
        private SarRetyFofiUpdate UpdateWorker
        {
            get
            {
                return (SarRetyFofiUpdate)Worker.Get<SarRetyFofiUpdate>();
            }
        }
        private SarRetyFofiDelete DeleteWorker
        {
            get
            {
                return (SarRetyFofiDelete)Worker.Get<SarRetyFofiDelete>();
            }
        }
        private SarRetyFofiTruncate TruncateWorker
        {
            get
            {
                return (SarRetyFofiTruncate)Worker.Get<SarRetyFofiTruncate>();
            }
        }
        private SarRetyFofiGet GetWorker
        {
            get
            {
                return (SarRetyFofiGet)Worker.Get<SarRetyFofiGet>();
            }
        }
        private SarRetyFofiCheck CheckWorker
        {
            get
            {
                return (SarRetyFofiCheck)Worker.Get<SarRetyFofiCheck>();
            }
        }

        public bool Create(SAR_RETY_FOFI data)
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

        public bool CreateList(List<SAR_RETY_FOFI> listData)
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

        public bool Update(SAR_RETY_FOFI data)
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

        public bool UpdateList(List<SAR_RETY_FOFI> listData)
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

        public bool Delete(SAR_RETY_FOFI data)
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

        public bool DeleteList(List<SAR_RETY_FOFI> listData)
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

        public bool Truncate(SAR_RETY_FOFI data)
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

        public bool TruncateList(List<SAR_RETY_FOFI> listData)
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

        public List<SAR_RETY_FOFI> Get(SarRetyFofiSO search, CommonParam param)
        {
            List<SAR_RETY_FOFI> result = new List<SAR_RETY_FOFI>();
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

        public SAR_RETY_FOFI GetById(long id, SarRetyFofiSO search)
        {
            SAR_RETY_FOFI result = null;
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
