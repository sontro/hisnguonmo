using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisImpMestMaterial;
using MOS.MANAGER.HisImpMestMedicine;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMest.Moba.Depa
{
    partial class HisImpMestMobaDepaCreate : BusinessBase
    {
        private List<HIS_IMP_MEST_MATERIAL> recentHisImpMestMaterials;
        private List<HIS_IMP_MEST_MEDICINE> recentHisImpMestMedicines;
        private HIS_IMP_MEST recentHisImpMest;
        private HIS_EXP_MEST expMest;

        private HisImpMestCreate hisImpMestCreate;
        private ImpMestMaterialProcessor impMestMaterialProcessor;
        private ImpMestMedicineProcessor impMestMedicineProcessor;
        private HisImpMestAutoProcess hisImpMestAutoProcess;

        internal HisImpMestMobaDepaCreate()
            : base()
        {
            this.Init();
        }

        internal HisImpMestMobaDepaCreate(CommonParam param)
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

        internal bool Create(HisImpMestMobaDepaSDO data, ref HisImpMestResultSDO resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisImpMestMobaCheck checker = new HisImpMestMobaCheck(param);
                HIS_IMP_MEST impMest = new HIS_IMP_MEST();
                valid = valid && checker.ValidateDataDepa(data);
                valid = valid && checker.VerifyExpMestId(impMest, data.ExpMestId, null, ref this.expMest);
                valid = valid && checker.CheckValidMobaDepa(impMest, this.expMest);
                valid = valid && checker.CheckValidRequestRoom(impMest, this.expMest, data.RequestRoomId);
                if (valid)
                {
                    this.ProcessHisImpMest(data, impMest);

                    if (!this.impMestMaterialProcessor.Run(this.recentHisImpMest, data.MobaMaterials, this.expMest, ref this.recentHisImpMestMaterials))
                    {
                        throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                    }

                    if (!this.impMestMedicineProcessor.Run(this.recentHisImpMest, data.MobaMedicines, this.expMest, ref this.recentHisImpMestMedicines))
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
        private void ProcessHisImpMest(HisImpMestMobaDepaSDO data, HIS_IMP_MEST impMest)
        {
            impMest.IMP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__REQUEST;
            impMest.TDL_MOBA_EXP_MEST_CODE = this.expMest.EXP_MEST_CODE;
            impMest.DESCRIPTION = data.Description;
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
                resultData.ImpMedicines = new HisImpMestMedicineGet().GetViewByImpMestId(this.recentHisImpMest.ID);
            }

            if (IsNotNullOrEmpty(this.recentHisImpMestMaterials))
            {
                resultData.ImpMaterials = new HisImpMestMaterialGet().GetViewByImpMestId(this.recentHisImpMest.ID);
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
