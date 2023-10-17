using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisMedicineBean;
using MOS.MANAGER.HisMedicinePaty;
using MOS.MANAGER.HisMedicineType;
using MOS.MANAGER.HisMediStockMety;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;


namespace MOS.MANAGER.HisMedicine
{
    partial class HisMedicineGet : GetBase
    {
        private const string MEDICINE_PREFIX = "M";
        private const string MEDICINE_TYPE_PREFIX = "T";

        /// <summary>
        /// Lay danh sach medicine voi thong tin ton, kha dung ket hop voi tree medicine_type
        /// Va sap xep theo so luong ton kho cua thuoc
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        internal List<HisMedicineInStockSDO> GetInStockMedicineWithTypeTreeOrderByAmount(HisMedicineStockViewFilter filter)
        {
            try
            {
                List<HisMedicineInStockSDO> inStockMedicines = this.GetInStockMedicine(filter);
                List<V_HIS_MEDICINE_TYPE> medicineTypes = new HisMedicineTypeGet().GetView(new HisMedicineTypeViewFilterQuery());

                List<HisMedicineInStockSDO> list = this.GetInStockMedicineWithTypeTree(inStockMedicines, medicineTypes, "");

                List<HisMedicineInStockSDO> sortedList = null;
                if (list != null)
                {
                    List<HisMedicineInStockSDO> rootList = list.Where(o => string.IsNullOrEmpty(o.ParentNodeId)).ToList();
                    if (rootList != null && rootList.Count > 0)
                    {
                        sortedList = new List<HisMedicineInStockSDO>();
                        this.InStockMedicineTraversalToSortByAmount(rootList, list, sortedList);
                    }
                }
                return sortedList;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        /// <summary>
        /// Lay danh sach medicine voi thong tin ton, kha dung ket hop voi tree medicine_type
        /// Va sap xep theo han su dung cua thuoc
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        internal List<List<HisMedicineInStockSDO>> GetInStockMedicineWithTypeTreeOrderByExpiredDate(HisMedicineStockViewFilter filter)
        {
            try
            {
                List<List<HisMedicineInStockSDO>> result = null;
                List<HisMedicineInStockSDO> inStockMedicines = this.GetInStockMedicine(filter);
                List<V_HIS_MEDICINE_TYPE> medicineTypes = new HisMedicineTypeGet().GetView(new HisMedicineTypeViewFilterQuery());

                if (inStockMedicines != null && inStockMedicines.Count > 0 && IsNotNullOrEmpty(medicineTypes))
                {
                    result = new List<List<HisMedicineInStockSDO>>();
                    //Sap xep cac danh sach co HSD sap het len dau va thuoc ko co HSD de cuoi cung
                    var groups = inStockMedicines.GroupBy(o => o.EXPIRED_DATE).OrderByDescending(o => o.Key.HasValue).ThenBy(o => o.Key);
                    foreach (var group in groups)
                    {
                        var expiredDate = group.Key;
                        string key = expiredDate.HasValue ? expiredDate.ToString() : "null";//fix "null" de phuc vu client hien thi
                        List<HisMedicineInStockSDO> subListWithTree = this.GetInStockMedicineWithTypeTree(group.ToList<HisMedicineInStockSDO>(), medicineTypes, key);
                        subListWithTree.ForEach(em => em.EXPIRED_DATE = expiredDate);

                        List<HisMedicineInStockSDO> rootList = subListWithTree.Where(o => string.IsNullOrEmpty(o.ParentNodeId)).ToList();
                        List<HisMedicineInStockSDO> sortedList = null;
                        if (rootList != null && rootList.Count > 0)
                        {
                            sortedList = new List<HisMedicineInStockSDO>();
                            this.InStockMedicineTraversalToSortByExpiredDate(rootList, subListWithTree, sortedList);
                        }
                        result.Add(sortedList);
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        /// <summary>
        /// Lay danh sach cac medincine voi du lieu ton va kha dung
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        internal List<HisMedicineInStockSDO> GetInStockMedicine(HisMedicineStockViewFilter filter)
        {
            try
            {
                List<HisMedicineInStockSDO> result = new List<HisMedicineInStockSDO>();

                //Lay ra danh sach toan bo medicine_bean
                HisMedicineBeanViewFilterQuery medicineBeanFilter = new HisMedicineBeanViewFilterQuery();
                medicineBeanFilter.MEDI_STOCK_ID = filter.MEDI_STOCK_ID;
                medicineBeanFilter.MEDI_STOCK_IDs = filter.MEDI_STOCK_IDs;
                medicineBeanFilter.MEDICINE_ID = filter.ID;
                medicineBeanFilter.MEDICINE_IDs = filter.IDs;
                medicineBeanFilter.KEY_WORD = filter.KEY_WORD;
                //medicineBeanFilter.MEDICINE_IS_ACTIVE = filter.IS_ACTIVE;
                medicineBeanFilter.MEDICINE_TYPE_IS_ACTIVE = filter.MEDICINE_TYPE_IS_ACTIVE;
                medicineBeanFilter.MEDICINE_TYPE_ID = filter.MEDICINE_TYPE_ID;
                medicineBeanFilter.IN_STOCK = MOS.Filter.HisMedicineBeanViewFilter.InStockEnum.YES;
                medicineBeanFilter.MEDICINE_IS_ACTIVE = filter.MEDICINE_IS_ACTIVE;

                //Dung "new HisMedicineBeanGet()" thay vi dung "this" de ko truyen param vao (vi anh huong den start, limit, count trong param)
                List<V_HIS_MEDICINE_BEAN> vMedicineBeans = new HisMedicineBeanGet().GetView(medicineBeanFilter);

                //List<V_HIS_MEDICINE_BEAN> t = IsNotNullOrEmpty(vMedicineBeans) ? vMedicineBeans.Where(o => o.MEDICINE_TYPE_CODE == "TH4509").ToList() : null;
                List<HIS_MEDI_STOCK_METY> mediStockMetys = null;

                //co lay thong tin gia ban khong
                bool includeExpPrice = (filter.INCLUDE_EXP_PRICE && filter.EXP_PATIENT_TYPE_ID.HasValue);
                List<HIS_MEDICINE_PATY> medicinePatys = this.GetMedicinePaty(vMedicineBeans, filter);

                //Chi lay cac thong tin cau hinh "thuoc-kho" trong truong hop xem theo tung kho
                bool showByOneMedistock = filter.MEDI_STOCK_ID.HasValue || (filter.MEDI_STOCK_IDs != null && filter.MEDI_STOCK_IDs.Count == 1);
                if (showByOneMedistock)
                {
                    HisMediStockMetyFilterQuery stockMetyFilter = new HisMediStockMetyFilterQuery();
                    stockMetyFilter.MEDI_STOCK_ID = filter.MEDI_STOCK_ID;
                    stockMetyFilter.MEDI_STOCK_IDs = filter.MEDI_STOCK_IDs;
                    stockMetyFilter.MEDICINE_TYPE_ID = filter.MEDICINE_TYPE_ID;
                    mediStockMetys = new HisMediStockMetyGet().Get(stockMetyFilter);
                }

                //Neu co' yeu cau lay danh sach bao gom ca cac medicine_type ko co thuoc trong kho
                if (filter.INCLUDE_EMPTY)
                {
                    List<long> mediStockIds = new List<long>() { filter.MEDI_STOCK_ID ?? 0 };
                    if (filter.GROUP_BY_MEDI_STOCK || (filter.MEDI_STOCK_IDs != null && filter.MEDI_STOCK_IDs.Count == 1))
                    {
                        mediStockIds = filter.MEDI_STOCK_IDs;
                    }

                    List<V_HIS_MEDICINE_TYPE> medicineTypes = DAOWorker.SqlDAO.GetSql<V_HIS_MEDICINE_TYPE>(string.Format("SELECT * FROM V_HIS_MEDICINE_TYPE METY WHERE EXISTS (SELECT 1 FROM V_HIS_IMP_MEST_MEDICINE IMME WHERE IMME.MEDICINE_TYPE_ID = METY.ID AND IMME.MEDI_STOCK_ID IN ({0}))", string.Join(", ", mediStockIds)));

                    foreach (long mediStockId in mediStockIds)
                    {
                        foreach (V_HIS_MEDICINE_TYPE medicineType in medicineTypes)
                        {
                            //Neu medicine_type co is_leaf = 1 va ko co trong danh sach bean thi tuc la khong co thuoc trong kho
                            if (medicineType.IS_LEAF == MOS.UTILITY.Constant.IS_TRUE &&
                                (vMedicineBeans == null || !vMedicineBeans.Where(o => o.MEDICINE_TYPE_ID == medicineType.ID).Any()))
                            {
                                HisMedicineInStockSDO sdo = new HisMedicineInStockSDO();
                                sdo.ID = -1 * medicineType.ID; //lay id cua loai thuoc va nhan -1
                                sdo.IS_LEAF = medicineType.IS_LEAF;
                                sdo.MEDICINE_TYPE_IS_ACTIVE = medicineType.IS_ACTIVE;
                                sdo.MEDICINE_TYPE_ID = medicineType.ID;
                                sdo.MEDICINE_TYPE_CODE = medicineType.MEDICINE_TYPE_CODE;
                                sdo.MEDICINE_TYPE_NAME = medicineType.MEDICINE_TYPE_NAME;
                                sdo.MEDI_STOCK_ID = mediStockId;
                                sdo.PARENT_ID = medicineType.PARENT_ID;
                                sdo.SERVICE_ID = medicineType.SERVICE_ID;
                                sdo.SERVICE_UNIT_CODE = medicineType.SERVICE_UNIT_CODE;
                                sdo.SERVICE_UNIT_NAME = medicineType.SERVICE_UNIT_NAME;
                                sdo.ALERT_EXPIRED_DATE = medicineType.ALERT_EXPIRED_DATE;
                                sdo.NATIONAL_NAME = medicineType.NATIONAL_NAME;
                                sdo.ACTIVE_INGR_BHYT_CODE = medicineType.ACTIVE_INGR_BHYT_CODE;
                                sdo.ACTIVE_INGR_BHYT_NAME = medicineType.ACTIVE_INGR_BHYT_NAME;
                                sdo.CONCENTRA = medicineType.CONCENTRA;
                                sdo.MANUFACTURER_NAME = medicineType.MANUFACTURER_NAME;
                                sdo.LOCKING_REASON = medicineType.LOCKING_REASON;
                                sdo.REGISTER_NUMBER = medicineType.REGISTER_NUMBER;
                                sdo.TotalAmount = 0;
                                sdo.AvailableAmount = 0;

                                if (showByOneMedistock)
                                {
                                    HIS_MEDI_STOCK_METY stockMety = mediStockMetys != null ? mediStockMetys
                                        .FirstOrDefault(o => o.MEDI_STOCK_ID == mediStockId && o.MEDICINE_TYPE_ID == medicineType.ID) : null;

                                    sdo.BaseAmount = stockMety != null ? stockMety.ALERT_MAX_IN_STOCK : null;
                                    //neu co cau hinh theo kho thi lay theo kho, ko thi lay theo loai thuoc
                                    sdo.ALERT_MIN_IN_STOCK = stockMety != null && stockMety.ALERT_MIN_IN_STOCK.HasValue ? stockMety.ALERT_MIN_IN_STOCK : medicineType.ALERT_MIN_IN_STOCK;
                                }

                                result.Add(sdo);
                            }
                        }
                    }
                }

                if (IsNotNullOrEmpty(vMedicineBeans))
                {
                    Dictionary<string, HisMedicineInStockSDO> dic = new Dictionary<string, HisMedicineInStockSDO>();
                    foreach (V_HIS_MEDICINE_BEAN bean in vMedicineBeans)
                    {
                        string key = filter.GROUP_BY_MEDI_STOCK ? string.Format("{0}-{1}", bean.MEDICINE_ID, bean.MEDI_STOCK_ID) : bean.MEDICINE_ID.ToString();

                        if (dic.ContainsKey(key))
                        {
                            HisMedicineInStockSDO sdo = dic[key];
                            sdo.TotalAmount += bean.AMOUNT;
                            sdo.AvailableAmount += bean.IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE
                                && bean.MEDICINE_IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE ? bean.AMOUNT : 0;

                            if (String.IsNullOrWhiteSpace(sdo.LOCKING_REASON) && !String.IsNullOrWhiteSpace(bean.LOCKING_REASON))
                            {
                                sdo.LOCKING_REASON = bean.LOCKING_REASON;
                            }
                        }
                        else
                        {
                            HisMedicineInStockSDO sdo = new HisMedicineInStockSDO();
                            sdo.ID = bean.MEDICINE_ID;
                            //medicine_bean thi is_leaf luon la true
                            sdo.IS_LEAF = MOS.UTILITY.Constant.IS_TRUE;
                            sdo.MEDICINE_TYPE_IS_ACTIVE = bean.MEDICINE_TYPE_IS_ACTIVE;
                            sdo.IS_ACTIVE = bean.MEDICINE_IS_ACTIVE;
                            sdo.MEDICINE_TYPE_ID = bean.MEDICINE_TYPE_ID;
                            sdo.MEDICINE_TYPE_CODE = bean.MEDICINE_TYPE_CODE;
                            sdo.MEDICINE_TYPE_NAME = bean.MEDICINE_TYPE_NAME;
                            sdo.MEDI_STOCK_ID = filter.GROUP_BY_MEDI_STOCK ? bean.MEDI_STOCK_ID : filter.MEDI_STOCK_ID;
                            sdo.PARENT_ID = bean.PARENT_ID;
                            sdo.SERVICE_ID = bean.SERVICE_ID;
                            sdo.SUPPLIER_ID = bean.SUPPLIER_ID;
                            sdo.PACKAGE_NUMBER = bean.PACKAGE_NUMBER;
                            sdo.IMP_TIME = bean.IMP_TIME;
                            sdo.IMP_PRICE = bean.IMP_PRICE;
                            sdo.IMP_VAT_RATIO = bean.IMP_VAT_RATIO;
                            sdo.BID_NUMBER = bean.TDL_BID_NUMBER;
                            sdo.EXPIRED_DATE = bean.EXPIRED_DATE;
                            sdo.SERVICE_UNIT_CODE = bean.SERVICE_UNIT_CODE;
                            sdo.SERVICE_UNIT_NAME = bean.SERVICE_UNIT_NAME;
                            sdo.SUPPLIER_CODE = bean.SUPPLIER_CODE;
                            sdo.SUPPLIER_NAME = bean.SUPPLIER_NAME;
                            sdo.ALERT_EXPIRED_DATE = bean.ALERT_EXPIRED_DATE;
                            sdo.NATIONAL_NAME = bean.NATIONAL_NAME;
                            sdo.NUM_ORDER = bean.NUM_ORDER;
                            sdo.TotalAmount = bean.AMOUNT;
                            sdo.ACTIVE_INGR_BHYT_CODE = bean.ACTIVE_INGR_BHYT_CODE;
                            sdo.ACTIVE_INGR_BHYT_NAME = bean.ACTIVE_INGR_BHYT_NAME;
                            sdo.MANUFACTURER_NAME = bean.MANUFACTURER_NAME;
                            sdo.CONCENTRA = bean.CONCENTRA;
                            sdo.LOCKING_REASON = bean.LOCKING_REASON;
                            sdo.AvailableAmount = bean.IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE ? bean.AMOUNT : 0;
                            sdo.REGISTER_NUMBER = bean.REGISTER_NUMBER;

                            if (showByOneMedistock)
                            {
                                HIS_MEDI_STOCK_METY stockMety = mediStockMetys != null ? mediStockMetys
                                    .FirstOrDefault(o => o.MEDI_STOCK_ID == bean.MEDI_STOCK_ID && o.MEDICINE_TYPE_ID == bean.MEDICINE_TYPE_ID) : null;

                                sdo.BaseAmount = stockMety != null ? stockMety.ALERT_MAX_IN_STOCK : null;
                                //neu co cau hinh theo kho thi lay theo kho, ko thi lay theo loai thuoc
                                sdo.ALERT_MIN_IN_STOCK = stockMety != null && stockMety.ALERT_MIN_IN_STOCK.HasValue ? stockMety.ALERT_MIN_IN_STOCK : bean.ALERT_MIN_IN_STOCK;
                            }

                            if (includeExpPrice)
                            {
                                if (bean.TDL_IS_SALE_EQUAL_IMP_PRICE.HasValue && bean.TDL_IS_SALE_EQUAL_IMP_PRICE.Value == Constant.IS_TRUE)
                                {
                                    sdo.ExpPrice = bean.TDL_MEDICINE_IMP_PRICE;
                                    sdo.ExpVatRatio = bean.TDL_MEDICINE_IMP_VAT_RATIO;
                                }
                                else
                                {
                                    HIS_MEDICINE_PATY paty = medicinePatys != null ? medicinePatys.FirstOrDefault(o => o.MEDICINE_ID == bean.MEDICINE_ID) : null;
                                    if (paty != null)
                                    {
                                        sdo.ExpPrice = paty.EXP_PRICE;
                                        sdo.ExpVatRatio = paty.EXP_VAT_RATIO;
                                    }
                                }
                            }

                            dic.Add(key, sdo);
                        }
                    }

                    result.AddRange(dic.Values.ToList());
                }
                //sort va paging lai du lieu
                int start = param.Start.HasValue ? param.Start.Value : 0;
                int limit = param.Limit.HasValue ? param.Limit.Value : Int32.MaxValue;
                param.Count = result.Count;
                result = result.AsQueryable().OrderByProperty(filter.ORDER_FIELD, filter.ORDER_DIRECTION).Skip(start).Take(limit).ToList();
                return result;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        /// <summary>
        /// Xu ly de lay du lieu ton kho o 2 kho va hien thi tuong ung o 2 truong khac nhau
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        internal List<HisMedicineIn2StockSDO> GetIn2StockMedicine(HisMedicine2StockFilter filter)
        {
            List<HisMedicineIn2StockSDO> result = null;

            HisMedicineStockViewFilter firstFilter = new HisMedicineStockViewFilter();
            if (IsNotNullOrEmpty(filter.FIRST_MEDI_STOCK_IDs))
            {
                firstFilter.MEDI_STOCK_IDs = filter.FIRST_MEDI_STOCK_IDs;
                firstFilter.GROUP_BY_MEDI_STOCK = true;
            }
            else
            {
                firstFilter.MEDI_STOCK_ID = filter.FIRST_MEDI_STOCK_ID;
            }
            firstFilter.IS_ACTIVE = filter.IS_ACTIVE;
            firstFilter.IS_LEAF = filter.IS_LEAF;
            firstFilter.MEDICINE_TYPE_IS_ACTIVE = filter.MEDICINE_TYPE_IS_ACTIVE;
            firstFilter.ORDER_DIRECTION = filter.ORDER_DIRECTION;
            firstFilter.ORDER_FIELD = filter.ORDER_FIELD;

            List<HisMedicineInStockSDO> firstStock = this.GetInStockMedicine(firstFilter);

            HisMedicineStockViewFilter secondFilter = new HisMedicineStockViewFilter();
            secondFilter.MEDI_STOCK_ID = filter.SECOND_MEDI_STOCK_ID;
            secondFilter.IS_ACTIVE = filter.IS_ACTIVE;
            secondFilter.IS_LEAF = filter.IS_LEAF;
            secondFilter.MEDICINE_TYPE_IS_ACTIVE = filter.MEDICINE_TYPE_IS_ACTIVE;
            secondFilter.ORDER_DIRECTION = filter.ORDER_DIRECTION;
            secondFilter.ORDER_FIELD = filter.ORDER_FIELD;

            List<HisMedicineInStockSDO> secondStock = this.GetInStockMedicine(secondFilter);

            if (IsNotNullOrEmpty(firstStock) || IsNotNullOrEmpty(secondStock))
            {
                result = new List<HisMedicineIn2StockSDO>();
                Mapper.CreateMap<HisMedicineInStockSDO, HisMedicineIn2StockSDO>();

                //Duyet d/s kho 1 va tim cac du lieu o kho 2 tuong ung
                if (IsNotNullOrEmpty(firstStock))
                {
                    foreach (HisMedicineInStockSDO first in firstStock)
                    {
                        HisMedicineInStockSDO second = IsNotNullOrEmpty(secondStock) ? secondStock.Where(o => o.ID == first.ID).FirstOrDefault() : null;
                        HisMedicineIn2StockSDO s = Mapper.Map<HisMedicineIn2StockSDO>(first);
                        if (IsNotNullOrEmpty(filter.FIRST_MEDI_STOCK_IDs))
                        {
                            s.MEDI_STOCK_ID = first.MEDI_STOCK_ID;
                        }
                        else
                        {
                            s.MEDI_STOCK_ID = filter.FIRST_MEDI_STOCK_ID;
                        }
                        s.AvailableAmount = first.AvailableAmount;
                        s.TotalAmount = first.TotalAmount;

                        s.SECOND_MEDI_STOCK_ID = filter.SECOND_MEDI_STOCK_ID;
                        s.SecondAvailableAmount = second != null ? second.AvailableAmount : 0;
                        s.SecondTotalAmount = second != null ? second.TotalAmount : 0;

                        result.Add(s);
                    }
                }

                //Lay cac d/s co trong kho 2 va khong co trong kho 1
                List<HisMedicineInStockSDO> tmp = IsNotNullOrEmpty(secondStock) ? secondStock.Where(o => firstStock == null || !firstStock.Exists(t => t.ID == o.ID)).ToList() : null;
                if (IsNotNullOrEmpty(tmp))
                {
                    foreach (HisMedicineInStockSDO second in tmp)
                    {
                        if (IsNotNullOrEmpty(filter.FIRST_MEDI_STOCK_IDs))
                        {
                            foreach (var stock in filter.FIRST_MEDI_STOCK_IDs)
                            {
                                HisMedicineIn2StockSDO s = Mapper.Map<HisMedicineIn2StockSDO>(second);
                                s.MEDI_STOCK_ID = stock;
                                s.AvailableAmount = 0;
                                s.TotalAmount = 0;
                                s.SECOND_MEDI_STOCK_ID = second.MEDI_STOCK_ID;
                                s.SecondAvailableAmount = second.AvailableAmount;
                                s.SecondTotalAmount = second.TotalAmount;

                                result.Add(s);
                            }
                        }
                        else
                        {
                            HisMedicineIn2StockSDO s = Mapper.Map<HisMedicineIn2StockSDO>(second);
                            s.MEDI_STOCK_ID = filter.FIRST_MEDI_STOCK_ID;
                            s.AvailableAmount = 0;
                            s.TotalAmount = 0;
                            s.SECOND_MEDI_STOCK_ID = second.MEDI_STOCK_ID;
                            s.SecondAvailableAmount = second.AvailableAmount;
                            s.SecondTotalAmount = second.TotalAmount;

                            result.Add(s);
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Lay danh sach medicine voi thong tin ton, kha dung ket hop voi cay medicine_type
        /// De build tree, su dung khai niem "nodeId". Trong do:
        /// - "nodeId" cua cac node la medicine_type = nodeIdPrefix + T + medicine_type_id
        /// - "nodeId" cua cac node la medicine = nodeIdPrefix + M + medicine_id
        /// (nodeIdPrefix: duoc su dung trong truong hop tra ve nhieu list tree (vd: theo han su dung, tuong ung voi moi han su dung la 1 tree, khi do, cac phan tu trong cac tree can nodeIdPrefix de dam bao tinh duy nhat)
        /// </summary>
        /// <param name="inStockMedicines"></param>
        /// <param name="nodeIdPrefix"></param>
        /// <returns></returns>
        private List<HisMedicineInStockSDO> GetInStockMedicineWithTypeTree(List<HisMedicineInStockSDO> inStockMedicines, List<V_HIS_MEDICINE_TYPE> medicineTypes, string nodeIdPrefix)
        {
            try
            {
                List<HisMedicineInStockSDO> result = null;
                if (inStockMedicines != null && inStockMedicines.Count > 0 && medicineTypes != null && medicineTypes.Count > 0)
                {
                    result = new List<HisMedicineInStockSDO>();
                    Mapper.CreateMap<V_HIS_MEDICINE_TYPE, HisMedicineInStockSDO>();


                    foreach (HisMedicineInStockSDO sdo in inStockMedicines)
                    {
                        sdo.ParentNodeId = nodeIdPrefix + MEDICINE_TYPE_PREFIX + sdo.MEDICINE_TYPE_ID;
                        sdo.NodeId = nodeIdPrefix + MEDICINE_PREFIX + sdo.ID;
                        sdo.isTypeNode = false;
                        result.Add(sdo);

                        //neu node nay chua duoc add vao danh sach
                        if (!result.Exists(o => o.NodeId == sdo.ParentNodeId))
                        {
                            V_HIS_MEDICINE_TYPE type = medicineTypes.Where(o => o.ID == sdo.MEDICINE_TYPE_ID).SingleOrDefault();
                            HisMedicineInStockSDO t = Mapper.Map<HisMedicineInStockSDO>(type);

                            t.TotalAmount = inStockMedicines.Where(o => o.MEDICINE_TYPE_ID == type.ID).Sum(o => o.TotalAmount);
                            t.AvailableAmount = inStockMedicines.Where(o => o.MEDICINE_TYPE_ID == type.ID).Sum(o => o.AvailableAmount);
                            t.NodeId = nodeIdPrefix + MEDICINE_TYPE_PREFIX + type.ID;
                            t.ParentNodeId = type.PARENT_ID.HasValue ? nodeIdPrefix + MEDICINE_TYPE_PREFIX + type.PARENT_ID : null;
                            t.isTypeNode = true;
                            t.BaseAmount = sdo.BaseAmount;
                            t.RealBaseAmount = sdo.RealBaseAmount;
                            t.ALERT_MIN_IN_STOCK = sdo.ALERT_MIN_IN_STOCK;
                            t.MEDI_STOCK_ID = null;//set null tranh bi set sai du lieu khi dung mapper (vi trong HIS_MATERIAL_TYPE ko co MEDI_STOCK_ID)
                            result.Add(t);
                            this.MedicineTypeTraversalToBuildTree(type, medicineTypes, result, nodeIdPrefix);
                        }

                        // Chi hien thi baseAmount o cac dong cha (Dong lo khong hien thi)
                        sdo.BaseAmount = null;
                        sdo.RealBaseAmount = null;
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        /// <summary>
        /// Duyet danh sach de thuc hien build tree (tao du lieu co quan he "cha-con")
        /// Duyet tu node "la'" (leaf) dan len cac node phia tren
        /// </summary>
        /// <param name="child"></param>
        /// <param name="allMedicineTypes"></param>
        /// <param name="builtTrees"></param>
        private void MedicineTypeTraversalToBuildTree(V_HIS_MEDICINE_TYPE child, List<V_HIS_MEDICINE_TYPE> allMedicineTypes, List<HisMedicineInStockSDO> builtTrees, string nodeIdPrefix)
        {
            if (builtTrees == null)
            {
                builtTrees = new List<HisMedicineInStockSDO>();
            }

            //Tim medicine_type la cha cua "child"
            if (child.PARENT_ID.HasValue)
            {
                string nodeId = nodeIdPrefix + MEDICINE_TYPE_PREFIX + child.PARENT_ID.Value;
                HisMedicineInStockSDO sdo = builtTrees.Where(t => t.NodeId == nodeId).FirstOrDefault();
                V_HIS_MEDICINE_TYPE parent = allMedicineTypes.Where(o => o.ID == child.PARENT_ID).SingleOrDefault();

                //Neu chua co thi tao moi
                //Neu da co trong danh sach "builtTrees" thi cap nhat lai so luong
                if (sdo == null)
                {
                    if (parent != null)
                    {
                        Mapper.CreateMap<V_HIS_MEDICINE_TYPE, HisMedicineInStockSDO>();
                        sdo = Mapper.Map<HisMedicineInStockSDO>(parent);
                        sdo.NodeId = nodeId;
                        sdo.ParentNodeId = parent.PARENT_ID.HasValue ? nodeIdPrefix + MEDICINE_TYPE_PREFIX + parent.PARENT_ID : null;
                        sdo.isTypeNode = true;
                        sdo.MEDI_STOCK_ID = null;//set null tranh bi set sai du lieu khi dung mapper (vi trong HIS_MATERIAL_TYPE ko co MEDI_STOCK_ID)

                        builtTrees.Add(sdo);
                        sdo.TotalAmount = builtTrees.Where(o => o.ParentNodeId == sdo.NodeId).Sum(o => o.TotalAmount);
                        sdo.AvailableAmount = builtTrees.Where(o => o.ParentNodeId == sdo.NodeId).Sum(o => o.AvailableAmount);
                        this.MedicineTypeTraversalToBuildTree(parent, allMedicineTypes, builtTrees, nodeIdPrefix);
                    }
                }
                else
                {
                    sdo.TotalAmount = builtTrees.Where(o => o.ParentNodeId == sdo.NodeId).Sum(o => o.TotalAmount);
                    sdo.AvailableAmount = builtTrees.Where(o => o.ParentNodeId == sdo.NodeId).Sum(o => o.AvailableAmount);
                    this.MedicineTypeTraversalToBuildTree(parent, allMedicineTypes, builtTrees, nodeIdPrefix);
                }
            }
        }

        /// <summary>
        /// Sap xep theo cac thu tu uu tien sau:
        /// - Danh sach co so luong duoi muc canh bao nhung co amount > 0
        /// - Danh sach co so luong tren muc canh bao
        /// - Danh sach co so luong lon hon 0 nhung ko co muc canh bao
        /// - Danh sach co so luong bang 0
        /// - Sap xep theo NUM_ORDER
        /// </summary>
        /// <param name="listToSort"></param>
        /// <param name="all"></param>
        /// <param name="sortedList"></param>
        private void InStockMedicineTraversalToSortByAmount(List<HisMedicineInStockSDO> listToSort, List<HisMedicineInStockSDO> all, List<HisMedicineInStockSDO> sortedList)
        {
            if (sortedList == null)
            {
                sortedList = new List<HisMedicineInStockSDO>();
            }

            if (listToSort != null && listToSort.Count > 0)
            {
                //Danh sach co so luong duoi muc canh bao nhung co amount > 0
                List<HisMedicineInStockSDO> lessThanWarning = listToSort
                    .Where(o => o.ALERT_MIN_IN_STOCK.HasValue && o.TotalAmount.HasValue
                        && o.TotalAmount <= o.ALERT_MIN_IN_STOCK && o.TotalAmount > 0)
                    .OrderByDescending(o => o.NUM_ORDER)
                    .ThenBy(o => o.MEDICINE_TYPE_NAME)
                    .ToList();

                //Danh sach co so luong tren muc canh bao
                List<HisMedicineInStockSDO> greaterThanWarning = listToSort
                    .Where(o => o.ALERT_MIN_IN_STOCK.HasValue
                        && o.TotalAmount.HasValue && o.TotalAmount > o.ALERT_MIN_IN_STOCK)
                    .OrderByDescending(o => o.NUM_ORDER)
                    .ThenBy(o => o.MEDICINE_TYPE_NAME)
                    .ToList();

                //Danh sach co so luong lon hon 0 nhung ko co muc canh bao
                List<HisMedicineInStockSDO> greaterThan0AndNoWarning = listToSort
                    .Where(o => !o.ALERT_MIN_IN_STOCK.HasValue
                        && o.TotalAmount.HasValue && o.TotalAmount > 0)
                    .OrderByDescending(o => o.NUM_ORDER)
                    .ThenBy(o => o.MEDICINE_TYPE_NAME)
                    .ToList();
                //Danh sach co so luong bang 0
                List<HisMedicineInStockSDO> equal0 = listToSort
                    .Where(o => !o.TotalAmount.HasValue || o.TotalAmount == 0)
                    .OrderBy(o => o.ALERT_MIN_IN_STOCK)
                    .ThenByDescending(o => o.NUM_ORDER)
                    .ThenBy(o => o.MEDICINE_TYPE_NAME)
                    .ToList();

                sortedList.AddRange(lessThanWarning);
                sortedList.AddRange(greaterThanWarning);
                sortedList.AddRange(greaterThan0AndNoWarning);
                sortedList.AddRange(equal0);

                foreach (HisMedicineInStockSDO tmp in listToSort)
                {
                    List<HisMedicineInStockSDO> newListToSort = all.Where(o => o.ParentNodeId == tmp.NodeId).ToList();
                    InStockMedicineTraversalToSortByAmount(newListToSort, all, sortedList);
                }
            }
        }

        /// <summary>
        /// Sap xep theo cac thu tu uu tien sau:
        /// - Thuoc co han su dung nam trong khoang canh bao so voi ngay hien tai (tu nho den lon)
        /// - Thuoc co han su dung chua nam trong khoang canh bao so voi ngay hien tai (tu nho den lon)
        /// - Thuoc ko co thong tin han su dung
        /// - Sap xep theo NUM_ORDER
        /// </summary>
        /// <param name="listToSort"></param>
        /// <param name="all"></param>
        /// <param name="sortedList"></param>
        private void InStockMedicineTraversalToSortByExpiredDate(List<HisMedicineInStockSDO> listToSort, List<HisMedicineInStockSDO> all, List<HisMedicineInStockSDO> sortedList)
        {
            if (sortedList == null)
            {
                sortedList = new List<HisMedicineInStockSDO>();
            }

            if (listToSort != null && listToSort.Count > 0)
            {
                DateTime now = DateTime.Now;
                //Thuoc co han su dung nam trong khoang canh bao so voi ngay hien tai (tu nho den lon)
                List<HisMedicineInStockSDO> inWarningRange = listToSort
                    .Where(o => o.ALERT_EXPIRED_DATE.HasValue
                        && o.EXPIRED_DATE.HasValue
                        && Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime((long)o.EXPIRED_DATE.Value).HasValue
                        && Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime((long)o.EXPIRED_DATE.Value).Value.AddDays(-1 * o.ALERT_EXPIRED_DATE.Value) <= now)
                    .OrderBy(o => Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime((long)o.EXPIRED_DATE.Value).Value.AddDays(-1 * o.ALERT_EXPIRED_DATE.Value))
                    .ThenByDescending(o => o.NUM_ORDER)
                    .ThenBy(o => o.MEDICINE_TYPE_NAME)
                    .ToList();

                //Thuoc co han su dung chua nam trong khoang canh bao so voi ngay hien tai (tu nho den lon)
                List<HisMedicineInStockSDO> outWarningRange = listToSort
                    .Where(o => o.ALERT_EXPIRED_DATE.HasValue
                        && o.EXPIRED_DATE.HasValue
                        && Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime((long)o.EXPIRED_DATE.Value).HasValue
                        && Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime((long)o.EXPIRED_DATE.Value).Value.AddDays(-1 * o.ALERT_EXPIRED_DATE.Value) > now)
                    .OrderBy(o => Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime((long)o.EXPIRED_DATE.Value).Value.AddDays(-1 * o.ALERT_EXPIRED_DATE.Value))
                    .ThenByDescending(o => o.NUM_ORDER)
                    .ThenBy(o => o.MEDICINE_TYPE_NAME)
                    .ToList();

                //Thuoc ko co thong tin canh bao han su dung
                List<HisMedicineInStockSDO> noAlert = listToSort
                    .Where(o => !o.ALERT_EXPIRED_DATE.HasValue
                        || !o.EXPIRED_DATE.HasValue
                        || !Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime((long)o.EXPIRED_DATE.Value).HasValue)
                    .OrderByDescending(o => o.NUM_ORDER)
                    .ThenBy(o => o.MEDICINE_TYPE_NAME)
                    .ToList();

                sortedList.AddRange(inWarningRange);
                sortedList.AddRange(outWarningRange);
                sortedList.AddRange(noAlert);

                foreach (HisMedicineInStockSDO tmp in listToSort)
                {
                    List<HisMedicineInStockSDO> newListToSort = all.Where(o => o.ParentNodeId == tmp.NodeId).ToList();
                    InStockMedicineTraversalToSortByExpiredDate(newListToSort, all, sortedList);
                }
            }
        }

        private List<HIS_MEDICINE_PATY> GetMedicinePaty(List<V_HIS_MEDICINE_BEAN> vMedicineBeans, HisMedicineStockViewFilter filter)
        {
            List<HIS_MEDICINE_PATY> rs = null;
            if (IsNotNullOrEmpty(vMedicineBeans) && filter.INCLUDE_EXP_PRICE && filter.EXP_PATIENT_TYPE_ID.HasValue)
            {
                List<long> medicineIds = vMedicineBeans.Where(o => !o.TDL_IS_SALE_EQUAL_IMP_PRICE.HasValue || o.TDL_IS_SALE_EQUAL_IMP_PRICE.Value != Constant.IS_TRUE).Select(s => s.MEDICINE_ID).Distinct().ToList();
                if (IsNotNullOrEmpty(medicineIds))
                {
                    HisMedicinePatyFilterQuery mediPatyFilter = new HisMedicinePatyFilterQuery();
                    mediPatyFilter.MEDICINE_IDs = medicineIds;
                    mediPatyFilter.PATIENT_TYPE_ID = filter.EXP_PATIENT_TYPE_ID.Value;
                    mediPatyFilter.IS_ACTIVE = IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE;
                    rs = new HisMedicinePatyGet().Get(mediPatyFilter);
                }
            }

            return rs;
        }
    }
}
