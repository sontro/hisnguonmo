using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTreatment.Get
{
    class ServiceInformation
    {
        public string TDL_SERVICE_NAME { get; set; }
        public string EXECUTE_ROOM_NAME { get; set; }
        public long SERVICE_REQ_STT_ID { get; set; }
        public short? IS_WAIT_CHILD { get; set; } 
        public short? HAS_CHILD { get; set; }
        public long TREATMENT_ID { get; set; }
        public long TDL_SERVICE_TYPE_ID { get; set; }
        public long? PARENT_ID { get; set; }
        public long? RESULTING_ORDER { get; set; }
        public long? NUM_ORDER { get; set; }
        public long? SERVICE_REQ_ID { get; set; }
        public string ADDRESS { get; set; }
    }

    class TreatmentInformation
    {
        public long ID { get; set; }
        public long TDL_PATIENT_DOB { get; set; }
        public short? TDL_PATIENT_IS_HAS_NOT_DAY_DOB { get; set; }
        public string TREATMENT_CODE { get; set; }
        public string TDL_HEIN_CARD_NUMBER { get; set; }
        public string TDL_PATIENT_NAME { get; set; }
        public string TDL_PATIENT_GENDER_NAME { get; set; }
        public string TDL_HEIN_MEDI_ORG_NAME { get; set; }
        public string TDL_PATIENT_ADDRESS { get; set; }
        public string TDL_PATIENT_MOBILE { get; set; }
    }

    class HisTreatmentGetKioskInformation : GetBase
    {

        internal HisTreatmentGetKioskInformation()
            : base()
        {

        }

        internal HisTreatmentGetKioskInformation(CommonParam param)
            : base(param)
        {

        }

        internal List<KioskInformationSDO> Run(string input)
        {
            List<KioskInformationSDO> result = null;
            try
            {
                if (!string.IsNullOrWhiteSpace(input))
                {
                    List<long> typeIds = new List<long>(){
                        IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__CDHA,
                        IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__GPBL,
                        IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__KH,
                        IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__NS,
                        IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__PHCN,
                        IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__PT,
                        IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__SA,
                        IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__TDCN,
                        IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__TT,
                        IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__XN
                    };
                    string mobile = input;
                    string treatmentCode = input;

                    //Tu dong xu ly de dien them so 0 phia truoc cho du 12 ky tu
                    try
                    {
                        long num = long.Parse(input);
                        treatmentCode = num.ToString("D12");
                    }
                    catch(Exception ex)
                    {
                        LogSystem.Warn(ex);
                    }

                    string sql = "SELECT ID, TDL_HEIN_CARD_NUMBER, TDL_PATIENT_NAME, TDL_PATIENT_GENDER_NAME, TDL_PATIENT_DOB, TDL_PATIENT_IS_HAS_NOT_DAY_DOB, TDL_HEIN_MEDI_ORG_NAME, TDL_PATIENT_ADDRESS, TDL_PATIENT_MOBILE, TREATMENT_CODE FROM HIS_TREATMENT WHERE TDL_PATIENT_MOBILE = :param1 OR TDL_PATIENT_PHONE = :param2 OR TREATMENT_CODE = :param3";
                    List<TreatmentInformation> treatments = DAOWorker.SqlDAO.GetSql<TreatmentInformation>(sql, mobile, mobile, treatmentCode);

                    if (IsNotNullOrEmpty(treatments))
                    {
                        List<long> treatmentIds = treatments.Select(o => o.ID).ToList();
                        string treatmentIdStr = string.Join(",", treatmentIds);
                        string typeIdStr = string.Join(",", typeIds);

                        string servicesSql = "SELECT TDL_SERVICE_NAME, EXECUTE_ROOM_NAME, SERVICE_REQ_STT_ID, IS_WAIT_CHILD, HAS_CHILD, TREATMENT_ID, TDL_SERVICE_TYPE_ID, SR.PARENT_ID,SR.RESULTING_ORDER, SR.NUM_ORDER, SS.SERVICE_REQ_ID, R.ADDRESS "
                        + " FROM HIS_SERE_SERV SS "
                        + " JOIN HIS_SERVICE_REQ SR ON SR.ID = SS.SERVICE_REQ_ID "
                        + " JOIN HIS_EXECUTE_ROOM ER ON ER.ROOM_ID = SR.EXECUTE_ROOM_ID "
                        + " JOIN HIS_ROOM R ON R.ID = ER.ROOM_ID "
                        + " WHERE SR.IS_DELETE = 0 AND SR.IS_NO_EXECUTE IS NULL "
                        + " AND TDL_SERVICE_TYPE_ID IN (" + typeIdStr + ")"
                        + " AND TREATMENT_ID IN (" + treatmentIdStr + ")";

                        List<ServiceInformation> services = DAOWorker.SqlDAO.GetSql<ServiceInformation>(servicesSql);

                        result = new List<KioskInformationSDO>();

                        foreach (TreatmentInformation t in treatments)
                        {
                            KioskInformationSDO sdo = new KioskInformationSDO();
                            sdo.Dob = t.TDL_PATIENT_DOB;
                            sdo.HasNotDayDob = t.TDL_PATIENT_IS_HAS_NOT_DAY_DOB == Constant.IS_TRUE;
                            sdo.HeinCardNumber = t.TDL_HEIN_CARD_NUMBER;
                            sdo.HeinMediOrgName = t.TDL_HEIN_MEDI_ORG_NAME;
                            sdo.PatientAddress = t.TDL_PATIENT_ADDRESS;
                            sdo.PatientName = t.TDL_PATIENT_NAME;
                            sdo.GenderName = t.TDL_PATIENT_GENDER_NAME;
                            sdo.TreatmentCode = t.TREATMENT_CODE;
                            sdo.Mobile = t.TDL_PATIENT_MOBILE;

                            List<ServiceInformation> exams = services != null ? services.Where(o => o.TREATMENT_ID == t.ID && o.TDL_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__KH).ToList() : null;
                            List<ServiceInformation> subs = services != null ? services.Where(o => o.TREATMENT_ID == t.ID && o.TDL_SERVICE_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__KH).ToList() : null;

                            if (IsNotNullOrEmpty(exams))
                            {
                                List<KioskServiceSDO> examinations = new List<KioskServiceSDO>();
                                foreach (ServiceInformation s in exams)
                                {
                                    KioskServiceSDO kioskService = new KioskServiceSDO();
                                    kioskService.RoomName = s.EXECUTE_ROOM_NAME;
                                    kioskService.ServiceName = s.TDL_SERVICE_NAME;
                                    kioskService.Address = s.ADDRESS;
                                    kioskService.ResultingNumOrder = s.RESULTING_ORDER;
                                    kioskService.NumOrder = s.NUM_ORDER;
                                    if (s.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL)
                                    {
                                        kioskService.Status = STATUS.NEW;
                                    }
                                    else if (s.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__DXL && (s.HAS_CHILD == Constant.IS_TRUE || (subs != null && subs.Exists(o => o.PARENT_ID == s.SERVICE_REQ_ID))) && s.IS_WAIT_CHILD == Constant.IS_TRUE)
                                    {
                                        kioskService.Status = STATUS.WAIT_FOR_SUBCLINICAL;
                                    }
                                    else if (s.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__DXL && (s.HAS_CHILD == Constant.IS_TRUE || (subs != null && subs.Exists(o => o.PARENT_ID == s.SERVICE_REQ_ID))) && s.IS_WAIT_CHILD != Constant.IS_TRUE)
                                    {
                                        kioskService.Status = STATUS.WAIT_FOR_CONCLUSION;
                                    }
                                    else if (s.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__DXL && s.HAS_CHILD != Constant.IS_TRUE)
                                    {
                                        kioskService.Status = STATUS.IN_PROCESS;
                                    }
                                    else if (s.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__HT)
                                    {
                                        kioskService.Status = STATUS.FINISH;
                                    }
                                    examinations.Add(kioskService);
                                }
                                sdo.Examinations = examinations;
                            }

                            if (IsNotNullOrEmpty(subs))
                            {
                                List<KioskServiceSDO> subclinicals = new List<KioskServiceSDO>();
                                foreach (ServiceInformation s in subs)
                                {
                                    KioskServiceSDO kioskService = new KioskServiceSDO();
                                    kioskService.RoomName = s.EXECUTE_ROOM_NAME;
                                    kioskService.ServiceName = s.TDL_SERVICE_NAME;
                                    kioskService.Address = s.ADDRESS;
                                    kioskService.ResultingNumOrder = s.RESULTING_ORDER;
                                    kioskService.NumOrder = s.NUM_ORDER;
                                    if (s.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL)
                                    {
                                        kioskService.Status = STATUS.NEW;
                                    }
                                    else if (s.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__DXL)
                                    {
                                        kioskService.Status = STATUS.IN_PROCESS;
                                    }
                                    else if (s.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__HT)
                                    {
                                        kioskService.Status = STATUS.FINISH;
                                    }
                                    subclinicals.Add(kioskService);
                                }
                                sdo.Subclinicals = subclinicals;
                            }
                            result.Add(sdo);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
                param.HasException = true;
            }
            return result;
        }
    }
}
