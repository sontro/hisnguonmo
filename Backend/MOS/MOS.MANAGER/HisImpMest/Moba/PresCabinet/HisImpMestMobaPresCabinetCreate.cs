using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisImpMestMaterial;
using MOS.MANAGER.HisImpMestMedicine;
using MOS.MANAGER.HisMediStockMaty;
using MOS.MANAGER.HisMediStockMety;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServBill;
using MOS.MANAGER.HisSereServDeposit;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMest.Moba.PresCabinet
{
    partial class HisImpMestMobaPresCabinetCreate : BusinessBase
    {
        private List<HIS_IMP_MEST_MATERIAL> recentImpMaterials;
        private List<HIS_IMP_MEST_MEDICINE> recentImpMedicines;
        private List<HIS_IMP_MEST> recentHisImpMests;

        private ImpMestProcessor impMestProcessor;
        private MaterialProcessor materialProcessor;
        private MedicineProcessor medicineProcessor;
        private SereServProcessor sereServProcessor;

        internal HisImpMestMobaPresCabinetCreate()
            : base()
        {
            this.Init();
        }

        internal HisImpMestMobaPresCabinetCreate(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.impMestProcessor = new ImpMestProcessor(param);
            this.materialProcessor = new MaterialProcessor(param);
            this.medicineProcessor = new MedicineProcessor(param);
            this.sereServProcessor = new SereServProcessor(param);
        }

        internal bool Run(HisImpMestMobaPresCabinetSDO data, ref List<HisImpMestResultSDO> resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_EXP_MEST expMest = null;
                List<HIS_IMP_MEST> impMests = new List<HIS_IMP_MEST>();
                HIS_TREATMENT treatment = null;
                List<HIS_EXP_MEST_MEDICINE> expMedicines = null;
                List<HIS_EXP_MEST_MATERIAL> expMaterials = null;
                List<HIS_SERE_SERV> allSereServs = new List<HIS_SERE_SERV>();
                WorkPlaceSDO workPlace = null;
                Dictionary<long, MobaData> dicImpMest = new Dictionary<long, MobaData>();

                HisImpMestMobaPresCabinetCheck checker = new HisImpMestMobaPresCabinetCheck(param);
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);

                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.VerifyExpMestId(data.ExpMestId, ref expMest);
                valid = valid && treatmentChecker.IsUnLock(expMest.TDL_TREATMENT_ID.Value, ref treatment);
                valid = valid && treatmentChecker.IsUnTemporaryLock(treatment);
                valid = valid && checker.VerifyAllowMoba(data, expMest);
                valid = valid && checker.VerifyWorkingRoom(expMest, data.WorkingRoomId, ref workPlace);
                valid = valid && checker.ValidateDataPres(data, expMest, ref expMedicines, ref expMaterials);
                valid = valid && checker.CheckMaxDayAllowMobaPrescription(expMest);
                valid = valid && checker.VerifySereServ(expMest, allSereServs);
                valid = valid && this.PrepareData(data, expMest, expMedicines, expMaterials, ref dicImpMest);
                if (valid)
                {

                    List<HIS_SERE_SERV> lisSereServUpdates = new List<HIS_SERE_SERV>();
                    Mapper.CreateMap<HIS_SERE_SERV, HIS_SERE_SERV>();

                    List<HIS_SERE_SERV> allSereServBeforeUpdates = Mapper.Map<List<HIS_SERE_SERV>>(allSereServs);

                    if (!this.impMestProcessor.Run(data, expMest, dicImpMest, workPlace, ref this.recentHisImpMests))
                    {
                        throw new Exception("impMestProcessor. Ket thuc nghiep vu. Rollback du lieu");
                    }

                    //Xu ly HisImpMestMedicine
                    if (!materialProcessor.Run(this.recentHisImpMests, dicImpMest, expMaterials, allSereServs, ref this.recentImpMaterials, ref lisSereServUpdates))
                    {
                        throw new Exception("materialProcessor. Ket thuc nghiep vu. Rollback du lieu");
                    }

                    //Xu ly HisImpMestMaterial
                    if (!this.medicineProcessor.Run(this.recentHisImpMests, dicImpMest, expMedicines, allSereServs, ref this.recentImpMedicines, ref lisSereServUpdates))
                    {
                        throw new Exception("medicineProcessor. Ket thuc nghiep vu. Rollback du lieu");
                    }

                    //Xu ly HisSereServ
                    if (!this.sereServProcessor.Run(treatment, allSereServs, lisSereServUpdates, allSereServBeforeUpdates))
                    {
                        throw new Exception("sereServProcessor. Ket thuc nghiep vu. Rollback du lieu");
                    }

                    this.ProcessAuto();

                    this.PassResult(ref resultData);

                    this.ProcessEventLog(expMest);

                    result = true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                this.RollbackData();
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private bool PrepareData(HisImpMestMobaPresCabinetSDO data, HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MEDICINE> expMedicines, List<HIS_EXP_MEST_MATERIAL> expMaterials, ref Dictionary<long, MobaData> dicImpMest)
        {
            bool valid = true;
            try
            {
                if (HisImpMestCFG.AUTO_SET_IMP_STOCK_MOBA_PRES_CABINET)
                {
                    List<string> notExists = new List<string>();
                    if (IsNotNullOrEmpty(data.MobaPresMedicines))
                    {
                        HisMediStockMetyFilterQuery metyFilter = new HisMediStockMetyFilterQuery();
                        metyFilter.MEDICINE_TYPE_IDs = expMedicines.Select(s => s.TDL_MEDICINE_TYPE_ID ?? 0).ToList();
                        metyFilter.MEDI_STOCK_ID = expMest.MEDI_STOCK_ID;
                        List<HIS_MEDI_STOCK_METY> stockMetys = new HisMediStockMetyGet().Get(metyFilter);

                        foreach (HisMobaPresMedicineSDO sdo in data.MobaPresMedicines)
                        {
                            HIS_EXP_MEST_MEDICINE medicine = expMedicines.FirstOrDefault(o => o.ID == sdo.ExpMestMedicineId);
                            HIS_MEDI_STOCK_METY mety = stockMetys != null ? stockMetys.FirstOrDefault(o => o.MEDICINE_TYPE_ID == medicine.TDL_MEDICINE_TYPE_ID) : null;
                            if (mety == null || !mety.EXP_MEDI_STOCK_ID.HasValue)
                            {
                                if (!data.ImpMediStockId.HasValue)
                                {
                                    HIS_MEDICINE_TYPE medicineType = HisMedicineTypeCFG.DATA.FirstOrDefault(o => o.ID == medicine.TDL_MEDICINE_TYPE_ID);
                                    string name = medicineType != null ? medicineType.MEDICINE_TYPE_NAME : "";
                                    notExists.Add(name);
                                    continue;
                                }
                                MobaData d = null;
                                if (dicImpMest.ContainsKey(data.ImpMediStockId.Value))
                                {
                                    d = dicImpMest[data.ImpMediStockId.Value];
                                }
                                else
                                {
                                    d = new MobaData();
                                    dicImpMest[data.ImpMediStockId.Value] = d;
                                }
                                if (d.MobaMedicines == null) d.MobaMedicines = new List<HisMobaPresMedicineSDO>();
                                d.MobaMedicines.Add(sdo);
                            }
                            else
                            {
                                MobaData d = null;
                                if (dicImpMest.ContainsKey(mety.EXP_MEDI_STOCK_ID.Value))
                                {
                                    d = dicImpMest[mety.EXP_MEDI_STOCK_ID.Value];
                                }
                                else
                                {
                                    d = new MobaData();
                                    dicImpMest[mety.EXP_MEDI_STOCK_ID.Value] = d;
                                }
                                if (d.MobaMedicines == null) d.MobaMedicines = new List<HisMobaPresMedicineSDO>();
                                d.MobaMedicines.Add(sdo);
                            }

                        }
                    }

                    if (IsNotNullOrEmpty(data.MobaPresMaterials))
                    {
                        HisMediStockMatyFilterQuery matyFilter = new HisMediStockMatyFilterQuery();
                        matyFilter.MATERIAL_TYPE_IDs = expMaterials.Select(s => s.TDL_MATERIAL_TYPE_ID ?? 0).ToList();
                        matyFilter.MEDI_STOCK_ID = expMest.MEDI_STOCK_ID;
                        List<HIS_MEDI_STOCK_MATY> stockMatys = new HisMediStockMatyGet().Get(matyFilter);

                        foreach (HisMobaPresMaterialSDO sdo in data.MobaPresMaterials)
                        {
                            HIS_EXP_MEST_MATERIAL material = expMaterials.FirstOrDefault(o => o.ID == sdo.ExpMestMaterialId);
                            HIS_MEDI_STOCK_MATY mety = stockMatys != null ? stockMatys.FirstOrDefault(o => o.MATERIAL_TYPE_ID == material.TDL_MATERIAL_TYPE_ID) : null;
                            if (mety == null || !mety.EXP_MEDI_STOCK_ID.HasValue)
                            {
                                if (!data.ImpMediStockId.HasValue)
                                {
                                    HIS_MATERIAL_TYPE materialType = HisMaterialTypeCFG.DATA.FirstOrDefault(o => o.ID == material.TDL_MATERIAL_TYPE_ID);
                                    string name = materialType != null ? materialType.MATERIAL_TYPE_NAME : "";
                                    notExists.Add(name);
                                    continue;
                                }
                                MobaData d = null;
                                if (dicImpMest.ContainsKey(data.ImpMediStockId.Value))
                                {
                                    d = dicImpMest[data.ImpMediStockId.Value];
                                }
                                else
                                {
                                    d = new MobaData();
                                    dicImpMest[data.ImpMediStockId.Value] = d;
                                }
                                if (d.MobaMaterials == null) d.MobaMaterials = new List<HisMobaPresMaterialSDO>();
                                d.MobaMaterials.Add(sdo);
                            }
                            else
                            {
                                MobaData d = null;
                                if (dicImpMest.ContainsKey(mety.EXP_MEDI_STOCK_ID.Value))
                                {
                                    d = dicImpMest[mety.EXP_MEDI_STOCK_ID.Value];
                                }
                                else
                                {
                                    d = new MobaData();
                                    dicImpMest[mety.EXP_MEDI_STOCK_ID.Value] = d;
                                }
                                if (d.MobaMaterials == null) d.MobaMaterials = new List<HisMobaPresMaterialSDO>();
                                d.MobaMaterials.Add(sdo);
                            }

                        }
                    }
                }
                else
                {
                    long mediStock = data.ImpMediStockId.HasValue ? data.ImpMediStockId.Value : expMest.MEDI_STOCK_ID;
                    MobaData d = new MobaData();
                    d.MobaMaterials = data.MobaPresMaterials;
                    d.MobaMedicines = data.MobaPresMedicines;
                    dicImpMest[mediStock] = d;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        private void ProcessAuto()
        {
            try
            {
                foreach (var imp in this.recentHisImpMests)
                {
                    try
                    {

                        HisImpMestAutoProcess hisImpMestAutoProcess = new HisImpMestAutoProcess(param);
                        hisImpMestAutoProcess.Run(imp);
                    }
                    catch (Exception ex)
                    {
                        Inventec.Common.Logging.LogSystem.Error(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void PassResult(ref List<HisImpMestResultSDO> resultData)
        {
            try
            {
                resultData = new List<HisImpMestResultSDO>();
                foreach (var imp in this.recentHisImpMests)
                {
                    HisImpMestResultSDO rs = new HisImpMestResultSDO();
                    rs.ImpMest = new HisImpMestGet().GetViewById(imp.ID);

                    if (IsNotNullOrEmpty(this.recentImpMedicines) && this.recentImpMedicines.Any(a => a.IMP_MEST_ID == imp.ID))
                    {
                        rs.ImpMedicines = new HisImpMestMedicineGet().GetViewByImpMestId(rs.ImpMest.ID);
                    }
                    if (IsNotNullOrEmpty(this.recentImpMedicines) && this.recentImpMedicines.Any(a => a.IMP_MEST_ID == imp.ID))
                    {
                        rs.ImpMaterials = new HisImpMestMaterialGet().GetViewByImpMestId(rs.ImpMest.ID);
                    }
                    resultData.Add(rs);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void ProcessEventLog(HIS_EXP_MEST expMest)
        {
            try
            {
                foreach (var impMest in this.recentHisImpMests)
                {
                    new EventLogGenerator(EventLog.Enum.HisImpMest_TaoPhieuNhapThuHoi).ImpMestCode(impMest.IMP_MEST_CODE).ExpMestCode(expMest.EXP_MEST_CODE).Run();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        internal void RollbackData()
        {
            this.sereServProcessor.Rollback();
            this.medicineProcessor.Rollback();
            this.materialProcessor.Rollback();
            this.impMestProcessor.Rollback();
        }
    }

    class MobaData
    {
        public List<HisMobaPresMedicineSDO> MobaMedicines { get; set; }
        public List<HisMobaPresMaterialSDO> MobaMaterials { get; set; }
    }
}
