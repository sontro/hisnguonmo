using Inventec.Common.Logging;
using Inventec.Core;
using MOS.ApiConsumerManager;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisCard;
using MOS.MANAGER.HisPatient;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServTein;
using MOS.TDO;
using MOS.UTILITY;
using SMS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Test
{
    /// <summary>
    /// Xu ly tich hop tra ket qua XN sang he thong EHR
    /// </summary>
    class HisServiceReqTestEhrResulting : BusinessBase
    {
        internal HisServiceReqTestEhrResulting()
            : base()
        {

        }

        internal HisServiceReqTestEhrResulting(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(HIS_PATIENT patient, HIS_SERVICE_REQ data, List<HIS_SERE_SERV> sereServs, List<V_HIS_SERE_SERV_TEIN> ssTeins, ref string biinResult)
        {
            bool result = false;
            try
            {
                if (data != null
                    && IsNotNullOrEmpty(sereServs)
                    && IsNotNullOrEmpty(ssTeins)
                    && patient != null
                    && ApiConsumerStore.EhrLisConsumer != null)
                {
                    Dictionary<long, int> genderMap = new Dictionary<long, int>(){
                        {IMSys.DbConfig.HIS_RS.HIS_GENDER.ID__FEMALE, (int) EhrGender.FEMALE},
                        {IMSys.DbConfig.HIS_RS.HIS_GENDER.ID__MALE, (int) EhrGender.MALE},
                        {IMSys.DbConfig.HIS_RS.HIS_GENDER.ID__UNKNOWN, (int) EhrGender.UNKNOWN}
                    };

                    List<HIS_CARD> cards = new HisCardGet().GetByPatientId(data.TDL_PATIENT_ID);

                    EhrLisResultingTDO resulting = new EhrLisResultingTDO();

                    V_HIS_ROOM room = HisRoomCFG.DATA.Where(o => o.ID == data.EXECUTE_ROOM_ID).FirstOrDefault();

                    List<EhrLisResultTDO> lisResults = new List<EhrLisResultTDO>();
                    foreach (HIS_SERE_SERV ss in sereServs)
                    {
                        List<V_HIS_SERE_SERV_TEIN> teins = ssTeins.Where(o => o.SERE_SERV_ID == ss.ID).ToList();
                        if (IsNotNullOrEmpty(teins))
                        {
                            string value = "";
                            string ref_value = "";
                            foreach (V_HIS_SERE_SERV_TEIN t in teins)
                            {
                                value = value + string.Format("{0} ---- {1} – {2},", t.TEST_INDEX_NAME, t.VALUE, t.TEST_INDEX_UNIT_NAME);
                                ref_value = ref_value + string.Format("({0}),", t.DESCRIPTION);
                            }
                            value = value.Substring(0, value.Length - 1);
                            ref_value = ref_value.Substring(0, ref_value.Length - 1);
                            EhrLisResultTDO lisRs = new EhrLisResultTDO();
                            lisRs.group_name = ss.TDL_SERVICE_NAME;
                            lisRs.value = value;
                            lisRs.ref_value = ref_value;

                            lisResults.Add(lisRs);
                        }
                    }
                    string phoneNumber = !string.IsNullOrWhiteSpace(patient.MOBILE) ? patient.MOBILE : patient.PHONE;

                    EhrPatientTDO subject = new EhrPatientTDO();
                    subject.address = data.TDL_PATIENT_ADDRESS;
                    subject.birth_date = data.TDL_PATIENT_DOB.ToString().Substring(0, 8);
                    subject.card_code = IsNotNullOrEmpty(cards) ? cards.OrderBy(o => o.ID).FirstOrDefault().CARD_CODE : patient.REGISTER_CODE;
                    subject.gender = data.TDL_PATIENT_GENDER_ID.HasValue ? genderMap[data.TDL_PATIENT_GENDER_ID.Value] : (int)EhrGender.OTHER;
                    subject.name = data.TDL_PATIENT_NAME;
                    subject.phone_number = phoneNumber;

                    resulting.subject = subject;
                    resulting.identifier = data.ID.ToString();
                    resulting.test_date = data.FINISH_TIME.HasValue ? data.FINISH_TIME.ToString() : null;
                    resulting.apppointed_doctor = data.REQUEST_USERNAME;
                    resulting.examination_doctor = data.EXECUTE_USERNAME;
                    resulting.examination_unit = room.ROOM_NAME;
                    resulting.result = lisResults;

                    var rs = ApiConsumerStore.EhrLisConsumer.PostWithouApiParam<EhrResponseTDO>("", resulting, 0);
                    if (rs != null && rs.code == (int)EhrResponseCode.SUCCESS)
                    {
                        result = true;
                        biinResult = rs.result;
                    }
                    else
                    {
                        LogSystem.Warn("Gui ket qua sang EHR that bai. ServiceReqCode: " + data.SERVICE_REQ_CODE + "." + LogUtil.TraceData("Input", resulting) + "." + "." + LogUtil.TraceData("ResponseData", rs) + LogUtil.TraceData("ResponseData", rs));
                        result = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
    }
}
