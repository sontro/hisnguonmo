using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisBid;
using MOS.MANAGER.HisMedicalContract;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMest
{
    class HisImpMestUtil
    {
        public static void SetTdl(HIS_IMP_MEST impMest, HIS_EXP_MEST expMest)
        {
            if (impMest != null && expMest != null)
            {
                impMest.TDL_PATIENT_ADDRESS = expMest.TDL_PATIENT_ADDRESS;
                impMest.TDL_PATIENT_ID = expMest.TDL_PATIENT_ID;
                impMest.TDL_PATIENT_CODE = expMest.TDL_PATIENT_CODE;
                impMest.TDL_PATIENT_DOB = expMest.TDL_PATIENT_DOB;
                impMest.TDL_PATIENT_FIRST_NAME = expMest.TDL_PATIENT_FIRST_NAME;
                impMest.TDL_PATIENT_GENDER_ID = expMest.TDL_PATIENT_GENDER_ID;
                impMest.TDL_PATIENT_GENDER_NAME = expMest.TDL_PATIENT_GENDER_NAME;
                impMest.TDL_PATIENT_IS_HAS_NOT_DAY_DOB = expMest.TDL_PATIENT_IS_HAS_NOT_DAY_DOB;
                impMest.TDL_PATIENT_LAST_NAME = expMest.TDL_PATIENT_LAST_NAME;
                impMest.TDL_TREATMENT_CODE = expMest.TDL_TREATMENT_CODE;
                impMest.TDL_PATIENT_NAME = expMest.TDL_PATIENT_NAME;
                impMest.TDL_TREATMENT_CODE = expMest.TDL_TREATMENT_CODE;
                impMest.TDL_TREATMENT_ID = expMest.TDL_TREATMENT_ID;
            }
        }

        public static void SetTdl(HIS_IMP_MEST impMestNew, HIS_IMP_MEST impMestOld)
        {
            if (impMestNew != null && impMestOld != null)
            {
                impMestNew.TDL_PATIENT_ADDRESS = impMestOld.TDL_PATIENT_ADDRESS;
                impMestNew.TDL_PATIENT_ID = impMestOld.TDL_PATIENT_ID;
                impMestNew.TDL_PATIENT_CODE = impMestOld.TDL_PATIENT_CODE;
                impMestNew.TDL_PATIENT_DOB = impMestOld.TDL_PATIENT_DOB;
                impMestNew.TDL_PATIENT_FIRST_NAME = impMestOld.TDL_PATIENT_FIRST_NAME;
                impMestNew.TDL_PATIENT_GENDER_ID = impMestOld.TDL_PATIENT_GENDER_ID;
                impMestNew.TDL_PATIENT_GENDER_NAME = impMestOld.TDL_PATIENT_GENDER_NAME;
                impMestNew.TDL_PATIENT_IS_HAS_NOT_DAY_DOB = impMestOld.TDL_PATIENT_IS_HAS_NOT_DAY_DOB;
                impMestNew.TDL_PATIENT_LAST_NAME = impMestOld.TDL_PATIENT_LAST_NAME;
                impMestNew.TDL_TREATMENT_CODE = impMestOld.TDL_TREATMENT_CODE;
                impMestNew.TDL_PATIENT_NAME = impMestOld.TDL_PATIENT_NAME;
                impMestNew.TDL_TREATMENT_CODE = impMestOld.TDL_TREATMENT_CODE;
                impMestNew.TDL_TREATMENT_ID = impMestOld.TDL_TREATMENT_ID;
                impMestNew.TDL_CHMS_EXP_MEST_CODE = impMestOld.TDL_CHMS_EXP_MEST_CODE;
                impMestNew.TDL_MOBA_EXP_MEST_CODE = impMestOld.TDL_MOBA_EXP_MEST_CODE;
                impMestNew.SPECIAL_MEDICINE_TYPE = impMestOld.SPECIAL_MEDICINE_TYPE;
            }
        }

        public static void SetTdl(HIS_IMP_MEST impMest, HIS_TREATMENT treatment)
        {
            if (impMest != null && treatment != null)
            {
                impMest.TDL_PATIENT_ADDRESS = treatment.TDL_PATIENT_ADDRESS;
                impMest.TDL_PATIENT_ID = treatment.PATIENT_ID;
                impMest.TDL_PATIENT_CODE = treatment.TDL_PATIENT_CODE;
                impMest.TDL_PATIENT_DOB = treatment.TDL_PATIENT_DOB;
                impMest.TDL_PATIENT_FIRST_NAME = treatment.TDL_PATIENT_FIRST_NAME;
                impMest.TDL_PATIENT_GENDER_ID = treatment.TDL_PATIENT_GENDER_ID;
                impMest.TDL_PATIENT_GENDER_NAME = treatment.TDL_PATIENT_GENDER_NAME;
                impMest.TDL_PATIENT_IS_HAS_NOT_DAY_DOB = treatment.TDL_PATIENT_IS_HAS_NOT_DAY_DOB;
                impMest.TDL_PATIENT_LAST_NAME = treatment.TDL_PATIENT_LAST_NAME;
                impMest.TDL_PATIENT_NAME = treatment.TDL_PATIENT_NAME;
                impMest.TDL_TREATMENT_CODE = treatment.TREATMENT_CODE;
                impMest.TDL_TREATMENT_ID = treatment.ID;
            }
        }

        public static void SetTdl(HIS_IMP_MEST impMest, List<HIS_MEDICINE> medicines, List<HIS_MATERIAL> materials)
        {
            if (impMest != null)
            {
                impMest.TDL_BID_NAMES = "";
                impMest.TDL_BID_NUMBERS = "";
                impMest.TDL_BID_GROUP_CODES = "";

                List<long> lstBidIds = new List<long>();
                List<string> listBidNumber = new List<string>();
                List<string> listBidGroupCode = new List<string>();

                if (medicines != null && medicines.Count > 0)
                {
                    lstBidIds.AddRange(medicines.Where(o => o.BID_ID.HasValue).Select(s => s.BID_ID.Value).ToList());
                    listBidNumber.AddRange(medicines.Where(o => !String.IsNullOrWhiteSpace(o.TDL_BID_NUMBER)).Select(s => s.TDL_BID_NUMBER).ToList());
                    listBidGroupCode.AddRange(medicines.Where(o => !String.IsNullOrWhiteSpace(o.TDL_BID_GROUP_CODE)).Select(s => s.TDL_BID_GROUP_CODE).ToList());
                }

                if (materials != null && materials.Count > 0)
                {
                    lstBidIds.AddRange(materials.Where(o => o.BID_ID.HasValue).Select(s => s.BID_ID.Value).ToList());
                    listBidNumber.AddRange(materials.Where(o => !String.IsNullOrWhiteSpace(o.TDL_BID_NUMBER)).Select(s => s.TDL_BID_NUMBER).ToList());
                    listBidGroupCode.AddRange(materials.Where(o => !String.IsNullOrWhiteSpace(o.TDL_BID_GROUP_CODE)).Select(s => s.TDL_BID_GROUP_CODE).ToList());
                }

                lstBidIds = lstBidIds.Distinct().ToList();
                listBidNumber = listBidNumber.Distinct().ToList();
                listBidGroupCode = listBidGroupCode.Distinct().ToList();

                if (lstBidIds != null && lstBidIds.Count > 0)
                {
                    List<HIS_BID> bids = new HisBidGet().GetByIds(lstBidIds);

                    if (bids != null && bids.Count > 0)
                    {
                        string bidNames = String.Join(", ", bids.Select(s => s.BID_NAME).ToList());
                        impMest.TDL_BID_NAMES = CommonUtil.SubString(bidNames, 4000);
                    }
                }

                if (listBidNumber != null && listBidNumber.Count > 0)
                {
                    string bidNumbers = String.Join(", ", listBidNumber);
                    impMest.TDL_BID_NUMBERS = CommonUtil.SubString(bidNumbers, 4000);
                }

                if (listBidGroupCode != null && listBidGroupCode.Count > 0)
                {
                    string bidGroups = String.Join(", ", listBidGroupCode);
                    impMest.TDL_BID_GROUP_CODES = CommonUtil.SubString(bidGroups, 500);
                }
            }
        }

        public static bool CheckIsDiff(HIS_IMP_MEST newImpMest, HIS_IMP_MEST oldImpMest)
        {
            if (newImpMest != null && oldImpMest != null)
            {
                return (
                    newImpMest.DELIVERER != oldImpMest.DELIVERER
                    || newImpMest.DESCRIPTION != oldImpMest.DESCRIPTION
                    || newImpMest.DISCOUNT != oldImpMest.DISCOUNT
                    || newImpMest.DISCOUNT_RATIO != oldImpMest.DISCOUNT_RATIO
                    || newImpMest.DOCUMENT_DATE != oldImpMest.DOCUMENT_DATE
                    || newImpMest.DOCUMENT_NUMBER != oldImpMest.DOCUMENT_NUMBER
                    || newImpMest.DOCUMENT_PRICE != oldImpMest.DOCUMENT_PRICE
                    || newImpMest.IMP_MEST_STT_ID != oldImpMest.IMP_MEST_STT_ID
                    || newImpMest.IMP_MEST_TYPE_ID != oldImpMest.IMP_MEST_TYPE_ID
                    || newImpMest.MEDI_STOCK_ID != oldImpMest.MEDI_STOCK_ID
                    || newImpMest.MEDI_STOCK_PERIOD_ID != oldImpMest.MEDI_STOCK_PERIOD_ID
                    || newImpMest.REQ_DEPARTMENT_ID != oldImpMest.REQ_DEPARTMENT_ID
                    || newImpMest.REQ_LOGINNAME != oldImpMest.REQ_LOGINNAME
                    || newImpMest.REQ_ROOM_ID != oldImpMest.REQ_ROOM_ID
                    );
            }
            else
            {
                return true;
            }
        }
    }
}
