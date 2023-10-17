using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisPatient;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisRoom;
using MOS.MANAGER.Token;
using MOS.LibraryHein.Bhyt.HeinRightRoute;
using MOS.LibraryHein.Bhyt.HeinUpToStandard;
using MOS.LibraryHein.Bhyt.HeinJoin5Year;
using MOS.LibraryHein.Bhyt.HeinPaid6Month;
using MOS.UTILITY;
using System.Globalization;
using MOS.MANAGER.HisServiceFollow;
using MOS.ServicePaty;
using Inventec.Token.ResourceSystem;
using System.Threading.Tasks;
using MOS.LibraryHein.Bhyt.HeinLevel;
using MOS.LibraryHein.Bhyt.HeinHasBirthCertificate;
using MOS.MANAGER.HisGender;
using MOS.Filter;

namespace MOS.MANAGER.HisServiceReq.Exam.Register.Dkk
{
    /// <summary>
    /// Dang ky kham tren cay kiosk
    /// </summary>
    partial class HisServiceReqExamRegisterDkk : BusinessBase
    {
        private HisServiceReqExamRegister hisServiceReqExamRegister;

        internal HisServiceReqExamRegisterDkk()
            : base()
        {
            this.Init();
        }

        internal HisServiceReqExamRegisterDkk(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisServiceReqExamRegister = new HisServiceReqExamRegister(param);
        }

        /// <summary>
        /// Dang ky kham qua tong dai (DKK)
        /// </summary>
        /// <param name="tdo"></param>
        /// <param name="resultData"></param>
        /// <returns></returns>
        internal bool Run(HisExamRegisterDkkSDO sdo, ref HisServiceReqExamRegisterResultSDO resultData)
        {
            bool result = false;
            try
            {
                HisServiceReqExamRegisterSDO registerSDO = this.ToRegisterSdo(sdo);
                HIS_TREATMENT treatment = null;
                WorkPlaceSDO workPlace = null;
                result = this.hisServiceReqExamRegister.Create(registerSDO, true, ref resultData, ref treatment, ref workPlace);
                //tao thread update thong tin trieu chung, ly do kham
                ThreadUpdateServiceReqInfo(sdo, resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private HisServiceReqExamRegisterSDO ToRegisterSdo(HisExamRegisterDkkSDO sdo)
        {
            HisServiceReqExamRegisterSDO result = null;
            if (sdo != null)
            {
                long treatmentTime = sdo.RegisterDate > 0 ? sdo.RegisterDate : Inventec.Common.DateTime.Get.Now().Value;
                HIS_PATIENT_TYPE patientType = HisPatientTypeCFG.DATA.Where(o => o.PATIENT_TYPE_CODE == sdo.PatientTypeCode).FirstOrDefault();
                V_HIS_ROOM room = HisRoomCFG.DATA != null ? HisRoomCFG.DATA.Where(o => o.ROOM_CODE == sdo.RoomCode).FirstOrDefault() : null;
                V_HIS_SERVICE sv = HisServiceCFG.DATA_VIEW != null ? HisServiceCFG.DATA_VIEW.Where(o => o.SERVICE_CODE == sdo.ServiceCode).FirstOrDefault() : null;

                if (room == null)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Phong kham ko ton tai");
                }

                HIS_GENDER gender = HisGenderCFG.DATA.Where(o => o.GENDER_CODE == sdo.GenderCode).FirstOrDefault();
                HIS_BRANCH hisBranch = new TokenManager(param).GetBranch();
                HIS_TREATMENT treatment = new HIS_TREATMENT();

                HIS_PATIENT_TYPE_ALTER hisPatientTypeAlter = new HIS_PATIENT_TYPE_ALTER();
                hisPatientTypeAlter.PATIENT_TYPE_ID = patientType.ID;
                hisPatientTypeAlter.TREATMENT_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__KHAM;//mac dinh la kham
                hisPatientTypeAlter.LOG_TIME = treatmentTime;
                hisPatientTypeAlter.EXECUTE_ROOM_ID = sdo.RequestRoomId;

                if (patientType.ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT)
                {
                    hisPatientTypeAlter.ADDRESS = sdo.BhytAddress;
                    hisPatientTypeAlter.HEIN_CARD_FROM_TIME = sdo.BhytFromTime;
                    hisPatientTypeAlter.HEIN_CARD_NUMBER = sdo.BhytNumber;
                    hisPatientTypeAlter.HEIN_CARD_TO_TIME = sdo.BhytToTime;
                    hisPatientTypeAlter.HEIN_MEDI_ORG_CODE = sdo.MediOrgCode;
                    if (!String.IsNullOrWhiteSpace(sdo.MediOrgName))
                        hisPatientTypeAlter.HEIN_MEDI_ORG_NAME = sdo.MediOrgName;
                    else
                        hisPatientTypeAlter.HEIN_MEDI_ORG_NAME = GetMediOrgNameByCode(sdo.MediOrgCode);
                    hisPatientTypeAlter.LEVEL_CODE = hisBranch.HEIN_LEVEL_CODE;
                    hisPatientTypeAlter.LIVE_AREA_CODE = sdo.LiveCode;
                    //Neu dung noi KCB ban dau hoac BV la tuyen huyen hoac tuyen xa thi la dung tuyen, 
                    //con lai la trai tuyen (vi thuc te se ko co cac hinh thuc cap cuu, gioi thieu neu dang ky qua tong dai)
                    hisPatientTypeAlter.RIGHT_ROUTE_CODE = hisBranch.HEIN_MEDI_ORG_CODE == sdo.MediOrgCode || hisBranch.HEIN_LEVEL_CODE == HeinLevelCode.COMMUNE || hisBranch.HEIN_LEVEL_CODE == HeinLevelCode.DISTRICT ? HeinRightRouteCode.TRUE : HeinRightRouteCode.FALSE;
                    hisPatientTypeAlter.JOIN_5_YEAR = sdo.Join5Year ? HeinJoin5YearCode.TRUE : HeinJoin5YearCode.FALSE;
                    hisPatientTypeAlter.PAID_6_MONTH = sdo.Paid6Month ? HeinPaid6MonthCode.TRUE : HeinPaid6MonthCode.FALSE;
                    hisPatientTypeAlter.HAS_BIRTH_CERTIFICATE = HeinHasBirthCertificateCode.FALSE; //BN dang ky kiosk se ko co giay chung sinh
                }

                HIS_PATIENT hisPatient = null;
                hisPatient = new HIS_PATIENT();
                hisPatient.DOB = sdo.Dob;
                hisPatient.FIRST_NAME = sdo.FirstName;
                hisPatient.GENDER_ID = gender.ID;
                hisPatient.LAST_NAME = sdo.LastName;

                if (sdo.PatientId.HasValue && sdo.PatientId.Value > 0)
                {
                    hisPatient = new HisPatientGet(param).GetById(sdo.PatientId.Value);
                }
                else if (!String.IsNullOrWhiteSpace(sdo.CardCode))
                {
                    var hisCard = new MOS.MANAGER.HisCard.HisCardGet(param).GetCardByCode(sdo.CardCode);
                    if (hisCard != null)
                    {
                        if (hisCard.PATIENT_ID.HasValue)
                        {
                            hisPatient = new HisPatientGet().GetById(hisCard.PATIENT_ID.Value);
                        }

                        if (String.IsNullOrWhiteSpace(sdo.CardServiceCode))
                        {
                            sdo.CardServiceCode = hisCard.SERVICE_CODE;
                        }
                    }
                    hisPatient.HAS_CARD = Constant.IS_TRUE;
                    treatment.HAS_CARD = Constant.IS_TRUE;
                }

                //check neu khong co the thong minh ma co the bhyt ==> tim benh nhan co the tuong ung va kiem tra thong tin ten tuoi gioi tinh.
                if (!String.IsNullOrWhiteSpace(sdo.BhytNumber) && patientType.ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT && hisPatient.ID == 0)
                {
                    string sql = "SELECT * FROM HIS_PATIENT WHERE IS_ACTIVE=1 AND TDL_HEIN_CARD_NUMBER = :param1 ORDER BY ID DESC";
                    var lstPatient = DAOWorker.SqlDAO.GetSql<HIS_PATIENT>(sql, sdo.BhytNumber);
                    foreach (var item in lstPatient)
                    {
                        try
                        {
                            bool valid = true;
                            valid = valid && item.DOB == sdo.Dob;
                            valid = valid && item.GENDER_ID == gender.ID;
                            valid = valid && item.FIRST_NAME.Trim().ToLower() == sdo.FirstName.Trim().ToLower();
                            valid = valid && CheckLastName(item.LAST_NAME, sdo.LastName);
                            if (valid)
                            {
                                hisPatient = item;
                                break;
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                //check neu khong co the thong minh ma co the CccdNumber ==> tim benh nhan co CccdNumber tuong ung va kiem tra thong tin ten tuoi gioi tinh.
                if (!String.IsNullOrWhiteSpace(sdo.CccdNumber) && hisPatient.ID == 0)
                {
                    HisPatientAdvanceFilter filterPatient = new HisPatientAdvanceFilter() { CCCD_NUMBER__EXACT = sdo.CccdNumber };
                    var lstPatient = new HisPatientGet().GetSdoAdvance(filterPatient);
                    foreach (var item in lstPatient)
                    {
                        try
                        {
                            bool valid = true;
                            valid = valid && item.DOB == sdo.Dob;
                            valid = valid && item.GENDER_ID == gender.ID;
                            valid = valid && item.FIRST_NAME.Trim().ToLower() == sdo.FirstName.Trim().ToLower();
                            valid = valid && CheckLastName(item.LAST_NAME, sdo.LastName);
                            if (valid)
                            {
                                hisPatient = item;
                                break;
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                }

                ProcessUpadatePatientInfo(sdo, ref hisPatient);

                HisPatientProfileSDO patientProfile = new HisPatientProfileSDO();
                patientProfile.HisPatient = hisPatient;
                patientProfile.HisPatientTypeAlter = hisPatientTypeAlter;
                patientProfile.TreatmentTime = treatmentTime;
                patientProfile.HisTreatment = treatment;
                patientProfile.RequestRoomId = sdo.RequestRoomId;
                patientProfile.CardCode = sdo.CardCode;
                patientProfile.CardServiceCode = sdo.CardServiceCode;
                patientProfile.DepartmentId = room.DEPARTMENT_ID;

                result = new HisServiceReqExamRegisterSDO();
                result.InstructionTimes = new List<long>() {treatmentTime};
                result.InstructionTime = treatmentTime;
                result.HisPatientProfile = patientProfile;
                result.RequestRoomId = sdo.RequestRoomId;
                
                ServiceReqDetailSDO service = new ServiceReqDetailSDO();
                service.Amount = 1;
                service.ServiceId = sv != null ? sv.ID : 0;
                service.PatientTypeId = patientType.ID;
                service.RoomId = room.ID;
                result.ServiceReqDetails = new List<ServiceReqDetailSDO>() { service };
            }
            return result;
        }

        private string GetMediOrgNameByCode(string mediOrgCode)
        {
            string result = mediOrgCode;
            try
            {
                if (IsNotNullOrEmpty(mediOrgCode))
                {
                    var mediOrg = HisMediOrgCFG.DATA.Where(o => o.MEDI_ORG_CODE == mediOrgCode && o.IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE).FirstOrDefault();
                    if (mediOrg != null) result = mediOrg.MEDI_ORG_NAME;
                }
            }
            catch (Exception ex)
            {
                result = mediOrgCode;
                Inventec.Common.Logging.LogSystem.Error(String.Format("Khong lay duoc ten CSKCBBD theo ma: {0}", mediOrgCode), ex);
            }
            return result;
        }

        private void ProcessUpadatePatientInfo(HisExamRegisterDkkSDO sdo, ref HIS_PATIENT hisPatient)
        {
            if ((String.IsNullOrWhiteSpace(hisPatient.REGISTER_CODE) && !String.IsNullOrWhiteSpace(sdo.PeopleRegisterCode))
                || (!String.IsNullOrWhiteSpace(hisPatient.REGISTER_CODE) && !String.IsNullOrWhiteSpace(sdo.PeopleRegisterCode) && hisPatient.REGISTER_CODE != sdo.PeopleRegisterCode))
            {
                hisPatient.REGISTER_CODE = sdo.PeopleRegisterCode;
            }

            if ((String.IsNullOrWhiteSpace(hisPatient.CCCD_NUMBER) && !String.IsNullOrWhiteSpace(sdo.CccdNumber))
                || (!String.IsNullOrWhiteSpace(hisPatient.CCCD_NUMBER) && !String.IsNullOrWhiteSpace(sdo.CccdNumber)
                && (hisPatient.CCCD_NUMBER != sdo.CccdNumber || hisPatient.CMND_NUMBER != sdo.CccdNumber)))
            {
                if (sdo.CccdNumber.Length == 9 && String.IsNullOrWhiteSpace(hisPatient.CMND_NUMBER))
                {
                    hisPatient.CMND_NUMBER = sdo.CccdNumber;
                }
                else if (sdo.CccdNumber.Length == 12 && String.IsNullOrWhiteSpace(hisPatient.CCCD_NUMBER))
                {
                    hisPatient.CCCD_NUMBER = sdo.CccdNumber;
                }
            }

            if ((String.IsNullOrWhiteSpace(hisPatient.EMAIL) && !String.IsNullOrWhiteSpace(sdo.Email))
                || (!String.IsNullOrWhiteSpace(hisPatient.EMAIL) && !String.IsNullOrWhiteSpace(sdo.Email) && hisPatient.EMAIL != sdo.Email))
            {
                hisPatient.EMAIL = sdo.Email;
            }

            if ((String.IsNullOrWhiteSpace(hisPatient.PHONE) && !String.IsNullOrWhiteSpace(sdo.Phone))
               || (!String.IsNullOrWhiteSpace(hisPatient.PHONE) && !String.IsNullOrWhiteSpace(sdo.Phone) && hisPatient.PHONE != sdo.Phone))
            {
                hisPatient.PHONE = sdo.Phone;
            }

            if ((String.IsNullOrWhiteSpace(hisPatient.ETHNIC_CODE) && !String.IsNullOrWhiteSpace(sdo.EthnicCode))
               || (!String.IsNullOrWhiteSpace(hisPatient.ETHNIC_CODE) && !String.IsNullOrWhiteSpace(sdo.EthnicCode) && hisPatient.ETHNIC_CODE != sdo.EthnicCode))
            {
                hisPatient.ETHNIC_CODE = sdo.EthnicCode;
            }

            if ((String.IsNullOrWhiteSpace(hisPatient.ETHNIC_NAME) && !String.IsNullOrWhiteSpace(sdo.EthnicName))
               || (!String.IsNullOrWhiteSpace(hisPatient.ETHNIC_NAME) && !String.IsNullOrWhiteSpace(sdo.EthnicName) && hisPatient.ETHNIC_NAME != sdo.EthnicName))
            {
                hisPatient.ETHNIC_NAME = sdo.EthnicName;
            }

            if ((String.IsNullOrWhiteSpace(hisPatient.FIRST_NAME) && !String.IsNullOrWhiteSpace(sdo.FirstName))
               || (!String.IsNullOrWhiteSpace(hisPatient.FIRST_NAME) && !String.IsNullOrWhiteSpace(sdo.FirstName) && hisPatient.FIRST_NAME != sdo.FirstName))
            {
                hisPatient.FIRST_NAME = sdo.FirstName;
            }

            if ((String.IsNullOrWhiteSpace(hisPatient.LAST_NAME) && !String.IsNullOrWhiteSpace(sdo.LastName))
               || (!String.IsNullOrWhiteSpace(hisPatient.LAST_NAME) && !String.IsNullOrWhiteSpace(sdo.LastName) && hisPatient.LAST_NAME != sdo.LastName))
            {
                hisPatient.LAST_NAME = sdo.LastName;
            }

            HIS_GENDER gender = HisGenderCFG.DATA.Where(o => o.GENDER_CODE == sdo.GenderCode).FirstOrDefault();
            if (gender != null && ((hisPatient.GENDER_ID != null && gender.ID != null)
               || (hisPatient.GENDER_ID != null && gender.ID != null && hisPatient.GENDER_ID != gender.ID)))
            {
                hisPatient.GENDER_ID = gender.ID;
            }

            if ((hisPatient.DOB != null && sdo.Dob != null)
               || (hisPatient.DOB != null && sdo.Dob != null && hisPatient.DOB != sdo.Dob))
            {
                hisPatient.DOB = sdo.Dob;
            }
            //benh nhan cu khong co thong tin tinh, huyen, xa, dia chi
            bool validAddres = true;
            validAddres = validAddres && String.IsNullOrWhiteSpace(hisPatient.ADDRESS);
            validAddres = validAddres && String.IsNullOrWhiteSpace(hisPatient.COMMUNE_CODE);
            validAddres = validAddres && String.IsNullOrWhiteSpace(hisPatient.COMMUNE_NAME);
            validAddres = validAddres && String.IsNullOrWhiteSpace(hisPatient.DISTRICT_CODE);
            validAddres = validAddres && String.IsNullOrWhiteSpace(hisPatient.DISTRICT_NAME);
            validAddres = validAddres && String.IsNullOrWhiteSpace(hisPatient.PROVINCE_CODE);
            validAddres = validAddres && String.IsNullOrWhiteSpace(hisPatient.PROVINCE_NAME);

            if (validAddres)
            {
                hisPatient.ADDRESS = sdo.Address;
                hisPatient.COMMUNE_CODE = sdo.CommuneCode;
                hisPatient.COMMUNE_NAME = sdo.CommuneName;
                hisPatient.DISTRICT_CODE = sdo.DistrictCode;
                hisPatient.DISTRICT_NAME = sdo.DistrictName;
                hisPatient.PROVINCE_CODE = sdo.ProvinceCode;
                hisPatient.PROVINCE_NAME = sdo.ProvinceName;
            }
            else if (String.IsNullOrWhiteSpace(hisPatient.ADDRESS) && !String.IsNullOrWhiteSpace(sdo.Address))
            {
                hisPatient.ADDRESS = sdo.Address;
            }
        }

        private bool CheckLastName(string patientLastname, string dkkLastname)
        {
            bool result = true;
            try
            {
                if ((String.IsNullOrWhiteSpace(patientLastname) && !String.IsNullOrWhiteSpace(dkkLastname)) || (!String.IsNullOrWhiteSpace(patientLastname) && String.IsNullOrWhiteSpace(dkkLastname)))
                {
                    throw new Exception("Lastname khac nhau");
                }

                if (!String.IsNullOrWhiteSpace(patientLastname) && !String.IsNullOrWhiteSpace(dkkLastname))
                {
                    patientLastname = patientLastname.Trim();
                    dkkLastname = dkkLastname.Trim();
                    string[] splitPatient = patientLastname.Split(' ');
                    string[] splitDkk = dkkLastname.Split(' ');

                    if (splitPatient.Count() == splitDkk.Count())
                    {
                        for (int i = 0; i < splitPatient.Count(); i++)
                        {
                            if (splitPatient[i].ToLower() != splitDkk[i].ToLower())
                            {
                                throw new Exception("Lastname khac nhau");
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("Lastname khac nhau");
                    }
                }
            }
            catch (Exception ex)
            {
                result = false;
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
            throw new NotImplementedException();
        }

        private void ThreadUpdateServiceReqInfo(HisExamRegisterDkkSDO sdo, HisServiceReqExamRegisterResultSDO resultData)
        {
            try
            {
                foreach (var item in resultData.ServiceReqs)
                {
                    item.HOSPITALIZATION_REASON = sdo.RegisterReason;
                    item.NOTE = sdo.RegisterSymptom;
                }

                Task updateInfo = Task.Factory.StartNew((object obj) => UpdateServiceReqInfo(obj), resultData.ServiceReqs);
                updateInfo.Start();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void UpdateServiceReqInfo(object obj)
        {
            try
            {
                if (obj != null && obj.GetType() == typeof(List<V_HIS_SERVICE_REQ>))
                {
                    var datas = obj as List<V_HIS_SERVICE_REQ>;
                    List<string> sqls = new List<string>();
                    string sql = "UPDATE HIS_SERVICE_REQ SET HOSPITALIZATION_REASON='{0}', NOTE='{1}' WHERE ID={2}";
                    foreach (var item in datas)
                    {
                        sqls.Add(string.Format(sql, item.HOSPITALIZATION_REASON, item.NOTE, item.ID));
                    }

                    if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                    {
                        Inventec.Common.Logging.LogSystem.Error("Cap nhat his_service_req that bai.");
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
