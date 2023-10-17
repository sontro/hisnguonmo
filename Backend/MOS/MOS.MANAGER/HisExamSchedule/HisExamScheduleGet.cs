using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExamSchedule
{
    partial class HisExamScheduleGet : BusinessBase
    {
        internal HisExamScheduleGet()
            : base()
        {

        }

        internal HisExamScheduleGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_EXAM_SCHEDULE> Get(HisExamScheduleFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisExamScheduleDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_EXAM_SCHEDULE GetById(long id)
        {
            try
            {
                return GetById(id, new HisExamScheduleFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_EXAM_SCHEDULE GetById(long id, HisExamScheduleFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisExamScheduleDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }
    }
}
