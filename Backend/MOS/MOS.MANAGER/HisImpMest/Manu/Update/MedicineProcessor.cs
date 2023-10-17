using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisImpMestMedicine;
using MOS.MANAGER.HisMedicine;
using MOS.MANAGER.HisMedicineBean;
using MOS.MANAGER.HisMedicinePaty;
using MOS.MANAGER.HisMedicineType;
using MOS.MANAGER.HisMediContractMety;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMest.Manu.Update
{
    class MedicineProcessor : BusinessBase
    {
        private HisImpMestMedicineCreate hisImpMestMedicineCreate;
        private HisImpMestMedicineUpdate hisImpMestMedicineUpdate;
        private HisMedicineUpdate hisMedicineUpdate;
        private HisMedicineCreate hisMedicineCreate;
        private HisMedicinePatyCreate hisMedicinePatyCreate;
        private HisMedicinePatyUpdate hisMedicinePatyUpdate;
        private HisMediContractMetyUpdate hisMediContractMetyUpdate;

        private List<HisMedicineWithPatySDO> outputs = new List<HisMedicineWithPatySDO>();
        private List<HIS_MEDICINE> hisMedicines;
        private List<HIS_IMP_MEST_MEDICINE> hisImpMestMedicines;

        internal MedicineProcessor(CommonParam param)
            : base(param)
        {
            this.hisImpMestMedicineCreate = new HisImpMestMedicineCreate(param);
            this.hisImpMestMedicineUpdate = new HisImpMestMedicineUpdate(param);
            this.hisMedicineCreate = new HisMedicineCreate(param);
            this.hisMedicinePatyCreate = new HisMedicinePatyCreate(param);
            this.hisMedicinePatyUpdate = new HisMedicinePatyUpdate(param);
            this.hisMedicineUpdate = new HisMedicineUpdate(param);
            this.hisMediContractMetyUpdate = new HisMediContractMetyUpdate(param);
        }

        internal bool Run(HIS_IMP_MEST impMest, List<HisMedicineWithPatySDO> manuMedicines, ref List<HisMedicineWithPatySDO> impMedicineSDOs, ref List<string> sqls)
        {
            bool result = false;
            try
            {
                this.hisImpMestMedicines = new HisImpMestMedicineGet().GetByImpMestId(impMest.ID);
                if (IsNotNullOrEmpty(this.hisImpMestMedicines))
                {
                    this.hisMedicines = new HisMedicineGet().GetByIds(this.hisImpMestMedicines.Select(s => s.MEDICINE_ID).Distinct().ToList());
                }

                this.ProcessMedicine(impMest, manuMedicines, ref sqls);

                this.ProcessImpMestMedicine(impMest, ref sqls);

                this.ProcessTruncateMedicine(ref sqls);

                if (IsNotNullOrEmpty(outputs))
                {
                    impMedicineSDOs = outputs;
                }
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        private void ProcessMedicine(HIS_IMP_MEST impMest, List<HisMedicineWithPatySDO> manuMedicines, ref List<string> sqls)
        {
            if (!IsNotNullOrEmpty(manuMedicines))
            {
                return;
            }

            List<HIS_MEDI_CONTRACT_METY> mediContractMetys = this.GetMediContractMety(manuMedicines);

            List<HIS_MEDI_CONTRACT_METY> updates = new List<HIS_MEDI_CONTRACT_METY>();
            List<HIS_MEDI_CONTRACT_METY> befores = new List<HIS_MEDI_CONTRACT_METY>();

            //Lay danh sach medicine_type de lay thong tin gia noi bo (internal_price)
            List<long> medicineTypeIds = manuMedicines.Select(o => o.Medicine.MEDICINE_TYPE_ID).ToList();
            List<HIS_MEDICINE_TYPE> hisMedicineTypes = new HisMedicineTypeGet().GetByIds(medicineTypeIds);

            Mapper.CreateMap<HIS_MEDI_CONTRACT_METY, HIS_MEDI_CONTRACT_METY>();

            foreach (var imp in manuMedicines)
            {
                HisMedicineWithPatySDO updated = new HisMedicineWithPatySDO();

                HIS_MEDICINE medicine = imp.Medicine;

                HIS_MEDICINE_TYPE medicineType = hisMedicineTypes
                        .Where(o => o.ID == medicine.MEDICINE_TYPE_ID).SingleOrDefault();

                medicine.INTERNAL_PRICE = medicineType.INTERNAL_PRICE;
                medicine.MEDICINE_REGISTER_NUMBER = !string.IsNullOrWhiteSpace(imp.Medicine.MEDICINE_REGISTER_NUMBER) ? imp.Medicine.MEDICINE_REGISTER_NUMBER : medicineType.REGISTER_NUMBER;
                medicine.MEDICINE_BYT_NUM_ORDER = medicineType.BYT_NUM_ORDER;
                medicine.MEDICINE_TCY_NUM_ORDER = medicineType.TCY_NUM_ORDER;
                medicine.MEDICINE_IS_STAR_MARK = medicineType.IS_STAR_MARK;
                medicine.ACTIVE_INGR_BHYT_CODE = !string.IsNullOrWhiteSpace(imp.Medicine.ACTIVE_INGR_BHYT_CODE) ? imp.Medicine.ACTIVE_INGR_BHYT_CODE : medicineType.ACTIVE_INGR_BHYT_CODE;
                medicine.ACTIVE_INGR_BHYT_NAME = !string.IsNullOrWhiteSpace(imp.Medicine.ACTIVE_INGR_BHYT_NAME) ? imp.Medicine.ACTIVE_INGR_BHYT_NAME : medicineType.ACTIVE_INGR_BHYT_NAME;
                medicine.SUPPLIER_ID = impMest.SUPPLIER_ID;
                medicine.IS_PREGNANT = MOS.UTILITY.Constant.IS_TRUE;


                if (medicineType.IMP_UNIT_ID.HasValue && (!medicineType.IMP_UNIT_CONVERT_RATIO.HasValue || medicineType.IMP_UNIT_CONVERT_RATIO.Value <= 0))
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    throw new Exception("medicineType.IMP_UNIT_CONVERT_RATIO <= 0");
                }

                if (medicineType.IMP_UNIT_ID.HasValue)
                {
                    medicine.IMP_UNIT_AMOUNT = medicine.AMOUNT;
                    medicine.IMP_UNIT_PRICE = medicine.IMP_PRICE;
                    medicine.AMOUNT = medicine.IMP_UNIT_AMOUNT.Value * medicineType.IMP_UNIT_CONVERT_RATIO.Value;
                    medicine.IMP_PRICE = medicine.IMP_UNIT_PRICE.Value / medicineType.IMP_UNIT_CONVERT_RATIO.Value;
                }
                else
                {
                    medicine.IMP_UNIT_AMOUNT = null;
                    medicine.IMP_UNIT_PRICE = null;

                }

                if (medicine.MEDICAL_CONTRACT_ID.HasValue)
                {
                    HIS_MEDI_CONTRACT_METY contractMety = mediContractMetys.FirstOrDefault(o => o.MEDICAL_CONTRACT_ID == medicine.MEDICAL_CONTRACT_ID.Value && o.MEDICINE_TYPE_ID == medicine.MEDICINE_TYPE_ID && o.CONTRACT_PRICE == medicine.CONTRACT_PRICE);
                    if (contractMety == null)
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception(String.Format("Khong ton tai HIS_MEDI_CONTRACT_METY tuong ung voi MEDICAL_CONTRACT_ID: {0} va MEDICINE_TYPE_ID: {0} cua lo thuoc.", medicine.MEDICAL_CONTRACT_ID.Value, medicine.MEDICINE_TYPE_ID));
                    }

                    if (medicine.IMP_VAT_RATIO > 0
                        && (!contractMety.IMP_VAT_RATIO.HasValue || contractMety.IMP_VAT_RATIO.Value == 0))
                    {
                        HIS_MEDI_CONTRACT_METY before = Mapper.Map<HIS_MEDI_CONTRACT_METY>(contractMety);
                        contractMety.IMP_VAT_RATIO = medicine.IMP_VAT_RATIO;
                        if (contractMety.CONTRACT_PRICE.HasValue)
                            contractMety.IMP_PRICE = contractMety.CONTRACT_PRICE.Value / (decimal)(1 + contractMety.IMP_VAT_RATIO.Value);
                        updates.Add(contractMety);
                        befores.Add(before);
                    }
                }

                HisMedicineUtil.SetTdl(medicine, medicineType);
                HisMedicineUtil.SetTdl(medicine, impMest);

                if (medicine.ID > 0)
                {
                    HIS_MEDICINE oldMedicine = this.hisMedicines != null ? this.hisMedicines.FirstOrDefault(o => o.ID == medicine.ID) : null;
                    if (oldMedicine == null)
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Khong lay duoc Medicine theo Id: " + medicine.ID);
                    }
                    if (medicine.MEDICINE_TYPE_ID != oldMedicine.MEDICINE_TYPE_ID)
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Khong cho sua loai cua lo thuoc " + LogUtil.TraceData("oldMedicine", oldMedicine));
                    }
                    if (HisMedicineUtil.CheckIsDiff(medicine, oldMedicine))
                    {
                        if (!this.hisMedicineUpdate.Update(medicine))
                        {
                            throw new Exception("Update HIS_MEDICINE that bai");
                        }
                    }
                }
                else
                {
                    if (!this.hisMedicineCreate.Create(medicine))
                    {
                        throw new Exception("Tao HIS_MEDICINE that bai");
                    }
                }
                updated.Medicine = medicine;
                updated.Temperature = imp.Temperature;

                #region Xy ly thong tin chinh sach gia
                List<HIS_MEDICINE_PATY> listToInserted = new List<HIS_MEDICINE_PATY>();
                List<HIS_MEDICINE_PATY> listToUpdate = new List<HIS_MEDICINE_PATY>();
                List<HIS_MEDICINE_PATY> listToDelete = null;
                List<HIS_MEDICINE_PATY> existedMedicinePatys = new HisMedicinePatyGet().GetByMedicineId(medicine.ID);

                List<HIS_MEDICINE_PATY> medicinePatys = imp.MedicinePaties;
                if (IsNotNullOrEmpty(medicinePatys))
                {
                    //Cap nhat medicine_id cho cac medicine_paty
                    if (medicineType.IMP_UNIT_ID.HasValue)
                    {
                        medicinePatys.ForEach(o =>
                            {
                                o.IMP_UNIT_EXP_PRICE = o.EXP_PRICE;
                                o.EXP_PRICE = o.EXP_PRICE / medicineType.IMP_UNIT_CONVERT_RATIO.Value;
                            });
                    }
                    else
                    {
                        medicinePatys.ForEach(o =>
                        {
                            o.IMP_UNIT_EXP_PRICE = null;
                        });
                    }

                    foreach (var mp in medicinePatys)
                    {
                        mp.MEDICINE_ID = medicine.ID;
                        var old = existedMedicinePatys != null ? existedMedicinePatys.FirstOrDefault(o => o.PATIENT_TYPE_ID == mp.PATIENT_TYPE_ID) : null;
                        if (old != null)
                        {
                            mp.ID = old.ID;
                            listToUpdate.Add(mp);
                        }
                        else
                        {
                            listToInserted.Add(mp);
                        }
                    }
                }

                List<long> medicinePatyIds = IsNotNullOrEmpty(medicinePatys) ? medicinePatys.Select(o => o.ID).ToList() : null;

                if (existedMedicinePatys != null)
                {
                    List<long> existedMedicinePatyIds = existedMedicinePatys.Select(o => o.ID).ToList();
                    //D/s can delete la d/s ton tai tren he thong nhung ko co trong d/s gui tu client
                    listToDelete = existedMedicinePatys.Where(o => medicinePatyIds == null || !medicinePatyIds.Contains(o.ID)).ToList();
                }

                if (this.IsNotNullOrEmpty(listToInserted))
                {
                    if (!this.hisMedicinePatyCreate.CreateList(listToInserted))
                    {
                        throw new Exception("Tao HIS_MEDICINE_PATY that bai");
                    }
                }
                if (this.IsNotNullOrEmpty(listToUpdate))
                {
                    if (!this.hisMedicinePatyUpdate.UpdateList(listToUpdate))
                    {
                        throw new Exception("Update HIS_MEDICINE_PATY that bai");
                    }
                }
                if (IsNotNullOrEmpty(listToDelete))
                {
                    string sql = DAOWorker.SqlDAO.AddInClause(listToDelete.Select(s => s.ID).ToList(), "DELETE HIS_MEDICINE_PATY WHERE %IN_CLAUSE% ", "ID");
                    sqls.Add(sql);
                    //if (!this.hisMedicinePatyTruncate.TruncateList(listToDelete))
                    //{
                    //    throw new Exception("Xoa HIS_MEDICINE_PATY that bai");
                    //}
                }
                #endregion

                if (IsNotNullOrEmpty(updates) && !this.hisMediContractMetyUpdate.UpdateList(updates, befores))
                {
                    throw new Exception("Update HIS_MEDI_CONTRACT_METY that bai");
                }

                updated.MedicinePaties = medicinePatys;
                outputs.Add(updated);
            }
        }

        private void ProcessImpMestMedicine(HIS_IMP_MEST impMest, ref List<string> sqls)
        {
            List<HIS_IMP_MEST_MEDICINE> listNew = new List<HIS_IMP_MEST_MEDICINE>();
            List<HIS_IMP_MEST_MEDICINE> listToDelete = new List<HIS_IMP_MEST_MEDICINE>();
            List<HIS_IMP_MEST_MEDICINE> listToCreate = new List<HIS_IMP_MEST_MEDICINE>();
            List<HIS_IMP_MEST_MEDICINE> listToUpdate = new List<HIS_IMP_MEST_MEDICINE>();

            if (IsNotNullOrEmpty(this.outputs))
            {
                foreach (HisMedicineWithPatySDO sdo in this.outputs)
                {
                    HIS_IMP_MEST_MEDICINE impMestMedicine = this.hisImpMestMedicines != null ? this.hisImpMestMedicines.FirstOrDefault(o => o.MEDICINE_ID == sdo.Medicine.ID) : null;
                    if (impMestMedicine != null)
                    {
                        if (impMestMedicine.AMOUNT != sdo.Medicine.AMOUNT
                            || impMestMedicine.PRICE != sdo.Medicine.IMP_PRICE
                            || impMestMedicine.VAT_RATIO != sdo.Medicine.IMP_VAT_RATIO
                            || impMestMedicine.DOCUMENT_PRICE != sdo.Medicine.DOCUMENT_PRICE
                            || impMestMedicine.IMP_UNIT_AMOUNT != sdo.Medicine.IMP_UNIT_AMOUNT
                            || impMestMedicine.IMP_UNIT_PRICE != sdo.Medicine.IMP_UNIT_PRICE
                            || impMestMedicine.TDL_IMP_UNIT_CONVERT_RATIO != sdo.Medicine.TDL_IMP_UNIT_CONVERT_RATIO
                            || impMestMedicine.TDL_IMP_UNIT_ID != sdo.Medicine.TDL_IMP_UNIT_ID
                            || impMestMedicine.CONTRACT_PRICE != sdo.Medicine.CONTRACT_PRICE
                            || impMestMedicine.TEMPERATURE != sdo.Temperature)
                        {
                            impMestMedicine.AMOUNT = sdo.Medicine.AMOUNT;
                            impMestMedicine.PRICE = sdo.Medicine.IMP_PRICE;
                            impMestMedicine.DOCUMENT_PRICE = sdo.Medicine.DOCUMENT_PRICE;
                            impMestMedicine.VAT_RATIO = sdo.Medicine.IMP_VAT_RATIO;
                            impMestMedicine.IMP_UNIT_AMOUNT = sdo.Medicine.IMP_UNIT_AMOUNT;
                            impMestMedicine.IMP_UNIT_PRICE = sdo.Medicine.IMP_UNIT_PRICE;
                            impMestMedicine.TDL_IMP_UNIT_CONVERT_RATIO = sdo.Medicine.TDL_IMP_UNIT_CONVERT_RATIO;
                            impMestMedicine.TDL_IMP_UNIT_ID = sdo.Medicine.TDL_IMP_UNIT_ID;
                            impMestMedicine.CONTRACT_PRICE = sdo.Medicine.CONTRACT_PRICE;
                            impMestMedicine.TEMPERATURE = sdo.Temperature;
                            listToUpdate.Add(impMestMedicine);
                        }
                    }
                    else
                    {
                        impMestMedicine = new HIS_IMP_MEST_MEDICINE();
                        impMestMedicine.IMP_MEST_ID = impMest.ID;
                        impMestMedicine.MEDICINE_ID = sdo.Medicine.ID;
                        impMestMedicine.AMOUNT = sdo.Medicine.AMOUNT;
                        impMestMedicine.DOCUMENT_PRICE = sdo.Medicine.DOCUMENT_PRICE;
                        impMestMedicine.PRICE = sdo.Medicine.IMP_PRICE;
                        impMestMedicine.VAT_RATIO = sdo.Medicine.IMP_VAT_RATIO;
                        impMestMedicine.IMP_UNIT_AMOUNT = sdo.Medicine.IMP_UNIT_AMOUNT;
                        impMestMedicine.IMP_UNIT_PRICE = sdo.Medicine.IMP_UNIT_PRICE;
                        impMestMedicine.TDL_IMP_UNIT_CONVERT_RATIO = sdo.Medicine.TDL_IMP_UNIT_CONVERT_RATIO;
                        impMestMedicine.TDL_IMP_UNIT_ID = sdo.Medicine.TDL_IMP_UNIT_ID;
                        impMestMedicine.CONTRACT_PRICE = sdo.Medicine.CONTRACT_PRICE;
                        impMestMedicine.TEMPERATURE = sdo.Temperature;
                        listToCreate.Add(impMestMedicine);
                    }
                    listNew.Add(impMestMedicine);
                }
            }

            List<long> updateIds = listNew.Select(o => o.ID).Distinct().ToList();

            //lay ra danh sach can xoa la danh sach co trong he thong ma ko co trong danh sach y/c tu client
            listToDelete = this.hisImpMestMedicines != null ? this.hisImpMestMedicines.Where(o => updateIds == null || !updateIds.Contains(o.ID)).ToList() : null;
            if (IsNotNullOrEmpty(listToCreate))
            {
                if (!this.hisImpMestMedicineCreate.CreateList(listToCreate))
                {
                    throw new Exception("Tao HIS_IMP_MEST_MEDICINE that bai");
                }
            }

            if (IsNotNullOrEmpty(listToUpdate))
            {
                if (!this.hisImpMestMedicineUpdate.UpdateList(listToUpdate))
                {
                    throw new Exception("Update HIS_IMP_MEST_MEDICINE that bai");
                }
            }

            if (IsNotNullOrEmpty(listToDelete))
            {
                string sql = DAOWorker.SqlDAO.AddInClause(listToDelete.Select(s => s.ID).ToList(), "DELETE HIS_IMP_MEST_MEDICINE WHERE %IN_CLAUSE% ", "ID");
                sqls.Add(sql);
            }

        }

        private void ProcessTruncateMedicine(ref List<string> sqls)
        {
            List<HIS_MEDICINE_BEAN> needToDeleteMedicineBeans = new List<HIS_MEDICINE_BEAN>();
            List<HIS_MEDICINE_PATY> needToDeleteMedicinePatys = new List<HIS_MEDICINE_PATY>();
            List<HIS_MEDICINE> needToDeleteMedicines = this.hisMedicines != null ? this.hisMedicines.Where(o => !this.outputs.Exists(e => e.Medicine.ID == o.ID)).ToList() : null;

            if (needToDeleteMedicines != null && needToDeleteMedicines.Count > 0)
            {
                //Lay ra danh sach medicine_bean va medicine_paty can xoa tuong ung
                List<long> needToDeleteMedicineIds = needToDeleteMedicines.Select(o => o.ID).ToList();
                needToDeleteMedicineBeans = new HisMedicineBeanGet().GetByMedicineIds(needToDeleteMedicineIds);
                needToDeleteMedicinePatys = new HisMedicinePatyGet().GetByMedicineIds(needToDeleteMedicineIds);
            }

            if (IsNotNullOrEmpty(needToDeleteMedicineBeans))
            {
                string sql = DAOWorker.SqlDAO.AddInClause(needToDeleteMedicineBeans.Select(s => s.ID).ToList(), "DELETE HIS_MEDICINE_BEAN WHERE %IN_CLAUSE% ", "ID");
                sqls.Add(sql);
            }

            if (IsNotNullOrEmpty(needToDeleteMedicinePatys))
            {
                string sql = DAOWorker.SqlDAO.AddInClause(needToDeleteMedicinePatys.Select(s => s.ID).ToList(), "DELETE HIS_MEDICINE_PATY WHERE %IN_CLAUSE% ", "ID");
                sqls.Add(sql);
            }

            if (IsNotNullOrEmpty(needToDeleteMedicines))
            {
                string sql = DAOWorker.SqlDAO.AddInClause(needToDeleteMedicines.Select(s => s.ID).ToList(), "DELETE HIS_MEDICINE WHERE %IN_CLAUSE% ", "ID");
                sqls.Add(sql);
            }
        }

        private List<HIS_MEDI_CONTRACT_METY> GetMediContractMety(List<HisMedicineWithPatySDO> manuMedicines)
        {
            if (!manuMedicines.Any(a => a.Medicine.MEDICAL_CONTRACT_ID.HasValue))
            {
                return null;
            }

            List<long> medicalContractIds = manuMedicines.Where(o => o.Medicine.MEDICAL_CONTRACT_ID.HasValue).Select(s => s.Medicine.MEDICAL_CONTRACT_ID.Value).Distinct().ToList();

            return new HisMediContractMetyGet().GetByMedicalContractIds(medicalContractIds);
        }

        internal void Rollback()
        {
            try
            {
                this.hisImpMestMedicineUpdate.RollbackData();
                this.hisImpMestMedicineCreate.RollbackData();
                this.hisMediContractMetyUpdate.RollbackData();
                this.hisMedicinePatyUpdate.RollbackData();
                this.hisMedicinePatyCreate.RollbackData();
                this.hisMedicineUpdate.RollbackData();
                this.hisMedicineCreate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
