using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisEyeSurgryDesc
{
    public partial class HisEyeSurgryDescDAO : EntityBase
    {
        private HisEyeSurgryDescCreate CreateWorker
        {
            get
            {
                return (HisEyeSurgryDescCreate)Worker.Get<HisEyeSurgryDescCreate>();
            }
        }
        private HisEyeSurgryDescUpdate UpdateWorker
        {
            get
            {
                return (HisEyeSurgryDescUpdate)Worker.Get<HisEyeSurgryDescUpdate>();
            }
        }
        private HisEyeSurgryDescDelete DeleteWorker
        {
            get
            {
                return (HisEyeSurgryDescDelete)Worker.Get<HisEyeSurgryDescDelete>();
            }
        }
        private HisEyeSurgryDescTruncate TruncateWorker
        {
            get
            {
                return (HisEyeSurgryDescTruncate)Worker.Get<HisEyeSurgryDescTruncate>();
            }
        }
        private HisEyeSurgryDescGet GetWorker
        {
            get
            {
                return (HisEyeSurgryDescGet)Worker.Get<HisEyeSurgryDescGet>();
            }
        }
        private HisEyeSurgryDescCheck CheckWorker
        {
            get
            {
                return (HisEyeSurgryDescCheck)Worker.Get<HisEyeSurgryDescCheck>();
            }
        }

        public bool Create(HIS_EYE_SURGRY_DESC data)
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

        public bool CreateList(List<HIS_EYE_SURGRY_DESC> listData)
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

        public bool Update(HIS_EYE_SURGRY_DESC data)
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

        public bool UpdateList(List<HIS_EYE_SURGRY_DESC> listData)
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

        public bool Delete(HIS_EYE_SURGRY_DESC data)
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

        public bool DeleteList(List<HIS_EYE_SURGRY_DESC> listData)
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

        public bool Truncate(HIS_EYE_SURGRY_DESC data)
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

        public bool TruncateList(List<HIS_EYE_SURGRY_DESC> listData)
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

        public List<HIS_EYE_SURGRY_DESC> Get(HisEyeSurgryDescSO search, CommonParam param)
        {
            List<HIS_EYE_SURGRY_DESC> result = new List<HIS_EYE_SURGRY_DESC>();
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

        public HIS_EYE_SURGRY_DESC GetById(long id, HisEyeSurgryDescSO search)
        {
            HIS_EYE_SURGRY_DESC result = null;
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
