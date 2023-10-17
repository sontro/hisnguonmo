using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisEquipmentSet
{
    public partial class HisEquipmentSetDAO : EntityBase
    {
        private HisEquipmentSetCreate CreateWorker
        {
            get
            {
                return (HisEquipmentSetCreate)Worker.Get<HisEquipmentSetCreate>();
            }
        }
        private HisEquipmentSetUpdate UpdateWorker
        {
            get
            {
                return (HisEquipmentSetUpdate)Worker.Get<HisEquipmentSetUpdate>();
            }
        }
        private HisEquipmentSetDelete DeleteWorker
        {
            get
            {
                return (HisEquipmentSetDelete)Worker.Get<HisEquipmentSetDelete>();
            }
        }
        private HisEquipmentSetTruncate TruncateWorker
        {
            get
            {
                return (HisEquipmentSetTruncate)Worker.Get<HisEquipmentSetTruncate>();
            }
        }
        private HisEquipmentSetGet GetWorker
        {
            get
            {
                return (HisEquipmentSetGet)Worker.Get<HisEquipmentSetGet>();
            }
        }
        private HisEquipmentSetCheck CheckWorker
        {
            get
            {
                return (HisEquipmentSetCheck)Worker.Get<HisEquipmentSetCheck>();
            }
        }

        public bool Create(HIS_EQUIPMENT_SET data)
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

        public bool CreateList(List<HIS_EQUIPMENT_SET> listData)
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

        public bool Update(HIS_EQUIPMENT_SET data)
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

        public bool UpdateList(List<HIS_EQUIPMENT_SET> listData)
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

        public bool Delete(HIS_EQUIPMENT_SET data)
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

        public bool DeleteList(List<HIS_EQUIPMENT_SET> listData)
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

        public bool Truncate(HIS_EQUIPMENT_SET data)
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

        public bool TruncateList(List<HIS_EQUIPMENT_SET> listData)
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

        public List<HIS_EQUIPMENT_SET> Get(HisEquipmentSetSO search, CommonParam param)
        {
            List<HIS_EQUIPMENT_SET> result = new List<HIS_EQUIPMENT_SET>();
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

        public HIS_EQUIPMENT_SET GetById(long id, HisEquipmentSetSO search)
        {
            HIS_EQUIPMENT_SET result = null;
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
