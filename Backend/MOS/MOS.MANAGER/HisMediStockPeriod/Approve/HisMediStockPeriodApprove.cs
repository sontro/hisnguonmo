using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisMaterial;
using MOS.MANAGER.HisMaterialPaty;
using MOS.MANAGER.HisMedicine;
using MOS.MANAGER.HisMedicinePaty;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisMediStockPeriod.Approve
{
    class HisMediStockPeriodApprove : BusinessBase
    {
        private MediStockPeriodProcessor mediStockPeriodProcessor;
        private ExpMestProcessor expMestProcessor;
        private ImpMestProcessor impMestProcessor;
        private ExpMaterialProcessor expMaterialProcessor;
        private ExpMedicineProcessor expMedicineProcessor;
        private ImpMaterialProcessor impMaterialProcessor;
        private ImpMedicineProcessor impMedicineProcessor;

        internal HisMediStockPeriodApprove()
            : base()
        {
            this.Init();
        }

        internal HisMediStockPeriodApprove(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.mediStockPeriodProcessor = new MediStockPeriodProcessor(param);
            this.expMestProcessor = new ExpMestProcessor(param);
            this.impMestProcessor = new ImpMestProcessor(param);
            this.expMaterialProcessor = new ExpMaterialProcessor(param);
            this.expMedicineProcessor = new ExpMedicineProcessor(param);
            this.impMaterialProcessor = new ImpMaterialProcessor(param);
            this.impMedicineProcessor = new ImpMedicineProcessor(param);
        }

        internal bool Run(HisMestPeriodApproveSDO data, ref HisMestPeriodApproveResultSDO resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_MEDI_STOCK_PERIOD raw = null;
                HIS_EXP_MEST expMest = null;
                HIS_IMP_MEST impMest = null;
                WorkPlaceSDO workplace = null;
                List<HIS_MEST_PERIOD_MEDI> periodMedicines = null;
                List<HIS_MEST_PERIOD_MATE> periodMaterials = null;
                List<ExpMaterialSDO> ExpMaterials = null;
                List<ExpMedicineSDO> ExpMedicines = null;
                List<HisMaterialWithPatySDO> ImpMaterials = null;
                List<HisMedicineWithPatySDO> ImpMedicines = null;

                HisMediStockPeriodApproveCheck checker = new HisMediStockPeriodApproveCheck(param);
                HisMediStockPeriodCheck commonChecker = new HisMediStockPeriodCheck(param);

                valid = valid && commonChecker.VerifyRequireField(data);
                valid = valid && commonChecker.VerifyId(data.MediStockPeriodId, ref raw);
                valid = valid && commonChecker.IsUnLock(raw);
                valid = valid && commonChecker.IsNotApprove(raw);
                valid = valid && this.HasWorkPlaceInfo(data.WorkingRoomId, ref workplace);
                valid = valid && commonChecker.VerifyWokingInStock(raw, workplace);
                valid = valid && checker.ValidData(raw, ref periodMaterials, ref periodMedicines);
                valid = valid && checker.IsValidForExpKeyConfig();
                valid = valid && this.PrepareData(periodMaterials, periodMedicines, ref ExpMaterials, ref ExpMedicines, ref ImpMaterials, ref ImpMedicines);

                if (valid)
                {
                    List<string> sqls = new List<string>();

                    if (!this.mediStockPeriodProcessor.Run(raw))
                    {
                        throw new Exception("mediStockPeriodProcessor. Ket thuc nghiep vu");
                    }

                    if (!this.expMestProcessor.Run(data, ExpMaterials, ExpMedicines, raw, ref expMest))
                    {
                        throw new Exception("expMestProcessor. Ket thuc nghiep vu");
                    }

                    if (!this.impMestProcessor.Run(data, ImpMaterials, ImpMedicines, raw, ref impMest))
                    {
                        throw new Exception("impMestProcessor. Ket thuc nghiep vu");
                    }

                    if (!this.expMaterialProcessor.Run(ExpMaterials, expMest, ref sqls))
                    {
                        throw new Exception("expMaterialProcessor. Ket thuc nghiep vu");
                    }

                    if (!this.expMedicineProcessor.Run(ExpMedicines, expMest, ref sqls))
                    {
                        throw new Exception("expMedicineProcessor. Ket thuc nghiep vu");
                    }

                    if (!this.impMaterialProcessor.Run(ImpMaterials, impMest))
                    {
                        throw new Exception("impMaterialProcessor. Ket thuc nghiep vu");
                    }

                    if (!this.impMedicineProcessor.Run(ImpMedicines, impMest))
                    {
                        throw new Exception("impMedicineProcessor. Ket thuc nghiep vu");
                    }

                    if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("sqls: " + sqls.ToString());
                    }

                    this.PassResult(ref resultData, expMest, impMest);
                    result = true;

                    if (impMest != null)
                    {
                        new EventLogGenerator(EventLog.Enum.HisImpMest_TaoPhieuNhap).ImpMestCode(impMest.IMP_MEST_CODE).Run();
                    }
                    if (expMest != null)
                    {
                        new EventLogGenerator(EventLog.Enum.HisExpMest_TaoPhieuXuat).ExpMestCode(expMest.EXP_MEST_CODE).Run();
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                this.Rollback();
                result = false;
            }
            return result;
        }

        private bool PrepareData(List<HIS_MEST_PERIOD_MATE> periodMaterials, List<HIS_MEST_PERIOD_MEDI> periodMedicines, ref List<ExpMaterialSDO> ExpMaterials, ref List<ExpMedicineSDO> ExpMedicines, ref List<HisMaterialWithPatySDO> ImpMaterials, ref List<HisMedicineWithPatySDO> ImpMedicines)
        {
            bool valid = true;
            try
            {
                if (IsNotNullOrEmpty(periodMaterials))
                {
                    foreach (HIS_MEST_PERIOD_MATE mate in periodMaterials)
                    {
                        decimal diff = (mate.INVENTORY_AMOUNT ?? 0) - (mate.VIR_END_AMOUNT ?? 0);
                        if (diff < 0)
                        {
                            ExpMaterialSDO expMate = new ExpMaterialSDO();
                            expMate.Amount = -diff;
                            expMate.MaterialId = mate.MATERIAL_ID;
                            if (ExpMaterials == null) ExpMaterials = new List<ExpMaterialSDO>();
                            ExpMaterials.Add(expMate);
                        }
                        else if (diff > 0)
                        {
                            HisMaterialWithPatySDO impMate = new HisMaterialWithPatySDO();
                            HIS_MATERIAL material = new HisMaterialGet().GetById(mate.MATERIAL_ID);
                            impMate.Material = new HIS_MATERIAL();
                            impMate.Material.AMOUNT = diff;
                            impMate.Material.BID_ID = material.BID_ID;
                            impMate.Material.CONCENTRA = material.CONCENTRA;
                            impMate.Material.EXPIRED_DATE = material.EXPIRED_DATE;
                            impMate.Material.IMP_PRICE = material.IMP_PRICE;
                            impMate.Material.IMP_VAT_RATIO = material.IMP_VAT_RATIO;
                            impMate.Material.INTERNAL_PRICE = material.INTERNAL_PRICE;
                            impMate.Material.IS_PREGNANT = Constant.IS_TRUE;
                            impMate.Material.IS_SALE_EQUAL_IMP_PRICE = material.IS_SALE_EQUAL_IMP_PRICE;
                            impMate.Material.MANUFACTURER_ID = material.MANUFACTURER_ID;
                            impMate.Material.MATERIAL_TYPE_ID = material.MATERIAL_TYPE_ID;
                            impMate.Material.MAX_REUSE_COUNT = material.MAX_REUSE_COUNT;
                            impMate.Material.NATIONAL_NAME = material.NATIONAL_NAME;
                            impMate.Material.PACKAGE_NUMBER = material.PACKAGE_NUMBER;
                            impMate.Material.SUPPLIER_ID = material.SUPPLIER_ID;
                            impMate.Material.TDL_BID_GROUP_CODE = material.TDL_BID_GROUP_CODE;
                            impMate.Material.TDL_BID_NUM_ORDER = material.TDL_BID_NUM_ORDER;
                            impMate.Material.TDL_BID_NUMBER = material.TDL_BID_NUMBER;
                            impMate.Material.TDL_BID_PACKAGE_CODE = material.TDL_BID_PACKAGE_CODE;
                            impMate.Material.TDL_BID_YEAR = material.TDL_BID_YEAR;
                            impMate.Material.TDL_SERVICE_ID = material.TDL_SERVICE_ID;
                            if (material.IS_SALE_EQUAL_IMP_PRICE != Constant.IS_TRUE)
                            {
                                List<HIS_MATERIAL_PATY> materialPatys = new HisMaterialPatyGet().GetByMaterialId(material.ID);
                                if (IsNotNullOrEmpty(materialPatys))
                                {
                                    impMate.MaterialPaties = (from r in materialPatys select new HIS_MATERIAL_PATY() { MATERIAL_ID = r.MATERIAL_ID, PATIENT_TYPE_ID = r.PATIENT_TYPE_ID, EXP_PRICE = r.EXP_PRICE, EXP_VAT_RATIO = r.EXP_VAT_RATIO, IS_ACTIVE = r.IS_ACTIVE }).ToList();
                                }
                            }
                            if (ImpMaterials == null) ImpMaterials = new List<HisMaterialWithPatySDO>();
                            ImpMaterials.Add(impMate);
                        }
                    }
                }

                if (IsNotNullOrEmpty(periodMedicines))
                {
                    foreach (HIS_MEST_PERIOD_MEDI medi in periodMedicines)
                    {
                        decimal diff = (medi.INVENTORY_AMOUNT ?? 0) - (medi.VIR_END_AMOUNT ?? 0);
                        if (diff < 0)
                        {
                            ExpMedicineSDO expMate = new ExpMedicineSDO();
                            expMate.Amount = -diff;
                            expMate.MedicineId = medi.MEDICINE_ID;
                            if (ExpMedicines == null) ExpMedicines = new List<ExpMedicineSDO>();
                            ExpMedicines.Add(expMate);
                        }
                        else if (diff > 0)
                        {
                            HisMedicineWithPatySDO impMedi = new HisMedicineWithPatySDO();
                            HIS_MEDICINE medicine = new HisMedicineGet().GetById(medi.MEDICINE_ID);
                            impMedi.Medicine = new HIS_MEDICINE();
                            impMedi.Medicine.AMOUNT = diff;
                            impMedi.Medicine.BID_ID = medicine.BID_ID;
                            impMedi.Medicine.CONCENTRA = medicine.CONCENTRA;
                            impMedi.Medicine.EXPIRED_DATE = medicine.EXPIRED_DATE;
                            impMedi.Medicine.IMP_PRICE = medicine.IMP_PRICE;
                            impMedi.Medicine.IMP_VAT_RATIO = medicine.IMP_VAT_RATIO;
                            impMedi.Medicine.INTERNAL_PRICE = medicine.INTERNAL_PRICE;
                            impMedi.Medicine.IS_PREGNANT = Constant.IS_TRUE;
                            impMedi.Medicine.IS_SALE_EQUAL_IMP_PRICE = medicine.IS_SALE_EQUAL_IMP_PRICE;
                            impMedi.Medicine.MANUFACTURER_ID = medicine.MANUFACTURER_ID;
                            impMedi.Medicine.MEDICINE_TYPE_ID = medicine.MEDICINE_TYPE_ID;
                            impMedi.Medicine.NATIONAL_NAME = medicine.NATIONAL_NAME;
                            impMedi.Medicine.PACKAGE_NUMBER = medicine.PACKAGE_NUMBER;
                            impMedi.Medicine.SUPPLIER_ID = medicine.SUPPLIER_ID;
                            impMedi.Medicine.TDL_BID_GROUP_CODE = medicine.TDL_BID_GROUP_CODE;
                            impMedi.Medicine.TDL_BID_NUM_ORDER = medicine.TDL_BID_NUM_ORDER;
                            impMedi.Medicine.TDL_BID_NUMBER = medicine.TDL_BID_NUMBER;
                            impMedi.Medicine.TDL_BID_PACKAGE_CODE = medicine.TDL_BID_PACKAGE_CODE;
                            impMedi.Medicine.TDL_BID_YEAR = medicine.TDL_BID_YEAR;
                            impMedi.Medicine.TDL_SERVICE_ID = medicine.TDL_SERVICE_ID;
                            impMedi.Medicine.ACTIVE_INGR_BHYT_CODE = medicine.ACTIVE_INGR_BHYT_CODE;
                            impMedi.Medicine.ACTIVE_INGR_BHYT_NAME = medicine.ACTIVE_INGR_BHYT_NAME;
                            impMedi.Medicine.MEDICINE_BYT_NUM_ORDER = medicine.MEDICINE_BYT_NUM_ORDER;
                            impMedi.Medicine.MEDICINE_IS_STAR_MARK = medicine.MEDICINE_IS_STAR_MARK;
                            impMedi.Medicine.MEDICINE_REGISTER_NUMBER = medicine.MEDICINE_REGISTER_NUMBER;
                            impMedi.Medicine.MEDICINE_TCY_NUM_ORDER = medicine.MEDICINE_TCY_NUM_ORDER;

                            if (medicine.IS_SALE_EQUAL_IMP_PRICE != Constant.IS_TRUE)
                            {
                                List<HIS_MEDICINE_PATY> medicinePatys = new HisMedicinePatyGet().GetByMedicineId(medicine.ID);
                                if (IsNotNullOrEmpty(medicinePatys))
                                {
                                    impMedi.MedicinePaties = (from r in medicinePatys select new HIS_MEDICINE_PATY() { MEDICINE_ID = r.MEDICINE_ID, PATIENT_TYPE_ID = r.PATIENT_TYPE_ID, EXP_PRICE = r.EXP_PRICE, EXP_VAT_RATIO = r.EXP_VAT_RATIO, IS_ACTIVE = r.IS_ACTIVE }).ToList();
                                }
                            }
                            if (ImpMedicines == null) ImpMedicines = new List<HisMedicineWithPatySDO>();
                            ImpMedicines.Add(impMedi);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                valid = false;
            }
            return valid;
        }


        private void PassResult(ref HisMestPeriodApproveResultSDO resultData, HIS_EXP_MEST expMest, HIS_IMP_MEST impMest)
        {
            try
            {
                resultData = new HisMestPeriodApproveResultSDO();
                resultData.ExpMest = expMest;
                resultData.ImpMest = impMest;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void Rollback()
        {
            this.impMedicineProcessor.Rollback();
            this.impMaterialProcessor.Rollback();
            this.expMedicineProcessor.Rollback();
            this.expMaterialProcessor.Rollback();
            this.impMestProcessor.Rollback();
            this.expMestProcessor.Rollback();
            this.mediStockPeriodProcessor.Rollback();
        }
    }
}
