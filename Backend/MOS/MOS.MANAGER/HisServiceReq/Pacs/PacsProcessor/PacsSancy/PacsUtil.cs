using MedilinkHL7.SDK;
using NHapi.Base.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHapi.Base.Model;
using NHapi.Base.validation;
using System.Globalization;
using MOS.MANAGER.Config;

namespace MOS.MANAGER.HisServiceReq.Pacs.PacsProcessor.PacsSancy
{
    class PacsUtil
    {
        internal static PacsReceivedData ProcessReceived(string data)
        {
            PacsReceivedData result = null;
            switch (PacsCFG.LIBRARY_HL7_VERSION)
            {
                case SendSANCY.VersionV2.V21:
                    break;
                case SendSANCY.VersionV2.V22:
                    break;
                case SendSANCY.VersionV2.V23:
                    break;
                case SendSANCY.VersionV2.V231:
                    result = ProcessReceivedHl7_231(data);
                    break;
                case SendSANCY.VersionV2.V24:
                    result = ProcessReceivedHl7_24(data);
                    break;
                case SendSANCY.VersionV2.V25:
                    break;
                case SendSANCY.VersionV2.V251:
                    break;
                case SendSANCY.VersionV2.V26:
                    break;
                case SendSANCY.VersionV2.V27:
                    result = ProcessReceivedHl7_27(data);
                    break;
                case SendSANCY.VersionV2.V271:
                    break;
                case SendSANCY.VersionV2.V28:
                    break;
                case SendSANCY.VersionV2.V281:
                    break;
                case SendSANCY.VersionV2.V282:
                    break;
                default:
                    break;
            }

            return result;
        }

        private static PacsReceivedData ProcessReceivedHl7_24(string data)
        {
            PacsReceivedData result = null;
            try
            {
                Inventec.Common.Logging.LogSystem.Debug(String.Format("extracHl7 Received data: {0}", data));
                if (data.Contains("ORU^R01"))
                {
                    StringBuilder sb = new StringBuilder(data);
                    MedilinkHL7.SDK.HL7Sender sender = new MedilinkHL7.SDK.HL7Sender();
                    string s_Received = MedilinkHL7.SDK.HL7Sender.StripMLLPContainer(sb);

                    //bản tị Hl7 lỗi Parse khi ký tự xuống dòng là \n cần đổi sang \r
                    s_Received = s_Received.Replace('\n', '\r');
                    s_Received = s_Received.Trim('\r');

                    var parser = new PipeParser();
                    var r24_Temp = (NHapi.Model.V24.Message.ORU_R01)parser.Parse(s_Received);
                    if (r24_Temp != null)
                    {
                        NHapi.Model.V24.Group.ORU_R01_PATIENT_RESULT patientResult = r24_Temp.GetPATIENT_RESULT();
                        string studyID = "";
                        if (patientResult != null && patientResult.ORDER_OBSERVATIONRepetitionsUsed > 0)
                        {
                            for (int i = 0; i < patientResult.ORDER_OBSERVATIONRepetitionsUsed; i++)
                            {
                                NHapi.Model.V24.Group.ORU_R01_ORDER_OBSERVATION order = patientResult.GetORDER_OBSERVATION(i);
                                if (order != null)
                                {
                                    NHapi.Model.V24.Segment.ORC orc = order.ORC;
                                    NHapi.Model.V24.Segment.OBR obr = order.OBR;
                                    if (orc != null && obr != null)
                                    {
                                        decimal idChiDinh = 0M;
                                        if (orc != null)
                                        {
                                            decimal.TryParse(orc.PlacerOrderNumber.EntityIdentifier.Value, out idChiDinh);
                                        }

                                        if (idChiDinh == 0M && obr != null)
                                        {
                                            decimal.TryParse(obr.PlacerOrderNumber.EntityIdentifier.Value, out idChiDinh);
                                        }

                                        if (idChiDinh == 0M)
                                        {
                                            string err = "Không lấy được id chỉ định để cập nhật kết quả. Có thể: ORC.PlacerOrderNumber.EntityIdentifier=0 và OBR.PlacerOrderNumber.EntityIdentifier=0. 1 trong 2 field này phải khác 0";
                                            throw new Exception(err);
                                        }
                                        else
                                        {
                                            DateTime dti_NgayTraKetQua = DateTime.Now, dti_thoiGianBatDauChup = DateTime.Now, dti_thoiGianKetThucChup = DateTime.Now;
                                            string thoiGianBatDauChup = obr.ObservationDateTime.Time.Value;
                                            string thoiGianKetThucChup = obr.ObservationEndDateTime.Time.Value;
                                            string thoiGianTraKetQua = "";
                                            string mayThucHien = "";//2.3.1 không có thông tin máy thực hiện trong obx
                                            string KTVTraKQ = "";
                                            string MaKTVTraKQ = "";
                                            string descriptionDetail = "";

                                            NHapi.Model.V24.Group.ORU_R01_OBSERVATION Observation = order.GetOBSERVATION();
                                            if (Observation != null)
                                            {
                                                NHapi.Model.V24.Segment.OBX obx = Observation.OBX;
                                                if (obx != null)
                                                {
                                                    thoiGianTraKetQua = obx.DateTimeOfTheObservation.Time.Value;

                                                    if (obx.EquipmentInstanceIdentifier.Length == 0)
                                                    {
                                                        Inventec.Common.Logging.LogSystem.Error("obx.EquipmentInstanceIdentifier is null");
                                                    }
                                                    else
                                                    {
                                                        try
                                                        {
                                                            mayThucHien = obx.EquipmentInstanceIdentifier[0].EntityIdentifier.Value.ToString();
                                                        }
                                                        catch
                                                        {
                                                        }
                                                    }

                                                    //có 3 thông tin phổ biến của ResponsibleObserver là IDNumber, FamilyName , GivenName
                                                    // FamilyName , GivenName tên người thực hiện
                                                    if (obx.ResponsibleObserver != null)
                                                    {
                                                        try
                                                        {
                                                            KTVTraKQ = obx.ResponsibleObserver.GivenName.Value;
                                                            MaKTVTraKQ = obx.ResponsibleObserver.IDNumber.Value;
                                                        }
                                                        catch
                                                        {
                                                            string err = "obx.ResponsibleObserver is null";
                                                            Inventec.Common.Logging.LogSystem.Error(err);
                                                        }
                                                    }

                                                    if (obx.ObservationSubId != null)
                                                    {
                                                        studyID = obx.ObservationSubId.ToString();
                                                        if (String.IsNullOrWhiteSpace(studyID) && studyID.Contains("^"))
                                                        {
                                                            int _index = studyID.LastIndexOf("^");
                                                            studyID = studyID.Substring(_index).Trim('^');
                                                        }
                                                    }

                                                    if (obx.ObservationValue.Length > 0)
                                                    {
                                                        string des = "";
                                                        foreach (var item in obx.ObservationValue)
                                                        {
                                                            if (item.Data != null)
                                                            {
                                                                des += item.Data.ToString() + "^";
                                                            }
                                                        }

                                                        descriptionDetail = des.Trim('^');
                                                    }
                                                }
                                                else
                                                {
                                                    throw new Exception("HL7 thiếu phần ORU_R01_PATIENT_RESULT.ORU_R01_ORDER_OBSERVATION.ORU_R01_OBSERVATION.OBX - Phần thông tin kết quả");
                                                }
                                            }
                                            else
                                            {
                                                throw new Exception("HL7 thiếu phần ORU_R01_PATIENT_RESULT.ORU_R01_ORDER_OBSERVATION.ORU_R01_OBSERVATION");
                                            }

                                            //co nhieu dong OBX thi ghep noi dung lai
                                            if (order.OBSERVATIONRepetitionsUsed > 1)
                                            {
                                                List<string> descrip = new List<string>();
                                                for (int j = 0; j < order.OBSERVATIONRepetitionsUsed; j++)
                                                {
                                                    string des = "";
                                                    NHapi.Model.V24.Group.ORU_R01_OBSERVATION Observationi = order.GetOBSERVATION(j);
                                                    if (Observationi != null)
                                                    {
                                                        NHapi.Model.V24.Segment.OBX obxx = Observationi.OBX;
                                                        if (obxx.ObservationValue.Length > 0)
                                                        {
                                                            foreach (var item in obxx.ObservationValue)
                                                            {
                                                                if (item.Data != null)
                                                                {
                                                                    des += item.Data.ToString() + "^";
                                                                }
                                                            }
                                                            descrip.Add(des.Trim('^'));
                                                        }
                                                    }
                                                }

                                                descriptionDetail = string.Join("\r\n", descrip);
                                            }

                                            if (String.IsNullOrWhiteSpace(thoiGianKetThucChup))
                                            {
                                                thoiGianKetThucChup = thoiGianTraKetQua;
                                            }

                                            if (!String.IsNullOrEmpty(thoiGianKetThucChup))
                                            {
                                                try
                                                {
                                                    if (thoiGianKetThucChup.Length == 8)
                                                    {
                                                        dti_thoiGianKetThucChup = DateTime.ParseExact(thoiGianKetThucChup, "yyyyMMdd", CultureInfo.InvariantCulture);
                                                    }
                                                    else if (thoiGianKetThucChup.Length == 12)
                                                    {
                                                        dti_thoiGianKetThucChup = DateTime.ParseExact(thoiGianKetThucChup, "yyyyMMddHHmm", CultureInfo.InvariantCulture);
                                                    }
                                                    else if (thoiGianKetThucChup.Length == 14)
                                                    {
                                                        dti_thoiGianKetThucChup = DateTime.ParseExact(thoiGianKetThucChup, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
                                                    }
                                                    else
                                                    {
                                                        dti_thoiGianKetThucChup = DateTime.Parse(thoiGianKetThucChup);
                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    Inventec.Common.Logging.LogSystem.Error("Loi dinh dang ngay thang trong HL7: obx.ObservationEndDateTime \r\n >>>>>>>>>>>>>>>>>>>>>>>>>>>> lay thoi gian hien tai");
                                                    Inventec.Common.Logging.LogSystem.Error(ex);
                                                    dti_thoiGianKetThucChup = DateTime.Now;
                                                }
                                            }
                                            else
                                            {
                                                Inventec.Common.Logging.LogSystem.Error("obx.ObservationEndDateTime is null");
                                                dti_thoiGianKetThucChup = DateTime.Now;
                                            }

                                            if (!String.IsNullOrEmpty(thoiGianBatDauChup))
                                            {
                                                try
                                                {
                                                    if (thoiGianBatDauChup.Length == 8)
                                                    {
                                                        dti_thoiGianBatDauChup = DateTime.ParseExact(thoiGianBatDauChup, "yyyyMMdd", CultureInfo.InvariantCulture);
                                                    }
                                                    else if (thoiGianBatDauChup.Length == 12)
                                                    {
                                                        dti_thoiGianBatDauChup = DateTime.ParseExact(thoiGianBatDauChup, "yyyyMMddHHmm", CultureInfo.InvariantCulture);
                                                    }
                                                    else if (thoiGianBatDauChup.Length == 14)
                                                    {
                                                        dti_thoiGianBatDauChup = DateTime.ParseExact(thoiGianBatDauChup, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
                                                    }
                                                    else
                                                    {
                                                        dti_thoiGianBatDauChup = DateTime.Parse(thoiGianBatDauChup);
                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    Inventec.Common.Logging.LogSystem.Error("Loi dinh dang ngay thang trong HL7: obx.ObservationDateTime \r\n >>>>>>>>>>>>>>>>>>>>>>>>>>>> lay thoi gian hien tai");
                                                    Inventec.Common.Logging.LogSystem.Error(ex);
                                                    dti_thoiGianBatDauChup = DateTime.Now;
                                                }
                                            }
                                            else
                                            {
                                                Inventec.Common.Logging.LogSystem.Error("obx.ObservationDateTime is null");
                                                dti_thoiGianBatDauChup = dti_thoiGianKetThucChup;
                                            }

                                            if (dti_thoiGianKetThucChup == null)
                                            {
                                                dti_thoiGianBatDauChup = DateTime.Now;
                                                dti_thoiGianKetThucChup = DateTime.Now;
                                            }

                                            if (!String.IsNullOrEmpty(thoiGianTraKetQua))
                                            {
                                                try
                                                {
                                                    if (thoiGianTraKetQua.Length == 8)
                                                    {
                                                        dti_NgayTraKetQua = DateTime.ParseExact(thoiGianTraKetQua, "yyyyMMdd", CultureInfo.InvariantCulture);
                                                    }
                                                    else if (thoiGianTraKetQua.Length == 12)
                                                    {
                                                        dti_NgayTraKetQua = DateTime.ParseExact(thoiGianTraKetQua, "yyyyMMddHHmm", CultureInfo.InvariantCulture);
                                                    }
                                                    else if (thoiGianTraKetQua.Length == 14)
                                                    {
                                                        dti_NgayTraKetQua = DateTime.ParseExact(thoiGianTraKetQua, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
                                                    }
                                                    else
                                                    {
                                                        dti_NgayTraKetQua = DateTime.Parse(thoiGianTraKetQua);
                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    Inventec.Common.Logging.LogSystem.Error("Loi dinh dang ngay thang trong HL7: obx.DateTimeOfTheObservation");
                                                    Inventec.Common.Logging.LogSystem.Error(ex);
                                                    dti_NgayTraKetQua = dti_thoiGianKetThucChup;
                                                }
                                            }
                                            else
                                            {
                                                Inventec.Common.Logging.LogSystem.Error("obx.DateTimeOfTheObservation is null");
                                                dti_NgayTraKetQua = dti_thoiGianKetThucChup;
                                            }

                                            if (dti_NgayTraKetQua == null)
                                            {
                                                dti_NgayTraKetQua = DateTime.Now;
                                            }

                                            if (String.IsNullOrWhiteSpace(KTVTraKQ))
                                            {
                                                try
                                                {
                                                    KTVTraKQ = obr.PrincipalResultInterpreter.Name.ToString();
                                                }
                                                catch
                                                {
                                                    Inventec.Common.Logging.LogSystem.Error("obr.PrincipalResultInterpreter is null");
                                                }
                                            }

                                            List<string> maBacSiTraKQ = new List<string>();
                                            for (int t = 0; t < obr.TechnicianRepetitionsUsed; t++)
                                            {
                                                NHapi.Model.V24.Datatype.NDL technician = obr.GetTechnician(t);
                                                if (technician != null && !String.IsNullOrWhiteSpace(technician.Name.ToString()))
                                                {
                                                    maBacSiTraKQ.Add(technician.Name.ToString());
                                                }
                                            }

                                            if (maBacSiTraKQ.Count > 0)
                                            {
                                                MaKTVTraKQ = string.Join(";", maBacSiTraKQ.Distinct());
                                            }

                                            string moTa = "", ghichu = "", ketLuan = "";

                                            ProcessDescriptionDetail(descriptionDetail, ref moTa, ref ghichu, ref ketLuan);

                                            result = new PacsReceivedData();
                                            result.BeginTime = Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(dti_thoiGianBatDauChup);
                                            result.Conclude = ketLuan;
                                            result.Description = !String.IsNullOrWhiteSpace(moTa) ? moTa : descriptionDetail;
                                            result.EndTime = Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(dti_thoiGianKetThucChup);
                                            result.FinishTime = Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(dti_NgayTraKetQua);
                                            result.Machine = mayThucHien;
                                            result.Note = ghichu;
                                            result.SereServId = (long)idChiDinh;
                                            result.studyID = studyID;
                                            result.SubclinicalResultUsername = KTVTraKQ;
                                            result.SubclinicalResultLoginname = MaKTVTraKQ;
                                        }
                                    }
                                    else
                                    {
                                        if (orc == null)
                                        {
                                            throw new Exception("HL7 thiếu phần ORU_R01_PATIENT_RESULT.ORU_R01_ORDER_OBSERVATION.ORC - Phần thông tin chỉ định");
                                        }
                                        else
                                        {
                                            throw new Exception("HL7 thiếu phần ORU_R01_PATIENT_RESULT.ORU_R01_ORDER_OBSERVATION.OBR - Phần thông tin kết quả");
                                        }
                                    }
                                }
                                else
                                {
                                    throw new Exception("HL7 thiếu phần ORU_R01_PATIENT_RESULT.ORU_R01_ORDER_OBSERVATION");
                                }
                            }
                        }
                        else
                        {
                            throw new Exception("HL7 thiếu phần ORU_R01_PATIENT_RESULT");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result = null;
                Inventec.Common.Logging.LogSystem.Error(ex);
            }

            return result;
        }

        private static PacsReceivedData ProcessReceivedHl7_231(string data)
        {
            PacsReceivedData result = null;
            try
            {
                Inventec.Common.Logging.LogSystem.Debug(String.Format("extracHl7 Received data: {0}", data));
                if (data.Contains("ORU^R01"))
                {
                    StringBuilder sb = new StringBuilder(data);
                    MedilinkHL7.SDK.HL7Sender sender = new MedilinkHL7.SDK.HL7Sender();
                    string s_Received = MedilinkHL7.SDK.HL7Sender.StripMLLPContainer(sb);
                    var parser = new PipeParser();
                    var r231_Temp = (NHapi.Model.V231.Message.ORU_R01)parser.Parse(s_Received);
                    if (r231_Temp != null)
                    {
                        NHapi.Model.V231.Group.ORU_R01_PATIENT_RESULT patientResult = r231_Temp.GetPATIENT_RESULT();
                        string studyID = "";
                        if (patientResult != null && patientResult.ORDER_OBSERVATIONRepetitionsUsed > 0)
                        {
                            for (int i = 0; i < patientResult.ORDER_OBSERVATIONRepetitionsUsed; i++)
                            {
                                NHapi.Model.V231.Group.ORU_R01_ORDER_OBSERVATION order = patientResult.GetORDER_OBSERVATION(i);
                                if (order != null)
                                {
                                    NHapi.Model.V231.Segment.ORC orc = order.ORC;
                                    NHapi.Model.V231.Segment.OBR obr = order.OBR;
                                    if (orc != null && obr != null)
                                    {
                                        NHapi.Model.V231.Group.ORU_R01_OBSERVATION Observation = order.GetOBSERVATION();
                                        if (Observation != null)
                                        {
                                            decimal idChiDinh = 0M;
                                            if (orc != null)
                                            {
                                                decimal.TryParse(orc.PlacerOrderNumber.EntityIdentifier.Value, out idChiDinh);
                                            }

                                            if (idChiDinh == 0M && obr != null)
                                            {
                                                decimal.TryParse(obr.PlacerOrderNumber.EntityIdentifier.Value, out idChiDinh);
                                            }

                                            if (idChiDinh == 0M)
                                            {
                                                string err = "Không lấy được id chỉ định để cập nhật kết quả. Có thể: ORC.PlacerOrderNumber.EntityIdentifier=0 và OBR.PlacerOrderNumber.EntityIdentifier=0. 1 trong 2 field này phải khác 0";
                                                throw new Exception(err);
                                            }
                                            else
                                            {
                                                DateTime dti_NgayTraKetQua = DateTime.Now, dti_thoiGianBatDauChup = DateTime.Now, dti_thoiGianKetThucChup = DateTime.Now;
                                                string thoiGianBatDauChup = obr.ObservationDateTime.TimeOfAnEvent.Value;
                                                string thoiGianKetThucChup = obr.ObservationEndDateTime.TimeOfAnEvent.Value;
                                                string thoiGianTraKetQua = "";
                                                string KTVTraKQ = "";
                                                string MaKTVTraKQ = "";
                                                string descriptionDetail = "";

                                                //lấy 1 dòng kết quả để xử lý các thông tin khác
                                                NHapi.Model.V231.Segment.OBX obx = Observation.OBX;
                                                if (obx != null)
                                                {
                                                    thoiGianTraKetQua = obx.DateTimeOfTheObservation.TimeOfAnEvent.Value;
                                                    if (String.IsNullOrWhiteSpace(thoiGianKetThucChup))
                                                    {
                                                        thoiGianKetThucChup = thoiGianTraKetQua;
                                                    }

                                                    //có 3 thông tin phổ biến của ResponsibleObserver là IDNumber, FamilyName , GivenName
                                                    // FamilyName , GivenName tên người thực hiện
                                                    NHapi.Model.V231.Datatype.XCN[] ResponsibleObserver = obx.GetResponsibleObserver();
                                                    if (ResponsibleObserver.Length > 0)
                                                    {
                                                        try
                                                        {
                                                            KTVTraKQ = ResponsibleObserver[0].GivenName.Value;
                                                            MaKTVTraKQ = ResponsibleObserver[0].IDNumber.Value;
                                                        }
                                                        catch
                                                        {
                                                            string err = "obx.ResponsibleObserver is null";
                                                            Inventec.Common.Logging.LogSystem.Error(err);
                                                        }
                                                    }

                                                    if (String.IsNullOrWhiteSpace(KTVTraKQ))
                                                    {
                                                        try
                                                        {
                                                            KTVTraKQ = obr.PrincipalResultInterpreter.Name.ToString();
                                                        }
                                                        catch
                                                        {
                                                            Inventec.Common.Logging.LogSystem.Error("obr.PrincipalResultInterpreter is null");
                                                        }
                                                    }

                                                    if (obx.ObservationSubID != null)
                                                    {
                                                        studyID = obx.ObservationSubID.ToString();
                                                        if (String.IsNullOrWhiteSpace(studyID) && studyID.Contains("^"))
                                                        {
                                                            int _index = studyID.LastIndexOf("^");
                                                            studyID = studyID.Substring(_index).Trim('^');
                                                        }
                                                    }

                                                    Varies[] ObservationValue = obx.GetObservationValue();
                                                    if (ObservationValue.Length > 0)
                                                    {
                                                        string des = "";
                                                        foreach (var item in ObservationValue)
                                                        {
                                                            if (item.Data != null)
                                                            {
                                                                des += item.Data.ToString() + "^";
                                                            }
                                                        }

                                                        descriptionDetail = des.Trim('^');
                                                    }
                                                }
                                                else
                                                {
                                                    throw new Exception("HL7 thiếu phần ORU_R01_PATIENT_RESULT.ORU_R01_ORDER_OBSERVATION.ORU_R01_OBSERVATION.OBX - Phần thông tin kết quả");
                                                }

                                                //co nhieu dong OBX thi ghep noi dung lai
                                                if (order.OBSERVATIONRepetitionsUsed > 1)
                                                {
                                                    List<string> descrip = new List<string>();
                                                    for (int j = 0; j < order.OBSERVATIONRepetitionsUsed; j++)
                                                    {
                                                        string des = "";
                                                        NHapi.Model.V231.Group.ORU_R01_OBSERVATION Observationi = order.GetOBSERVATION(j);
                                                        if (Observationi != null)
                                                        {
                                                            NHapi.Model.V231.Segment.OBX obxx = Observationi.OBX;
                                                            Varies[] ObservationValue = obxx.GetObservationValue();
                                                            if (ObservationValue.Length > 0)
                                                            {
                                                                foreach (var item in ObservationValue)
                                                                {
                                                                    if (item.Data != null)
                                                                    {
                                                                        des += item.Data.ToString() + "^";
                                                                    }
                                                                }
                                                                descrip.Add(des.Trim('^'));
                                                            }
                                                        }
                                                    }

                                                    descriptionDetail = string.Join("\r\n", descrip);
                                                }

                                                if (!String.IsNullOrEmpty(thoiGianKetThucChup))
                                                {
                                                    try
                                                    {
                                                        if (thoiGianKetThucChup.Length == 8)
                                                        {
                                                            dti_thoiGianKetThucChup = DateTime.ParseExact(thoiGianKetThucChup, "yyyyMMdd", CultureInfo.InvariantCulture);
                                                        }
                                                        else if (thoiGianKetThucChup.Length == 12)
                                                        {
                                                            dti_thoiGianKetThucChup = DateTime.ParseExact(thoiGianKetThucChup, "yyyyMMddHHmm", CultureInfo.InvariantCulture);
                                                        }
                                                        else if (thoiGianKetThucChup.Length == 14)
                                                        {
                                                            dti_thoiGianKetThucChup = DateTime.ParseExact(thoiGianKetThucChup, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
                                                        }
                                                        else
                                                        {
                                                            dti_thoiGianKetThucChup = DateTime.Parse(thoiGianKetThucChup);
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        Inventec.Common.Logging.LogSystem.Error("Loi dinh dang ngay thang trong HL7: obx.ObservationEndDateTime \r\n >>>>>>>>>>>>>>>>>>>>>>>>>>>> lay thoi gian hien tai");
                                                        Inventec.Common.Logging.LogSystem.Error(ex);
                                                        dti_thoiGianKetThucChup = DateTime.Now;
                                                    }
                                                }
                                                else
                                                {
                                                    Inventec.Common.Logging.LogSystem.Error("obx.ObservationEndDateTime is null");
                                                    dti_thoiGianKetThucChup = DateTime.Now;
                                                }

                                                if (!String.IsNullOrEmpty(thoiGianBatDauChup))
                                                {
                                                    try
                                                    {
                                                        if (thoiGianBatDauChup.Length == 8)
                                                        {
                                                            dti_thoiGianBatDauChup = DateTime.ParseExact(thoiGianBatDauChup, "yyyyMMdd", CultureInfo.InvariantCulture);
                                                        }
                                                        else if (thoiGianBatDauChup.Length == 12)
                                                        {
                                                            dti_thoiGianBatDauChup = DateTime.ParseExact(thoiGianBatDauChup, "yyyyMMddHHmm", CultureInfo.InvariantCulture);
                                                        }
                                                        else if (thoiGianBatDauChup.Length == 14)
                                                        {
                                                            dti_thoiGianBatDauChup = DateTime.ParseExact(thoiGianBatDauChup, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
                                                        }
                                                        else
                                                        {
                                                            dti_thoiGianBatDauChup = DateTime.Parse(thoiGianBatDauChup);
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        Inventec.Common.Logging.LogSystem.Error("Loi dinh dang ngay thang trong HL7: obx.ObservationDateTime \r\n >>>>>>>>>>>>>>>>>>>>>>>>>>>> lay thoi gian hien tai");
                                                        Inventec.Common.Logging.LogSystem.Error(ex);
                                                        dti_thoiGianBatDauChup = DateTime.Now;
                                                    }
                                                }
                                                else
                                                {
                                                    Inventec.Common.Logging.LogSystem.Error("obx.ObservationDateTime is null");
                                                    dti_thoiGianBatDauChup = dti_thoiGianKetThucChup;
                                                }

                                                if (dti_thoiGianKetThucChup == null)
                                                {
                                                    dti_thoiGianBatDauChup = DateTime.Now;
                                                    dti_thoiGianKetThucChup = DateTime.Now;
                                                }

                                                if (!String.IsNullOrEmpty(thoiGianTraKetQua))
                                                {
                                                    try
                                                    {
                                                        if (thoiGianTraKetQua.Length == 8)
                                                        {
                                                            dti_NgayTraKetQua = DateTime.ParseExact(thoiGianTraKetQua, "yyyyMMdd", CultureInfo.InvariantCulture);
                                                        }
                                                        else if (thoiGianTraKetQua.Length == 12)
                                                        {
                                                            dti_NgayTraKetQua = DateTime.ParseExact(thoiGianTraKetQua, "yyyyMMddHHmm", CultureInfo.InvariantCulture);
                                                        }
                                                        else if (thoiGianTraKetQua.Length == 14)
                                                        {
                                                            dti_NgayTraKetQua = DateTime.ParseExact(thoiGianTraKetQua, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
                                                        }
                                                        else
                                                        {
                                                            dti_NgayTraKetQua = DateTime.Parse(thoiGianTraKetQua);
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        Inventec.Common.Logging.LogSystem.Error("Loi dinh dang ngay thang trong HL7: obx.DateTimeOfTheObservation");
                                                        Inventec.Common.Logging.LogSystem.Error(ex);
                                                        dti_NgayTraKetQua = dti_thoiGianKetThucChup;
                                                    }
                                                }
                                                else
                                                {
                                                    Inventec.Common.Logging.LogSystem.Error("obx.DateTimeOfTheObservation is null");
                                                    dti_NgayTraKetQua = dti_thoiGianKetThucChup;
                                                }

                                                if (dti_NgayTraKetQua == null)
                                                {
                                                    dti_NgayTraKetQua = DateTime.Now;
                                                }

                                                List<string> maBacSiTraKQ = new List<string>();
                                                for (int t = 0; t < obr.TechnicianRepetitionsUsed; t++)
                                                {
                                                    NHapi.Model.V231.Datatype.NDL technician = obr.GetTechnician(t);
                                                    if (technician != null && !String.IsNullOrWhiteSpace(technician.Name.ToString()))
                                                    {
                                                        maBacSiTraKQ.Add(technician.Name.ToString());
                                                    }
                                                }

                                                if (maBacSiTraKQ.Count > 0)
                                                {
                                                    MaKTVTraKQ = string.Join(";", maBacSiTraKQ.Distinct());
                                                }

                                                string moTa = "", ghichu = "", ketLuan = "";

                                                ProcessDescriptionDetail(descriptionDetail, ref moTa, ref ghichu, ref ketLuan);

                                                result = new PacsReceivedData();
                                                result.BeginTime = Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(dti_thoiGianBatDauChup);
                                                result.Conclude = ketLuan;
                                                result.Description = !String.IsNullOrWhiteSpace(moTa) ? moTa : descriptionDetail;
                                                result.EndTime = Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(dti_thoiGianKetThucChup);
                                                result.FinishTime = Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(dti_NgayTraKetQua);
                                                result.Machine = ProcessSubstringObx(data);
                                                result.Note = ghichu;
                                                result.SereServId = (long)idChiDinh;
                                                result.studyID = studyID;
                                                result.SubclinicalResultUsername = KTVTraKQ;
                                                result.SubclinicalResultLoginname = MaKTVTraKQ;
                                            }
                                        }
                                        else
                                        {
                                            throw new Exception("HL7 thiếu phần ORU_R01_PATIENT_RESULT.ORU_R01_ORDER_OBSERVATION.ORU_R01_OBSERVATION");
                                        }
                                    }
                                    else
                                    {
                                        if (orc == null)
                                        {
                                            throw new Exception("HL7 thiếu phần ORU_R01_PATIENT_RESULT.ORU_R01_ORDER_OBSERVATION.ORC - Phần thông tin chỉ định");
                                        }
                                        else
                                        {
                                            throw new Exception("HL7 thiếu phần ORU_R01_PATIENT_RESULT.ORU_R01_ORDER_OBSERVATION.OBR - Phần thông tin kết quả");
                                        }
                                    }
                                }
                                else
                                {
                                    throw new Exception("HL7 thiếu phần ORU_R01_PATIENT_RESULT.ORU_R01_ORDER_OBSERVATION");
                                }
                            }
                        }
                        else
                        {
                            throw new Exception("HL7 thiếu phần ORU_R01_PATIENT_RESULT");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result = null;
                Inventec.Common.Logging.LogSystem.Error(ex);
            }

            return result;
        }

        private static void ProcessDescriptionDetail(string data, ref string moTa, ref string ghiChu, ref string ketLuan)
        {
            try
            {
                moTa = data;
                if (!String.IsNullOrWhiteSpace(data))
                {
                    string moTa_BatDau = "", moTa_KetThuc = "", ghiChu_BatDau = "", ghiChu_KetThuc = "", ketLuan_BatDau = "", ketLuan_KetThuc = "";

                    if (PacsCFG.PROCESS_DESCRIPTION != null)
                    {
                        if (PacsCFG.PROCESS_DESCRIPTION.MO_TA != null)
                        {
                            moTa_BatDau = PacsCFG.PROCESS_DESCRIPTION.MO_TA.BAT_DAU ?? "";
                            moTa_KetThuc = PacsCFG.PROCESS_DESCRIPTION.MO_TA.KET_THUC ?? "";
                        }

                        if (PacsCFG.PROCESS_DESCRIPTION.GHI_CHU != null)
                        {
                            ghiChu_BatDau = PacsCFG.PROCESS_DESCRIPTION.GHI_CHU.BAT_DAU ?? "";
                            ghiChu_KetThuc = PacsCFG.PROCESS_DESCRIPTION.GHI_CHU.KET_THUC ?? "";
                        }

                        if (PacsCFG.PROCESS_DESCRIPTION.KET_LUAN != null)
                        {
                            ketLuan_BatDau = PacsCFG.PROCESS_DESCRIPTION.KET_LUAN.BAT_DAU ?? "";
                            ketLuan_KetThuc = PacsCFG.PROCESS_DESCRIPTION.KET_LUAN.KET_THUC ?? "";
                        }
                    }
                    else
                    {
                        moTa_BatDau = "mô tả:";
                        ghiChu_BatDau = "ghi chú:";
                        moTa_KetThuc = ghiChu_KetThuc = ketLuan_BatDau = "kết luận:";
                    }

                    string temp = data;
                    int vitriketthuc = 0;
                    if (!String.IsNullOrWhiteSpace(moTa_BatDau) && temp.ToLower().Contains(moTa_BatDau.ToLower()))
                    {
                        moTa = temp.Substring(temp.ToLower().IndexOf(moTa_BatDau.ToLower())).Replace(moTa_BatDau.ToUpper(), "");
                        vitriketthuc = moTa.IndexOf(moTa_KetThuc);
                        if (vitriketthuc <= 0)
                        {
                            vitriketthuc = moTa.Length;
                        }

                        moTa = moTa.Substring(0, vitriketthuc).Trim();
                    }

                    if (!String.IsNullOrWhiteSpace(moTa))
                    {
                        temp = temp.Substring(vitriketthuc).Trim();
                    }

                    if (!String.IsNullOrWhiteSpace(ghiChu_BatDau) && temp.ToLower().Contains(ghiChu_BatDau.ToLower()))
                    {
                        ghiChu = temp.Substring(temp.ToLower().IndexOf(ghiChu_BatDau.ToLower())).Replace(ghiChu_BatDau.ToUpper(), "");
                        vitriketthuc = ghiChu.IndexOf(ketLuan_BatDau);
                        if (!string.IsNullOrWhiteSpace(ghiChu_KetThuc))
                            vitriketthuc = ghiChu.IndexOf(ghiChu_KetThuc);
                        if (vitriketthuc <= 0)
                        {
                            vitriketthuc = ghiChu.Length;
                        }

                        ghiChu = ghiChu.Substring(0, vitriketthuc).Trim();
                    }

                    if (!String.IsNullOrWhiteSpace(ghiChu))
                    {
                        temp = temp.Replace(ghiChu, "");
                    }

                    if (!String.IsNullOrWhiteSpace(ketLuan_BatDau) && temp.ToLower().Contains(ketLuan_BatDau.ToLower()))
                    {
                        ketLuan = temp.Substring(temp.ToLower().IndexOf(ketLuan_BatDau.ToLower())).Replace(ketLuan_BatDau.ToUpper(), "");
                        vitriketthuc = ketLuan.IndexOf(ketLuan_KetThuc);
                        if (vitriketthuc <= 0)
                        {
                            vitriketthuc = ketLuan.Length;
                        }

                        ketLuan = ketLuan.Substring(0, vitriketthuc).Trim();
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        /// <summary>
        /// hl7 2.3.1 không có OBX 18 nên xử lý cắt chuỗi để lấy dữ liệu
        /// </summary>
        /// <param name="fileData"></param>
        /// <returns></returns>
        private static string ProcessSubstringObx(string fileData)
        {
            string result = "";
            try
            {
                if (!String.IsNullOrWhiteSpace(fileData))
                {
                    int obxIndex = fileData.IndexOf("OBX");
                    string data = fileData.Substring(obxIndex);
                    //chỉ lấy 1 bản tin OBX
                    int nextIndex = data.IndexOf("OBX", 3);
                    if (nextIndex > 0)
                    {
                        data = data.Substring(0, nextIndex);
                    }

                    string[] splitData = data.Split('|');
                    if (splitData.Length > 18 && !String.IsNullOrWhiteSpace(splitData[18]))
                    {
                        string[] may = splitData[18].Trim().Split('^');
                        result = may.First();
                    }
                }
            }
            catch (Exception ex)
            {
                result = "";
                Inventec.Common.Logging.LogSystem.Error(ex);
            }

            return result;
        }

        private static PacsReceivedData ProcessReceivedHl7_27(string hl7Message)
        {
            PacsReceivedData result = null;
            HL7ResultData data = SendSANCY.ProcessReceivedHl7(hl7Message);
            if (data != null)
            {
                result = new PacsReceivedData();
                result.BeginTime = data.BeginTime;
                result.Conclude = data.Conclude;
                result.Description = data.Description;
                result.EndTime = data.EndTime;
                result.FinishTime = data.FinishTime;
                result.Machine = data.Machine;
                result.Note = data.Note;
                result.NumberOfFilm = data.NumberOfFilm;
                result.SereServId = data.SereServId;
                result.studyID = data.studyID;
                result.SubclinicalResultLoginname = data.ExecuteLoginname + ";" + data.SubclinicalResultLoginname;
                result.SubclinicalResultUsername = data.ExecuteUsername + ";" + data.SubclinicalResultUsername;
            }
            return result;
        }
    }
}
