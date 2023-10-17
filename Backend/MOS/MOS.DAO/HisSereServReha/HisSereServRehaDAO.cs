using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisSereServReha
{
    public partial class HisSereServRehaDAO : EntityBase
    {
        private HisSereServRehaCreate CreateWorker
        {
            get
            {
                return (HisSereServRehaCreate)Worker.Get<HisSereServRehaCreate>();
            }
        }
        private HisSereServRehaUpdate UpdateWorker
        {
            get
            {
                return (HisSereServRehaUpdate)Worker.Get<HisSereServRehaUpdate>();
            }
        }
        private HisSereServRehaDelete DeleteWorker
        {
            get
            {
                return (HisSereServRehaDelete)Worker.Get<HisSereServRehaDelete>();
            }
        }
        private HisSereServRehaTruncate TruncateWorker
        {
            get
            {
                return (HisSereServRehaTruncate)Worker.Get<HisSereServRehaTruncate>();
            }
        }
        private HisSereServRehaGet GetWorker
        {
            get
            {
                return (HisSereServRehaGet)Worker.Get<HisSereServRehaGet>();
            }
        }
        private HisSereServRehaCheck CheckWorker
        {
            get
            {
                return (HisSereServRehaCheck)Worker.Get<HisSereServRehaCheck>();
            }
        }

        public bool Create(HIS_SERE_SERV_REHA data)
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

        public bool CreateList(List<HIS_SERE_SERV_REHA> listData)
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

        public bool Update(HIS_SERE_SERV_REHA data)
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

        public bool UpdateList(List<HIS_SERE_SERV_REHA> listData)
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

        public bool Delete(HIS_SERE_SERV_REHA data)
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

        public bool DeleteList(List<HIS_SERE_SERV_REHA> listData)
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

        public bool Truncate(HIS_SERE_SERV_REHA data)
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

        public bool TruncateList(List<HIS_SERE_SERV_REHA> listData)
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

        public List<HIS_SERE_SERV_REHA> Get(HisSereServRehaSO search, CommonParam param)
        {
            List<HIS_SERE_SERV_REHA> result = new List<HIS_SERE_SERV_REHA>();
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

        public HIS_SERE_SERV_REHA GetById(long id, HisSereServRehaSO search)
        {
            HIS_SERE_SERV_REHA result = null;
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
