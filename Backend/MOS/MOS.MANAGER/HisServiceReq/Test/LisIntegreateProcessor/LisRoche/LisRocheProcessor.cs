using Inventec.Common.Logging;
using Inventec.Common.WebApiClient;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LIS.RocheV2;
using MOS.LIS.RocheV3;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.TDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Test.LisIntegreateProcessor.LisRoche
{
    class LisRocheProcessor : BusinessBase, ILisProcessor
    {
        internal LisRocheProcessor()
            : base()
        {

        }

        internal LisRocheProcessor(CommonParam param)
            : base(param)
        {

        }

        public bool RequestOrder(OrderData data, ref List<string> messages)
        {
            bool result = false;
            try
            {
                RocheAstmOrderMessage message = new RocheAstmOrderMessage(
                    LisRocheCFG.SENDING_APP_CODE,
                    LisRocheCFG.RECEIVING_APP_CODE,
                    PatientMaker.MakePatientData(data.ServiceReq, data.Inserts),
                    OrderMaker.MakeUpdateOrderData(data.ServiceReq, data.Inserts, data.Deletes));

                RocheHl7OrderMessage hl7Message = new RocheHl7OrderMessage(
                    PatientMaker.MakeHl7PatientData(data.ServiceReq, data.Inserts),
                    OrderMaker.MakeUpdateHl7OrderData(data.ServiceReq, data.Inserts, data.Deletes));

                //result = new RocheSender(param).Run(data.ServiceReq.BARCODE, data.ServiceReq.EXECUTE_ROOM_ID, this.MakeMessage(message));

                if (LisRocheCFG.MESSAGE_FORMAT_TYPE == RocheMessageFormatType.ASTM_HL7)
                {
                    bool rs1 = new RocheSender(null, false).Run(data.ServiceReq.BARCODE, data.ServiceReq.EXECUTE_ROOM_ID, this.MakeMessage(message));
                    bool rs2 = new RocheSender(null, true).Run(data.ServiceReq.BARCODE, data.ServiceReq.EXECUTE_ROOM_ID, this.MakeMessage(hl7Message));
                    result = rs1 || rs2;
                }
                else if (LisRocheCFG.MESSAGE_FORMAT_TYPE == RocheMessageFormatType.HL7)
                {
                    result = new RocheSender(param, true).Run(data.ServiceReq.BARCODE, data.ServiceReq.EXECUTE_ROOM_ID, this.MakeMessage(hl7Message));
                }
                else if (LisRocheCFG.MESSAGE_FORMAT_TYPE == RocheMessageFormatType.ASTM)
                {
                    result = new RocheSender(param, false).Run(data.ServiceReq.BARCODE, data.ServiceReq.EXECUTE_ROOM_ID, this.MakeMessage(message));
                }
            }
            catch (Exception ex)
            {
                result = false;
                LogSystem.Error(ex);
            }
            return result;
        }

        public bool DeleteOrder(OrderData data, ref List<string> messages)
        {
            bool result = false;
            try
            {
                RocheAstmOrderMessage message = new RocheAstmOrderMessage(
                    LisRocheCFG.SENDING_APP_CODE,
                    LisRocheCFG.RECEIVING_APP_CODE,
                    PatientMaker.MakePatientData(data.ServiceReq, data.Availables),
                    OrderMaker.MakeOrderData(data.ServiceReq, data.Availables, OrderType.DELETE_SAMPLE));

                RocheHl7OrderMessage hl7Message = new RocheHl7OrderMessage(
                    PatientMaker.MakeHl7PatientData(data.ServiceReq, data.Availables),
                    OrderMaker.MakeHl7OrderData(data.ServiceReq, data.Availables, Hl7OrderType.DELETE_SAMPLE));

                if (LisRocheCFG.MESSAGE_FORMAT_TYPE == RocheMessageFormatType.ASTM_HL7)
                {
                    bool rs1 = new RocheSender(null, false).Run(data.ServiceReq.BARCODE, data.ServiceReq.EXECUTE_ROOM_ID, this.MakeMessage(message));
                    bool rs2 = new RocheSender(null, true).Run(data.ServiceReq.BARCODE, data.ServiceReq.EXECUTE_ROOM_ID, this.MakeMessage(hl7Message));
                    result = rs1 || rs2;
                }
                else if (LisRocheCFG.MESSAGE_FORMAT_TYPE == RocheMessageFormatType.HL7)
                {
                    result = new RocheSender(param, true).Run(data.ServiceReq.BARCODE, data.ServiceReq.EXECUTE_ROOM_ID, this.MakeMessage(hl7Message));
                }
                else if (LisRocheCFG.MESSAGE_FORMAT_TYPE == RocheMessageFormatType.ASTM)
                {
                    result = new RocheSender(param, false).Run(data.ServiceReq.BARCODE, data.ServiceReq.EXECUTE_ROOM_ID, this.MakeMessage(message));
                }
            }
            catch (Exception ex)
            {
                result = false;
                LogSystem.Error(ex);
            }
            return result;
        }

        public bool UpdatePatientInfo(HIS_PATIENT data, ref List<string> messages)
        {
            bool result = false;
            try
            {
                //Chi ho tro voi loai HL7
                if (LisRocheCFG.MESSAGE_FORMAT_TYPE == RocheMessageFormatType.ASTM_HL7
                    || LisRocheCFG.MESSAGE_FORMAT_TYPE == RocheMessageFormatType.HL7)
                {
                    string address = data.VIR_ADDRESS;
                    string patientName = data.VIR_PATIENT_NAME;
                    string patientId = data.PATIENT_CODE;
                    DateTime dateOfBirth = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(data.DOB).Value;
                    RocheHl7Gender gender = data.GENDER_ID == IMSys.DbConfig.HIS_RS.HIS_GENDER.ID__FEMALE ?
                        RocheHl7Gender.FEMALE : RocheHl7Gender.MALE;
                    string phoneNumber = !string.IsNullOrWhiteSpace(data.PHONE) ? data.PHONE : data.MOBILE;

                    RocheHl7UpdatePatientInfoMessage hl7Message = new RocheHl7UpdatePatientInfoMessage(patientId, patientName, dateOfBirth, gender, address, phoneNumber);

                    result = new RocheSender(param, true).Run(data.PATIENT_CODE, null, this.MakeMessage(hl7Message));
                }
            }
            catch (Exception ex)
            {
                result = false;
                LogSystem.Error(ex);
            }
            return result;
        }

        /// <summary>
        /// Cap nhat thong tin ket qua cua yeu cau xet nghiem dua vao message tra ve
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal bool UpdateResult(string data, bool isHl7)
        {
            bool result = false;
            try
            {
                if (isHl7)
                {
                    RocheHl7BaseMessage message = RocheHl7Util.DetechReceivingMessage(data);

                    if (message != null)
                    {
                        //Neu la goi tin "ket qua" --> luu ket qua
                        //Neu la goi tin "sample seen" (da lay mau) --> cap nhat trang thai sang "dang thuc hien"
                        if (message.Type == Hl7MessageType.RESULT)
                        {
                            HisTestResultTDO orderResult = this.Convert((RocheHl7ResultMessage)message);
                            return new HisServiceReqTestUpdate().UpdateResult(orderResult, true);
                        }
                        else if (message.Type == Hl7MessageType.ANTIBIOTIC_RESULT)
                        {
                            HisTestResultTDO orderResult = this.Convert((RocheHl7AntibioticResultMessage)message);
                            return new HisServiceReqTestUpdate().UpdateResult(orderResult, true);
                        }
                        else if (message.Type == Hl7MessageType.SAMPLE_SEEN)
                        {
                            string seenOrderCode = ((RocheHl7SampleSeenMessage)message).OrderCode;
                            return new HisServiceReqTestUpdate().UpdateStart(seenOrderCode, "roche", "roche");
                        }
                    }
                }
                else
                {
                    RocheAstmBaseMessage message = RocheAstmUtil.DetechReceivingMessage(data, LisRocheCFG.SENDING_APP_CODE, LisRocheCFG.RECEIVING_APP_CODE);

                    if (message != null)
                    {
                        //Neu la goi tin "ket qua" --> luu ket qua
                        //Neu la goi tin "sample seen" (da lay mau) --> cap nhat trang thai sang "dang thuc hien"
                        if (message.Type == MessageType.RESULT)
                        {
                            HisTestResultTDO orderResult = this.Convert((RocheAstmResultMessage)message);
                            return new HisServiceReqTestUpdate().UpdateResult(orderResult, true);
                        }
                        else if (message.Type == MessageType.SAMPLE_SEEN)
                        {
                            string seenOrderCode = ((RocheAstmSampleSeenMessage)message).OrderData.OrderCode;
                            return new HisServiceReqTestUpdate().UpdateStart(seenOrderCode, "roche", "roche");
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        private HisTestResultTDO Convert(RocheAstmResultMessage rocheResultMessage)
        {
            if (rocheResultMessage.OrderData != null && rocheResultMessage.TestIndexResults != null && rocheResultMessage.TestIndexResults.Count > 0)
            {
                HisTestResultTDO data = new HisTestResultTDO();
                data.ServiceReqCode = rocheResultMessage.OrderData.OrderCode;

                List<HisTestIndexResultTDO> testIndexDatas = new List<HisTestIndexResultTDO>();
                foreach (RocheAstmResultRecordData index in rocheResultMessage.TestIndexResults)
                {
                    HisTestIndexResultTDO tdo = new HisTestIndexResultTDO();
                    tdo.Value = index.Value;
                    tdo.TestIndexCode = index.TestIndexCode;
                    tdo.MayXetNghiemID = index.MachineCode;
                    testIndexDatas.Add(tdo);
                }
                data.TestIndexDatas = testIndexDatas;
                return data;
            }
            return null;
        }

        private HisTestResultTDO Convert(RocheHl7ResultMessage rocheResultMessage)
        {
            if (!string.IsNullOrWhiteSpace(rocheResultMessage.OrderCode) && rocheResultMessage.TestIndexResults != null && rocheResultMessage.TestIndexResults.Count > 0)
            {
                HisTestResultTDO data = new HisTestResultTDO();
                data.ServiceReqCode = rocheResultMessage.OrderCode;

                List<HisTestIndexResultTDO> testIndexDatas = new List<HisTestIndexResultTDO>();
                foreach (RocheHl7ResultRecordData index in rocheResultMessage.TestIndexResults)
                {
                    HisTestIndexResultTDO tdo = new HisTestIndexResultTDO();
                    tdo.Value = index.Value;
                    tdo.TestIndexCode = index.TestIndexCode;
                    tdo.MayXetNghiemID = index.MachineCode;
                    testIndexDatas.Add(tdo);
                }
                data.TestIndexDatas = testIndexDatas;
                return data;
            }
            return null;
        }

        private HisTestResultTDO Convert(RocheHl7AntibioticResultMessage rocheResultMessage)
        {
            if (!string.IsNullOrWhiteSpace(rocheResultMessage.OrderCode) && rocheResultMessage.Results != null && rocheResultMessage.Results.Count > 0)
            {
                HisTestResultTDO data = new HisTestResultTDO();
                data.ServiceReqCode = rocheResultMessage.OrderCode;

                List<HisTestIndexResultTDO> testIndexDatas = new List<HisTestIndexResultTDO>();
                foreach (RocheHl7MicroBioticResultRecordData index in rocheResultMessage.Results)
                {
                    if (IsNotNullOrEmpty(index.Antibiotics))
                    {
                        foreach (RocheHl7AntibioticData d in index.Antibiotics)
                        {
                            HisTestIndexResultTDO tdo = new HisTestIndexResultTDO();
                            tdo.BacteriumCode = index.BacteriaCode;
                            tdo.BacteriumName = index.BacteriaName;
                            tdo.TestIndexCode = index.TestIndexCode;
                            tdo.MayXetNghiemID = index.MachineCode;
                            tdo.AntibioticCode = d.AntibioticCode;
                            tdo.AntibioticName = d.AntibioticName;
                            tdo.Mic = d.Result;
                            tdo.SriCode = d.SRI;
                            testIndexDatas.Add(tdo);
                        }
                    }
                    else
                    {
                        HisTestIndexResultTDO tdo = new HisTestIndexResultTDO();
                        tdo.BacteriumCode = index.BacteriaCode;
                        tdo.BacteriumName = index.BacteriaName;
                        tdo.TestIndexCode = index.TestIndexCode;
                        tdo.MayXetNghiemID = index.MachineCode;
                        testIndexDatas.Add(tdo);
                    }
                }
                data.TestIndexDatas = testIndexDatas;
                return data;
            }
            return null;
        }

        private string MakeMessage(RocheAstmOrderMessage message)
        {
            if (message != null)
            {
                return LisRocheCFG.MESSAGE_FORMAT_IS_USING_VIETNAMESE ?
                    message.ToMessage() : Inventec.Common.String.Convert.UnSignVNese(message.ToMessage());
            }
            return null;
        }

        private string MakeMessage(RocheHl7BaseMessage message)
        {
            if (message != null)
            {
                return message.ToMessage();
            }
            return null;
        }
    }
}