using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Token;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisCoTreatment
{
    partial class HisCoTreatmentCreate : BusinessBase
    {
        private List<HIS_CO_TREATMENT> recentHisCoTreatments = new List<HIS_CO_TREATMENT>();

        internal HisCoTreatmentCreate()
            : base()
        {

        }

        internal HisCoTreatmentCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HisCoTreatmentSDO data, ref HIS_CO_TREATMENT resultData)
        {
            bool result = false;
            try
            {
                HIS_DEPARTMENT_TRAN departTran = null;
                HisCoTreatmentCheck checker = new HisCoTreatmentCheck(param);
                if (checker.CheckValid(data, ref departTran))
                {
                    HIS_CO_TREATMENT toInserter = new HIS_CO_TREATMENT();
                    toInserter.TDL_TREATMENT_ID = data.TreatmentId;
                    toInserter.DEPARTMENT_ID = data.DepartmentId;
                    toInserter.DEPARTMENT_TRAN_ID = departTran.ID;
                    toInserter.COTREATMENT_REQUEST = data.CotreatmentRequest;
                    bool valid = true;
                    valid = valid && checker.VerifyRequireField(toInserter);
                    valid = valid && checker.CheckExists(toInserter);
                    if (valid)
                    {
                        if (!DAOWorker.HisCoTreatmentDAO.Create(toInserter))
                        {
                            MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisCoTreatment_ThemMoiThatBai);
                            throw new Exception("Them moi thong tin HisCoTreatment that bai." + LogUtil.TraceData("data", data));
                        }
                        this.recentHisCoTreatments.Add(toInserter);
                        resultData = toInserter;
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                this.RollbackData();
                result = false;
            }
            return result;
        }

        internal void RollbackData()
        {
            if (IsNotNullOrEmpty(this.recentHisCoTreatments))
            {
                if (!DAOWorker.HisCoTreatmentDAO.TruncateList(this.recentHisCoTreatments))
                {
                    LogSystem.Warn("Rollback du lieu HisCoTreatment that bai, can kiem tra lai." + LogUtil.TraceData("recentHisCoTreatments", this.recentHisCoTreatments));
                }
                this.recentHisCoTreatments = null;
            }
        }

    }
}
