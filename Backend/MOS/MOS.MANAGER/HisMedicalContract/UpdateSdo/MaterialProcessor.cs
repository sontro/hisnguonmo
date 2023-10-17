using AutoMapper;
using Inventec.Common.ObjectChecker;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisMediContractMaty;
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
    class MaterialProcessor : BusinessBase
    {

        private HisMediContractMatyCreate mediContractMatyCreate;
        private HisMediContractMatyUpdate mediContractMatyUpdate;

        internal MaterialProcessor(CommonParam param)
            : base(param)
        {
            this.mediContractMatyCreate = new HisMediContractMatyCreate(param);
            this.mediContractMatyUpdate = new HisMediContractMatyUpdate(param);
        }

        internal bool Run(HIS_MEDICAL_CONTRACT contract, List<HisMediContractMatySDO> materialSdos, List<HIS_MATERIAL_TYPE> materialTypes, List<HIS_MATERIAL> existMaterials, List<HIS_MEDI_CONTRACT_MATY> lstOld, ref List<string> sqls, ref List<string> changePriceLogs, ref List<HIS_SALE_PROFIT_CFG> saleProfitCfgs)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(materialSdos) || IsNotNullOrEmpty(lstOld))
                {
                    List<HIS_MEDI_CONTRACT_MATY> inserts = new List<HIS_MEDI_CONTRACT_MATY>();
                    List<HIS_MEDI_CONTRACT_MATY> updates = new List<HIS_MEDI_CONTRACT_MATY>();
                    List<HIS_MEDI_CONTRACT_MATY> befores = new List<HIS_MEDI_CONTRACT_MATY>();
                    List<HIS_MEDI_CONTRACT_MATY> deletes = new List<HIS_MEDI_CONTRACT_MATY>();

                    this.MakeMediContractMaty(contract, materialSdos, lstOld, materialTypes, existMaterials, ref inserts, ref updates, ref deletes, ref befores, ref sqls, ref changePriceLogs, ref saleProfitCfgs);

                    if (IsNotNullOrEmpty(updates) && !this.mediContractMatyUpdate.UpdateList(updates, befores))
                    {
                        throw new Exception("mediContractMatyUpdate. Ket thuc nghiep vu");
                    }

                    if (IsNotNullOrEmpty(inserts) && !this.mediContractMatyCreate.CreateList(inserts))
                    {
                        throw new Exception("mediContractMatyCreate. Ket thuc nghiep vu");
                    }

                    if (IsNotNullOrEmpty(deletes))
                    {
                        sqls.Add(DAOWorker.SqlDAO.AddInClause(deletes.Select(s => s.ID).ToList(), "DELETE HIS_MEDI_CONTRACT_MATY WHERE %IN_CLAUSE% ", "ID"));
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


        private void MakeMediContractMaty(HIS_MEDICAL_CONTRACT contract, List<HisMediContractMatySDO> materialSdos, List<HIS_MEDI_CONTRACT_MATY> lstOld, List<HIS_MATERIAL_TYPE> materialTypes, List<HIS_MATERIAL> existMaterials, ref List<HIS_MEDI_CONTRACT_MATY> inserts, ref List<HIS_MEDI_CONTRACT_MATY> updates, ref List<HIS_MEDI_CONTRACT_MATY> deletes, ref List<HIS_MEDI_CONTRACT_MATY> befores, ref List<string> sqls, ref List<string> changePriceLogs, ref List<HIS_SALE_PROFIT_CFG> saleProfits)
        {
            List<HIS_MEDI_CONTRACT_MATY> lstNotDelete = new List<HIS_MEDI_CONTRACT_MATY>();

            Mapper.CreateMap<HIS_MEDI_CONTRACT_MATY, HIS_MEDI_CONTRACT_MATY>();

            List<HIS_MATERIAL> saleMaterials = existMaterials != null ? existMaterials.Where(o => o.IS_SALE_EQUAL_IMP_PRICE != Constant.IS_TRUE).ToList() : null;

            if (IsNotNullOrEmpty(materialSdos))
            {
                string solo = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.SoLo);
                string gia = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.Gia);
                string loiNhuan = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.LoiNhuan);

                foreach (HisMediContractMatySDO sdo in materialSdos)
                {
                    HIS_MATERIAL_TYPE type = materialTypes.FirstOrDefault(o => o.ID == sdo.MaterialTypeId);

                    HIS_MEDI_CONTRACT_MATY exists = lstOld != null ? lstOld.FirstOrDefault(o => o.MATERIAL_TYPE_ID == sdo.MaterialTypeId
                        && o.BID_GROUP_CODE == sdo.BidGroupCode
                        && o.BID_NUMBER == sdo.BidNumber
                        && o.CONTRACT_PRICE == sdo.ContractPrice 
                        && o.BID_MATERIAL_TYPE_ID == sdo.BidMaterialTypeId) : null;
                    if (exists != null)
                    {
                        lstNotDelete.Add(exists);
                        HIS_MEDI_CONTRACT_MATY before = Mapper.Map<HIS_MEDI_CONTRACT_MATY>(exists);
                        exists.AMOUNT = sdo.Amount;
                        exists.BID_MATERIAL_TYPE_ID = sdo.BidMaterialTypeId;
                        exists.CONCENTRA = sdo.Concentra;
                        exists.DAY_LIFESPAN = sdo.DayLifespan;
                        exists.EXPIRED_DATE = sdo.ExpiredDate;
                        exists.IMP_PRICE = sdo.ImpPrice;
                        exists.IMP_VAT_RATIO = sdo.ImpVatRatio;
                        exists.INTERNAL_PRICE = type.INTERNAL_PRICE;
                        exists.MANUFACTURER_ID = sdo.ManufacturerId;
                        exists.MATERIAL_TYPE_ID = sdo.MaterialTypeId;
                        exists.MEDICAL_CONTRACT_ID = contract.ID;
                        exists.MONTH_LIFESPAN = sdo.MonthLifespan;
                        exists.NATIONAL_NAME = sdo.NationalName;
                        exists.HOUR_LIFESPAN = sdo.HourLifespan;
                        exists.CONTRACT_PRICE = sdo.ContractPrice;
                        exists.IMP_EXPIRED_DATE = sdo.ImpExpiredDate;
                        exists.NOTE = sdo.Note;

                        if (ValueChecker.IsPrimitiveDiff<HIS_MEDI_CONTRACT_MATY>(before, exists))
                        {
                            updates.Add(exists);
                            befores.Add(before);
                            decimal diffAmount = exists.AMOUNT - before.AMOUNT;
                            if (diffAmount != 0 && sdo.BidMaterialTypeId.HasValue)
                            {
                                sqls.Add(String.Format("UPDATE HIS_BID_MATERIAL_TYPE SET TDL_CONTRACT_AMOUNT = (NVL(TDL_CONTRACT_AMOUNT,0)+ {0}) WHERE ID = {1}", diffAmount, sdo.BidMaterialTypeId.Value));
                            }

                            //Tu dong cap nhat lai chinh sach gia ban trong truong hop thay doi gia hop dong
                            //Gia ban se tinh theo loi nhuan va gia hop dong moi
                            if (exists.CONTRACT_PRICE != before.CONTRACT_PRICE && IsNotNullOrEmpty(saleMaterials) && exists.CONTRACT_PRICE.HasValue)
                            {
                                List<HIS_MATERIAL> materials = saleMaterials.Where(o => o.MATERIAL_TYPE_ID == exists.MATERIAL_TYPE_ID).ToList();
                                if (saleProfits == null)
                                {
                                    HisSaleProfitCfgFilterQuery filter = new HisSaleProfitCfgFilterQuery();
                                    filter.IS_ACTIVE = Constant.IS_TRUE;
                                    saleProfits = new HisSaleProfitCfgGet().Get(filter);
                                }

                                if (IsNotNullOrEmpty(materials))
                                {
                                    foreach (HIS_MATERIAL m in materials)
                                    {
                                        decimal oldProfitRatio = m.PROFIT_RATIO.HasValue ? m.PROFIT_RATIO.Value : 0;
                                        decimal newProfitRatio = HisSaleProfitRatioUtil.GetProfitRatio(exists.CONTRACT_PRICE.Value, type.TDL_SERVICE_UNIT_ID, saleProfits, true);
                                        string packageNumber = m.PACKAGE_NUMBER != null ? m.PACKAGE_NUMBER : "";

                                        string sql = String.Format("UPDATE HIS_MATERIAL_PATY MP SET EXP_PRICE = {0} * (1 + {1}) / (1 + EXP_VAT_RATIO) WHERE MATERIAL_ID = {2}", exists.CONTRACT_PRICE, newProfitRatio, m.ID);
                                        sqls.Add(sql);

                                        //Luu lai nhat ky tac dong thay doi gia
                                        string changePriceLog = "";
                                        if (oldProfitRatio != newProfitRatio)
                                        {
                                            string updateMaterialSql = String.Format("UPDATE HIS_MATERIAL M SET PROFIT_RATIO = {0} WHERE ID = {1}", newProfitRatio, m.ID);
                                            sqls.Add(updateMaterialSql);

                                            changePriceLog = string.Format("{0} - {1} ({2}: {3}): {4}: {5} --> {6}, {7}: {8} --> {9}", type.MATERIAL_TYPE_CODE, type.MATERIAL_TYPE_NAME, solo, packageNumber, gia, before.CONTRACT_PRICE.Value, exists.CONTRACT_PRICE.Value, loiNhuan, oldProfitRatio * 100, newProfitRatio * 100);
                                        }
                                        else
                                        {
                                            changePriceLog = string.Format("{0} - {1} ({2}: {3}): {4}: {5} --> {6}", type.MATERIAL_TYPE_CODE, type.MATERIAL_TYPE_NAME, solo, packageNumber, gia, before.CONTRACT_PRICE.Value, exists.CONTRACT_PRICE.Value);
                                        }

                                        changePriceLogs.Add(changePriceLog);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        HIS_MEDI_CONTRACT_MATY detail = new HIS_MEDI_CONTRACT_MATY();
                        detail.AMOUNT = sdo.Amount;
                        detail.BID_MATERIAL_TYPE_ID = sdo.BidMaterialTypeId;
                        detail.CONCENTRA = sdo.Concentra;
                        detail.DAY_LIFESPAN = sdo.DayLifespan;
                        detail.EXPIRED_DATE = sdo.ExpiredDate;
                        detail.IMP_EXPIRED_DATE = sdo.ImpExpiredDate;
                        detail.IMP_PRICE = sdo.ImpPrice;
                        detail.IMP_VAT_RATIO = sdo.ImpVatRatio;
                        detail.INTERNAL_PRICE = type.INTERNAL_PRICE;
                        detail.MANUFACTURER_ID = sdo.ManufacturerId;
                        detail.MATERIAL_TYPE_ID = sdo.MaterialTypeId;
                        detail.MEDICAL_CONTRACT_ID = contract.ID;
                        detail.MONTH_LIFESPAN = sdo.MonthLifespan;
                        detail.NATIONAL_NAME = sdo.NationalName;
                        detail.HOUR_LIFESPAN = sdo.HourLifespan;
                        detail.CONTRACT_PRICE = sdo.ContractPrice;
                        detail.NOTE = sdo.Note;
                        detail.BID_NUMBER = sdo.BidNumber;
                        detail.BID_GROUP_CODE = sdo.BidGroupCode;
                        inserts.Add(detail);

                        if (sdo.BidMaterialTypeId.HasValue)
                        {
                            sqls.Add(String.Format("UPDATE HIS_BID_MATERIAL_TYPE SET TDL_CONTRACT_AMOUNT = (NVL(TDL_CONTRACT_AMOUNT,0)+ {0}) WHERE ID = {1}", sdo.Amount, sdo.BidMaterialTypeId.Value));
                        }
                    }
                }
            }
            if (IsNotNullOrEmpty(lstOld))
            {
                deletes = lstOld.Where(o => !lstNotDelete.Any(a => a.ID == o.ID)).ToList();
                foreach (var toDel in deletes)
                {
                    sqls.Add(String.Format("UPDATE HIS_BID_MATERIAL_TYPE SET TDL_CONTRACT_AMOUNT = (NVL(TDL_CONTRACT_AMOUNT,0)- {0}) WHERE ID = {1}", toDel.AMOUNT, toDel.BID_MATERIAL_TYPE_ID.Value));
                }
            }
        }

        internal void Rollback()
        {
            try
            {
                this.mediContractMatyCreate.RollbackData();
                this.mediContractMatyUpdate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

    }
}
