using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisDepartmentTran;
using MOS.MANAGER.HisEmployee;
using MOS.MANAGER.HisMediRecord;
using MOS.MANAGER.HisProgram;
using MOS.MANAGER.HisSereServExt;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisTransaction;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTreatment.Update.Unfinish
{
    class HisTreatmentUnfinishCheck : BusinessBase
    {
        internal HisTreatmentUnfinishCheck()
            : base()
        {
        }

        internal HisTreatmentUnfinishCheck(CommonParam param)
            : base(param)
        {
        }

        internal bool IsNotStored(HIS_TREATMENT treatment)
        {
            bool valid = true;
            try
            {
                if (treatment.MEDI_RECORD_ID.HasValue)
                {
                    HIS_MEDI_RECORD mediRecord = new HisMediRecordGet().GetById(treatment.MEDI_RECORD_ID.Value);
                    if (mediRecord != null && mediRecord.IS_NOT_STORED != Constant.IS_TRUE)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_BenhAnDaLuuTru, mediRecord.STORE_CODE);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool HasNoTreatmentDebt(long treatmentId)
        {
            bool valid = true;
            try
            {
                HisTransactionFilterQuery filter = new HisTransactionFilterQuery();
                filter.TREATMENT_ID = treatmentId;
                filter.TRANSACTION_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__NO;
                filter.DEBT_TYPE = IMSys.DbConfig.HIS_RS.HIS_TRANSACTION.DEBT_TYPE__TREAT;
                filter.IS_CANCEL = false;

                List<HIS_TRANSACTION> transactions = new HisTransactionGet().Get(filter);
                if (IsNotNullOrEmpty(transactions))
                {
                    List<string> transactionCodes = transactions.Select(o => o.TRANSACTION_CODE).ToList();
                    string transactionCodeStr = string.Join(",", transactionCodes);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_HoSoDaDuocChotNo, transactionCodeStr);
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool IsAllow(long? treatmentTypeId)
        {
            bool valid = true;
            try
            {
                HIS_TREATMENT_TYPE treatmentType = HisTreatmentTypeCFG.DATA.Where(o => o.ID == treatmentTypeId).FirstOrDefault();
                if (treatmentType != null && treatmentType.IS_NOT_ALLOW_UNPAUSE == Constant.IS_TRUE && !HisEmployeeUtil.IsAdmin())
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_ChiChoPhepQuanTriHeThongMo, treatmentType.TREATMENT_TYPE_NAME);
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }
    }
}
