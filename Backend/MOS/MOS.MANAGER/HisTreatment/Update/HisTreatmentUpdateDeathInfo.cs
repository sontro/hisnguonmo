using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTreatment
{
    partial class HisTreatmentUpdate : BusinessBase
    {

        internal bool UpdateDeathInfo(HIS_TREATMENT data, ref HIS_TREATMENT resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_TREATMENT raw = null;
                HisTreatmentCheck checker = new HisTreatmentCheck(param);
                valid = valid && checker.VerifyId(data.ID, ref raw);
                if (valid)
                {
                    Mapper.CreateMap<HIS_TREATMENT, HIS_TREATMENT>();
                    HIS_TREATMENT beforeUpdate = Mapper.Map<HIS_TREATMENT>(raw);
                    raw.DEATH_CAUSE_ID = data.DEATH_CAUSE_ID;
                    raw.DEATH_WITHIN_ID = data.DEATH_WITHIN_ID;
                    raw.DEATH_TIME = data.DEATH_TIME;
                    raw.IS_HAS_AUPOPSY = data.IS_HAS_AUPOPSY;
                    raw.MAIN_CAUSE = data.MAIN_CAUSE;
                    raw.SURGERY = data.SURGERY;
                    raw.DEATH_DOCUMENT_TYPE = data.DEATH_DOCUMENT_TYPE;
                    raw.DEATH_DOCUMENT_NUMBER = data.DEATH_DOCUMENT_NUMBER;
                    raw.DEATH_DOCUMENT_PLACE = data.DEATH_DOCUMENT_PLACE;
                    raw.DEATH_DOCUMENT_DATE = data.DEATH_DOCUMENT_DATE;
                    raw.DEATH_PLACE = data.DEATH_PLACE;
                    raw.TDL_PATIENT_RELATIVE_NAME = data.TDL_PATIENT_RELATIVE_NAME;
                    raw.DEATH_STATUS = data.DEATH_STATUS;
                    raw.DEATH_DOCUMENT_TYPE_CODE = data.DEATH_DOCUMENT_TYPE_CODE;
                    raw.DEATH_ISSUED_DATE = data.DEATH_ISSUED_DATE;

                    if (checker.CheckDeathInfo(raw))
                    {
                        if (!this.Update(data, beforeUpdate))
                        {
                            MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTreatment_CapNhatThatBai);
                            throw new Exception("Cap nhat thong tin HisTreatment that bai." + LogUtil.TraceData("data", data));
                        }
                        resultData = raw;
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
    }
}
