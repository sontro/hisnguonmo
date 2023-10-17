using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExecuteRoom;
using MOS.MANAGER.HisServiceReq.Exam;
using MOS.MANAGER.HisServiceReq.Exam.Register;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTreatment.Update.Finish
{
    class RegisterTreatmentProcessor : BusinessBase
    {
        HisServiceReqExamRegister hisServiceReqExamRegister;

        internal RegisterTreatmentProcessor()
            : base()
        {
            this.Init();
        }

        internal RegisterTreatmentProcessor(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisServiceReqExamRegister = new HisServiceReqExamRegister(this.param);
        }

        internal bool Run(HisTreatmentFinishSDO data, HIS_TREATMENT treatment, HIS_PATIENT hisPatient, List<HIS_PATIENT_TYPE_ALTER> ptas, List<HIS_SERE_SERV> existsSereServs, WorkPlaceSDO workPlace, ref long? nextExamNumOrder)
        {
            bool result = false;
            try
            {
                if (data.IsTemporary || data.TreatmentEndTypeId != IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE.ID__HEN || !HisTreatmentCFG.AUTO_CREATE_WHEN_APPOINTMENT || (data.IsCreateNewTreatment && treatment.MEDI_RECORD_ID.HasValue))
                {
                    return true;
                }

                HisServiceReqExamRegisterSDO registerData = new HisServiceReqExamRegisterSDO();

                registerData.RequestRoomId = workPlace.RoomId;

                registerData.HisPatientProfile = new HisPatientProfileSDO();
                registerData.ServiceReqDetails = new List<ServiceReqDetailSDO>();

                if (data.AppointmentTime.HasValue)
                {
                    registerData.InstructionTime = data.AppointmentTime.Value;
                    registerData.InstructionTimes = new List<long> { data.AppointmentTime.Value };
                }

                registerData.HisPatientProfile.RequestRoomId = workPlace.RoomId;
                registerData.HisPatientProfile.TreatmentTime = registerData.InstructionTime;

                registerData.HisPatientProfile.HisPatient = hisPatient;
                registerData.HisPatientProfile.HisTreatment = new HIS_TREATMENT();

                Mapper.CreateMap<HIS_PATIENT_TYPE_ALTER, HIS_PATIENT_TYPE_ALTER>();
                HIS_PATIENT_TYPE_ALTER lastPaty = ptas.OrderByDescending(o => o.LOG_TIME).ThenByDescending(o => o.ID).FirstOrDefault();

                HIS_PATIENT_TYPE_ALTER hisPatientTypeAlter = Mapper.Map<HIS_PATIENT_TYPE_ALTER>(lastPaty);

                hisPatientTypeAlter.ID = 0;
                hisPatientTypeAlter.LOG_TIME = registerData.InstructionTime;
                hisPatientTypeAlter.EXECUTE_ROOM_ID = workPlace.RoomId;
                hisPatientTypeAlter.TREATMENT_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__KHAM;

                if (hisPatientTypeAlter.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT)
                {
                    HIS_BRANCH branch = HisBranchCFG.DATA.FirstOrDefault(o => o.ID == workPlace.BranchId);
                    if (IsNotNull(branch) && branch.HEIN_MEDI_ORG_CODE != hisPatientTypeAlter.HEIN_MEDI_ORG_CODE)
                    {
                        hisPatientTypeAlter.RIGHT_ROUTE_TYPE_CODE = MOS.LibraryHein.Bhyt.HeinRightRouteType.HeinRightRouteTypeCode.APPOINTMENT;
                    }

                    //thoi gian hen kham so voi thoi gian the tinh theo ngay
                    long instructionDate = registerData.InstructionTime - registerData.InstructionTime % 1000000;
                    long cardDate = hisPatientTypeAlter.HEIN_CARD_TO_TIME.HasValue ? hisPatientTypeAlter.HEIN_CARD_TO_TIME.Value - hisPatientTypeAlter.HEIN_CARD_TO_TIME.Value % 1000000 : 0;

                    if (cardDate > 0 && cardDate < instructionDate)
                    {
                        hisPatientTypeAlter.PATIENT_TYPE_ID = HisPatientTypeCFG.PATIENT_TYPE_ID__HOSPITAL_FEE;
                        hisPatientTypeAlter.ADDRESS = null;
                        hisPatientTypeAlter.FREE_CO_PAID_TIME = null;
                        hisPatientTypeAlter.HEIN_CARD_FROM_TIME = null;
                        hisPatientTypeAlter.HEIN_CARD_NUMBER = null;
                        hisPatientTypeAlter.HEIN_CARD_TO_TIME = null;
                        hisPatientTypeAlter.HEIN_MEDI_ORG_CODE = null;
                        hisPatientTypeAlter.HEIN_MEDI_ORG_NAME = null;
                        hisPatientTypeAlter.JOIN_5_YEAR = null;
                        hisPatientTypeAlter.LEVEL_CODE = null;
                        hisPatientTypeAlter.LIVE_AREA_CODE = null;
                        hisPatientTypeAlter.RIGHT_ROUTE_CODE = null;
                        hisPatientTypeAlter.RIGHT_ROUTE_TYPE_CODE = null;
                        hisPatientTypeAlter.HAS_BIRTH_CERTIFICATE = null;
                    }
                }

                registerData.HisPatientProfile.HisPatientTypeAlter = hisPatientTypeAlter;

                if (IsNotNullOrEmpty(data.AppointmentExamRoomIds))
                {
                    List<V_HIS_EXECUTE_ROOM> executeRooms = HisExecuteRoomCFG.DATA.Where(o => data.AppointmentExamRoomIds.Contains(o.ROOM_ID)).ToList();
                    if (IsNotNullOrEmpty(executeRooms))
                    {
                        List<V_HIS_EXECUTE_ROOM> listExecuteRoomNos = executeRooms.Where(o => o.ALLOW_NOT_CHOOSE_SERVICE == Constant.IS_FALSE).ToList();
                        List<V_HIS_EXECUTE_ROOM> listExecuteRoomYess = executeRooms.Where(o => o.ALLOW_NOT_CHOOSE_SERVICE == Constant.IS_TRUE).ToList();

                        ServiceReqDetailSDO detail = new ServiceReqDetailSDO();
                        if (IsNotNullOrEmpty(listExecuteRoomNos))
                        {
                            HIS_SERE_SERV mainExam = null;
                            if (IsNotNullOrEmpty(existsSereServs))
                            {
                                if (data.ServiceReqId.HasValue)
                                {
                                    mainExam = existsSereServs.FirstOrDefault(o => o.SERVICE_REQ_ID == data.ServiceReqId.Value);
                                }
                                else
                                {
                                    mainExam = existsSereServs.Where(o => o.TDL_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__KH &&
                                        o.IS_NO_EXECUTE != Constant.IS_TRUE).OrderByDescending(o => o.TDL_IS_MAIN_EXAM == 1 ? 1 : 0).FirstOrDefault();
                                }
                            }
                            if (mainExam == null)
                            {
                                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_KhongTimThayThongTinChiDinhKham);
                                return false;
                            }
                            if (IsNotNull(mainExam))
                            {
                                detail.Amount = mainExam.AMOUNT;
                                detail.OtherPaySourceId = mainExam.OTHER_PAY_SOURCE_ID;
                                detail.PackageId = mainExam.PACKAGE_ID;
                                detail.PatientTypeId = hisPatientTypeAlter.PATIENT_TYPE_ID;
                                detail.PrimaryPatientTypeId = hisPatientTypeAlter.PRIMARY_PATIENT_TYPE_ID;
                                detail.RoomId = data.AppointmentExamRoomIds.First();
                                detail.ServiceConditionId = mainExam.SERVICE_CONDITION_ID;
                                detail.ServiceId = mainExam.SERVICE_ID;
                                registerData.ServiceReqDetails.Add(detail);

                                string sql = "SELECT * FROM HIS_SERVICE_REQ WHERE ID = :param1";
                                HIS_SERVICE_REQ hisServiceReq = DAOWorker.SqlDAO.GetSqlSingle<HIS_SERVICE_REQ>(sql, mainExam.SERVICE_REQ_ID);
                                if (IsNotNull(hisServiceReq))
                                {
                                    registerData.Priority = hisServiceReq.PRIORITY;
                                    registerData.PriorityTypeId = hisServiceReq.PRIORITY_TYPE_ID;
                                }
                            }
                        }

                        if (IsNotNullOrEmpty(listExecuteRoomYess))
                        {
                            detail.RoomId = data.AppointmentExamRoomIds.First();
                            registerData.ServiceReqDetails.Add(detail);
                        }
                    }
                }

                HisServiceReqExamRegisterResultSDO resultData = null;
                //gan true de kho gan lai thoi gian theo server
                if (!this.hisServiceReqExamRegister.Create(registerData, true, ref resultData, ref treatment, ref workPlace))
                {
                    throw new Exception("Rollback du lieu, hisServiceReqExamRegister error");
                }

                //Gan lai so thu tu kham cua lan sau cho ho so dieu tri hien tai
                nextExamNumOrder = resultData.ServiceReqs[0].NUM_ORDER;

                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
                param.HasException = true;
            }
            return result;
        }

        internal void Rollback()
        {
            try
            {
                this.hisServiceReqExamRegister.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
