using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTreatment
{
    partial class HisTreatmentUpdateJsonForm : BusinessBase
    {
		private List<HIS_TREATMENT> beforeUpdateHisTreatments = new List<HIS_TREATMENT>();

        internal HisTreatmentUpdateJsonForm()
            : base()
        {

        }

        internal HisTreatmentUpdateJsonForm(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }
		
        internal bool Run(HIS_TREATMENT data, ref HIS_TREATMENT resultData)
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
                    this.beforeUpdateHisTreatments.Add(Mapper.Map<HIS_TREATMENT>(raw));
                    raw.JSON_FORM_ID = data.JSON_FORM_ID;

                    if (!DAOWorker.HisTreatmentDAO.Update(raw))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTreatment_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisTreatment that bai." + LogUtil.TraceData("data", data));
                    }
                    resultData = raw;
                    result = true;
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
