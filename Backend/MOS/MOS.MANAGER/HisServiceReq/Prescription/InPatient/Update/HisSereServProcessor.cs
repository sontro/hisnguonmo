using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisMaterial;
using MOS.MANAGER.HisMedicine;
using MOS.MANAGER.HisMedicineBean;
using MOS.MANAGER.HisMedicineBean.Handle;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServ.Delete;
using MOS.MANAGER.HisServiceReq.Common;
using MOS.SDO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Prescription.InPatient.Update
{
    class HisSereServProcessor : BusinessBase
    {
        private HisSereServUpdate hisSereServUpdate;
        private HisSereServCreateSql hisSereServCreate;
        private HisSereServDeleteSql hisSereServDeleteSql;

        internal HisSereServProcessor()
            : base()
        {
            this.Init();
        }

        internal HisSereServProcessor(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisSereServUpdate = new HisSereServUpdate(param);
            this.hisSereServCreate = new HisSereServCreateSql(param);
            this.hisSereServDeleteSql = new HisSereServDeleteSql(param);
        }

        internal bool Run(HIS_TREATMENT treatment, List<HIS_SERE_SERV> existSereServs, List<HIS_PATIENT_TYPE_ALTER> ptas, HIS_SERVICE_REQ serviceReq, HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MEDICINE> insertMedicines, List<HIS_EXP_MEST_MEDICINE> deleteMedicines, List<HIS_EXP_MEST_MATERIAL> insertMaterials, List<HIS_EXP_MEST_MATERIAL> deleteMaterials, List<V_HIS_MEDICINE_2> newsMedicines, ref List<HIS_SERE_SERV> resultData)
        {
            try
            {
                if (IsNotNullOrEmpty(insertMedicines) || IsNotNullOrEmpty(deleteMedicines) || IsNotNullOrEmpty(insertMaterials) || IsNotNullOrEmpty(deleteMaterials))
                {
                    //List<HIS_SERE_SERV> existSereServs = new HisSereServGet().GetByTreatmentId(treatment.ID);
                    List<HIS_SERE_SERV> insertData = new List<HIS_SERE_SERV>();
                    List<HIS_SERE_SERV> deleteData = new List<HIS_SERE_SERV>();

                    //Gia lo thuoc/vat tu da duoc xu ly khi tao thong tin phieu xuat
                    //nen ko can truyen vao trong priceAdder
                    HisSereServSetPrice priceAdder = new HisSereServSetPrice(param, treatment, null, null);

                    this.DataToDelete(serviceReq.ID, existSereServs, deleteMedicines, deleteMaterials, ref deleteData);
                    this.DataToInsert(treatment, priceAdder, serviceReq, expMest, insertMaterials, ref insertData);
                    this.DataToInsert(treatment, priceAdder, serviceReq, expMest, insertMedicines, newsMedicines, ref insertData);
                    this.ProcessSereServ(treatment, ptas, serviceReq, existSereServs, insertData, deleteData);

                    this.PassResult(existSereServs, insertData, deleteData, ref resultData);
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

        private void PassResult(List<HIS_SERE_SERV> olds, List<HIS_SERE_SERV> inserts, List<HIS_SERE_SERV> deletes, ref List<HIS_SERE_SERV> resultData)
        {
            if (IsNotNullOrEmpty(inserts) || IsNotNullOrEmpty(olds))
            {
                resultData = new List<HIS_SERE_SERV>();
                if (IsNotNullOrEmpty(inserts))
                {
                    resultData.AddRange(inserts);
                }
                if (IsNotNullOrEmpty(olds))
                {
                    List<HIS_SERE_SERV> remains = olds;
                    remains.RemoveAll(o => deletes != null && deletes.Exists(t => t.ID == o.ID));
                    resultData.AddRange(remains);
                }
            }
        }

        private void ProcessSereServ(HIS_TREATMENT treatment, List<HIS_PATIENT_TYPE_ALTER> ptas, HIS_SERVICE_REQ serviceReq, List<HIS_SERE_SERV> existSereServs, List<HIS_SERE_SERV> insertData, List<HIS_SERE_SERV> deleteData)
        {
            if (IsNotNullOrEmpty(insertData) || IsNotNullOrEmpty(deleteData))
            {
                List<HIS_SERE_SERV> remainData = new List<HIS_SERE_SERV>();

                //Can xoa sere_serv truoc roi moi insert de viec tinh toan lai ti le BHYT duoc chinh xac
                this.DeleteSereServ(existSereServs, deleteData, ref remainData);
                this.InsertSereServ(treatment, ptas, serviceReq, insertData, remainData);
            }
        }

        private void DeleteSereServ(List<HIS_SERE_SERV> existSereServs, List<HIS_SERE_SERV> deleteData, ref List<HIS_SERE_SERV> remainData)
        {
            remainData = existSereServs;

            if (IsNotNullOrEmpty(deleteData))
            {
                if (!this.hisSereServDeleteSql.Run(deleteData))
                {
                    throw new Exception("Rollback du lieu");
                }
                remainData.RemoveAll(o => deleteData.Exists(t => t.ID == o.ID));
            }
        }

        private void InsertSereServ(HIS_TREATMENT treatment, List<HIS_PATIENT_TYPE_ALTER> ptas, HIS_SERVICE_REQ serviceReq, List<HIS_SERE_SERV> insertData, List<HIS_SERE_SERV> existSereServs)
        {
            if (IsNotNullOrEmpty(insertData) || IsNotNullOrEmpty(existSereServs))
            {
                //Tao ID "fake" de dinh danh cac sere_serv chua co trong DB
                long maxId = IsNotNullOrEmpty(existSereServs) ? existSereServs.Max(o => o.ID) : 0;
                long maxExistedSereServId = maxId;
                insertData.ForEach(o => o.ID = ++maxId);

                //Xu ly de set thong tin ti le chi tra, doi tuong va lay thong tin thay doi
                List<HIS_SERE_SERV> toUpdateData = new List<HIS_SERE_SERV>();
                if (IsNotNullOrEmpty(insertData))
                {
                    toUpdateData.AddRange(insertData);
                }
                if (IsNotNullOrEmpty(existSereServs))
                {
                    toUpdateData.AddRange(existSereServs);
                }

                List<HIS_SERE_SERV> changeRecords = null;
                List<HIS_SERE_SERV> oldOfChangeRecords = null;

                if (!new HisSereServUpdateHein(param, treatment, ptas, false).Update(existSereServs, toUpdateData, ref changeRecords, ref oldOfChangeRecords))
                {
                    throw new Exception("Rollback du lieu");
                }

                List<HIS_SERE_SERV> toUpdates = IsNotNullOrEmpty(changeRecords) ? changeRecords.Where(o => o.ID <= maxExistedSereServId).ToList() : null;

                if (IsNotNullOrEmpty(insertData))
                {
                    if (!this.hisSereServCreate.Run(insertData, serviceReq))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }
                }

                if (IsNotNullOrEmpty(toUpdates))
                {
                    List<HIS_SERE_SERV> olds = oldOfChangeRecords.Where(o => o.ID <= maxExistedSereServId).ToList();
                    //tao thread moi de update sere_serv cu~
                    Thread thread = new Thread(new ParameterizedThreadStart(this.ThreadProcessUpdateSereServ));
                    thread.Priority = ThreadPriority.Highest;
                    UpdateSereServThreadData threadData = new UpdateSereServThreadData();
                    threadData.SereServs = toUpdates;
                    thread.Start(threadData);
                }
            }
        }

        private void ThreadProcessUpdateSereServ(object threadData)
        {
            try
            {
                UpdateSereServThreadData td = (UpdateSereServThreadData)threadData;
                List<HIS_SERE_SERV> sereServs = td.SereServs;

                if (!this.hisSereServUpdate.UpdateRaw(sereServs))
                {
                    LogSystem.Error("Cap nhat lai ti le BHYT cua sere_serv that bai");
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        /// <summary>
        /// Lay cac sere_serv_id can delete dua vao danh sach exp_mest_medicine/material can xoa
        /// </summary>
        /// <param name="serviceReqId"></param>
        /// <param name="exists"></param>
        /// <param name="deleteMedicines"></param>
        /// <param name="deleteMaterials"></param>
        /// <param name="deleteData"></param>
        private void DataToDelete(long serviceReqId, List<HIS_SERE_SERV> exists, List<HIS_EXP_MEST_MEDICINE> deleteMedicines, List<HIS_EXP_MEST_MATERIAL> deleteMaterials, ref List<HIS_SERE_SERV> deleteData)
        {
            if (IsNotNullOrEmpty(exists))
            {
                deleteData = new List<HIS_SERE_SERV>();
                if (IsNotNullOrEmpty(deleteMedicines))
                {
                    foreach (HIS_EXP_MEST_MEDICINE medicine in deleteMedicines)
                    {
                        HIS_SERE_SERV tmp = null;
                        if (exists.Exists(e => e.SERVICE_REQ_ID == serviceReqId && e.MEDICINE_ID.HasValue && e.EXP_MEST_MEDICINE_ID.HasValue))
                        {
                            tmp = exists.Where(o => o.SERVICE_REQ_ID == serviceReqId && o.EXP_MEST_MEDICINE_ID.HasValue && o.EXP_MEST_MEDICINE_ID.Value == medicine.ID).FirstOrDefault();
                        }
                        else if (exists.Exists(e => e.MEDICINE_ID.HasValue && !e.EXP_MEST_MEDICINE_ID.HasValue))
                        {
                            tmp = exists.Where(t => t.SERVICE_REQ_ID == serviceReqId
                            && t.AMOUNT == medicine.AMOUNT
                            && t.IS_EXPEND == medicine.IS_EXPEND
                            && t.SERVICE_CONDITION_ID == medicine.SERVICE_CONDITION_ID
                            && t.OTHER_PAY_SOURCE_ID == medicine.OTHER_PAY_SOURCE_ID
                            && t.IS_OUT_PARENT_FEE == medicine.IS_OUT_PARENT_FEE
                            && t.MEDICINE_ID == medicine.MEDICINE_ID
                            && t.PATIENT_TYPE_ID == medicine.PATIENT_TYPE_ID
                            && t.EXPEND_TYPE_ID == medicine.EXPEND_TYPE_ID
                            && t.PARENT_ID == medicine.SERE_SERV_PARENT_ID).FirstOrDefault();
                        }
                        if (tmp != null)
                        {
                            deleteData.Add(tmp);
                        }
                    }
                }

                if (IsNotNullOrEmpty(deleteMaterials))
                {
                    foreach (HIS_EXP_MEST_MATERIAL material in deleteMaterials)
                    {
                        HIS_SERE_SERV tmp = null;
                        if (exists.Exists(e => e.SERVICE_REQ_ID == serviceReqId && e.MATERIAL_ID.HasValue && e.EXP_MEST_MATERIAL_ID.HasValue))
                        {
                            tmp = exists.Where(o => o.SERVICE_REQ_ID == serviceReqId && o.EXP_MEST_MATERIAL_ID.HasValue && o.EXP_MEST_MATERIAL_ID.Value == material.ID).FirstOrDefault();
                        }
                        else if (exists.Exists(e => e.MATERIAL_ID.HasValue && !e.EXP_MEST_MATERIAL_ID.HasValue))
                        {
                            tmp = exists.Where(t => t.SERVICE_REQ_ID == serviceReqId
                            && t.AMOUNT == material.AMOUNT
                            && t.IS_EXPEND == material.IS_EXPEND
                            && t.SERVICE_CONDITION_ID == material.SERVICE_CONDITION_ID
                            && t.OTHER_PAY_SOURCE_ID == material.OTHER_PAY_SOURCE_ID
                            && t.IS_OUT_PARENT_FEE == material.IS_OUT_PARENT_FEE
                            && t.MATERIAL_ID == material.MATERIAL_ID
                            && t.PATIENT_TYPE_ID == material.PATIENT_TYPE_ID
                            && t.EXPEND_TYPE_ID == material.EXPEND_TYPE_ID
                            && t.PARENT_ID == material.SERE_SERV_PARENT_ID
                            && t.STENT_ORDER == material.STENT_ORDER
                            && t.EQUIPMENT_SET_ID == material.EQUIPMENT_SET_ID
                            && t.EQUIPMENT_SET_ORDER == material.EQUIPMENT_SET_ORDER
                            ).FirstOrDefault();
                        }
                        if (tmp != null)
                        {
                            deleteData.Add(tmp);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Tao ra du lieu sere_serv can insert dua vao danh sach exp_mest_medicine
        /// </summary>
        /// <param name="priceAdder"></param>
        /// <param name="serviceReq"></param>
        /// <param name="expMest"></param>
        /// <param name="insertMedicines"></param>
        /// <param name="sereServs"></param>
        private void DataToInsert(HIS_TREATMENT treatment, HisSereServSetPrice priceAdder, HIS_SERVICE_REQ serviceReq, HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MEDICINE> insertMedicines, List<V_HIS_MEDICINE_2> newsMedicines, ref List<HIS_SERE_SERV> sereServs)
        {
            if (IsNotNullOrEmpty(insertMedicines) && IsNotNullOrEmpty(newsMedicines))
            {
                foreach (HIS_EXP_MEST_MEDICINE m in insertMedicines)
                {
                    V_HIS_MEDICINE_2 medicine2 = newsMedicines.Where(o => o.ID == m.MEDICINE_ID).FirstOrDefault();
                    HIS_MEDICINE_TYPE medicineType = HisMedicineTypeCFG.DATA.FirstOrDefault(o => o.ID == m.TDL_MEDICINE_TYPE_ID);
                    if (medicineType == null)
                    {
                        HisMedicineTypeCFG.Reload();
                        medicineType = HisMedicineTypeCFG.DATA.Where(o => o.ID == m.TDL_MEDICINE_TYPE_ID).FirstOrDefault();
                    }

                    HIS_SERE_SERV sereServ = new HIS_SERE_SERV();
                    sereServ.SERVICE_REQ_ID = expMest.SERVICE_REQ_ID;
                    sereServ.AMOUNT = m.AMOUNT;
                    sereServ.PATIENT_TYPE_ID = m.PATIENT_TYPE_ID.Value;
                    sereServ.IS_EXPEND = m.IS_EXPEND;
                    sereServ.SERVICE_CONDITION_ID = m.SERVICE_CONDITION_ID;
                    sereServ.OTHER_PAY_SOURCE_ID = m.OTHER_PAY_SOURCE_ID;
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
                    sereServ.IS_NOT_PRES = m.IS_NOT_PRES;
                    sereServ.TDL_IS_VACCINE = medicineType.IS_VACCINE;

                    //Can set vao o day de phuc vu xu ly tinh toan ti le BHYT o ham AddPriceForNonService
                    sereServ.HEIN_CARD_NUMBER = sereServ.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT ? serviceReq.TDL_HEIN_CARD_NUMBER : null;

                    //can set thua du lieu o day luon do phan xu ly ti le thanh toan
                    //co su dung cac truong thua du lieu
                    HisSereServUtil.SetTdl(sereServ, serviceReq, medicine2);
                    priceAdder.AddPriceForNonService(sereServ, serviceReq.INTRUCTION_TIME, serviceReq.ICD_CODE, serviceReq.ICD_SUB_CODE);
                    sereServs.Add(sereServ);
                }
            }
        }

        /// <summary>
        /// Tao ra du lieu sere_serv can insert dua vao danh sach exp_mest_material
        /// </summary>
        /// <param name="priceAdder"></param>
        /// <param name="serviceReq"></param>
        /// <param name="expMest"></param>
        /// <param name="insertMaterials"></param>
        /// <param name="sereServs"></param>
        private void DataToInsert(HIS_TREATMENT treatment, HisSereServSetPrice priceAdder, HIS_SERVICE_REQ serviceReq, HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MATERIAL> insertMaterials, ref List<HIS_SERE_SERV> sereServs)
        {
            if (IsNotNullOrEmpty(insertMaterials))
            {
                foreach (HIS_EXP_MEST_MATERIAL m in insertMaterials)
                {
                    HIS_MATERIAL_TYPE materialType = HisMaterialTypeCFG.DATA.Where(o => o.ID == m.TDL_MATERIAL_TYPE_ID).FirstOrDefault();
                    if (materialType == null)
                    {
                        HisMaterialTypeCFG.Reload();
                        materialType = HisMaterialTypeCFG.DATA.Where(o => o.ID == m.TDL_MATERIAL_TYPE_ID).FirstOrDefault();
                    }
                    HIS_SERE_SERV sereServ = new HIS_SERE_SERV();
                    sereServ.SERVICE_REQ_ID = expMest.SERVICE_REQ_ID;
                    sereServ.AMOUNT = m.AMOUNT;
                    sereServ.PATIENT_TYPE_ID = m.PATIENT_TYPE_ID.Value;
                    sereServ.IS_EXPEND = m.IS_EXPEND;
                    sereServ.SERVICE_CONDITION_ID = m.SERVICE_CONDITION_ID;
                    sereServ.OTHER_PAY_SOURCE_ID = m.OTHER_PAY_SOURCE_ID;
                    sereServ.PRICE = m.PRICE.HasValue ? m.PRICE.Value : 0;
                    sereServ.ORIGINAL_PRICE = sereServ.PRICE;
                    sereServ.PRIMARY_PRICE = sereServ.PRICE;
                    sereServ.VAT_RATIO = m.VAT_RATIO.HasValue ? m.VAT_RATIO.Value : 0;
                    sereServ.MATERIAL_ID = m.MATERIAL_ID;
                    sereServ.PARENT_ID = m.SERE_SERV_PARENT_ID;
                    sereServ.IS_OUT_PARENT_FEE = m.IS_OUT_PARENT_FEE;
                    sereServ.STENT_ORDER = m.STENT_ORDER;
                    sereServ.EXP_MEST_MATERIAL_ID = m.ID;
                    sereServ.EQUIPMENT_SET_ID = m.EQUIPMENT_SET_ID;
                    sereServ.EQUIPMENT_SET_ORDER = m.EQUIPMENT_SET_ORDER;
                    sereServ.EXPEND_TYPE_ID = m.EXPEND_TYPE_ID;
                    sereServ.IS_NOT_PRES = m.IS_NOT_PRES;

                    //Can set vao o day de phuc vu xu ly tinh toan ti le BHYT o ham AddPriceForNonService
                    sereServ.HEIN_CARD_NUMBER = sereServ.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT ? serviceReq.TDL_HEIN_CARD_NUMBER : null;

                    //can set thua du lieu o day luon do phan xu ly ti le thanh toan co su dung cac truong thua du lieu
                    HisSereServUtil.SetTdl(sereServ, serviceReq, materialType);
                    priceAdder.AddPriceForNonService(sereServ, serviceReq.INTRUCTION_TIME, serviceReq.ICD_CODE, serviceReq.ICD_SUB_CODE);
                    sereServs.Add(sereServ);
                }
            }
        }

        internal void Rollback()
        {
            this.hisSereServCreate.Rollback();
            this.hisSereServDeleteSql.Rollback();
            this.hisSereServUpdate.RollbackData();
        }
    }
}
