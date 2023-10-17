using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisCard;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.HisWorkPlace;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTransaction
{
    class HisTransactionUtil : BusinessBase
    {
        /// <summary>
        /// Luu thong tin chi tiet ho so vien phi vao thong tin giao dich
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="sereServs"></param>
        /// <param name="existTransations"></param>
        public static void SetTreatmentFeeInfo(HIS_TRANSACTION transaction, bool notCheckConfig = false)
        {
            List<HIS_TRANSACTION> existTransations = null;
            SetTreatmentFeeInfo(transaction, ref existTransations, notCheckConfig);
        }

        /// <summary>
        /// Luu thong tin chi tiet ho so vien phi vao thong tin giao dich
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="sereServs"></param>
        /// <param name="existTransations"></param>
        public static void SetTreatmentFeeInfo(HIS_TRANSACTION transaction, ref List<HIS_TRANSACTION> existTransations, bool notCheckConfig = false)
        {
            try
            {
                if ((notCheckConfig || HisTransactionCFG.SET_TREATMENT_FEE_INFO) && transaction.TREATMENT_ID.HasValue)
                {
                    List<HIS_SERE_SERV> sereServs = new HisSereServGet().GetByTreatmentId(transaction.TREATMENT_ID.Value);

                    existTransations = new HisTransactionGet().GetByTreatmentId(transaction.TREATMENT_ID.Value);

                    //Tổng chi phí
                    transaction.TREATMENT_TOTAL_PRICE = sereServs != null && sereServs.Count > 0 ?
                        sereServs.Sum(o => o.VIR_TOTAL_PRICE) : 0;

                    //Tiền thuốc
                    transaction.TREATMENT_MEDICINE_PRICE = sereServs != null && sereServs.Count > 0 ?
                        sereServs.Where(o => o.MEDICINE_ID.HasValue).Sum(o => o.VIR_TOTAL_PRICE) : 0;

                    //Tiền Vật tư
                    transaction.TREATMENT_MATERIAL_PRICE = sereServs != null && sereServs.Count > 0 ?
                        sereServs.Where(o => o.MATERIAL_ID.HasValue).Sum(o => o.VIR_TOTAL_PRICE) : 0;

                    //Tiền mau
                    transaction.TREATMENT_BLOOD_PRICE = sereServs != null && sereServs.Count > 0 ?
                        sereServs.Where(o => o.BLOOD_ID.HasValue).Sum(o => o.VIR_TOTAL_PRICE) : 0;

                    //Tiền CLS (CĐHA, TDCN, Xét nghiệm, noi soi, sieu am, giai phau benh ly)
                    transaction.TREATMENT_SUBCLINICAL_PRICE = sereServs != null && sereServs.Count > 0 ?
                        sereServs.Where(o => HisServiceTypeCFG.SUBCLINICAL_TYPE_IDs.Contains(o.TDL_SERVICE_TYPE_ID))
                        .Sum(o => o.VIR_TOTAL_PRICE) : 0;

                    //Tiền PTTT
                    transaction.TREATMENT_SURG_PRICE = sereServs != null && sereServs.Count > 0 ?
                        sereServs.Where(o => o.TDL_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__PT
                            || o.TDL_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__TT)
                            .Sum(o => o.VIR_TOTAL_PRICE) : 0;

                    //Tiền khám bệnh
                    transaction.TREATMENT_EXAM_PRICE = sereServs != null && sereServs.Count > 0 ?
                        sereServs.Where(o => o.TDL_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__KH)
                            .Sum(o => o.VIR_TOTAL_PRICE) : 0;

                    //Tiền giường
                    transaction.TREATMENT_BED_PRICE = sereServs != null && sereServs.Count > 0 ?
                        sereServs.Where(o => o.TDL_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__G)
                            .Sum(o => o.VIR_TOTAL_PRICE) : 0;

                    //Tiền BH đồng chi trả
                    transaction.TREATMENT_HEIN_PRICE = sereServs != null && sereServs.Count > 0 ?
                        sereServs.Sum(o => o.VIR_TOTAL_HEIN_PRICE) : 0;

                    //Tiền BN đồng chi trả
                    transaction.TREATMENT_PATIENT_PRICE = sereServs != null && sereServs.Count > 0 ?
                        sereServs.Sum(o => o.VIR_TOTAL_PATIENT_PRICE) : 0;

                    //Tiền tạm ứng
                    transaction.TREATMENT_DEPOSIT_AMOUNT = existTransations != null && existTransations.Count > 0 ?
                        existTransations.Where(o => o.TRANSACTION_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TU && !o.IS_CANCEL.HasValue).Sum(o => o.AMOUNT) : 0;

                    //Tiền hoàn ứng
                    transaction.TREATMENT_REPAY_AMOUNT = existTransations != null && existTransations.Count > 0 ?
                        existTransations.Where(o => o.TRANSACTION_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__HU && !o.IS_CANCEL.HasValue).Sum(o => o.AMOUNT) : 0;

                    //Tiền BN đã thanh toán
                    transaction.TREATMENT_BILL_AMOUNT = existTransations != null && existTransations.Count > 0 ?
                        existTransations.Where(o => o.TRANSACTION_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TT && !o.IS_CANCEL.HasValue).Sum(o => o.AMOUNT) : 0;

                    //Tiền đã chốt nợ
                    transaction.TREATMENT_DEBT_AMOUNT = existTransations != null && existTransations.Count > 0 ?
                        existTransations.Where(o => o.TRANSACTION_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__NO && !o.IS_CANCEL.HasValue).Sum(o => o.AMOUNT) : 0;

                    //Tiền đã kết chuyển
                    transaction.TREATMENT_TRANSFER_AMOUNT = existTransations != null && existTransations.Count > 0 ?
                        existTransations.Where(o => o.TRANSACTION_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TT && !o.IS_CANCEL.HasValue).Sum(o => (o.KC_AMOUNT ?? 0)) : 0;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        /// <summary>
        /// Luu thong tin chi tiet ho so vien phi vao thong tin giao dich .
        /// Danh cho thanh toan hai so
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="source"></param>
        public static void SetTreatmentFeeInfo(HIS_TRANSACTION transaction, HIS_TRANSACTION source)
        {
            try
            {
                if (HisTransactionCFG.SET_TREATMENT_FEE_INFO && transaction != null && source != null)
                {
                    //Tổng chi phí
                    transaction.TREATMENT_TOTAL_PRICE = source.TREATMENT_TOTAL_PRICE;
                    //Tiền thuốc
                    transaction.TREATMENT_MEDICINE_PRICE = source.TREATMENT_MEDICINE_PRICE;
                    //Tiền Vật tư
                    transaction.TREATMENT_MATERIAL_PRICE = source.TREATMENT_MATERIAL_PRICE;
                    //Tiền mau
                    transaction.TREATMENT_BLOOD_PRICE = source.TREATMENT_BLOOD_PRICE;
                    //Tiền CLS (CĐHA, TDCN, Xét nghiệm, noi soi, sieu am, giai phau benh ly)
                    transaction.TREATMENT_SUBCLINICAL_PRICE = source.TREATMENT_SUBCLINICAL_PRICE;
                    //Tiền PTTT
                    transaction.TREATMENT_SURG_PRICE = source.TREATMENT_SURG_PRICE;
                    //Tiền khám bệnh
                    transaction.TREATMENT_EXAM_PRICE = source.TREATMENT_EXAM_PRICE;
                    //Tiền giường
                    transaction.TREATMENT_BED_PRICE = source.TREATMENT_BED_PRICE;
                    //Tiền BH đồng chi trả
                    transaction.TREATMENT_HEIN_PRICE = source.TREATMENT_HEIN_PRICE;
                    //Tiền BN đồng chi trả
                    transaction.TREATMENT_PATIENT_PRICE = source.TREATMENT_PATIENT_PRICE;
                    //Tiền tạm ứng
                    transaction.TREATMENT_DEPOSIT_AMOUNT = source.TREATMENT_DEPOSIT_AMOUNT;
                    //Tiền hoàn ứng
                    transaction.TREATMENT_REPAY_AMOUNT = source.TREATMENT_REPAY_AMOUNT;
                    //Tiền BN đã thanh toán
                    transaction.TREATMENT_BILL_AMOUNT = source.TREATMENT_BILL_AMOUNT;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }
        /// <summary>
        /// Luu du thua du lieu
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="patient"></param>
        public static void SetTdl(HIS_TRANSACTION transaction, HIS_PATIENT patient)
        {
            if (transaction != null && patient != null)
            {
                //lay du lieu danh muc
                HIS_CAREER career = HisCareerCFG.DATA != null && patient.CAREER_ID.HasValue ?
                    HisCareerCFG.DATA.Where(o => o.ID == patient.CAREER_ID.Value).FirstOrDefault() : null;
                HIS_GENDER gender = HisGenderCFG.DATA != null ?
                    HisGenderCFG.DATA.Where(o => o.ID == patient.GENDER_ID).FirstOrDefault() : null;
                HIS_MILITARY_RANK militaryRank = HisMilitaryRankCFG.DATA != null ?
                    HisMilitaryRankCFG.DATA.Where(o => patient.MILITARY_RANK_ID.HasValue && o.ID == patient.MILITARY_RANK_ID).FirstOrDefault() : null;
                HIS_WORK_PLACE workPlace = null;
                
                if (patient.WORK_PLACE_ID.HasValue)
                {
                    workPlace = new HisWorkPlaceGet().GetById(patient.WORK_PLACE_ID.Value);
                }
                    

                transaction.TDL_PATIENT_ADDRESS = patient.VIR_ADDRESS;
                transaction.TDL_PATIENT_NAME = patient.VIR_PATIENT_NAME;
                transaction.TDL_PATIENT_CAREER_NAME = career != null ? career.CAREER_NAME : null;
                transaction.TDL_PATIENT_CODE = patient.PATIENT_CODE;
                transaction.TDL_PATIENT_DISTRICT_CODE = patient.DISTRICT_CODE;
                transaction.TDL_PATIENT_DOB = patient.DOB;
                transaction.TDL_PATIENT_FIRST_NAME = patient.FIRST_NAME;
                transaction.TDL_PATIENT_GENDER_ID = patient.GENDER_ID;
                transaction.TDL_PATIENT_GENDER_NAME = gender.GENDER_NAME;
                transaction.TDL_PATIENT_IS_HAS_NOT_DAY_DOB = patient.IS_HAS_NOT_DAY_DOB;
                transaction.TDL_PATIENT_LAST_NAME = patient.LAST_NAME;
                transaction.TDL_PATIENT_MILITARY_RANK_NAME = militaryRank != null ? militaryRank.MILITARY_RANK_NAME : null;
                transaction.TDL_PATIENT_NATIONAL_NAME = patient.NATIONAL_NAME;
                transaction.TDL_PATIENT_PROVINCE_CODE = patient.PROVINCE_CODE;
                transaction.TDL_PATIENT_WORK_PLACE = patient.WORK_PLACE;
                transaction.TDL_PATIENT_WORK_PLACE_NAME = workPlace != null ? workPlace.WORK_PLACE_NAME : null;
            }
        }

        /// <summary>
        /// Luu du thua du lieu
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="treatment"></param>
        public static void SetTdl(HIS_TRANSACTION transaction, HIS_TREATMENT treatment)
        {
            if (transaction != null && treatment != null)
            {
                transaction.TDL_TREATMENT_CODE = treatment.TREATMENT_CODE;
                transaction.TDL_PATIENT_ID = treatment.PATIENT_ID;
                transaction.TDL_PATIENT_ADDRESS = treatment.TDL_PATIENT_ADDRESS;
                transaction.TDL_PATIENT_NAME = treatment.TDL_PATIENT_NAME;
                transaction.TDL_PATIENT_CAREER_NAME = treatment.TDL_PATIENT_CAREER_NAME;
                transaction.TDL_PATIENT_CODE = treatment.TDL_PATIENT_CODE;
                transaction.TDL_PATIENT_DISTRICT_CODE = treatment.TDL_PATIENT_DISTRICT_CODE;
                transaction.TDL_PATIENT_DOB = treatment.TDL_PATIENT_DOB;
                transaction.TDL_PATIENT_FIRST_NAME = treatment.TDL_PATIENT_FIRST_NAME;
                transaction.TDL_PATIENT_GENDER_ID = treatment.TDL_PATIENT_GENDER_ID;
                transaction.TDL_PATIENT_GENDER_NAME = treatment.TDL_PATIENT_GENDER_NAME;
                transaction.TDL_PATIENT_IS_HAS_NOT_DAY_DOB = treatment.TDL_PATIENT_IS_HAS_NOT_DAY_DOB;
                transaction.TDL_PATIENT_LAST_NAME = treatment.TDL_PATIENT_LAST_NAME;
                transaction.TDL_PATIENT_MILITARY_RANK_NAME = treatment.TDL_PATIENT_MILITARY_RANK_NAME;
                transaction.TDL_PATIENT_NATIONAL_NAME = treatment.TDL_PATIENT_NATIONAL_NAME;
                transaction.TDL_PATIENT_PROVINCE_CODE = treatment.TDL_PATIENT_PROVINCE_CODE;
                transaction.TDL_PATIENT_WORK_PLACE = treatment.TDL_PATIENT_WORK_PLACE;
                transaction.TDL_PATIENT_WORK_PLACE_NAME = treatment.TDL_PATIENT_WORK_PLACE_NAME;
                transaction.TREATMENT_TYPE_ID = treatment.TDL_TREATMENT_TYPE_ID;
                transaction.TDL_PATIENT_CLASSIFY_ID = treatment.TDL_PATIENT_CLASSIFY_ID; 
            }
        }

        /// <summary>
        /// Luu du thua du lieu
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="expMest"></param>
        public static void SetTdl(HIS_TRANSACTION transaction, HIS_EXP_MEST expMest)
        {
            if (transaction != null && expMest != null)
            {
                transaction.TDL_TREATMENT_CODE = expMest.TDL_TREATMENT_CODE;
                transaction.TDL_PATIENT_ID = expMest.TDL_PATIENT_ID;
                transaction.TDL_PATIENT_ADDRESS = expMest.TDL_PATIENT_ADDRESS;
                transaction.TDL_PATIENT_NAME = expMest.TDL_PATIENT_NAME;
                transaction.TDL_PATIENT_CODE = expMest.TDL_PATIENT_CODE;
                transaction.TDL_PATIENT_DOB = expMest.TDL_PATIENT_DOB;
                transaction.TDL_PATIENT_FIRST_NAME = expMest.TDL_PATIENT_FIRST_NAME;
                transaction.TDL_PATIENT_GENDER_ID = expMest.TDL_PATIENT_GENDER_ID;
                transaction.TDL_PATIENT_GENDER_NAME = expMest.TDL_PATIENT_GENDER_NAME;
                transaction.TDL_PATIENT_IS_HAS_NOT_DAY_DOB = expMest.TDL_PATIENT_IS_HAS_NOT_DAY_DOB;
                transaction.TDL_PATIENT_LAST_NAME = expMest.TDL_PATIENT_LAST_NAME;
            }
        }

        internal static string NVL(string input, string separator)
        {
            return !string.IsNullOrWhiteSpace(input) ? string.Format("{0}{1}", input, separator) : "";
        }

        internal static void AddProperty(ExpandoObject expando, string propertyName, object propertyValue)
        {
            var expandoDict = expando as IDictionary<string, object>;
            if (expandoDict.ContainsKey(propertyName))
                expandoDict[propertyName] = propertyValue;
            else
                expandoDict.Add(propertyName, propertyValue);
        }

        /// <summary>
        /// Luu du thua du lieu
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="vacc"></param>
        public static void SetTdl(HIS_TRANSACTION transaction, HIS_VACCINATION vacc)
        {
            if (transaction != null && vacc != null)
            {
                transaction.TDL_PATIENT_ID = vacc.PATIENT_ID;
                transaction.TDL_PATIENT_ADDRESS = vacc.TDL_PATIENT_ADDRESS;
                transaction.TDL_PATIENT_NAME = vacc.TDL_PATIENT_NAME;
                transaction.TDL_PATIENT_CODE = vacc.TDL_PATIENT_CODE;
                transaction.TDL_PATIENT_DOB = vacc.TDL_PATIENT_DOB;
                transaction.TDL_PATIENT_FIRST_NAME = vacc.TDL_PATIENT_FIRST_NAME;
                transaction.TDL_PATIENT_GENDER_ID = vacc.TDL_PATIENT_GENDER_ID;
                transaction.TDL_PATIENT_GENDER_NAME = vacc.TDL_PATIENT_GENDER_NAME;
                transaction.TDL_PATIENT_IS_HAS_NOT_DAY_DOB = vacc.TDL_PATIENT_IS_HAS_NOT_DAY_DOB;
                transaction.TDL_PATIENT_LAST_NAME = vacc.TDL_PATIENT_LAST_NAME;
            }
        }

        public static void SetTransactionInfo(HIS_TRANSACTION transaction, HIS_TREATMENT treatment, List<HIS_TRANSACTION> oldTransactions, List<HIS_SERE_SERV> sereServs)
        {
            if (HisTransactionCFG.SET_TREATMENT_FEE_INFO && transaction != null && treatment != null)
            {
                ExpandoObject infoObject = new ExpandoObject();
                if (sereServs != null && sereServs.Count > 0)
                {
                    //Tong tien BH chi tra cua cac dich vu duoc thanh toan trong giao dich hien tai
                    decimal totalHeinPrice = 0;

                    //Tong tien BN chi tra cua cac dich vu duoc thanh toan trong giao dich hien tai
                    decimal totalPatientPrice = 0;

                    //Tong tien BN cung chi tra cua cac dich vu duoc thanh toan trong giao dich hien tai
                    decimal totalPatientPriceBhyt = 0;

                    /// <summary>
                    /// Tong tien vien phi cua cac dich vu duoc thanh toan trong giao dich hien tai
                    /// 1. DTTT VP - DTPT Khong co
                    /// 2. DTTT VP - DTPT Co: Lay chenh lech
                    /// 2. DTT BHYT - DTPT VP: Lay chenh lech
                    /// </summary>
                    decimal totalPatientPriceFee = 0;

                    /// <summary>
                    /// Tong tien khac cua cac dich vu duoc thanh toan trong giao dich hien tai
                    /// 1. DTTT khong phai la BHYT va VP
                    /// 2. DTTT BHYT, VP - DTPT Khong phai la BHYT va VP: Lay chenh lech
                    /// </summary>
                    decimal totalPatientPriceOther = 0;

                    /// <summary>
                    /// Tong tien BN tu tra cua cac dich vu duoc thanh toan trong giao dich hien tai
                    /// DTTT BHYT va co Tran
                    /// </summary>
                    decimal totalPatientPriceDiff = 0;

                    foreach (HIS_SERE_SERV sere in sereServs)
                    {
                        totalPatientPrice += (sere.VIR_TOTAL_PATIENT_PRICE ?? 0);
                        if (sere.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT)
                        {
                            totalHeinPrice += (sere.VIR_TOTAL_HEIN_PRICE ?? 0);
                            if (!sere.PRIMARY_PATIENT_TYPE_ID.HasValue)
                            {
                                if ((sere.VIR_TOTAL_PATIENT_PRICE ?? 0) > (sere.VIR_TOTAL_PATIENT_PRICE_BHYT ?? 0))
                                {
                                    totalPatientPriceBhyt += (sere.VIR_TOTAL_PATIENT_PRICE_BHYT ?? 0);
                                    totalPatientPriceDiff += (sere.VIR_TOTAL_PATIENT_PRICE ?? 0) - (sere.VIR_TOTAL_PATIENT_PRICE_BHYT ?? 0);
                                }
                                else
                                {
                                    totalPatientPriceBhyt += (sere.VIR_TOTAL_PATIENT_PRICE ?? 0);
                                }
                            }
                            else if (sere.PRIMARY_PATIENT_TYPE_ID.Value == HisPatientTypeCFG.PATIENT_TYPE_ID__HOSPITAL_FEE)
                            {
                                if ((sere.VIR_TOTAL_PATIENT_PRICE ?? 0) > (sere.VIR_TOTAL_PATIENT_PRICE_BHYT ?? 0))
                                {
                                    decimal diffPrice = (sere.VIR_TOTAL_PATIENT_PRICE ?? 0) - (sere.VIR_TOTAL_PATIENT_PRICE_BHYT ?? 0);
                                    decimal limitPrice = (sere.LIMIT_PRICE ?? 0) * (1 + sere.VAT_RATIO) * sere.AMOUNT;
                                    decimal diffLimit = limitPrice - (sere.VIR_TOTAL_PATIENT_PRICE_BHYT ?? 0);
                                    if (diffLimit <= 0)
                                    {
                                        totalPatientPriceFee += diffPrice;
                                    }
                                    else if (diffLimit < diffPrice)
                                    {
                                        totalPatientPriceDiff += diffLimit;
                                        totalPatientPriceFee += (diffPrice - diffLimit);
                                    }
                                    else
                                    {
                                        totalPatientPriceDiff += diffPrice;
                                    }
                                }
                            }
                            else
                            {
                                if ((sere.VIR_TOTAL_PATIENT_PRICE ?? 0) > (sere.VIR_TOTAL_PATIENT_PRICE_BHYT ?? 0))
                                {
                                    decimal diffPrice = (sere.VIR_TOTAL_PATIENT_PRICE ?? 0) - (sere.VIR_TOTAL_PATIENT_PRICE_BHYT ?? 0);
                                    decimal limitPrice = (sere.LIMIT_PRICE ?? 0) * (1 + sere.VAT_RATIO) * sere.AMOUNT;
                                    decimal diffLimit = limitPrice - (sere.VIR_TOTAL_PATIENT_PRICE_BHYT ?? 0);
                                    if (diffLimit <= 0)
                                    {
                                        totalPatientPriceOther += diffPrice;
                                    }
                                    else if (diffLimit < diffPrice)
                                    {
                                        totalPatientPriceDiff += diffLimit;
                                        totalPatientPriceOther += diffPrice - diffLimit;
                                    }
                                    else
                                    {
                                        totalPatientPriceDiff += diffPrice;
                                    }
                                }
                            }
                        }
                        else if (sere.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__HOSPITAL_FEE)
                        {
                            if (!sere.PRIMARY_PATIENT_TYPE_ID.HasValue)
                            {
                                totalPatientPriceFee += (sere.VIR_TOTAL_PATIENT_PRICE ?? 0);
                            }
                            else
                            {
                                decimal limitPrice = (sere.LIMIT_PRICE ?? 0) * (1 + sere.VAT_RATIO) * sere.AMOUNT;
                                if (limitPrice >= (sere.VIR_TOTAL_PATIENT_PRICE ?? 0))
                                {
                                    totalPatientPriceFee += (sere.VIR_TOTAL_PATIENT_PRICE ?? 0);
                                }
                                else
                                {
                                    totalPatientPriceFee += limitPrice;
                                    totalPatientPriceOther += ((sere.VIR_TOTAL_PATIENT_PRICE ?? 0) - limitPrice);
                                }
                            }
                        }
                        else
                        {
                            totalPatientPriceOther += (sere.VIR_TOTAL_PATIENT_PRICE ?? 0);
                        }
                    }

                    if (totalHeinPrice > 0)
                    {
                        AddProperty(infoObject, HisTransactionConstant.TOTAL_HEIN_PRICE, totalHeinPrice);
                    }
                    if (totalPatientPrice > 0)
                    {
                        AddProperty(infoObject, HisTransactionConstant.TOTAL_PATIENT_PRICE, totalPatientPrice);
                    }
                    if (totalPatientPriceBhyt > 0)
                    {
                        AddProperty(infoObject, HisTransactionConstant.TOTAL_PATIENT_PRICE_BHYT, totalPatientPriceBhyt);
                    }
                    if (totalPatientPriceFee > 0)
                    {
                        AddProperty(infoObject, HisTransactionConstant.TOTAL_PATIENT_PRICE_FEE, totalPatientPriceFee);
                    }
                    if (totalPatientPriceOther > 0)
                    {
                        AddProperty(infoObject, HisTransactionConstant.TOTAL_PATIENT_PRICE_OTHER, totalPatientPriceOther);
                    }
                    if (totalPatientPriceDiff > 0)
                    {
                        AddProperty(infoObject, HisTransactionConstant.TOTAL_PATIENT_PRICE_DIFF, totalPatientPriceDiff);
                    }
                }

                if (oldTransactions != null && oldTransactions.Count > 0)
                {
                    decimal totalPreAmountOut = 0;
                    decimal totalPreHeinPrice = 0;
                    decimal totalPreHeinPriceOut = 0;

                    foreach (HIS_TRANSACTION tran in oldTransactions)
                    {
                        if (tran.IS_CANCEL == Constant.IS_TRUE || tran.TRANSACTION_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TT)
                            continue;
                        TransactionInfoSDO info = null;
                        if (!String.IsNullOrWhiteSpace(tran.TRANSACTION_INFO))
                        {
                            try
                            {
                                info = Newtonsoft.Json.JsonConvert.DeserializeObject<TransactionInfoSDO>(tran.TRANSACTION_INFO);
                            }
                            catch (Exception ex)
                            {
                                Inventec.Common.Logging.LogSystem.Error(ex);
                            }
                        }
                        if (info != null)
                        {
                            totalPreHeinPrice += (info.TOTAL_HEIN_PRICE ?? 0);
                        }
                        if (tran.TREATMENT_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU
                            && treatment.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU)
                        {
                            totalPreAmountOut += (tran.SERE_SERV_AMOUNT ?? 0);
                            if (info != null)
                            {
                                totalPreHeinPriceOut += (info.TOTAL_HEIN_PRICE ?? 0);
                            }
                        }
                    }

                    if (totalPreAmountOut > 0)
                    {
                        AddProperty(infoObject, HisTransactionConstant.TOTAL_PRE_BILL_AMOUNT_OUT, totalPreAmountOut);
                    }
                    if (totalPreHeinPrice > 0)
                    {
                        AddProperty(infoObject, HisTransactionConstant.TOTAL_PRE_HEIN_PRICE, totalPreHeinPrice);
                    }
                    if (totalPreHeinPriceOut > 0)
                    {
                        AddProperty(infoObject, HisTransactionConstant.TOTAL_PRE_HEIN_PRICE_OUT, totalPreHeinPriceOut);
                    }
                }

                transaction.TRANSACTION_INFO = Newtonsoft.Json.JsonConvert.SerializeObject(infoObject);
            }
        }

        public static void SetEpaymentInfo(HIS_TRANSACTION transaction, HIS_CARD card, string yttTransactionCode, long? yttTransactionTime)
        {
            transaction.TIG_TRANSACTION_CODE = yttTransactionCode;
            transaction.TIG_TRANSACTION_TIME = yttTransactionTime;

            SetCardInfo(transaction, card);
        }

        public static void SetCardInfo(HIS_TRANSACTION transaction, HIS_CARD card)
        {
            if (card != null)
            {
                transaction.CARD_ID = card.ID;
                transaction.TDL_CARD_CODE = card.CARD_CODE;
                transaction.TDL_BANK_CARD_CODE = card.BANK_CARD_CODE;
            }
        }
    }
}
