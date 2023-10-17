using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisExpMest;
using MOS.MANAGER.HisExpMest.Common;
using MOS.MANAGER.HisExpMest.Common.Auto;
using MOS.MANAGER.HisExpMest.Common.Create;
using MOS.MANAGER.HisExpMestBlood;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace MOS.MANAGER.HisExpMest.Manu.Update
{
    //Xuat tra NCC
    partial class HisExpMestManuUpdate : BusinessBase
    {
        private HisExpMestAutoProcess hisExpMestAutoProcess;
        private HisExpMestProcessor hisExpMestProcessor;
        private MaterialProcessor materialProcessor;
        private MedicineProcessor medicineProcessor;
        private BloodProcessor bloodProcessor;

        private HisExpMestResultSDO recentResultSDO;

        internal HisExpMestManuUpdate()
            : base()
        {
            this.Init();
        }

        internal HisExpMestManuUpdate(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisExpMestAutoProcess = new HisExpMestAutoProcess(param);
            this.hisExpMestProcessor = new HisExpMestProcessor(param);
            this.materialProcessor = new MaterialProcessor(param);
            this.medicineProcessor = new MedicineProcessor(param);
            this.bloodProcessor = new BloodProcessor(param);
        }

        internal bool Run(HisExpMestManuSDO data, ref HisExpMestResultSDO resultData)
        {
            bool result = false;
            try
            {
                HIS_EXP_MEST expMest = null;
                HIS_IMP_MEST manuImpMest = null;
                HisExpMestManuCheck checker = new HisExpMestManuCheck(param);
                HisExpMestCheck expMestCheck = new HisExpMestCheck(param);
                bool valid = true;
                valid = valid && checker.IsAllowed(data);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.VerifyData(data, ref manuImpMest);
                valid = valid && (data.ExpMestId.HasValue && expMestCheck.VerifyId(data.ExpMestId.Value, ref expMest));
                valid = valid && expMestCheck.IsInRequest(expMest);
                valid = valid && expMestCheck.HasNoNationalCode(expMest);
                valid = valid && expMestCheck.HasToExpMestReason(data.ExpMestReasonId);
                if (valid)
                {
                    List<HIS_EXP_MEST_MATERIAL> expMestMaterials = null;
                    List<HIS_EXP_MEST_MEDICINE> expMestMedicines = null;
                    List<HIS_EXP_MEST_BLOOD> expMestBloods = null;
                    List<string> sqls = new List<string>();

                    //Tao exp_mest_material
                    if (!this.hisExpMestProcessor.Run(data, expMest, manuImpMest, ref expMest))
                    {
                        throw new Exception("hisExpMestProcessor Rollback du lieu. Ket thuc nghiep vu");
                    }

                    //Tao exp_mest_material
                    if (!this.materialProcessor.Run(data, expMest, ref expMestMaterials, ref sqls))
                    {
                        throw new Exception("ExpMestMaterialMaker Rollback du lieu. Ket thuc nghiep vu");
                    }

                    //Tao exp_mest_medicine
                    if (!this.medicineProcessor.Run(data, expMest, ref expMestMedicines, ref sqls))
                    {
                        throw new Exception("ExpMestMedicineMaker Rollback du lieu. Ket thuc nghiep vu");
                    }

                    //Tao exp_mest_blood
                    if (!this.bloodProcessor.Run(data, expMest, ref expMestBloods, ref sqls))
                    {
                        throw new Exception("ExpMestMedicineMaker Rollback du lieu. Ket thuc nghiep vu");
                    }

                    //Set TDL_TOTAL_PRICE
                    this.ProcessTdlTotalPrice(expMest, expMestMaterials, expMestMedicines, expMestBloods, ref sqls);

                    ///execute sql se thuc hien cuoi cung de de dang trong viec rollback
                    if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("Rollback du lieu");
                    }

                    this.ProcessAuto(expMest);

                    this.PassResult(expMest, expMestMaterials, expMestMedicines, expMestBloods, ref resultData);

                    new EventLogGenerator(EventLog.Enum.HisExpMest_SuaPhieuXuat).ExpMestCode(expMest.EXP_MEST_CODE).Run();

                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                resultData = null;
                this.RollBack();
                result = false;
            }
            return result;
        }

        private void ProcessTdlTotalPrice(HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MATERIAL> expMestMaterials, List<HIS_EXP_MEST_MEDICINE> expMestMedicines, List<HIS_EXP_MEST_BLOOD> expMestBloods, ref List<string> sqls)
        {
            try
            {
                decimal? totalPrice = null;
                if (IsNotNullOrEmpty(expMestMaterials))
                {
                    decimal matePrice = 0;
                    foreach (HIS_EXP_MEST_MATERIAL mate in expMestMaterials)
                    {
                        if (!mate.PRICE.HasValue)
                        {
                            continue;
                        }
                        matePrice += (mate.AMOUNT * mate.PRICE.Value * (1 + (mate.VAT_RATIO ?? 0)));
                    }
                    if (matePrice > 0)
                    {
                        totalPrice = matePrice;
                    }
                }
                if (IsNotNullOrEmpty(expMestMedicines))
                {
                    decimal mediPrice = 0;
                    foreach (HIS_EXP_MEST_MEDICINE medi in expMestMedicines)
                    {
                        if (!medi.PRICE.HasValue)
                        {
                            continue;
                        }
                        mediPrice += (medi.AMOUNT * medi.PRICE.Value * (1 + (medi.VAT_RATIO ?? 0)));
                    }
                    if (mediPrice > 0)
                    {
                        totalPrice = (totalPrice ?? 0) + mediPrice;
                    }
                }
                if (IsNotNullOrEmpty(expMestBloods))
                {
                    decimal mediPrice = 0;
                    foreach (HIS_EXP_MEST_BLOOD medi in expMestBloods)
                    {
                        if (!medi.PRICE.HasValue)
                        {
                            continue;
                        }
                        mediPrice += (medi.PRICE.Value * (1 + (medi.VAT_RATIO ?? 0)));
                    }
                    if (mediPrice > 0)
                    {
                        totalPrice = (totalPrice ?? 0) + mediPrice;
                    }
                }
                if (totalPrice.HasValue)
                {
                    string updateSql = string.Format("UPDATE HIS_EXP_MEST SET TDL_TOTAL_PRICE = {0} WHERE ID = {1}", totalPrice.Value.ToString("G27", CultureInfo.InvariantCulture), expMest.ID);
                    sqls.Add(updateSql);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        /// <summary>
        /// Tu dong phe duyet, thuc xuat trong truong hop kho co cau hinh tu dong
        /// </summary>
        private void ProcessAuto(HIS_EXP_MEST expMest)
        {
            try
            {
                this.hisExpMestAutoProcess.Run(expMest, ref this.recentResultSDO);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void PassResult(HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MATERIAL> expMaterials, List<HIS_EXP_MEST_MEDICINE> expMedicines, List<HIS_EXP_MEST_BLOOD> expBloods, ref HisExpMestResultSDO resultData)
        {
            if (this.recentResultSDO!=null)
            {
                resultData = this.recentResultSDO;
            }
            else
            {
                resultData = new HisExpMestResultSDO();
                resultData.ExpMest = expMest;
                resultData.ExpMaterials = expMaterials;
                resultData.ExpMedicines = expMedicines;
                resultData.ExpBloods = expBloods;
            }
        }

        private void RollBack()
        {
            this.hisExpMestProcessor.Rollback();
            this.materialProcessor.Rollback();
            this.medicineProcessor.Rollback();
            this.bloodProcessor.Rollback();
        }
    }
}
