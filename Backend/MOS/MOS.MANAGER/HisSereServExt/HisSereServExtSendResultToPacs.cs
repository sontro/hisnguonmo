using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.HisServiceReq;
using MOS.PACS.Fhir;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedilinkHL7.SDK;
using MOS.UTILITY;
using MOS.MANAGER.HisTreatmentBedRoom;
using MOS.MANAGER.HisServiceReq.Pacs.PacsProcessor.PacsSancy;
using MOS.MANAGER.HisKskContract;

namespace MOS.MANAGER.HisSereServExt
{
    class HisSereServExtSendResultToPacs : BusinessBase
    {
        private FhirProcessor fhirProcessor;

        internal HisSereServExtSendResultToPacs()
            : base()
        {
        }

        internal HisSereServExtSendResultToPacs(CommonParam param)
            : base(param)
        {
        }

        internal bool Run(HIS_SERE_SERV_EXT sereServExt)
        {
            bool result = false;
            try
            {
                if (sereServExt == null)
                {
                    return result;
                }

                if (PacsCFG.PACS_INTEGRATE_OPTION == (int)PacsIntegrateOption.PACS_BACH_KHOA)
                {
                    if (String.IsNullOrWhiteSpace(PacsCFG.FHIR_CONNECT_INFO))
                        throw new NullReferenceException("Cau hinh FHIR OPTION thieu thong tin");

                    List<string> cfgs = PacsCFG.FHIR_CONNECT_INFO.Split('|').ToList();
                    if (cfgs == null || cfgs.Count < 3)
                    {
                        throw new ArgumentNullException("Cau hinh FHIR OPTION thieu thong tin");
                    }

                    string uri = cfgs[0];
                    string loginname = cfgs[1];
                    string password = cfgs[2];

                    if (String.IsNullOrWhiteSpace(uri) || String.IsNullOrWhiteSpace(loginname) || String.IsNullOrWhiteSpace(password))
                        throw new ArgumentNullException("Cau hinh FHIR OPTION thieu thong tin");

                    fhirProcessor = new FhirProcessor(uri, loginname, password, Config.HisEmployeeCFG.DATA, Config.HisServiceCFG.DATA_VIEW);

                    var sereServ = new HisSereServGet().GetById(sereServExt.SERE_SERV_ID);
                    var treatment = sereServ != null && sereServ.TDL_TREATMENT_ID.HasValue ? new HisTreatmentGet().GetById(sereServ.TDL_TREATMENT_ID.Value) : null;
                    var serviceReq = sereServ != null && sereServ.SERVICE_REQ_ID.HasValue ? new HisServiceReqGet().GetById(sereServ.SERVICE_REQ_ID.Value) : null;

                    if (sereServ != null && treatment != null)
                    {
                        List<string> sqls = new List<string>();

                        V_HIS_ROOM exeRoom = HisRoomCFG.DATA.FirstOrDefault(o => o.ID == serviceReq.EXECUTE_ROOM_ID);

                        string studyID = "";
                        if (fhirProcessor.SendResult(treatment, serviceReq, sereServ, exeRoom, sereServExt, ref studyID))
                        {
                            string sql = "UPDATE HIS_SERE_SERV_EXT SET JSON_PRINT_ID = 'studyID:{0}' WHERE ID = {1}";
                            sqls.Add(string.Format(sql, studyID, sereServExt.ID));
                        }
                        else
                        {
                            Inventec.Common.Logging.LogSystem.Warn(string.Format("Khong gui dc ket qua SERE_SERV_EXT_ID: {0} SERE_SERV_ID: {1} ", sereServExt.ID, sereServ.ID));
                            Inventec.Common.Logging.LogAction.Warn(string.Format("Khong gui dc ket qua SERE_SERV_EXT_ID: {0} SERE_SERV_ID: {1} ", sereServExt.ID, sereServ.ID));
                        }

                        if (!DAOWorker.SqlDAO.Execute(sqls))
                        {
                            LogSystem.Warn("Update Status Pacs that bai sql: " + sqls.ToString());
                            LogAction.Warn("Update Status Pacs that bai sql: " + sqls.ToString());
                        }
                    }
                }
                else if (PacsCFG.PACS_INTEGRATE_OPTION == (int)PacsIntegrateOption.PACS_SANCY
                    && PacsCFG.LIBRARY_HL7_VERSION == MedilinkHL7.SDK.SendSANCY.VersionV2.V27)
                {
                    HIS_SERVICE_REQ serviceReq = null;
                    HIS_TREATMENT treatment = null;
                    HIS_SERE_SERV sereServ = null;
                    PacsAddress pAdd = null;

                    HL7ResultData hl7ResultData = MakeHL7ResultData(sereServExt, ref treatment, ref serviceReq, ref sereServ, ref pAdd);
                    if (hl7ResultData == null)
                        return result;
                    if (PacsCFG.CONNECTION_TYPE == PacsConnectionType.TCP_IP)
                    {
                        string hl7Message = new SendSANCY().CreateResultHl7(hl7ResultData, SendSANCY.VersionV2.V27);
                        List<PacsAddress> adds = PacsCFG.PACS_ADDRESS.Where(o => !String.IsNullOrWhiteSpace(o.Address) && o.Port > 0).GroupBy(p => new { p.Address, p.Port }).Select(g => g.First()).ToList();
                        if (IsNotNullOrEmpty(adds))
                        {
                            foreach (var add in adds)
                            {
                                new SendSANCY().SendHl7Message(hl7Message, add.Address, add.Port, "", string.Format("{0}_{1}_{2}", add.Address, add.Port, DateTime.Now.ToString("yyyyMMddHHmmssfff")));
                            }
                            result = true;
                        }
                    }
                    else if (PacsCFG.CONNECTION_TYPE == PacsConnectionType.FILE)
                    {
                        List<PacsFileAddress> fileAdds = PacsCFG.PACS_FILE_ADDRESSES.Where(o => !String.IsNullOrWhiteSpace(o.SaveFolder) && !String.IsNullOrWhiteSpace(o.Ip)).GroupBy(p => new { p.SaveFolder, p.Ip }).Select(g => g.First()).ToList();
                        if (IsNotNullOrEmpty(fileAdds))
                        {
                            foreach (var fileAdd in fileAdds)
                            {
                                string fileName = string.Format("ORU_{0}_{1}_{2}.HL7", serviceReq.SERVICE_REQ_CODE, sereServ.TDL_SERVICE_CODE, DateTime.Now.ToString("yyyyMMddHHmmssfff"));

                                string hl7Message = new SendSANCY().CreateResultHl7(hl7ResultData, SendSANCY.VersionV2.V27);

                                if (FileHandler.Write(fileAdd.Ip, fileAdd.User, fileAdd.Password, hl7Message, fileName, fileAdd.SaveFolder))
                                {
                                    return true;
                                }
                                else
                                {
                                    LogSystem.Error(" File: Tao du lieu HL7 va luu vao folder cua he thong PACS that bai. IP: " + fileAdd.Ip + "; SaveFolder: " + fileAdd.SaveFolder + "; pacsData: " + hl7Message);
                                    return false;
                                }
                            }
                        }
                    }
                    else if (PacsCFG.CONNECTION_TYPE == PacsConnectionType.API)
                    {
                        Output output = new SendSANCY().SendResultHl7ByApi(hl7ResultData, pAdd.Address, pAdd.Port, PacsCFG.PACS_API_INFO.SendResult, SendSANCY.VersionV2.V27);
                        if (output != null && output.Success)
                        {
                            return true;
                        }
                        else
                        {
                            LogSystem.Warn(String.Format("Cap nhat ket qua sang server Pacs Sancy that bai. Ma: {0}-{1}", serviceReq.SERVICE_REQ_CODE, sereServ.TDL_SERVICE_CODE)
                                + LogUtil.TraceData("\n Output", output));
                            return false;
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {
                result = false;
                Inventec.Common.Logging.LogSystem.Error(ex);
                Inventec.Common.Logging.LogAction.Error(ex);
            }
            return result;
        }

        private HL7ResultData MakeHL7ResultData(HIS_SERE_SERV_EXT sereServExt, ref HIS_TREATMENT treatment, ref HIS_SERVICE_REQ serviceReq, ref HIS_SERE_SERV sereServ, ref PacsAddress pAdd)
        {
            HL7ResultData result = null;
            try
            {
                V_HIS_SERVICE service = null;
                V_HIS_ROOM exeRoom = null;
                List<PacsAddress> pAddresses = null;

                HisSereServCheck ssChecker = new HisSereServCheck(param);
                HisServiceReqCheck reqChecker = new HisServiceReqCheck(param);
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);

                bool valid = true;
                valid = valid && ssChecker.VerifyId(sereServExt.SERE_SERV_ID, ref sereServ);
                valid = valid && reqChecker.VerifyId(sereServ.SERVICE_REQ_ID.Value, ref serviceReq);
                valid = valid && treatmentChecker.VerifyId(serviceReq.TREATMENT_ID, ref treatment);
                valid = valid && this.IsValid(sereServ, ref exeRoom, ref pAddresses, ref service);
                if (valid)
                {
                    List<V_HIS_TREATMENT_BED_ROOM> treatBedRooms = new HisTreatmentBedRoomGet().GetViewCurrentInByTreatmentId(treatment.ID);
                    V_HIS_TREATMENT_BED_ROOM bedRoom = treatBedRooms != null ? treatBedRooms.FirstOrDefault() : null;
                    long requestRoomId = sereServ.TDL_REQUEST_ROOM_ID;
                    long excecuteRoomId = sereServ.TDL_EXECUTE_ROOM_ID;
                    V_HIS_ROOM reqroom = HisRoomCFG.DATA.FirstOrDefault(o => o.ID == requestRoomId);
                    V_HIS_ROOM exeroom = HisRoomCFG.DATA.FirstOrDefault(o => o.ID == excecuteRoomId);
                    pAdd = pAddresses.FirstOrDefault(o => o.RoomCode == exeRoom.ROOM_CODE);
                    V_HIS_KSK_CONTRACT kskContract = null;
                    if (serviceReq.TDL_KSK_CONTRACT_ID.HasValue)
                    {
                        kskContract = new HisKskContractGet().GetViewById(serviceReq.TDL_KSK_CONTRACT_ID.Value);
                    }

                    result = new PacsSancyProcessor(param).MakeHL7ResultData(treatment, serviceReq, reqroom, exeroom, bedRoom, sereServ, pAdd, kskContract, sereServExt);
                }
            }
            catch (Exception ex)
            {
                result = null;
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }

        private bool IsValid(HIS_SERE_SERV sereServ, ref  V_HIS_ROOM exeRoom, ref List<PacsAddress> pAddresses, ref V_HIS_SERVICE service)
        {
            bool valid = true;
            try
            {
                if (sereServ != null)
                {
                    service = HisServiceCFG.DATA_VIEW.FirstOrDefault(o => o.ID == sereServ.SERVICE_ID);
                    exeRoom = HisRoomCFG.DATA.FirstOrDefault(o => o.ID == sereServ.TDL_EXECUTE_ROOM_ID);
                    pAddresses = PacsCFG.PACS_ADDRESS;

                    if (service == null)
                    {
                        throw new Exception("Khong tim thay dich vu");
                    }

                    if (!HisServiceReqTypeCFG.PACS_TYPE_IDs.Contains(sereServ.TDL_SERVICE_REQ_TYPE_ID))
                    {
                        throw new Exception("Dich vu khong thuoc loai CDHA, NS, SA, TDCN");
                    }

                    if (exeRoom == null)
                    {
                        throw new Exception("Khong lay duoc thong tin phong thuc hien");
                    }

                    if (!IsNotNullOrEmpty(pAddresses))
                    {
                        throw new Exception("Khong lay duoc thong tin dia chi pacs");
                    }

                    List<string> roomCodes = pAddresses.Select(o => o.RoomCode).ToList();

                    if (!IsNotNullOrEmpty(roomCodes))
                    {
                        throw new Exception("Khong lay duoc thong tin ma phong thuc hien trong cau hinh dia chi Pacs");
                    }

                    if (!roomCodes.Contains(exeRoom.ROOM_CODE))
                    {
                        throw new Exception("Dich vu khong co phong xu ly duoc cau hinh tai cau hinh he thong MOS.PACS.ADDRESS");
                    }
                }
            }
            catch (Exception ex)
            {
                valid = false;
                LogSystem.Error(ex);
            }

            return valid;
        }
    }
}
