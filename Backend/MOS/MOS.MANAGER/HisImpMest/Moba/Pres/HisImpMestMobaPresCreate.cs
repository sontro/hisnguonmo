using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisImpMestMaterial;
using MOS.MANAGER.HisImpMestMedicine;
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

namespace MOS.MANAGER.HisImpMest.Moba.Pres
{
    partial class HisImpMestMobaPresCreate : BusinessBase
    {
        private List<HIS_IMP_MEST_MATERIAL> recentHisImpMestMaterials;
        private List<HIS_IMP_MEST_MEDICINE> recentHisImpMestMedicines;
        private List<HIS_EXP_MEST_MEDICINE> recentExpMestMedicines;
        private List<HIS_EXP_MEST_MATERIAL> recentExpMestMaterials;
        private HIS_IMP_MEST recentHisImpMest;
        private HIS_EXP_MEST expMest;

        private HisImpMestCreate hisImpMestCreate;
        private ImpMestMaterialProcessor impMestMaterialProcessor;
        private ImpMestMedicineProcessor impMestMedicineProcessor;
        private SereServProcessor sereServProcessor;
        private HisImpMestAutoProcess hisImpMestAutoProcess;

        internal HisImpMestMobaPresCreate()
            : base()
        {
            this.Init();
        }

        internal HisImpMestMobaPresCreate(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisImpMestCreate = new HisImpMestCreate(param);
            this.impMestMaterialProcessor = new ImpMestMaterialProcessor(param);
            this.impMestMedicineProcessor = new ImpMestMedicineProcessor(param);
            this.sereServProcessor = new SereServProcessor(param);
            this.hisImpMestAutoProcess = new HisImpMestAutoProcess(param);
        }

        internal bool Create(HisImpMestMobaPresSDO data, ref HisImpMestResultSDO resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_IMP_MEST impMest = new HIS_IMP_MEST();
                HIS_TREATMENT treatment = null;
                this.recentExpMestMedicines = new List<HIS_EXP_MEST_MEDICINE>();
                this.recentExpMestMaterials = new List<HIS_EXP_MEST_MATERIAL>();
                List<HIS_SERE_SERV> allSereServs = new List<HIS_SERE_SERV>();
                HisImpMestMobaCheck checker = new HisImpMestMobaCheck(param);
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);

                valid = valid && checker.VerifyExpMestId(impMest, data.ExpMestId, data.ImpMediStockId, ref this.expMest);
                valid = valid && treatmentChecker.IsUnLock(expMest.TDL_TREATMENT_ID.Value, ref treatment);
                valid = valid && treatmentChecker.IsUnTemporaryLock(treatment);
                valid = valid && checker.CheckValidMobaPres(impMest, this.expMest);
                valid = valid && checker.CheckValidRequestRoom(impMest, this.expMest, data.RequestRoomId);
                valid = valid && checker.ValidateDataPres(data, this.recentExpMestMedicines, this.recentExpMestMaterials, this.expMest);
                valid = valid && checker.CheckMaxDayAllowMobaPrescription(this.expMest);
                valid = valid && checker.VerifyServiceReq(this.expMest);
                valid = valid && checker.VerifySereServ(this.expMest, allSereServs);
                if (valid)
                {

                    List<HIS_SERE_SERV> lisSereServUpdates = new List<HIS_SERE_SERV>();
                    Mapper.CreateMap<HIS_SERE_SERV, HIS_SERE_SERV>();

                    List<HIS_SERE_SERV> allSereServBeforeUpdates = Mapper.Map<List<HIS_SERE_SERV>>(allSereServs);
                    this.ProcessHisImpMest(data, impMest);

                    //Xu ly HisImpMestMedicine
                    if (!impMestMaterialProcessor.Run(this.recentHisImpMest, data.MobaPresMaterials, this.recentExpMestMaterials, allSereServs, ref this.recentHisImpMestMaterials, ref lisSereServUpdates))
                    {
                        throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                    }

                    //Xu ly HisImpMestMaterial
                    if (!this.impMestMedicineProcessor.Run(this.recentHisImpMest, data.MobaPresMedicines, this.recentExpMestMedicines, allSereServs, ref this.recentHisImpMestMedicines, ref lisSereServUpdates))
                    {
                        throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                    }

                    //Xu ly HisSereServ
                    if (!this.sereServProcessor.Run(treatment, allSereServs, lisSereServUpdates, allSereServBeforeUpdates))
                    {
                        throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                    }

                    this.ProcessAuto();

                    this.PassResult(ref resultData);

                    new EventLogGenerator(EventLog.Enum.HisImpMest_TaoPhieuNhapThuHoi).ImpMestCode(this.recentHisImpMest.IMP_MEST_CODE).ExpMestCode(this.expMest.EXP_MEST_CODE).Run();

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

        /// <summary>
        /// Xu ly thong tin phieu nhap
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private void ProcessHisImpMest(HisImpMestMobaPresSDO data, HIS_IMP_MEST impMest)
        {
            impMest.IMP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__REQUEST;
            impMest.TDL_MOBA_EXP_MEST_CODE = this.expMest.EXP_MEST_CODE;
            impMest.SPECIAL_MEDICINE_TYPE = this.expMest.SPECIAL_MEDICINE_TYPE; //lay thong tin phan loai don thuoc dac biet dua vao phieu xuat
            impMest.DESCRIPTION = data.Description;
            impMest.TRACKING_ID = data.TrackingId;

            HisImpMestUtil.SetTdl(impMest, expMest);
            if (!this.hisImpMestCreate.Create(impMest))
            {
                throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
            }
            this.recentHisImpMest = impMest;
        }

        private void ProcessAuto()
        {
            try
            {
                this.hisImpMestAutoProcess.Run(this.recentHisImpMest);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void PassResult(ref HisImpMestResultSDO resultData)
        {
            resultData = new HisImpMestResultSDO();
            resultData.ImpMest = new HisImpMestGet().GetViewById(this.recentHisImpMest.ID);

            if (IsNotNullOrEmpty(this.recentHisImpMestMedicines))
            {
                resultData.ImpMedicines = new HisImpMestMedicineGet().GetViewByImpMestId(resultData.ImpMest.ID);
            }
            if (IsNotNullOrEmpty(this.recentHisImpMestMaterials))
            {
                resultData.ImpMaterials = new HisImpMestMaterialGet().GetViewByImpMestId(resultData.ImpMest.ID);
            }
        }

        internal void RollbackData()
        {
            this.sereServProcessor.Rollback();
            this.impMestMedicineProcessor.Rollback();
            this.impMestMaterialProcessor.Rollback();
            this.hisImpMestCreate.RollbackData();
        }
    }
}
