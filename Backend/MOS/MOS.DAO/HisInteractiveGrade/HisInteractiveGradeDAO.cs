using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisInteractiveGrade
{
    public partial class HisInteractiveGradeDAO : EntityBase
    {
        private HisInteractiveGradeCreate CreateWorker
        {
            get
            {
                return (HisInteractiveGradeCreate)Worker.Get<HisInteractiveGradeCreate>();
            }
        }
        private HisInteractiveGradeUpdate UpdateWorker
        {
            get
            {
                return (HisInteractiveGradeUpdate)Worker.Get<HisInteractiveGradeUpdate>();
            }
        }
        private HisInteractiveGradeDelete DeleteWorker
        {
            get
            {
                return (HisInteractiveGradeDelete)Worker.Get<HisInteractiveGradeDelete>();
            }
        }
        private HisInteractiveGradeTruncate TruncateWorker
        {
            get
            {
                return (HisInteractiveGradeTruncate)Worker.Get<HisInteractiveGradeTruncate>();
            }
        }
        private HisInteractiveGradeGet GetWorker
        {
            get
            {
                return (HisInteractiveGradeGet)Worker.Get<HisInteractiveGradeGet>();
            }
        }
        private HisInteractiveGradeCheck CheckWorker
        {
            get
            {
                return (HisInteractiveGradeCheck)Worker.Get<HisInteractiveGradeCheck>();
            }
        }

        public bool Create(HIS_INTERACTIVE_GRADE data)
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

        public bool CreateList(List<HIS_INTERACTIVE_GRADE> listData)
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

        public bool Update(HIS_INTERACTIVE_GRADE data)
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

        public bool UpdateList(List<HIS_INTERACTIVE_GRADE> listData)
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

        public bool Delete(HIS_INTERACTIVE_GRADE data)
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

        public bool DeleteList(List<HIS_INTERACTIVE_GRADE> listData)
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

        public bool Truncate(HIS_INTERACTIVE_GRADE data)
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

        public bool TruncateList(List<HIS_INTERACTIVE_GRADE> listData)
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

        public List<HIS_INTERACTIVE_GRADE> Get(HisInteractiveGradeSO search, CommonParam param)
        {
            List<HIS_INTERACTIVE_GRADE> result = new List<HIS_INTERACTIVE_GRADE>();
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

        public HIS_INTERACTIVE_GRADE GetById(long id, HisInteractiveGradeSO search)
        {
            HIS_INTERACTIVE_GRADE result = null;
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
