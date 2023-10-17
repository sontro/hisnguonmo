using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMest;
using MOS.MANAGER.HisExpMest.Common;
using MOS.MANAGER.HisExpMest.Common.Update;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisServiceReq;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.MANAGER.HisExpMest.Common.Auto;
using MOS.MANAGER.EventLogUtil;
using MOS.LibraryEventLog;
using System.Globalization;
using MOS.MANAGER.HisExpMest.Common.Get;

namespace MOS.MANAGER.HisExpMest.Sale.Update
{
    //Xuat ban
    partial class HisExpMestSaleUpdateList : BusinessBase
    {
        private List<HisExpMestProcessor> hisExpMestProcessors;
        private List<MedicineProcessor> medicineProcessors;
        private List<MaterialProcessor> materialProcessors;

        internal HisExpMestSaleUpdateList()
            : base()
        {
            this.Init();
        }

        internal HisExpMestSaleUpdateList(CommonParam paramUpdate)
            : base(paramUpdate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisExpMestProcessors = new List<HisExpMestProcessor>();
            this.medicineProcessors = new List<MedicineProcessor>();
            this.materialProcessors = new List<MaterialProcessor>();
        }

        internal bool Run(List<HisExpMestSaleSDO> data, ref List<HisExpMestSaleResultSDO> resultData)
        {
            bool result = false;
            try
            {
                List<HIS_EXP_MEST> listRaw = new List<HIS_EXP_MEST>();
                bool valid = true;
                AutoEnum en = AutoEnum.NONE;

                HisExpMestSaleCheck checker = new HisExpMestSaleCheck(param);
                HisExpMestCheck expMestCheck = new HisExpMestCheck(param);
                valid = valid && checker.IsAllowed(data);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && expMestCheck.VerifyIds(data.Select(s => s.ExpMestId ?? 0).ToList(), listRaw);
                valid = valid && expMestCheck.IsInRequest(listRaw);
                valid = valid && expMestCheck.HasNotBill(listRaw);
                valid = valid && expMestCheck.HasNoNationalCode(listRaw);
                valid = valid && checker.IsValidGroup(data, listRaw);
                valid = valid && checker.CheckAuto(data.FirstOrDefault().MediStockId, ref en);
                if (valid)
                {
                    List<string> sqls = new List<string>();
                    List<HIS_EXP_MEST> resultExpMests = new List<HIS_EXP_MEST>();
                    List<HisExpMestResultSDO> resultSdos = new List<HisExpMestResultSDO>();

                    long time = Inventec.Common.DateTime.Get.Now().Value;
                    string loginname = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    string username = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();

                    foreach (var sdo in data)
                    {
                        List<HIS_EXP_MEST_MATERIAL> expMestMaterials = null;
                        List<HIS_EXP_MEST_MEDICINE> expMestMedicines = null;
                        HIS_EXP_MEST raw = listRaw.FirstOrDefault(o => o.ID == sdo.ExpMestId);
                        HIS_EXP_MEST rsExpMest = null;

                        HisExpMestProcessor expMestProcessor = new HisExpMestProcessor(param);
                        this.hisExpMestProcessors.Add(expMestProcessor);
                        //Update exp_mest
                        if (!expMestProcessor.Run(sdo, raw, ref rsExpMest, en, time, loginname, username))
                        {
                            throw new Exception("expMestProcessor. Ket thuc nghiep vu");
                        }

                        MedicineProcessor medicineProcessor = new MedicineProcessor(param);
                        this.medicineProcessors.Add(medicineProcessor);
                        //Tao exp_mest_medicine
                        if (!medicineProcessor.Run(sdo, rsExpMest, ref expMestMedicines, ref sqls, en, time, loginname, username))
                        {
                            throw new Exception("medicineProcessor. Ket thuc nghiep vu");
                        }

                        MaterialProcessor materialProcessor = new MaterialProcessor(param);
                        this.materialProcessors.Add(materialProcessor);
                        //Tao exp_mest_material
                        if (!materialProcessor.Run(sdo, rsExpMest, ref expMestMaterials, ref sqls, en, time, loginname, username))
                        {
                            throw new Exception("materialProcessor Rollback du lieu. Ket thuc nghiep vu");
                        }

                        //SET TDL_TOTAL_PRICE
                        this.ProcessTdlTotalPrice(sdo, rsExpMest, expMestMaterials, expMestMedicines, ref sqls);

                        HisExpMestResultSDO rsSdo = null;
                        this.PassResult(rsExpMest, expMestMaterials, expMestMedicines, ref rsSdo);
                        resultSdos.Add(rsSdo);
                        resultExpMests.Add(rsExpMest);
                    }

                    ///execute sql se thuc hien cuoi cung de de dang trong viec rollback
                    if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("Rollback du lieu");
                    }

                    result = true;

                    new EventLogGenerator(EventLog.Enum.HisExpMest_SuaPhieuXuatBan, this.GenerateEventLog(resultExpMests)).Run();

                    //LogSystem.Info("Update. Auto Approve Begin");
                    //this.ProcessAuto(resultSdos);
                    //LogSystem.Info("Update. Auto Approve End");


                    this.PassResultSale(resultSdos, ref resultData);
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

        private void ProcessTdlTotalPrice(HisExpMestSaleSDO sdo, HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MATERIAL> expMestMaterials, List<HIS_EXP_MEST_MEDICINE> expMestMedicines, ref List<string> sqls)
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
            if (totalPrice.HasValue)
            {
                string updateSql = string.Format("UPDATE HIS_EXP_MEST SET TDL_TOTAL_PRICE = {0}, TRANSFER_AMOUNT = NULL WHERE ID = {1}", totalPrice.Value.ToString("G27", CultureInfo.InvariantCulture), expMest.ID);
                sqls.Add(updateSql);
            }
        }

        private string GenerateEventLog(List<HIS_EXP_MEST> hisExpMests)
        {
            string log = "";
            try
            {
                if (IsNotNullOrEmpty(hisExpMests))
                {
                    List<string> logs = new List<string>();
                    foreach (var item in hisExpMests)
                    {
                        string s = String.Format("{0}: {1}", SimpleEventKey.EXP_MEST_CODE, item.EXP_MEST_CODE);
                        if (item.PRESCRIPTION_ID.HasValue)
                        {
                            s = String.Format("{0}({1}: {2}. {3}: {4})", s, SimpleEventKey.TREATMENT_CODE, item.TDL_TREATMENT_CODE, SimpleEventKey.SERVICE_REQ_CODE, item.TDL_SERVICE_REQ_CODE);
                        }
                        logs.Add(s);
                    }
                    log = String.Join(". ", logs);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                log = "";
            }
            return log;
        }

        private void PassResult(HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MATERIAL> expMaterials, List<HIS_EXP_MEST_MEDICINE> expMedicines, ref HisExpMestResultSDO resultData)
        {
            resultData = new HisExpMestResultSDO();
            resultData.ExpMest = expMest;
            resultData.ExpMaterials = expMaterials;
            resultData.ExpMedicines = expMedicines;
        }

        private void PassResultSale(List<HisExpMestResultSDO> resultSdos, ref List<HisExpMestSaleResultSDO> resultData)
        {
            try
            {
                LogSystem.Info("====================UpdateList Begin PassResultSale");
                if (IsNotNullOrEmpty(resultSdos))
                {
                    resultData = new List<HisExpMestSaleResultSDO>();
                    List<V_HIS_EXP_MEST> expMests = new HisExpMestGet().GetViewByIds(resultSdos.Select(s => s.ExpMest.ID).ToList());
                    List<V_HIS_EXP_MEST_MEDICINE> medicines = null;
                    List<V_HIS_EXP_MEST_MATERIAL> materials = null;
                    if (resultSdos.Any(a => IsNotNullOrEmpty(a.ExpMedicines)))
                    {
                        medicines = new HisExpMestMedicineGet().GetViewByExpMestIds(resultSdos.Where(o => IsNotNullOrEmpty(o.ExpMedicines)).Select(s => s.ExpMest.ID).ToList());
                    }
                    if (resultSdos.Any(a => IsNotNullOrEmpty(a.ExpMaterials)))
                    {
                        materials = new HisExpMestMaterialGet().GetViewByExpMestIds(resultSdos.Where(o => IsNotNullOrEmpty(o.ExpMaterials)).Select(s => s.ExpMest.ID).ToList());
                    }
                    foreach (V_HIS_EXP_MEST item in expMests)
                    {
                        HisExpMestSaleResultSDO sdo = new HisExpMestSaleResultSDO();
                        sdo.ExpMest = item;
                        sdo.ExpMedicines = medicines != null ? medicines.Where(o => o.EXP_MEST_ID == item.ID).ToList() : null;
                        sdo.ExpMaterials = materials != null ? materials.Where(o => o.EXP_MEST_ID == item.ID).ToList() : null;
                        resultData.Add(sdo);
                    }
                }
                LogSystem.Info("====================UpdateList End PassResultSale");
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        /// <summary>
        /// Tu dong phe duyet, thuc xuat trong truong hop kho co cau hinh tu dong
        /// </summary>
        private void ProcessAuto(List<HisExpMestResultSDO> hisExpMests)
        {
            try
            {
                if (IsNotNullOrEmpty(hisExpMests))
                {
                    foreach (var sdo in hisExpMests)
                    {
                        HisExpMestResultSDO rsSdo = null;
                        if (new HisExpMestAutoProcess().Run(sdo.ExpMest, sdo.ExpMedicines, sdo.ExpMaterials, ref rsSdo))
                        {
                            if (rsSdo != null)
                            {
                                sdo.ExpMest.EXP_MEST_STT_ID = rsSdo.ExpMest.EXP_MEST_STT_ID;
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void RollBack()
        {
            if (IsNotNullOrEmpty(this.materialProcessors))
            {
                foreach (var processor in this.materialProcessors)
                {
                    processor.Rollback();
                }
            }
            if (IsNotNullOrEmpty(this.medicineProcessors))
            {
                foreach (var processor in this.medicineProcessors)
                {
                    processor.Rollback();
                }
            }
            if (IsNotNullOrEmpty(this.hisExpMestProcessors))
            {
                foreach (var processor in this.hisExpMestProcessors)
                {
                    processor.Rollback();
                }
            }
        }
    }
}
