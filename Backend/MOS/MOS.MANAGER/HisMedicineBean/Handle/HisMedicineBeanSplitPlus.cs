using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.DAO.Sql;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisMedicine;
using MOS.MANAGER.HisMedicineBean.Update;
using MOS.MANAGER.HisMedicinePaty;
using MOS.MANAGER.HisMedicineType;
using MOS.OracleUDT;
using MOS.SDO;
using MOS.UTILITY;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MOS.MANAGER.HisMedicineBean.Handle
{
    class BeanResultHolder
    {
        public bool IsSuccess { get; set; }
        public THisMedicineBean[] Data { get; set; }
    }
    //can bo sung code check khoa
    class HisMedicineBeanSplitPlus : BusinessBase
    {
        private List<long> recentLockedBeanIds = new List<long>();

        private HisMedicineBeanLock hisMedicineBeanLock;

        internal HisMedicineBeanSplitPlus()
            : base()
        {
            this.Init();
        }

        internal HisMedicineBeanSplitPlus(CommonParam paramSplit)
            : base(paramSplit)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisMedicineBeanLock = new HisMedicineBeanLock(param);
        }

        /// <summary>
        /// Xy ly tach medicine_bean dua theo medicine_type
        /// </summary>
        /// <param name="hisMedicineDTOs"></param>
        internal bool SplitByMedicineType(List<ExpMedicineTypeSDO> data, long mediStockId, long? expiredDate, List<HIS_MEDICINE_BEAN> useMedicineBeans, List<long> notUseBeandIds, ref Dictionary<ExpMedicineTypeSDO, List<HIS_MEDICINE_BEAN>> dicToUse, ref List<HIS_MEDICINE_PATY> medicinePaties)
        {
            bool result = false;
            try
            {
                bool valid = true;

                HisMedicineBeanCheck checker = new HisMedicineBeanCheck(param);
                valid = valid && IsNotNullOrEmpty(data);
                valid = valid && checker.IsValidRequireAmount(data);
                if (valid)
                {
                    List<HIS_MEDICINE_BEAN> medicineBeans = null;
                    if (IsNotNullOrEmpty(useMedicineBeans))
                    {
                        medicineBeans = useMedicineBeans;
                    }
                    else
                    {
                        medicineBeans = this.GetMedicineBeanToSplit(data, mediStockId, notUseBeandIds, expiredDate);
                    }

                    if (!IsNotNullOrEmpty(medicineBeans))
                    {
                        List<string> typeNames = HisMedicineTypeCFG.DATA
                            .Where(o => data.Exists(t => t.MedicineTypeId == o.ID))
                            .Select(o => o.MEDICINE_TYPE_NAME)
                            .ToList();
                        string typeNameStr = string.Join(",", typeNames);
                        if (expiredDate.HasValue)
                        {
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMedicineBean_KhongDuKhaDungHetHanSuDung, typeNameStr);
                        }
                        else
                        {
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMedicineBean_KhongDuKhaDung, typeNameStr);
                        }
                        return false;
                    }

                    //Neu trong y/c tach ben co truyen vao doi tuong thanh toan va cac bean trong kho ton tai bean 
                    //ko co cau hinh "ban bang gia nhap" thi lay chinh sach gia, phuc vu kiem tra chinh sach gia
                    List<long> medicineIds = medicineBeans
                        .Where(o => o.TDL_IS_SALE_EQUAL_IMP_PRICE != MOS.UTILITY.Constant.IS_TRUE
                            && data.Exists(t => t.PatientTypeId.HasValue && t.MedicineTypeId == o.TDL_MEDICINE_TYPE_ID))
                        .Select(o => o.MEDICINE_ID).Distinct().ToList();

                    List<HIS_MEDICINE_PATY> paties = null;
                    if (IsNotNullOrEmpty(medicineIds))
                    {
                        List<long> patientTypeIds = data
                        .Where(o => o.PatientTypeId.HasValue)
                        .Select(o => o.PatientTypeId.Value).Distinct().ToList();

                        HisMedicinePatyFilterQuery filter = new HisMedicinePatyFilterQuery();
                        filter.MEDICINE_IDs = medicineIds;
                        filter.PATIENT_TYPE_IDs = patientTypeIds;
                        paties = new HisMedicinePatyGet().Get(filter);
                    }

                    medicinePaties = paties;

                    Dictionary<ExpMedicineTypeSDO, List<HIS_MEDICINE_BEAN>> dicLock = new Dictionary<ExpMedicineTypeSDO, List<HIS_MEDICINE_BEAN>>();
                    List<HIS_MEDICINE_BEAN> detachList = new List<HIS_MEDICINE_BEAN>();

                    foreach (ExpMedicineTypeSDO requireToExp in data)
                    {
                        //Lay ra cac bean thuoc loai y/c tach
                        //Trong trường hợp y/c tách có thông tin đối tượng thanh toán thì kiểm tra thông tin chính sách giá:
                        // - Bean phải thuộc lô thuốc được cấu hình bán bằng giá nhập
                        // - Hoặc phải có chính sách giá trong medicine_paty
                        List<HIS_MEDICINE_BEAN> beanToSplits = medicineBeans
                            .Where(o => o.TDL_MEDICINE_TYPE_ID == requireToExp.MedicineTypeId &&
                                (!requireToExp.PatientTypeId.HasValue
                                || o.TDL_IS_SALE_EQUAL_IMP_PRICE == MOS.UTILITY.Constant.IS_TRUE
                                || (paties != null && paties.Exists(t => t.MEDICINE_ID == o.MEDICINE_ID && t.PATIENT_TYPE_ID == requireToExp.PatientTypeId.Value))))
                            .ToList();
                        if (!IsNotNullOrEmpty(beanToSplits))
                        {
                            V_HIS_MEDICINE_TYPE medicineType = new HisMedicineTypeGet().GetViewById(requireToExp.MedicineTypeId);
                            if (expiredDate.HasValue)
                            {
                                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMedicineBean_ThuocKhaDungKhongDuHetHanSuDungDeThucHien, medicineType.MEDICINE_TYPE_NAME, "0", medicineType.SERVICE_UNIT_NAME, requireToExp.Amount.ToString(), medicineType.SERVICE_UNIT_NAME);
                            }
                            else
                            {
                                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMedicineBean_ThuocKhaDungKhongDuDeThucHien, medicineType.MEDICINE_TYPE_NAME, "0", medicineType.SERVICE_UNIT_NAME, requireToExp.Amount.ToString(), medicineType.SERVICE_UNIT_NAME);
                            }
                            throw new Exception("Rollback du lieu");
                        }
                        this.SplitBean(beanToSplits, requireToExp, detachList, dicLock);
                    }

                    dicToUse = new Dictionary<ExpMedicineTypeSDO, List<HIS_MEDICINE_BEAN>>();

                    //Tach medicine_bean
                    this.DetachBean(detachList, data, dicToUse, this.recentLockedBeanIds);

                    //Khoa medicine_bean
                    this.LockBean(dicLock, dicToUse, this.recentLockedBeanIds);

                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                dicToUse = null;
                this.RollBack();
                param.HasException = true;
                result = false;
            }
            return result;
        }


        /// <summary>
        /// Thuc hien tach bean (khi duyet phieu xuat) theo so luong truyen vao. Tuy nhien:
        /// - Neu so luong ton trong kho it hon so luong yeu cau thi se thuc hien tach bean theo so luong ton
        /// - Neu so luong ton trong kho lon hon hoac bang so luong yeu cau thi thuc hien tach bean theo so luong yeu cau
        /// </summary>
        /// <param name="data"></param>
        /// <param name="mediStockId"></param>
        /// <param name="notUseBeandIds"></param>
        /// <param name="listToUse"></param>
        /// <param name="medicinePaties"></param>
        /// <returns></returns>
        internal bool SplitAndDecreaseByMedicineType(List<ExpMedicineTypeSDO> data, long mediStockId, long? expiredDate, List<long> notUseBeandIds, ref Dictionary<ExpMedicineTypeSDO, List<HIS_MEDICINE_BEAN>> dicToUse, ref List<HIS_MEDICINE_PATY> medicinePaties)
        {
            bool result = false;
            try
            {
                bool valid = true;

                HisMedicineBeanCheck checker = new HisMedicineBeanCheck(param);
                valid = valid && IsNotNullOrEmpty(data);
                valid = valid && checker.IsValidRequireAmount(data);
                if (valid)
                {
                    List<HIS_MEDICINE_BEAN> useMedicineBeans = this.GetMedicineBeanToSplit(data, mediStockId, notUseBeandIds, expiredDate);

                    Dictionary<long, decimal> dicMedicineBean = new Dictionary<long, decimal>();

                    if (IsNotNullOrEmpty(useMedicineBeans))
                    {
                        var Groups = useMedicineBeans.GroupBy(g => g.TDL_MEDICINE_TYPE_ID);
                        foreach (var group in Groups)
                        {
                            dicMedicineBean[group.Key] = group.Sum(s => s.AMOUNT);
                        }
                    }

                    foreach (ExpMedicineTypeSDO sdo in data)
                    {
                        if (dicMedicineBean.ContainsKey(sdo.MedicineTypeId))
                        {
                            decimal availAmount = dicMedicineBean[sdo.MedicineTypeId];
                            if (availAmount <= 0)
                            {
                                sdo.Amount = 0;
                                continue;
                            }
                            if (availAmount >= sdo.Amount)
                            {
                                dicMedicineBean[sdo.MedicineTypeId] = availAmount - sdo.Amount;
                                continue;
                            }
                            else
                            {
                                sdo.Amount = availAmount;
                                dicMedicineBean[sdo.MedicineTypeId] = 0;
                                continue;
                            }
                        }
                        else
                        {
                            sdo.Amount = 0;
                        }
                    }
                    data = data.Where(o => o.Amount > 0).ToList();

                    if (!IsNotNullOrEmpty(data))
                    {
                        return true;
                    }
                    result = this.SplitByMedicineType(data, mediStockId, expiredDate, useMedicineBeans, notUseBeandIds, ref dicToUse, ref medicinePaties);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                dicToUse = null;
                param.HasException = true;
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Xu ly tach bean dua vao danh sach bean co san va so luong can tach
        /// </summary>
        /// <param name="beanToSplits"></param>
        /// <param name="requireAmount"></param>
        /// <param name="beforeUpdateList"></param>
        /// <param name="insertList"></param>
        /// <param name="updateList"></param>
        /// <param name="listToUse"></param>
        private void SplitBean(List<HIS_MEDICINE_BEAN> beanToSplits, ExpMedicineTypeSDO requireToExp, List<HIS_MEDICINE_BEAN> detachList, Dictionary<ExpMedicineTypeSDO, List<HIS_MEDICINE_BEAN>> dicLock)
        {
            if (requireToExp.ExpMestMetyReqId == 2397)
            {

            }
            if (!IsNotNullOrEmpty(beanToSplits))
            {
                return;
            }
            decimal requireAmount = requireToExp.Amount;
            decimal existsAmount = beanToSplits.Sum(o => o.AMOUNT);

            //Neu so luong thuoc co trong kho < so luong thuoc yeu cau xuat
            if (existsAmount < requireAmount)
            {
                V_HIS_MEDICINE_TYPE medicineType = new HisMedicineTypeGet().GetViewById(beanToSplits[0].TDL_MEDICINE_TYPE_ID);

                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMedicineBean_ThuocKhaDungKhongDuDeThucHien, medicineType.MEDICINE_TYPE_NAME, existsAmount.ToString(), medicineType.SERVICE_UNIT_NAME, requireAmount.ToString(), medicineType.SERVICE_UNIT_NAME);
                throw new Exception();
            }

            //Sap xep uu tien theo HSD, thoi gian nhap va so luong ton de tao bean
            //neu cau hinh uu tien theo thoi gian nhap
            if (HisMediStockCFG.EXPORT_OPTION == (int)HisMediStockCFG.ExportOption.IMP_TIME)
            {
                beanToSplits = beanToSplits
                .OrderBy(o => !o.EXP_MEST_MEDICINE_ID.HasValue)//Uu tien cac bean co exp_mest_medicine_id truoc (xay ra trong truong hop sua phieu xuat)
                .ThenBy(o => o.TDL_MEDICINE_IMP_TIME)
                .ThenBy(o => o.TDL_MEDICINE_EXPIRED_DATE.HasValue)//co hsd se bi xep sau, uu tien ko co HSD
                .ThenBy(o => o.TDL_MEDICINE_EXPIRED_DATE)
                .ThenBy(o => o.AMOUNT).ToList();
            }
            else
            {
                beanToSplits = beanToSplits
                .OrderBy(o => !o.EXP_MEST_MEDICINE_ID.HasValue)//Uu tien cac bean co exp_mest_medicine_id truoc (xay ra trong truong hop sua phieu xuat)
                .ThenBy(o => o.TDL_MEDICINE_EXPIRED_DATE.HasValue) //co hsd se bi xep sau, uu tien ko co HSD
                .ThenBy(o => o.TDL_MEDICINE_EXPIRED_DATE)
                .ThenBy(o => o.TDL_MEDICINE_IMP_TIME)
                .ThenBy(o => o.AMOUNT).ToList();
            }

            decimal leftAmount = requireAmount;
            int i = 0;

            Mapper.CreateMap<HIS_MEDICINE_BEAN, HIS_MEDICINE_BEAN>();
            while (leftAmount > 0)
            {
                if (beanToSplits[i].AMOUNT <= 0)
                {
                    i++;
                    continue;
                }
                HIS_MEDICINE_BEAN toUpdateDto = Mapper.Map<HIS_MEDICINE_BEAN>(beanToSplits[i]);

                /*
                 * Neu leftAmount > beanToExps[0].AMOUNT thi thuc hien tach medicine_bean.
                 * Medicine_bean duoc tao moi la medicine_bean duoc xuat (bi khoa).
                 * Neu leftAmount <= beanToExps[0].AMOUNT thi thuc hien xuat medicine_bean co san (bi khoa)
                 */
                decimal splitAmount = 0;
                if (beanToSplits[i].AMOUNT > leftAmount)
                {
                    if (requireToExp.DetachKeys == null) requireToExp.DetachKeys = new List<string>();
                    toUpdateDto.DETACH_KEY = Guid.NewGuid().ToString();//key de lay lai du lieu tu DB
                    toUpdateDto.DETACH_AMOUNT = leftAmount;
                    splitAmount = leftAmount;
                    beanToSplits[i].AMOUNT = beanToSplits[i].AMOUNT - leftAmount;
                    requireToExp.DetachKeys.Add(toUpdateDto.DETACH_KEY);
                    detachList.Add(toUpdateDto);
                }
                else
                {
                    splitAmount = beanToSplits[i].AMOUNT;
                    beanToSplits[i].AMOUNT = 0;
                    if (!dicLock.ContainsKey(requireToExp)) dicLock[requireToExp] = new List<HIS_MEDICINE_BEAN>();
                    dicLock[requireToExp].Add(toUpdateDto);
                }

                leftAmount = leftAmount - splitAmount;
                i++;
            }
        }

        /// <summary>
        /// Tao moi bean
        /// </summary>
        /// <param name="insertList"></param>
        private void DetachBean(List<HIS_MEDICINE_BEAN> detachList, List<ExpMedicineTypeSDO> data, Dictionary<ExpMedicineTypeSDO, List<HIS_MEDICINE_BEAN>> dicToUse, List<long> rencentLockedBeans)
        {
            if (IsNotNullOrEmpty(detachList))
            {
                Mapper.CreateMap<HIS_MEDICINE_BEAN, THisMedicineBean>();
                List<THisMedicineBean> input = Mapper.Map<List<THisMedicineBean>>(detachList);
                THisMedicineBean[] sereServArray = new THisMedicineBean[input.Count];
                for (int i = 0; i < input.Count; i++)
                {
                    sereServArray[i] = input[i];
                    sereServArray[i].MODIFIER = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    sereServArray[i].APP_MODIFIER = Constant.APPLICATION_CODE;
                }

                TMedicineBean tMedicineBean = new TMedicineBean();
                tMedicineBean.MedicineBeanArray = sereServArray;

                string storedSql = "PKG_DETACH_MEDICINE_BEAN.PRO_EXECUTE";

                OracleParameter param1 = SqlExecuteUnmanaged.CreateCustomTypeArrayInputParameter<TMedicineBean>("P_MEDICINE_BEAN", "HIS_RS.T_MEDICINE_BEAN", tMedicineBean, ParameterDirection.InputOutput);
                OracleParameter param2 = new OracleParameter("P_RESULT", OracleDbType.Int32, ParameterDirection.Output);

                object resultHolder = null;

                if (!DAOWorker.SqlDAO.ExecuteStoredUnmanaged(OutputHandler, ref resultHolder, storedSql, param1, param2))
                {
                    throw new Exception("Update medicine_bean that bai" + LogUtil.TraceData("input", input));
                }

                if (resultHolder == null || !((BeanResultHolder)resultHolder).IsSuccess)
                {
                    throw new Exception("detach medicine_bean that bai");
                }

                HisMedicineBeanFilterQuery filter = new HisMedicineBeanFilterQuery();
                filter.DETACH_KEYs = detachList.Select(o => o.DETACH_KEY).ToList();
                var result = new HisMedicineBeanGet().Get(filter);

                if (IsNotNullOrEmpty(result))
                {
                    if (dicToUse == null)
                    {
                        dicToUse = new Dictionary<ExpMedicineTypeSDO, List<HIS_MEDICINE_BEAN>>();
                    }
                    foreach (HIS_MEDICINE_BEAN bean in result)
                    {
                        ExpMedicineTypeSDO sdo = data.FirstOrDefault(o => IsNotNullOrEmpty(o.DetachKeys) && o.DetachKeys.Contains(bean.DETACH_KEY));
                        if (!dicToUse.ContainsKey(sdo)) dicToUse[sdo] = new List<HIS_MEDICINE_BEAN>();
                        dicToUse[sdo].Add(bean);
                    }
                    rencentLockedBeans.AddRange(result.Select(o => o.ID).ToList());
                }
            }
        }

        //Xu ly ket qua tra ve khi goi procedure
        private void OutputHandler(ref object resultHolder, OracleDataReader dataReader, params OracleParameter[] parameters)
        {
            try
            {
                BeanResultHolder rs = new BeanResultHolder();
                if (parameters[1] != null && parameters[1].Value != null && parameters[1].Value != DBNull.Value)
                {
                    rs.IsSuccess = Int32.Parse(parameters[1].Value.ToString()) == Constant.IS_TRUE;
                }

                if (rs.IsSuccess && parameters[0] != null && parameters[0].Value != null && parameters[0].Value != DBNull.Value)
                {
                    TMedicineBean sereServ = (TMedicineBean)parameters[0].Value;
                    rs.Data = sereServ.MedicineBeanArray;
                }
                resultHolder = rs;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        /// <summary>
        /// Cap nhat bean co san
        /// </summary>
        /// <param name="updateList"></param>
        private void LockBean(Dictionary<ExpMedicineTypeSDO, List<HIS_MEDICINE_BEAN>> dicLock, Dictionary<ExpMedicineTypeSDO, List<HIS_MEDICINE_BEAN>> dicToUse, List<long> rencentLockedBeanIds)
        {
            if (IsNotNullOrEmpty(dicLock))
            {
                List<HIS_MEDICINE_BEAN> toLocks = new List<HIS_MEDICINE_BEAN>();
                foreach (List<HIS_MEDICINE_BEAN> listLock in dicLock.Values)
                {
                    //Ko lay cac bean co exp_mest_medicine_id, do cac bean nay da thuoc phieu xuat va dang bi khoa
                    toLocks.AddRange(listLock);
                }
                List<long> toLockIds = toLocks.Where(t => !t.EXP_MEST_MEDICINE_ID.HasValue).Select(s => s.ID).ToList();
                if (!this.hisMedicineBeanLock.Run(toLockIds))
                {
                    throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                }
                if (dicToUse == null)
                {
                    dicToUse = new Dictionary<ExpMedicineTypeSDO, List<HIS_MEDICINE_BEAN>>();
                }
                foreach (var dic in dicLock)
                {
                    if (dicToUse.ContainsKey(dic.Key)) dicToUse[dic.Key].AddRange(dic.Value);
                    else dicToUse[dic.Key] = dic.Value;
                }
                toLocks.ForEach(o => o.IS_ACTIVE = 0);

                rencentLockedBeanIds.AddRange(toLockIds);
            }
        }

        /// <summary>
        /// Lay danh sach medicine_bean tuong ung voi danh sach medicine_type
        /// </summary>
        /// <param name="data"></param>
        /// <param name="mediStockId"></param>
        /// <returns></returns>
        private List<HIS_MEDICINE_BEAN> GetMedicineBeanToSplit(List<ExpMedicineTypeSDO> data, long mediStockId, List<long> notUseBeandIds, long? expiredDate)
        {
            List<long> expMestMedicineIds = new List<long>();
            foreach (ExpMedicineTypeSDO t in data)
            {
                if (IsNotNullOrEmpty(t.ExpMestMedicineIds))
                {
                    expMestMedicineIds.AddRange(t.ExpMestMedicineIds);
                }
            }

            if (expiredDate.HasValue)
            {
                try
                {
                    //Lay dau ngay de check HSD (vi thuoc khi nhap kho, HSD ko luu gio, phut, giay
                    expiredDate = Inventec.Common.DateTime.Get.StartDay(expiredDate.Value);
                }
                catch (Exception ex)
                {
                    LogSystem.Error(ex);
                }
            }

            HisMedicineBeanFilterQuery medicineBeanFilter = new HisMedicineBeanFilterQuery();
            medicineBeanFilter.MEDICINE_TYPE_IDs = data.Select(o => o.MedicineTypeId).ToList();
            medicineBeanFilter.MEDI_STOCK_ID = mediStockId;
            medicineBeanFilter.ACTIVE__OR__EXP_MEST_MEDICINE_IDs = expMestMedicineIds; //luu y: filter nay ko duoc phep truyen vao null
            medicineBeanFilter.MEDICINE_IS_ACTIVE = IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE;
            medicineBeanFilter.NOT_IN_IDs = notUseBeandIds;
            medicineBeanFilter.EXPIRED_DATE_NULl__OR__GREATER_THAN__OR__EQUAL = expiredDate;

            return new HisMedicineBeanGet().Get(medicineBeanFilter);
        }

        /// <summary>
        /// Rollback du lieu
        /// </summary>
        internal void RollBack()
        {
            //Chi can rollback bang cach mo khoa cac bean dang bi lock
            if (IsNotNullOrEmpty(this.recentLockedBeanIds))
            {
                if (!new HisMedicineBeanUnlock(param).Run(this.recentLockedBeanIds))
                {
                    LogSystem.Error("Mo khoa thong tin Medicine_bean that bai. Can kiem tra lai log.");
                }
                this.recentLockedBeanIds = null;
            }
        }
    }
}
