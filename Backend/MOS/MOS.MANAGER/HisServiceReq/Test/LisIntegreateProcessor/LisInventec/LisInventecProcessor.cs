using Inventec.Common.Logging;
using Inventec.Common.WebApiClient;
using Inventec.Core;
using Inventec.Token.ResourceSystem;
using LIS.SDO;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisKskContract;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Test.LisIntegreateProcessor.LisInventec
{
    class LisInventecProcessor : BusinessBase, ILisProcessor
    {
        internal LisInventecProcessor()
            : base()
        {
        }

        internal LisInventecProcessor(CommonParam param)
            : base(param)
        {
        }

        public bool RequestOrder(OrderData data, ref List<string> messages)
        {
            try
            {
                string lisAddress = null;

                if (data != null && data.ServiceReq != null
                    && this.IsHavingLisAddress(data.ServiceReq.EXECUTE_ROOM_ID, ref lisAddress))
                {
                    V_HIS_EXECUTE_ROOM hisExecuteRoom = HisExecuteRoomCFG.DATA
                        .Where(o => o.ROOM_ID == data.ServiceReq.EXECUTE_ROOM_ID)
                        .FirstOrDefault();

                    V_HIS_ROOM hisRequestRoom = HisRoomCFG.DATA
                    .Where(o => o.ID == data.ServiceReq.REQUEST_ROOM_ID).FirstOrDefault();

                    V_HIS_SAMPLE_ROOM hisSampleRoom = HisSampleRoomCFG.DATA
                    .Where(o => o.ID == data.ServiceReq.SAMPLE_ROOM_ID)
                    .FirstOrDefault();

                    HIS_TREATMENT hisTreatment = new HisTreatmentGet().GetById(data.ServiceReq.TREATMENT_ID);
                    HIS_PATIENT_TYPE hisPatientType = HisPatientTypeCFG.DATA.FirstOrDefault(o => o.ID == data.ServiceReq.TDL_PATIENT_TYPE_ID);

                    string parentServiceCode = "";
                    List<ServiceReqSDO> sdos = new List<ServiceReqSDO>();
                    List<ServiceSDO> services = new List<ServiceSDO>();

                    foreach (HIS_SERE_SERV s in data.Availables)
                    {
                        ServiceSDO service = new ServiceSDO();
                        V_HIS_SERVICE hs = HisServiceCFG.DATA_VIEW.Where(o => o.ID == s.SERVICE_ID).FirstOrDefault();
                        if (hs != null)
                        {
                            service.ServiceCode = hs.SERVICE_CODE;
                            service.ServiceName = !string.IsNullOrWhiteSpace(hs.TESTING_TECHNIQUE) ? hs.TESTING_TECHNIQUE : hs.SERVICE_NAME;
                            if (hs.PARENT_ID.HasValue && String.IsNullOrWhiteSpace(parentServiceCode))
                            {
                                V_HIS_SERVICE pa = HisServiceCFG.DATA_VIEW.Where(o => o.ID == hs.PARENT_ID.Value).FirstOrDefault();
                                if (pa != null)
                                {
                                    parentServiceCode = pa.SERVICE_CODE;
                                }
                            }
                        }
                        service.ServiceNumOrder = s.ID;
                        service.TestIndexs = HisTestIndexCFG.DATA_VIEW
                        .Where(o => s.SERVICE_ID == o.TEST_SERVICE_TYPE_ID && o.IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE)
                        .Select(o => new TestIndexSDO
                        {
                            TestIndexCode = o.TEST_INDEX_CODE,
                            TestIndexName = o.TEST_INDEX_NAME,
                            TestIndexUnit = o.TEST_INDEX_UNIT_SYMBOL
                        }).ToList();
                        services.Add(service);
                    }

                    ServiceReqSDO sdo = new ServiceReqSDO();
                    sdo.FirstName = data.ServiceReq.TDL_PATIENT_FIRST_NAME;
                    sdo.LastName = data.ServiceReq.TDL_PATIENT_LAST_NAME;
                    sdo.PatientCode = data.ServiceReq.TDL_PATIENT_CODE;
                    sdo.NumOrder = data.ServiceReq.NUM_ORDER;
                    sdo.InstructionTime = data.ServiceReq.INTRUCTION_TIME;
                    sdo.Dob = data.ServiceReq.TDL_PATIENT_DOB;
                    sdo.GenderCode = this.GetGenderCode(data.ServiceReq.TDL_PATIENT_GENDER_ID);
                    if (hisSampleRoom != null)
                    {
                        sdo.SampleRoomCode = hisSampleRoom.SAMPLE_ROOM_CODE;
                        sdo.SampleRoomName = hisSampleRoom.SAMPLE_ROOM_NAME;
                    }
                    if (hisExecuteRoom != null)
                    {
                        sdo.ExecuteRoomCode = hisExecuteRoom.EXECUTE_ROOM_CODE;
                        sdo.ExecuteRoomName = hisExecuteRoom.EXECUTE_ROOM_NAME;
                    }
                    if (hisRequestRoom != null)
                    {
                        sdo.RequestRoomCode = hisRequestRoom.ROOM_CODE;
                        sdo.RequestRoomName = hisRequestRoom.ROOM_NAME;
                        sdo.RequestDepartmentCode = hisRequestRoom.DEPARTMENT_CODE;
                        sdo.RequestDepartmentName = hisRequestRoom.DEPARTMENT_NAME;
                    }
                    sdo.RequestLoginname = data.ServiceReq.REQUEST_LOGINNAME;
                    sdo.RequestUsername = data.ServiceReq.REQUEST_USERNAME;
                    sdo.ServiceReqCode = data.ServiceReq.SERVICE_REQ_CODE;
                    sdo.TreatmentCode = data.ServiceReq.TDL_TREATMENT_CODE;
                    sdo.CallSampleOrder = data.ServiceReq.CALL_SAMPLE_ORDER;
                    if (data.ServiceReq.IS_SEND_BARCODE_TO_LIS == Constant.IS_TRUE)
                    {
                        sdo.Barcode = data.ServiceReq.BARCODE;
                    }

                    sdo.IsAntibioticResistance = data.ServiceReq.IS_ANTIBIOTIC_RESISTANCE == Constant.IS_TRUE;
                    if (data.KskContract != null && data.Availables.Any(a => a.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__KSK))
                    {
                        sdo.KskContractCode = data.KskContract.KSK_CONTRACT_CODE;
                    }
                    sdo.Services = services;
                    sdo.ParentServiceCode = parentServiceCode;

                    sdo.HasNoDobDate = data.ServiceReq.TDL_PATIENT_IS_HAS_NOT_DAY_DOB == Constant.IS_TRUE;
                    sdo.CommuneCode = data.ServiceReq.TDL_PATIENT_COMMUNE_CODE;
                    sdo.CommuneName = data.ServiceReq.TDL_PATIENT_COMMUNE_NAME;
                    sdo.DistrictCode = data.ServiceReq.TDL_PATIENT_DISTRICT_CODE;
                    sdo.DistrictName = data.ServiceReq.TDL_PATIENT_DISTRICT_NAME;
                    sdo.ProvinceCode = data.ServiceReq.TDL_PATIENT_PROVINCE_CODE;
                    sdo.ProvinceName = data.ServiceReq.TDL_PATIENT_PROVINCE_NAME;
                    sdo.NationalCode = data.ServiceReq.TDL_PATIENT_NATIONAL_CODE;
                    sdo.NationalName = data.ServiceReq.TDL_PATIENT_NATIONAL_NAME;
                    sdo.Address = data.ServiceReq.TDL_PATIENT_ADDRESS;
                    sdo.PhoneNumber = data.ServiceReq.TDL_PATIENT_PHONE;
                    sdo.CareerName = data.ServiceReq.TDL_PATIENT_CAREER_NAME;
                    sdo.WorkPlace = data.ServiceReq.TDL_PATIENT_WORK_PLACE_NAME;
                    sdo.HeinCardNumber = data.ServiceReq.TDL_HEIN_CARD_NUMBER;
                    sdo.CccdNumber = data.ServiceReq.TDL_PATIENT_CCCD_NUMBER;
                    sdo.CccdPlace = data.ServiceReq.TDL_PATIENT_CCCD_PLACE;
                    sdo.CccdDate = data.ServiceReq.TDL_PATIENT_CCCD_DATE;
                    sdo.CmndNumber = data.ServiceReq.TDL_PATIENT_CMND_NUMBER;
                    sdo.CmndPlace = data.ServiceReq.TDL_PATIENT_CMND_PLACE;
                    sdo.CmndDate = data.ServiceReq.TDL_PATIENT_CMND_DATE;
                    sdo.HeinCardFromTime = IsNotNull(hisTreatment) ? hisTreatment.TDL_HEIN_CARD_FROM_TIME : null;
                    sdo.HeinCardToTime = IsNotNull(hisTreatment) ? hisTreatment.TDL_HEIN_CARD_TO_TIME : null;
                    sdo.PatientTypeName = IsNotNull(hisPatientType) ? hisPatientType.PATIENT_TYPE_NAME : "";
                    sdo.HeinMediOrgCode = data.ServiceReq.TDL_HEIN_MEDI_ORG_CODE;
                    sdo.HeinMediOrgName = data.ServiceReq.TDL_HEIN_MEDI_ORG_NAME;
                    if (data.ServiceReq.IS_EMERGENCY == Constant.IS_TRUE)
                    {
                        sdo.IsEmergency = true;
                    }
                    else
                    {
                        sdo.IsEmergency = false;
                    }
                    sdo.TreatmentTypeId = IsNotNull(hisTreatment) ? hisTreatment.TDL_TREATMENT_TYPE_ID : null;
                    V_HIS_EXECUTE_ROOM executeRoom = HisExecuteRoomCFG.DATA.FirstOrDefault(o => o.ROOM_ID == data.ServiceReq.EXECUTE_ROOM_ID);
                    if (executeRoom != null)
                    {
                        sdo.ExecuteMediOrgCode = executeRoom.HEIN_MEDI_ORG_CODE;
                    }

                    sdos.Add(sdo);
                    string tokenCode = Inventec.Token.ResourceSystem.ResourceTokenManager.GetTokenCode();
                    ApiConsumer serviceConsumer = new ApiConsumer(lisAddress, tokenCode, MOS.UTILITY.Constant.APPLICATION_CODE);

                    var ro = serviceConsumer.Post<Inventec.Core.ApiResultObject<bool>>("/api/LisSample/CreateByServiceReqSDO", null, sdos);

                    if (ro != null && ro.Param != null)
                    {
                        if (ro.Param.BugCodes != null)
                        {
                            param.BugCodes.AddRange(ro.Param.BugCodes);
                        }
                        if (ro.Param.Messages != null)
                        {
                            param.Messages.AddRange(ro.Param.Messages);
                        }
                    }

                    if (ro == null || !ro.Success)
                    {
                        LogSystem.Error("Gui y/c xet nghiem sang he thong LIS cua Lis that bai. Ket qua: " + LogUtil.TraceData("ro", ro));
                        return false;
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return false;
        }

        public bool DeleteOrder(OrderData data, ref List<string> messages)
        {
            try
            {
                string lisAddress = null;

                if (data != null && data.ServiceReq != null
                    && this.IsHavingLisAddress(data.ServiceReq.EXECUTE_ROOM_ID, ref lisAddress))
                {
                    string tokenCode = ResourceTokenManager.GetTokenCode();
                    ApiConsumer serviceConsumer = new ApiConsumer(lisAddress, tokenCode, MOS.UTILITY.Constant.APPLICATION_CODE);

                    var ro = serviceConsumer.Post<ApiResultObject<bool>>("/api/LisSample/DeleteByServiceReqCode", null, data.ServiceReq.SERVICE_REQ_CODE);
                    if (ro == null || !ro.Success)
                    {
                        LogSystem.Error(string.Format("Gui huy y/c xet nghiem (ma: {0}) sang he thong LIS cua Lis that bai. Ket qua: {1} ", data.ServiceReq.SERVICE_REQ_CODE, LogUtil.TraceData("ro", ro)));
                        if (ro.Param.BugCodes.Contains("LIS001"))
                        {
                            MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisServiceReq_YeuCauXetNghiemDaDuocLayMauHoacDaCoKetQua);
                        }
                        return false;
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return false;
        }

        public bool UpdatePatientInfo(HIS_PATIENT data, ref List<string> messages)
        {
            try
            {
                List<HIS_TREATMENT> treats = new HisTreatmentGet().GetByPatientId(data.ID);
                List<long> treatIds = treats != null && treats.Count > 0 ? treats.Select(o => o.ID).ToList() : null;
                List<HIS_SERVICE_REQ> serviceReqs = treatIds != null && treatIds.Count > 0 ? new HisServiceReqGet().GetByTreatmentIds(treatIds) : null;
                serviceReqs = serviceReqs.Where(o =>
                    o.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__XN
                    && o.SERVICE_REQ_STT_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__HT
                    && o.IS_SENT_EXT == Constant.IS_TRUE
                    ).ToList();
                if (serviceReqs != null && serviceReqs.Count > 0)
                {
                    foreach (HIS_SERVICE_REQ serviceReq in serviceReqs)
                    {
                        List<HIS_SERE_SERV> ss = serviceReq != null ? new HisSereServGet().GetByServiceReqId(serviceReq.ID) : null;
                        V_HIS_TREATMENT_FEE_1 treat = new HisTreatmentGet().GetFeeView1ById(serviceReq.TREATMENT_ID);

                        HIS_KSK_CONTRACT contract = null;
                        if (treat.TDL_KSK_CONTRACT_ID.HasValue)
                        {
                            contract = new HisKskContractGet().GetById(treat.TDL_KSK_CONTRACT_ID.Value);
                        }

                        string lisAddress = null;

                        if (data != null && serviceReqs != null
                            && this.IsHavingLisAddress(serviceReq.EXECUTE_ROOM_ID, ref lisAddress))
                        {
                            V_HIS_EXECUTE_ROOM hisExecuteRoom = HisExecuteRoomCFG.DATA
                                .Where(o => o.ROOM_ID == serviceReq.EXECUTE_ROOM_ID)
                                .FirstOrDefault();

                            V_HIS_ROOM hisRequestRoom = HisRoomCFG.DATA
                            .Where(o => o.ID == serviceReq.REQUEST_ROOM_ID).FirstOrDefault();

                            V_HIS_SAMPLE_ROOM hisSampleRoom = HisSampleRoomCFG.DATA
                            .Where(o => o.ID == serviceReq.SAMPLE_ROOM_ID)
                            .FirstOrDefault();

                            HIS_TREATMENT hisTreatment = new HisTreatmentGet().GetById(serviceReq.TREATMENT_ID);
                            HIS_PATIENT_TYPE hisPatientType = HisPatientTypeCFG.DATA.FirstOrDefault(o => o.ID == serviceReq.TDL_PATIENT_TYPE_ID);

                            string parentServiceCode = "";
                            List<ServiceSDO> services = new List<ServiceSDO>();

                            foreach (HIS_SERE_SERV s in ss)
                            {
                                ServiceSDO service = new ServiceSDO();
                                V_HIS_SERVICE hs = HisServiceCFG.DATA_VIEW.Where(o => o.ID == s.SERVICE_ID).FirstOrDefault();
                                if (hs != null)
                                {
                                    service.ServiceCode = hs.SERVICE_CODE;
                                    service.ServiceName = !string.IsNullOrWhiteSpace(hs.TESTING_TECHNIQUE) ? hs.TESTING_TECHNIQUE : hs.SERVICE_NAME;
                                    if (hs.PARENT_ID.HasValue && String.IsNullOrWhiteSpace(parentServiceCode))
                                    {
                                        V_HIS_SERVICE pa = HisServiceCFG.DATA_VIEW.Where(o => o.ID == hs.PARENT_ID.Value).FirstOrDefault();
                                        if (pa != null)
                                        {
                                            parentServiceCode = pa.SERVICE_CODE;
                                        }
                                    }
                                }
                                service.ServiceNumOrder = s.ID;
                                service.TestIndexs = HisTestIndexCFG.DATA_VIEW
                                .Where(o => s.SERVICE_ID == o.TEST_SERVICE_TYPE_ID && o.IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE)
                                .Select(o => new TestIndexSDO
                                {
                                    TestIndexCode = o.TEST_INDEX_CODE,
                                    TestIndexName = o.TEST_INDEX_NAME,
                                    TestIndexUnit = o.TEST_INDEX_UNIT_SYMBOL
                                }).ToList();
                                services.Add(service);
                            }

                            ServiceReqSDO sdo = new ServiceReqSDO();
                            sdo.FirstName = serviceReq.TDL_PATIENT_FIRST_NAME;
                            sdo.LastName = serviceReq.TDL_PATIENT_LAST_NAME;
                            sdo.PatientCode = serviceReq.TDL_PATIENT_CODE;
                            sdo.NumOrder = serviceReq.NUM_ORDER;
                            sdo.InstructionTime = serviceReq.INTRUCTION_TIME;
                            sdo.Dob = serviceReq.TDL_PATIENT_DOB;
                            sdo.GenderCode = this.GetGenderCode(serviceReq.TDL_PATIENT_GENDER_ID);
                            if (hisSampleRoom != null)
                            {
                                sdo.SampleRoomCode = hisSampleRoom.SAMPLE_ROOM_CODE;
                                sdo.SampleRoomName = hisSampleRoom.SAMPLE_ROOM_NAME;
                            }
                            if (hisExecuteRoom != null)
                            {
                                sdo.ExecuteRoomCode = hisExecuteRoom.EXECUTE_ROOM_CODE;
                                sdo.ExecuteRoomName = hisExecuteRoom.EXECUTE_ROOM_NAME;
                            }
                            if (hisRequestRoom != null)
                            {
                                sdo.RequestRoomCode = hisRequestRoom.ROOM_CODE;
                                sdo.RequestRoomName = hisRequestRoom.ROOM_NAME;
                                sdo.RequestDepartmentCode = hisRequestRoom.DEPARTMENT_CODE;
                                sdo.RequestDepartmentName = hisRequestRoom.DEPARTMENT_NAME;
                            }
                            sdo.RequestLoginname = serviceReq.REQUEST_LOGINNAME;
                            sdo.RequestUsername = serviceReq.REQUEST_USERNAME;
                            sdo.ServiceReqCode = serviceReq.SERVICE_REQ_CODE;
                            sdo.TreatmentCode = serviceReq.TDL_TREATMENT_CODE;
                            sdo.CallSampleOrder = serviceReq.CALL_SAMPLE_ORDER;
                            if (serviceReq.IS_SEND_BARCODE_TO_LIS == Constant.IS_TRUE)
                            {
                                sdo.Barcode = serviceReq.BARCODE;
                            }

                            sdo.IsAntibioticResistance = serviceReq.IS_ANTIBIOTIC_RESISTANCE == Constant.IS_TRUE;
                            if (serviceReq != null && ss.Any(a => a.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__KSK) && contract != null )
                            {
                                sdo.KskContractCode = contract.KSK_CONTRACT_CODE;
                            }
                            sdo.Services = services;
                            sdo.ParentServiceCode = parentServiceCode;

                            sdo.HasNoDobDate = serviceReq.TDL_PATIENT_IS_HAS_NOT_DAY_DOB == Constant.IS_TRUE;
                            sdo.CommuneCode = serviceReq.TDL_PATIENT_COMMUNE_CODE;
                            sdo.CommuneName = serviceReq.TDL_PATIENT_COMMUNE_NAME;
                            sdo.DistrictCode = serviceReq.TDL_PATIENT_DISTRICT_CODE;
                            sdo.DistrictName = serviceReq.TDL_PATIENT_DISTRICT_NAME;
                            sdo.ProvinceCode = serviceReq.TDL_PATIENT_PROVINCE_CODE;
                            sdo.ProvinceName = serviceReq.TDL_PATIENT_PROVINCE_NAME;
                            sdo.NationalCode = serviceReq.TDL_PATIENT_NATIONAL_CODE;
                            sdo.NationalName = serviceReq.TDL_PATIENT_NATIONAL_NAME;
                            sdo.Address = serviceReq.TDL_PATIENT_ADDRESS;
                            sdo.PhoneNumber = serviceReq.TDL_PATIENT_PHONE;
                            sdo.CareerName = serviceReq.TDL_PATIENT_CAREER_NAME;
                            sdo.WorkPlace = serviceReq.TDL_PATIENT_WORK_PLACE_NAME;
                            sdo.HeinCardNumber = serviceReq.TDL_HEIN_CARD_NUMBER;
                            sdo.CccdNumber = serviceReq.TDL_PATIENT_CCCD_NUMBER;
                            sdo.CccdPlace = serviceReq.TDL_PATIENT_CCCD_PLACE;
                            sdo.CccdDate = serviceReq.TDL_PATIENT_CCCD_DATE;
                            sdo.CmndNumber = serviceReq.TDL_PATIENT_CMND_NUMBER;
                            sdo.CmndPlace = serviceReq.TDL_PATIENT_CMND_PLACE;
                            sdo.CmndDate = serviceReq.TDL_PATIENT_CMND_DATE;
                            sdo.HeinCardFromTime = IsNotNull(hisTreatment) ? hisTreatment.TDL_HEIN_CARD_FROM_TIME : null;
                            sdo.HeinCardToTime = IsNotNull(hisTreatment) ? hisTreatment.TDL_HEIN_CARD_TO_TIME : null;
                            sdo.PatientTypeName = IsNotNull(hisPatientType) ? hisPatientType.PATIENT_TYPE_NAME : "";
                            sdo.HeinMediOrgCode = serviceReq.TDL_HEIN_MEDI_ORG_CODE;
                            sdo.HeinMediOrgName = serviceReq.TDL_HEIN_MEDI_ORG_NAME;
                            sdo.TreatmentTypeId = IsNotNull(hisTreatment) ? hisTreatment.TDL_TREATMENT_TYPE_ID : null;
                            V_HIS_EXECUTE_ROOM executeRoom = HisExecuteRoomCFG.DATA.FirstOrDefault(o => o.ROOM_ID == serviceReq.EXECUTE_ROOM_ID);
                            if (executeRoom != null)
                            {
                                sdo.ExecuteMediOrgCode = executeRoom.HEIN_MEDI_ORG_CODE;
                            }

                            string tokenCode = Inventec.Token.ResourceSystem.ResourceTokenManager.GetTokenCode();
                            ApiConsumer serviceConsumer = new ApiConsumer(lisAddress, tokenCode, MOS.UTILITY.Constant.APPLICATION_CODE);

                            var ro = serviceConsumer.Post<Inventec.Core.ApiResultObject<bool>>("/api/LisSample/UpdateByServiceReqSDO", null, sdo);

                            if (ro != null && ro.Param != null)
                            {
                                if (ro.Param.BugCodes != null)
                                {
                                    param.BugCodes.AddRange(ro.Param.BugCodes);
                                }
                                if (ro.Param.Messages != null)
                                {
                                    param.Messages.AddRange(ro.Param.Messages);
                                }
                            }

                            if (ro == null || !ro.Success)
                            {
                                LogSystem.Error("Gui y/c xet nghiem sang he thong LIS cua Lis that bai. Ket qua: " + LogUtil.TraceData("ro", ro));
                            }
                        }
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return false;
        }

        /// <summary>
        /// Kiem tra xem room da duoc cau hinh dia chi ket noi LIS chua
        /// </summary>
        /// <param name="executeRoomId"></param>
        /// <param name="lisUrl"></param>
        /// <returns></returns>
        private bool IsHavingLisAddress(long executeRoomId, ref string lisUrl)
        {
            bool result = false;
            try
            {
                V_HIS_EXECUTE_ROOM hisExecuteRoom = HisExecuteRoomCFG.DATA
                        .Where(o => o.ROOM_ID == executeRoomId)
                        .FirstOrDefault();

                lisUrl = LisInventecCFG.ADDRESSES
                        .Where(o => o.RoomCode == hisExecuteRoom.EXECUTE_ROOM_CODE)
                        .Select(o => o.Url)
                        .FirstOrDefault();

                if (string.IsNullOrWhiteSpace(lisUrl))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_ChuaCauHinhDiaChiKetNoiLis);
                    LogSystem.Warn("Chua cau hinh dia chi url he thong lis tuong ung voi phong XN: " + hisExecuteRoom.EXECUTE_ROOM_NAME);
                    result = false;
                }
                result = true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                result = false;
            }

            return result;
        }

        private string GetGenderCode(long? genderId)
        {
            if (genderId.HasValue && genderId.Value > 0)
            {
                return HisGenderCFG.DATA.FirstOrDefault(o => o.ID == genderId.Value).GENDER_CODE;
            }
            return null;
        }
    }
}
