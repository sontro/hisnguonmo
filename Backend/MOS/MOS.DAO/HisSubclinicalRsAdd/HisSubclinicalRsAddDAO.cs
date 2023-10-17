using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisSubclinicalRsAdd
{
    public partial class HisSubclinicalRsAddDAO : EntityBase
    {
        private HisSubclinicalRsAddCreate CreateWorker
        {
            get
            {
                return (HisSubclinicalRsAddCreate)Worker.Get<HisSubclinicalRsAddCreate>();
            }
        }
        private HisSubclinicalRsAddUpdate UpdateWorker
        {
            get
            {
                return (HisSubclinicalRsAddUpdate)Worker.Get<HisSubclinicalRsAddUpdate>();
            }
        }
        private HisSubclinicalRsAddDelete DeleteWorker
        {
            get
            {
                return (HisSubclinicalRsAddDelete)Worker.Get<HisSubclinicalRsAddDelete>();
            }
        }
        private HisSubclinicalRsAddTruncate TruncateWorker
        {
            get
            {
                return (HisSubclinicalRsAddTruncate)Worker.Get<HisSubclinicalRsAddTruncate>();
            }
        }
        private HisSubclinicalRsAddGet GetWorker
        {
            get
            {
                return (HisSubclinicalRsAddGet)Worker.Get<HisSubclinicalRsAddGet>();
            }
        }
        private HisSubclinicalRsAddCheck CheckWorker
        {
            get
            {
                return (HisSubclinicalRsAddCheck)Worker.Get<HisSubclinicalRsAddCheck>();
            }
        }

        public bool Create(HIS_SUBCLINICAL_RS_ADD data)
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

        public bool CreateList(List<HIS_SUBCLINICAL_RS_ADD> listData)
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

        public bool Update(HIS_SUBCLINICAL_RS_ADD data)
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

        public bool UpdateList(List<HIS_SUBCLINICAL_RS_ADD> listData)
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

        public bool Delete(HIS_SUBCLINICAL_RS_ADD data)
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

        public bool DeleteList(List<HIS_SUBCLINICAL_RS_ADD> listData)
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

        public bool Truncate(HIS_SUBCLINICAL_RS_ADD data)
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

        public bool TruncateList(List<HIS_SUBCLINICAL_RS_ADD> listData)
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

        public List<HIS_SUBCLINICAL_RS_ADD> Get(HisSubclinicalRsAddSO search, CommonParam param)
        {
            List<HIS_SUBCLINICAL_RS_ADD> result = new List<HIS_SUBCLINICAL_RS_ADD>();
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

        public HIS_SUBCLINICAL_RS_ADD GetById(long id, HisSubclinicalRsAddSO search)
        {
            HIS_SUBCLINICAL_RS_ADD result = null;
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
