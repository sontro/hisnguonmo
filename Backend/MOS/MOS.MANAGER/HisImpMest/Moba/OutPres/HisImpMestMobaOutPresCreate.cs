using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisImpMestMaterial;
using MOS.MANAGER.HisImpMestMedicine;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMest.Moba.OutPres
{
    class HisImpMestMobaOutPresCreate : BusinessBase
    {
        private List<HIS_IMP_MEST_MATERIAL> recentHisImpMestMaterials;
        private List<HIS_IMP_MEST_MEDICINE> recentHisImpMestMedicines;
        private List<HIS_SERE_SERV> recentSereServMedicines;
        private List<HIS_SERE_SERV> recentSereServMaterials;
        private HIS_IMP_MEST recentHisImpMest;
        private HIS_EXP_MEST expMest;
        private HIS_SERVICE_REQ serviceReq;

        private HisImpMestCreate hisImpMestCreate;
        private ImpMestMaterialProcessor impMestMaterialProcessor;
        private ImpMestMedicineProcessor impMestMedicineProcessor;
        private HisImpMestAutoProcess hisImpMestAutoProcess;

        internal HisImpMestMobaOutPresCreate()
            : base()
        {
            this.Init();
        }

        internal HisImpMestMobaOutPresCreate(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisImpMestCreate = new HisImpMestCreate(param);
            this.impMestMaterialProcessor = new ImpMestMaterialProcessor(param);
            this.impMestMedicineProcessor = new ImpMestMedicineProcessor(param);
            this.hisImpMestAutoProcess = new HisImpMestAutoProcess(param);
        }

        internal bool Run(HisImpMestMobaOutPresSDO data, ref HisImpMestResultSDO resultData)
        {
            bool result = false;
            try
            {
                if (!HisExpMestCFG.ALLOW_MOBA_EXAM_PRES)
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisImpMest_KhongChoPhepThuHoiDonPhongKham);
                    return false;
                }
                bool valid = true;
                HIS_IMP_MEST impMest = new HIS_IMP_MEST();
                List<HIS_EXP_MEST_MEDICINE> allExpMestMedicines = null;
                List<HIS_EXP_MEST_MATERIAL> allExpMestMaterials = null;
                HisImpMestMobaOutPresCheck checker = new HisImpMestMobaOutPresCheck(param);
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);

                valid = valid && checker.ValidData(data);
                valid = valid && checker.VerifyServiceReqId(data.ServiceReqId, ref this.serviceReq, ref this.expMest);
                valid = valid && checker.CheckValidRequestRoom(impMest, this.expMest, data.RequestRoomId);
                valid = valid && checker.VerifySereServ(data, ref this.recentSereServMedicines, ref this.recentSereServMaterials);
                valid = valid && checker.VerifyExpMest(this.expMest, ref allExpMestMedicines, ref allExpMestMaterials);
                if (valid)
                {
                    this.ProcessHisImpMest(data, impMest);

                    //Xu ly HisImpMestMedicine
                    if (!impMestMaterialProcessor.Run(this.recentHisImpMest, data.MobaPresMaterials, allExpMestMaterials, this.recentSereServMaterials, ref this.recentHisImpMestMaterials))
                    {
                        throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                    }

                    //Xu ly HisImpMestMaterial
                    if (!this.impMestMedicineProcessor.Run(this.recentHisImpMest, data.MobaPresMedicines, allExpMestMedicines, this.recentSereServMedicines, ref this.recentHisImpMestMedicines))
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

        private void ProcessHisImpMest(HisImpMestMobaOutPresSDO data, HIS_IMP_MEST impMest)
        {
            impMest.IMP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__REQUEST;
            impMest.TDL_MOBA_EXP_MEST_CODE = this.expMest.EXP_MEST_CODE;
            impMest.DESCRIPTION = data.Description;
            impMest.IMP_MEST_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__DONKTL;
            impMest.MOBA_EXP_MEST_ID = this.expMest.ID;
            impMest.MEDI_STOCK_ID = this.expMest.MEDI_STOCK_ID;
            impMest.SPECIAL_MEDICINE_TYPE = this.expMest.SPECIAL_MEDICINE_TYPE; //lay thong tin phan loai don thuoc dac biet dua vao phieu xuat
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
            this.impMestMedicineProcessor.Rollback();
            this.impMestMaterialProcessor.Rollback();
            this.hisImpMestCreate.RollbackData();
        }
    }
}
