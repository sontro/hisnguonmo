using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisImpMest.Common;
using MOS.MANAGER.HisImpMestMedicine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMest.UnImport
{
    class ThreadData
    {
        public List<long> MedicineTypeIds = new List<long>();
        public List<long> MaterialTypeIds = new List<long>();
    }
    partial class HisImpMestCancelImport : BusinessBase
    {
        HIS_IMP_MEST beforeHisImpMest;

        private BloodProcessor bloodProcessor;
        private MaterialProcessor materialProcessor;
        private MedicineProcessor medicineProcessor;
        private MaterialReusableProcessor materialReusProcessor;

        internal HisImpMestCancelImport()
            : base()
        {
            this.Init();
        }

        internal HisImpMestCancelImport(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.bloodProcessor = new BloodProcessor(param);
            this.materialProcessor = new MaterialProcessor(param);
            this.medicineProcessor = new MedicineProcessor(param);
            this.materialReusProcessor = new MaterialReusableProcessor(param);
        }

        internal bool Cancel(HIS_IMP_MEST data, ref HIS_IMP_MEST resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_IMP_MEST raw = null;
                HisImpMestCheck checker = new HisImpMestCheck(param);
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.HasNotInAggrImpMest(raw);
                valid = valid && checker.HasNotMediStockPeriod(raw);
                valid = valid && checker.HasNoNationalCode(raw);
                valid = valid && checker.IsUnLockMediStock(raw);
                valid = valid && checker.VerifyStatusForCancelImport(raw);
                valid = valid && checker.VerifyTypeForCancelImport(raw);
                valid = valid && checker.CheckMediStockPermission(raw, false);
                valid = valid && checker.CheckImpLoginnamePermission(raw);
                List<long> MedicineTypeIds=new List<long>();
                List<long> MaterialTypeIds=new List<long>();
                if (valid)
                {
                    List<HIS_IMP_MEST_MATERIAL> materials = null;
                    if (!this.bloodProcessor.Run(raw))
                    {
                        throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                    }

                    if (!this.materialProcessor.Run(raw, ref materials, ref MaterialTypeIds))
                    {
                        throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                    }

                    if (!this.materialReusProcessor.Run(raw, materials, ref MaterialTypeIds))
                    {
                        throw new Exception("materialReusProcessor. Ket thuc nghiep vu. Rollback du lieu");
                    }

                    if (!this.medicineProcessor.Run(raw, ref MedicineTypeIds))
                    {
                        throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                    }

                    this.ProcessImpMest(raw);
                    resultData = raw;
                    result = true;
                    new EventLogGenerator(EventLog.Enum.HisImpMest_HuyThucNhapPhieuNhap).ImpMestCode(raw.IMP_MEST_CODE).Run();

                    this.InitThreadUpdateLastExpPrice(MedicineTypeIds, MaterialTypeIds);
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

        internal void ProcessImpMest(HIS_IMP_MEST data)
        {
            Mapper.CreateMap<HIS_IMP_MEST, HIS_IMP_MEST>();
            HIS_IMP_MEST before = Mapper.Map<HIS_IMP_MEST>(data);
            data.IMP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__APPROVAL;
            data.IMP_TIME = null;
            data.IMP_USERNAME = null;
            data.IMP_LOGINNAME = null;
            if (!DAOWorker.HisImpMestDAO.Update(data))
            {
                throw new Exception("Khong update duoc HIS_IMP_MEST. Ket thuc nghiep vu.");
            }
            this.beforeHisImpMest = before;
        }

        private void InitThreadUpdateLastExpPrice(List<long> medicineTypeIds, List<long> materialTypeIds)
        {
            try
            {

                Thread thread = new Thread(new ParameterizedThreadStart(this.ThreadUpdateLastExpPrice));
                thread.Priority = ThreadPriority.Highest;
                ThreadData threadData = new ThreadData();
                threadData.MedicineTypeIds = medicineTypeIds;
                threadData.MaterialTypeIds = materialTypeIds;
                thread.Start(threadData);
            }
            catch (Exception ex)
            {
                LogSystem.Error("Khoi tao tien trinh cap nhat gia ban moi nhat that bai", ex);
            }
        }

        private void ThreadUpdateLastExpPrice(object data)
        {
            try
            {
                ThreadData threadData = (ThreadData)data;
                new LastPriceProcessor().Run(threadData.MedicineTypeIds, threadData.MaterialTypeIds);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        internal void RollbackData()
        {
            if (this.beforeHisImpMest != null)
            {
                if (!DAOWorker.HisImpMestDAO.Update(this.beforeHisImpMest))
                {
                    LogSystem.Warn("Rollback HIS_IMP_MEST that bai. Kiem tra lai du lieu.");
                }
            }
            this.medicineProcessor.Rollback();
            this.materialProcessor.Rollback();
            this.bloodProcessor.Rollback();
        }
    }
}
