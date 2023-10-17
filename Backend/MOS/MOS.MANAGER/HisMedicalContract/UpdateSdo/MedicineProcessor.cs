using AutoMapper;
using Inventec.Common.ObjectChecker;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisMediContractMety;
using MOS.MANAGER.HisSaleProfitCfg;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisMedicalContract.UpdateSdo
{
    class MedicineProcessor : BusinessBase
    {

        private HisMediContractMetyCreate mediContractMetyCreate;
        private HisMediContractMetyUpdate mediContractMetyUpdate;

        internal MedicineProcessor(CommonParam param)
            : base(param)
        {
            this.mediContractMetyCreate = new HisMediContractMetyCreate(param);
            this.mediContractMetyUpdate = new HisMediContractMetyUpdate(param);
        }

        internal bool Run(HIS_MEDICAL_CONTRACT contract, List<HisMediContractMetySDO> medicineSdos, List<HIS_MEDICINE_TYPE> medicineTypes, List<HIS_MEDICINE> existMedicines, List<HIS_MEDI_CONTRACT_METY> lstOld, ref List<string> sqls, ref List<string> changePriceLogs, ref List<HIS_SALE_PROFIT_CFG> saleProfitCfgs)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(medicineSdos) || IsNotNullOrEmpty(lstOld))
                {
                    List<HIS_MEDI_CONTRACT_METY> inserts = new List<HIS_MEDI_CONTRACT_METY>();
                    List<HIS_MEDI_CONTRACT_METY> updates = new List<HIS_MEDI_CONTRACT_METY>();
                    List<HIS_MEDI_CONTRACT_METY> befores = new List<HIS_MEDI_CONTRACT_METY>();
                    List<HIS_MEDI_CONTRACT_METY> deletes = new List<HIS_MEDI_CONTRACT_METY>();
                    this.MakeMediContractMety(contract, medicineSdos, lstOld, medicineTypes, existMedicines, ref inserts, ref updates, ref deletes, ref befores, ref sqls, ref changePriceLogs, ref saleProfitCfgs);

                    if (IsNotNullOrEmpty(updates) && !this.mediContractMetyUpdate.UpdateList(updates, befores))
                    {
                        throw new Exception("mediContractMetyUpdate. Ket thuc nghiep vu");
                    }

                    if (IsNotNullOrEmpty(inserts) && !this.mediContractMetyCreate.CreateList(inserts))
                    {
                        throw new Exception("mediContractMetyCreate. Ket thuc nghiep vu");
                    }

                    if (IsNotNullOrEmpty(deletes))
                    {
                        sqls.Add(DAOWorker.SqlDAO.AddInClause(deletes.Select(s => s.ID).ToList(), "DELETE HIS_MEDI_CONTRACT_METY WHERE %IN_CLAUSE% ", "ID"));
                    }

                }
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
                param.HasException = true;
            }
            return result;
        }


        private void MakeMediContractMety(HIS_MEDICAL_CONTRACT contract, List<HisMediContractMetySDO> medicineSdos, List<HIS_MEDI_CONTRACT_METY> lstOld, List<HIS_MEDICINE_TYPE> medicineTypes, List<HIS_MEDICINE> existMedicines, ref List<HIS_MEDI_CONTRACT_METY> inserts, ref List<HIS_MEDI_CONTRACT_METY> updates, ref List<HIS_MEDI_CONTRACT_METY> deletes, ref List<HIS_MEDI_CONTRACT_METY> befores, ref List<string> sqls, ref List<string> changePriceLogs, ref List<HIS_SALE_PROFIT_CFG> saleProfits)
        {
            List<HIS_MEDI_CONTRACT_METY> lstNotDelete = new List<HIS_MEDI_CONTRACT_METY>();

            Mapper.CreateMap<HIS_MEDI_CONTRACT_METY, HIS_MEDI_CONTRACT_METY>();

            List<HIS_MEDICINE> saleMedicines = existMedicines != null ? existMedicines.Where(o => o.IS_SALE_EQUAL_IMP_PRICE != Constant.IS_TRUE).ToList() : null;

            if (IsNotNullOrEmpty(medicineSdos))
            {
                string solo = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.SoLo);
                string gia = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.Gia);
                string loiNhuan = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.LoiNhuan);

                foreach (HisMediContractMetySDO sdo in medicineSdos)
                {
                    HIS_MEDICINE_TYPE type = medicineTypes.FirstOrDefault(o => o.ID == sdo.MedicineTypeId);

                    HIS_MEDI_CONTRACT_METY exists = lstOld != null ? lstOld.FirstOrDefault(o => o.MEDICINE_TYPE_ID == sdo.MedicineTypeId
                        && o.BID_GROUP_CODE == sdo.BidGroupCode
                        && o.BID_NUMBER == sdo.BidNumber
                        && o.CONTRACT_PRICE == sdo.ContractPrice 
                        && o.BID_MEDICINE_TYPE_ID == sdo.BidMedicineTypeId) : null;
                    if (exists != null)
                    {
                        lstNotDelete.Add(exists);
                        HIS_MEDI_CONTRACT_METY before = Mapper.Map<HIS_MEDI_CONTRACT_METY>(exists);
                        exists.AMOUNT = sdo.Amount;
                        exists.BID_MEDICINE_TYPE_ID = sdo.BidMedicineTypeId;
                        exists.CONCENTRA = sdo.Concentra;
                        exists.DAY_LIFESPAN = sdo.DayLifespan;
                        exists.EXPIRED_DATE = sdo.ExpiredDate;
                        exists.IMP_PRICE = sdo.ImpPrice;
                        exists.IMP_VAT_RATIO = sdo.ImpVatRatio;
                        exists.INTERNAL_PRICE = type.INTERNAL_PRICE;
                        exists.MANUFACTURER_ID = sdo.ManufacturerId;
                        exists.MEDICINE_TYPE_ID = sdo.MedicineTypeId;
                        exists.MEDICAL_CONTRACT_ID = contract.ID;
                        exists.MONTH_LIFESPAN = sdo.MonthLifespan;
                        exists.NATIONAL_NAME = sdo.NationalName;
                        exists.HOUR_LIFESPAN = sdo.HourLifespan;
                        exists.CONTRACT_PRICE = sdo.ContractPrice;
                        exists.MEDICINE_REGISTER_NUMBER = sdo.MedicineRegisterNumber;
                        exists.IMP_EXPIRED_DATE = sdo.ImpExpiredDate;
                        exists.NOTE = sdo.Note;

                        if (ValueChecker.IsPrimitiveDiff<HIS_MEDI_CONTRACT_METY>(before, exists))
                        {
                            updates.Add(exists);
                            befores.Add(before);
                            decimal diffAmount = exists.AMOUNT - before.AMOUNT;

                            if (diffAmount != 0 && sdo.BidMedicineTypeId.HasValue)
                            {
                                sqls.Add(String.Format("UPDATE HIS_BID_MEDICINE_TYPE SET TDL_CONTRACT_AMOUNT = (NVL(TDL_CONTRACT_AMOUNT,0)+ {0}) WHERE ID = {1}", diffAmount, sdo.BidMedicineTypeId.Value));
                            }

                            //Tu dong cap nhat lai chinh sach gia ban trong truong hop thay doi gia hop dong
                            //Gia ban se tinh theo loi nhuan va gia hop dong moi
                            if (exists.CONTRACT_PRICE != before.CONTRACT_PRICE && IsNotNullOrEmpty(saleMedicines) && exists.CONTRACT_PRICE.HasValue)
                            {
                                List<HIS_MEDICINE> medicines = saleMedicines.Where(o => o.MEDICINE_TYPE_ID == exists.MEDICINE_TYPE_ID).ToList();
                                if (saleProfits == null)
                                {
                                    HisSaleProfitCfgFilterQuery filter = new HisSaleProfitCfgFilterQuery();
                                    filter.IS_ACTIVE = Constant.IS_TRUE;
                                    saleProfits = new HisSaleProfitCfgGet().Get(filter);
                                }

                                if (IsNotNullOrEmpty(medicines))
                                {
                                    foreach (HIS_MEDICINE m in medicines)
                                    {
                                        decimal oldProfitRatio = m.PROFIT_RATIO.HasValue ? m.PROFIT_RATIO.Value : 0;
                                        decimal newProfitRatio = HisSaleProfitRatioUtil.GetProfitRatio(exists.CONTRACT_PRICE.Value, type.TDL_SERVICE_UNIT_ID, saleProfits, true);
                                        string packageNumber = m.PACKAGE_NUMBER != null ? m.PACKAGE_NUMBER : "";

                                        string sql = String.Format("UPDATE HIS_MEDICINE_PATY MP SET EXP_PRICE = {0} * (1 + {1}) / (1 + EXP_VAT_RATIO) WHERE MEDICINE_ID = {2}", exists.CONTRACT_PRICE, newProfitRatio, m.ID);
                                        sqls.Add(sql);

                                        //Luu lai nhat ky tac dong thay doi gia
                                        string changePriceLog = "";
                                        if (oldProfitRatio != newProfitRatio)
                                        {
                                            string updateMedicineSql = String.Format("UPDATE HIS_MEDICINE M SET PROFIT_RATIO = {0} WHERE ID = {1}", newProfitRatio, m.ID);
                                            sqls.Add(updateMedicineSql);

                                            changePriceLog = string.Format("{0} - {1} ({2}: {3}): {4}: {5} --> {6}, {7}: {8} --> {9}", type.MEDICINE_TYPE_CODE, type.MEDICINE_TYPE_NAME, solo, packageNumber, gia, before.CONTRACT_PRICE.Value, exists.CONTRACT_PRICE.Value, loiNhuan, oldProfitRatio * 100, newProfitRatio * 100);
                                        }
                                        else
                                        {
                                            changePriceLog = string.Format("{0} - {1} ({2}: {3}): {4}: {5} --> {6}", type.MEDICINE_TYPE_CODE, type.MEDICINE_TYPE_NAME, solo, packageNumber, gia, before.CONTRACT_PRICE.Value, exists.CONTRACT_PRICE.Value);
                                        }

                                        changePriceLogs.Add(changePriceLog);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        HIS_MEDI_CONTRACT_METY detail = new HIS_MEDI_CONTRACT_METY();
                        detail.AMOUNT = sdo.Amount;
                        detail.BID_MEDICINE_TYPE_ID = sdo.BidMedicineTypeId;
                        detail.CONCENTRA = sdo.Concentra;
                        detail.DAY_LIFESPAN = sdo.DayLifespan;
                        detail.EXPIRED_DATE = sdo.ExpiredDate;
                        detail.IMP_EXPIRED_DATE = sdo.ImpExpiredDate;
                        detail.IMP_PRICE = sdo.ImpPrice;
                        detail.IMP_VAT_RATIO = sdo.ImpVatRatio;
                        detail.INTERNAL_PRICE = type.INTERNAL_PRICE;
                        detail.MANUFACTURER_ID = sdo.ManufacturerId;
                        detail.MEDICINE_TYPE_ID = sdo.MedicineTypeId;
                        detail.MEDICAL_CONTRACT_ID = contract.ID;
                        detail.MONTH_LIFESPAN = sdo.MonthLifespan;
                        detail.NATIONAL_NAME = sdo.NationalName;
                        detail.HOUR_LIFESPAN = sdo.HourLifespan;
                        detail.CONTRACT_PRICE = sdo.ContractPrice;
                        detail.MEDICINE_REGISTER_NUMBER = sdo.MedicineRegisterNumber;
                        detail.NOTE = sdo.Note;
                        detail.BID_NUMBER = sdo.BidNumber;
                        detail.BID_GROUP_CODE = sdo.BidGroupCode;
                        inserts.Add(detail);

                        if (sdo.BidMedicineTypeId.HasValue)
                        {
                            sqls.Add(String.Format("UPDATE HIS_BID_MEDICINE_TYPE SET TDL_CONTRACT_AMOUNT = (NVL(TDL_CONTRACT_AMOUNT,0)+ {0}) WHERE ID = {1}", sdo.Amount, sdo.BidMedicineTypeId.Value));
                        }
                    }
                }
            }
            if (IsNotNullOrEmpty(lstOld))
            {
                deletes = lstOld.Where(o => !lstNotDelete.Any(a => a.ID == o.ID)).ToList();
                foreach (var toDel in deletes)
                {
                    sqls.Add(String.Format("UPDATE HIS_BID_MEDICINE_TYPE SET TDL_CONTRACT_AMOUNT = (NVL(TDL_CONTRACT_AMOUNT,0)- {0}) WHERE ID = {1}", toDel.AMOUNT, toDel.BID_MEDICINE_TYPE_ID.Value)); ;
                }
            }
        }

        internal void Rollback()
        {
            try
            {
                this.mediContractMetyCreate.RollbackData();
                this.mediContractMetyUpdate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

    }
}
