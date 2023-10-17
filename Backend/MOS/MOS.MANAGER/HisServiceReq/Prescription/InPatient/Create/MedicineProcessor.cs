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

namespace MOS.MANAGER.HisServiceReq.Prescription.InPatient.Create
{
    class MedicineProcessor : BusinessBase
    {
        private List<HisMedicineBeanSplit> beanSpliters;
        private HisExpMestMedicineCreate hisExpMestMedicineCreate;

        internal MedicineProcessor(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.hisExpMestMedicineCreate = new HisExpMestMedicineCreate(param);
            this.beanSpliters = new List<HisMedicineBeanSplit>();
        }

        internal bool Run(HisSereServPackage37 processPackage37, HisSereServPackageBirth processPackageBirth, HisSereServPackagePttm processPackagePttm, List<PresMedicineSDO> medicines, List<HIS_EXP_MEST> expMests, ref List<HIS_EXP_MEST_MEDICINE> resultData, ref List<V_HIS_MEDICINE_2> choosenMedicines, HIS_TREATMENT treatment, ref List<string> sqls, long? useTime = null)
        {
            try
            {
                if (IsNotNullOrEmpty(medicines) && IsNotNullOrEmpty(expMests))
                {
                    List<ReqMedicineData> reqData = this.MakeReqMedicineData(medicines, expMests);

                    List<HIS_EXP_MEST_MEDICINE> expMestMedicines = new List<HIS_EXP_MEST_MEDICINE>();
                    Dictionary<HIS_EXP_MEST_MEDICINE, List<long>> medicineDic = new Dictionary<HIS_EXP_MEST_MEDICINE, List<long>>();
                    List<HIS_MEDICINE_PATY> medicinePaties = new List<HIS_MEDICINE_PATY>();

                    //- Duyet danh sach yeu cau de tao ra cac lenh tach bean. Can thoa man:
                    //  + 2 req co loai thuoc giong nhau thi can thuoc 2 lenh tach bean khac nhau
                    //  + 2 req co medi_stock_id khac nhau thi can thuoc 2 lenh tach bean khac nhau
                    //  + 2 req co dau * va ko phai dau * can thuoc 2 lenh tach bean khac nhau
                    //- Sau khi thuc hien tach bean, thuc hien tao exp_mest_medicine tuong ung
                    bool byMedicine = IsNotNullOrEmpty(reqData) && reqData.Exists(t => t.MedicineId.HasValue);

                    //Neu co cau hinh ko ke thuoc/vat tu het HSD va hinh thuc ke don ko phai la ke theo lo
                    //thi can gom nhom theo thoi gian y lenh de xu ly ko layb lo thuoc het HSD
                    if (HisMediStockCFG.DONT_PRES_EXPIRED_ITEM && !byMedicine)
                    {
                        var groups = reqData.GroupBy(o => new { o.MediStockId, o.InstructionTime, o.IsNotPres, o.Tutorial, o.MixedInfusion });
                        foreach (var group in groups)
                        {
                            List<ReqMedicineData> lst = group.ToList();
                            while (lst.Count > 0)
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
                                List<HIS_MEDICINE_PATY> paties = null;
                                List<HIS_EXP_MEST_MEDICINE> data = this.SplitBeanAndMakeData(byMedicine, group.Key.InstructionTime, useTime, processPackage37, processPackageBirth, processPackagePttm, expMests, treatment, toSplits, ref medicineDic, ref paties);
                                if (IsNotNullOrEmpty(data))
                                {
                                    expMestMedicines.AddRange(data);
                                    if (IsNotNullOrEmpty(paties))
                                    {
                                        medicinePaties.AddRange(paties);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        var groups = reqData.GroupBy(o => new { o.MediStockId, o.IsNotPres, o.Tutorial, o.MixedInfusion });
                        foreach (var group in groups)
                        {
                            List<ReqMedicineData> lst = group.ToList();
                            while (lst.Count > 0)
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
                                List<HIS_MEDICINE_PATY> paties = null;
                                List<HIS_EXP_MEST_MEDICINE> data = this.SplitBeanAndMakeData(byMedicine, null, useTime, processPackage37, processPackageBirth, processPackagePttm, expMests, treatment, toSplits, ref medicineDic, ref paties);
                                if (IsNotNullOrEmpty(data))
                                {
                                    expMestMedicines.AddRange(data);
                                    if (IsNotNullOrEmpty(paties))
                                    {
                                        medicinePaties.AddRange(paties);
                                    }
                                }
                            }
                        }
                    }

                    List<long> medicineIds = expMestMedicines.Select(o => o.MEDICINE_ID.Value).ToList();
                    choosenMedicines = new HisMedicineGet().GetView2ByIds(medicineIds);

                    if (!new HisServiceReqPresCheck(param).IsValidMedicineWithBidDate(choosenMedicines, expMestMedicines, expMests))
                    {
                        throw new Exception("IsValidMedicineWithBidDate false. Rollback du lieu");
                    }

                    if (IsNotNullOrEmpty(expMestMedicines) && !new HisServiceReqPresUtil(param).ProcessAutoChangeBhytToHospitalFee(choosenMedicines, medicinePaties, expMestMedicines, expMests))
                    {
                        throw new Exception("Rollback du lieu");
                    }

                    if (IsNotNullOrEmpty(expMestMedicines) && !this.hisExpMestMedicineCreate.CreateList(expMestMedicines))
                    {
                        throw new Exception("Tao exp_mest_medicine that bai. Rollback du lieu");
                    }

                    this.SqlUpdateBean(medicineDic, ref sqls);
                    resultData = expMestMedicines;
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

        /// <summary>
        /// Tach bean theo ReqMedicineData va tao ra exp_mest_medicine tuong ung
        /// </summary>
        /// <param name="toSplits">Danh sach ReqMedicineData dam bao ko co medicine_type_id nao trung nhau va cung thuoc 1 medi_stock_id</param>
        /// <param name="expMest"></param>
        /// <returns></returns>
        private List<HIS_EXP_MEST_MEDICINE> SplitBeanAndMakeData(bool byMedicine, long? instructionTime, long? useTime, HisSereServPackage37 processPackage37, HisSereServPackageBirth processPackageBirth, HisSereServPackagePttm processPackagePttm, List<HIS_EXP_MEST> expMests, HIS_TREATMENT treatment, List<ReqMedicineData> toSplits, ref Dictionary<HIS_EXP_MEST_MEDICINE, List<long>> medicineDic, ref List<HIS_MEDICINE_PATY> medicinePaties)
        {
            List<HIS_MEDICINE_BEAN> medicineBeans = null;

            //Neu ke theo lo
            if (byMedicine)
            {
                List<ExpMedicineSDO> reqSplits = toSplits.Select(o => new ExpMedicineSDO
                {
                    Amount = o.Amount,
                    MedicineId = o.MedicineId.Value,
                    PatientTypeId = o.PatientTypeId
                }).ToList();

                HisMedicineBeanSplit spliter = new HisMedicineBeanSplit(param);
                this.beanSpliters.Add(spliter);

                if (!spliter.SplitByMedicine(reqSplits, toSplits[0].MediStockId, null, ref medicineBeans, ref medicinePaties))
                {
                    throw new Exception("Tach bean that bai. Rollback du lieu");
                }
            }
            else
            {
                List<ExpMedicineTypeSDO> reqSplits = toSplits.Select(o => new ExpMedicineTypeSDO
                {
                    Amount = o.Amount,
                    MedicineTypeId = o.MedicineTypeId,
                    PatientTypeId = o.PatientTypeId
                }).ToList();

                HisMedicineBeanSplit spliter = new HisMedicineBeanSplit(param);
                this.beanSpliters.Add(spliter);

                if (!spliter.SplitByMedicineType(reqSplits, toSplits[0].MediStockId, instructionTime, null, null, ref medicineBeans, ref medicinePaties))
                {
                    throw new Exception("Tach bean that bai. Rollback du lieu");
                }
            }

            List<HIS_EXP_MEST_MEDICINE> data = new List<HIS_EXP_MEST_MEDICINE>();

            //Duyet theo tung yeu cau de tao ra du lieu duyet (exp_mest_medicine) tuong ung
            foreach (ReqMedicineData req in toSplits)
            {
                //Do danh sach mety_req dam bao ko co medicine_type_id nao trung nhau ==> dung medicine_type_id de lay ra cac bean tuong ung
                //Neu ke theo lo thi can cu vao medicine_id
                List<HIS_MEDICINE_BEAN> reqBeans = medicineBeans
                    .Where(o => (o.TDL_MEDICINE_TYPE_ID == req.MedicineTypeId && !req.MedicineId.HasValue) || (o.MEDICINE_ID == req.MedicineId))
                    .ToList();

                List<HIS_EXP_MEST_MEDICINE> medicines = new List<HIS_EXP_MEST_MEDICINE>();
                if (!IsNotNullOrEmpty(reqBeans))
                {
                    throw new Exception("Ko tach duoc bean tuong ung voi MedicineTypeId:" + req.MedicineTypeId);
                }
                //Lay exp_mest tuong ung voi kho xuat
                HIS_EXP_MEST expMest = expMests.Where(o => o.MEDI_STOCK_ID == req.MediStockId).FirstOrDefault();
                if (expMest == null)
                {
                    LogSystem.Error("Ko ton tai exp_mest tuong ung voi medi_stock_id: " + req.ExpMestId);
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
                    exp.USE_ORIGINAL_UNIT_FOR_PRES = req.UseOriginalUnitForPres ? (short?)Constant.IS_TRUE : null;

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
                    exp.IS_OUT_PARENT_FEE = req.IsOutParentFee && req.SereServParentId.HasValue ? new Nullable<short>(MOS.UTILITY.Constant.IS_TRUE) : null;
                    //tam thoi chua co danh muc, vi hien tai chi co 1 loai, nen fix code gia tri 1 
                    exp.EXPEND_TYPE_ID = req.IsBedExpend ? new Nullable<long>(1) : null;
                    exp.SERE_SERV_PARENT_ID = req.SereServParentId;
                    exp.TUTORIAL = req.Tutorial;
                    exp.MORNING = req.Morning;
                    exp.NOON = req.Noon;
                    exp.AFTERNOON = req.Afternoon;
                    exp.EVENING = req.Evening;
                    exp.HTU_ID = req.HtuId;
                    exp.BREATH_SPEED = req.BreathSpeed;
                    exp.BREATH_TIME = req.BreathTime;
                    exp.IS_NOT_PRES = req.IsNotPres ? (short?)Constant.IS_TRUE : null;
                    //Neu phan so luong lam tron chu ko phai do BS ke va DTTT la BHYT thi tu dong chuyen sang DTTT la vien phi
                    exp.PATIENT_TYPE_ID = req.IsNotPres && req.PatientTypeId == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT ? HisPatientTypeCFG.PATIENT_TYPE_ID__HOSPITAL_FEE : req.PatientTypeId;

                    if (req.NumOfDays.HasValue)
                    {
                        long dayCount = req.NumOfDays.Value == 0 ? 1 : req.NumOfDays.Value;

                        DateTime time = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(useTime > 0 ? useTime.Value : req.InstructionTime).Value;
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
                    medicines.Add(exp);
                    medicineDic.Add(exp, beans.Select(o => o.ID).ToList());
                }
                //Xu ly de ap dung chinh sach gia 3 ngay 7 ngay
                processPackage37.Apply3Day7Day(null, null, medicines, req.InstructionTime);
                //Xu ly de ap dung goi de
                processPackageBirth.Run(medicines, req.SereServParentId);
                //Xu ly de ap dung goi phau thuat tham my
                processPackagePttm.Run(medicines, req.SereServParentId, req.InstructionTime);

                data.AddRange(medicines);
            }
            return data;
        }

        private void SqlUpdateBean(Dictionary<HIS_EXP_MEST_MEDICINE, List<long>> useBeandIdDic, ref List<string> sqls)
        {
            if (IsNotNullOrEmpty(useBeandIdDic))
            {
                foreach (HIS_EXP_MEST_MEDICINE expMestMedicine in useBeandIdDic.Keys)
                {
                    List<long> beanIds = useBeandIdDic[expMestMedicine];
                    //cap nhat danh sach cac bean da dung
                    string query = DAOWorker.SqlDAO.AddInClause(beanIds, "UPDATE HIS_MEDICINE_BEAN SET SESSION_KEY = NULL, SESSION_TIME = NULL, IS_ACTIVE = 0, EXP_MEST_MEDICINE_ID = {0} WHERE %IN_CLAUSE% ", "ID");
                    query = string.Format(query, expMestMedicine.ID);
                    sqls.Add(query);
                }
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
        private List<ReqMedicineData> MakeReqMedicineData(List<PresMedicineSDO> medicines, List<HIS_EXP_MEST> expMests)
        {
            if (IsNotNullOrEmpty(medicines))
            {
                List<ReqMedicineData> reqData = new List<ReqMedicineData>();

                foreach (PresMedicineSDO sdo in medicines)
                {
                    short? isStarMark = HisMedicineTypeCFG.STAR_IDs != null && HisMedicineTypeCFG.STAR_IDs.Contains(sdo.MedicineTypeId) ?
                        (short?)Constant.IS_TRUE : null;

                    //Lay exp_mest tuong ung voi kho xuat va thoi gian y lenh
                    List<HIS_EXP_MEST> exps = null;

                    if (HisServiceReqCFG.MANY_DAYS_PRESCRIPTION_OPTION == HisServiceReqCFG.ManyDaysPrescriptionOption.BY_PRES)
                    {
                        if (!HisExpMestCFG.IS_SPLIT_STAR_MARK)
                        {
                            exps = expMests.Where(o => o.MEDI_STOCK_ID == sdo.MediStockId).ToList();
                        }
                        else
                        {
                            exps = expMests.Where(o => o.MEDI_STOCK_ID == sdo.MediStockId && o.IS_STAR_MARK == isStarMark).ToList();
                        }
                    }
                    else
                    {
                        if (!HisExpMestCFG.IS_SPLIT_STAR_MARK)
                        {
                            exps = expMests.Where(o => o.MEDI_STOCK_ID == sdo.MediStockId
                                && sdo.InstructionTimes.Contains(o.TDL_INTRUCTION_TIME.Value)).ToList();
                        }
                        else
                        {
                            exps = expMests.Where(o => o.MEDI_STOCK_ID == sdo.MediStockId
                                && sdo.InstructionTimes.Contains(o.TDL_INTRUCTION_TIME.Value)
                                && o.IS_STAR_MARK == isStarMark).ToList();
                        }
                    }

                    if (!IsNotNullOrEmpty(exps))
                    {
                        LogSystem.Error("Ko ton tai exp_mest tuong ung voi medi_stock_id: " + sdo.MediStockId);
                    }

                    int index = 0;
                    foreach (HIS_EXP_MEST expMest in exps)
                    {
                        ReqMedicineData req = new ReqMedicineData(sdo, expMest.ID, expMest.SERVICE_REQ_ID.Value, expMest.TDL_TREATMENT_ID.Value, expMest.TDL_INTRUCTION_TIME.Value, ++index);
                        reqData.Add(req);
                    }
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
