using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisHoreHoha
{
    public partial class HisHoreHohaDAO : EntityBase
    {
        private HisHoreHohaCreate CreateWorker
        {
            get
            {
                return (HisHoreHohaCreate)Worker.Get<HisHoreHohaCreate>();
            }
        }
        private HisHoreHohaUpdate UpdateWorker
        {
            get
            {
                return (HisHoreHohaUpdate)Worker.Get<HisHoreHohaUpdate>();
            }
        }
        private HisHoreHohaDelete DeleteWorker
        {
            get
            {
                return (HisHoreHohaDelete)Worker.Get<HisHoreHohaDelete>();
            }
        }
        private HisHoreHohaTruncate TruncateWorker
        {
            get
            {
                return (HisHoreHohaTruncate)Worker.Get<HisHoreHohaTruncate>();
            }
        }
        private HisHoreHohaGet GetWorker
        {
            get
            {
                return (HisHoreHohaGet)Worker.Get<HisHoreHohaGet>();
            }
        }
        private HisHoreHohaCheck CheckWorker
        {
            get
            {
                return (HisHoreHohaCheck)Worker.Get<HisHoreHohaCheck>();
            }
        }

        public bool Create(HIS_HORE_HOHA data)
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

        public bool CreateList(List<HIS_HORE_HOHA> listData)
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

        public bool Update(HIS_HORE_HOHA data)
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

        public bool UpdateList(List<HIS_HORE_HOHA> listData)
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

        public bool Delete(HIS_HORE_HOHA data)
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

        public bool DeleteList(List<HIS_HORE_HOHA> listData)
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

        public bool Truncate(HIS_HORE_HOHA data)
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

        public bool TruncateList(List<HIS_HORE_HOHA> listData)
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

        public List<HIS_HORE_HOHA> Get(HisHoreHohaSO search, CommonParam param)
        {
            List<HIS_HORE_HOHA> result = new List<HIS_HORE_HOHA>();
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

        public HIS_HORE_HOHA GetById(long id, HisHoreHohaSO search)
        {
            HIS_HORE_HOHA result = null;
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
