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

namespace MOS.MANAGER.HisDispense.Handler.Create
{
    class ImpMestProcessor : BusinessBase
    {
        private HIS_IMP_MEST recentHisImpMest;
        private HIS_MEDICINE recentHisMedicine;

        private HisImpMestCreate hisImpMestCreate;

        private HisMedicineCreate hisMedicineCreate;
        private HisMedicinePatyCreate hisMedicinePatyCreate;
        private HisImpMestMedicineCreate hisImpMestMedicineCreate;

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
            this.hisImpMestCreate = new HisImpMestCreate(param);
            this.hisImpMestMedicineCreate = new HisImpMestMedicineCreate(param);
            this.hisMedicineCreate = new HisMedicineCreate(param);
            this.hisMedicinePatyCreate = new HisMedicinePatyCreate(param);
        }

        internal bool Run(HisDispenseSDO data, HIS_DISPENSE hisDispense, List<HIS_EXP_MEST_MEDICINE> medicines, List<HIS_EXP_MEST_MATERIAL> materials, ref HIS_IMP_MEST impMest, ref HIS_IMP_MEST_MEDICINE impMestMedicine)
        {
            bool result = false;
            try
            {
                this.ProcessHisImpMest(data, hisDispense);
                this.ProcessHisMedicine(data, materials, medicines);
                this.ProcessHisImpMestMedicine(ref impMestMedicine);
                impMest = this.recentHisImpMest;
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void ProcessHisImpMest(HisDispenseSDO data, HIS_DISPENSE hisDispense)
        {
            long time = Inventec.Common.DateTime.Get.Now().Value;
            HIS_IMP_MEST impMest = new HIS_IMP_MEST();
            impMest.DISPENSE_ID = hisDispense.ID;
            impMest.TDL_DISPENSE_CODE = hisDispense.DISPENSE_CODE;
            impMest.MEDI_STOCK_ID = data.MediStockId;
            impMest.REQ_ROOM_ID = data.RequestRoomId;
            impMest.IMP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__REQUEST;
            impMest.IMP_MEST_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__BCT;
            if (!this.hisImpMestCreate.Create(impMest))
            {
                throw new Exception("hisImpMestCreate. Ket thuc nghiep vu");
            }
            this.recentHisImpMest = impMest;
        }

        private void ProcessHisMedicine(HisDispenseSDO data, List<HIS_EXP_MEST_MATERIAL> materials, List<HIS_EXP_MEST_MEDICINE> medicines)
        {
            HIS_MEDICINE_TYPE medicineType = new HisMedicineTypeGet().GetById(data.MedicineTypeId);

            HIS_MEDICINE medicine = new HIS_MEDICINE();
            medicine.AMOUNT = data.Amount;
            medicine.MEDICINE_TYPE_ID = data.MedicineTypeId;
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
            this.ProcessImpPrice(medicine, medicineType, materials, medicines);
            HisMedicineUtil.SetTdl(medicine, medicineType);
            HisMedicineUtil.SetTdl(medicine, this.recentHisImpMest);

            if (!this.hisMedicineCreate.Create(medicine))
            {
                throw new Exception("hisMedicineCreate. Ket thuc nghiep vu");
            }
            this.recentHisMedicine = medicine;

            if (IsNotNullOrEmpty(data.MedicinePaties))
            {
                data.MedicinePaties.ForEach(o => o.MEDICINE_ID = medicine.ID);
                if (!this.hisMedicinePatyCreate.CreateList(data.MedicinePaties))
                {
                    throw new Exception("hisMedicinePatyCreate. Ket thuc nghiep vu");
                }
            }
        }

        private void ProcessHisImpMestMedicine(ref HIS_IMP_MEST_MEDICINE medicine)
        {
            HIS_IMP_MEST_MEDICINE impMestMedicine = new HIS_IMP_MEST_MEDICINE();
            impMestMedicine.AMOUNT = this.recentHisMedicine.AMOUNT;
            impMestMedicine.IMP_MEST_ID = this.recentHisImpMest.ID;
            impMestMedicine.MEDICINE_ID = this.recentHisMedicine.ID;
            impMestMedicine.PRICE = this.recentHisMedicine.IMP_PRICE;
            impMestMedicine.VAT_RATIO = this.recentHisMedicine.IMP_VAT_RATIO;

            if (!this.hisImpMestMedicineCreate.Create(impMestMedicine))
            {
                throw new Exception("hisImpMestMedicineCreate. Ket thuc nghiep vu");
            }
            medicine = impMestMedicine;
        }

        private void ProcessImpPrice(HIS_MEDICINE medicine, HIS_MEDICINE_TYPE medicineType, List<HIS_EXP_MEST_MATERIAL> materials, List<HIS_EXP_MEST_MEDICINE> medicines)
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
                    if (IsNotNullOrEmpty(materials))
                    {
                        totalPrice += materials.Sum(o => ((o.PRICE ?? 0) * o.AMOUNT * (1 + (o.VAT_RATIO ?? 0))));
                    }
                    if (IsNotNullOrEmpty(medicines))
                    {
                        totalPrice += medicines.Sum(o => ((o.PRICE ?? 0) * o.AMOUNT * (1 + (o.VAT_RATIO ?? 0))));
                    }

                    medicine.IMP_PRICE = totalPrice / medicine.AMOUNT;
                    medicine.IMP_VAT_RATIO = 0;
                }
            }
            else
            {
                decimal totalPrice = 0;
                if (IsNotNullOrEmpty(materials))
                {
                    totalPrice += materials.Sum(o => ((o.PRICE ?? 0) * o.AMOUNT * (1 + (o.VAT_RATIO ?? 0))));
                }
                if (IsNotNullOrEmpty(medicines))
                {
                    totalPrice += medicines.Sum(o => ((o.PRICE ?? 0) * o.AMOUNT * (1 + (o.VAT_RATIO ?? 0))));
                }

                medicine.IMP_PRICE = totalPrice / medicine.AMOUNT;
                medicine.IMP_VAT_RATIO = 0;
            }
        }

        internal void RollbackData()
        {
            try
            {
                this.hisImpMestMedicineCreate.RollbackData();
                this.hisMedicinePatyCreate.RollbackData();
                this.hisMedicineCreate.RollbackData();
                this.hisImpMestCreate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
