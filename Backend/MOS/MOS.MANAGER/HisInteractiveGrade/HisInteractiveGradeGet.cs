using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisInteractiveGrade
{
    partial class HisInteractiveGradeGet : BusinessBase
    {
        internal HisInteractiveGradeGet()
            : base()
        {

        }

        internal HisInteractiveGradeGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_INTERACTIVE_GRADE> Get(HisInteractiveGradeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisInteractiveGradeDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_INTERACTIVE_GRADE GetById(long id)
        {
            try
            {
                return GetById(id, new HisInteractiveGradeFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_INTERACTIVE_GRADE GetById(long id, HisInteractiveGradeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisInteractiveGradeDAO.GetById(id, filter.Query());
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
