using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDocumentBook
{
    public partial class HisDocumentBookManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_DOCUMENT_BOOK>> GetView(HisDocumentBookViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_DOCUMENT_BOOK>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_DOCUMENT_BOOK> resultData = null;
                if (valid)
                {
                    resultData = new HisDocumentBookGet(param).GetView(filter);
                }
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }
            return result;
        }
    }
}
