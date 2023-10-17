using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisAssessmentObject
{
    public partial class HisAssessmentObjectDAO : EntityBase
    {
        private HisAssessmentObjectCreate CreateWorker
        {
            get
            {
                return (HisAssessmentObjectCreate)Worker.Get<HisAssessmentObjectCreate>();
            }
        }
        private HisAssessmentObjectUpdate UpdateWorker
        {
            get
            {
                return (HisAssessmentObjectUpdate)Worker.Get<HisAssessmentObjectUpdate>();
            }
        }
        private HisAssessmentObjectDelete DeleteWorker
        {
            get
            {
                return (HisAssessmentObjectDelete)Worker.Get<HisAssessmentObjectDelete>();
            }
        }
        private HisAssessmentObjectTruncate TruncateWorker
        {
            get
            {
                return (HisAssessmentObjectTruncate)Worker.Get<HisAssessmentObjectTruncate>();
            }
        }
        private HisAssessmentObjectGet GetWorker
        {
            get
            {
                return (HisAssessmentObjectGet)Worker.Get<HisAssessmentObjectGet>();
            }
        }
        private HisAssessmentObjectCheck CheckWorker
        {
            get
            {
                return (HisAssessmentObjectCheck)Worker.Get<HisAssessmentObjectCheck>();
            }
        }

        public bool Create(HIS_ASSESSMENT_OBJECT data)
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

        public bool CreateList(List<HIS_ASSESSMENT_OBJECT> listData)
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

        public bool Update(HIS_ASSESSMENT_OBJECT data)
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

        public bool UpdateList(List<HIS_ASSESSMENT_OBJECT> listData)
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

        public bool Delete(HIS_ASSESSMENT_OBJECT data)
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

        public bool DeleteList(List<HIS_ASSESSMENT_OBJECT> listData)
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

        public bool Truncate(HIS_ASSESSMENT_OBJECT data)
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

        public bool TruncateList(List<HIS_ASSESSMENT_OBJECT> listData)
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

        public List<HIS_ASSESSMENT_OBJECT> Get(HisAssessmentObjectSO search, CommonParam param)
        {
            List<HIS_ASSESSMENT_OBJECT> result = new List<HIS_ASSESSMENT_OBJECT>();
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

        public HIS_ASSESSMENT_OBJECT GetById(long id, HisAssessmentObjectSO search)
        {
            HIS_ASSESSMENT_OBJECT result = null;
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
        public bool ExistsCode(string code, long? id)
        {
            try
            {
                return CheckWorker.ExistsCode(code, id);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                throw;
            }
        }
        public HIS_ASSESSMENT_OBJECT GetByCode(string code, HisAssessmentObjectSO search)
        {
            HIS_ASSESSMENT_OBJECT result = null;

            try
            {
                result = GetWorker.GetByCode(code, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }

            return result;
        }
        
    }
}
