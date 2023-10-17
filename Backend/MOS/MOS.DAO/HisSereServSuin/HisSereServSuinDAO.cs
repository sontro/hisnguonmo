using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisSereServSuin
{
    public partial class HisSereServSuinDAO : EntityBase
    {
        private HisSereServSuinCreate CreateWorker
        {
            get
            {
                return (HisSereServSuinCreate)Worker.Get<HisSereServSuinCreate>();
            }
        }
        private HisSereServSuinUpdate UpdateWorker
        {
            get
            {
                return (HisSereServSuinUpdate)Worker.Get<HisSereServSuinUpdate>();
            }
        }
        private HisSereServSuinDelete DeleteWorker
        {
            get
            {
                return (HisSereServSuinDelete)Worker.Get<HisSereServSuinDelete>();
            }
        }
        private HisSereServSuinTruncate TruncateWorker
        {
            get
            {
                return (HisSereServSuinTruncate)Worker.Get<HisSereServSuinTruncate>();
            }
        }
        private HisSereServSuinGet GetWorker
        {
            get
            {
                return (HisSereServSuinGet)Worker.Get<HisSereServSuinGet>();
            }
        }
        private HisSereServSuinCheck CheckWorker
        {
            get
            {
                return (HisSereServSuinCheck)Worker.Get<HisSereServSuinCheck>();
            }
        }

        public bool Create(HIS_SERE_SERV_SUIN data)
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

        public bool CreateList(List<HIS_SERE_SERV_SUIN> listData)
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

        public bool Update(HIS_SERE_SERV_SUIN data)
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

        public bool UpdateList(List<HIS_SERE_SERV_SUIN> listData)
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

        public bool Delete(HIS_SERE_SERV_SUIN data)
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

        public bool DeleteList(List<HIS_SERE_SERV_SUIN> listData)
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

        public bool Truncate(HIS_SERE_SERV_SUIN data)
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

        public bool TruncateList(List<HIS_SERE_SERV_SUIN> listData)
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

        public List<HIS_SERE_SERV_SUIN> Get(HisSereServSuinSO search, CommonParam param)
        {
            List<HIS_SERE_SERV_SUIN> result = new List<HIS_SERE_SERV_SUIN>();
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

        public HIS_SERE_SERV_SUIN GetById(long id, HisSereServSuinSO search)
        {
            HIS_SERE_SERV_SUIN result = null;
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
