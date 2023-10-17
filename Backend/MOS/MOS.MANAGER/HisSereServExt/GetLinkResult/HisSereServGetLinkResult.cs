using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisTreatment;
using MedilinkHL7.SDK;
using MOS.PACS.HL7;
using MOS.MANAGER.HisServiceReq.Pacs.PacsProcessor.PacsSancy;
using MOS.MANAGER.HisTreatmentBedRoom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOS.MANAGER.Config.CFG;
using MOS.MANAGER.HisKskContract;

namespace MOS.MANAGER.HisSereServExt.GetLinkResult
{
    public class HisSereServGetLinkResult : BusinessBase
    {
        internal HisSereServGetLinkResult()
            : base()
        {
            this.Init();
        }

        internal HisSereServGetLinkResult(CommonParam paramUpdate)
            : base(paramUpdate)
        {
            this.Init();
        }

        private void Init()
        {
        }

        internal bool Run(long sereServId, ref string resultData)
        {
            bool result = false;
            try
            {

                HIS_SERE_SERV sereServ = null;
                V_HIS_SERVICE service = null;
                HIS_SERVICE_REQ serviceReq = null;
                HIS_TREATMENT treatment = null;
                V_HIS_ROOM exeRoom = null;
                List<PacsAddress> pAddresses = null;

                HisSereServCheck ssChecker = new HisSereServCheck(param);
                HisServiceReqCheck reqChecker = new HisServiceReqCheck(param);
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);

                bool valid = true;
                valid = valid && ssChecker.VerifyId(sereServId, ref sereServ);
                valid = valid && reqChecker.VerifyId(sereServ.SERVICE_REQ_ID.Value, ref serviceReq);
                valid = valid && treatmentChecker.VerifyId(serviceReq.TREATMENT_ID, ref treatment);
                valid = valid && this.IsValid(sereServ, ref exeRoom, ref pAddresses, ref service);
                if (valid)
                {
                    if (string.IsNullOrWhiteSpace(PacsCFG.WCF_ADDRESS))
                    {
                        resultData = "";
                    }
                    else
                    {
                        List<V_HIS_TREATMENT_BED_ROOM> treatBedRooms = new HisTreatmentBedRoomGet().GetViewCurrentInByTreatmentId(treatment.ID);
                        V_HIS_TREATMENT_BED_ROOM bedRoom = treatBedRooms != null ? treatBedRooms.FirstOrDefault() : null;
                        V_HIS_ROOM reqroom = HisRoomCFG.DATA.FirstOrDefault(o => o.ID == sereServ.TDL_REQUEST_ROOM_ID);
                        V_HIS_ROOM exeroom = HisRoomCFG.DATA.FirstOrDefault(o => o.ID == sereServ.TDL_EXECUTE_ROOM_ID);
                        PacsAddress pAdd = pAddresses.FirstOrDefault(o => o.RoomCode == exeRoom.ROOM_CODE);
                        V_HIS_KSK_CONTRACT kskContract = null;
                        if (serviceReq.TDL_KSK_CONTRACT_ID.HasValue)
                        {
                            kskContract = new HisKskContractGet().GetViewById(serviceReq.TDL_KSK_CONTRACT_ID.Value);
                        }

                        HL7PACS pacsData = new PacsSancyProcessor(param).MakeHL7PACS(treatment, serviceReq, reqroom, exeroom, bedRoom, sereServ, pAdd, kskContract);
                        pacsData.EnteringDevice = pAdd.CloudInfo;

                        string data = HL7Processor.GetLinkView(PacsCFG.WCF_ADDRESS, pAdd.Address, pAdd.Port, pacsData, sereServ.ID.ToString());

                        if (String.IsNullOrWhiteSpace(data))
                        {
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSereServExt_KhongLayDuocLinhKetQua);
                            result = false;
                        }

                        MedilinkHL7.SDK.AckMessage ack = new MedilinkHL7.SDK.AckMessage(data);
                        if (ack != null)
                        {
                            resultData = ack.getObservationValue(data);
                        }
                    }

                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
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
