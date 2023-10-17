using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisExpMest.Common.Auto;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Sale.Create
{
    class HisExpMestSaleCreateList : BusinessBase
    {
        private List<HisExpMestProcessor> expMestProcessors;
        private List<MaterialProcessor> materialProcessors;
        private List<MedicineProcessor> medicineProcessors;

        internal HisExpMestSaleCreateList()
            : base()
        {
            this.Init();
        }

        internal HisExpMestSaleCreateList(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
            this.expMestProcessors = new List<HisExpMestProcessor>();
            this.materialProcessors = new List<MaterialProcessor>();
            this.medicineProcessors = new List<MedicineProcessor>();
        }

        internal bool Run(List<HisExpMestSaleSDO> data, ref List<HisExpMestSaleResultSDO> resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                AutoEnum en = AutoEnum.NONE;

                HisExpMestSaleCheck checker = new HisExpMestSaleCheck(param);
                valid = valid && checker.IsAllowed(data);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.IsValidGroup(data, null);
                valid = valid && checker.CheckAuto(data.FirstOrDefault().MediStockId, ref en);
                if (valid)
                {
                    List<string> sqls = new List<string>();
                    List<HisExpMestResultSDO> resultSdos = new List<HisExpMestResultSDO>();
                    List<HIS_EXP_MEST> hisExpMests = new List<HIS_EXP_MEST>();

                    long time = Inventec.Common.DateTime.Get.Now().Value;
                    string loginname = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    string username = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();

                    foreach (var sdo in data)
                    {
                        List<HIS_EXP_MEST_MATERIAL> expMestMaterials = null;
                        List<HIS_EXP_MEST_MEDICINE> expMestMedicines = null;
                        HIS_EXP_MEST expMest = null;

                        HisExpMestProcessor expMestProcesser = new HisExpMestProcessor(param);
                        this.expMestProcessors.Add(expMestProcesser);
                        //Tao exp_mest
                        if (!expMestProcesser.Run(sdo, ref expMest, en, time, loginname, username))
                        {
                            throw new Exception("expMestProcesser Rollback du lieu. Ket thuc nghiep vu");
                        }

                        MaterialProcessor materialProcessor = new MaterialProcessor(param);
                        this.materialProcessors.Add(materialProcessor);
                        //Tao exp_mest_material
                        if (!materialProcessor.Run(sdo.ClientSessionKey, sdo.PatientTypeId, sdo.MaterialBeanIds, sdo.Materials, expMest, ref expMestMaterials, ref sqls, en, time, loginname, username))
                        {
                            throw new Exception("materialProcessor. Ket thuc nghiep vu");
                        }

                        MedicineProcessor medicineProcessor = new MedicineProcessor(param);
                        this.medicineProcessors.Add(medicineProcessor);
                        //Tao exp_mest_medicine
                        if (!medicineProcessor.Run(sdo.ClientSessionKey, sdo.PatientTypeId, sdo.MedicineBeanIds, sdo.Medicines, expMest, ref expMestMedicines, ref sqls, en, time, loginname, username))
                        {
                            throw new Exception("medicineProcessor Rollback du lieu. Ket thuc nghiep vu");
                        }

                        //Set TDL_TOTAL_PRICE
                        this.ProcessTdlTotalPrice(sdo, expMest, expMestMaterials, expMestMedicines, ref sqls);

                        HisExpMestResultSDO rsSdo = null;
                        this.PassResult(expMest, expMestMaterials, expMestMedicines, ref rsSdo);
                        resultSdos.Add(rsSdo);
                        hisExpMests.Add(expMest);
                    }
                    ///execute sql se thuc hien cuoi cung de de dang trong viec rollback
                    if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("Rollback du lieu");
                    }

                    result = true;
                    new EventLogGenerator(EventLog.Enum.HisExpMest_TaoPhieuXuatBan, this.GenerateEventLog(hisExpMests)).Run();

                    //LogSystem.Info("Create. Auto Approve Begin");
                    //this.ProcessAuto(resultSdos);
                    //LogSystem.Info("Create. Auto Approve End");

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
                LogSystem.Info("====================CreateList Begin PassResultSale");
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
                LogSystem.Info("====================CreateList End PassResultSale");
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
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
            if (IsNotNullOrEmpty(this.medicineProcessors))
            {
                foreach (var processor in this.medicineProcessors)
                {
                    processor.Rollback();
                }
            }
            if (IsNotNullOrEmpty(this.materialProcessors))
            {
                foreach (var processor in this.materialProcessors)
                {
                    processor.Rollback();
                }
            }
            if (IsNotNullOrEmpty(this.expMestProcessors))
            {
                foreach (var processor in this.expMestProcessors)
                {
                    processor.Rollback();
                }
            }
        }
    }

}
