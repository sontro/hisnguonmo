using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisSkinSurgeryDesc
{
    public partial class HisSkinSurgeryDescDAO : EntityBase
    {
        private HisSkinSurgeryDescCreate CreateWorker
        {
            get
            {
                return (HisSkinSurgeryDescCreate)Worker.Get<HisSkinSurgeryDescCreate>();
            }
        }
        private HisSkinSurgeryDescUpdate UpdateWorker
        {
            get
            {
                return (HisSkinSurgeryDescUpdate)Worker.Get<HisSkinSurgeryDescUpdate>();
            }
        }
        private HisSkinSurgeryDescDelete DeleteWorker
        {
            get
            {
                return (HisSkinSurgeryDescDelete)Worker.Get<HisSkinSurgeryDescDelete>();
            }
        }
        private HisSkinSurgeryDescTruncate TruncateWorker
        {
            get
            {
                return (HisSkinSurgeryDescTruncate)Worker.Get<HisSkinSurgeryDescTruncate>();
            }
        }
        private HisSkinSurgeryDescGet GetWorker
        {
            get
            {
                return (HisSkinSurgeryDescGet)Worker.Get<HisSkinSurgeryDescGet>();
            }
        }
        private HisSkinSurgeryDescCheck CheckWorker
        {
            get
            {
                return (HisSkinSurgeryDescCheck)Worker.Get<HisSkinSurgeryDescCheck>();
            }
        }

        public bool Create(HIS_SKIN_SURGERY_DESC data)
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

        public bool CreateList(List<HIS_SKIN_SURGERY_DESC> listData)
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

        public bool Update(HIS_SKIN_SURGERY_DESC data)
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

        public bool UpdateList(List<HIS_SKIN_SURGERY_DESC> listData)
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

        public bool Delete(HIS_SKIN_SURGERY_DESC data)
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

        public bool DeleteList(List<HIS_SKIN_SURGERY_DESC> listData)
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

        public bool Truncate(HIS_SKIN_SURGERY_DESC data)
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

        public bool TruncateList(List<HIS_SKIN_SURGERY_DESC> listData)
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

        public List<HIS_SKIN_SURGERY_DESC> Get(HisSkinSurgeryDescSO search, CommonParam param)
        {
            List<HIS_SKIN_SURGERY_DESC> result = new List<HIS_SKIN_SURGERY_DESC>();
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

        public HIS_SKIN_SURGERY_DESC GetById(long id, HisSkinSurgeryDescSO search)
        {
            HIS_SKIN_SURGERY_DESC result = null;
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
