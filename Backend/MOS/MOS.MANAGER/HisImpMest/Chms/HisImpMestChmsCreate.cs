using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisImpMestBlood;
using MOS.MANAGER.HisImpMestMaterial;
using MOS.MANAGER.HisImpMestMedicine;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMest.Chms
{
    partial class HisImpMestChmsCreate : BusinessBase
    {
        private HIS_IMP_MEST recentHisImpMest;
        private List<HIS_IMP_MEST_MATERIAL> recentHisImpMestMaterials;
        private List<HIS_IMP_MEST_MEDICINE> recentHisImpMestMedicines;
        private List<HIS_IMP_MEST_BLOOD> recentHisImpMestBloods;

        private HisImpMestCreate hisImpMestCreate;
        private MedicineProcessor medicineProcessor;
        private MaterialProcessor materialProcessor;
        private BloodProcessor bloodProcessor;
        private HisImpMestAutoProcess hisImpMestAutoProcess;

        internal HisImpMestChmsCreate()
            : base()
        {
            this.Init();
        }

        internal HisImpMestChmsCreate(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisImpMestCreate = new HisImpMestCreate(param);
            this.materialProcessor = new MaterialProcessor(param);
            this.medicineProcessor = new MedicineProcessor(param);
            this.bloodProcessor = new BloodProcessor(param);
            this.hisImpMestAutoProcess = new HisImpMestAutoProcess(param);
        }

        internal bool Create(HIS_IMP_MEST data, ref HisImpMestResultSDO resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_EXP_MEST expMest = null;
                List<HIS_EXP_MEST_MATERIAL> hisExpMestMaterials = null;
                List<HIS_EXP_MEST_MEDICINE> hisExpMestMedicines = null;
                List<HIS_EXP_MEST_BLOOD> hisExpMestBloods = null;
                HisImpMestChmsCheck checker = new HisImpMestChmsCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.VerifyExpMestId(data.CHMS_EXP_MEST_ID.Value, ref expMest);
                valid = valid && checker.IsValidExpMestType(data, expMest);
                valid = valid && checker.IsValidExpMestStt(expMest);
                valid = valid && checker.IsValidImpMediStock(data, expMest);
                valid = valid && checker.IsNotExistsImpMest(expMest);
                valid = valid && checker.IsNotExistsMoba(expMest);
                valid = valid && checker.IsValidMediStock(data);
                valid = valid && checker.ValidData(expMest, ref hisExpMestMedicines, ref hisExpMestMaterials, ref hisExpMestBloods);
                if (valid)
                {
                    List<string> sqls = new List<string>();

                    this.ProcessHisImpMest(data, expMest);

                    if (!this.materialProcessor.Run(this.recentHisImpMest, hisExpMestMaterials, ref this.recentHisImpMestMaterials, ref sqls))
                    {
                        throw new Exception("materialProcessor. Rollback du lieu");
                    }

                    if (!this.medicineProcessor.Run(this.recentHisImpMest, hisExpMestMedicines, ref this.recentHisImpMestMedicines, ref sqls))
                    {
                        throw new Exception("medicineProcessor. Rollback du lieu");
                    }

                    if (!this.bloodProcessor.Run(this.recentHisImpMest, hisExpMestBloods, ref this.recentHisImpMestBloods))
                    {
                        throw new Exception("bloodProcessor. Rollback du lieu");
                    }

                    if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("sqls: " + sqls.ToString());
                    }

                    this.PassResult(ref resultData);
                    result = true;

                    new EventLogGenerator(EventLog.Enum.HisImpMest_TaoPhieuNhapChuyenKho).ImpMestCode(this.recentHisImpMest.IMP_MEST_CODE).ExpMestCode(expMest.EXP_MEST_CODE).Run();

                    this.ProcessAuto(expMest);

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

        private void PassResult(ref HisImpMestResultSDO resultData)
        {
            resultData = new HisImpMestResultSDO();
            resultData.ImpMest = new HisImpMestGet().GetViewById(this.recentHisImpMest.ID);
            resultData.ImpMedicines = new HisImpMestMedicineGet().GetViewByImpMestId(this.recentHisImpMest.ID);
            resultData.ImpMaterials = new HisImpMestMaterialGet().GetViewByImpMestId(this.recentHisImpMest.ID);
            resultData.ImpBloods = new HisImpMestBloodGet().GetViewByImpMestId(this.recentHisImpMest.ID);
        }

        private void ProcessHisImpMest(HIS_IMP_MEST data, HIS_EXP_MEST expMest)
        {
            Mapper.CreateMap<HIS_IMP_MEST, HIS_IMP_MEST>();
            HIS_IMP_MEST update = Mapper.Map<HIS_IMP_MEST>(data);
            update.TDL_CHMS_EXP_MEST_CODE = expMest.EXP_MEST_CODE;
            update.CHMS_MEDI_STOCK_ID = expMest.MEDI_STOCK_ID;

            if (!this.hisImpMestCreate.Create(update))
            {
                throw new Exception("Khong tao duoc HIS_IMP_MEST. Ket thuc nghiep vu");
            }
            this.recentHisImpMest = update;
        }

        private void ProcessAuto(HIS_EXP_MEST expMest)
        {
            try
            {
                this.hisImpMestAutoProcess.Run(recentHisImpMest, expMest.MEDI_STOCK_ID);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        internal void RollbackData()
        {
            this.medicineProcessor.Rollback();
            this.materialProcessor.Rollback();
            this.bloodProcessor.Rollback();
            this.hisImpMestCreate.RollbackData();
        }
    }
}
