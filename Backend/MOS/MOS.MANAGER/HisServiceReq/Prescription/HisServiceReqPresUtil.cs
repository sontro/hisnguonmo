using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.Config.CFG;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisServiceReq.Common;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Prescription
{
    class HisServiceReqPresUtil : BusinessBase
    {
        internal HisServiceReqPresUtil()
            : base()
        {
        }

        internal HisServiceReqPresUtil(CommonParam param)
            : base(param)
        {
        }

        /// <summary>
        /// Xu ly nghiep vu tu dong chuyen doi tuong thanh toan tu BHYT sang vien phi neu thuoc duoc ke da qua han thau
        /// </summary>
        /// <param name="expMestMedicines"></param>
        public bool ProcessAutoChangeBhytToHospitalFee(List<V_HIS_MEDICINE_2> choosenMedicines, List<HIS_MEDICINE_PATY> medicinePaties, List<HIS_EXP_MEST_MEDICINE> expMestMedicines, HIS_EXP_MEST expMest)
        {
            return this.ProcessAutoChangeBhytToHospitalFee(choosenMedicines, medicinePaties, expMestMedicines, new List<HIS_EXP_MEST>() { expMest });
        }

        /// <summary>
        /// Xu ly nghiep vu tu dong chuyen doi tuong thanh toan tu BHYT sang vien phi neu thuoc duoc ke da qua han thau
        /// </summary>
        /// <param name="expMestMedicines"></param>
        public bool ProcessAutoChangeBhytToHospitalFee(List<V_HIS_MEDICINE_2> choosenMedicines, List<HIS_MEDICINE_PATY> medicinePaties, List<HIS_EXP_MEST_MEDICINE> expMestMedicines, List<HIS_EXP_MEST> expMests)
        {
            if (HisServiceReqCFG.AUTO_CHANGE_BHYT_TO_HOSPITAL_FEE_IF_BID_EXPIRED_DATE_IS_EXCEEDED && IsNotNullOrEmpty(expMestMedicines))
            {
                List<HIS_EXP_MEST_MEDICINE> bhyts = expMestMedicines.Where(o => o.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT).ToList();

                if (IsNotNullOrEmpty(bhyts))
                {
                    foreach (HIS_EXP_MEST_MEDICINE exp in bhyts)
                    {
                        V_HIS_MEDICINE_2 medicine = choosenMedicines.Where(o => o.ID == exp.MEDICINE_ID).FirstOrDefault();
                        HIS_EXP_MEST expMest = expMests.Where(o => exp.EXP_MEST_ID.HasValue && exp.EXP_MEST_ID == o.ID).FirstOrDefault();

                        //Neu thuoc duoc ke co thong tin "han thau"
                        if (medicine.VALID_TO_TIME.HasValue && expMest != null && expMest.TDL_INTRUCTION_TIME.HasValue)
                        {
                            DateTime validToTime = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(medicine.VALID_TO_TIME.Value).Value;
                            DateTime instructionTime = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(expMest.TDL_INTRUCTION_TIME.Value).Value;

                            //Neu "han thau" da qua so ngay duoc thiet lap thi kiem tra xem thuoc do cho phep ban voi doi tuong vien phi ko
                            //- Neu co thi tu dong doi qua doi tuong vien phi va co tra ve thong bao
                            //- Neu khong thi hien thi thong bao, ket thuc xu ly (tra ve that bai)
                            if (validToTime.AddDays(HisServiceReqCFG.AUTO_CHANGE_BHYT_TO_HOSPITAL_FEE_IF_BID_EXPIRED_DATE_IS_EXCEEDED__NUMBER_OF_DAY) < instructionTime)
                            {
                                string validTimeStr = Inventec.Common.DateTime.Convert.SystemDateTimeToDateString(validToTime);

                                if (medicine.IS_SALE_EQUAL_IMP_PRICE == Constant.IS_TRUE || (IsNotNullOrEmpty(medicinePaties) && medicinePaties.Exists(t => t.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__HOSPITAL_FEE && t.MEDICINE_ID == medicine.ID)))
                                {
                                    exp.PATIENT_TYPE_ID = HisPatientTypeCFG.PATIENT_TYPE_ID__HOSPITAL_FEE;
                                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_LoThuocDaQuaHanThauHeThongTuDongChuyenDoiSangVienPhi, medicine.MEDICINE_TYPE_NAME, HisServiceReqCFG.AUTO_CHANGE_BHYT_TO_HOSPITAL_FEE_IF_BID_EXPIRED_DATE_IS_EXCEEDED__NUMBER_OF_DAY.ToString(), validTimeStr);
                                }
                                else
                                {
                                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_LoThuocDaQuaHanThauKhongChoPhepKeBhyt, medicine.MEDICINE_TYPE_NAME, HisServiceReqCFG.AUTO_CHANGE_BHYT_TO_HOSPITAL_FEE_IF_BID_EXPIRED_DATE_IS_EXCEEDED__NUMBER_OF_DAY.ToString(), validTimeStr);
                                    return false;
                                }
                            }
                        }
                    }
                }
            }
            return true;
        }

        public static void SqlUpdateExpMest(List<HIS_EXP_MEST> expMests, List<HIS_EXP_MEST_MATERIAL> expMestMaterials, List<HIS_EXP_MEST_MEDICINE> expMestMedicines, ref List<string> sqls)
        {
            try
            {
                if (expMests == null)
                {
                    return;
                }
                foreach (HIS_EXP_MEST exp in expMests)
                {
                    List<HIS_EXP_MEST_MATERIAL> lstMaterial = expMestMaterials != null ? expMestMaterials.Where(o => o.EXP_MEST_ID == exp.ID).ToList() : null;
                    List<HIS_EXP_MEST_MEDICINE> lstMedicine = expMestMedicines != null ? expMestMedicines.Where(o => o.EXP_MEST_ID == exp.ID).ToList() : null;

                    decimal totalPrice = 0;

                    totalPrice += lstMaterial != null && lstMaterial.Count > 0 ? lstMaterial.Sum(o => o.AMOUNT * (o.PRICE ?? 0) * (1 + (o.VAT_RATIO ?? 0))) : 0;
                    totalPrice += lstMedicine != null && lstMedicine.Count > 0 ? lstMedicine.Sum(o => o.AMOUNT * (o.PRICE ?? 0) * (1 + (o.VAT_RATIO ?? 0))) : 0;
                    string hasNotPresStr = (lstMaterial != null && lstMaterial.Exists(o => o.IS_NOT_PRES == Constant.IS_TRUE)) || (lstMedicine != null && lstMedicine.Exists(o => o.IS_NOT_PRES == Constant.IS_TRUE)) ? Constant.IS_TRUE.ToString() : "NULL";

                    string isUsingApprovalRequired = HisMedicineTypeAcinCFG.APPROVAL_REQUIRED != null
                        && lstMedicine != null
                        && lstMedicine.Exists(t => t.TDL_MEDICINE_TYPE_ID.HasValue && HisMedicineTypeAcinCFG.APPROVAL_REQUIRED_MEDICINE_TYPE_IDS.Contains(t.TDL_MEDICINE_TYPE_ID.Value)) ? "1" : "NULL";

                    string updateSql = string.Format("UPDATE HIS_EXP_MEST SET TDL_TOTAL_PRICE = {0}, HAS_NOT_PRES = {1}, IS_USING_APPROVAL_REQUIRED = {2} WHERE ID = {3}", totalPrice.ToString("G27", CultureInfo.InvariantCulture), hasNotPresStr, isUsingApprovalRequired, exp.ID);
                    sqls.Add(updateSql);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        public static void SqlUpdateExpMest(HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MATERIAL> expMestMaterials, List<HIS_EXP_MEST_MEDICINE> expMestMedicines, ref List<string> sqls)
        {
            if (expMest != null)
            {
                SqlUpdateExpMest(new List<HIS_EXP_MEST>() { expMest }, expMestMaterials, expMestMedicines, ref sqls);
            }
        }

        public void InitThreadUpdateHisExpMest(List<HIS_EXP_MEST> expMests, List<HIS_EXP_MEST_MEDICINE> expMestMedicines, List<HIS_EXP_MEST_MATERIAL> expMestMaterials)
        {
            try
            {
                Thread thread = new Thread(new ParameterizedThreadStart(UpdateHisExpMest));
                thread.Priority = ThreadPriority.Lowest;
                UpdateHisExpMestThreadData threadData = new UpdateHisExpMestThreadData();
                threadData.expMests = expMests;
                threadData.expMestMedicines = expMestMedicines;
                threadData.expMestMaterials = expMestMaterials;
                thread.Start(threadData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void UpdateHisExpMest(object data)
        {
            try
            {
                UpdateHisExpMestThreadData threadData = (UpdateHisExpMestThreadData)data;
                List<HIS_EXP_MEST> expMests = threadData.expMests;
                List<HIS_EXP_MEST_MEDICINE> expMestMedicines = threadData.expMestMedicines;
                List<HIS_EXP_MEST_MATERIAL> expMestMaterials = threadData.expMestMaterials;
                if (expMests == null)
                    return;
                List<HIS_EXP_MEST> expMests_Get = new HisExpMestGet().GetByIds(expMests.Select(o => o.ID).ToList()) ?? new List<HIS_EXP_MEST>();
                List<string> sqls = new List<string>();
                foreach (HIS_EXP_MEST exp in expMests)
                {
                    HIS_EXP_MEST exp_get = expMests_Get.Where(o => o.ID == exp.ID).FirstOrDefault();
                    if (exp_get != null && exp.AGGR_EXP_MEST_ID != exp_get.AGGR_EXP_MEST_ID)
                    {
                        List<HIS_EXP_MEST_MEDICINE> lstMedicine = expMestMedicines != null ? expMestMedicines.Where(o => o.EXP_MEST_ID == exp.ID).ToList() : null;
                        string lstMedicineIds = lstMedicine != null ? String.Join(", ", lstMedicine.Select(o => o.ID)) : null;
                        List<HIS_EXP_MEST_MATERIAL> lstMaterial = expMestMaterials != null ? expMestMaterials.Where(o => o.EXP_MEST_ID == exp.ID).ToList() : null;
                        string lstMaterialIds = lstMaterial != null ? String.Join(", ", lstMaterial.Select(o => o.ID)) : null;

                        if (IsNotNullOrEmpty(lstMedicineIds))
                            sqls.Add(string.Format("UPDATE HIS_EXP_MEST_MEDICINE SET TDL_AGGR_EXP_MEST_ID = {0} WHERE ID IN ({1})", exp_get.AGGR_EXP_MEST_ID, lstMedicineIds));
                        if (IsNotNullOrEmpty(lstMaterialIds))
                            sqls.Add(string.Format("UPDATE HIS_EXP_MEST_MATERIAL SET TDL_AGGR_EXP_MEST_ID = {0} WHERE ID IN ({1})", exp_get.AGGR_EXP_MEST_ID, lstMaterialIds));
                    }
                }
                if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                {
                    throw new Exception("sql update failed!");
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error("Khoi tao tien trinh cap nhat thong tin du thua du lieu phieu linh(TDL_AGGR_EXP_MEST_ID)", ex);
            }
        }
    }
}
