using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisTreatment;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Common
{
    public class SendIntegratorCheck
    {
        /// <summary>
        /// Kiem tra xem y lenh (service_req) co duoc phep gui sang he thong tich hop LIS ko.
        /// Thoa man 1 trong cac dieu kien sau:
        /// 1: Thuoc dien ko can thanh toan
        /// 2: Da thanh toan/hoac da tam ung
        /// 3: Da gui sang LIS va co su cap nhat noi dung
        /// </summary>
        /// <param name="treatment">treatment</param>
        /// <param name="sr">HIS_SERVICE_REQ can check</param>
        /// <param name="ss">HIS_SERE_SERV</param>
        /// <param name="bills">HIS_SERE_SERV_BILL ko tinh cancel</param>
        /// <param name="deposits">HIS_SERE_SERV_DEPOSIT ko tinh cancel</param>
        /// <param name="repays">HIS_SESE_DEPO_REPAY ko tinh cancel</param>
        /// <returns></returns>
        public static bool IsAllowSend(V_HIS_TREATMENT_FEE_1 treatment, HIS_SERVICE_REQ sr, List<HIS_SERE_SERV> ss, List<HIS_SERE_SERV_BILL> bills, List<HIS_SERE_SERV_DEPOSIT> deposits, List<HIS_SESE_DEPO_REPAY> repays, List<HIS_SERE_SERV_DEBT> depts)
        {
            if (treatment == null || sr == null || ss == null || ss.Count <= 0)
            {
                return false;
            }
            //Xu ly de ko lay cac giao dich bi huy (tranh truong hop ben ngoai truyen vao cac giao dich da huy)
            bills = bills != null ? bills.Where(o => (!o.IS_CANCEL.HasValue || o.IS_CANCEL.Value != Constant.IS_TRUE) && o.IS_ACTIVE == Constant.IS_TRUE).ToList() : null;
            deposits = deposits != null ? deposits.Where(o => (!o.IS_CANCEL.HasValue || o.IS_CANCEL.Value != Constant.IS_TRUE) && o.IS_ACTIVE == Constant.IS_TRUE).ToList() : null;
            repays = repays != null ? repays.Where(o => (!o.IS_CANCEL.HasValue || o.IS_CANCEL.Value != Constant.IS_TRUE) && o.IS_ACTIVE == Constant.IS_TRUE).ToList() : null;
            depts = depts != null ? depts.Where(o => (!o.IS_CANCEL.HasValue || o.IS_CANCEL.Value != Constant.IS_TRUE) && o.IS_ACTIVE == Constant.IS_TRUE).ToList() : null;
            List<HIS_SERE_SERV> availableSs = ss != null && sr != null ?
                ss.Where(o => o.SERVICE_REQ_ID == sr.ID && o.IS_DELETE != Constant.IS_TRUE && o.VIR_PRICE > 0).ToList() : null;

            return (sr.IS_SENT_EXT == Constant.IS_TRUE && sr.IS_UPDATED_EXT == Constant.IS_TRUE)
                || SendIntegratorCheck.IsNotNeedToPay(treatment, sr, availableSs)
                || SendIntegratorCheck.IsPaid(treatment, availableSs, bills, deposits, repays, depts);
        }

        /// <summary>
        /// Kiem tra xem benh nhan co can phai tra tien cho chi dinh XN do ko
        /// </summary>
        /// <param name="treatment"></param>
        /// <param name="sr"></param>
        /// <param name="ss"></param>
        /// <returns></returns>
        private static bool IsNotNeedToPay(V_HIS_TREATMENT_FEE_1 treatment, HIS_SERVICE_REQ sr, List<HIS_SERE_SERV> ss)
        {
            if (treatment != null && sr != null && ss != null && ss.Count > 0)
            {
                return treatment.OWE_TYPE_ID.HasValue //ho so duoc danh dau "cho no vien phi"
                    || treatment.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU //doi tuong "noi tru"
                    || (treatment.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNGOAITRU && HisServiceReqCFG.NOT_REQUIRE_FEE_FOR_BHYT == HisServiceReqCFG.NotRequireFeeOption.BY_PAYMENT_PATIENT_TYPE_AND_NOT_OUT_PATIENT) //doi tuong "dieu tri ngoai tru va co cau hinh = 3"
                    || ((treatment.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU && HisServiceReqCFG.NOT_REQUIRE_FEE_FOR_BHYT == HisServiceReqCFG.NotRequireFeeOption.BY_IN_PATIENT_AND_OUT_PATIENT) // doi tuong "dieu tri noi tru va co cau hinh = 4"
                    || (treatment.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNGOAITRU && HisServiceReqCFG.NOT_REQUIRE_FEE_FOR_BHYT == HisServiceReqCFG.NotRequireFeeOption.BY_IN_PATIENT_AND_OUT_PATIENT)) // doi tuong "dieu tri ngoai tru va co cau hinh = 4"
                    || sr.IS_NOT_REQUIRE_FEE == Constant.IS_TRUE //y lenh duoc danh dau "thu sau"
                    || SendIntegratorCheck.IsNotRequireFeeForBhyt(ss, treatment) //ko thu tien voi BHYT
                    || !Lis2CFG.LIS_FORBID_NOT_ENOUGH_FEE
                    || SendIntegratorCheck.IsEmergency(sr.REQUEST_ROOM_ID) //phong chi dinh la cap cuu
                    || SendIntegratorCheck.IsEmergency(sr.EXECUTE_ROOM_ID) //phong xu ly la cap cuu
                    || SendIntegratorCheck.IsNoPatientPrice(ss); //so tien benh nhan can phai thanh toan <= 0
            }
            return true;
        }

        private static bool IsPaid(V_HIS_TREATMENT_FEE_1 treatment, List<HIS_SERE_SERV> ss, List<HIS_SERE_SERV_BILL> bills, List<HIS_SERE_SERV_DEPOSIT> deposits, List<HIS_SESE_DEPO_REPAY> repays, List<HIS_SERE_SERV_DEBT> depts)
        {
            if (ss != null && ss.Count > 0)
            {
                //Lay cac tam ung chua bi hoan ung
                List<HIS_SERE_SERV_DEPOSIT> noRepayDeposits = deposits != null ?
                    deposits.Where(o => repays == null || !repays.Exists(t => t.SERE_SERV_DEPOSIT_ID == o.ID)).ToList() : null;
                //Neu tat ca deu duoc tam ung hoac da duoc thanh toan thi duoc coi la da "tra tien"
                decimal? unpaid = new HisTreatmentGet().GetUnpaid(treatment);

                List<HIS_SERE_SERV> toCheck = null;
                //Neu option 1 thi chi kiem tra nhung ban ghi ko duoc BHYT chi tra
                if (HisServiceReqCFG.NOT_REQUIRE_FEE_FOR_BHYT == HisServiceReqCFG.NotRequireFeeOption.BY_PATIENT_TYPE)
                {
                    toCheck = ss.Where(o => o.PATIENT_TYPE_ID != HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT).ToList();
                }
                //Neu option 2, 3 thi kiem tra ca nhung ban ghi co DTTT la BHYT nhung ko duoc BHYT chi tra
                else if (HisServiceReqCFG.NOT_REQUIRE_FEE_FOR_BHYT == HisServiceReqCFG.NotRequireFeeOption.BY_PAYMENT_PATIENT_TYPE
                    || HisServiceReqCFG.NOT_REQUIRE_FEE_FOR_BHYT == HisServiceReqCFG.NotRequireFeeOption.BY_PAYMENT_PATIENT_TYPE_AND_NOT_OUT_PATIENT)
                {
                    toCheck = ss.Where(o => o.PATIENT_TYPE_ID != HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT || o.VIR_TOTAL_HEIN_PRICE <= 0).ToList();
                }
                // nếu option 4 thi kiem tra nhung ban ghi khong dc quy chi tra
                else if (HisServiceReqCFG.NOT_REQUIRE_FEE_FOR_BHYT == HisServiceReqCFG.NotRequireFeeOption.BY_IN_PATIENT_AND_OUT_PATIENT)
                {
                    toCheck = ss.Where(o => o.IS_FUND_ACCEPTED != Constant.IS_TRUE).ToList();
                }
                else
                {
                    toCheck = ss;
                }

                if (toCheck == null || toCheck.Count == 0)
                {
                    return true;
                }

                return !unpaid.HasValue || unpaid.Value <= 0
                    || toCheck.All(o => (noRepayDeposits != null && noRepayDeposits.Exists(t => t.SERE_SERV_ID == o.ID))
                        || (bills != null && bills.Exists(t => t.SERE_SERV_ID == o.ID))
                        || (depts != null && depts.Exists(t => t.SERE_SERV_ID == o.ID))
                    );
            }
            return true;
        }

        private static bool IsEmergency(long roomId)
        {
            V_HIS_EXECUTE_ROOM room = HisExecuteRoomCFG.DATA != null ? HisExecuteRoomCFG.DATA.Where(o => o.ROOM_ID == roomId).FirstOrDefault() : null;
            return room != null && room.IS_EMERGENCY == Constant.IS_TRUE;
        }

        private static bool IsNotRequireFeeForBhyt(List<HIS_SERE_SERV> ss, V_HIS_TREATMENT_FEE_1 treatment)
        {
            //Neu check theo ho so thi chi can y lenh loai la BHYT (ton tai 1 dv duoc BHYT thanh toan) thi se cho phep
            if (HisServiceReqCFG.NOT_REQUIRE_FEE_FOR_BHYT == HisServiceReqCFG.NotRequireFeeOption.BY_PAYMENT_PATIENT_TYPE
                || HisServiceReqCFG.NOT_REQUIRE_FEE_FOR_BHYT == HisServiceReqCFG.NotRequireFeeOption.BY_PAYMENT_PATIENT_TYPE_AND_NOT_OUT_PATIENT)
            {
                return ss.All(o => o.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT && (o.VIR_TOTAL_HEIN_PRICE > 0 || o.VIR_PRICE == 0));
            }
            else if (HisServiceReqCFG.NOT_REQUIRE_FEE_FOR_BHYT == HisServiceReqCFG.NotRequireFeeOption.BY_PATIENT_TYPE)
            {
                return ss.Exists(o => o.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT);
            }
            else if (HisServiceReqCFG.NOT_REQUIRE_FEE_FOR_BHYT == HisServiceReqCFG.NotRequireFeeOption.BY_IN_PATIENT_AND_OUT_PATIENT)
            {
                return ((treatment.TOTAL_PATIENT_PRICE - treatment.TOTAL_DEPOSIT_AMOUNT - treatment.TOTAL_DEBT_AMOUNT - treatment.TOTAL_BILL_AMOUNT + treatment.TOTAL_BILL_TRANSFER_AMOUNT + treatment.TOTAL_REPAY_AMOUNT) <= 0);
            }
            return false;
        }

        private static bool IsNoPatientPrice(List<HIS_SERE_SERV> ss)
        {
            return ss.Sum(o => o.VIR_TOTAL_PATIENT_PRICE) <= 0;
        }
    }
}
