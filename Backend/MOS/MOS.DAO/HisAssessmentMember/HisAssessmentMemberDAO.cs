using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisAssessmentMember
{
    public partial class HisAssessmentMemberDAO : EntityBase
    {
        private HisAssessmentMemberCreate CreateWorker
        {
            get
            {
                return (HisAssessmentMemberCreate)Worker.Get<HisAssessmentMemberCreate>();
            }
        }
        private HisAssessmentMemberUpdate UpdateWorker
        {
            get
            {
                return (HisAssessmentMemberUpdate)Worker.Get<HisAssessmentMemberUpdate>();
            }
        }
        private HisAssessmentMemberDelete DeleteWorker
        {
            get
            {
                return (HisAssessmentMemberDelete)Worker.Get<HisAssessmentMemberDelete>();
            }
        }
        private HisAssessmentMemberTruncate TruncateWorker
        {
            get
            {
                return (HisAssessmentMemberTruncate)Worker.Get<HisAssessmentMemberTruncate>();
            }
        }
        private HisAssessmentMemberGet GetWorker
        {
            get
            {
                return (HisAssessmentMemberGet)Worker.Get<HisAssessmentMemberGet>();
            }
        }
        private HisAssessmentMemberCheck CheckWorker
        {
            get
            {
                return (HisAssessmentMemberCheck)Worker.Get<HisAssessmentMemberCheck>();
            }
        }

        public bool Create(HIS_ASSESSMENT_MEMBER data)
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

        public bool CreateList(List<HIS_ASSESSMENT_MEMBER> listData)
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

        public bool Update(HIS_ASSESSMENT_MEMBER data)
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

        public bool UpdateList(List<HIS_ASSESSMENT_MEMBER> listData)
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

        public bool Delete(HIS_ASSESSMENT_MEMBER data)
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

        public bool DeleteList(List<HIS_ASSESSMENT_MEMBER> listData)
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

        public bool Truncate(HIS_ASSESSMENT_MEMBER data)
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

        public bool TruncateList(List<HIS_ASSESSMENT_MEMBER> listData)
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

        public List<HIS_ASSESSMENT_MEMBER> Get(HisAssessmentMemberSO search, CommonParam param)
        {
            List<HIS_ASSESSMENT_MEMBER> result = new List<HIS_ASSESSMENT_MEMBER>();
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

        public HIS_ASSESSMENT_MEMBER GetById(long id, HisAssessmentMemberSO search)
        {
            HIS_ASSESSMENT_MEMBER result = null;
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
