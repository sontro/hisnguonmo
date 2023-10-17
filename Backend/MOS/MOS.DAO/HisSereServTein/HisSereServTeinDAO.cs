using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisSereServTein
{
    public partial class HisSereServTeinDAO : EntityBase
    {
        private HisSereServTeinCreate CreateWorker
        {
            get
            {
                return (HisSereServTeinCreate)Worker.Get<HisSereServTeinCreate>();
            }
        }
        private HisSereServTeinUpdate UpdateWorker
        {
            get
            {
                return (HisSereServTeinUpdate)Worker.Get<HisSereServTeinUpdate>();
            }
        }
        private HisSereServTeinDelete DeleteWorker
        {
            get
            {
                return (HisSereServTeinDelete)Worker.Get<HisSereServTeinDelete>();
            }
        }
        private HisSereServTeinTruncate TruncateWorker
        {
            get
            {
                return (HisSereServTeinTruncate)Worker.Get<HisSereServTeinTruncate>();
            }
        }
        private HisSereServTeinGet GetWorker
        {
            get
            {
                return (HisSereServTeinGet)Worker.Get<HisSereServTeinGet>();
            }
        }
        private HisSereServTeinCheck CheckWorker
        {
            get
            {
                return (HisSereServTeinCheck)Worker.Get<HisSereServTeinCheck>();
            }
        }

        public bool Create(HIS_SERE_SERV_TEIN data)
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

        public bool CreateList(List<HIS_SERE_SERV_TEIN> listData)
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

        public bool Update(HIS_SERE_SERV_TEIN data)
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

        public bool UpdateList(List<HIS_SERE_SERV_TEIN> listData)
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

        public bool Delete(HIS_SERE_SERV_TEIN data)
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

        public bool DeleteList(List<HIS_SERE_SERV_TEIN> listData)
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

        public bool Truncate(HIS_SERE_SERV_TEIN data)
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

        public bool TruncateList(List<HIS_SERE_SERV_TEIN> listData)
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

        public List<HIS_SERE_SERV_TEIN> Get(HisSereServTeinSO search, CommonParam param)
        {
            List<HIS_SERE_SERV_TEIN> result = new List<HIS_SERE_SERV_TEIN>();
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

        public HIS_SERE_SERV_TEIN GetById(long id, HisSereServTeinSO search)
        {
            HIS_SERE_SERV_TEIN result = null;
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
