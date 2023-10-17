using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisNumOrderIssue
{
    partial class HisNumOrderIssueGet : BusinessBase
    {
        internal HisNumOrderIssueGet()
            : base()
        {

        }

        internal HisNumOrderIssueGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_NUM_ORDER_ISSUE> Get(HisNumOrderIssueFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisNumOrderIssueDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_NUM_ORDER_ISSUE GetById(long id)
        {
            try
            {
                return GetById(id, new HisNumOrderIssueFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_NUM_ORDER_ISSUE GetById(long id, HisNumOrderIssueFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisNumOrderIssueDAO.GetById(id, filter.Query());
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
