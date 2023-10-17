using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisDepartment;
using MOS.MANAGER.HisExecuteRoom;
using MOS.MANAGER.HisKskContract;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServBill;
using MOS.MANAGER.HisSereServDebt;
using MOS.MANAGER.HisSereServDeposit;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisServiceReq.Common;
using MOS.MANAGER.HisSeseDepoRepay;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.HisTestSampleType;
using MOS.TDO;
using System;
using System.Collections.Generic;
using System.Linq;
using MOS.UTILITY;
using MOS.MANAGER.HisSereServTein;
using MOS.MANAGER.Config.CFG;
using MOS.MANAGER.HisPaanPosition;

namespace MOS.MANAGER.HisServiceReq.Test
{
    partial class HisServiceReqTestGet : GetBase
    {
        internal HisServiceReqTestGet()
            : base()
        {

        }

        internal HisServiceReqTestGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HisTestServiceReqTDO> GetTdo(long createTimeFrom, long createTimeTo, bool? isSpecimen, string roomTypeCode, string kskContractCode, string executeDepartmentCode, bool? hasContract)
        {
            return this.GetTdo(createTimeFrom, createTimeTo, isSpecimen, null, roomTypeCode, null, kskContractCode, executeDepartmentCode, hasContract);
        }

        internal List<HisTestServiceReqTDO> GetTdo(long createTimeFrom, long createTimeTo, bool? isSpecimen, string roomTypeCode)
        {
            return this.GetTdo(createTimeFrom, createTimeTo, isSpecimen, null, roomTypeCode, null, null, null, null);
        }

        internal HisTestServiceReqTDO GetTdo(string serviceReqCode, bool? isSpecimen)
        {
            List<HisTestServiceReqTDO> res = this.GetTdo(null, null, isSpecimen, serviceReqCode, null, null, null, null, null);
            return IsNotNullOrEmpty(res) ? res[0] : null;
        }

        internal List<HisTestServiceReqTDO> GetTdoByTurnCode(string turnCode, bool? isSpecimen)
        {
            if (String.IsNullOrWhiteSpace(turnCode)) return null;
            return this.GetTdo(null, null, isSpecimen, null, null, turnCode, null, null, null);
        }

        internal List<HisTestServiceReqTDO> GetTdoByTreatmentCode(string treatmentCode, bool? isSpecimen)
        {
            if (String.IsNullOrWhiteSpace(treatmentCode)) return null;
            return this.GetTdo(null, null, isSpecimen, null, null, null, null, null, null, treatmentCode);
        }

        /// <summary>
        /// Phuc vu cung cap api cho 3rd-party
        /// </summary>
        /// <param name="createTimeFrom"></param>
        /// <param name="createTimeTo"></param>
        /// <returns></returns>
        private List<HisTestServiceReqTDO> GetTdo(long? createTimeFrom, long? createTimeTo, bool? isSpecimen, string serviceReqCode, string roomTypeCode, string turnCode, string kskContractCode, string executeDepartmentCode, bool? hasContract, string treatmentCode = null)
        {
            try
            {
                List<HisTestServiceReqTDO> result = null;
                List<long> roomIds = null;
                if (!string.IsNullOrWhiteSpace(roomTypeCode))
                {
                    roomIds = HisExecuteRoomCFG.DATA.Where(o => o.TEST_TYPE_CODE == roomTypeCode).Select(o => o.ROOM_ID).ToList();
                }

                long? kskContractId = null;
                if (!String.IsNullOrWhiteSpace(kskContractCode))
                {
                    //gan 0 de neu sai ma hop dong thi khong tim duoc du lieu
                    kskContractId = 0;
                    HIS_KSK_CONTRACT kskContract = new HisKskContractGet().GetByCode(kskContractCode);
                    if (IsNotNull(kskContract))
                    {
                        kskContractId = kskContract.ID;
                    }
                }

                long? departmentId = null;
                if (!String.IsNullOrWhiteSpace(executeDepartmentCode))
                {
                    //gan 0 de neu sai ma khoa thi khong tim duoc du lieu
                    departmentId = 0;
                    HIS_DEPARTMENT department = HisDepartmentCFG.DATA.FirstOrDefault(o => o.DEPARTMENT_CODE == executeDepartmentCode);
                    if (IsNotNull(department))
                    {
                        departmentId = department.ID;
                    }
                }

                List<HIS_SERVICE_REQ> hisServiceReqs = this.GetData(createTimeFrom, createTimeTo, serviceReqCode, turnCode, treatmentCode, roomIds, kskContractId, departmentId, hasContract);

                if (IsNotNullOrEmpty(hisServiceReqs))
                {
                    List<long> serviceReqIds = hisServiceReqs.Select(o => o.ID).ToList();
                    List<long> svReqIds = hisServiceReqs.Where(o => o.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL).Select(o => o.ID).ToList();

                    List<HIS_SERE_SERV> vHisSereServs = new HisSereServGet().GetByServiceReqIdsAndIsSpecimen(serviceReqIds, isSpecimen);

                    if (!IsNotNullOrEmpty(vHisSereServs))
                    {
                        if (!isSpecimen.HasValue)
                        {
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_KhongCoDuLieuChiTiet);
                            return null;
                        }

                        if (isSpecimen.HasValue && isSpecimen.Value)
                        {
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_KhongCoDuLieuChiTietHoacChuaDuocLayMau);
                            return null;
                        }

                        if (isSpecimen.HasValue && !isSpecimen.Value)
                        {
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_KhongCoDuLieuChiTietHoacDaDuocLayMau);
                            return null;
                        }
                    }

                    List<long> treatmentIds = vHisSereServs != null ? vHisSereServs
                        .Where(o => o.TDL_TREATMENT_ID.HasValue)
                        .Select(o => o.TDL_TREATMENT_ID.Value).Distinct().ToList() : null;

                    List<long> checkTreatmentIds = hisServiceReqs.Where(o => o.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL).Select(o => o.TREATMENT_ID).ToList();

                    List<V_HIS_TREATMENT_FEE_1> treatments = new HisTreatmentGet().GetFeeView1ByIds(checkTreatmentIds);
                    List<HIS_PATIENT_TYPE_ALTER> patientTypeAlters = new HisPatientTypeAlterGet().GetByTreatmentIds(treatmentIds);

                    List<long> sereServServiceReqIds = vHisSereServs.Where(o => o.SERVICE_REQ_ID.HasValue).Select(o => o.SERVICE_REQ_ID.Value).ToList();
                    List<long> srIds = hisServiceReqs.Where(o => o.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__DXL).Select(o => o.ID).ToList();

                    List<HIS_KSK_CONTRACT> kskContracts = null;
                    List<long> kskContractIds = hisServiceReqs.Where(o => o.TDL_KSK_CONTRACT_ID.HasValue).Select(s => s.TDL_KSK_CONTRACT_ID.Value).Distinct().ToList();
                    if (kskContractIds != null && kskContractIds.Count > 0)
                    {
                        kskContracts = new HisKskContractGet().GetByIds(kskContractIds);
                    }

                    List<HIS_PAAN_POSITION> paanPositions = null;
                    List<long> paanPositionIds = hisServiceReqs.Where(o => o.PAAN_POSITION_ID.HasValue).Select(s => s.PAAN_POSITION_ID.Value).Distinct().ToList();
                    if (paanPositionIds != null && paanPositionIds.Count > 0)
                    {
                        paanPositions = new HisPaanPositionGet().GetByIds(paanPositionIds);
                    }

                    List<long> sereServIds = vHisSereServs.Where(o => svReqIds.Contains(o.SERVICE_REQ_ID ?? 0)).Select(o => o.ID).ToList();

                    List<HIS_SERE_SERV_BILL> bills = new HisSereServBillGet().GetNoCancelBySereServIds(sereServIds);
                    List<HIS_SERE_SERV_DEPOSIT> deposits = new HisSereServDepositGet().GetNoCancelBySereServIds(sereServIds);
                    List<HIS_SERE_SERV_DEBT> depts = new HisSereServDebtGet().GetNoCancelBySereServIds(sereServIds);
                    List<long> sereServDepositIds = deposits != null ? deposits.Select(o => o.ID).ToList() : null;
                    List<HIS_SESE_DEPO_REPAY> repays = new HisSeseDepoRepayGet().GetNoCancelBySereServDepositIds(sereServDepositIds);

                    HisPatientTypeAlterGet patientTypeAlterGet = new HisPatientTypeAlterGet();
                    HisTestSampleTypeGet testSampleTypeGet = new HisTestSampleTypeGet();
                    result = new List<HisTestServiceReqTDO>();

                    foreach (HIS_SERVICE_REQ sr in hisServiceReqs)
                    {
                        List<HIS_SERE_SERV> ss = vHisSereServs != null ? vHisSereServs.Where(o => o.SERVICE_REQ_ID == sr.ID).ToList() : null;

                        //Neu he thong co cau hinh khong cho phep lay cac XN chua du vien phi
                        if (LisCFG.LIS_FORBID_NOT_ENOUGH_FEE && sr.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL)
                        {
                            V_HIS_TREATMENT_FEE_1 treat = treatments != null ? treatments.Where(o => o.ID == sr.TREATMENT_ID).FirstOrDefault() : null;
                            HIS_SERVICE_REQ req = Mapper.Map<HIS_SERVICE_REQ>(sr);
                            //Neu dich vu ko cho phep bat dau thi bo qua
                            if (IsNotNullOrEmpty(ss) && treat != null && !SendIntegratorCheck.IsAllowSend(treat, req, ss, bills, deposits, repays, depts))
                            {
                                //Chi bo sung message khi tim kiem theo ma
                                if (!string.IsNullOrWhiteSpace(serviceReqCode) || !string.IsNullOrWhiteSpace(turnCode))
                                {
                                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_NoVienPhi);
                                }

                                continue;
                            }
                        }

                        if (IsNotNullOrEmpty(ss) && (sereServServiceReqIds.Contains(sr.ID) || srIds.Contains(sr.ID)))
                        {
                            HIS_PATIENT_TYPE_ALTER patientTypeAlter = patientTypeAlterGet
                                .GetApplied(sr.TREATMENT_ID, sr.INTRUCTION_TIME, patientTypeAlters);
                            HIS_TEST_SAMPLE_TYPE testSampleType = sr.TEST_SAMPLE_TYPE_ID.HasValue ? HisTestSampleTypeCFG.DATA.Where(o => o.ID == sr.TEST_SAMPLE_TYPE_ID.Value).FirstOrDefault() : null;
                            HisTestServiceReqTDO tdo = this.MakeData(patientTypeAlter, testSampleType, ss, sr, kskContracts, paanPositions);
                            result.Add(tdo);
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        private List<HIS_SERVICE_REQ> GetData(long? createTimeFrom, long? createTimeTo, string serviceReqCode, string turnCode, string treatmentCode, List<long> executeRoomIds, long? kskContractId, long? departmentId, bool? hasContract)
        {
            HisServiceReqFilterQuery filter = new HisServiceReqFilterQuery();
            filter.INTRUCTION_DATE_FROM = createTimeFrom.HasValue ? Inventec.Common.DateTime.Get.StartDay(createTimeFrom.Value) : null;
            filter.INTRUCTION_DATE_TO = createTimeTo.HasValue ? Inventec.Common.DateTime.Get.StartDay(createTimeTo.Value) : null;
            filter.TDL_KSK_CONTRACT_ID = kskContractId;
            filter.SERVICE_REQ_STT_IDs = new List<long>() { IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL, IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__DXL };
            filter.SERVICE_REQ_TYPE_IDs = new List<long>() { IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__XN, IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__GPBL };

            if (LisLabconnCFG.IS_USING_SID_INSTEAD_OF_SERVICE_REQ_CODE)
            {
                filter.BARCODE__EXACT = serviceReqCode;
            }
            else
            {
                filter.SERVICE_REQ_CODE__EXACT = serviceReqCode;
            }

            filter.ASSIGN_TURN_CODE__EXACT = turnCode;
            filter.TREATMENT_CODE__EXACT = treatmentCode;
            filter.EXECUTE_ROOM_IDs = executeRoomIds;
            filter.IS_NO_EXECUTE = false;

            filter.INTRUCTION_TIME_FROM = createTimeFrom;
            if (IntegratedSystemCFG.INTRUCTION_TIME_MUST_BE_LESS_THAN_SYSTEM_TIME_OPTION)
            {
                long thoiGianHtai = Inventec.Common.DateTime.Get.Now() ?? 0;
                filter.INTRUCTION_TIME_TO = thoiGianHtai;
            }
            else
            {
                filter.INTRUCTION_TIME_TO = createTimeTo;
            }
            filter.EXECUTE_DEPARTMENT_ID = departmentId;
            filter.HAS_TDL_KSK_CONTRACT_ID = hasContract;

            return new HisServiceReqGet().Get(filter);
        }

        private HisTestServiceReqTDO MakeData(HIS_PATIENT_TYPE_ALTER patientTypeAlter, HIS_TEST_SAMPLE_TYPE testSampleType, List<HIS_SERE_SERV> vHisSereServs, HIS_SERVICE_REQ o, List<HIS_KSK_CONTRACT> kskContracts, List<HIS_PAAN_POSITION> paanPositions)
        {
            HIS_DEPARTMENT department = HisDepartmentCFG.DATA.Where(t => t.ID == o.REQUEST_DEPARTMENT_ID).FirstOrDefault();
            HIS_DEPARTMENT exeDepart = HisDepartmentCFG.DATA.Where(t => t.ID == o.EXECUTE_DEPARTMENT_ID).FirstOrDefault();

            HisTestServiceReqTDO tdo = new HisTestServiceReqTDO();
            if (patientTypeAlter != null)
            {
                HIS_PATIENT_TYPE patientType = HisPatientTypeCFG.DATA.Where(t => t.ID == patientTypeAlter.PATIENT_TYPE_ID).FirstOrDefault();
                HIS_TREATMENT_TYPE treatmentType = HisTreatmentTypeCFG.DATA.Where(t => t.ID == patientTypeAlter.TREATMENT_TYPE_ID).FirstOrDefault();
                tdo.PatientTypeName = patientType.PATIENT_TYPE_NAME;
                tdo.PatientTypeCode = patientType.PATIENT_TYPE_CODE;
                tdo.TreatmentTypeCode = treatmentType.TREATMENT_TYPE_CODE;
                tdo.TreatmentTypeName = treatmentType.TREATMENT_TYPE_NAME;
                tdo.HeinCardNumber = patientTypeAlter.HEIN_CARD_NUMBER;
                tdo.HeinMediOrgCode = patientTypeAlter.HEIN_MEDI_ORG_CODE;
                tdo.HeinMediOrgName = patientTypeAlter.HEIN_MEDI_ORG_NAME;
                tdo.HeinCardFromTime = patientTypeAlter.HEIN_CARD_FROM_TIME;
                tdo.HeinCardToTime = patientTypeAlter.HEIN_CARD_TO_TIME;
            }
            V_HIS_ROOM room = HisRoomCFG.DATA.Where(x => x.ID == o.REQUEST_ROOM_ID).FirstOrDefault();
            V_HIS_ROOM exeRoom = HisRoomCFG.DATA.FirstOrDefault(r => r.ID == o.EXECUTE_ROOM_ID);

            tdo.Address = o.TDL_PATIENT_ADDRESS;
            tdo.DateOfBirth = o.TDL_PATIENT_DOB;
            tdo.Gender = o.TDL_PATIENT_GENDER_NAME;
            tdo.IcdCode = o.ICD_CODE;
            tdo.IcdName = o.ICD_NAME;
            tdo.PatientCode = o.TDL_PATIENT_CODE;
            tdo.PatientName = o.TDL_PATIENT_NAME;
            tdo.SamplingNumOrder = o.CALL_SAMPLE_ORDER;
            tdo.IcdSubCode = o.ICD_SUB_CODE;
            tdo.IcdText = o.ICD_TEXT;

            if (LisLabconnCFG.IS_USING_SID_INSTEAD_OF_SERVICE_REQ_CODE)
            {
                tdo.ServiceReqCode = o.BARCODE;
            }
            else
            {
                tdo.ServiceReqCode = o.SERVICE_REQ_CODE;
            }
            tdo.OriginalServiceReqCode = o.SERVICE_REQ_CODE;
            tdo.CreateTime = o.CREATE_TIME.Value;
            tdo.InstructionTime = o.INTRUCTION_TIME;
            tdo.RequestLoginName = o.REQUEST_LOGINNAME;
            tdo.RequestUserName = o.REQUEST_USERNAME;
            string reqDepartmentCodePrefix = LisLabconnCFG.REQUEST_DEPRTMENT_CODE_PREFIX != null ? LisLabconnCFG.REQUEST_DEPRTMENT_CODE_PREFIX : "";
            string reqRoomCodePrefix = LisLabconnCFG.REQUEST_ROOM_CODE_PREFIX != null ? LisLabconnCFG.REQUEST_ROOM_CODE_PREFIX : "";

            tdo.RequestRoomName = room != null ? room.ROOM_NAME : null;
            tdo.RequestRoomCode = room != null ? reqRoomCodePrefix + room.ROOM_CODE : null;
            tdo.RequestDepartmentCode = reqDepartmentCodePrefix + department.DEPARTMENT_CODE;
            tdo.RequestDepartmentName = department.DEPARTMENT_NAME;
            tdo.TreatmentCode = o.TDL_TREATMENT_CODE;
            tdo.TurnCode = o.ASSIGN_TURN_CODE;
            tdo.ExecuteRoomName = exeRoom != null ? exeRoom.ROOM_NAME : null;
            tdo.ExecuteRoomCode = exeRoom != null ? exeRoom.ROOM_CODE : null;
            tdo.ExecuteDepartmentCode = exeDepart != null ? exeDepart.DEPARTMENT_CODE : null;
            tdo.ExecuteDepartmentName = exeDepart != null ? exeDepart.DEPARTMENT_NAME : null;
            tdo.PhoneNumber = !string.IsNullOrEmpty(o.TDL_PATIENT_MOBILE) ? o.TDL_PATIENT_MOBILE : o.TDL_PATIENT_PHONE;
            tdo.TestSampleTypeCode = IsNotNull(testSampleType) ? testSampleType.TEST_SAMPLE_TYPE_CODE : null;
            tdo.TestSampleTypeName = IsNotNull(testSampleType) ? testSampleType.TEST_SAMPLE_TYPE_NAME : null;
            tdo.CmndNumber = o.TDL_PATIENT_CMND_NUMBER;
            tdo.CccdNumber = o.TDL_PATIENT_CCCD_NUMBER;
            tdo.PassportNumber = o.TDL_PATIENT_PASSPORT_NUMBER;
            tdo.NationalName = o.TDL_PATIENT_NATIONAL_NAME;
            //cap cuu va uu tien -> cap cuu -> uu tien-> thuong
            tdo.Priority = (o.PRIORITY ?? 0) + (o.IS_EMERGENCY == Constant.IS_TRUE ? 2 : 0);

            HIS_KSK_CONTRACT kskContract = (o.TDL_KSK_CONTRACT_ID.HasValue && kskContracts != null) ? kskContracts.FirstOrDefault(e => e.ID == o.TDL_KSK_CONTRACT_ID.Value) : null;
            tdo.KskContractCode = IsNotNull(kskContract) ? kskContract.KSK_CONTRACT_CODE : null;

            tdo.OriginalBarcode = o.BARCODE;
            tdo.SampleTime = o.SAMPLE_TIME;
            tdo.SampleLoginName = o.SAMPLER_LOGINNAME;
            tdo.SampleUserName = o.SAMPLER_USERNAME;
            tdo.ReceiveSampleLoginname = o.RECEIVE_SAMPLE_LOGINNAME;
            tdo.ReceiveSampleUsername = o.RECEIVE_SAMPLE_USERNAME;
            tdo.ReceiveSampleTime = o.RECEIVE_SAMPLE_TIME;

            HIS_PAAN_POSITION paanPosition = (o.PAAN_POSITION_ID.HasValue && paanPositions != null) ? paanPositions.FirstOrDefault(e => e.ID == o.PAAN_POSITION_ID.Value) : null;
            tdo.PaanPositionCode = IsNotNull(paanPosition) ? paanPosition.PAAN_POSITION_CODE : null;
            tdo.PaanPositionName = IsNotNull(paanPosition) ? paanPosition.PAAN_POSITION_NAME : null;

            tdo.TestServiceTypeList = vHisSereServs
                .Select(s => new HisTestServiceTypeTDO
                {
                    TestServiceTypeCode = s.TDL_SERVICE_CODE,
                    TestServiceTypeName = s.TDL_SERVICE_NAME,
                    IsSpecimen = s.IS_SPECIMEN.HasValue
                        && s.IS_SPECIMEN.Value == MOS.UTILITY.Constant.IS_TRUE,
                    SearchCode = string.Format("{0}-{1}-{2}", s.TDL_TREATMENT_CODE, s.TDL_SERVICE_REQ_CODE, s.ID)
                }).ToList();
            return tdo;
        }

        internal HisTestDetailTDO GetDetailBySearchCode(string searchCode)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(searchCode))
                {
                    return null;
                }

                string[] splitData = searchCode.Split('-');

                if (splitData.Length < 3)
                {
                    return null;
                }

                if (String.IsNullOrWhiteSpace(splitData[0]) || String.IsNullOrWhiteSpace(splitData[1]) || String.IsNullOrWhiteSpace(splitData[2]))
                {
                    return null;
                }

                HisSereServFilterQuery ssFilter = new HisSereServFilterQuery();
                ssFilter.TDL_TREATMENT_CODE_EXACT = splitData[0];
                ssFilter.TDL_SERVICE_REQ_CODE_EXACT = splitData[1];
                ssFilter.ID = long.Parse(splitData[2]);
                ssFilter.HAS_EXECUTE = true;
                List<HIS_SERE_SERV> listSereServ = new HisSereServGet().Get(ssFilter);
                if (IsNotNullOrEmpty(listSereServ))
                {
                    HIS_SERE_SERV sereServ = listSereServ.First();
                    HIS_TREATMENT treatment = new HisTreatmentGet().GetById(sereServ.TDL_TREATMENT_ID ?? 0);
                    HIS_SERVICE_REQ serviceReq = new HisServiceReqGet().GetById(sereServ.SERVICE_REQ_ID ?? 0);
                    List<V_HIS_SERE_SERV_TEIN> listSereServTein = new HisSereServTeinGet().GetViewBySereServIds(new List<long> { sereServ.ID });

                    return this.MakeDataDetail(treatment, serviceReq, sereServ, listSereServTein);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return null;
        }

        private HisTestDetailTDO MakeDataDetail(HIS_TREATMENT treatment, HIS_SERVICE_REQ serviceReq, HIS_SERE_SERV sereServ, List<V_HIS_SERE_SERV_TEIN> listSereServTein)
        {

            HisTestDetailTDO tdo = new HisTestDetailTDO();
            if (treatment != null)
            {
                HIS_PATIENT_TYPE patientType = HisPatientTypeCFG.DATA.FirstOrDefault(t => t.ID == treatment.TDL_PATIENT_TYPE_ID);
                HIS_TREATMENT_TYPE treatmentType = HisTreatmentTypeCFG.DATA.FirstOrDefault(t => t.ID == treatment.TDL_TREATMENT_TYPE_ID);
                tdo.PatientTypeName = patientType.PATIENT_TYPE_NAME;
                tdo.PatientTypeCode = patientType.PATIENT_TYPE_CODE;
                tdo.TreatmentTypeCode = treatmentType.TREATMENT_TYPE_CODE;
                tdo.TreatmentTypeName = treatmentType.TREATMENT_TYPE_NAME;
                tdo.HeinCardNumber = treatment.TDL_HEIN_CARD_NUMBER;
                tdo.HeinMediOrgCode = treatment.TDL_HEIN_MEDI_ORG_CODE;
                tdo.HeinMediOrgName = treatment.TDL_HEIN_MEDI_ORG_NAME;
                tdo.Address = treatment.TDL_PATIENT_ADDRESS;
                tdo.DateOfBirth = treatment.TDL_PATIENT_DOB;
                tdo.Gender = treatment.TDL_PATIENT_GENDER_NAME;
                tdo.PatientCode = treatment.TDL_PATIENT_CODE;
                tdo.PatientName = treatment.TDL_PATIENT_NAME;
                tdo.PhoneNumber = !string.IsNullOrEmpty(treatment.TDL_PATIENT_MOBILE) ? treatment.TDL_PATIENT_MOBILE : treatment.TDL_PATIENT_PHONE;
                tdo.CmndNumber = treatment.TDL_PATIENT_CMND_NUMBER;
                tdo.CccdNumber = treatment.TDL_PATIENT_CCCD_NUMBER;
                tdo.PassportNumber = treatment.TDL_PATIENT_PASSPORT_NUMBER;
                tdo.NationalName = treatment.TDL_PATIENT_NATIONAL_NAME;
                tdo.TreatmentCode = treatment.TREATMENT_CODE;
            }

            if (serviceReq != null)
            {
                V_HIS_ROOM room = HisRoomCFG.DATA.FirstOrDefault(x => x.ID == serviceReq.REQUEST_ROOM_ID);
                V_HIS_ROOM exeRoom = HisRoomCFG.DATA.FirstOrDefault(r => r.ID == serviceReq.EXECUTE_ROOM_ID);

                string reqDepartmentCodePrefix = LisLabconnCFG.REQUEST_DEPRTMENT_CODE_PREFIX != null ? LisLabconnCFG.REQUEST_DEPRTMENT_CODE_PREFIX : "";
                string reqRoomCodePrefix = LisLabconnCFG.REQUEST_ROOM_CODE_PREFIX != null ? LisLabconnCFG.REQUEST_ROOM_CODE_PREFIX : "";

                if (LisLabconnCFG.IS_USING_SID_INSTEAD_OF_SERVICE_REQ_CODE)
                {
                    tdo.ServiceReqCode = serviceReq.BARCODE;
                }
                else
                {
                    tdo.ServiceReqCode = serviceReq.SERVICE_REQ_CODE;
                }

                tdo.OriginalServiceReqCode = serviceReq.SERVICE_REQ_CODE;
                tdo.CreateTime = serviceReq.CREATE_TIME.Value;
                tdo.InstructionTime = serviceReq.INTRUCTION_TIME;
                tdo.RequestLoginName = serviceReq.REQUEST_LOGINNAME;
                tdo.RequestUserName = serviceReq.REQUEST_USERNAME;
                tdo.TurnCode = serviceReq.ASSIGN_TURN_CODE;
                //cap cuu va uu tien -> cap cuu -> uu tien-> thuong
                tdo.Priority = (serviceReq.PRIORITY ?? 0) + (serviceReq.IS_EMERGENCY == Constant.IS_TRUE ? 2 : 0);
                tdo.SamplingNumOrder = serviceReq.CALL_SAMPLE_ORDER;
                tdo.IcdCode = serviceReq.ICD_CODE;
                tdo.IcdName = serviceReq.ICD_NAME;
                tdo.ExecuteLoginName = serviceReq.EXECUTE_LOGINNAME;
                tdo.ExecuteUserName = serviceReq.EXECUTE_USERNAME;
                tdo.StartTime = serviceReq.START_TIME;
                tdo.FinishTime = serviceReq.FINISH_TIME;
                tdo.IcdSubCode = serviceReq.ICD_SUB_CODE;
                tdo.IcdText = serviceReq.ICD_TEXT;
                tdo.SampleTime = serviceReq.SAMPLE_TIME;
                tdo.SampleLoginName = serviceReq.SAMPLER_LOGINNAME;
                tdo.SampleUserName = serviceReq.SAMPLER_USERNAME;
                tdo.ReceiveSampleTime = serviceReq.RECEIVE_SAMPLE_TIME;
                tdo.ReceiveSampleLoginname = serviceReq.RECEIVE_SAMPLE_LOGINNAME;
                tdo.ReceiveSampleUsername = serviceReq.RECEIVE_SAMPLE_USERNAME;

                if (room != null)
                {
                    tdo.RequestRoomName = room.ROOM_NAME;
                    tdo.RequestRoomCode = reqRoomCodePrefix + room.ROOM_CODE;
                    tdo.RequestDepartmentCode = reqDepartmentCodePrefix + room.DEPARTMENT_CODE;
                    tdo.RequestDepartmentName = room.DEPARTMENT_NAME;
                }

                if (exeRoom != null)
                {
                    tdo.ExecuteRoomName = exeRoom.ROOM_NAME;
                    tdo.ExecuteRoomCode = exeRoom.ROOM_CODE;
                    tdo.ExecuteDepartmentCode = exeRoom.DEPARTMENT_CODE;
                    tdo.ExecuteDepartmentName = exeRoom.DEPARTMENT_NAME;
                    tdo.ExecuteBranchCode = exeRoom.BRANCH_CODE;
                    tdo.ExecuteBranchName = exeRoom.BRANCH_NAME;
                }

                HIS_TEST_SAMPLE_TYPE testSampleType = serviceReq.TEST_SAMPLE_TYPE_ID.HasValue ? new HisTestSampleTypeGet().GetById(serviceReq.TEST_SAMPLE_TYPE_ID.Value) : null;

                tdo.TestSampleTypeCode = IsNotNull(testSampleType) ? testSampleType.TEST_SAMPLE_TYPE_CODE : null;
                tdo.TestSampleTypeName = IsNotNull(testSampleType) ? testSampleType.TEST_SAMPLE_TYPE_NAME : null;
            }

            tdo.ServiceCode = sereServ.TDL_SERVICE_CODE;
            tdo.ServiceName = sereServ.TDL_SERVICE_NAME;

            if (listSereServTein != null && listSereServTein.Count > 0)
            {
                tdo.TestDatas = new List<HisTestDetailResultTDO>();
                foreach (var ssTein in listSereServTein)
                {
                    HisTestDetailResultTDO detail = new HisTestDetailResultTDO();

                    detail.TestIndexCode = ssTein.TEST_INDEX_CODE;
                    detail.TestIndexName = ssTein.TEST_INDEX_NAME;
                    detail.TestIndexUnitName = ssTein.TEST_INDEX_UNIT_NAME;
                    detail.TestIndexGroupCode = ssTein.TEST_INDEX_GROUP_CODE;
                    detail.TestIndexGroupName = ssTein.TEST_INDEX_GROUP_NAME;
                    detail.MachineCode = ssTein.MACHINE_CODE;
                    detail.MachineName = ssTein.MACHINE_NAME;
                    detail.Value = ssTein.VALUE;
                    detail.ResultCode = ssTein.RESULT_CODE;
                    detail.Description = ssTein.DESCRIPTION;
                    detail.Note = ssTein.NOTE;
                    detail.ResultDescription = ssTein.RESULT_DESCRIPTION;

                    tdo.TestDatas.Add(detail);
                }
            }

            return tdo;
        }
    }
}
