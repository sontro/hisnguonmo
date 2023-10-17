using Inventec.Common.Logging;
using Inventec.Core;
using MedilinkHL7.SDK;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServExt;
using MOS.MANAGER.HisCard;
using MOS.MANAGER.HisPatient;
using MOS.MANAGER.HisServiceReq.Update.Finish;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.HisTreatmentBedRoom;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOS.MANAGER.HisServiceReq.Pacs.PacsThread;
using MOS.MANAGER.Config.CFG;
using AutoMapper;

namespace MOS.MANAGER.HisServiceReq.Pacs.PacsProcessor.PacsSancy
{
    class PacsSancyProcessor : BusinessBase, IPacsProcessor
    {
        internal PacsSancyProcessor()
            : base()
        {
        }

        internal PacsSancyProcessor(CommonParam param)
            : base(param)
        {

        }

        bool IPacsProcessor.SendOrder(PacsOrderData data, ref List<string> sqls)
        {
            bool result = false;
            try
            {
                bool? valid = null;
                V_HIS_ROOM room = HisRoomCFG.DATA.FirstOrDefault(o => o.ID == data.ServiceReq.REQUEST_ROOM_ID);
                V_HIS_ROOM exeRoom = HisRoomCFG.DATA.FirstOrDefault(o => o.ID == data.ServiceReq.EXECUTE_ROOM_ID);
                List<V_HIS_TREATMENT_BED_ROOM> treatBedRooms = new HisTreatmentBedRoomGet().GetViewCurrentInByTreatmentId(data.Treatment.ID);
                V_HIS_TREATMENT_BED_ROOM bedRoom = treatBedRooms != null ? treatBedRooms.FirstOrDefault() : null;
                PacsAddress add = PacsCFG.PACS_ADDRESS.FirstOrDefault(o => o.RoomCode == exeRoom.ROOM_CODE && !String.IsNullOrWhiteSpace(o.Address) && o.Port > 0);

                PacsFileAddress fileAdd = PacsCFG.PACS_FILE_ADDRESSES.FirstOrDefault(o => o.RoomCode == exeRoom.ROOM_CODE && !String.IsNullOrWhiteSpace(o.SaveFolder));

                if ((PacsCFG.CONNECTION_TYPE != PacsConnectionType.FILE && PacsCFG.CONNECTION_TYPE != PacsConnectionType.API && !IsNotNull(add)) || (PacsCFG.CONNECTION_TYPE == PacsConnectionType.FILE && !IsNotNull(fileAdd)))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_PhongXuLyChuaDuocCauHinhDiaChiPacs, exeRoom.ROOM_NAME);
                    LogSystem.Warn(String.Format("Phong xu ly ma {0} khong co cau hinh dia chi server Pacs", exeRoom.ROOM_CODE));
                    return false;
                }

                List<long> successIds = new List<long>();
                if (IsNotNullOrEmpty(data.Inserts))
                {
                    if (PacsCFG.CONNECTION_TYPE == PacsConnectionType.API && PacsCFG.PACS_API_INFO != null && String.IsNullOrWhiteSpace(PacsCFG.PACS_API_INFO.Order))
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_ChuaDuocCauHinhTichHopPacs);
                        LogSystem.Warn("Khong co cau hinh Api Order Pacs");
                        return false;
                    }

                    foreach (HIS_SERE_SERV sereServ in data.Inserts)
                    {
                        if (this.SendNewOrder(data.Treatment, data.ServiceReq, sereServ, room, exeRoom, bedRoom, data.KskContract, add, fileAdd))
                        {
                            if (!valid.HasValue) valid = true;
                            sereServ.IS_SENT_EXT = Constant.IS_TRUE;
                            successIds.Add(sereServ.ID);
                        }
                        else
                        {
                            valid = false;
                        }
                    }
                }

                if (IsNotNullOrEmpty(data.Deletes))
                {
                    if (PacsCFG.CONNECTION_TYPE == PacsConnectionType.API && PacsCFG.PACS_API_INFO != null && String.IsNullOrWhiteSpace(PacsCFG.PACS_API_INFO.Delete))
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_ChuaDuocCauHinhTichHopPacs);
                        LogSystem.Warn("Khong co cau hinh Api Delete Pacs");
                        return false;
                    }

                    foreach (HIS_SERE_SERV sereServ in data.Deletes)
                    {
                        if (this.SendDeleteOrder(data.Treatment, data.ServiceReq, sereServ, room, exeRoom, bedRoom, data.KskContract, add, fileAdd))
                        {
                            if (!valid.HasValue) valid = true;
                            sereServ.IS_SENT_EXT = Constant.IS_TRUE;
                            successIds.Add(sereServ.ID);
                        }
                        else
                        {
                            valid = false;
                        }
                    }
                }
                if (IsNotNullOrEmpty(successIds))
                {
                    string sql = "UPDATE HIS_SERE_SERV SET IS_SENT_EXT = 1 WHERE IS_SENT_EXT IS NULL AND %IN_CLAUSE%";
                    sql = DAOWorker.SqlDAO.AddInClause(successIds, sql, "ID");
                    sqls.Add(sql);
                }
                data.IsSuccess = valid;
                return true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        private bool SendNewOrder(HIS_TREATMENT treatment, HIS_SERVICE_REQ serviceReq, HIS_SERE_SERV sereServ, V_HIS_ROOM room, V_HIS_ROOM exeRoom, V_HIS_TREATMENT_BED_ROOM bedRoom, V_HIS_KSK_CONTRACT kskContract, PacsAddress add, PacsFileAddress fileAdd)
        {
            try
            {
                HL7PACS pacsData = this.MakeHL7PACS(treatment, serviceReq, room, exeRoom, bedRoom, sereServ, add, kskContract);

                SendSANCY.VersionV2 version = PacsCFG.LIBRARY_HL7_VERSION;

                if (PacsCFG.CONNECTION_TYPE == PacsConnectionType.FILE)
                {
                    string fileName = string.Format("ORM_{0}_{1}_{2}.HL7", serviceReq.SERVICE_REQ_CODE, sereServ.TDL_SERVICE_CODE, DateTime.Now.ToString("yyyyMMddHHmmssfff"));

                    string data = SendSANCY.CreateRequest(pacsData, null, OrderControl.NW, version);

                    if (FileHandler.Write(fileAdd.Ip, fileAdd.User, fileAdd.Password, data, fileName, fileAdd.SaveFolder))
                    {
                        return true;
                    }
                    else
                    {
                        LogSystem.Error("Luu file chi dinh sang he thong PACS that bai. IP: " + fileAdd.Ip + "; SaveFolder: " + fileAdd.SaveFolder + "; pacsData: " + data);
                        return false;
                    }
                }
                else if (PacsCFG.CONNECTION_TYPE == PacsConnectionType.API)
                {
                    Output output = new SendSANCY().SendOrderByApi(pacsData, add.Address, add.Port, PacsCFG.PACS_API_INFO.Order, version);

                    if (output != null && output.Success)
                    {
                        return true;
                    }
                    else
                    {
                        LogSystem.Warn(String.Format("Gui Cancel dich vu cdha sang server Pacs Sancy that bai. Ma: {0}-{1}", serviceReq.SERVICE_REQ_CODE, sereServ.TDL_SERVICE_CODE)
                            + LogUtil.TraceData("\n Output", output));
                        return false;
                    }
                }
                else
                {
                    Output output = new SendSANCY().SendOrderToSancy(pacsData, add.Address, add.Port, version);
                    if (output != null && output.Success)
                    {
                        return true;
                    }
                    else
                    {
                        LogSystem.Warn(String.Format("Gui dich vu cdha sang server Pacs Sancy that bai. Ma: {0}-{1}", serviceReq.SERVICE_REQ_CODE, sereServ.TDL_SERVICE_CODE)
                            + LogUtil.TraceData("\n Output", output));
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return false;
            }
        }

        private bool SendDeleteOrder(HIS_TREATMENT treatment, HIS_SERVICE_REQ serviceReq, HIS_SERE_SERV sereServ, V_HIS_ROOM room, V_HIS_ROOM exeRoom, V_HIS_TREATMENT_BED_ROOM bedRoom, V_HIS_KSK_CONTRACT kskContract, PacsAddress add, PacsFileAddress fileAdd)
        {
            try
            {
                HL7PACS pacsData = this.MakeHL7PACS(treatment, serviceReq, room, exeRoom, bedRoom, sereServ, add, kskContract);

                SendSANCY.VersionV2 version = PacsCFG.LIBRARY_HL7_VERSION;

                if (PacsCFG.CONNECTION_TYPE == PacsConnectionType.FILE)
                {
                    string fileName = string.Format("ORM_{0}_{1}_{2}.HL7", serviceReq.SERVICE_REQ_CODE, sereServ.TDL_SERVICE_CODE, DateTime.Now.ToString("yyyyMMddHHmmssfff"));

                    string data = SendSANCY.CreateRequest(pacsData, null, OrderControl.CA, version);

                    if (FileHandler.Write(fileAdd.Ip, fileAdd.User, fileAdd.Password, data, fileName, fileAdd.SaveFolder))
                    {
                        return true;
                    }
                    else
                    {
                        LogSystem.Error("Luu file chi dinh sang he thong PACS that bai. IP: " + fileAdd.Ip + "; SaveFolder: " + fileAdd.SaveFolder + "; pacsData: " + data);
                        return false;
                    }
                }

                if (PacsCFG.CONNECTION_TYPE == PacsConnectionType.API)
                {
                    string data = SendSANCY.CreateRequest(pacsData, null, OrderControl.CA, version);
                    data = HL7Sender.CreateMLLPMessage(data);
                    Output output = new SendSANCY().CallApiMessage(data, add.Address, add.Port, PacsCFG.PACS_API_INFO.Delete, version);

                    if (output != null && output.Success)
                    {
                        return true;
                    }
                    else
                    {
                        LogSystem.Warn(String.Format("Gui Cancel dich vu cdha sang server Pacs Sancy that bai. Ma: {0}-{1}", serviceReq.SERVICE_REQ_CODE, sereServ.TDL_SERVICE_CODE)
                            + LogUtil.TraceData("\n Output", output));
                        return false;
                    }
                }
                else
                {
                    Output output = SendSANCY.SendCancelOrderToSancy(pacsData, add.Address, add.Port);

                    if (output != null && output.Success)
                    {
                        return true;
                    }
                    else
                    {
                        LogSystem.Warn(String.Format("Gui Cancel dich vu cdha sang server Pacs Sancy that bai. Ma: {0}-{1}", serviceReq.SERVICE_REQ_CODE, sereServ.TDL_SERVICE_CODE)
                            + LogUtil.TraceData("\n Output", output));
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return false;
            }
        }

        public MedilinkHL7.SDK.HL7PACS MakeHL7PACS(HIS_TREATMENT treatment, HIS_SERVICE_REQ serviceReq, V_HIS_ROOM room, V_HIS_ROOM exeRoom, V_HIS_TREATMENT_BED_ROOM bedRoom, HIS_SERE_SERV data, PacsAddress add, V_HIS_KSK_CONTRACT kskContract)
        {
            HL7PACS hl7PACS = new HL7PACS();
            hl7PACS.tenPhanMemHIS = Constant.HIS_CODE__PACS;
            hl7PACS.tenPhanMemPACS = Constant.APP_CODE__SANCY;
            if (PacsCFG.PACS_API_INFO != null && !String.IsNullOrWhiteSpace(PacsCFG.PACS_API_INFO.AppCode))
            {
                hl7PACS.tenPhanMemPACS = PacsCFG.PACS_API_INFO.AppCode;
            }
            hl7PACS.idChiDinh = data.ID;

            hl7PACS.hoTenBenhNhan = EliminateSpecialCharacter(treatment.TDL_PATIENT_NAME) + GetAgeForChild(Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(treatment.TDL_PATIENT_DOB).Value, DateTime.Now);
            hl7PACS.gioiTinh = treatment.TDL_PATIENT_GENDER_ID == IMSys.DbConfig.HIS_RS.HIS_GENDER.ID__FEMALE ? MedilinkHL7.Define.AdministrativeSex.F : MedilinkHL7.Define.AdministrativeSex.M;
            hl7PACS.idBenhNhan = PacsCFG.PATIENT_CODE_PREFIX + treatment.TDL_PATIENT_CODE;
            hl7PACS.idDotVaoVien = Convert.ToDecimal(treatment.TREATMENT_CODE);
            hl7PACS.diaChiBenhNhan = EliminateSpecialCharacter(treatment.TDL_PATIENT_ADDRESS);
            hl7PACS.ngaySinh = treatment.TDL_PATIENT_DOB.ToString().Substring(0, 8);
            hl7PACS.soVaoVien_STTKham = !String.IsNullOrWhiteSpace(treatment.IN_CODE) ? treatment.IN_CODE : (serviceReq.NUM_ORDER.HasValue ? serviceReq.NUM_ORDER.ToString() : "1");
            hl7PACS.ngayVaoVien = treatment.CLINICAL_IN_TIME.HasValue ? treatment.CLINICAL_IN_TIME.ToString().Substring(0, 8) : treatment.IN_TIME.ToString().Substring(0, 8);
            hl7PACS.maChanDoan = serviceReq.ICD_CODE;
            hl7PACS.chanDoan = EliminateSpecialCharacter(serviceReq.ICD_NAME);
            hl7PACS.noiLamViec = EliminateSpecialCharacter(treatment.TDL_PATIENT_WORK_PLACE);

            hl7PACS.doiTuongBenhNhan = HisPatientTypeCFG.DATA.FirstOrDefault(o => o.ID == treatment.TDL_PATIENT_TYPE_ID).PATIENT_TYPE_CODE;

            LoaiBenhNhan loaiBN = treatment.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU ? LoaiBenhNhan.I : LoaiBenhNhan.O;
            hl7PACS.loaiBenhNhan = loaiBN;

            hl7PACS.maVienPhiChiDinh = data.TDL_SERVICE_CODE;
            hl7PACS.tenVienPhiChiDinh = data.TDL_SERVICE_NAME;
            hl7PACS.sttLayMau_ChiDinh = serviceReq.NUM_ORDER.HasValue ? serviceReq.NUM_ORDER.ToString() : "1";
            if (PacsCFG.IS_ALLOW_REQUEST_ROOM)
            {
                hl7PACS.tenKhoaPhong = EliminateSpecialCharacter(room.ROOM_NAME) + " - " + EliminateSpecialCharacter(room.DEPARTMENT_NAME);
            }
            else
            {
                hl7PACS.tenKhoaPhong = EliminateSpecialCharacter(room.DEPARTMENT_NAME);
            }
            hl7PACS.phong = bedRoom != null ? bedRoom.BED_ROOM_NAME : "";
            hl7PACS.giuong = bedRoom != null ? bedRoom.BED_NAME : "";
            hl7PACS.modality = data.TDL_PACS_TYPE_CODE;
            hl7PACS.doUuTien = DoUuTien.R;
            hl7PACS.phuongThucDiChuyen = PhuongThucDiChuyen.WALK;
            hl7PACS.thoiGianChiDinh = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(serviceReq.INTRUCTION_TIME);

            HIS_EMPLOYEE employee = HisEmployeeCFG.DATA.Where(o => o.LOGINNAME == serviceReq.REQUEST_LOGINNAME).FirstOrDefault();
            HIS_DEPARTMENT requestDepartment = HisDepartmentCFG.DATA.Where(o => o.ID == serviceReq.REQUEST_DEPARTMENT_ID).FirstOrDefault();

            if (PacsCFG.REQUEST_USER_INFO_OPTION == PacsRequestUserInfoOption.ACCOUNT_INFO)
            {
                //Set lai gia tri cua truong de truyen vao dung vi tri trong ban tin HL7
                //PV1.8: user name bác sĩ chỉ định
                hl7PACS.maBacSiChiDinh = EliminateSpecialCharacter(serviceReq.REQUEST_LOGINNAME);
                hl7PACS.hoTenBacSiChiDinh = EliminateSpecialCharacter(serviceReq.REQUEST_USERNAME);

                if (PacsCFG.LIBRARY_HL7_VERSION == MedilinkHL7.SDK.SendSANCY.VersionV2.V231)
                {
                    //Set lai gia tri cua truong de truyen vao dung vi tri trong ban tin HL7
                    //ORC12.1: user name bác sĩ chỉ định
                    //ORC12.3: Login của user chỉ định
                    hl7PACS.maBacSiChiDinh = EliminateSpecialCharacter(serviceReq.REQUEST_USERNAME);
                    hl7PACS.hoTenBacSiChiDinh = EliminateSpecialCharacter(serviceReq.REQUEST_LOGINNAME);
                }
            }
            else
            {
                hl7PACS.maBacSiChiDinh = employee != null ? employee.DIPLOMA : null;
            }

            if (PacsCFG.IS_SET_BRANCH_CODE)
            {
                //Truyen sang la CS1 hoac CS2 (thong nhat voi ben tich hop PACS vien Tim)
                hl7PACS.tenCoSo = room.BRANCH_CODE;
                hl7PACS.tenCoSoTiepNhan = exeRoom.BRANCH_CODE;
            }
            else if (PacsCFG.IS_SET_BRANCH_CODE_BY_MEDI_ORG_CODE)
            {
                //PACS cloud gui sang thong tin ma KCBBĐ cua chi nhanh chi dinh
                hl7PACS.tenCoSo = room.HEIN_MEDI_ORG_CODE;
                hl7PACS.tenCoSoTiepNhan = string.Format("CS{0}", exeRoom.HEIN_MEDI_ORG_CODE);
            }

            if (!string.IsNullOrEmpty(serviceReq.TDL_PATIENT_CCCD_NUMBER))
            {
                hl7PACS.CccdNumber = serviceReq.TDL_PATIENT_CCCD_NUMBER;
            }
            else if (!string.IsNullOrEmpty(serviceReq.TDL_PATIENT_CMND_NUMBER))
            {
                hl7PACS.CccdNumber = serviceReq.TDL_PATIENT_CMND_NUMBER;
            }

            if (!string.IsNullOrEmpty(serviceReq.TDL_PATIENT_PHONE))
            {
                hl7PACS.PhoneNumber = serviceReq.TDL_PATIENT_PHONE;
            }
            else if (!string.IsNullOrEmpty(serviceReq.TDL_PATIENT_MOBILE))
            {
                hl7PACS.PhoneNumber = serviceReq.TDL_PATIENT_MOBILE;
            }
            hl7PACS.ServiceReqCode = serviceReq.SERVICE_REQ_CODE;

            HisCardFilterQuery cardFilter = new HisCardFilterQuery();
            cardFilter.PATIENT_ID = serviceReq.TDL_PATIENT_ID;
            cardFilter.IS_ACTIVE = Constant.IS_TRUE;
            cardFilter.HAS_CARD_CODE = true;
            var cards = new HisCardGet().Get(cardFilter);
            if (IsNotNullOrEmpty(cards))
            {
                hl7PACS.SmartCardNumber = cards.OrderByDescending(o => o.ID).FirstOrDefault().CARD_CODE;
            }
            else
            {
                var patient = new HisPatientGet().GetById(serviceReq.TDL_PATIENT_ID);
                hl7PACS.SmartCardNumber = IsNotNull(patient) ? patient.REGISTER_CODE : null;
            }

            hl7PACS.maKhoaPhong = requestDepartment != null ? requestDepartment.DEPARTMENT_CODE : null;
            V_HIS_SERVICE_REQ vHisServieRep = new HisServiceReqGet().GetViewFromTable(serviceReq);
            if (vHisServieRep != null)
            {
                // Kiem tra config de them du lieu ma phong thuc hien va ten phong thuc hien
                if (PacsCFG.PACS_EXCUTE_ROOM_OPTION == 1)
                {
                    hl7PACS.maPhongThucHien = vHisServieRep.EXECUTE_ROOM_CODE;
                    hl7PACS.tenPhongThucHien = vHisServieRep.EXECUTE_ROOM_NAME;
                }
            }

            if (PacsCFG.LIBRARY_HL7_VERSION == MedilinkHL7.SDK.SendSANCY.VersionV2.V231 || !String.IsNullOrWhiteSpace(add.CloudInfo))
            {
                hl7PACS.soTheBHYT = treatment.TDL_HEIN_CARD_NUMBER ?? treatment.TDL_PATIENT_CODE;
            }
            else if (!String.IsNullOrWhiteSpace(treatment.TDL_HEIN_CARD_NUMBER))
            {
                hl7PACS.soTheBHYT = treatment.TDL_HEIN_CARD_NUMBER;
            }

            if (IsNotNull(kskContract))
            {
                hl7PACS.KskContractCode = kskContract.KSK_CONTRACT_CODE;
                hl7PACS.KskCompanyName = kskContract.WORK_PLACE_NAME;
            }

            return hl7PACS;
        }

        public MedilinkHL7.SDK.HL7ResultData MakeHL7ResultData(HIS_TREATMENT treatment, HIS_SERVICE_REQ serviceReq, V_HIS_ROOM room, V_HIS_ROOM exeRoom, V_HIS_TREATMENT_BED_ROOM bedRoom, HIS_SERE_SERV data, PacsAddress add, V_HIS_KSK_CONTRACT kskContract, HIS_SERE_SERV_EXT sereServExt)
        {
            HL7ResultData result = new HL7ResultData();
            HL7PACS pacsData = new PacsSancyProcessor(param).MakeHL7PACS(treatment, serviceReq, room, exeRoom, bedRoom, data, add, kskContract);

            Mapper.CreateMap<HL7PACS, HL7ResultData>();
            result = Mapper.Map<HL7ResultData>(pacsData);
            result.Description = sereServExt.DESCRIPTION;
            result.Conclude = sereServExt.CONCLUDE;
            result.BeginTime = sereServExt.BEGIN_TIME;
            result.EndTime = sereServExt.END_TIME;
            result.SubclinicalResultLoginname = sereServExt.SUBCLINICAL_RESULT_LOGINNAME;
            result.SubclinicalResultUsername = sereServExt.SUBCLINICAL_RESULT_USERNAME;
            result.SereServId = sereServExt.SERE_SERV_ID;
            result.NumberOfFilm = sereServExt.NUMBER_OF_FILM;
            result.Note = sereServExt.NOTE;
            result.ExecuteUsername = serviceReq.EXECUTE_USERNAME;
            result.ExecuteLoginname = serviceReq.EXECUTE_LOGINNAME;
            result.FinishTime = serviceReq.FINISH_TIME;
            return result;
        }

        public MedilinkHL7.SDK.HL7PACS MakeHL7PACSForADTA08(HIS_PATIENT patient)
        {
            if (patient == null) return null;

            HL7PACS hl7PACS = new HL7PACS();
            hl7PACS.tenPhanMemHIS = Constant.HIS_CODE__PACS;
            hl7PACS.tenPhanMemPACS = Constant.APP_CODE__SANCY;
            hl7PACS.hoTenBenhNhan = EliminateSpecialCharacter(patient.VIR_PATIENT_NAME) + GetAgeForChild(Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(patient.DOB).Value, DateTime.Now);
            hl7PACS.gioiTinh = patient.GENDER_ID == IMSys.DbConfig.HIS_RS.HIS_GENDER.ID__FEMALE ? MedilinkHL7.Define.AdministrativeSex.F : MedilinkHL7.Define.AdministrativeSex.M;
            hl7PACS.idBenhNhan = PacsCFG.PATIENT_CODE_PREFIX + patient.PATIENT_CODE;
            hl7PACS.diaChiBenhNhan = EliminateSpecialCharacter(patient.VIR_ADDRESS);
            hl7PACS.ngaySinh = patient.DOB.ToString().Substring(0, 8);
            hl7PACS.noiLamViec = EliminateSpecialCharacter(patient.WORK_PLACE);
            hl7PACS.doUuTien = DoUuTien.R;
            hl7PACS.phuongThucDiChuyen = PhuongThucDiChuyen.WALK;

            if (!string.IsNullOrEmpty(patient.CCCD_NUMBER))
            {
                hl7PACS.CccdNumber = patient.CCCD_NUMBER;
            }
            else if (!string.IsNullOrEmpty(patient.CMND_NUMBER))
            {
                hl7PACS.CccdNumber = patient.CMND_NUMBER;
            }

            if (!string.IsNullOrEmpty(patient.PHONE))
            {
                hl7PACS.PhoneNumber = patient.PHONE;
            }
            else if (!string.IsNullOrEmpty(patient.MOBILE))
            {
                hl7PACS.PhoneNumber = patient.MOBILE;
            }

            HisCardFilterQuery cardFilter = new HisCardFilterQuery();
            cardFilter.PATIENT_ID = patient.ID;
            cardFilter.IS_ACTIVE = Constant.IS_TRUE;
            cardFilter.HAS_CARD_CODE = true;
            var cards = new HisCardGet().Get(cardFilter);
            if (IsNotNullOrEmpty(cards))
            {
                hl7PACS.SmartCardNumber = cards.OrderByDescending(o => o.ID).FirstOrDefault().CARD_CODE;
            }
            else
            {
                hl7PACS.SmartCardNumber = IsNotNull(patient) ? patient.REGISTER_CODE : null;
            }

            List<HIS_TREATMENT> allTreatments = new HisTreatmentGet().GetByPatientId(patient.ID);
            HIS_TREATMENT treatment = IsNotNullOrEmpty(allTreatments) ? allTreatments.OrderByDescending(o => o.ID).FirstOrDefault() : null;
            if (treatment != null)
            {
                if (PacsCFG.LIBRARY_HL7_VERSION == MedilinkHL7.SDK.SendSANCY.VersionV2.V231)
                {
                    hl7PACS.soTheBHYT = treatment.TDL_HEIN_CARD_NUMBER ?? treatment.TDL_PATIENT_CODE;
                }
                hl7PACS.idDotVaoVien = Convert.ToDecimal(treatment.TREATMENT_CODE);
                hl7PACS.soVaoVien_STTKham = treatment.IN_CODE;
                hl7PACS.ngayVaoVien = treatment.CLINICAL_IN_TIME.HasValue ? treatment.CLINICAL_IN_TIME.ToString().Substring(0, 8) : treatment.IN_TIME.ToString().Substring(0, 8);
                hl7PACS.doiTuongBenhNhan = HisPatientTypeCFG.DATA.FirstOrDefault(o => o.ID == treatment.TDL_PATIENT_TYPE_ID).PATIENT_TYPE_CODE;
                LoaiBenhNhan loaiBN = treatment.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU ? LoaiBenhNhan.I : LoaiBenhNhan.O;
                hl7PACS.loaiBenhNhan = loaiBN;
            }

            return hl7PACS;
        }

        void IPacsProcessor.UpdateStatus(List<PacsOrderData> listData, List<string> sqls)
        {
            try
            {
                List<long> newIds = new List<long>();
                List<long> updateIds = new List<long>();

                foreach (PacsOrderData item in listData)
                {
                    if (!item.IsSuccess.HasValue) continue;
                    if (item.IsSuccess.Value)
                    {
                        if (!item.ServiceReq.IS_SENT_EXT.HasValue)
                        {
                            newIds.Add(item.ServiceReq.ID);
                        }
                        else if (item.ServiceReq.IS_SENT_EXT == Constant.IS_TRUE
                            && item.ServiceReq.IS_UPDATED_EXT == Constant.IS_TRUE)
                        {
                            updateIds.Add(item.ServiceReq.ID);
                        }
                    }
                }

                if (IsNotNullOrEmpty(newIds))
                {
                    string sql = "UPDATE HIS_SERVICE_REQ SET IS_SENT_EXT = 1 WHERE %IN_CLAUSE% ";
                    sql = DAOWorker.SqlDAO.AddInClause(newIds, sql, "ID");
                    sqls.Add(sql);
                }
                if (IsNotNullOrEmpty(updateIds))
                {
                    string sql = "UPDATE HIS_SERVICE_REQ SET IS_UPDATED_EXT = NULL WHERE IS_UPDATED_EXT IS NOT NULL AND %IN_CLAUSE% ";
                    sql = DAOWorker.SqlDAO.AddInClause(updateIds, sql, "ID");
                    sqls.Add(sql);
                }

                if (IsNotNullOrEmpty(sqls))
                {
                    if (!DAOWorker.SqlDAO.Execute(sqls))
                    {
                        LogSystem.Warn("Update Status Pacs that bai sql: " + sqls.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private static string GetAgeForChild(DateTime from, DateTime to)
        {
            if (from > to) return GetAgeForChild(to, from);

            string tuoi = "";
            try
            {
                string caption__ThangTuoi = "TH";
                string caption__NgayTuoi = "NT";

                TimeSpan diff__hour = (to - from);
                TimeSpan diff__month = (to.Date - from.Date);

                long tongsogiay__hour = diff__hour.Ticks;
                System.DateTime newDate__hour = new System.DateTime(tongsogiay__hour);
                int month__hour = ((newDate__hour.Year - 1) * 12 + newDate__hour.Month - 1);
                if (month__hour == 0)
                {
                    //Neu Bn tren 24 gio va duoi 1 thang tuoi => hien thi "xyz ngay tuoi"
                    tuoi = " " + ((int)diff__month.TotalDays + caption__NgayTuoi);
                }
                else
                {
                    long tongsogiay = diff__month.Ticks;
                    System.DateTime newDate = new System.DateTime(tongsogiay);
                    int month = ((newDate.Year - 1) * 12 + newDate.Month - 1);
                    if (month == 0)
                    {
                        //Neu Bn tren 24 gio va duoi 1 thang tuoi => hien thi "xyz ngay tuoi"
                        tuoi = " " + ((int)diff__month.TotalDays + caption__NgayTuoi);
                    }
                    else
                    {
                        //- Duoi 72 thang tuoi: tinh chinh xac den thang nhu hien tai
                        if (month < 72)
                        {
                            tuoi = " " + (month + caption__ThangTuoi);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                tuoi = "";
            }
            return tuoi;
        }

        private static string EliminateSpecialCharacter(string s)
        {
            if (!string.IsNullOrWhiteSpace(s))
            {
                return s.Replace("|", "").Replace("\r", "").Replace("\n", "");
            }
            return s;
        }

        bool IPacsProcessor.UpdatePatientInfo(HIS_PATIENT patient, ref List<string> messages)
        {
            bool result = true;
            try
            {
                if (IsNotNull(patient))
                {
                    if (PacsCFG.PATIENT_INFO_UPDATE_OPTION == PatientInfoUpdateOption.ADTA08)
                    {
                        HL7PACS pacsData = this.MakeHL7PACSForADTA08(patient);

                        SendSANCY.VersionV2 version = PacsCFG.LIBRARY_HL7_VERSION;

                        if (PacsCFG.CONNECTION_TYPE == PacsConnectionType.FILE)
                        {
                            List<PacsFileAddress> fileAdds = PacsCFG.PACS_FILE_ADDRESSES.Where(o => !String.IsNullOrWhiteSpace(o.SaveFolder) && !String.IsNullOrWhiteSpace(o.Ip)).GroupBy(p => new { p.SaveFolder, p.Ip }).Select(g => g.First()).ToList();
                            if (IsNotNullOrEmpty(fileAdds))
                            {
                                foreach (var fileAdd in fileAdds)
                                {
                                    string fileName = string.Format("ADTA08_{0}_{1}.HL7", patient.PATIENT_CODE, DateTime.Now.ToString("yyyyMMddHHmmssfff"));

                                    string data = SendSANCY.CreatePatientInfo(pacsData, version);

                                    if (FileHandler.Write(fileAdd.Ip, fileAdd.User, fileAdd.Password, data, fileName, fileAdd.SaveFolder))
                                    {
                                        return true;
                                    }
                                    else
                                    {
                                        LogSystem.Error(" File: Tao du lieu HL7 va luu vao folder cua he thong PACS that bai. IP: " + fileAdd.Ip + "; SaveFolder: " + fileAdd.SaveFolder + "; pacsData: " + data);
                                        return false;
                                    }
                                }
                            }
                        }
                        else if (PacsCFG.CONNECTION_TYPE == PacsConnectionType.API)
                        {
                            List<PacsAddress> adds = PacsCFG.PACS_ADDRESS.Where(o => !String.IsNullOrWhiteSpace(o.Address) && o.Port > 0).GroupBy(p => new { p.Address, p.Port }).Select(g => g.First()).ToList();
                            if (IsNotNullOrEmpty(adds))
                            {
                                string patientInfoHl7 = SendSANCY.CreatePatientInfo(pacsData, version);
                                patientInfoHl7 = HL7Sender.CreateMLLPMessage(patientInfoHl7);
                                foreach (var add in adds)
                                {
                                    Output output = new SendSANCY().CallApiMessage(patientInfoHl7, add.Address, add.Port, PacsCFG.PACS_API_INFO.PatientInfo, version);
                                    if (output != null && output.Success)
                                    {
                                        return true;
                                    }
                                    else
                                    {
                                        LogSystem.Warn(String.Format("API: Gui thong tin benh nhan {0} sang he thong PACS Sancy theo thong tin dia chi IP {1} va port {2} that bai.", patient.PATIENT_CODE, add.Address, add.Port)
                                            + LogUtil.TraceData("\n Output", output));
                                        return false;
                                    }
                                }
                            }
                        }
                        else
                        {
                            List<PacsAddress> adds = PacsCFG.PACS_ADDRESS.Where(o => !String.IsNullOrWhiteSpace(o.Address) && o.Port > 0).GroupBy(p => new { p.Address, p.Port }).Select(g => g.First()).ToList();
                            if (IsNotNullOrEmpty(adds))
                            {
                                foreach (var add in adds)
                                {
                                    Output output = new SendSANCY().SendPatientInfo(pacsData, add.Address, add.Port, version);
                                    if (output != null && output.Success)
                                    {
                                        return true;
                                    }
                                    else
                                    {
                                        LogSystem.Warn(String.Format("TCP/IP: Gui thong tin benh nhan {0} sang he thong PACS Sancy theo thong tin dia chi IP {1} va port {2} that bai.", patient.PATIENT_CODE, add.Address, add.Port)
                                            + LogUtil.TraceData("\n Output", output));
                                        return false;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        HisServiceReqFilterQuery filter = new HisServiceReqFilterQuery();
                        filter.IS_NOT_SENT__OR__UPDATED = false; //lay cac y lenh chua gui sang PACS hoac co cap nhat
                        filter.SERVICE_REQ_STT_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL;
                        filter.ALLOW_SEND_PACS = true;
                        filter.NOT_IN_SERVICE_REQ_TYPE_IDs = new List<long>() { IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__XN };
                        filter.TDL_PATIENT_ID = patient.ID;
                        List<HIS_SERVICE_REQ> serviceReqs = new HisServiceReqGet().Get(filter);

                        if (IsNotNullOrEmpty(serviceReqs))
                        {
                            List<HIS_SERE_SERV> sereServs = new List<HIS_SERE_SERV>();
                            List<long> serviceReqIds = serviceReqs != null ? serviceReqs.Select(o => o.ID).ToList() : null;
                            if (serviceReqIds != null && serviceReqIds.Count > 0)
                            {
                                HisSereServFilterQuery ssFilter = new HisSereServFilterQuery();
                                ssFilter.HAS_EXECUTE = true;
                                ssFilter.SERVICE_REQ_IDs = serviceReqIds;
                                //voi sancy thi can lay ca cac du lieu da xoa (is_delete = 1)
                                ssFilter.IS_INCLUDE_DELETED = true;
                                sereServs = new HisSereServGet().Get(ssFilter);
                            }

                            List<PacsOrderData> orderData = GetDataProcessor.Prepare(sereServs, serviceReqs);
                            foreach (var data in orderData)
                            {
                                V_HIS_ROOM room = HisRoomCFG.DATA.FirstOrDefault(o => o.ID == data.ServiceReq.REQUEST_ROOM_ID);
                                V_HIS_ROOM exeRoom = HisRoomCFG.DATA.FirstOrDefault(o => o.ID == data.ServiceReq.EXECUTE_ROOM_ID);
                                List<V_HIS_TREATMENT_BED_ROOM> treatBedRooms = new HisTreatmentBedRoomGet().GetViewCurrentInByTreatmentId(data.Treatment.ID);
                                V_HIS_TREATMENT_BED_ROOM bedRoom = treatBedRooms != null ? treatBedRooms.FirstOrDefault() : null;

                                PacsAddress add = PacsCFG.PACS_ADDRESS.FirstOrDefault(o => o.RoomCode == exeRoom.ROOM_CODE && !String.IsNullOrWhiteSpace(o.Address) && o.Port > 0);
                                PacsFileAddress fileAdd = PacsCFG.PACS_FILE_ADDRESSES.FirstOrDefault(o => o.RoomCode == exeRoom.ROOM_CODE && !String.IsNullOrWhiteSpace(o.SaveFolder));

                                if ((PacsCFG.CONNECTION_TYPE != PacsConnectionType.FILE && PacsCFG.CONNECTION_TYPE != PacsConnectionType.API && !IsNotNull(add)) || (PacsCFG.CONNECTION_TYPE == PacsConnectionType.FILE && !IsNotNull(fileAdd)))
                                {
                                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_PhongXuLyChuaDuocCauHinhDiaChiPacs, exeRoom.ROOM_NAME);
                                    LogSystem.Warn(String.Format("Phong xu ly ma {0} khong co cau hinh dia chi server Pacs", exeRoom.ROOM_CODE));
                                    return false;
                                }

                                if (IsNotNullOrEmpty(data.Availables))
                                {
                                    foreach (HIS_SERE_SERV sereServ in data.Availables)
                                    {
                                        if (sereServ.IS_DELETE == Constant.IS_TRUE)
                                        {
                                            if (!this.SendDeleteOrder(data.Treatment, data.ServiceReq, sereServ, room, exeRoom, bedRoom, data.KskContract, add, fileAdd))
                                            {
                                                Inventec.Common.Logging.LogSystem.Error("Gui chi dinh sang he thong PACS that bai");
                                                Inventec.Common.Logging.LogSystem.Error(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => sereServ), sereServ));
                                                return false;
                                            }
                                        }
                                        else
                                        {
                                            if (!this.SendNewOrder(data.Treatment, data.ServiceReq, sereServ, room, exeRoom, bedRoom, data.KskContract, add, fileAdd))
                                            {
                                                Inventec.Common.Logging.LogSystem.Error("Gui chi dinh sang he thong PACS that bai");
                                                Inventec.Common.Logging.LogSystem.Error(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => sereServ), sereServ));
                                                return false;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }
    }
}
