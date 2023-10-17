using AutoMapper;
using Inventec.Common.ObjectChecker;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisImpMest;
using MOS.MANAGER.HisImpMestMedicine;
using MOS.MANAGER.HisMedicine;
using MOS.MANAGER.HisMedicinePaty;
using MOS.MANAGER.HisMedicineType;
using MOS.MANAGER.Config;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisDispense.Handler.Update
{
    class ImpMestProcessor : BusinessBase
    {
        private HIS_IMP_MEST recentHisImpMest;
        private HIS_MEDICINE recentHisMedicine;

        private HisImpMestUpdate hisImpMestUpdate;

        private HisMedicineUpdate hisMedicineUpdate;
        private HisMedicinePatyCreate hisMedicinePatyCreate;
        private HisMedicinePatyUpdate hisMedicinePatyUpdate;
        private HisImpMestMedicineUpdate hisImpMestMedicineUpdate;

        internal ImpMestProcessor()
            : base()
        {
            this.Init();
        }

        internal ImpMestProcessor(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisImpMestUpdate = new HisImpMestUpdate(param);
            this.hisImpMestMedicineUpdate = new HisImpMestMedicineUpdate(param);
            this.hisMedicinePatyCreate = new HisMedicinePatyCreate(param);
            this.hisMedicinePatyUpdate = new HisMedicinePatyUpdate(param);
            this.hisMedicineUpdate = new HisMedicineUpdate(param);
        }

        internal bool Run(HisDispenseUpdateSDO data, HIS_DISPENSE dispense, HIS_IMP_MEST impMest, List<HIS_EXP_MEST_MEDICINE> expMestMedicines, List<HIS_EXP_MEST_MATERIAL> expMestMaterials,ref HIS_IMP_MEST_MEDICINE impMestMedicine, ref List<string> sqls)
        {
            bool result = false;
            try
            {
                HIS_IMP_MEST_MEDICINE hisImpMestMedicine = null;
                this.ProcessHisImpMest(data, dispense, impMest);
                this.ProcessHisMedicine(data, expMestMaterials, expMestMedicines, ref hisImpMestMedicine, ref sqls);
                this.ProcessHisImpMestMedicine(hisImpMestMedicine);
                impMestMedicine = hisImpMestMedicine;
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        private void ProcessHisImpMest(HisDispenseUpdateSDO data, HIS_DISPENSE dispense, HIS_IMP_MEST impMest)
        {
            Mapper.CreateMap<HIS_IMP_MEST, HIS_IMP_MEST>();
            HIS_IMP_MEST before = Mapper.Map<HIS_IMP_MEST>(impMest);
            impMest.IMP_MEST_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__BCT;
            impMest.IMP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__REQUEST;
            impMest.DISPENSE_ID = dispense.ID;
            impMest.TDL_DISPENSE_CODE = dispense.DISPENSE_CODE;

            if (ValueChecker.IsPrimitiveDiff<HIS_IMP_MEST>(before, impMest))
            {
                if (!this.hisImpMestUpdate.Update(impMest, before))
                {
                    throw new Exception("hisImpMestUpdate. Rollback du lieu. Ket thuc nghiep vu");
                }
            }
            this.recentHisImpMest = impMest;
        }

        private void ProcessHisMedicine(HisDispenseUpdateSDO data, List<HIS_EXP_MEST_MATERIAL> expMestMaterials, List<HIS_EXP_MEST_MEDICINE> expMestMedicines, ref HIS_IMP_MEST_MEDICINE impMestMedicine, ref List<string> sqls)
        {
            List<HIS_IMP_MEST_MEDICINE> olds = new HisImpMestMedicineGet().GetByImpMestId(this.recentHisImpMest.ID);
            if (olds == null || olds.Count != 1)
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                throw new Exception("So luong HIS_IMP_MEST_MEDICINE theo ImpMestId khac 1");
            }

            impMestMedicine = olds[0];
            HIS_MEDICINE medicine = new HisMedicineGet().GetById(impMestMedicine.MEDICINE_ID);
            HIS_MEDICINE_TYPE medicineType = new HisMedicineTypeGet().GetById(medicine.MEDICINE_TYPE_ID);
            Mapper.CreateMap<HIS_MEDICINE, HIS_MEDICINE>();
            HIS_MEDICINE before = Mapper.Map<HIS_MEDICINE>(medicine);
            medicine.AMOUNT = data.Amount;
            medicine.EXPIRED_DATE = data.ExpiredDate;
            medicine.TDL_BID_NUMBER = data.HeinDocumentNumber;
            medicine.PACKAGE_NUMBER = data.PackageNumber;
            medicine.IS_SALE_EQUAL_IMP_PRICE = null;
            medicine.MEDICINE_BYT_NUM_ORDER = medicineType.BYT_NUM_ORDER;
            medicine.MEDICINE_IS_STAR_MARK = medicineType.IS_STAR_MARK;
            medicine.MEDICINE_REGISTER_NUMBER = medicineType.REGISTER_NUMBER;
            medicine.MEDICINE_TCY_NUM_ORDER = medicineType.TCY_NUM_ORDER;
            medicine.TDL_SERVICE_ID = medicineType.SERVICE_ID;
            medicine.INTERNAL_PRICE = medicineType.INTERNAL_PRICE;
            medicine.IS_PREGNANT = Constant.IS_TRUE;
            this.ProcessImpPrice(medicine,medicineType, expMestMaterials, expMestMedicines);
            HisMedicineUtil.SetTdl(medicine, medicineType);
            HisMedicineUtil.SetTdl(medicine, this.recentHisImpMest);

            if (HisMedicineUtil.CheckIsDiff(medicine, before))
            {
                if (!this.hisMedicineUpdate.Update(medicine, before))
                {
                    throw new Exception("hisMedicineCreate. Ket thuc nghiep vu");
                }
            }
            this.recentHisMedicine = medicine;

            #region Xy ly thong tin chinh sach gia

            if (IsNotNullOrEmpty(data.MedicinePaties))
            {
                data.MedicinePaties.ForEach(o => o.MEDICINE_ID = medicine.ID);
            }

            List<HIS_MEDICINE_PATY> medicinePatys = data.MedicinePaties;
            if (IsNotNullOrEmpty(medicinePatys))
            {
                //Cap nhat medicine_id cho cac medicine_paty
                medicinePatys.ForEach(o => o.MEDICINE_ID = medicine.ID);
            }

            List<HIS_MEDICINE_PATY> listToInserted = IsNotNullOrEmpty(medicinePatys) ? medicinePatys.Where(o => o.ID <= 0).ToList() : null;
            List<HIS_MEDICINE_PATY> listToUpdate = null;
            List<HIS_MEDICINE_PATY> listToDelete = null;

            List<HIS_MEDICINE_PATY> existedMedicinePatys = new HisMedicinePatyGet().GetByMedicineId(medicine.ID);
            List<long> medicinePatyIds = IsNotNullOrEmpty(medicinePatys) ? medicinePatys.Select(o => o.ID).ToList() : null;

            if (existedMedicinePatys != null)
            {
                List<long> existedMedicinePatyIds = existedMedicinePatys.Select(o => o.ID).ToList();
                //D/s can update la d/s da ton tai tren he thong va co trong d/s gui tu client
                listToUpdate = IsNotNullOrEmpty(medicinePatys) ? medicinePatys.Where(o => existedMedicinePatyIds.Contains(o.ID)).ToList() : null;
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
                if (sqls == null)
                {
                    sqls = new List<string>();
                }
                string query = DAOWorker.SqlDAO.AddInClause(listToDelete.Select(s => s.MEDICINE_ID).ToList(), "DELETE HIS_MEDICINE_PATY WHERE %IN_CLAUSE%", "ID");
                sqls.Add(query);
            }
            #endregion

        }

        private void ProcessHisImpMestMedicine(HIS_IMP_MEST_MEDICINE impMestMedicine)
        {
            Mapper.CreateMap<HIS_IMP_MEST_MEDICINE, HIS_IMP_MEST_MEDICINE>();
            HIS_IMP_MEST_MEDICINE before = Mapper.Map<HIS_IMP_MEST_MEDICINE>(impMestMedicine);
            impMestMedicine.AMOUNT = this.recentHisMedicine.AMOUNT;
            impMestMedicine.IMP_MEST_ID = this.recentHisImpMest.ID;
            impMestMedicine.MEDICINE_ID = this.recentHisMedicine.ID;
            impMestMedicine.PRICE = this.recentHisMedicine.IMP_PRICE;
            impMestMedicine.VAT_RATIO = this.recentHisMedicine.IMP_VAT_RATIO;

            if (!this.hisImpMestMedicineUpdate.Update(impMestMedicine))
            {
                throw new Exception("hisImpMestMedicineCreate. Ket thuc nghiep vu");
            }
        }

        private void ProcessImpPrice(HIS_MEDICINE medicine, HIS_MEDICINE_TYPE medicineType, List<HIS_EXP_MEST_MATERIAL> expMestMaterials, List<HIS_EXP_MEST_MEDICINE> expMestMedicines)
        {
            if (medicineType.IMP_PRICE.HasValue)
            {
                if (HisDispenseCFG.IS_MEDICINE_IMP_PRICE_OPTION)
                {
                    medicine.IMP_PRICE = medicineType.IMP_PRICE.Value;
                    medicine.IMP_VAT_RATIO = medicineType.IMP_VAT_RATIO ?? 0;
                }
                else
                {
                    decimal totalPrice = 0;
                    if (IsNotNullOrEmpty(expMestMaterials))
                    {
                        totalPrice += expMestMaterials.Sum(o => ((o.PRICE ?? 0) * o.AMOUNT * (1 + (o.VAT_RATIO ?? 0))));
                    }
                    if (IsNotNullOrEmpty(expMestMedicines))
                    {
                        totalPrice += expMestMedicines.Sum(o => ((o.PRICE ?? 0) * o.AMOUNT * (1 + (o.VAT_RATIO ?? 0))));
                    }

                    medicine.IMP_PRICE = totalPrice / medicine.AMOUNT;
                    medicine.IMP_VAT_RATIO = 0;
                }
            }
            else
            {
                decimal totalPrice = 0;
                if (IsNotNullOrEmpty(expMestMaterials))
                {
                    totalPrice += expMestMaterials.Sum(o => ((o.PRICE ?? 0) * o.AMOUNT * (1 + (o.VAT_RATIO ?? 0))));
                }
                if (IsNotNullOrEmpty(expMestMedicines))
                {
                    totalPrice += expMestMedicines.Sum(o => ((o.PRICE ?? 0) * o.AMOUNT * (1 + (o.VAT_RATIO ?? 0))));
                }

                medicine.IMP_PRICE = totalPrice / medicine.AMOUNT;
                medicine.IMP_VAT_RATIO = 0;
            }
        }

        internal void RollbackData()
        {
            try
            {
                this.hisImpMestMedicineUpdate.RollbackData();
                this.hisMedicinePatyUpdate.RollbackData();
                this.hisMedicinePatyCreate.RollbackData();
                this.hisMedicineUpdate.RollbackData();
                this.hisImpMestUpdate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
