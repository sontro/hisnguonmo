using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisSereServRation;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Configuration;

namespace MOS.MANAGER.HisServiceReq.Ration.AutoAssign
{
    public class HisServiceReqAutoAssignRationProcessor : BusinessBase
    {
        private static bool IS_SENDING = false;
        private static Regex RegexCheckHours = new Regex(@"^(2[0-3]|[01]?[0-9]):([0-5]?[0-9])$");

        public HisServiceReqAutoAssignRationProcessor()
            : base()
        {
        }

        public HisServiceReqAutoAssignRationProcessor(CommonParam param)
            : base(param)
        {
        }

        public static void Run()
        {
            try
            {
                if (IS_SENDING)
                {
                    LogSystem.Debug("Tien trinh tu dong AutoAssignRation dang chay, khong cho phep khoi tao tien trinh moi");
                    return;
                }

                IS_SENDING = true;

                if (string.IsNullOrWhiteSpace(HisServiceReqCFG.AUTO_CREATE_RATION_WITH_TIME_OPTION))
                {
                    LogSystem.Debug("Tien trinh tu dong AutoAssignRation chua cai dat key cau hinh");
                    return;
                }

                if (!RegexCheckHours.IsMatch(HisServiceReqCFG.AUTO_CREATE_RATION_WITH_TIME_OPTION))
                {
                    LogSystem.Debug("Tien trinh tu dong AutoAssignRation cau hinh khong hop le");
                    return;
                }

                // Lay thoi gian theo key cau hinh
                DateTime setedDateTime = Convert.ToDateTime(HisServiceReqCFG.AUTO_CREATE_RATION_WITH_TIME_OPTION);
                // Lay thoi gian theo key web config
                DateTime endDateTime = DateTime.Now.AddMilliseconds(int.Parse(ConfigurationManager.AppSettings["MOS.API.Scheduler.AutoCreateRation"]));
                LogSystem.Debug("now: " + DateTime.Now.ToString() + ", time_config: " + setedDateTime.ToString() + ", end_range_time: " + endDateTime.ToString());
                if (DateTime.Now <= setedDateTime && setedDateTime <= endDateTime)
                {
                    LogSystem.Debug("Cau hinh thoei gian hop le");
                    HisTreatmentFilterQuery filter = new HisTreatmentFilterQuery();
                    filter.HAS_AUTO_CREATE_RATION = true;
                    filter.IS_PAUSE = false;

                    List<HIS_TREATMENT> listTreatments = new HisTreatmentGet().Get(filter);
                    if (listTreatments != null && listTreatments.Count > 0)
                    {
                        List<HIS_SERVICE_REQ> listServiceReqs = new List<HIS_SERVICE_REQ>();
                        List<HIS_SERE_SERV_RATION> listSereServRations = new List<HIS_SERE_SERV_RATION>();
                        int skip = 0;
                        while (listTreatments.Count - skip > 0)
                        {
                            var listIds = listTreatments.Skip(skip).Take(100).ToList();
                            skip += 100;
                            HisServiceReqFilterQuery reqFilter = new HisServiceReqFilterQuery();
                            reqFilter.SERVICE_REQ_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__AN;
                            reqFilter.IS_FOR_AUTO_CREATE_RATION = true;
                            reqFilter.TREATMENT_IDs = listIds.Select(s => s.ID).ToList();

                            List<HIS_SERVICE_REQ> reqs = new HisServiceReqGet().Get(reqFilter);
                            if (reqs != null && reqs.Count > 0)
                            {
                                listServiceReqs.AddRange(reqs);
                                List<HIS_SERE_SERV_RATION> sereServRations = new HisSereServRationGet().GetByServiceReqIds(reqs.Select(s => s.ID).ToList());
                                if (sereServRations != null && sereServRations.Count > 0)
                                {
                                    listSereServRations.AddRange(sereServRations);
                                }
                            }
                        }

                        if (listServiceReqs != null && listServiceReqs.Count > 0)
                        {
                            long dateNow = Inventec.Common.DateTime.Get.StartDay() ?? 0;
                            bool isFriday = DateTime.Now.DayOfWeek == DayOfWeek.Friday;

                            foreach (var treatment in listTreatments)
                            {
                                //tạo thời gian trong vòng lặp để mỗi hồ sơ có thời gian riêng
                                List<long> listIntructionTimes = new List<long>();

                                DateTime intructionTimeNextDay = DateTime.Now.AddDays(1);
                                listIntructionTimes.Add(Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(intructionTimeNextDay) ?? 0);
                                if (isFriday)
                                {
                                    DateTime intructionTimeNextDay2 = DateTime.Now.AddDays(2);
                                    listIntructionTimes.Add(Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(intructionTimeNextDay2) ?? 0);
                                    DateTime intructionTimeNextDay3 = DateTime.Now.AddDays(3);
                                    listIntructionTimes.Add(Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(intructionTimeNextDay3) ?? 0);
                                }

                                //lấy ra y lệnh suất ăn cùng khoa với khoa bệnh nhân đang điều trị
                                List<HIS_SERVICE_REQ> serviceReqByTreatment = listServiceReqs.Where(o => o.TREATMENT_ID == treatment.ID && o.REQUEST_DEPARTMENT_ID == treatment.LAST_DEPARTMENT_ID).ToList();
                                LogSystem.Debug("Lay thong tin y lenh theo id ho so va co cung phong yeu cau va phong xu ly: ho so: " + treatment.TREATMENT_CODE);
                                if (serviceReqByTreatment != null && serviceReqByTreatment.Count > 0)
                                {
                                    var groupDate = serviceReqByTreatment.GroupBy(o => o.INTRUCTION_DATE).OrderByDescending(o => o.Key).ToList();
                                    LogSystem.Debug("treatment_code: " + treatment.TREATMENT_CODE + ", intructionDate: " + groupDate.First().Key.ToString());
                                    if (groupDate.First().Key == dateNow)
                                    {
                                        LogSystem.Debug("Y lenh duoc phep chi dinh");
                                        List<HIS_SERVICE_REQ> validReqs = new List<HIS_SERVICE_REQ>();
                                        var forPatient = groupDate.First().Where(o => o.IS_FOR_HOMIE != Constant.IS_TRUE).OrderByDescending(o => o.INTRUCTION_TIME).ThenByDescending(o => o.ID).FirstOrDefault();
                                        if (forPatient != null)
                                            validReqs.Add(forPatient);
                                        var forPatientHomie = groupDate.First().Where(o => o.IS_FOR_HOMIE == Constant.IS_TRUE).OrderByDescending(o => o.INTRUCTION_TIME).ThenByDescending(o => o.ID).FirstOrDefault();
                                        if (forPatientHomie != null)
                                            validReqs.Add(forPatientHomie);
                                        foreach (var serviceReq in validReqs)
                                        {
                                            //chỉ định dịch vụ
                                            HisRationServiceReqSDO data = new HisRationServiceReqSDO();
                                            data.Description = serviceReq.DESCRIPTION;
                                            data.IcdCauseCode = serviceReq.ICD_CAUSE_CODE;
                                            data.IcdCauseName = serviceReq.ICD_CAUSE_NAME;
                                            data.IcdCode = serviceReq.ICD_CODE;
                                            data.IcdName = serviceReq.ICD_NAME;
                                            data.IcdSubCode = serviceReq.ICD_SUB_CODE;
                                            data.IcdText = serviceReq.ICD_TEXT;
                                            data.InstructionTimes = listIntructionTimes;
                                            data.IsForAutoCreateRation = true;
                                            data.ParentServiceReqId = serviceReq.PARENT_ID;
                                            data.RequestLoginName = serviceReq.REQUEST_LOGINNAME;
                                            data.RequestRoomId = serviceReq.REQUEST_ROOM_ID;
                                            data.RequestUserName = serviceReq.REQUEST_USERNAME;
                                            data.IsForHomie = serviceReq.IS_FOR_HOMIE == Constant.IS_TRUE ? true : false;
                                            data.TreatmentIds = new List<long>() { serviceReq.TREATMENT_ID };
                                            data.RationServices = new List<RationServiceSDO>();

                                            List<HIS_SERE_SERV_RATION> sereServs = listSereServRations.Where(o => o.SERVICE_REQ_ID == serviceReq.ID).ToList();
                                            foreach (HIS_SERE_SERV_RATION ss in sereServs)
                                            {
                                                RationServiceSDO rs = new RationServiceSDO();
                                                //làm tròn số lượng lên. tránh trường hợp y lệnh mẫu ăn từ chiều có số lượng 0.5
                                                rs.Amount = Math.Round(ss.AMOUNT, 0, MidpointRounding.AwayFromZero);
                                                rs.PatientTypeId = ss.PATIENT_TYPE_ID;
                                                rs.RationTimeIds = new List<long>() { serviceReq.RATION_TIME_ID ?? 0 };
                                                rs.RoomId = serviceReq.EXECUTE_ROOM_ID;
                                                rs.ServiceId = ss.SERVICE_ID;
                                                rs.InstructionNote = ss.INSTRUCTION_NOTE;
                                                data.RationServices.Add(rs);
                                            }

                                            HisServiceReqListResultSDO resultData = null;
                                            CommonParam param = new CommonParam();
                                            if (!new HisServiceReqRationCreate(param).Run(data, false, ref resultData))
                                            {
                                                Inventec.Common.Logging.LogSystem.Error("Lỗi báo ăn tự động: " + Inventec.Common.Logging.LogUtil.TraceData("____param___", param) + Inventec.Common.Logging.LogUtil.TraceData("____data___", data));
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    LogSystem.Debug("Tien trinh tu dong AutoAssignRation thoi gian khong phai nam trong khoang duoc cau hinh");
                }

                IS_SENDING = false;
            }
            catch (Exception ex)
            {
                IS_SENDING = false;
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
