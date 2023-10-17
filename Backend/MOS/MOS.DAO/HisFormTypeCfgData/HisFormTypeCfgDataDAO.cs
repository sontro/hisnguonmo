using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisFormTypeCfgData
{
    public partial class HisFormTypeCfgDataDAO : EntityBase
    {
        private HisFormTypeCfgDataCreate CreateWorker
        {
            get
            {
                return (HisFormTypeCfgDataCreate)Worker.Get<HisFormTypeCfgDataCreate>();
            }
        }
        private HisFormTypeCfgDataUpdate UpdateWorker
        {
            get
            {
                return (HisFormTypeCfgDataUpdate)Worker.Get<HisFormTypeCfgDataUpdate>();
            }
        }
        private HisFormTypeCfgDataDelete DeleteWorker
        {
            get
            {
                return (HisFormTypeCfgDataDelete)Worker.Get<HisFormTypeCfgDataDelete>();
            }
        }
        private HisFormTypeCfgDataTruncate TruncateWorker
        {
            get
            {
                return (HisFormTypeCfgDataTruncate)Worker.Get<HisFormTypeCfgDataTruncate>();
            }
        }
        private HisFormTypeCfgDataGet GetWorker
        {
            get
            {
                return (HisFormTypeCfgDataGet)Worker.Get<HisFormTypeCfgDataGet>();
            }
        }
        private HisFormTypeCfgDataCheck CheckWorker
        {
            get
            {
                return (HisFormTypeCfgDataCheck)Worker.Get<HisFormTypeCfgDataCheck>();
            }
        }

        public bool Create(HIS_FORM_TYPE_CFG_DATA data)
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

        public bool CreateList(List<HIS_FORM_TYPE_CFG_DATA> listData)
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

        public bool Update(HIS_FORM_TYPE_CFG_DATA data)
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

        public bool UpdateList(List<HIS_FORM_TYPE_CFG_DATA> listData)
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

        public bool Delete(HIS_FORM_TYPE_CFG_DATA data)
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

        public bool DeleteList(List<HIS_FORM_TYPE_CFG_DATA> listData)
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

        public bool Truncate(HIS_FORM_TYPE_CFG_DATA data)
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

        public bool TruncateList(List<HIS_FORM_TYPE_CFG_DATA> listData)
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

        public List<HIS_FORM_TYPE_CFG_DATA> Get(HisFormTypeCfgDataSO search, CommonParam param)
        {
            List<HIS_FORM_TYPE_CFG_DATA> result = new List<HIS_FORM_TYPE_CFG_DATA>();
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

        public HIS_FORM_TYPE_CFG_DATA GetById(long id, HisFormTypeCfgDataSO search)
        {
            HIS_FORM_TYPE_CFG_DATA result = null;
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
