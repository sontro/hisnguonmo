using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisExpMest.Common;
using MOS.MANAGER.HisExpMest.Common.Auto;
using MOS.MANAGER.HisVitaminA;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.VitaminA.Create
{
    class HisExpMestVitaminACreate : BusinessBase
    {
        private HIS_EXP_MEST recentExpMest = null;
        private List<HIS_EXP_MEST_MEDICINE> expMestMedicines = null;
        private HisExpMestResultSDO recentResultSDO;

        private ExpMestProcessor expMestProcessor;
        private MedicineProcessor medicineProcessor;
        private VitaminAProcessor vitaminAProcessor;
        private HisExpMestAutoProcess hisExpMestAutoProcess;

        internal HisExpMestVitaminACreate()
            : base()
        {
            this.Init();
        }

        internal HisExpMestVitaminACreate(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.expMestProcessor = new ExpMestProcessor(param);
            this.medicineProcessor = new MedicineProcessor(param);
            this.vitaminAProcessor = new VitaminAProcessor(param);
            this.hisExpMestAutoProcess = new HisExpMestAutoProcess(param);
        }

        internal bool Run(HisExpMestVitaminASDO data, ref HisExpMestResultSDO resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                WorkPlaceSDO workPlace = null;
                List<HIS_VITAMIN_A> vitaminAs = new List<HIS_VITAMIN_A>();
                HisExpMestVitaminACheck checker = new HisExpMestVitaminACheck(param);
                HisVitaminACheck vitaminAChecker = new HisVitaminACheck(param);
                valid = valid && checker.ValidData(data);
                valid = valid && this.HasWorkPlaceInfo(data.ReqRoomId, ref workPlace);
                valid = valid && vitaminAChecker.VerifyIds(data.VitaminAIds, vitaminAs);
                valid = valid && vitaminAChecker.HasMedicineTypeId(vitaminAs);
                valid = valid && vitaminAChecker.HasNoExpMestId(vitaminAs);
                valid = valid && vitaminAChecker.HasExecuteTime(vitaminAs);
                if (valid)
                {
                    List<String> sqls = new List<string>();

                    if (!this.expMestProcessor.Run(data, ref this.recentExpMest))
                    {
                        throw new Exception("expMestProcessor. Ket thuc nghiep vu");
                    }

                    if (!this.medicineProcessor.Run(this.recentExpMest, vitaminAs, ref expMestMedicines, ref sqls))
                    {
                        throw new Exception("medicineProcessor. Ket thuc nghiep vu");
                    }

                    if (!this.vitaminAProcessor.Run(this.recentExpMest, vitaminAs))
                    {
                        throw new Exception("vitaminAProcessor. Ket thuc nghiep vu");
                    }

                    //Set TDL_TOTAL_PRICE
                    this.ProcessTdlTotalPrice(this.recentExpMest, expMestMedicines, ref sqls);

                    ///execute sql se thuc hien cuoi cung de de dang trong viec rollback
                    if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("Rollback du lieu");
                    }

                    this.ProcessAuto();

                    this.PassResult( expMestMedicines, ref resultData);

                    new EventLogGenerator(EventLog.Enum.HisExpMest_TaoPhieuXuat).ExpMestCode(this.recentExpMest.EXP_MEST_CODE).Run();

                    result = true;
                }

            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                this.Rollback();
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void ProcessTdlTotalPrice(HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MEDICINE> expMestMedicines, ref List<string> sqls)
        {
            try
            {
                decimal? totalPrice = null;
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
                    string updateSql = string.Format("UPDATE HIS_EXP_MEST SET TDL_TOTAL_PRICE = {0} WHERE ID = {1}", totalPrice.Value, expMest.ID);
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
        private void ProcessAuto()
        {
            try
            {
                this.hisExpMestAutoProcess.Run(this.recentExpMest, ref this.recentResultSDO);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void PassResult(List<HIS_EXP_MEST_MEDICINE> expMedicines, ref HisExpMestResultSDO resultData)
        {
            if (this.recentResultSDO != null)
            {
                resultData = this.recentResultSDO;
            }
            else
            {
                resultData = new HisExpMestResultSDO();
                resultData.ExpMest = this.recentExpMest;
                resultData.ExpMedicines = expMedicines;
            }
        }

        private void Rollback()
        {
            try
            {
                this.vitaminAProcessor.RollbackData();
                this.medicineProcessor.RollbackData();
                this.expMestProcessor.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

    }
}
