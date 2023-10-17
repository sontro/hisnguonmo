using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisTreatment;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Pacs
{
    class SendCheck
    {
        /// <summary>
        /// Kiem tra xem y lenh (service_req) co duoc phep gui sang he thong tich hop PACS ko.
        /// Thoa man 1 trong cac dieu kien sau:
        /// 1: Thuoc dien ko can thanh toan
        /// 2: Da thanh toan/hoac da tam ung
        /// 3: Da gui sang PACS va co su cap nhat noi dung
        /// </summary>
        /// <param name="treatment">treatment</param>
        /// <param name="sr">HIS_SERVICE_REQ can check</param>
        /// <param name="ss">HIS_SERE_SERV</param>
        /// <param name="bills">HIS_SERE_SERV_BILL ko tinh cancel</param>
        /// <param name="deposits">HIS_SERE_SERV_DEPOSIT ko tinh cancel</param>
        /// <param name="repays">HIS_SESE_DEPO_REPAY ko tinh cancel</param>
        /// <returns></returns>
        public static bool IsAllowSend(HIS_TREATMENT treatment, HIS_SERVICE_REQ sr, List<HIS_SERE_SERV> ss, List<HIS_SERE_SERV_BILL> bills, List<HIS_SERE_SERV_DEPOSIT> deposits, List<HIS_SESE_DEPO_REPAY> repays)
        {
            //Xu ly de ko lay cac giao dich bi huy (tranh truong hop ben ngoai truyen vao cac giao dich da huy)
            bills = bills != null ? bills.Where(o => !o.IS_CANCEL.HasValue || o.IS_CANCEL.Value != Constant.IS_TRUE).ToList() : null;
            deposits = deposits != null ? deposits.Where(o => !o.IS_CANCEL.HasValue || o.IS_CANCEL.Value != Constant.IS_TRUE).ToList() : null;
            repays = repays != null ? repays.Where(o => !o.IS_CANCEL.HasValue || o.IS_CANCEL.Value != Constant.IS_TRUE).ToList() : null;

            List<HIS_SERE_SERV> availableSs = ss != null && sr != null ?
                ss.Where(o => o.SERVICE_REQ_ID == sr.ID && o.IS_DELETE != Constant.IS_TRUE).ToList() : null;

            return (sr.IS_SENT_EXT == Constant.IS_TRUE && sr.IS_UPDATED_EXT == Constant.IS_TRUE)
                || SendCheck.IsNotNeedToPay(treatment, sr, availableSs)
                || SendCheck.IsPaid(availableSs, bills, deposits, repays);
        }

        /// <summary>
        /// Kiem tra xem benh nhan co can phai tra tien cho chi dinh PACS do ko
        /// </summary>
        /// <param name="treatment"></param>
        /// <param name="sr"></param>
        /// <param name="ss"></param>
        /// <returns></returns>
        private static bool IsNotNeedToPay(HIS_TREATMENT treatment, HIS_SERVICE_REQ sr, List<HIS_SERE_SERV> ss)
        {
            if (treatment != null && sr != null && ss != null && ss.Count > 0)
            {
                return treatment.OWE_TYPE_ID.HasValue //ho so duoc danh dau "cho no vien phi"
                    || treatment.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU //doi tuong "noi tru"
                    || sr.IS_NOT_REQUIRE_FEE == Constant.IS_TRUE //y lenh duoc danh dau "thu sau"
                    || SendCheck.IsNotRequireFeeForBhyt(ss) //ko thu tien voi BHYT
                    || SendCheck.IsEmergency(sr.REQUEST_ROOM_ID) //phong chi dinh la cap cuu
                    || SendCheck.IsEmergency(sr.EXECUTE_ROOM_ID) //phong xu ly la cap cuu
                    || SendCheck.IsNoPatientPrice(ss); //so tien benh nhan can phai thanh toan <= 0
            }
            return false;
        }

        private static bool IsPaid(List<HIS_SERE_SERV> ss, List<HIS_SERE_SERV_BILL> bills, List<HIS_SERE_SERV_DEPOSIT> deposits, List<HIS_SESE_DEPO_REPAY> repays)
        {
            if (ss != null && ss.Count > 0)
            {
                //Lay cac tam ung chua bi hoan ung
                List<HIS_SERE_SERV_DEPOSIT> noRepayDeposits = deposits != null ?
                    deposits.Where(o => repays == null || !repays.Exists(t => t.SERE_SERV_DEPOSIT_ID == o.ID)).ToList() : null;
                //Neu tat ca deu duoc tam ung hoac da duoc thanh toan thi duoc coi la da "tra tien"
                return ss.All(o => (noRepayDeposits != null && noRepayDeposits.Exists(t => t.SERE_SERV_ID == o.ID))
                    || (bills != null && bills.Exists(t => t.SERE_SERV_ID == o.ID)));
            }
            return false;
        }

        private static bool IsEmergency(long roomId)
        {
            V_HIS_EXECUTE_ROOM room = HisExecuteRoomCFG.DATA != null ? HisExecuteRoomCFG.DATA.Where(o => o.ROOM_ID == roomId).FirstOrDefault() : null;
            return room != null && room.IS_EMERGENCY == Constant.IS_TRUE;
        }

        private static bool IsNotRequireFeeForBhyt(List<HIS_SERE_SERV> ss)
        {
            return ss.All(o => o.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT)
                && HisServiceReqCFG.NOT_REQUIRE_FEE_FOR_BHYT == HisServiceReqCFG.NotRequireFeeOption.NOT_BLOCKING;
        }

        private static bool IsNoPatientPrice(List<HIS_SERE_SERV> ss)
        {
            return ss.Sum(o => o.VIR_TOTAL_PATIENT_PRICE) <= 0;
        }
    }
}
