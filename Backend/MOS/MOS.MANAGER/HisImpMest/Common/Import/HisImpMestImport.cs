using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisImpMest.Common;
using MOS.MANAGER.HisImpMestMaterial;
using MOS.MANAGER.HisImpMestMedicine;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMest.Import
{
    class ThreadData
    {
        public List<long> MedicineTypeIds = new List<long>();
        public List<long> MaterialTypeIds = new List<long>();
    }
    partial class HisImpMestImport : BusinessBase
    {
        private List<HIS_IMP_MEST_MATERIAL> hisImpMestMaterials;
        private List<HIS_IMP_MEST_MEDICINE> hisImpMestMedicines;
        private List<HIS_IMP_MEST> beforeHisImpMests = new List<HIS_IMP_MEST>();
        private HIS_IMP_MEST recentHisImpMest;

        private BloodProcessor bloodProcessor;
        private MaterialProcessor materialProcessor;
        private MedicineProcessor medicineProcessor;

        internal HisImpMestImport()
            : base()
        {
            this.Init();
        }

        internal HisImpMestImport(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.bloodProcessor = new BloodProcessor(param);
            this.materialProcessor = new MaterialProcessor(param);
            this.medicineProcessor = new MedicineProcessor(param);
        }

        internal bool Import(HIS_IMP_MEST data, bool isAuto, ref HIS_IMP_MEST resultData)
        {
            bool result = false;
            try
            {
                HIS_IMP_MEST raw = null;
                HisImpMestCheck checker = new HisImpMestCheck(param);
                bool valid = true;
                List<long> MedicineTypeIds = new List<long>();
                List<long> MaterialTypeIds = new List<long>();
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.HasNotMediStockPeriod(raw);
                valid = valid && checker.IsUnLockMediStock(raw);
                valid = valid && checker.VerifyStatusForImport(raw);
                valid = valid && checker.CheckMediStockPermission(raw, isAuto);
                valid = valid && checker.HasNotInAggrImpMest(raw);
                valid = valid && checker.IsValidTypeChangeStatus(raw, IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__IMPORT);
                valid = valid && checker.IsValidImpTime(data.IMP_TIME);

                if (valid)
                {
                    this.ProcessHisImpMest(raw, data.IMP_TIME);
                    this.ProcessChildHisImpMest();

                    if (!this.materialProcessor.Run(this.recentHisImpMest, this.hisImpMestMaterials, ref MaterialTypeIds))
                    {
                        throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                    }
                    if (!this.medicineProcessor.Run(this.recentHisImpMest, this.hisImpMestMedicines, ref MedicineTypeIds))
                    {
                        throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                    }
                    if (!this.bloodProcessor.Run(this.recentHisImpMest))
                    {
                        throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                    }

                    this.PassResult(ref resultData);

                    result = true;
                    new EventLogGenerator(EventLog.Enum.HisImpMest_ThucNhapPhieuNhap).ImpMestCode(this.recentHisImpMest.IMP_MEST_CODE).Run();

                    this.InitThreadUpdateLastExpPrice(MedicineTypeIds, MaterialTypeIds);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                this.Rollback();
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void ProcessHisImpMest(HIS_IMP_MEST raw, long? impTime)
        {

            Mapper.CreateMap<HIS_IMP_MEST, HIS_IMP_MEST>();
            this.beforeHisImpMests.Add(Mapper.Map<HIS_IMP_MEST>(raw));

            raw.IMP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__IMPORT;
            raw.IMP_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
            raw.IMP_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
            if (impTime.HasValue)
            {
                raw.IMP_TIME = impTime.Value;
                // Neu thoi gian duyet > thoi gian nhap thi gan lai thoi gian duyet tranh Check o DB IMP_TIME >= APPROVAL_TIME 
                if (raw.APPROVAL_TIME > raw.IMP_TIME)
                {
                    raw.APPROVAL_TIME = raw.IMP_TIME;
                }
            }
            else
            {
                raw.IMP_TIME = Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(DateTime.Now);
            }

            if (!DAOWorker.HisImpMestDAO.Update(raw))
            {
                MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.CapNhatThatBai);
                throw new Exception("Cap nhat thong tin hisExpMest that bai." + LogUtil.TraceData("raw", raw));
            }
            this.recentHisImpMest = raw;
        }

        private void ProcessChildHisImpMest()
        {
            if (this.recentHisImpMest.IMP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__THT)
            {
                List<HIS_IMP_MEST> listChild = new HisImpMestGet().GetByAggrImpMestId(this.recentHisImpMest.ID);
                if (IsNotNullOrEmpty(listChild))
                {
                    HisImpMestCheck childChecker = new HisImpMestCheck(param);
                    bool valid = true;
                    valid = valid && childChecker.IsUnLock(listChild);
                    foreach (var impRaw in listChild)
                    {
                        valid = valid && childChecker.HasNotMediStockPeriod(impRaw);
                        valid = valid && childChecker.VerifyStatusForImport(impRaw);
                    }
                    if (!valid)
                    {
                        throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                    }
                    Mapper.CreateMap<HIS_IMP_MEST, HIS_IMP_MEST>();
                    List<HIS_IMP_MEST> befores = Mapper.Map<List<HIS_IMP_MEST>>(listChild);
                    listChild.ForEach(o =>
                        {
                            o.IMP_MEST_STT_ID = this.recentHisImpMest.IMP_MEST_STT_ID;
                            o.IMP_TIME = this.recentHisImpMest.IMP_TIME;
                            o.IMP_USERNAME = this.recentHisImpMest.IMP_USERNAME;
                            o.IMP_LOGINNAME = this.recentHisImpMest.IMP_LOGINNAME;
                        });
                    if (!DAOWorker.HisImpMestDAO.UpdateList(listChild))
                    {
                        throw new Exception("Cap nhat trang thai thuc nhap cho cac phieu con cua phieu tong hop that bai. Rollback du lieu");
                    }
                    this.beforeHisImpMests.AddRange(befores);
                    this.hisImpMestMedicines = new HisImpMestMedicineGet().GetByImpMestIds(listChild.Select(s => s.ID).ToList());
                    this.hisImpMestMaterials = new HisImpMestMaterialGet().GetByImpMestIds(listChild.Select(s => s.ID).ToList());
                }
            }
            else
            {
                this.hisImpMestMedicines = new HisImpMestMedicineGet().GetByImpMestId(this.recentHisImpMest.ID);
                this.hisImpMestMaterials = new HisImpMestMaterialGet().GetByImpMestId(this.recentHisImpMest.ID);
            }
        }

        private void PassResult(ref HIS_IMP_MEST resultData)
        {
            resultData = this.recentHisImpMest;
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

        /// <summary>
        /// Luu y: Ham rollback nay can dat "private".
        /// Neu ben ngoai sau khi "thuc nhap" thanh cong 
        /// va muon rollback thi can thuc hien nghiep vu "huy thuc nhap"
        /// </summary>
        private void Rollback()
        {
            this.bloodProcessor.Rollback();
            this.medicineProcessor.Rollback();
            this.materialProcessor.Rollback();
            if (IsNotNullOrEmpty(this.beforeHisImpMests))
            {
                if (!DAOWorker.HisImpMestDAO.UpdateList(this.beforeHisImpMests))
                {
                    LogSystem.Warn("Rollback ImpMest that bai. Kiem tra lai du lieu");
                }
                this.beforeHisImpMests = null;//tranh goi rollback 2 lan
            }
        }
    }
}
