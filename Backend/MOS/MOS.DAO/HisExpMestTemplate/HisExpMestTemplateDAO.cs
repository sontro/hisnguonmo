using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisExpMestTemplate
{
    public partial class HisExpMestTemplateDAO : EntityBase
    {
        private HisExpMestTemplateCreate CreateWorker
        {
            get
            {
                return (HisExpMestTemplateCreate)Worker.Get<HisExpMestTemplateCreate>();
            }
        }
        private HisExpMestTemplateUpdate UpdateWorker
        {
            get
            {
                return (HisExpMestTemplateUpdate)Worker.Get<HisExpMestTemplateUpdate>();
            }
        }
        private HisExpMestTemplateDelete DeleteWorker
        {
            get
            {
                return (HisExpMestTemplateDelete)Worker.Get<HisExpMestTemplateDelete>();
            }
        }
        private HisExpMestTemplateTruncate TruncateWorker
        {
            get
            {
                return (HisExpMestTemplateTruncate)Worker.Get<HisExpMestTemplateTruncate>();
            }
        }
        private HisExpMestTemplateGet GetWorker
        {
            get
            {
                return (HisExpMestTemplateGet)Worker.Get<HisExpMestTemplateGet>();
            }
        }
        private HisExpMestTemplateCheck CheckWorker
        {
            get
            {
                return (HisExpMestTemplateCheck)Worker.Get<HisExpMestTemplateCheck>();
            }
        }

        public bool Create(HIS_EXP_MEST_TEMPLATE data)
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

        public bool CreateList(List<HIS_EXP_MEST_TEMPLATE> listData)
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

        public bool Update(HIS_EXP_MEST_TEMPLATE data)
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

        public bool UpdateList(List<HIS_EXP_MEST_TEMPLATE> listData)
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

        public bool Delete(HIS_EXP_MEST_TEMPLATE data)
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

        public bool DeleteList(List<HIS_EXP_MEST_TEMPLATE> listData)
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

        public bool Truncate(HIS_EXP_MEST_TEMPLATE data)
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

        public bool TruncateList(List<HIS_EXP_MEST_TEMPLATE> listData)
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

        public List<HIS_EXP_MEST_TEMPLATE> Get(HisExpMestTemplateSO search, CommonParam param)
        {
            List<HIS_EXP_MEST_TEMPLATE> result = new List<HIS_EXP_MEST_TEMPLATE>();
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

        public HIS_EXP_MEST_TEMPLATE GetById(long id, HisExpMestTemplateSO search)
        {
            HIS_EXP_MEST_TEMPLATE result = null;
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
