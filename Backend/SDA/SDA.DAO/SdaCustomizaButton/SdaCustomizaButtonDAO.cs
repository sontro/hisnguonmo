using SDA.DAO.StagingObject;
using SDA.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.DAO.SdaCustomizeButton
{
    public partial class SdaCustomizeButtonDAO : EntityBase
    {
        private SdaCustomizeButtonCreate CreateWorker
        {
            get
            {
                return (SdaCustomizeButtonCreate)Worker.Get<SdaCustomizeButtonCreate>();
            }
        }
        private SdaCustomizeButtonUpdate UpdateWorker
        {
            get
            {
                return (SdaCustomizeButtonUpdate)Worker.Get<SdaCustomizeButtonUpdate>();
            }
        }
        private SdaCustomizeButtonDelete DeleteWorker
        {
            get
            {
                return (SdaCustomizeButtonDelete)Worker.Get<SdaCustomizeButtonDelete>();
            }
        }
        private SdaCustomizeButtonTruncate TruncateWorker
        {
            get
            {
                return (SdaCustomizeButtonTruncate)Worker.Get<SdaCustomizeButtonTruncate>();
            }
        }
        private SdaCustomizeButtonGet GetWorker
        {
            get
            {
                return (SdaCustomizeButtonGet)Worker.Get<SdaCustomizeButtonGet>();
            }
        }
        private SdaCustomizeButtonCheck CheckWorker
        {
            get
            {
                return (SdaCustomizeButtonCheck)Worker.Get<SdaCustomizeButtonCheck>();
            }
        }

        public bool Create(SDA_CUSTOMIZE_BUTTON data)
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

        public bool CreateList(List<SDA_CUSTOMIZE_BUTTON> listData)
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

        public bool Update(SDA_CUSTOMIZE_BUTTON data)
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

        public bool UpdateList(List<SDA_CUSTOMIZE_BUTTON> listData)
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

        public bool Delete(SDA_CUSTOMIZE_BUTTON data)
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

        public bool DeleteList(List<SDA_CUSTOMIZE_BUTTON> listData)
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

        public bool Truncate(SDA_CUSTOMIZE_BUTTON data)
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

        public bool TruncateList(List<SDA_CUSTOMIZE_BUTTON> listData)
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

        public List<SDA_CUSTOMIZE_BUTTON> Get(SdaCustomizeButtonSO search, CommonParam param)
        {
            List<SDA_CUSTOMIZE_BUTTON> result = new List<SDA_CUSTOMIZE_BUTTON>();
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

        public SDA_CUSTOMIZE_BUTTON GetById(long id, SdaCustomizeButtonSO search)
        {
            SDA_CUSTOMIZE_BUTTON result = null;
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
