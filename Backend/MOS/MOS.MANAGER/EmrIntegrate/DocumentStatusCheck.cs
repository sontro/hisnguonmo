using Inventec.Common.Logging;
using Inventec.Core;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MOS.MANAGER.EmrIntegrate
{
    public class DocumentStatusCheck: BusinessBase
    {
        internal DocumentStatusCheck()
            : base()
        {

        }

        internal DocumentStatusCheck(CommonParam param)
            : base(param)
        {

        }

        internal bool IsValidStatus(EmrDocumentChangeStatusSDO data, ref long? documentStatusId)
        {
            try
            {
                if (data == null)
                {
                    return false;
                }
                if (data.DocumentStatus == DocumentStatusEnum.SIGNING)
                {
                    documentStatusId = IMSys.DbConfig.HIS_RS.HIS_EMR_DOCUMENT_STT.SIGNING;
                    return true;
                }
                else if (data.DocumentStatus == DocumentStatusEnum.SIGNING_FINISHED)
                {
                    documentStatusId = IMSys.DbConfig.HIS_RS.HIS_EMR_DOCUMENT_STT.SIGNING_FINISHED;
                    return true;
                }
                else if (data.DocumentStatus == DocumentStatusEnum.SIGNING_REJECTED)
                {
                    documentStatusId = IMSys.DbConfig.HIS_RS.HIS_EMR_DOCUMENT_STT.SIGNING_REJECTED;
                    return true;
                }
                else if (data.DocumentStatus == DocumentStatusEnum.UNSIGN)
                {
                    documentStatusId = IMSys.DbConfig.HIS_RS.HIS_EMR_DOCUMENT_STT.UNSIGN;
                    return true;
                }
                else if (data.DocumentStatus == DocumentStatusEnum.UNSAVE_OR_DELETED)
                {
                    documentStatusId = IMSys.DbConfig.HIS_RS.HIS_EMR_DOCUMENT_STT.UNSAVE;
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }
    }
}
