using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServ.Delete;
using MOS.MANAGER.HisSereServ.Update;
using MOS.MANAGER.HisSereServ.Update.PayslipInfo;
using MOS.MANAGER.HisSereServBill;
using MOS.MANAGER.HisSereServDeposit;
using MOS.MANAGER.HisServiceReq;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Common.RecoverNotTaken
{
    class SereServProcessor : BusinessBase
    {
        private HisSereServDeleteSql hisSereServDeleteSql;
        private HisSereServCreateSql hisSereServCreateSql;
        private HisSereServUpdateSql hisSereServUpdateSql;
        private HisSereServUpdatePayslipInfo hisSereServUpdatePayslipInfo;
        private bool rollbackPayslipInfo = false;

        internal SereServProcessor()
            : base()
        {
            this.Init();
        }

        internal SereServProcessor(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisSereServDeleteSql = new HisSereServDeleteSql(param);
            this.hisSereServCreateSql = new HisSereServCreateSql(param);
            this.hisSereServUpdateSql = new HisSereServUpdateSql(param);
            this.hisSereServUpdatePayslipInfo = new HisSereServUpdatePayslipInfo(param);
        }

        internal bool Run(HIS_EXP_MEST expMest, HIS_TREATMENT treatment, List<HIS_SERE_SERV> oldSereServs, ResultMedicineData newMedicine, ResultMaterialData newMaterial, ref List<string> sqls)
        {
            bool result = false;
            try
            {
                List<HIS_SERE_SERV> inserts = new List<HIS_SERE_SERV>();
                List<HIS_SERE_SERV> deletes = new List<HIS_SERE_SERV>();
                List<HIS_SERE_SERV> updates = new List<HIS_SERE_SERV>();
                List<HIS_SERE_SERV> beforeUpdates = new List<HIS_SERE_SERV>();
                List<HIS_SERE_SERV> notChanges = new List<HIS_SERE_SERV>();

                HIS_SERVICE_REQ serviceReq = new HisServiceReqGet().GetById(expMest.SERVICE_REQ_ID.Value);

                HisSereServSetPrice priceAdder = new HisSereServSetPrice(param, treatment, null, null);
                Mapper.CreateMap<HIS_SERE_SERV, HIS_SERE_SERV>();

                this.ProcessMedicine(expMest, serviceReq, oldSereServs, newMedicine, priceAdder, ref inserts, ref deletes, ref updates, ref beforeUpdates);

                this.ProcessMaterial(expMest, serviceReq, oldSereServs, newMaterial, priceAdder, ref inserts, ref deletes, ref updates, ref beforeUpdates);

                notChanges = oldSereServs.Where(o => !deletes.Exists(e => e.ID == o.ID) && !updates.Exists(e => e.ID == o.ID)).ToList();

                this.CheckHasEdit(treatment, updates, deletes);

                this.DeleteSereServ(deletes);

                this.InsertSereServ(serviceReq, inserts);

                this.UpdateSereServ(updates, beforeUpdates);

                if (IsNotNullOrEmpty(inserts)
                    || IsNotNullOrEmpty(updates)
                    || notChanges.Any(a => a.IS_NO_EXECUTE == Constant.IS_TRUE))
                {
                    List<HIS_SERE_SERV> sereServs = new List<HIS_SERE_SERV>();
                    if (notChanges.Any(a => a.IS_NO_EXECUTE == Constant.IS_TRUE)
                        || (IsNotNullOrEmpty(updates) && updates.Any(a => a.IS_NO_EXECUTE == Constant.IS_TRUE)))
                    {
                        if (IsNotNullOrEmpty(notChanges))
                        {
                            notChanges.ForEach(o => o.IS_NO_EXECUTE = null);
                            sereServs.AddRange(notChanges);
                        }
                        if (IsNotNullOrEmpty(updates))
                        {
                            updates.ForEach(o => o.IS_NO_EXECUTE = null);
                            sereServs.AddRange(updates);
                        }
                    }
                    else if (IsNotNullOrEmpty(updates))
                    {
                        sereServs.AddRange(updates);
                    }
                    else if (IsNotNullOrEmpty(inserts))
                    {
                        sereServs.AddRange(inserts);
                    }
                    HisSereServPayslipSDO sdo = new HisSereServPayslipSDO();
                    sdo.Field = UpdateField.IS_NO_EXECUTE;
                    sdo.SereServs = sereServs;
                    sdo.TreatmentId = treatment.ID;
                    List<HIS_SERE_SERV> resultData = null;
                    //Voi api nay (trong tinh huong luon update theo service-req, tuc la update ca don,
                    //chu ko phai la update 1 vai thuoc trong don) thi cho phep cap nhat "no-execute" voi thuoc
                    result = this.hisSereServUpdatePayslipInfo.Run(sdo, treatment, true, ref resultData);
                    this.rollbackPayslipInfo = result;
                }
                else
                {
                    result = true;
                }
                if (result && (IsNotNullOrEmpty(inserts)
                    || IsNotNullOrEmpty(updates)))
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_CoSuThayDoiThongTinLoHoacGia);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
                param.HasException = true;
            }
            return result;
        }

        private void DeleteSereServ(List<HIS_SERE_SERV> deleteData)
        {
            if (IsNotNullOrEmpty(deleteData))
            {
                if (!this.hisSereServDeleteSql.Run(deleteData))
                {
                    throw new Exception("hisSereServDeleteSql. Rollback du lieu");
                }
            }
        }

        private void InsertSereServ(HIS_SERVICE_REQ serviceReq, List<HIS_SERE_SERV> insertData)
        {
            if (IsNotNullOrEmpty(insertData))
            {
                if (!this.hisSereServCreateSql.Run(insertData, serviceReq))
                {
                    throw new Exception("hisSereServCreateSql. Rollback du lieu. Ket thuc nghiep vu");
                }
            }
        }

        private void UpdateSereServ(List<HIS_SERE_SERV> updateData, List<HIS_SERE_SERV> befores)
        {
            if (IsNotNullOrEmpty(updateData))
            {
                if (!this.hisSereServUpdateSql.Run(updateData, befores))
                {
                    throw new Exception("hisSereServUpdateSql. Rollback du lieu. Ket thuc nghiep vu");
                }
            }
        }

        private void ProcessMedicine(HIS_EXP_MEST expMest, HIS_SERVICE_REQ serviceReq, List<HIS_SERE_SERV> oldSereServs, ResultMedicineData newMedicine, HisSereServSetPrice priceAdder, ref List<HIS_SERE_SERV> inserts, ref List<HIS_SERE_SERV> deletes, ref List<HIS_SERE_SERV> updates, ref List<HIS_SERE_SERV> beforeUpdates)
        {
            if (newMedicine != null)
            {
                if (IsNotNullOrEmpty(newMedicine.Inserts))
                {
                    this.MakeDataByMedicine(expMest, serviceReq, newMedicine.Inserts, newMedicine.Medicines, priceAdder, ref inserts);
                }

                if (IsNotNullOrEmpty(newMedicine.Updates))
                {
                    foreach (HIS_EXP_MEST_MEDICINE up in newMedicine.Updates)
                    {
                        HIS_EXP_MEST_MEDICINE before = newMedicine.Befores.FirstOrDefault(o => o.ID == up.ID);
                        HIS_SERE_SERV ss = null;
                        if (oldSereServs.Exists(e => e.MEDICINE_ID.HasValue && e.EXP_MEST_MEDICINE_ID.HasValue))
                        {
                            ss = oldSereServs.Where(o => o.EXP_MEST_MEDICINE_ID.HasValue && o.EXP_MEST_MEDICINE_ID.Value == before.ID).FirstOrDefault();
                        }
                        else if (oldSereServs.Exists(e => e.MEDICINE_ID.HasValue && !e.EXP_MEST_MEDICINE_ID.HasValue))
                        {
                            ss = oldSereServs.Where(t =>
                                t.AMOUNT == before.AMOUNT
                            && t.IS_EXPEND == before.IS_EXPEND
                            && t.IS_OUT_PARENT_FEE == before.IS_OUT_PARENT_FEE
                            && t.MEDICINE_ID == before.MEDICINE_ID
                            && t.PATIENT_TYPE_ID == before.PATIENT_TYPE_ID
                            && t.PARENT_ID == before.SERE_SERV_PARENT_ID
                            && t.EXPEND_TYPE_ID == before.EXPEND_TYPE_ID
                            ).FirstOrDefault();
                        }
                        if (ss == null)
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                            throw new Exception("Khong tim thay HIS_SERE_SERV tuong ung voi HIS_EXP_MEST_MEDICINE.\n" + LogUtil.TraceData("ExpMestMedicine", before));
                        }
                        if (Inventec.Common.ObjectChecker.ValueChecker.IsPrimitiveDiff<HIS_EXP_MEST_MEDICINE>(before, up))
                        {
                            HIS_SERE_SERV ssBefore = Mapper.Map<HIS_SERE_SERV>(ss);
                            V_HIS_MEDICINE_2 medicine2 = newMedicine.Medicines.Where(o => o.ID == up.MEDICINE_ID).FirstOrDefault();

                            //can set thua du lieu o day luon do phan xu ly ti le thanh toan
                            //co su dung cac truong thua du lieu
                            HisSereServUtil.SetTdl(ss, medicine2);
                            ss.PRICE = (up.PRICE ?? 0);
                            ss.VAT_RATIO = (up.VAT_RATIO ?? 0);
                            ss.MEDICINE_ID = up.MEDICINE_ID;
                            updates.Add(ss);
                            beforeUpdates.Add(ssBefore);
                        }
                    }
                }

                if (IsNotNullOrEmpty(newMedicine.DicDelete))
                {
                    foreach (var dic in newMedicine.DicDelete)
                    {
                        HIS_EXP_MEST_MEDICINE expDelete = dic.Key;
                        HIS_SERE_SERV ss = null;
                        if (oldSereServs.Exists(e => e.MEDICINE_ID.HasValue && e.EXP_MEST_MEDICINE_ID.HasValue))
                        {
                            ss = oldSereServs.Where(o => o.EXP_MEST_MEDICINE_ID.HasValue && o.EXP_MEST_MEDICINE_ID.Value == expDelete.ID).FirstOrDefault();
                        }
                        else if (oldSereServs.Exists(e => e.MEDICINE_ID.HasValue && !e.EXP_MEST_MEDICINE_ID.HasValue))
                        {
                            ss = oldSereServs.Where(t =>
                                t.AMOUNT == expDelete.AMOUNT
                            && t.IS_EXPEND == expDelete.IS_EXPEND
                            && t.IS_OUT_PARENT_FEE == expDelete.IS_OUT_PARENT_FEE
                            && t.MEDICINE_ID == expDelete.MEDICINE_ID
                            && t.PATIENT_TYPE_ID == expDelete.PATIENT_TYPE_ID
                            && t.PARENT_ID == expDelete.SERE_SERV_PARENT_ID
                            && t.EXPEND_TYPE_ID == expDelete.EXPEND_TYPE_ID
                            ).FirstOrDefault();
                        }
                        if (ss == null)
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                            throw new Exception("Khong tim thay HIS_SERE_SERV tuong ung voi HIS_EXP_MEST_MEDICINE.\n" + LogUtil.TraceData("ExpMestMedicine", expDelete));
                        }
                        deletes.Add(ss);
                    }

                }
            }
        }

        private void ProcessMaterial(HIS_EXP_MEST expMest, HIS_SERVICE_REQ serviceReq, List<HIS_SERE_SERV> oldSereServs, ResultMaterialData newMaterial, HisSereServSetPrice priceAdder, ref List<HIS_SERE_SERV> inserts, ref List<HIS_SERE_SERV> deletes, ref List<HIS_SERE_SERV> updates, ref List<HIS_SERE_SERV> beforeUpdates)
        {
            if (newMaterial != null)
            {
                if (IsNotNullOrEmpty(newMaterial.Inserts))
                {
                    this.MakeDataByMaterial(expMest, serviceReq, newMaterial.Inserts, priceAdder, ref inserts);
                }

                if (IsNotNullOrEmpty(newMaterial.Updates))
                {
                    foreach (HIS_EXP_MEST_MATERIAL up in newMaterial.Updates)
                    {
                        HIS_EXP_MEST_MATERIAL before = newMaterial.Befores.FirstOrDefault(o => o.ID == up.ID);
                        HIS_SERE_SERV ss = null;
                        if (oldSereServs.Exists(e => e.MATERIAL_ID.HasValue && e.EXP_MEST_MATERIAL_ID.HasValue))
                        {
                            ss = oldSereServs.Where(o => o.EXP_MEST_MATERIAL_ID.HasValue && o.EXP_MEST_MATERIAL_ID.Value == before.ID).FirstOrDefault();
                        }
                        else if (oldSereServs.Exists(e => e.MATERIAL_ID.HasValue && !e.EXP_MEST_MATERIAL_ID.HasValue))
                        {
                            ss = oldSereServs.Where(t =>
                                t.AMOUNT == before.AMOUNT
                            && t.IS_EXPEND == before.IS_EXPEND
                            && t.IS_OUT_PARENT_FEE == before.IS_OUT_PARENT_FEE
                            && t.MATERIAL_ID == before.MATERIAL_ID
                            && t.PATIENT_TYPE_ID == before.PATIENT_TYPE_ID
                            && t.PARENT_ID == before.SERE_SERV_PARENT_ID
                            && t.EXPEND_TYPE_ID == before.EXPEND_TYPE_ID
                            ).FirstOrDefault();
                        }
                        if (ss == null)
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                            throw new Exception("Khong tim thay HIS_SERE_SERV tuong ung voi HIS_EXP_MEST_MATERIAL.\n" + LogUtil.TraceData("ExpMestMaterial", before));
                        }
                        if (Inventec.Common.ObjectChecker.ValueChecker.IsPrimitiveDiff<HIS_EXP_MEST_MATERIAL>(before, up))
                        {
                            HIS_SERE_SERV ssBefore = Mapper.Map<HIS_SERE_SERV>(ss);
                            ss.PRICE = (up.PRICE ?? 0);
                            ss.VAT_RATIO = (up.VAT_RATIO ?? 0);
                            ss.MATERIAL_ID = up.MATERIAL_ID;
                            updates.Add(ss);
                            beforeUpdates.Add(ssBefore);
                        }
                    }
                }

                if (IsNotNullOrEmpty(newMaterial.DicDelete))
                {
                    foreach (var dic in newMaterial.DicDelete)
                    {
                        HIS_EXP_MEST_MATERIAL expDelete = dic.Key;
                        HIS_SERE_SERV ss = null;
                        if (oldSereServs.Exists(e => e.MATERIAL_ID.HasValue && e.EXP_MEST_MATERIAL_ID.HasValue))
                        {
                            ss = oldSereServs.Where(o => o.EXP_MEST_MATERIAL_ID.HasValue && o.EXP_MEST_MATERIAL_ID.Value == expDelete.ID).FirstOrDefault();
                        }
                        else if (oldSereServs.Exists(e => e.MATERIAL_ID.HasValue && !e.EXP_MEST_MATERIAL_ID.HasValue))
                        {
                            ss = oldSereServs.Where(t =>
                                t.AMOUNT == expDelete.AMOUNT
                            && t.IS_EXPEND == expDelete.IS_EXPEND
                            && t.IS_OUT_PARENT_FEE == expDelete.IS_OUT_PARENT_FEE
                            && t.MATERIAL_ID == expDelete.MATERIAL_ID
                            && t.PATIENT_TYPE_ID == expDelete.PATIENT_TYPE_ID
                            && t.PARENT_ID == expDelete.SERE_SERV_PARENT_ID
                            && t.EXPEND_TYPE_ID == expDelete.EXPEND_TYPE_ID
                            ).FirstOrDefault();
                        }
                        if (ss == null)
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                            throw new Exception("Khong tim thay HIS_SERE_SERV tuong ung voi HIS_EXP_MEST_MATERIAL.\n" + LogUtil.TraceData("ExpMestMaterial", expDelete));
                        }
                        deletes.Add(ss);
                    }

                }
            }
        }

        private void MakeDataByMedicine(HIS_EXP_MEST expMest, HIS_SERVICE_REQ serviceReq, List<HIS_EXP_MEST_MEDICINE> insertMedicines, List<V_HIS_MEDICINE_2> medicines, HisSereServSetPrice priceAdder, ref List<HIS_SERE_SERV> inserts)
        {
            if (IsNotNullOrEmpty(insertMedicines))
            {
                List<HIS_SERE_SERV> medicineSereServs = new List<HIS_SERE_SERV>();

                foreach (HIS_EXP_MEST_MEDICINE m in insertMedicines)
                {
                    V_HIS_MEDICINE_2 medicine2 = medicines.Where(o => o.ID == m.MEDICINE_ID).FirstOrDefault();

                    HIS_SERE_SERV sereServ = new HIS_SERE_SERV();
                    sereServ.SERVICE_REQ_ID = expMest.SERVICE_REQ_ID;
                    sereServ.AMOUNT = m.AMOUNT;
                    sereServ.PATIENT_TYPE_ID = m.PATIENT_TYPE_ID.Value;
                    sereServ.IS_EXPEND = m.IS_EXPEND;
                    sereServ.PRICE = m.PRICE.HasValue ? m.PRICE.Value : 0;
                    sereServ.ORIGINAL_PRICE = sereServ.PRICE;
                    sereServ.PRIMARY_PRICE = sereServ.PRICE;
                    sereServ.VAT_RATIO = m.VAT_RATIO.HasValue ? m.VAT_RATIO.Value : 0;
                    sereServ.MEDICINE_ID = m.MEDICINE_ID;
                    sereServ.PARENT_ID = m.SERE_SERV_PARENT_ID;
                    sereServ.IS_OUT_PARENT_FEE = m.IS_OUT_PARENT_FEE;
                    sereServ.SERVICE_ID = medicine2.SERVICE_ID;
                    sereServ.EXP_MEST_MEDICINE_ID = m.ID;
                    sereServ.EXPEND_TYPE_ID = m.EXPEND_TYPE_ID;
                    sereServ.USE_ORIGINAL_UNIT_FOR_PRES = m.USE_ORIGINAL_UNIT_FOR_PRES;

                    //can set thua du lieu o day luon do phan xu ly ti le thanh toan
                    //co su dung cac truong thua du lieu
                    HisSereServUtil.SetTdl(sereServ, serviceReq, medicine2);
                    priceAdder.AddPriceForNonService(sereServ, serviceReq.INTRUCTION_TIME, serviceReq.ICD_CODE, serviceReq.ICD_SUB_CODE);
                    medicineSereServs.Add(sereServ);
                }

                if (!IsNotNullOrEmpty(medicineSereServs))
                {
                    throw new Exception("Loi du lieu. Ko tao duoc sere_serv tuong ung voi exp_mest_medicine");
                }
                else
                {
                    inserts.AddRange(medicineSereServs);
                }
            }
        }

        private void MakeDataByMaterial(HIS_EXP_MEST expMest, HIS_SERVICE_REQ serviceReq, List<HIS_EXP_MEST_MATERIAL> insertMaterials, HisSereServSetPrice priceAdder, ref List<HIS_SERE_SERV> inserts)
        {
            if (IsNotNullOrEmpty(insertMaterials))
            {
                List<HIS_SERE_SERV> materialSereServs = new List<HIS_SERE_SERV>();

                foreach (HIS_EXP_MEST_MATERIAL m in insertMaterials)
                {

                    HIS_MATERIAL_TYPE materialType = HisMaterialTypeCFG.DATA.Where(o => o.ID == m.TDL_MATERIAL_TYPE_ID).FirstOrDefault();

                    if (materialType == null)  //co the do chua thuc hien reload cau hinh MOS
                    {
                        HisMaterialTypeCFG.Reload();
                        materialType = HisMaterialTypeCFG.DATA.Where(o => o.ID == m.TDL_MATERIAL_TYPE_ID).FirstOrDefault();
                    }

                    HIS_SERE_SERV sereServ = new HIS_SERE_SERV();
                    sereServ.SERVICE_REQ_ID = expMest.SERVICE_REQ_ID;
                    sereServ.AMOUNT = m.AMOUNT;
                    sereServ.PATIENT_TYPE_ID = m.PATIENT_TYPE_ID.Value;
                    sereServ.PRICE = m.PRICE.HasValue ? m.PRICE.Value : 0;
                    sereServ.ORIGINAL_PRICE = sereServ.PRICE;
                    sereServ.PRIMARY_PRICE = sereServ.PRICE;
                    sereServ.VAT_RATIO = m.VAT_RATIO.HasValue ? m.VAT_RATIO.Value : 0;
                    sereServ.MATERIAL_ID = m.MATERIAL_ID;
                    sereServ.PARENT_ID = m.SERE_SERV_PARENT_ID;
                    sereServ.IS_EXPEND = m.IS_EXPEND;
                    sereServ.IS_OUT_PARENT_FEE = m.IS_OUT_PARENT_FEE;
                    sereServ.STENT_ORDER = m.STENT_ORDER;
                    sereServ.EXP_MEST_MATERIAL_ID = m.ID;
                    sereServ.EXPEND_TYPE_ID = m.EXPEND_TYPE_ID;
                    sereServ.EQUIPMENT_SET_ID = m.EQUIPMENT_SET_ID;
                    sereServ.EQUIPMENT_SET_ORDER = m.EQUIPMENT_SET_ORDER;
                    //can set thua du lieu o day luon do phan xu ly ti le thanh toan co su dung cac truong thua du lieu
                    HisSereServUtil.SetTdl(sereServ, serviceReq, materialType);
                    priceAdder.AddPriceForNonService(sereServ, serviceReq.INTRUCTION_TIME, serviceReq.ICD_CODE, serviceReq.ICD_SUB_CODE);
                    materialSereServs.Add(sereServ);
                }

                if (!IsNotNullOrEmpty(materialSereServs))
                {
                    throw new Exception("Loi du lieu. Ko tao duoc sere_serv tuong ung voi exp_mest_material");
                }
                else
                {
                    inserts.AddRange(materialSereServs);
                }
            }
        }

        private void CheckHasEdit(HIS_TREATMENT treatment, List<HIS_SERE_SERV> updates, List<HIS_SERE_SERV> deletes)
        {

            if (IsNotNullOrEmpty(updates) || IsNotNullOrEmpty(deletes))
            {
                if (treatment.IS_ACTIVE != Constant.IS_TRUE)
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_HoSoKhoaVienPhiVaCoThayDoiThongTinLoHoacGia);
                    throw new Exception("Ho so da khoa vien phi va co thay doi thong tin lo hoac gia: \n" + LogUtil.TraceData("Updates", updates) + "\n" + LogUtil.TraceData("deletes", deletes));
                }

                List<long> sereServIds = new List<long>();
                if (IsNotNullOrEmpty(updates))
                {
                    sereServIds.AddRange(updates.Select(s => s.ID).ToList());
                }
                if (IsNotNullOrEmpty(deletes))
                {
                    sereServIds.AddRange(deletes.Select(s => s.ID).ToList());
                }

                List<HIS_SERE_SERV_BILL> sereServBills = new HisSereServBillGet().GetNoCancelBySereServIds(sereServIds);
                if (IsNotNullOrEmpty(sereServBills))
                {
                    string names = String.Join(", ", sereServBills.Select(s => s.TDL_SERVICE_NAME).ToList());
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_ThuocVatTuDaThanhToanVaCoThayDoiThongTinLoHoacGia, names);
                    throw new Exception("Thuoc/vat tu da duoc thanh toan va co thay doi thong tin lo hoac gia: \n" + LogUtil.TraceData("Updates", updates) + "\n" + LogUtil.TraceData("deletes", deletes));
                }

                List<HIS_SERE_SERV_DEPOSIT> deposits = new HisSereServDepositGet().GetNoCancelBySereServIds(sereServIds);
                if (IsNotNullOrEmpty(deposits))
                {
                    string names = String.Join(", ", deposits.Select(s => s.TDL_SERVICE_NAME).ToList());
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_ThuocVatTuDaTamUngVaCoThayDoiThongTinLoHoacGia, names);
                    throw new Exception("Thuoc/vat tu da duoc tam ung dich vu va co thay doi thong tin lo hoac gia: \n" + LogUtil.TraceData("Updates", updates) + "\n" + LogUtil.TraceData("deletes", deletes));
                }
            }

        }

        internal void Rollback()
        {
            try
            {
                if (this.rollbackPayslipInfo) this.hisSereServUpdatePayslipInfo.Rollback();
                this.hisSereServUpdateSql.Rollback();
                this.hisSereServCreateSql.Rollback();
                this.hisSereServDeleteSql.Rollback();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
