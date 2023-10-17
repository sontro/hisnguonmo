using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Common.ObjectChecker;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisBidBloodType;
using MOS.MANAGER.HisBidMaterialType;
using MOS.MANAGER.HisBidMedicineType;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisBid
{
    partial class HisBidUpdate : BusinessBase
    {
        private List<HIS_BID> beforeUpdates = new List<HIS_BID>();
        private HisBidMaterialTypeCreate hisBidMaterialTypeCreate;
        private HisBidMaterialTypeUpdate hisBidMaterialTypeUpdate;
        private HisBidMedicineTypeCreate hisBidMedicineTypeCreate;
        private HisBidMedicineTypeUpdate hisBidMedicineTypeUpdate;
        private HisBidBloodTypeCreate hisBidBloodTypeCreate;
        private HisBidBloodTypeUpdate hisBidBloodTypeUpdate;

        internal HisBidUpdate()
            : base()
        {
            this.Init();
        }

        internal HisBidUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisBidBloodTypeCreate = new HisBidBloodTypeCreate(param);
            this.hisBidBloodTypeUpdate = new HisBidBloodTypeUpdate(param);
            this.hisBidMaterialTypeCreate = new HisBidMaterialTypeCreate(param);
            this.hisBidMaterialTypeUpdate = new HisBidMaterialTypeUpdate(param);
            this.hisBidMedicineTypeCreate = new HisBidMedicineTypeCreate(param);
            this.hisBidMedicineTypeUpdate = new HisBidMedicineTypeUpdate(param);
        }

        private List<HIS_SUPPLIER> hisSuppliers = new List<HIS_SUPPLIER>();
        private List<HIS_MANUFACTURER> hisManufactures = new List<HIS_MANUFACTURER>();

        internal bool Update(HIS_BID data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBidCheck checker = new HisBidCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_BID raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    List<string> logs = new List<string>();
                    List<HIS_BID_MATERIAL_TYPE> hisBidMaties = data.HIS_BID_MATERIAL_TYPE != null ? data.HIS_BID_MATERIAL_TYPE.ToList() : null;
                    List<HIS_BID_MEDICINE_TYPE> hisBidMeties = data.HIS_BID_MEDICINE_TYPE != null ? data.HIS_BID_MEDICINE_TYPE.ToList() : null;
                    List<HIS_BID_BLOOD_TYPE> hisBidBlties = data.HIS_BID_BLOOD_TYPE != null ? data.HIS_BID_BLOOD_TYPE.ToList() : null;

                    data.HIS_BID_MATERIAL_TYPE = null;//set null truoc khi goi update de tranh loi tang DAO
                    data.HIS_BID_MEDICINE_TYPE = null;//set null truoc khi goi update de tranh loi tang DAO
                    data.HIS_BID_BLOOD_TYPE = null;//set null truoc khi goi update de tranh loi tang DAO
                    result = DAOWorker.HisBidDAO.Update(data);

                    if (result)
                    {
                        HisBidLogUtil.GenerateLogBid(data, raw, ref logs);
                        this.beforeUpdates.Add(raw);
                        List<string> sqls = new List<string>();
                        this.ProcessBidMaterialType(data.ID, hisBidMaties, ref sqls, ref logs);
                        this.ProcessBidMedicineType(data.ID, hisBidMeties, ref sqls, ref logs);
                        this.ProcessBidBloodType(data.ID, hisBidBlties, ref sqls, ref logs);
                        if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                        {
                            throw new Exception("sql error: " + sqls.ToString());
                        }
                        var bid = new HIS_BID();
                        AutoMapper.Mapper.CreateMap<HIS_BID, HIS_BID>();
                        bid = AutoMapper.Mapper.Map<HIS_BID>(data);
                        bid.HIS_BID_MATERIAL_TYPE = hisBidMaties;
                        bid.HIS_BID_MEDICINE_TYPE = hisBidMeties;
                        bid.HIS_BID_BLOOD_TYPE = hisBidBlties;
                        this.ThreadUpdateTDL(bid);
                        result = true;
                        this.ProcessEventLog(raw, logs);
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                this.RollbackData();
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void ThreadUpdateTDL(HIS_BID data)
        {
            System.Threading.Thread material = new System.Threading.Thread(ProcessUpdateTDLMaterial);
            System.Threading.Thread medicine = new System.Threading.Thread(ProcessUpdateTDLMedicine);
            System.Threading.Thread blood = new System.Threading.Thread(ProcessUpdateTDLBlood);
            try
            {
                material.Start(data);
                medicine.Start(data);
                blood.Start(data);
            }
            catch (Exception ex)
            {
                material.Abort(data);
                medicine.Abort(data);
                blood.Abort(data);
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void ProcessUpdateTDLBlood(object obj)
        {
            try
            {
                if (obj != null && obj.GetType() == typeof(HIS_BID))
                {
                    var bid = (HIS_BID)obj;
                    List<HIS_BID_BLOOD_TYPE> hisBidBlties = bid.HIS_BID_BLOOD_TYPE != null ? bid.HIS_BID_BLOOD_TYPE.ToList() : null;

                    if (hisBidBlties != null && hisBidBlties.Count > 0)
                    {
                        HisBlood.HisBloodFilterQuery filter = new HisBlood.HisBloodFilterQuery();
                        filter.BID_ID = bid.ID;

                        var blood = new HisBlood.HisBloodGet().Get(filter);
                        if (blood != null && blood.Count > 0)
                        {
                            foreach (var item in blood)
                            {
                                var bl = hisBidBlties.FirstOrDefault(o => o.BLOOD_TYPE_ID == item.BLOOD_TYPE_ID);
                                if (bl != null)
                                {
                                    item.BID_NUM_ORDER = bl.BID_NUM_ORDER;
                                }
                            }

                            if (!new HisBlood.HisBloodUpdate().UpdateList(blood))
                            {
                                Inventec.Common.Logging.LogSystem.Error("UpdateList du lieu HIS_BLOOD that bai.");
                                Inventec.Common.Logging.LogSystem.Error(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => blood), blood));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void ProcessUpdateTDLMedicine(object obj)
        {
            try
            {
                if (obj != null && obj.GetType() == typeof(HIS_BID))
                {
                    var bid = (HIS_BID)obj;
                    List<HIS_BID_MEDICINE_TYPE> hisBidMeties = bid.HIS_BID_MEDICINE_TYPE != null ? bid.HIS_BID_MEDICINE_TYPE.ToList() : null;

                    if (hisBidMeties != null && hisBidMeties.Count > 0)
                    {
                        HisMedicine.HisMedicineFilterQuery filter = new HisMedicine.HisMedicineFilterQuery();
                        filter.BID_ID = bid.ID;

                        var medicine = new HisMedicine.HisMedicineGet().Get(filter);
                        if (medicine != null && medicine.Count > 0)
                        {
                            foreach (var item in medicine)
                            {
                                var me = hisBidMeties.FirstOrDefault(o => o.MEDICINE_TYPE_ID == item.MEDICINE_TYPE_ID && o.SUPPLIER_ID == item.SUPPLIER_ID && o.BID_GROUP_CODE == item.TDL_BID_GROUP_CODE);
                                if (me != null)
                                {
                                    item.TDL_BID_GROUP_CODE = me.BID_GROUP_CODE;
                                    item.TDL_BID_NUM_ORDER = me.BID_NUM_ORDER;
                                    item.TDL_BID_NUMBER = bid.BID_NUMBER;
                                    item.TDL_BID_PACKAGE_CODE = me.BID_PACKAGE_CODE;
                                    item.TDL_BID_YEAR = bid.BID_YEAR;
                                }
                            }

                            if (!new HisMedicine.HisMedicineUpdate().UpdateList(medicine))
                            {
                                Inventec.Common.Logging.LogSystem.Error("UpdateList du lieu HIS_MEDICINE that bai.");
                                Inventec.Common.Logging.LogSystem.Error(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => medicine), medicine));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void ProcessUpdateTDLMaterial(object obj)
        {
            try
            {
                if (obj != null && obj.GetType() == typeof(HIS_BID))
                {
                    var bid = (HIS_BID)obj;
                    List<HIS_BID_MATERIAL_TYPE> hisBidMaties = bid.HIS_BID_MATERIAL_TYPE != null ? bid.HIS_BID_MATERIAL_TYPE.ToList() : null;

                    if (hisBidMaties != null && hisBidMaties.Count > 0)
                    {
                        HisMaterial.HisMaterialFilterQuery filter = new HisMaterial.HisMaterialFilterQuery();
                        filter.BID_ID = bid.ID;

                        var material = new HisMaterial.HisMaterialGet().Get(filter);
                        if (material != null && material.Count > 0)
                        {
                            foreach (var item in material)
                            {
                                var me = hisBidMaties.FirstOrDefault(o => o.MATERIAL_TYPE_ID == item.MATERIAL_TYPE_ID && o.SUPPLIER_ID == item.SUPPLIER_ID && o.BID_GROUP_CODE == item.TDL_BID_GROUP_CODE);
                                if (me != null)
                                {
                                    item.TDL_BID_GROUP_CODE = me.BID_GROUP_CODE;
                                    item.TDL_BID_NUM_ORDER = me.BID_NUM_ORDER;
                                    item.TDL_BID_NUMBER = bid.BID_NUMBER;
                                    item.TDL_BID_PACKAGE_CODE = me.BID_PACKAGE_CODE;
                                    item.TDL_BID_YEAR = bid.BID_YEAR;
                                }
                            }

                            if (!new HisMaterial.HisMaterialUpdate().UpdateList(material))
                            {
                                Inventec.Common.Logging.LogSystem.Error("UpdateList du lieu HIS_MATERIAL that bai.");
                                Inventec.Common.Logging.LogSystem.Error(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => material), material));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void ProcessBidMaterialType(long bidId, List<HIS_BID_MATERIAL_TYPE> hisBidMaties, ref List<string> sqls, ref List<string> logs)
        {
            List<HIS_BID_MATERIAL_TYPE> oldBidMaties = new HisBidMaterialTypeGet().GetByBidId(bidId);
            List<HIS_BID_MATERIAL_TYPE> updateList = new List<HIS_BID_MATERIAL_TYPE>();
            List<HIS_BID_MATERIAL_TYPE> createList = new List<HIS_BID_MATERIAL_TYPE>();
            List<HIS_BID_MATERIAL_TYPE> deleteList = new List<HIS_BID_MATERIAL_TYPE>();
            List<HIS_BID_MATERIAL_TYPE> beforeList = new List<HIS_BID_MATERIAL_TYPE>();
            List<HIS_BID_MATERIAL_TYPE> notDeleteList = new List<HIS_BID_MATERIAL_TYPE>();

            if (IsNotNullOrEmpty(hisBidMaties))
            {
                Mapper.CreateMap<HIS_BID_MATERIAL_TYPE, HIS_BID_MATERIAL_TYPE>();

                foreach (var bidMaties in hisBidMaties)
                {
                    HIS_BID_MATERIAL_TYPE old = oldBidMaties != null ? oldBidMaties.Where(o => o.MATERIAL_TYPE_ID == bidMaties.MATERIAL_TYPE_ID && o.SUPPLIER_ID == bidMaties.SUPPLIER_ID && o.BID_GROUP_CODE == bidMaties.BID_GROUP_CODE).FirstOrDefault() : null;
                    if (old != null)
                    {
                        HIS_BID_MATERIAL_TYPE before = Mapper.Map<HIS_BID_MATERIAL_TYPE>(old);
                        old.AMOUNT = bidMaties.AMOUNT;
                        old.BID_GROUP_CODE = bidMaties.BID_GROUP_CODE;
                        old.BID_NUM_ORDER = bidMaties.BID_NUM_ORDER;
                        old.BID_PACKAGE_CODE = bidMaties.BID_PACKAGE_CODE;
                        old.CONCENTRA = bidMaties.CONCENTRA;
                        old.EXPIRED_DATE = bidMaties.EXPIRED_DATE;
                        old.IMP_PRICE = bidMaties.IMP_PRICE;
                        old.IMP_VAT_RATIO = bidMaties.IMP_VAT_RATIO;
                        old.INTERNAL_PRICE = bidMaties.INTERNAL_PRICE;
                        old.MANUFACTURER_ID = bidMaties.MANUFACTURER_ID;
                        old.MATERIAL_TYPE_ID = bidMaties.MATERIAL_TYPE_ID;
                        old.NATIONAL_NAME = bidMaties.NATIONAL_NAME;
                        old.SUPPLIER_ID = bidMaties.SUPPLIER_ID;
                        old.DAY_LIFESPAN = bidMaties.DAY_LIFESPAN;
                        old.HOUR_LIFESPAN = bidMaties.HOUR_LIFESPAN;
                        old.MONTH_LIFESPAN = bidMaties.MONTH_LIFESPAN;
                        old.JOIN_BID_MATERIAL_TYPE_CODE = bidMaties.JOIN_BID_MATERIAL_TYPE_CODE;
                        old.BID_MATERIAL_TYPE_CODE = bidMaties.BID_MATERIAL_TYPE_CODE;
                        old.BID_MATERIAL_TYPE_NAME = bidMaties.BID_MATERIAL_TYPE_NAME;
                        old.NOTE = bidMaties.NOTE;
                        old.IMP_MORE_RATIO = bidMaties.IMP_MORE_RATIO;
                        old.ADJUST_AMOUNT = bidMaties.ADJUST_AMOUNT;

                        if (ValueChecker.IsPrimitiveDiff<HIS_BID_MATERIAL_TYPE>(before, old))
                        {
                            updateList.Add(old);
                            beforeList.Add(before);
                        }
                        notDeleteList.Add(old);
                    }
                    else
                    {
                        bidMaties.BID_ID = bidId;
                        createList.Add(bidMaties);
                    }
                }
            }
            deleteList = oldBidMaties != null ? oldBidMaties.Where(o => notDeleteList == null || !notDeleteList.Exists(e => e.ID == o.ID)).ToList() : null;

            if (IsNotNullOrEmpty(createList))
            {
                if (!this.hisBidMaterialTypeCreate.CreateList(createList))
                {
                    throw new Exception("Tao du lieu HIS_BID_MATERIAL_TYPE that bai. Ket thuc nghiep vu. ");
                }
            }

            if (IsNotNullOrEmpty(updateList))
            {
                if (!this.hisBidMaterialTypeUpdate.UpdateList(updateList, beforeList))
                {
                    throw new Exception("Sua du lieu HIS_BID_MATERIAL_TYPE that bai. Ket thuc nghiep vu. ");
                }
            }

            if (IsNotNullOrEmpty(deleteList))
            {
                string sql = DAOWorker.SqlDAO.AddInClause(deleteList.Select(s => s.ID).ToList(), "DELETE HIS_BID_MATERIAL_TYPE WHERE %IN_CLAUSE% ", "ID");
                sqls.Add(sql);
            }

            HisBidLogUtil.ProcessLogMaterial(createList, updateList, beforeList, deleteList, ref this.hisSuppliers, ref this.hisManufactures, ref logs);
        }

        private void ProcessBidMedicineType(long bidId, List<HIS_BID_MEDICINE_TYPE> hisBidMeties, ref List<string> sqls, ref  List<string> logs)
        {
            List<HIS_BID_MEDICINE_TYPE> oldBidMeties = new HisBidMedicineTypeGet().GetByBidId(bidId);
            List<HIS_BID_MEDICINE_TYPE> updateList = new List<HIS_BID_MEDICINE_TYPE>();
            List<HIS_BID_MEDICINE_TYPE> createList = new List<HIS_BID_MEDICINE_TYPE>();
            List<HIS_BID_MEDICINE_TYPE> deleteList = new List<HIS_BID_MEDICINE_TYPE>();
            List<HIS_BID_MEDICINE_TYPE> beforeList = new List<HIS_BID_MEDICINE_TYPE>();
            List<HIS_BID_MEDICINE_TYPE> notDeleteList = new List<HIS_BID_MEDICINE_TYPE>();
            if (IsNotNullOrEmpty(hisBidMeties))
            {
                Mapper.CreateMap<HIS_BID_MEDICINE_TYPE, HIS_BID_MEDICINE_TYPE>();
                foreach (var bidMeties in hisBidMeties)
                {
                    HIS_BID_MEDICINE_TYPE old = oldBidMeties != null ? oldBidMeties.Where(o => o.MEDICINE_TYPE_ID == bidMeties.MEDICINE_TYPE_ID && o.SUPPLIER_ID == bidMeties.SUPPLIER_ID && o.BID_GROUP_CODE == bidMeties.BID_GROUP_CODE).FirstOrDefault() : null;
                    if (old != null)
                    {
                        HIS_BID_MEDICINE_TYPE before = Mapper.Map<HIS_BID_MEDICINE_TYPE>(old);
                        old.AMOUNT = bidMeties.AMOUNT;
                        old.BID_GROUP_CODE = bidMeties.BID_GROUP_CODE;
                        old.BID_NUM_ORDER = bidMeties.BID_NUM_ORDER;
                        old.BID_PACKAGE_CODE = bidMeties.BID_PACKAGE_CODE;
                        old.CONCENTRA = bidMeties.CONCENTRA;
                        old.EXPIRED_DATE = bidMeties.EXPIRED_DATE;
                        old.IMP_PRICE = bidMeties.IMP_PRICE;
                        old.IMP_VAT_RATIO = bidMeties.IMP_VAT_RATIO;
                        old.INTERNAL_PRICE = bidMeties.INTERNAL_PRICE;
                        old.MANUFACTURER_ID = bidMeties.MANUFACTURER_ID;
                        old.MEDICINE_TYPE_ID = bidMeties.MEDICINE_TYPE_ID;
                        old.NATIONAL_NAME = bidMeties.NATIONAL_NAME;
                        old.SUPPLIER_ID = bidMeties.SUPPLIER_ID;
                        old.MEDICINE_REGISTER_NUMBER = bidMeties.MEDICINE_REGISTER_NUMBER;
                        old.DAY_LIFESPAN = bidMeties.DAY_LIFESPAN;
                        old.HOUR_LIFESPAN = bidMeties.HOUR_LIFESPAN;
                        old.MONTH_LIFESPAN = bidMeties.MONTH_LIFESPAN;
                        old.HEIN_SERVICE_BHYT_NAME = bidMeties.HEIN_SERVICE_BHYT_NAME;
                        old.PACKING_TYPE_NAME = bidMeties.PACKING_TYPE_NAME;
                        old.ACTIVE_INGR_BHYT_NAME = bidMeties.ACTIVE_INGR_BHYT_NAME;
                        old.MEDICINE_USE_FORM_ID = bidMeties.MEDICINE_USE_FORM_ID;
                        old.DOSAGE_FORM = bidMeties.DOSAGE_FORM;
                        old.NOTE = bidMeties.NOTE;
                        old.IMP_MORE_RATIO = bidMeties.IMP_MORE_RATIO;
                        old.ADJUST_AMOUNT = bidMeties.ADJUST_AMOUNT;

                        if (ValueChecker.IsPrimitiveDiff<HIS_BID_MEDICINE_TYPE>(before, old))
                        {
                            updateList.Add(old);
                            beforeList.Add(before);
                        }
                        notDeleteList.Add(old);
                    }
                    else
                    {
                        bidMeties.BID_ID = bidId;
                        createList.Add(bidMeties);
                    }
                }
            }
            deleteList = oldBidMeties != null ? oldBidMeties.Where(o => notDeleteList == null || !notDeleteList.Exists(e => e.ID == o.ID)).ToList() : null;

            if (IsNotNullOrEmpty(createList))
            {
                if (!this.hisBidMedicineTypeCreate.CreateList(createList))
                {
                    throw new Exception("Tao du lieu HIS_BID_MEDICINE_TYPE that bai. Ket thuc nghiep vu. ");
                }
            }

            if (IsNotNullOrEmpty(updateList))
            {
                if (!this.hisBidMedicineTypeUpdate.UpdateList(updateList))
                {
                    throw new Exception("Sua du lieu HIS_BID_MEDICINE_TYPE that bai. Ket thuc nghiep vu. ");
                }
            }

            if (IsNotNullOrEmpty(deleteList))
            {
                string sql = DAOWorker.SqlDAO.AddInClause(deleteList.Select(s => s.ID).ToList(), "DELETE HIS_BID_MEDICINE_TYPE WHERE %IN_CLAUSE% ", "ID");
                sqls.Add(sql);
            }
            HisBidLogUtil.ProcessLogMedicine(createList, updateList, beforeList, deleteList, ref this.hisSuppliers, ref this.hisManufactures, ref logs);
        }

        private void ProcessBidBloodType(long bidId, List<HIS_BID_BLOOD_TYPE> hisBidBlties, ref List<string> sqls, ref  List<string> logs)
        {
            List<HIS_BID_BLOOD_TYPE> oldBidBlties = new HisBidBloodTypeGet().GetByBidId(bidId);
            List<HIS_BID_BLOOD_TYPE> updateList = new List<HIS_BID_BLOOD_TYPE>();
            List<HIS_BID_BLOOD_TYPE> createList = new List<HIS_BID_BLOOD_TYPE>();
            List<HIS_BID_BLOOD_TYPE> deleteList = new List<HIS_BID_BLOOD_TYPE>();
            List<HIS_BID_BLOOD_TYPE> beforeList = new List<HIS_BID_BLOOD_TYPE>();
            List<HIS_BID_BLOOD_TYPE> notDeleteList = new List<HIS_BID_BLOOD_TYPE>();
            if (IsNotNullOrEmpty(hisBidBlties))
            {
                Mapper.CreateMap<HIS_BID_BLOOD_TYPE, HIS_BID_BLOOD_TYPE>();
                foreach (var bidBlties in hisBidBlties)
                {
                    HIS_BID_BLOOD_TYPE old = oldBidBlties != null ? oldBidBlties.Where(o => o.BLOOD_TYPE_ID == bidBlties.BLOOD_TYPE_ID && o.SUPPLIER_ID == bidBlties.SUPPLIER_ID).FirstOrDefault() : null;
                    if (old != null)
                    {
                        HIS_BID_BLOOD_TYPE before = Mapper.Map<HIS_BID_BLOOD_TYPE>(old);
                        old.AMOUNT = bidBlties.AMOUNT;
                        old.BID_NUM_ORDER = bidBlties.BID_NUM_ORDER;
                        old.IMP_PRICE = bidBlties.IMP_PRICE;
                        old.IMP_VAT_RATIO = bidBlties.IMP_VAT_RATIO;
                        old.INTERNAL_PRICE = bidBlties.INTERNAL_PRICE;
                        old.BLOOD_TYPE_ID = bidBlties.BLOOD_TYPE_ID;
                        old.SUPPLIER_ID = bidBlties.SUPPLIER_ID;
                        if (ValueChecker.IsPrimitiveDiff<HIS_BID_BLOOD_TYPE>(before, old))
                        {
                            updateList.Add(old);
                            beforeList.Add(before);
                        }
                        notDeleteList.Add(old);
                    }
                    else
                    {
                        bidBlties.BID_ID = bidId;
                        createList.Add(bidBlties);
                    }
                }
            }
            deleteList = oldBidBlties != null ? oldBidBlties.Where(o => notDeleteList == null || !notDeleteList.Exists(e => e.ID == o.ID)).ToList() : null;

            if (IsNotNullOrEmpty(createList))
            {
                if (!this.hisBidBloodTypeCreate.CreateList(createList))
                {
                    throw new Exception("Tao du lieu HIS_BID_BLOOD_TYPE that bai. Ket thuc nghiep vu. ");
                }
            }

            if (IsNotNullOrEmpty(updateList))
            {
                if (!this.hisBidBloodTypeUpdate.UpdateList(updateList))
                {
                    throw new Exception("Sua du lieu HIS_BID_BLOOD_TYPE that bai. Ket thuc nghiep vu. ");
                }
            }

            if (IsNotNullOrEmpty(deleteList))
            {
                string sql = DAOWorker.SqlDAO.AddInClause(deleteList.Select(s => s.ID).ToList(), "DELETE HIS_BID_BLOOD_TYPE WHERE %IN_CLAUSE% ", "ID");
                sqls.Add(sql);
            }
            HisBidLogUtil.ProcessLogBlood(createList, updateList, beforeList, deleteList, ref this.hisSuppliers, ref logs);
        }

        internal bool UpdateList(List<HIS_BID> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBidCheck checker = new HisBidCheck(param);
                List<HIS_BID> listRaw = new List<HIS_BID>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    this.beforeUpdates.AddRange(listRaw);
                    result = DAOWorker.HisBidDAO.UpdateList(listData);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void ProcessEventLog(HIS_BID bid, List<string> logs)
        {
            try
            {
                new EventLogGenerator(LibraryEventLog.EventLog.Enum.HisBid_SuaThau, String.Join(", ", logs)).BidNumber(bid.BID_NUMBER).Run();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        internal void RollbackData()
        {
            try
            {
                this.hisBidBloodTypeCreate.RollbackData();
                this.hisBidBloodTypeUpdate.RollbackData();
                this.hisBidMaterialTypeCreate.RollbackData();
                this.hisBidMaterialTypeUpdate.RollbackData();
                this.hisBidMedicineTypeCreate.RollbackData();
                this.hisBidMedicineTypeUpdate.RollbackData();
                if (IsNotNullOrEmpty(this.beforeUpdates))
                {
                    if (!DAOWorker.HisBidDAO.UpdateList(this.beforeUpdates))
                    {
                        LogSystem.Warn("Rollback du lieu HisBid that bai, can kiem tra lai." + LogUtil.TraceData("beforeUpdates", this.beforeUpdates));
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
