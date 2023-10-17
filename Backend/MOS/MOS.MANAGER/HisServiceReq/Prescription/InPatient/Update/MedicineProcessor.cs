using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisMedicine;
using MOS.MANAGER.HisMedicineBean;
using MOS.MANAGER.HisMedicineBean.Handle;
using MOS.MANAGER.HisSereServ.Update;
using MOS.MANAGER.HisSereServ.Update.Package;
using MOS.SDO;
using MOS.UTILITY;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Prescription.InPatient.Update
{
    class MedicineProcessor : BusinessBase
    {
        private List<HisMedicineBeanSplit> beanSpliters;
        private HisExpMestMedicineCreate hisExpMestMedicineCreate;
        private HisExpMestMedicineUpdate hisExpMestMedicineUpdate;

        internal MedicineProcessor(CommonParam paramUpdate)
            : base(paramUpdate)
        {
            this.hisExpMestMedicineCreate = new HisExpMestMedicineCreate(param);
            this.beanSpliters = new List<HisMedicineBeanSplit>();
            this.hisExpMestMedicineUpdate = new HisExpMestMedicineUpdate(param);
        }

        internal bool Run(List<HIS_EXP_MEST_MEDICINE> olds, HisSereServPackage37 processPackage37, HisSereServPackageBirth processPackageBirth, HisSereServPackagePttm processPackagePttm, List<PresMedicineSDO> medicines, HIS_EXP_MEST expMest, HIS_TREATMENT treatment,long instructionTime, long? useTime, ref List<HIS_EXP_MEST_MEDICINE> refInserts, ref List<HIS_EXP_MEST_MEDICINE> refDeletes, ref List<HIS_EXP_MEST_MEDICINE> resultData, ref List<V_HIS_MEDICINE_2> newsMedicines, ref List<string> sqls)
        {
            try
            {
                if (expMest != null)
                {
                    List<HIS_EXP_MEST_MEDICINE> updates = new List<HIS_EXP_MEST_MEDICINE>();
                    List<HIS_EXP_MEST_MEDICINE> beforeUpdates = new List<HIS_EXP_MEST_MEDICINE>();
                    Dictionary<HIS_EXP_MEST_MEDICINE, List<long>> newMedicineDic = new Dictionary<HIS_EXP_MEST_MEDICINE, List<long>>();
                    List<HIS_MEDICINE_PATY> medicinePaties = null;

                    //Danh sach exp_mest_medicine
                    List<HIS_EXP_MEST_MEDICINE> news = this.MakeData(olds, medicines, instructionTime, useTime, expMest, treatment, ref newMedicineDic, ref medicinePaties);

                    if (IsNotNullOrEmpty(news))
                    {
                        List<long> medicineIds = news.Select(o => o.MEDICINE_ID.Value).ToList();
                        newsMedicines = new HisMedicineGet().GetView2ByIds(medicineIds);

                        if (!new HisServiceReqPresCheck(param).IsValidMedicineWithBidDate(newsMedicines, news, new List<HIS_EXP_MEST> { expMest }))
                        {
                            throw new Exception("IsValidMedicineWithBidDate false. Rollback du lieu");
                        }

                        if (IsNotNullOrEmpty(news) && !new HisServiceReqPresUtil(param).ProcessAutoChangeBhytToHospitalFee(newsMedicines, medicinePaties, news, expMest))
                        {
                            throw new Exception("Rollback du lieu");
                        }

                        //Xu ly de ap dung chinh sach gia 3 ngay 7 ngay
                        processPackage37.Apply3Day7Day(null, null, news, instructionTime);
                        //Xu ly de ap dung goi de
                        processPackageBirth.Run(news, news[0].SERE_SERV_PARENT_ID);
                        //Xu ly de ap dung goi phau thuat tham my
                        processPackagePttm.Run(news, news[0].SERE_SERV_PARENT_ID, instructionTime);
                    }

                    List<long> expMestMedicineIds = IsNotNullOrEmpty(olds) ? olds.Select(o => o.ID).ToList() : null;
                    List<HIS_MEDICINE_BEAN> oldBeans = new HisMedicineBeanGet().GetByExpMestMedicineIds(expMestMedicineIds);

                    List<HIS_EXP_MEST_MEDICINE> inserts = new List<HIS_EXP_MEST_MEDICINE>();
                    List<HIS_EXP_MEST_MEDICINE> deletes = new List<HIS_EXP_MEST_MEDICINE>();

                    this.GetDiff(olds, news, newMedicineDic, oldBeans, ref inserts, ref deletes, ref updates, ref beforeUpdates);

                    if (IsNotNullOrEmpty(inserts) && !this.hisExpMestMedicineCreate.CreateList(inserts))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }

                    if (IsNotNullOrEmpty(updates) && !this.hisExpMestMedicineUpdate.UpdateList(updates, beforeUpdates))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }

                    if (sqls == null)
                    {
                        sqls = new List<string>();
                    }

                    List<long> deleteIds = IsNotNullOrEmpty(deletes) ? deletes.Select(o => o.ID).ToList() : null;

                    //Cap nhat thong tin bean
                    this.SqlUpdateBean(newMedicineDic, deleteIds, ref sqls);

                    //Xoa cac exp_mest_medicine ko dung.
                    //Luu y: can thuc hien xoa exp_mest_medicine sau khi da cap nhat bean (tranh bi loi fk)
                    this.SqlDeleteExpMestMedicine(deleteIds, ref sqls);

                    this.PassResult(olds, inserts, updates, deletes, ref refInserts, ref refDeletes, ref resultData);
                }
                return true;
            }
            catch (Exception ex)
            {
                this.Rollback();
                LogSystem.Error(ex);
                return false;
            }
        }

        private List<HIS_EXP_MEST_MEDICINE> MakeData(List<HIS_EXP_MEST_MEDICINE> olds, List<PresMedicineSDO> medicines, long instructionTime, long? useTime, HIS_EXP_MEST expMest, HIS_TREATMENT treatment, ref Dictionary<HIS_EXP_MEST_MEDICINE, List<long>> medicineDic, ref List<HIS_MEDICINE_PATY> medicinePaties)
        {
            List<ReqMedicineData> reqData = this.MakeReqMedicineData(medicines, expMest, instructionTime);

            List<HIS_EXP_MEST_MEDICINE> expMestMedicines = new List<HIS_EXP_MEST_MEDICINE>();

            //Dictionary luu exp_mest_medicine va d/s bean tuong ung
            medicineDic = new Dictionary<HIS_EXP_MEST_MEDICINE, List<long>>();

            //- Duyet danh sach yeu cau de tao ra cac lenh tach bean. Can thoa man:
            //  + 2 req co loai thuoc giong nhau thi can thuoc 2 lenh tach bean khac nhau
            //  + 2 req co medi_stock_id khac nhau thi can thuoc 2 lenh tach bean khac nhau (cai nay ko
            // can xu ly vi sua don thuoc chi cho phep ke 1 kho)
            //- Sau khi thuc hien tach bean, thuc hien tao exp_mest_medicine tuong ung
            List<long> useBeandIds = new List<long>();
            bool byMedicine = IsNotNullOrEmpty(reqData) && reqData.Exists(t => t.MedicineId.HasValue);

            if (IsNotNullOrEmpty(reqData))
            {
                var groups = reqData.GroupBy(o => o.IsNotPres);

                foreach (var group in groups)
                {
                    List<ReqMedicineData> lst = group.ToList();

                    while (lst != null && lst.Count > 0)
                    {
                        List<ReqMedicineData> toSplits = new List<ReqMedicineData>();
                        foreach (ReqMedicineData s in lst)
                        {
                            if (!byMedicine && !toSplits.Exists(t => t.MedicineTypeId == s.MedicineTypeId))
                            {
                                toSplits.Add(s);
                            }
                            else if ((byMedicine && !toSplits.Exists(t => t.MedicineId == s.MedicineId)))
                            {
                                toSplits.Add(s);
                            }
                        }
                        lst.RemoveAll(o => toSplits.Exists(t => t.Id == o.Id));

                        //Thuc hien lenh tach bean
                        List<HIS_EXP_MEST_MEDICINE> data = this.SplitBeanAndMakeData(byMedicine, olds, toSplits, expMest, treatment, instructionTime, useTime, useBeandIds, ref medicineDic, ref medicinePaties);
                        if (IsNotNullOrEmpty(data))
                        {
                            expMestMedicines.AddRange(data);
                        }
                    }
                }
            }

            return expMestMedicines;
        }

        /// <summary>
        /// Tach bean theo ReqMedicineData va tao ra exp_mest_medicine tuong ung
        /// </summary>
        /// <param name="toSplits">Danh sach ReqMedicineData dam bao ko co medicine_type_id nao trung nhau va cung thuoc 1 medi_stock_id</param>
        /// <param name="expMest"></param>
        /// <returns></returns>
        private List<HIS_EXP_MEST_MEDICINE> SplitBeanAndMakeData(bool byMedicine, List<HIS_EXP_MEST_MEDICINE> olds, List<ReqMedicineData> toSplits,HIS_EXP_MEST expMest, HIS_TREATMENT treatment, long instructionTime, long? useTime, List<long> useBeandIds, ref Dictionary<HIS_EXP_MEST_MEDICINE, List<long>> medicineDic, ref List<HIS_MEDICINE_PATY> medicinePaties)
        {
            List<HIS_MEDICINE_BEAN> medicineBeans = null;

            if (!byMedicine)
            {
                List<ExpMedicineTypeSDO> reqSplits = new List<ExpMedicineTypeSDO>();
                foreach (ReqMedicineData req in toSplits)
                {
                    short? isExpend = req.IsExpend ? (short?)Constant.IS_TRUE : null;
                    short? isOutParentFee = req.IsOutParentFee ? (short?)Constant.IS_TRUE : null;

                    List<long> expMedicineIds = olds != null ? olds
                        .Where(o => o.TDL_MEDICINE_TYPE_ID == req.MedicineTypeId
                            && o.PATIENT_TYPE_ID == req.PatientTypeId
                            && o.SERE_SERV_PARENT_ID == req.SereServParentId
                            && o.IS_EXPEND == isExpend
                            && o.IS_EXPEND == req.ServiceConditionId
                            && o.OTHER_PAY_SOURCE_ID == req.OtherPaySourceId
                            && o.IS_OUT_PARENT_FEE == isOutParentFee)
                        .Select(o => o.ID).ToList() : null;

                    ExpMedicineTypeSDO sdo = new ExpMedicineTypeSDO();
                    sdo.Amount = req.Amount;
                    sdo.ExpMestMedicineIds = expMedicineIds;
                    sdo.MedicineTypeId = req.MedicineTypeId;
                    sdo.PatientTypeId = req.PatientTypeId;
                    reqSplits.Add(sdo);
                }
                HisMedicineBeanSplit spliter = new HisMedicineBeanSplit(param);
                this.beanSpliters.Add(spliter);

                //Truyen vao "useMedicineBeandIds" de viec tach bean ko lay ra ve cac bean da duoc su dung
                //(trong lan tach bean truoc do - trong cung 1 lan ke don)
                long? expiredDate = HisMediStockCFG.DONT_PRES_EXPIRED_ITEM ? (long?)instructionTime : null;

                if (!spliter.SplitByMedicineType(reqSplits, toSplits[0].MediStockId, expiredDate, null, useBeandIds, ref medicineBeans, ref medicinePaties))
                {
                    throw new Exception("Tach bean that bai. Rollback du lieu");
                }
            }
            else
            {
                List<ExpMedicineSDO> reqSplits = new List<ExpMedicineSDO>();

                foreach (ReqMedicineData req in toSplits)
                {
                    short? isExpend = req.IsExpend ? (short?)Constant.IS_TRUE : null;
                    short? isOutParentFee = req.IsOutParentFee ? (short?)Constant.IS_TRUE : null;

                    List<long> expMedicineIds = olds != null ? olds
                        .Where(o => o.MEDICINE_ID == req.MedicineId
                            && o.PATIENT_TYPE_ID == req.PatientTypeId
                            && o.SERE_SERV_PARENT_ID == req.SereServParentId
                            && o.IS_EXPEND == isExpend
                            && o.SERVICE_CONDITION_ID == req.ServiceConditionId
                            && o.OTHER_PAY_SOURCE_ID == req.OtherPaySourceId
                            && o.IS_OUT_PARENT_FEE == isOutParentFee)
                        .Select(o => o.ID).ToList() : null;

                    ExpMedicineSDO sdo = new ExpMedicineSDO();
                    sdo.Amount = req.Amount;
                    sdo.ExpMestMedicineIds = expMedicineIds;
                    sdo.MedicineId = req.MedicineId.Value;
                    sdo.PatientTypeId = req.PatientTypeId;
                    reqSplits.Add(sdo);
                }
                HisMedicineBeanSplit spliter = new HisMedicineBeanSplit(param);
                this.beanSpliters.Add(spliter);

                //Truyen vao "useMedicineBeandIds" de viec tach bean ko lay ra ve cac bean da duoc su dung
                //(trong lan tach bean truoc do - trong cung 1 lan ke don)
                if (!spliter.SplitByMedicine(reqSplits, toSplits[0].MediStockId, useBeandIds, ref medicineBeans, ref medicinePaties))
                {
                    throw new Exception("Tach bean that bai. Rollback du lieu");
                }
            }

            if (IsNotNullOrEmpty(medicineBeans))
            {
                //Cap nhat lai danh sach bean da duoc dung
                useBeandIds.AddRange(medicineBeans.Select(o => o.ID).ToList());

                List<HIS_EXP_MEST_MEDICINE> data = new List<HIS_EXP_MEST_MEDICINE>();

                //Duyet theo tung yeu cau de tao ra du lieu duyet (exp_mest_medicine) tuong ung
                foreach (ReqMedicineData req in toSplits)
                {
                    //Do danh sach mety_req dam bao ko co medicine_type_id nao trung nhau ==> dung medicine_type_id de lay ra cac bean tuong ung
                    List<HIS_MEDICINE_BEAN> reqBeans = medicineBeans
                    .Where(o => (o.TDL_MEDICINE_TYPE_ID == req.MedicineTypeId && !req.MedicineId.HasValue) || (o.MEDICINE_ID == req.MedicineId))
                    .ToList();
                    if (!IsNotNullOrEmpty(reqBeans))
                    {
                        throw new Exception("Ko tach duoc bean tuong ung voi MedicineTypeId:" + req.MedicineTypeId);
                    }

                    var group = reqBeans.GroupBy(o => new { o.MEDICINE_ID, o.TDL_MEDICINE_IMP_PRICE, o.TDL_MEDICINE_IMP_VAT_RATIO, o.TDL_SERVICE_ID });
                    foreach (var tmp in group)
                    {
                        List<HIS_MEDICINE_BEAN> beans = tmp.ToList();
                        HIS_EXP_MEST_MEDICINE exp = new HIS_EXP_MEST_MEDICINE();
                        exp.EXP_MEST_ID = req.ExpMestId;
                        exp.TDL_SERVICE_REQ_ID = req.ServiceReqId;
                        exp.TDL_TREATMENT_ID = req.TreatmentId;
                        exp.AMOUNT = beans.Sum(o => o.AMOUNT);
                        exp.TDL_MEDICINE_TYPE_ID = req.MedicineTypeId;
                        exp.MEDICINE_ID = tmp.Key.MEDICINE_ID;

                        bool isVaccine = false;
                        V_HIS_SERVICE service = HisServiceCFG.DATA_VIEW.FirstOrDefault(o => o.ID == tmp.Key.TDL_SERVICE_ID);
                        if (HisSereServCFG.IS_VACCINE_EXP_PRICE_OPTION)
                        {
                            if (IsNotNull(service) && service.IS_VACCINE == Constant.IS_TRUE)
                            {
                                isVaccine = true;
                            }
                        }

                        HIS_PATIENT_TYPE hisPatientType = HisPatientTypeCFG.DATA.Where(o => o.ID == req.PatientTypeId).FirstOrDefault();
                        if (isVaccine)
                        {
                            V_HIS_MEDI_STOCK stock = HisMediStockCFG.DATA.FirstOrDefault(o => o.ID == expMest.MEDI_STOCK_ID);
                            V_HIS_ROOM room = IsNotNull(stock) ? HisRoomCFG.DATA.FirstOrDefault(o => o.ID == stock.ROOM_ID) : null;
                            if (room == null)
                            {
                                throw new Exception("Khong ton tai chinh sach gia tuong ung voi medicine_id: " + tmp.Key.MEDICINE_ID + " va patient_type_id: " + req.PatientTypeId);
                            }

                            V_HIS_SERVICE_PATY paty = MOS.ServicePaty.ServicePatyUtil.GetApplied(HisServicePatyCFG.DATA, room.BRANCH_ID, room.ID, expMest.REQ_ROOM_ID, expMest.REQ_DEPARTMENT_ID, expMest.TDL_INTRUCTION_TIME ?? 0, treatment.IN_TIME, tmp.Key.TDL_SERVICE_ID, req.PatientTypeId, null, null, null, req.ServiceConditionId, treatment.TDL_PATIENT_CLASSIFY_ID, null);

                            if (paty == null)
                            {
                                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServicePaty_KhongTonTaiDuLieuPhuHop, service.SERVICE_NAME, service.SERVICE_CODE, hisPatientType.PATIENT_TYPE_NAME);
                                throw new Exception("Khong ton tai chinh sach gia tuong ung voi medicine_id: " + tmp.Key.MEDICINE_ID + "va patient_type_id: " + req.PatientTypeId);
                            }
                            exp.PRICE = paty.PRICE;
                            exp.VAT_RATIO = paty.VAT_RATIO;
                        }
                        else
                        {
                            //Neu ban bang gia nhap
                            if (beans[0].TDL_IS_SALE_EQUAL_IMP_PRICE == MOS.UTILITY.Constant.IS_TRUE)
                            {
                                exp.PRICE = tmp.Key.TDL_MEDICINE_IMP_PRICE;
                                exp.VAT_RATIO = tmp.Key.TDL_MEDICINE_IMP_VAT_RATIO;
                            }
                            else
                            {
                                HIS_MEDICINE_PATY paty = IsNotNullOrEmpty(medicinePaties) ? medicinePaties.Where(o => o.PATIENT_TYPE_ID == req.PatientTypeId && o.MEDICINE_ID == tmp.Key.MEDICINE_ID).FirstOrDefault() : null;
                                if (paty == null)
                                {
                                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServicePaty_KhongTonTaiDuLieuPhuHop, service.SERVICE_NAME, service.SERVICE_CODE, hisPatientType.PATIENT_TYPE_NAME);
                                    throw new Exception("Khong ton tai chinh sach gia tuong ung voi medicine_id: " + tmp.Key.MEDICINE_ID + "va patient_type_id: " + req.PatientTypeId);
                                }
                                exp.PRICE = paty.EXP_PRICE;
                                exp.VAT_RATIO = paty.EXP_VAT_RATIO;
                            }
                        }

                        exp.TDL_MEDI_STOCK_ID = req.MediStockId;
                        exp.NUM_ORDER = req.NumOrder;
                        exp.SPEED = req.Speed;
                        exp.IS_EXPEND = req.IsExpend ? new Nullable<short>(MOS.UTILITY.Constant.IS_TRUE) : null;
                        exp.SERVICE_CONDITION_ID = req.ServiceConditionId;
                        exp.OTHER_PAY_SOURCE_ID = req.OtherPaySourceId;
                        //tam thoi chua co danh muc, vi hien tai chi co 1 loai, nen fix code gia tri 1 
                        exp.EXPEND_TYPE_ID = req.IsBedExpend ? new Nullable<long>(1) : null;
                        exp.IS_OUT_PARENT_FEE = req.IsOutParentFee && req.SereServParentId.HasValue ? new Nullable<short>(MOS.UTILITY.Constant.IS_TRUE) : null;
                        exp.SERE_SERV_PARENT_ID = req.SereServParentId;
                        exp.TUTORIAL = req.Tutorial;
                        exp.MORNING = req.Morning;
                        exp.NOON = req.Noon;
                        exp.AFTERNOON = req.Afternoon;
                        exp.EVENING = req.Evening;
                        exp.HTU_ID = req.HtuId;
                        exp.BREATH_SPEED = req.BreathSpeed;
                        exp.BREATH_TIME = req.BreathTime;
                        exp.USE_ORIGINAL_UNIT_FOR_PRES = req.UseOriginalUnitForPres ? (short?)Constant.IS_TRUE : null;
                        exp.IS_NOT_PRES = req.IsNotPres ? (short?)Constant.IS_TRUE : null;
                        //Neu phan so luong lam tron chu ko phai do BS ke va DTTT la BHYT thi tu dong chuyen sang DTTT la vien phi
                        exp.PATIENT_TYPE_ID = req.IsNotPres && req.PatientTypeId == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT ? HisPatientTypeCFG.PATIENT_TYPE_ID__HOSPITAL_FEE : req.PatientTypeId;

                        if (req.NumOfDays.HasValue)
                        {
                            long dayCount = req.NumOfDays.Value == 0 ? 1 : req.NumOfDays.Value;

                            DateTime time = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(useTime > 0 ? useTime.Value : instructionTime).Value;
                            DateTime useTimeTo = time.AddDays(dayCount - 1);
                            exp.USE_TIME_TO = Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(useTimeTo);
                        }
                        exp.PREVIOUS_USING_COUNT = req.PreviousUsingCount;
                        exp.MIXED_INFUSION = req.MixedInfusion;
                        exp.IS_MIXED_MAIN = req.IsMixedMain;
                        exp.TUTORIAL_INFUSION = req.TutorialInfusion;
                        exp.PRES_AMOUNT = req.PresAmount;
                        exp.EXCEED_LIMIT_IN_PRES_REASON = req.ExceedLimitInPresReason;
                        exp.EXCEED_LIMIT_IN_DAY_REASON = req.ExceedLimitInDayReason;
                        exp.ODD_PRES_REASON = req.OddPresReason;
                        if (IsNotNullOrEmpty(req.MedicineInfoSdos))
                        {
                            foreach (var item in req.MedicineInfoSdos)
                            {
                                if (!item.IsNoPrescription && item.IntructionTime == req.InstructionTime)
                                {
                                    exp.OVER_KIDNEY_REASON = item.OverKidneyReason;
                                    exp.OVER_RESULT_TEST_REASON = item.OverResultTestReason;
                                }
                            }
                        }
                        data.Add(exp);

                        medicineDic.Add(exp, beans.Select(o => o.ID).ToList());
                    }
                }
                return data;
            }

            return null;
        }

        private void GetDiff(List<HIS_EXP_MEST_MEDICINE> olds, List<HIS_EXP_MEST_MEDICINE> news, Dictionary<HIS_EXP_MEST_MEDICINE, List<long>> newMedicineDic, List<HIS_MEDICINE_BEAN> oldBeans, ref List<HIS_EXP_MEST_MEDICINE> inserts, ref List<HIS_EXP_MEST_MEDICINE> deletes, ref List<HIS_EXP_MEST_MEDICINE> updates, ref List<HIS_EXP_MEST_MEDICINE> oldOfUpdates)
        {

            //Duyet du lieu truyen len de kiem tra thong tin thay doi
            if (!IsNotNullOrEmpty(news))
            {
                deletes = olds;
            }
            else if (!IsNotNullOrEmpty(olds))
            {
                inserts = news;
            }
            else
            {
                Mapper.CreateMap<HIS_EXP_MEST_MEDICINE, HIS_EXP_MEST_MEDICINE>();

                //Duyet danh sach moi, nhung du lieu co trong moi ma ko co trong cu ==> can them moi
                foreach (HIS_EXP_MEST_MEDICINE newMedicine in news)
                {
                    HIS_EXP_MEST_MEDICINE old = olds
                        .Where(t => !IsDiff(newMedicine, t, newMedicineDic, oldBeans)
                            && t.AMOUNT == newMedicine.AMOUNT
                            && t.IS_EXPEND == newMedicine.IS_EXPEND
                            && t.SERVICE_CONDITION_ID == newMedicine.IS_EXPEND
                            && t.IS_OUT_PARENT_FEE == newMedicine.IS_OUT_PARENT_FEE
                            && t.MEDICINE_ID == newMedicine.MEDICINE_ID
                            && t.PATIENT_TYPE_ID == newMedicine.PATIENT_TYPE_ID
                            && t.PRICE == newMedicine.PRICE
                            && t.SERE_SERV_PARENT_ID == newMedicine.SERE_SERV_PARENT_ID
                            && t.VAT_RATIO == newMedicine.VAT_RATIO
                            && t.EXPEND_TYPE_ID == newMedicine.EXPEND_TYPE_ID
                            && t.IS_NOT_PRES == newMedicine.IS_NOT_PRES
                        ).FirstOrDefault();

                    if (old == null)
                    {
                        inserts.Add(newMedicine);
                    }
                    else if (old.NUM_ORDER != newMedicine.NUM_ORDER || old.TUTORIAL != newMedicine.TUTORIAL || old.USE_TIME_TO != newMedicine.USE_TIME_TO
                        || old.MORNING != newMedicine.MORNING || old.NOON != newMedicine.NOON || old.AFTERNOON != newMedicine.AFTERNOON || old.EVENING != newMedicine.EVENING
                        || old.HTU_ID != newMedicine.HTU_ID
                        || old.PREVIOUS_USING_COUNT != newMedicine.PREVIOUS_USING_COUNT || old.MIXED_INFUSION != newMedicine.MIXED_INFUSION || old.IS_MIXED_MAIN != newMedicine.IS_MIXED_MAIN || old.TUTORIAL_INFUSION != newMedicine.TUTORIAL_INFUSION
                        || old.PRES_AMOUNT != newMedicine.PRES_AMOUNT
                        || old.EXCEED_LIMIT_IN_PRES_REASON != newMedicine.EXCEED_LIMIT_IN_PRES_REASON
                        || old.EXCEED_LIMIT_IN_DAY_REASON != newMedicine.EXCEED_LIMIT_IN_DAY_REASON
                        || old.ODD_PRES_REASON != newMedicine.ODD_PRES_REASON
                        || old.OVER_KIDNEY_REASON != newMedicine.OVER_KIDNEY_REASON
                        || old.OVER_RESULT_TEST_REASON != newMedicine.OVER_RESULT_TEST_REASON)
                    {
                        HIS_EXP_MEST_MEDICINE oldOfUpdate = Mapper.Map<HIS_EXP_MEST_MEDICINE>(old);
                        old.TUTORIAL = newMedicine.TUTORIAL;
                        old.MORNING = newMedicine.MORNING;
                        old.NOON = newMedicine.NOON;
                        old.AFTERNOON = newMedicine.AFTERNOON;
                        old.EVENING = newMedicine.EVENING;
                        old.HTU_ID = newMedicine.HTU_ID;
                        old.NUM_ORDER = newMedicine.NUM_ORDER;
                        old.USE_TIME_TO = newMedicine.USE_TIME_TO;
                        old.PREVIOUS_USING_COUNT = newMedicine.PREVIOUS_USING_COUNT;
                        old.MIXED_INFUSION = newMedicine.MIXED_INFUSION;
                        old.IS_MIXED_MAIN = newMedicine.IS_MIXED_MAIN;
                        old.TUTORIAL_INFUSION = newMedicine.TUTORIAL_INFUSION;
                        old.PRES_AMOUNT = newMedicine.PRES_AMOUNT;
                        old.EXCEED_LIMIT_IN_PRES_REASON = newMedicine.EXCEED_LIMIT_IN_PRES_REASON;
                        old.EXCEED_LIMIT_IN_DAY_REASON = newMedicine.EXCEED_LIMIT_IN_DAY_REASON;
                        old.ODD_PRES_REASON = newMedicine.ODD_PRES_REASON;
                        old.OVER_KIDNEY_REASON = newMedicine.OVER_KIDNEY_REASON;
                        old.OVER_RESULT_TEST_REASON = newMedicine.OVER_RESULT_TEST_REASON;

                        oldOfUpdates.Add(oldOfUpdate);
                        updates.Add(old);
                    }
                }

                //Duyet danh sach cu, nhung du lieu co trong cu ma ko co trong moi ==> can xoa
                foreach (HIS_EXP_MEST_MEDICINE old in olds)
                {
                    HIS_EXP_MEST_MEDICINE newMedicine = news
                        .Where(t => !IsDiff(t, old, newMedicineDic, oldBeans)
                            && t.AMOUNT == old.AMOUNT
                            && t.IS_EXPEND == old.IS_EXPEND
                            && t.SERVICE_CONDITION_ID == old.SERVICE_CONDITION_ID
                            && t.IS_OUT_PARENT_FEE == old.IS_OUT_PARENT_FEE
                            && t.MEDICINE_ID == old.MEDICINE_ID
                            && t.PATIENT_TYPE_ID == old.PATIENT_TYPE_ID
                            && t.PRICE == old.PRICE
                            && t.SERE_SERV_PARENT_ID == old.SERE_SERV_PARENT_ID
                            && t.VAT_RATIO == old.VAT_RATIO
                            && t.EXPEND_TYPE_ID == old.EXPEND_TYPE_ID
                            && t.IS_NOT_PRES == old.IS_NOT_PRES
                        ).FirstOrDefault();

                    if (newMedicine == null)
                    {
                        deletes.Add(old);
                    }
                }
            }
        }

        private bool IsDiff(HIS_EXP_MEST_MEDICINE newMedicine, HIS_EXP_MEST_MEDICINE oldMedicine, Dictionary<HIS_EXP_MEST_MEDICINE, List<long>> newMedicineDic, List<HIS_MEDICINE_BEAN> oldBeans)
        {
            List<long> oldBeanIdLst = IsNotNullOrEmpty(oldBeans) ? oldBeans.Where(o => o.EXP_MEST_MEDICINE_ID == oldMedicine.ID).Select(o => o.ID).ToList() : null;
            List<long> newBeanIdLst = newMedicineDic != null && newMedicineDic.ContainsKey(newMedicine) ? newMedicineDic[newMedicine] : null;
            return CommonUtil.IsDiff<long>(oldBeanIdLst, newBeanIdLst);
        }

        private void PassResult(List<HIS_EXP_MEST_MEDICINE> olds, List<HIS_EXP_MEST_MEDICINE> inserts, List<HIS_EXP_MEST_MEDICINE> updates, List<HIS_EXP_MEST_MEDICINE> deletes, ref List<HIS_EXP_MEST_MEDICINE> refInserts, ref List<HIS_EXP_MEST_MEDICINE> refDeletes, ref List<HIS_EXP_MEST_MEDICINE> resultData)
        {
            if (IsNotNullOrEmpty(inserts) || IsNotNullOrEmpty(olds))
            {
                resultData = new List<HIS_EXP_MEST_MEDICINE>();
                if (IsNotNullOrEmpty(inserts))
                {
                    resultData.AddRange(inserts);
                    refInserts.AddRange(inserts);
                }
                if (IsNotNullOrEmpty(deletes))
                {
                    refDeletes.AddRange(deletes);
                }
                if (IsNotNullOrEmpty(updates))
                {
                    resultData.AddRange(updates);
                }
                if (IsNotNullOrEmpty(olds))
                {
                    Mapper.CreateMap<HIS_EXP_MEST_MEDICINE, HIS_EXP_MEST_MEDICINE>();
                    //clone, tranh thay doi du lieu tra ve qua bien ref
                    List<HIS_EXP_MEST_MEDICINE> remains = Mapper.Map<List<HIS_EXP_MEST_MEDICINE>>(olds);
                    remains.RemoveAll(o => deletes != null && deletes.Exists(t => t.ID == o.ID));
                    remains.RemoveAll(o => updates != null && updates.Exists(t => t.ID == o.ID));//cac ban ghi update da duoc add vao resultData o tren
                    resultData.AddRange(remains);
                }
            }
        }

        private void SqlUpdateBean(Dictionary<HIS_EXP_MEST_MEDICINE, List<long>> newMedicineDic, List<long> deleteExpMestMedicineIds, ref List<string> sqls)
        {
            //Can cap nhat cac bean ko dung truoc
            //Tranh truong hop bean duoc gan lai vao cac exp_mest_medicine tao moi
            if (IsNotNullOrEmpty(deleteExpMestMedicineIds))
            {
                //cap nhat danh sach cac bean ko dung
                string query2 = DAOWorker.SqlDAO.AddInClause(deleteExpMestMedicineIds, "UPDATE HIS_MEDICINE_BEAN SET IS_ACTIVE = 1, EXP_MEST_MEDICINE_ID = NULL WHERE %IN_CLAUSE% ", "EXP_MEST_MEDICINE_ID");
                sqls.Add(query2);
            }

            if (IsNotNullOrEmpty(newMedicineDic))
            {
                //cap nhat danh sach cac bean da dung tuong ung voi cac exp_mest_medicine
                foreach (HIS_EXP_MEST_MEDICINE key in newMedicineDic.Keys)
                {
                    if (key.ID > 0) //chi xu ly voi cac exp_mest_medicine insert moi (duoc insert vao DB ==> co ID)
                    {
                        string query = DAOWorker.SqlDAO.AddInClause(newMedicineDic[key], "UPDATE HIS_MEDICINE_BEAN SET IS_ACTIVE = 0, EXP_MEST_MEDICINE_ID = {0} WHERE %IN_CLAUSE% ", "ID");
                        query = string.Format(query, key.ID);
                        sqls.Add(query);
                    }
                }
            }
        }

        private void SqlDeleteExpMestMedicine(List<long> deleteIds, ref List<string> sqls)
        {
            if (IsNotNullOrEmpty(deleteIds))
            {
                string sql = DAOWorker.SqlDAO.AddInClause(deleteIds, "UPDATE HIS_EXP_MEST_MEDICINE SET IS_DELETE = 1, EXP_MEST_ID = NULL, MEDICINE_ID = NULL, TDL_MEDI_STOCK_ID = NULL, TDL_MEDICINE_TYPE_ID = NULL WHERE %IN_CLAUSE%", "ID");
                sqls.Add(sql);
            }
        }

        /// <summary>
        /// Duyet theo y/c cua client de tao ra yeu cau tach bean tuong ung
        /// Với mỗi y/c, co bao nhieu exp_mest tuong ung voi kho đó thi tao ra bấy nhiêu y/c tách bean giống nhau
        /// (do cung 1 kho ma co nhieu exp_mest ==> do chỉ định nhiều ngày khác nhau)
        /// </summary>
        /// <param name="medicines"></param>
        /// <param name="expMests"></param>
        /// <returns></returns>
        private List<ReqMedicineData> MakeReqMedicineData(List<PresMedicineSDO> medicines, HIS_EXP_MEST expMest, long instructionTime)
        {
            if (IsNotNullOrEmpty(medicines))
            {
                List<ReqMedicineData> reqData = new List<ReqMedicineData>();
                int index = 0;
                foreach (PresMedicineSDO sdo in medicines)
                {
                    ReqMedicineData req = new ReqMedicineData(sdo, expMest.ID, expMest.SERVICE_REQ_ID.Value, expMest.TDL_TREATMENT_ID.Value, instructionTime, ++index);
                    reqData.Add(req);
                }
                return reqData;
            }
            return null;
        }

        internal void Rollback()
        {
            if (IsNotNullOrEmpty(this.beanSpliters))
            {
                foreach (HisMedicineBeanSplit spliter in this.beanSpliters)
                {
                    spliter.RollBack();
                }
            }

            this.hisExpMestMedicineCreate.RollbackData();
        }
    }
}
