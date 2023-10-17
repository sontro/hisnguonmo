using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMaterialPaty
{
    public partial class HisMaterialPatyDAO : EntityBase
    {
        private HisMaterialPatyCreate CreateWorker
        {
            get
            {
                return (HisMaterialPatyCreate)Worker.Get<HisMaterialPatyCreate>();
            }
        }
        private HisMaterialPatyUpdate UpdateWorker
        {
            get
            {
                return (HisMaterialPatyUpdate)Worker.Get<HisMaterialPatyUpdate>();
            }
        }
        private HisMaterialPatyDelete DeleteWorker
        {
            get
            {
                return (HisMaterialPatyDelete)Worker.Get<HisMaterialPatyDelete>();
            }
        }
        private HisMaterialPatyTruncate TruncateWorker
        {
            get
            {
                return (HisMaterialPatyTruncate)Worker.Get<HisMaterialPatyTruncate>();
            }
        }
        private HisMaterialPatyGet GetWorker
        {
            get
            {
                return (HisMaterialPatyGet)Worker.Get<HisMaterialPatyGet>();
            }
        }
        private HisMaterialPatyCheck CheckWorker
        {
            get
            {
                return (HisMaterialPatyCheck)Worker.Get<HisMaterialPatyCheck>();
            }
        }

        public bool Create(HIS_MATERIAL_PATY data)
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

        public bool CreateList(List<HIS_MATERIAL_PATY> listData)
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

        public bool Update(HIS_MATERIAL_PATY data)
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

        public bool UpdateList(List<HIS_MATERIAL_PATY> listData)
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

        public bool Delete(HIS_MATERIAL_PATY data)
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

        public bool DeleteList(List<HIS_MATERIAL_PATY> listData)
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

        public bool Truncate(HIS_MATERIAL_PATY data)
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

        public bool TruncateList(List<HIS_MATERIAL_PATY> listData)
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

        public List<HIS_MATERIAL_PATY> Get(HisMaterialPatySO search, CommonParam param)
        {
            List<HIS_MATERIAL_PATY> result = new List<HIS_MATERIAL_PATY>();
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

        public HIS_MATERIAL_PATY GetById(long id, HisMaterialPatySO search)
        {
            HIS_MATERIAL_PATY result = null;
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
