using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisEquipmentSetMaty
{
    public partial class HisEquipmentSetMatyDAO : EntityBase
    {
        private HisEquipmentSetMatyCreate CreateWorker
        {
            get
            {
                return (HisEquipmentSetMatyCreate)Worker.Get<HisEquipmentSetMatyCreate>();
            }
        }
        private HisEquipmentSetMatyUpdate UpdateWorker
        {
            get
            {
                return (HisEquipmentSetMatyUpdate)Worker.Get<HisEquipmentSetMatyUpdate>();
            }
        }
        private HisEquipmentSetMatyDelete DeleteWorker
        {
            get
            {
                return (HisEquipmentSetMatyDelete)Worker.Get<HisEquipmentSetMatyDelete>();
            }
        }
        private HisEquipmentSetMatyTruncate TruncateWorker
        {
            get
            {
                return (HisEquipmentSetMatyTruncate)Worker.Get<HisEquipmentSetMatyTruncate>();
            }
        }
        private HisEquipmentSetMatyGet GetWorker
        {
            get
            {
                return (HisEquipmentSetMatyGet)Worker.Get<HisEquipmentSetMatyGet>();
            }
        }
        private HisEquipmentSetMatyCheck CheckWorker
        {
            get
            {
                return (HisEquipmentSetMatyCheck)Worker.Get<HisEquipmentSetMatyCheck>();
            }
        }

        public bool Create(HIS_EQUIPMENT_SET_MATY data)
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

        public bool CreateList(List<HIS_EQUIPMENT_SET_MATY> listData)
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

        public bool Update(HIS_EQUIPMENT_SET_MATY data)
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

        public bool UpdateList(List<HIS_EQUIPMENT_SET_MATY> listData)
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

        public bool Delete(HIS_EQUIPMENT_SET_MATY data)
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

        public bool DeleteList(List<HIS_EQUIPMENT_SET_MATY> listData)
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

        public bool Truncate(HIS_EQUIPMENT_SET_MATY data)
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

        public bool TruncateList(List<HIS_EQUIPMENT_SET_MATY> listData)
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

        public List<HIS_EQUIPMENT_SET_MATY> Get(HisEquipmentSetMatySO search, CommonParam param)
        {
            List<HIS_EQUIPMENT_SET_MATY> result = new List<HIS_EQUIPMENT_SET_MATY>();
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

        public HIS_EQUIPMENT_SET_MATY GetById(long id, HisEquipmentSetMatySO search)
        {
            HIS_EQUIPMENT_SET_MATY result = null;
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
