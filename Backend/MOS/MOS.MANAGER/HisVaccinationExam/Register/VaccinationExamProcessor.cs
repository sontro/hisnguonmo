using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisVaccinationExam.Register
{
    class VaccinationExamProcessor : BusinessBase
    {
        private HisVaccinationExamCreate hisVaccinationExamCreate;

        internal VaccinationExamProcessor()
            : base()
        {
            this.hisVaccinationExamCreate = new HisVaccinationExamCreate(param);
        }

        internal VaccinationExamProcessor(CommonParam param)
            : base(param)
        {
            this.hisVaccinationExamCreate = new HisVaccinationExamCreate(param);
        }

        internal bool Run(HisPatientVaccinationSDO data, WorkPlaceSDO workPlace, HIS_PATIENT patient)
        {
            bool result = false;
            try
            {
                if (data.HisVaccinationExam != null)
                {
                    V_HIS_EXECUTE_ROOM exeRoom = HisExecuteRoomCFG.DATA.FirstOrDefault(o => o.ROOM_ID == data.HisVaccinationExam.EXECUTE_ROOM_ID);
                    if (exeRoom == null || exeRoom.IS_VACCINE != Constant.IS_TRUE)
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("EXECUTE_ROOM_ID Invalid. Ket thuc nghiep vu");
                    }
                    data.HisVaccinationExam.REQUEST_ROOM_ID = data.RequestRoomId;
                    data.HisVaccinationExam.REQUEST_DEPARTMENT_ID = workPlace.DepartmentId;
                    data.HisVaccinationExam.BRANCH_ID = workPlace.BranchId;
                    data.HisVaccinationExam.EXECUTE_DEPARTMENT_ID = exeRoom.DEPARTMENT_ID;
                    if (!String.IsNullOrWhiteSpace(data.RequestLoginname))
                    {
                        data.HisVaccinationExam.REQUEST_LOGINNAME = data.RequestLoginname;
                        data.HisVaccinationExam.REQUEST_USERNAME = data.RequestUsername;
                    }
                    else
                    {
                        data.HisVaccinationExam.REQUEST_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                        data.HisVaccinationExam.REQUEST_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                    }
                    data.HisVaccinationExam.PATIENT_ID = patient.ID;
                    if (data.HisVaccinationExam.REQUEST_TIME <= 0)
                    {
                        data.HisVaccinationExam.REQUEST_TIME = Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmmss"));
                    }

                    if (!this.hisVaccinationExamCreate.Create(data.HisVaccinationExam, patient))
                    {
                        throw new Exception("hisVaccinationExamCreate. Ket thuc nghiep vu");
                    }
                }
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal void Rollback()
        {
            try
            {
                this.hisVaccinationExamCreate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
