﻿using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisImpMest;
using MOS.MANAGER.HisImpMestMedicine;
using MOS.MANAGER.HisMedicine;
using MOS.MANAGER.HisMedicinePaty;
using MOS.MANAGER.HisMedicineType;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisMediStockPeriod.Approve
{
    class ImpMedicineProcessor : BusinessBase
    {
        private HisMedicineCreate hisMedicineCreate;
        private HisMedicinePatyCreate hisMedicinePatyCreate;
        private HisImpMestMedicineCreate hisImpMestMedicineCreate;

        internal ImpMedicineProcessor(CommonParam param)
            : base(param)
        {
            this.hisMedicineCreate = new HisMedicineCreate(param);
            this.hisImpMestMedicineCreate = new HisImpMestMedicineCreate(param);
            this.hisMedicinePatyCreate = new HisMedicinePatyCreate(param);
        }

        internal bool Run(List<HisMedicineWithPatySDO> inveMedicines, HIS_IMP_MEST impMest)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(inveMedicines))
                {
                    HisImpMestCheck checker = new HisImpMestCheck(param);
                    bool valid = true;
                    valid = valid && checker.IsNotExistStopImpMedicineType(inveMedicines);

                    if (!valid)
                    {
                        throw new Exception("Du lieu ko hop le");
                    }

                    List<HIS_IMP_MEST_MEDICINE> hisImpMestMedicines = new List<HIS_IMP_MEST_MEDICINE>();
                    List<HisMedicineWithPatySDO> output = new List<HisMedicineWithPatySDO>();
                    foreach (HisMedicineWithPatySDO impMedicine in inveMedicines)
                    {
                        HisMedicineWithPatySDO inserted = new HisMedicineWithPatySDO();

                        //insert thong tin medicine
                        HIS_MEDICINE medicine = impMedicine.Medicine;
                        medicine.IS_PREGNANT = MOS.UTILITY.Constant.IS_TRUE;

                        if (!this.hisMedicineCreate.Create(medicine))
                        {
                            throw new Exception("Tao HIS_MEDICINE that bai");
                        }
                        inserted.Medicine = medicine;

                        //neu co thong tin chinh sach gia thi insert thong tin gia ban cho tung medicine
                        if (IsNotNullOrEmpty(impMedicine.MedicinePaties))
                        {
                            List<HIS_MEDICINE_PATY> medicinePaties = impMedicine.MedicinePaties;
                            medicinePaties.ForEach(o => o.MEDICINE_ID = medicine.ID);

                            if (!this.hisMedicinePatyCreate.CreateList(medicinePaties))
                            {
                                throw new Exception("Tao HIS_MEDICINE_PATY that bai");
                            }
                            inserted.MedicinePaties = medicinePaties;
                        }
                        HIS_IMP_MEST_MEDICINE impMestMedicine = new HIS_IMP_MEST_MEDICINE();
                        impMestMedicine.IMP_MEST_ID =impMest.ID;
                        impMestMedicine.MEDICINE_ID = medicine.ID;
                        impMestMedicine.AMOUNT = medicine.AMOUNT;
                        impMestMedicine.PRICE = medicine.IMP_PRICE;
                        impMestMedicine.VAT_RATIO = medicine.IMP_VAT_RATIO;
                        hisImpMestMedicines.Add(impMestMedicine);
                        output.Add(inserted);
                    }

                    if (!this.hisImpMestMedicineCreate.CreateList(hisImpMestMedicines))
                    {
                        throw new Exception("Tao HIS_IMP_MEST_MEDICINE that bai");
                    }
                }
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

        internal void Rollback()
        {
            try
            {
                this.hisImpMestMedicineCreate.RollbackData();
                this.hisMedicinePatyCreate.RollbackData();
                this.hisMedicineCreate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
