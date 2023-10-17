using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisPtttHighTech
{
    public partial class HisPtttHighTechDAO : EntityBase
    {
        private HisPtttHighTechCreate CreateWorker
        {
            get
            {
                return (HisPtttHighTechCreate)Worker.Get<HisPtttHighTechCreate>();
            }
        }
        private HisPtttHighTechUpdate UpdateWorker
        {
            get
            {
                return (HisPtttHighTechUpdate)Worker.Get<HisPtttHighTechUpdate>();
            }
        }
        private HisPtttHighTechDelete DeleteWorker
        {
            get
            {
                return (HisPtttHighTechDelete)Worker.Get<HisPtttHighTechDelete>();
            }
        }
        private HisPtttHighTechTruncate TruncateWorker
        {
            get
            {
                return (HisPtttHighTechTruncate)Worker.Get<HisPtttHighTechTruncate>();
            }
        }
        private HisPtttHighTechGet GetWorker
        {
            get
            {
                return (HisPtttHighTechGet)Worker.Get<HisPtttHighTechGet>();
            }
        }
        private HisPtttHighTechCheck CheckWorker
        {
            get
            {
                return (HisPtttHighTechCheck)Worker.Get<HisPtttHighTechCheck>();
            }
        }

        public bool Create(HIS_PTTT_HIGH_TECH data)
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

        public bool CreateList(List<HIS_PTTT_HIGH_TECH> listData)
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

        public bool Update(HIS_PTTT_HIGH_TECH data)
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

        public bool UpdateList(List<HIS_PTTT_HIGH_TECH> listData)
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

        public bool Delete(HIS_PTTT_HIGH_TECH data)
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

        public bool DeleteList(List<HIS_PTTT_HIGH_TECH> listData)
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

        public bool Truncate(HIS_PTTT_HIGH_TECH data)
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

        public bool TruncateList(List<HIS_PTTT_HIGH_TECH> listData)
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

        public List<HIS_PTTT_HIGH_TECH> Get(HisPtttHighTechSO search, CommonParam param)
        {
            List<HIS_PTTT_HIGH_TECH> result = new List<HIS_PTTT_HIGH_TECH>();
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

        public HIS_PTTT_HIGH_TECH GetById(long id, HisPtttHighTechSO search)
        {
            HIS_PTTT_HIGH_TECH result = null;
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
