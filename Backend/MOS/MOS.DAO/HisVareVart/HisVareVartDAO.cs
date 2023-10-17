using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisVareVart
{
    public partial class HisVareVartDAO : EntityBase
    {
        private HisVareVartCreate CreateWorker
        {
            get
            {
                return (HisVareVartCreate)Worker.Get<HisVareVartCreate>();
            }
        }
        private HisVareVartUpdate UpdateWorker
        {
            get
            {
                return (HisVareVartUpdate)Worker.Get<HisVareVartUpdate>();
            }
        }
        private HisVareVartDelete DeleteWorker
        {
            get
            {
                return (HisVareVartDelete)Worker.Get<HisVareVartDelete>();
            }
        }
        private HisVareVartTruncate TruncateWorker
        {
            get
            {
                return (HisVareVartTruncate)Worker.Get<HisVareVartTruncate>();
            }
        }
        private HisVareVartGet GetWorker
        {
            get
            {
                return (HisVareVartGet)Worker.Get<HisVareVartGet>();
            }
        }
        private HisVareVartCheck CheckWorker
        {
            get
            {
                return (HisVareVartCheck)Worker.Get<HisVareVartCheck>();
            }
        }

        public bool Create(HIS_VARE_VART data)
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

        public bool CreateList(List<HIS_VARE_VART> listData)
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

        public bool Update(HIS_VARE_VART data)
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

        public bool UpdateList(List<HIS_VARE_VART> listData)
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

        public bool Delete(HIS_VARE_VART data)
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

        public bool DeleteList(List<HIS_VARE_VART> listData)
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

        public bool Truncate(HIS_VARE_VART data)
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

        public bool TruncateList(List<HIS_VARE_VART> listData)
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

        public List<HIS_VARE_VART> Get(HisVareVartSO search, CommonParam param)
        {
            List<HIS_VARE_VART> result = new List<HIS_VARE_VART>();
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

        public HIS_VARE_VART GetById(long id, HisVareVartSO search)
        {
            HIS_VARE_VART result = null;
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
