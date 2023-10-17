using COS.SDO;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisPatient
{
    class HisPatientCreateRegisterCode : BusinessBase
    {
        internal HisPatientCreateRegisterCode()
            : base()
        {

        }

        internal HisPatientCreateRegisterCode(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Run(long patientId)
        {
            bool result = false;
            try
            {
                if (!CosCFG.IS_CREATE_REGISTER_CODE)
                {
                    LogSystem.Debug("Khong bat cau hinh tao ma MS");
                    return true;
                }

                HIS_PATIENT raw = null;
                HisPatientCheck check = new HisPatientCheck(param);
                bool valid = true;
                valid = valid && check.VerifyId(patientId, ref raw);
                valid = valid && CheckDataCreate(param, raw);
                if (valid)
                {
                    CosRegisterReqByHisSDO sdo = new CosRegisterReqByHisSDO();
                    sdo.BhytNumber = raw.TDL_HEIN_CARD_NUMBER;
                    sdo.CmndNumber = raw.CMND_NUMBER;
                    if (String.IsNullOrWhiteSpace(sdo.CmndNumber) && !String.IsNullOrWhiteSpace(raw.CCCD_NUMBER))
                    {
                        sdo.CmndNumber = raw.CCCD_NUMBER;
                    }

                    sdo.Dob = raw.DOB;
                    sdo.FirstName = raw.FIRST_NAME;
                    sdo.GenderId = raw.GENDER_ID;
                    sdo.LastName = raw.LAST_NAME;
                    sdo.Mobile = raw.MOBILE;
                    if (String.IsNullOrWhiteSpace(sdo.Mobile) && !String.IsNullOrWhiteSpace(raw.PHONE))
                    {
                        sdo.Mobile = raw.PHONE;
                    }

                    var apiResult = ApiConsumerManager.ApiConsumerStore.CosConsumer.Post<CosRegisterReqByHisResultSDO>(true, "/api/CosRegister/RequestByHis", param, sdo);
                    if (apiResult != null)
                    {
                        string sql = "UPDATE HIS_PATIENT SET REGISTER_CODE = '{0}' WHERE ID = {1}";
                        sql = string.Format(sql, apiResult.RegisterCode, patientId);
                        if (!new MOS.DAO.Sql.SqlDAO().Execute(sql))
                        {
                            MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPatient_CapNhatThatBai);
                            throw new Exception("Cap nhat thong tin HisPatient that bai." + LogUtil.TraceData("data", apiResult));
                        }

                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result = false;
                param.HasException = true;
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }

        private bool CheckDataCreate(CommonParam param, HIS_PATIENT raw)
        {
            bool result = true;
            try
            {
                if (raw == null)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    throw new Exception("khong xac dinh duoc du lieu");
                }

                if (String.IsNullOrWhiteSpace(raw.PHONE) && String.IsNullOrWhiteSpace(raw.MOBILE) && String.IsNullOrWhiteSpace(raw.TDL_HEIN_CARD_NUMBER) && String.IsNullOrWhiteSpace(raw.CCCD_NUMBER) && String.IsNullOrWhiteSpace(raw.CMND_NUMBER))
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    throw new Exception("khong co thong tin cmnd, the bhyt, sdt");
                }

                if (!string.IsNullOrWhiteSpace(raw.REGISTER_CODE))
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.HisPatient_CapNhatThatBai);
                    throw new Exception("Benh nhan da co ma MS");
                }
            }
            catch (Exception ex)
            {
                result = false;
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }
    }
}
