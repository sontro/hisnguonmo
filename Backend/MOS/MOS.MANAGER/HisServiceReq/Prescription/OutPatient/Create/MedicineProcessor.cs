﻿using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisMedicine;
using MOS.MANAGER.HisMedicineBean;
using MOS.MANAGER.HisMedicineBean.Handle;
using MOS.MANAGER.HisMedicinePaty;
using MOS.MANAGER.HisSereServ.Update;
using MOS.MANAGER.HisSereServ.Update.Package;
using MOS.MANAGER.HisServicePaty;
using MOS.SDO;
using MOS.UTILITY;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Prescription.OutPatient.Create
{
    class MedicineProcessor : BusinessBase
    {
        private HisExpMestMedicineCreate hisExpMestMedicineCreate;

        internal MedicineProcessor(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.hisExpMestMedicineCreate = new HisExpMestMedicineCreate(param);
        }

        /// <summary>
        /// Kiem tra cac bean ma client gui len co hop le khong. Bean can dam bao:
        /// + Dang khong bi khoa hoac Neu dang bi khoa thi session-key = session-key cua client truyen len
        /// + Tong so luong phai dung voi so luong client gui len
        /// </summary>
        /// <returns></returns>
        internal bool Run(OutPatientPresSDO presSdo, HisSereServPackage37 processPackage37, HisSereServPackageBirth processPackageBirth, HisSereServPackagePttm processPackagePttm, List<HIS_EXP_MEST> expMests, HIS_TREATMENT treatment, ref List<HIS_EXP_MEST_MEDICINE> resultData, ref List<V_HIS_MEDICINE_2> choosenMedicines, ref List<string> sqls)
        {
            try
            {
                string sessionKey = SessionUtil.SessionKey(presSdo.ClientSessionKey);

                if (IsNotNullOrEmpty(presSdo.Medicines) && IsNotNullOrEmpty(expMests))
                {
                    List<long> medicineBeanIds = new List<long>();
                    foreach (PresMedicineSDO m in presSdo.Medicines)
                    {
                        medicineBeanIds.AddRange(m.MedicineBeanIds);
                    }

                    List<HIS_MEDICINE_BEAN> medicineBeans = IsNotNullOrEmpty(medicineBeanIds) ? new HisMedicineBeanGet().GetByIds(medicineBeanIds) : null;
                    if (!IsNotNullOrEmpty(medicineBeans))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        LogSystem.Warn("medicine_bean_id ko hop le");
                        return false;
                    }

                    List<HIS_MEDICINE_BEAN> unavailables = medicineBeans
                        .Where(o => o.IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__FALSE && o.SESSION_KEY != sessionKey)
                        .ToList();

                    if (IsNotNullOrEmpty(unavailables))
                    {
                        LogSystem.Warn("Ton tai meidicine_bean khong cho phep lay: co session_key khac session_key client va dang bi khoa" + LogUtil.TraceData("unavailables", unavailables));
                        return false;
                    }

                    //Neu trong d/s bean co bean ko co cau hinh "ban bang gia nhap" thi lay chinh sach gia,
                    //phuc vu lay chinh sach gia
                    List<long> medicineIds = medicineBeans
                        .Where(o => o.TDL_IS_SALE_EQUAL_IMP_PRICE != MOS.UTILITY.Constant.IS_TRUE)
                        .Select(o => o.MEDICINE_ID).Distinct().ToList();

                    List<HIS_MEDICINE_PATY> medicinePaties = null;
                    if (IsNotNullOrEmpty(medicineIds))
                    {
                        List<long> patientTypeIds = presSdo.Medicines.Select(o => o.PatientTypeId).Distinct().ToList();

                        HisMedicinePatyFilterQuery filter = new HisMedicinePatyFilterQuery();
                        filter.MEDICINE_IDs = medicineIds;
                        filter.PATIENT_TYPE_IDs = patientTypeIds;
                        medicinePaties = new HisMedicinePatyGet().Get(filter);
                    }

                    List<HIS_EXP_MEST_MEDICINE> data = new List<HIS_EXP_MEST_MEDICINE>();
                    Dictionary<HIS_EXP_MEST_MEDICINE, List<long>> useBeanIdDic = new Dictionary<HIS_EXP_MEST_MEDICINE, List<long>>();

                    var groups = presSdo.Medicines.GroupBy(o => new { o.MedicineTypeId, o.MixedInfusion });
                    foreach (var group in groups)
                    {
                        //Duyet theo y/c cua client de tao ra exp_mest_medicine tuong ung
                        foreach (PresMedicineSDO sdo in group)
                        {
                            //Lay exp_mest tuong ung voi kho xuat
                            HIS_EXP_MEST expMest = IsNotNullOrEmpty(expMests) ? expMests.Where(o => o.MEDI_STOCK_ID == sdo.MediStockId).FirstOrDefault() : null;
                            if (expMest == null)
                            {
                                LogSystem.Error("Ko ton tai exp_mest tuong ung voi medi_stock_id: " + sdo.MediStockId);
                                return false;
                            }

                            List<HIS_MEDICINE_BEAN> beans = medicineBeans
                                //Neu ke theo lo (medicine_id) thi chi lay theo medicine_id
                                .Where(o => ((o.TDL_MEDICINE_TYPE_ID == sdo.MedicineTypeId && !sdo.MedicineId.HasValue) || o.MEDICINE_ID == sdo.MedicineId)
                                    && o.MEDI_STOCK_ID == sdo.MediStockId && sdo.MedicineBeanIds != null && sdo.MedicineBeanIds.Contains(o.ID))
                                .OrderBy(o => o.MEDICINE_ID)
                                .ToList();

                            decimal beanAmount = IsNotNullOrEmpty(beans) ? beans.Sum(o => o.AMOUNT) : 0;

                            //Neu so luong cua bean ko khop so luong do client y/c ==> reject
                            if (sdo.Amount != beanAmount)
                            {
                                MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                                LogSystem.Warn("Ton tai meidicine_bean khong hop le: co so luong y/c khac voi tong so luong cua bean. So luong bean: " + beanAmount + LogUtil.TraceData("sdo", sdo));
                                return false;
                            }

                            //Group theo medicine_id de tao ra exp_mest_medicine
                            List<HIS_EXP_MEST_MEDICINE> expMestMedicines = new List<HIS_EXP_MEST_MEDICINE>();
                            var groupMedicines = beans.Select(o => new { o.MEDICINE_ID, o.TDL_MEDICINE_TYPE_ID, o.TDL_SERVICE_ID }).Distinct().ToList();

                            foreach (var medicine in groupMedicines)
                            {
                                List<HIS_MEDICINE_BEAN> b = beans.Where(o => o.MEDICINE_ID == medicine.MEDICINE_ID).ToList();
                                HIS_EXP_MEST_MEDICINE exp = new HIS_EXP_MEST_MEDICINE();
                                exp.EXP_MEST_ID = expMest.ID;
                                exp.TDL_SERVICE_REQ_ID = expMest.SERVICE_REQ_ID;
                                exp.TDL_TREATMENT_ID = expMest.TDL_TREATMENT_ID;
                                exp.AMOUNT = b.Sum(o => o.AMOUNT);
                                exp.IS_EXPEND = sdo.IsExpend ? new Nullable<short>(MOS.UTILITY.Constant.IS_TRUE) : null;
                                exp.PATIENT_TYPE_ID = sdo.PatientTypeId;
                                exp.SERE_SERV_PARENT_ID = sdo.SereServParentId;
                                exp.IS_OUT_PARENT_FEE = sdo.IsOutParentFee && sdo.SereServParentId.HasValue ? new Nullable<short>(MOS.UTILITY.Constant.IS_TRUE) : null;
                                exp.MEDICINE_ID = medicine.MEDICINE_ID;
                                exp.NUM_ORDER = sdo.NumOrder;
                                exp.SPEED = sdo.Speed;
                                //tam thoi chua co danh muc, vi hien tai chi co 1 loai, nen fix code gia tri 1 
                                exp.EXPEND_TYPE_ID = sdo.IsBedExpend ? new Nullable<long>(1) : null;
                                exp.USE_ORIGINAL_UNIT_FOR_PRES = sdo.UseOriginalUnitForPres ? (short?)Constant.IS_TRUE : null;

                                bool isVaccine = false;
                                V_HIS_SERVICE service = HisServiceCFG.DATA_VIEW.FirstOrDefault(o => o.ID == medicine.TDL_SERVICE_ID);
                                if (HisSereServCFG.IS_VACCINE_EXP_PRICE_OPTION)
                                {
                                    if (IsNotNull(service) && service.IS_VACCINE == Constant.IS_TRUE)
                                    {
                                        isVaccine = true;
                                    }
                                }
                                HIS_PATIENT_TYPE hisPatientType = HisPatientTypeCFG.DATA.Where(o => o.ID == sdo.PatientTypeId).FirstOrDefault();
                                if (isVaccine)
                                {
                                    V_HIS_MEDI_STOCK stock = HisMediStockCFG.DATA.FirstOrDefault(o => o.ID == expMest.MEDI_STOCK_ID);
                                    V_HIS_ROOM room = IsNotNull(stock) ? HisRoomCFG.DATA.FirstOrDefault(o => o.ID == stock.ROOM_ID) : null;
                                    if (room == null)
                                    {
                                        throw new Exception("Khong ton tai chinh sach gia tuong ung voi medicine_id: " + medicine.MEDICINE_ID + " va patient_type_id: " + sdo.PatientTypeId);
                                    }

                                    V_HIS_SERVICE_PATY paty = MOS.ServicePaty.ServicePatyUtil.GetApplied(HisServicePatyCFG.DATA, room.BRANCH_ID, room.ID, expMest.REQ_ROOM_ID, expMest.REQ_DEPARTMENT_ID, expMest.TDL_INTRUCTION_TIME ?? 0, treatment.IN_TIME, medicine.TDL_SERVICE_ID, sdo.PatientTypeId, null, null, null, sdo.ServiceConditionId, treatment.TDL_PATIENT_CLASSIFY_ID, null);

                                    if (paty == null)
                                    {
                                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServicePaty_KhongTonTaiDuLieuPhuHop, service.SERVICE_NAME, service.SERVICE_CODE, hisPatientType.PATIENT_TYPE_NAME);
                                        throw new Exception("Khong ton tai chinh sach gia tuong ung voi medicine_id: " + medicine.MEDICINE_ID + " va patient_type_id: " + sdo.PatientTypeId);
                                    }
                                    exp.PRICE = paty.PRICE;
                                    exp.VAT_RATIO = paty.VAT_RATIO;
                                }
                                else
                                {
                                    //Neu ban bang gia nhap
                                    if (b[0].TDL_IS_SALE_EQUAL_IMP_PRICE == MOS.UTILITY.Constant.IS_TRUE)
                                    {
                                        exp.PRICE = b[0].TDL_MEDICINE_IMP_PRICE;
                                        exp.VAT_RATIO = b[0].TDL_MEDICINE_IMP_VAT_RATIO;
                                    }
                                    else
                                    {
                                        HIS_MEDICINE_PATY paty = IsNotNullOrEmpty(medicinePaties) ? medicinePaties.Where(o => o.PATIENT_TYPE_ID == sdo.PatientTypeId && o.MEDICINE_ID == medicine.MEDICINE_ID).FirstOrDefault() : null;
                                        if (paty == null)
                                        {
                                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServicePaty_KhongTonTaiDuLieuPhuHop, service.SERVICE_NAME, service.SERVICE_CODE, hisPatientType.PATIENT_TYPE_NAME);
                                            throw new Exception("Khong ton tai chinh sach gia tuong ung voi medicine_id: " + medicine.MEDICINE_ID + " va patient_type_id: " + sdo.PatientTypeId);
                                        }
                                        exp.PRICE = paty.EXP_PRICE;
                                        exp.VAT_RATIO = paty.EXP_VAT_RATIO;
                                    }
                                }

                                exp.TUTORIAL = sdo.Tutorial;
                                exp.MORNING = sdo.Morning;
                                exp.NOON = sdo.Noon;
                                exp.AFTERNOON = sdo.Afternoon;
                                exp.EVENING = sdo.Evening;
                                exp.HTU_ID = sdo.HtuId;
                                exp.BREATH_SPEED = sdo.BreathSpeed;
                                exp.BREATH_TIME = sdo.BreathTime;

                                exp.TDL_MEDI_STOCK_ID = expMest.MEDI_STOCK_ID;
                                exp.TDL_MEDICINE_TYPE_ID = medicine.TDL_MEDICINE_TYPE_ID;
                                exp.SERVICE_CONDITION_ID = sdo.ServiceConditionId;
                                exp.OTHER_PAY_SOURCE_ID = sdo.OtherPaySourceId;
                                exp.MIXED_INFUSION = sdo.MixedInfusion;
                                exp.IS_MIXED_MAIN = sdo.IsMixedMain;
                                exp.TUTORIAL_INFUSION = sdo.TutorialInfusion;
                                exp.PRES_AMOUNT = sdo.PresAmount;
                                if (sdo.NumOfDays.HasValue)
                                {
                                    long dayCount = sdo.NumOfDays.Value == 0 ? 1 : sdo.NumOfDays.Value;

                                    DateTime time = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(presSdo.UseTime > 0 ? presSdo.UseTime.Value : presSdo.InstructionTime).Value;
                                    DateTime useTimeTo = time.AddDays(dayCount - 1);
                                    exp.USE_TIME_TO = Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(useTimeTo);
                                }
                                exp.PREVIOUS_USING_COUNT = sdo.PreviousUsingCount;
                                exp.EXCEED_LIMIT_IN_PRES_REASON = sdo.ExceedLimitInPresReason;
                                exp.EXCEED_LIMIT_IN_DAY_REASON = sdo.ExceedLimitInDayReason;
                                exp.ODD_PRES_REASON = sdo.OddPresReason;
                                if (IsNotNullOrEmpty(sdo.MedicineInfoSdos))
                                {
                                    foreach (var item in sdo.MedicineInfoSdos)
                                    {
                                        if (!item.IsNoPrescription && item.IntructionTime == presSdo.InstructionTime)
                                        {
                                            exp.OVER_KIDNEY_REASON = item.OverKidneyReason;
                                            exp.OVER_RESULT_TEST_REASON = item.OverResultTestReason;
                                        }
                                    }
                                }
                                
                                expMestMedicines.Add(exp);
                                //Luu medicine_bean tuong ung voi tung exp_mest_material
                                useBeanIdDic.Add(exp, b.Select(o => o.ID).ToList());
                            }

                            data.AddRange(expMestMedicines);
                        }
                    }
                    if (IsNotNullOrEmpty(data))
                    {
                        List<long> ids = data.Select(o => o.MEDICINE_ID.Value).Distinct().ToList();
                        choosenMedicines = new HisMedicineGet().GetView2ByIds(ids);

                        if (!new HisServiceReqPresUtil(param).ProcessAutoChangeBhytToHospitalFee(choosenMedicines, medicinePaties, data, expMests))
                        {
                            throw new Exception("Rollback du lieu");
                        }

                        //Xu ly de ap dung chinh sach gia 3 ngay 7 ngay
                        processPackage37.Apply3Day7Day(null, null, data, presSdo.InstructionTime);
                        //Xu ly de ap dung goi de
                        processPackageBirth.Run(data, data[0].SERE_SERV_PARENT_ID);
                        //Xu ly de ap dung goi phau thuat tham my
                        processPackagePttm.Run(data, data[0].SERE_SERV_PARENT_ID, presSdo.InstructionTime);

                        List<long> choosenMedicineIds = data.Select(o => o.MEDICINE_ID.Value).ToList();
                        List<V_HIS_MEDICINE_2> vchoosenMedicines = new HisMedicineGet().GetView2ByIds(choosenMedicineIds);

                        if (!new HisServiceReqPresCheck(param).IsValidMedicineWithBidDate(vchoosenMedicines, data, expMests))
                        {
                            throw new Exception("IsValidMedicineWithBidDate false. Rollback du lieu");
                        }

                        if (!this.hisExpMestMedicineCreate.CreateList(data))
                        {
                            throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                        }

                        this.SqlUpdateBean(sessionKey, useBeanIdDic, ref sqls);

                        resultData = data;
                    }
                }
            }
            catch (Exception ex)
            {
                this.Rollback();
                LogSystem.Error(ex);
                return false;
            }
            return true;
        }

        private void SqlUpdateBean(string sessionKey, Dictionary<HIS_EXP_MEST_MEDICINE, List<long>> useBeandIdDic, ref List<string> sqls)
        {
            if (IsNotNullOrEmpty(useBeandIdDic))
            {
                if (sqls == null)
                {
                    sqls = new List<string>();
                }

                List<long> medicineBeanIds = new List<long>();
                foreach (HIS_EXP_MEST_MEDICINE expMestMedicine in useBeandIdDic.Keys)
                {
                    List<long> beanIds = useBeandIdDic[expMestMedicine];
                    //cap nhat danh sach cac bean da dung
                    string query = DAOWorker.SqlDAO.AddInClause(beanIds, "UPDATE HIS_MEDICINE_BEAN SET SESSION_KEY = NULL, SESSION_TIME = NULL, IS_ACTIVE = 0, EXP_MEST_MEDICINE_ID = {0} WHERE %IN_CLAUSE% ", "ID");
                    query = string.Format(query, expMestMedicine.ID);
                    sqls.Add(query);

                    medicineBeanIds.AddRange(beanIds);
                }

                //cap nhat danh sach cac bean ko dung
                string query2 = DAOWorker.SqlDAO.AddNotInClause(medicineBeanIds, "UPDATE HIS_MEDICINE_BEAN SET SESSION_KEY = NULL, SESSION_TIME = NULL, EXP_MEST_MEDICINE_ID = NULL, IS_ACTIVE = 1 WHERE SESSION_KEY = '{0}' AND %IN_CLAUSE% ", "ID");
                query2 = string.Format(query2, sessionKey);
                sqls.Add(query2);
            }
        }

        internal void Rollback()
        {
            this.hisExpMestMedicineCreate.RollbackData();
        }
    }
}
