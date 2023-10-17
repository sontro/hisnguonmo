using SDA.DAO.StagingObject;
using SDA.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.DAO.SdaCommuneMap
{
    public partial class SdaCommuneMapDAO : EntityBase
    {
        private SdaCommuneMapCreate CreateWorker
        {
            get
            {
                return (SdaCommuneMapCreate)Worker.Get<SdaCommuneMapCreate>();
            }
        }
        private SdaCommuneMapUpdate UpdateWorker
        {
            get
            {
                return (SdaCommuneMapUpdate)Worker.Get<SdaCommuneMapUpdate>();
            }
        }
        private SdaCommuneMapDelete DeleteWorker
        {
            get
            {
                return (SdaCommuneMapDelete)Worker.Get<SdaCommuneMapDelete>();
            }
        }
        private SdaCommuneMapTruncate TruncateWorker
        {
            get
            {
                return (SdaCommuneMapTruncate)Worker.Get<SdaCommuneMapTruncate>();
            }
        }
        private SdaCommuneMapGet GetWorker
        {
            get
            {
                return (SdaCommuneMapGet)Worker.Get<SdaCommuneMapGet>();
            }
        }
        private SdaCommuneMapCheck CheckWorker
        {
            get
            {
                return (SdaCommuneMapCheck)Worker.Get<SdaCommuneMapCheck>();
            }
        }

        public bool Create(SDA_COMMUNE_MAP data)
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

        public bool CreateList(List<SDA_COMMUNE_MAP> listData)
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

        public bool Update(SDA_COMMUNE_MAP data)
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

        public bool UpdateList(List<SDA_COMMUNE_MAP> listData)
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

        public bool Delete(SDA_COMMUNE_MAP data)
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

        public bool DeleteList(List<SDA_COMMUNE_MAP> listData)
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

        public bool Truncate(SDA_COMMUNE_MAP data)
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

        public bool TruncateList(List<SDA_COMMUNE_MAP> listData)
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

        public List<SDA_COMMUNE_MAP> Get(SdaCommuneMapSO search, CommonParam param)
        {
            List<SDA_COMMUNE_MAP> result = new List<SDA_COMMUNE_MAP>();
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

        public SDA_COMMUNE_MAP GetById(long id, SdaCommuneMapSO search)
        {
            SDA_COMMUNE_MAP result = null;
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
