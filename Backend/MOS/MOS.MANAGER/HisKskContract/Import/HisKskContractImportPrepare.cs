using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisKskService;
using MOS.MANAGER.HisPatient;
using MOS.MANAGER.HisServicePaty;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisKskContract.Import
{
    class HisKskContractImportPrepare : BusinessBase
    {
        internal HisKskContractImportPrepare()
            : base()
        {

        }

        internal HisKskContractImportPrepare(CommonParam param)
            : base(param)
        {

        }

        private bool VerifyRequireField(HisKskContractSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (data.KskContractId <= 0) throw new ArgumentNullException("data.KskContractId");
                if (!IsNotNullOrEmpty(data.KskPatients)) throw new ArgumentNullException("data.KskPatients");
                foreach (HisKskPatientSDO patient in data.KskPatients)
                {
                    if (patient == null) throw new ArgumentNullException("patientSdo");
                    if (patient.Patient == null) throw new ArgumentNullException("patientSdo.Patient");
                    if (patient.KskId <= 0) throw new ArgumentNullException("patientSdo.KskId");
                    if (patient.IntructionTime <= 0) throw new ArgumentNullException("patientSdo.IntructionTime");
                }
            }
            catch (ArgumentNullException ex)
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool Prepare(HisKskContractSDO data, WorkPlaceSDO workplace, ref List<PrepareData> prepareData)
        {
            try
            {
                bool valid = true;
                HIS_KSK_CONTRACT kskContract = null;
                HisKskContractCheck commonChecker = new HisKskContractCheck(param);
                valid = valid && this.VerifyRequireField(data);
                valid = valid && commonChecker.VerifyId(data.KskContractId, ref kskContract);
                valid = valid && commonChecker.IsUnLock(kskContract);
                valid = valid && this.IsValidContractTime(kskContract);

                if (valid)
                {
                    List<long> kskIds = data.KskPatients.Select(o => o.KskId).Distinct().ToList();
                    List<long> additionKskIds = data.KskPatients.Where(o => o.AdditionKskId.HasValue).Select(o => o.AdditionKskId.Value).Distinct().ToList();

                    List<long> ids = new List<long>();
                    ids.AddRange(kskIds);
                    if (IsNotNullOrEmpty(additionKskIds))
                    {
                        ids.AddRange(additionKskIds);
                    }

                    List<HIS_KSK_SERVICE> kskServices = new HisKskServiceGet().GetByKskIds(ids);

                    foreach (HisKskPatientSDO sdo in data.KskPatients)
                    {
                        bool validate = true;

                        List<HIS_KSK_SERVICE> usingKskServices = kskServices != null ? kskServices
                            .Where(o => o.KSK_ID == sdo.KskId
                                || (sdo.AdditionKskId.HasValue && sdo.AdditionServiceIds != null && o.KSK_ID == sdo.AdditionKskId.Value && sdo.AdditionServiceIds.Contains(o.SERVICE_ID)))
                            .ToList() : null;

                        PrepareData prData = new PrepareData();
                        prData.KskServices = usingKskServices;
                        prData.KskContract = kskContract;
                        prData.HisKskPatientSDO = sdo;

                        CommonParam pr = new CommonParam();
                        this.ValidateIntructionTime(pr, kskContract, sdo, ref validate);
                        this.ValidatePatientInfo(pr, sdo, prData, ref validate);
                        this.ValidateTreatmentInfo(kskContract, sdo, workplace, prData, ref validate);
                        this.ValidateRoom(pr, kskServices, sdo, ref validate);
                        this.ValidateServicePaty(pr, kskServices, sdo, workplace, ref validate);

                        if (!validate)
                        {
                            valid = false;
                            sdo.IsError = true;
                            sdo.Descriptions.AddRange(pr.Messages);
                        }
                        else
                        {
                            prepareData.Add(prData);
                        }
                    }
                    return valid;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return false;
        }

        private void ValidatePatientInfo(CommonParam param, HisKskPatientSDO sdo, PrepareData prepareData, ref bool validate)
        {
            if (!String.IsNullOrWhiteSpace(sdo.Patient.PATIENT_CODE))
            {
                HIS_PATIENT raw = new HisPatientGet().GetByCode(sdo.Patient.PATIENT_CODE);
                if (raw == null)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisContract_MaBenhNhanKhongChinhXac);
                    validate = false;
                }
                else
                {
                    prepareData.Patient = raw;
                }
            }
            else
            {
                bool valid = true;
                HisPatientCheck checker = new HisPatientCheck(new CommonParam());
                valid = valid && checker.VerifyRequireField(sdo.Patient);
                valid = valid && checker.ExistsStoreCode(sdo.Patient.PATIENT_STORE_CODE, null);

                if (!valid)
                {
                    List<string> bugCodes = checker.GetBugCodes();
                    List<string> messages = checker.GetMessages();
                    if (IsNotNullOrEmpty(bugCodes))
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.DuLieuDauVaoKhongHopLe);
                    }
                    if (IsNotNullOrEmpty(messages))
                    {
                        param.Messages.AddRange(messages);
                    }
                    validate = false;
                }
                else
                {
                    prepareData.Patient = sdo.Patient;
                }
            }
            if (IsNotNull(prepareData.Patient))
            {
                prepareData.Patient.HRM_EMPLOYEE_CODE = sdo.HrmEmployeeCode;
            }
        }

        private void ValidateTreatmentInfo(HIS_KSK_CONTRACT contract, HisKskPatientSDO sdo, WorkPlaceSDO workPlace, PrepareData prepareData, ref bool validate)
        {
            HIS_TREATMENT treat = new HIS_TREATMENT();
            treat.BRANCH_ID = workPlace.BranchId;
            treat.PATIENT_ID = prepareData != null && prepareData.Patient != null ? prepareData.Patient.ID : 0;
            treat.IN_TIME = sdo.IntructionTime;
            treat.KSK_ORDER = sdo.KskOrder;
            treat.HRM_KSK_CODE = sdo.HrmKskCode;
            treat.TDL_KSK_CONTRACT_IS_RESTRICTED = contract.IS_RESTRICTED;

            HIS_PATIENT_TYPE_ALTER pta = new HIS_PATIENT_TYPE_ALTER();
            pta.PATIENT_TYPE_ID = HisPatientTypeCFG.PATIENT_TYPE_ID__KSK;
            pta.TREATMENT_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__KHAM;
            pta.KSK_CONTRACT_ID = contract.ID;
            pta.LOG_TIME = sdo.IntructionTime;
            pta.EXECUTE_ROOM_ID = workPlace.RoomId;

            HisTreatmentCreate checker = new HisTreatmentCreate(new CommonParam());

            if (!checker.ValidateBeforeCreate(treat, prepareData.Patient, pta, treat.PATIENT_ID <= 0))
            {
                sdo.IsError = true;
                List<string> bugCodes = checker.GetBugCodes();
                List<string> messages = checker.GetMessages();
                if (IsNotNullOrEmpty(bugCodes))
                {
                    sdo.Descriptions.Add(MessageUtil.GetMessage(LibraryMessage.Message.Enum.DuLieuDauVaoKhongHopLe, param.LanguageCode));
                }
                if (IsNotNullOrEmpty(messages))
                {
                    sdo.Descriptions.AddRange(messages);
                }
            }
            else
            {
                prepareData.PatientTypeAlter = pta;
                prepareData.Treatment = treat;
            }
        }

        private void ValidateServiceReqInfo(List<HIS_KSK_SERVICE> kskServices, HisKskPatientSDO sdo, WorkPlaceSDO workPlace, ref bool validate)
        {
            bool valid = true;
            HisTreatmentCreate checker = new HisTreatmentCreate(new CommonParam());

            if (!valid)
            {
                sdo.IsError = true;
                List<string> bugCodes = checker.GetBugCodes();
                List<string> messages = checker.GetMessages();
                if (IsNotNullOrEmpty(bugCodes))
                {
                    sdo.Descriptions.Add(MessageUtil.GetMessage(LibraryMessage.Message.Enum.DuLieuDauVaoKhongHopLe, param.LanguageCode));
                }
                if (IsNotNullOrEmpty(messages))
                {
                    sdo.Descriptions.AddRange(messages);
                }
            }
        }

        private void ValidateRoom(CommonParam param, List<HIS_KSK_SERVICE> kskServices, HisKskPatientSDO sdo, ref bool validate)
        {
            if (IsNotNullOrEmpty(kskServices))
            {
                List<HIS_KSK_SERVICE> invalids = kskServices.Where(o => HisServiceRoomCFG.DATA_VIEW == null || !HisServiceRoomCFG.DATA_VIEW.Exists(t => t.SERVICE_ID == o.SERVICE_ID && t.ROOM_ID == o.ROOM_ID && o.IS_ACTIVE == Constant.IS_TRUE)).ToList();
                if (IsNotNullOrEmpty(invalids))
                {
                    foreach (HIS_KSK_SERVICE s in invalids)
                    {
                        string serviceName = HisServiceCFG.DATA_VIEW.Where(o => o.ID == s.SERVICE_ID).Select(o => o.SERVICE_NAME).FirstOrDefault();
                        string roomName = HisRoomCFG.DATA.Where(o => o.ID == s.ROOM_ID).Select(o => o.ROOM_NAME).FirstOrDefault();
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_DichVuKhongTheThucHienTaiPhong, serviceName, roomName);
                        validate = false;
                    }
                }
            }
            else
            {
                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisKskContrack_KhongCoNhomDichVuTuongUng);
                validate = false;
            }
        }

        private void ValidateServicePaty(CommonParam param, List<HIS_KSK_SERVICE> kskServices, HisKskPatientSDO sdo, WorkPlaceSDO workPlace, ref bool validate)
        {
            if (IsNotNullOrEmpty(kskServices))
            {
                foreach (HIS_KSK_SERVICE s in kskServices)
                {
                    //Lay thong tin chinh sach gia duoc ap dung cho sere_serv
                    V_HIS_ROOM executeRoom = HisRoomCFG.DATA.Where(o => o.ID == s.ROOM_ID).FirstOrDefault();

                    V_HIS_SERVICE_PATY appliedServicePaty = new HisServicePatyGet().GetApplied(executeRoom.BRANCH_ID, s.ROOM_ID, workPlace.RoomId, workPlace.DepartmentId, HisServicePatyCFG.DATA, sdo.IntructionTime, sdo.IntructionTime, s.SERVICE_ID, HisPatientTypeCFG.PATIENT_TYPE_ID__KSK);
                    if (appliedServicePaty == null)
                    {
                        HIS_PATIENT_TYPE hisPatientType = HisPatientTypeCFG.DATA.Where(o => o.ID == HisPatientTypeCFG.PATIENT_TYPE_ID__KSK).FirstOrDefault();
                        V_HIS_SERVICE hisService = HisServiceCFG.DATA_VIEW.Where(o => o.ID == s.SERVICE_ID).FirstOrDefault();
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServicePaty_KhongTonTaiDuLieuPhuHop, hisService.SERVICE_NAME, hisService.SERVICE_TYPE_CODE, hisPatientType.PATIENT_TYPE_NAME);
                        validate = false;
                    }
                }
            }
        }

        private bool IsValidContractTime(HIS_KSK_CONTRACT kskContract)
        {
            if (kskContract.EXPIRY_DATE.HasValue)
            {
                long date = Convert.ToInt64(DateTime.Now.ToString("yyyyMMdd") + "000000");
                if (date > kskContract.EXPIRY_DATE.Value)
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisKskContract_HopDongKSKDaHetHieuLuc);
                    return false;
                }
            }
            return true;
        }

        private void ValidateIntructionTime(CommonParam param, HIS_KSK_CONTRACT kskContract, HisKskPatientSDO data, ref bool validate)
        {
            if (kskContract.EFFECT_DATE.HasValue || kskContract.EXPIRY_DATE.HasValue)
            {
                if (kskContract.EFFECT_DATE.HasValue && kskContract.EFFECT_DATE.Value > data.IntructionTime)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisKskContract_ThoiGianYLenhBeHoiThoiGianHieuLuc);
                    validate = false;
                }

                if (kskContract.EXPIRY_DATE.HasValue && kskContract.EXPIRY_DATE.Value < data.IntructionTime)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisKskContract_ThoiGianYLenhLonHoiThoiGianHetHan);
                    validate = false;
                }
            }
        }
    }
}
