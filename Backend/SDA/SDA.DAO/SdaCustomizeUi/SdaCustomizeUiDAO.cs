using SDA.DAO.StagingObject;
using SDA.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.DAO.SdaCustomizeUi
{
    public partial class SdaCustomizeUiDAO : EntityBase
    {
        private SdaCustomizeUiCreate CreateWorker
        {
            get
            {
                return (SdaCustomizeUiCreate)Worker.Get<SdaCustomizeUiCreate>();
            }
        }
        private SdaCustomizeUiUpdate UpdateWorker
        {
            get
            {
                return (SdaCustomizeUiUpdate)Worker.Get<SdaCustomizeUiUpdate>();
            }
        }
        private SdaCustomizeUiDelete DeleteWorker
        {
            get
            {
                return (SdaCustomizeUiDelete)Worker.Get<SdaCustomizeUiDelete>();
            }
        }
        private SdaCustomizeUiTruncate TruncateWorker
        {
            get
            {
                return (SdaCustomizeUiTruncate)Worker.Get<SdaCustomizeUiTruncate>();
            }
        }
        private SdaCustomizeUiGet GetWorker
        {
            get
            {
                return (SdaCustomizeUiGet)Worker.Get<SdaCustomizeUiGet>();
            }
        }
        private SdaCustomizeUiCheck CheckWorker
        {
            get
            {
                return (SdaCustomizeUiCheck)Worker.Get<SdaCustomizeUiCheck>();
            }
        }

        public bool Create(SDA_CUSTOMIZE_UI data)
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

        public bool CreateList(List<SDA_CUSTOMIZE_UI> listData)
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

        public bool Update(SDA_CUSTOMIZE_UI data)
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

        public bool UpdateList(List<SDA_CUSTOMIZE_UI> listData)
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

        public bool Delete(SDA_CUSTOMIZE_UI data)
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

        public bool DeleteList(List<SDA_CUSTOMIZE_UI> listData)
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

        public bool Truncate(SDA_CUSTOMIZE_UI data)
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

        public bool TruncateList(List<SDA_CUSTOMIZE_UI> listData)
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

        public List<SDA_CUSTOMIZE_UI> Get(SdaCustomizeUiSO search, CommonParam param)
        {
            List<SDA_CUSTOMIZE_UI> result = new List<SDA_CUSTOMIZE_UI>();
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

        public SDA_CUSTOMIZE_UI GetById(long id, SdaCustomizeUiSO search)
        {
            SDA_CUSTOMIZE_UI result = null;
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
