using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisMedicine;
using MOS.MANAGER.HisMedicineBean;
using MOS.MANAGER.HisMedicineBean.Handle;
using MOS.MANAGER.HisMedicineBean.Update;
using MOS.MANAGER.HisMediStockMety;
using MOS.UTILITY;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisExpMest.Common.UnexportAndUnapprove
{
    partial class MedicineProcessor : BusinessBase
    {
        private HisExpMestMedicineUpdate hisExpMestMedicineUpdate;
        private HisMedicineBeanUnexport hisMedicineBeanUnexport;
        private HisMediStockMetyIncreaseBaseAmount hisMediStockMetyIncreaseBaseAmount;

        internal MedicineProcessor()
            : base()
        {
            this.Init();
        }

        internal MedicineProcessor(CommonParam paramUpdate)
            : base(paramUpdate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisExpMestMedicineUpdate = new HisExpMestMedicineUpdate(param);
            this.hisMedicineBeanUnexport = new HisMedicineBeanUnexport(param);
            this.hisMediStockMetyIncreaseBaseAmount = new HisMediStockMetyIncreaseBaseAmount(param);
        }

        internal bool Run(HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MEDICINE> hisExpMestMedicines)
        {
            try
            {
                this.ProcessExpMestMedicine(hisExpMestMedicines);
                this.ProcessMedicineBean(hisExpMestMedicines, expMest.MEDI_STOCK_ID);
                this.ProcessMediStockMety(expMest, hisExpMestMedicines);
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }

        /// <summary>
        /// Cap nhat his_medicine_bean
        /// </summary>
        /// <param name="hisExpMestMedicines"></param>
        private void ProcessMedicineBean(List<HIS_EXP_MEST_MEDICINE> hisExpMestMedicines, long mediStockId)
        {
            if (IsNotNullOrEmpty(hisExpMestMedicines))
            {
                if (!this.hisMedicineBeanUnexport.Run(hisExpMestMedicines, mediStockId))
                {
                    throw new Exception("Rollback du lieu");
                }
            }
        }

        /// <summary>
        /// Huy thong tin xuat vao exp_mest_medicine
        /// </summary>
        /// <param name="expMestMedicines"></param>
        /// <param name="loginName"></param>
        /// <param name="userName"></param>
        /// <param name="expTime"></param>
        private void ProcessExpMestMedicine(List<HIS_EXP_MEST_MEDICINE> expMestMedicines)
        {
            if (IsNotNullOrEmpty(expMestMedicines))
            {
                Mapper.CreateMap<HIS_EXP_MEST_MEDICINE, HIS_EXP_MEST_MEDICINE>();
                List<HIS_EXP_MEST_MEDICINE> befores = Mapper.Map<List<HIS_EXP_MEST_MEDICINE>>(expMestMedicines);

                expMestMedicines.ForEach(o =>
                {
                    o.IS_EXPORT = null;
                    o.EXP_LOGINNAME = null;
                    o.EXP_USERNAME = null;
                    o.EXP_TIME = null;
                });

                if (!this.hisExpMestMedicineUpdate.UpdateList(expMestMedicines, befores))
                {
                    throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                }
            }
        }

        private void ProcessMediStockMety(HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MEDICINE> expMestMedicines)
        {
            if (HisMediStockCFG.IS_USE_BASE_AMOUNT_CABINET && this.IsCheckCabinetAmount(expMest) && IsNotNullOrEmpty(expMestMedicines))
            {
                HisMediStockMetyFilterQuery stockMetyFilter = new HisMediStockMetyFilterQuery();
                stockMetyFilter.MEDI_STOCK_ID = expMest.MEDI_STOCK_ID;
                stockMetyFilter.MEDICINE_TYPE_IDs = expMestMedicines.Select(s => (s.TDL_MEDICINE_TYPE_ID ?? 0)).Distinct().ToList();
                List<HIS_MEDI_STOCK_METY> mestMetys = new HisMediStockMetyGet().Get(stockMetyFilter);

                if (!IsNotNullOrEmpty(mestMetys))
                {
                    return;
                }

                Dictionary<long, decimal> dicIncrease = new Dictionary<long, decimal>();
                List<string> messageError = new List<string>();
                foreach (var item in mestMetys)
                {
                    if (!item.ALERT_MAX_IN_STOCK.HasValue || !item.REAL_BASE_AMOUNT.HasValue)
                        continue;
                    var medicines = expMestMedicines.Where(o => o.TDL_MEDICINE_TYPE_ID == item.MEDICINE_TYPE_ID).ToList();
                    if (IsNotNullOrEmpty(medicines))
                    {
                        decimal expAmount = medicines.Sum(s => s.AMOUNT);
                        decimal baseAmount = item.REAL_BASE_AMOUNT.Value + expAmount;
                        if (baseAmount > item.ALERT_MAX_IN_STOCK.Value)
                        {
                            HIS_MEDICINE_TYPE mety = HisMedicineTypeCFG.DATA.FirstOrDefault(o => o.ID == item.MEDICINE_TYPE_ID);
                            string name = mety != null ? mety.MEDICINE_TYPE_NAME : "";
                            messageError.Add(String.Format("{0}({1})", name, item.ALERT_MAX_IN_STOCK.Value));
                            continue;
                        }

                        dicIncrease[item.ID] = expAmount;
                    }
                }

                if (messageError.Count > 0)
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisImpMest_ThuocVatTuSauVuotQuaCoSo, String.Join(";", messageError));
                    throw new Exception("Co thuoc vuot qua co so");
                }

                if (dicIncrease.Count > 0 && !this.hisMediStockMetyIncreaseBaseAmount.Run(dicIncrease))
                {
                    throw new Exception("hisMediStockMetyIncreaseBaseAmount. Ket thuc nghiep vu");
                }
            }
        }

        private bool IsCheckCabinetAmount(HIS_EXP_MEST exp)
        {
            V_HIS_MEDI_STOCK stock = HisMediStockCFG.DATA.FirstOrDefault(o => o.ID == exp.MEDI_STOCK_ID);

            if (stock == null)
                throw new Exception("Khong lay duoc V_HIS_MEDI_STOCK ID: " + exp.MEDI_STOCK_ID);
            if (exp.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DDT
                    || exp.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DM
                    || exp.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DPK
                    || exp.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DTT
                    || exp.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__PL
                    || exp.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__THPK
                    || stock.IS_CABINET != Constant.IS_TRUE)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        internal void Rollback()
        {
            this.hisExpMestMedicineUpdate.RollbackData();
            this.hisMedicineBeanUnexport.Rollback();
            this.hisMediStockMetyIncreaseBaseAmount.Rollback();
        }
    }
}
