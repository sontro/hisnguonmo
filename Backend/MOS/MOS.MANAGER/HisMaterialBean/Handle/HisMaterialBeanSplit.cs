using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisMaterial;
using MOS.MANAGER.HisMaterialBean.Update;
using MOS.MANAGER.HisMaterialPaty;
using MOS.MANAGER.HisMaterialType;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMaterialBean.Handle
{
    //can bo sung code check khoa
    class HisMaterialBeanSplit : BusinessBase
    {
        private List<long> recentLockedBeanIds = new List<long>();

        private HisMaterialBeanUpdate hisMaterialBeanUpdate;
        private HisMaterialBeanLock hisMaterialBeanLock;

        internal HisMaterialBeanSplit()
            : base()
        {
            this.Init();
        }

        internal HisMaterialBeanSplit(CommonParam paramSplit)
            : base(paramSplit)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisMaterialBeanUpdate = new HisMaterialBeanUpdate(param);
            this.hisMaterialBeanLock = new HisMaterialBeanLock(param);
        }

        /// <summary>
        /// Xy ly tach material_bean dua theo material_type
        /// </summary>
        /// <param name="hisMaterialDTOs"></param>
        internal bool SplitByMaterialType(List<ExpMaterialTypeSDO> data, long mediStockId, long? expiredDate, List<HIS_MATERIAL_BEAN> userMaterialBeans, List<long> notUseBeandIds, ref List<HIS_MATERIAL_BEAN> listToUse, ref List<HIS_MATERIAL_PATY> materialPaties)
        {
            bool result = false;
            try
            {
                bool valid = true;

                HisMaterialBeanCheck checker = new HisMaterialBeanCheck(param);
                valid = valid && IsNotNullOrEmpty(data);
                valid = valid && checker.IsValidRequireAmount(data);
                if (valid)
                {
                    List<HIS_MATERIAL_BEAN> materialBeans = null;
                    if (IsNotNullOrEmpty(userMaterialBeans))
                    {
                        materialBeans = userMaterialBeans;
                    }
                    else
                    {
                        materialBeans = this.GetMaterialBeanToSplit(data, mediStockId, notUseBeandIds, expiredDate);
                    }
                    if (!IsNotNullOrEmpty(materialBeans))
                    {
                        List<string> typeNames = HisMaterialTypeCFG.DATA
                            .Where(o => data.Exists(t => t.MaterialTypeId == o.ID))
                            .Select(o => o.MATERIAL_TYPE_NAME)
                            .ToList();
                        string typeNameStr = string.Join(",", typeNames);
                        if (expiredDate.HasValue)
                        {
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMaterialBean_KhongDuKhaDungHetHanSuDung, typeNameStr);
                        }
                        else
                        {
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMaterialBean_KhongDuKhaDung, typeNameStr);
                        }
                        return false;
                    }

                    //Neu trong y/c tach ben co truyen vao doi tuong thanh toan va cac bean trong kho ton tai bean 
                    //ko co cau hinh "ban bang gia nhap" thi lay chinh sach gia, phuc vu kiem tra chinh sach gia
                    List<long> materialIds = materialBeans
                        .Where(o => o.TDL_IS_SALE_EQUAL_IMP_PRICE != MOS.UTILITY.Constant.IS_TRUE
                            && data.Exists(t => t.PatientTypeId.HasValue && t.MaterialTypeId == o.TDL_MATERIAL_TYPE_ID))
                        .Select(o => o.MATERIAL_ID).Distinct().ToList();

                    List<HIS_MATERIAL_PATY> paties = null;
                    if (IsNotNullOrEmpty(materialIds))
                    {
                        List<long> patientTypeIds = data
                        .Where(o => o.PatientTypeId.HasValue)
                        .Select(o => o.PatientTypeId.Value).Distinct().ToList();

                        HisMaterialPatyFilterQuery filter = new HisMaterialPatyFilterQuery();
                        filter.MATERIAL_IDs = materialIds;
                        filter.PATIENT_TYPE_IDs = patientTypeIds;
                        paties = new HisMaterialPatyGet().Get(filter);
                    }

                    materialPaties = paties;

                    List<HIS_MATERIAL_BEAN> lockList = new List<HIS_MATERIAL_BEAN>();
                    List<HIS_MATERIAL_BEAN> detachList = new List<HIS_MATERIAL_BEAN>();

                    foreach (ExpMaterialTypeSDO requireToExp in data)
                    {
                        //Lay ra cac bean thuoc loai y/c tach
                        //Trong trường hợp y/c tách có thông tin đối tượng thanh toán thì kiểm tra thông tin chính sách giá:
                        // - Bean phải thuộc lô thuốc được cấu hình bán bằng giá nhập
                        // - Hoặc phải có chính sách giá trong material_paty
                        List<HIS_MATERIAL_BEAN> beanToSplits = materialBeans
                            .Where(o => o.TDL_MATERIAL_TYPE_ID == requireToExp.MaterialTypeId &&
                                (!requireToExp.PatientTypeId.HasValue
                                || o.TDL_IS_SALE_EQUAL_IMP_PRICE == MOS.UTILITY.Constant.IS_TRUE
                                || (paties != null && paties.Exists(t => t.MATERIAL_ID == o.MATERIAL_ID && t.PATIENT_TYPE_ID == requireToExp.PatientTypeId.Value))))
                            .ToList();
                        if (!IsNotNullOrEmpty(beanToSplits))
                        {
                            V_HIS_MATERIAL_TYPE materialType = new HisMaterialTypeGet().GetViewById(requireToExp.MaterialTypeId);
                            if (expiredDate.HasValue)
                            {
                                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMaterialBean_ThuocKhaDungKhongDuHetHanSuDungDeThucHien, materialType.MATERIAL_TYPE_NAME, "0", materialType.SERVICE_UNIT_NAME, requireToExp.Amount.ToString(), materialType.SERVICE_UNIT_NAME);
                            }
                            else
                            {
                                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMaterialBean_ThuocKhaDungKhongDuDeThucHien, materialType.MATERIAL_TYPE_NAME, "0", materialType.SERVICE_UNIT_NAME, requireToExp.Amount.ToString(), materialType.SERVICE_UNIT_NAME);
                            }
                            throw new Exception("Rollback du lieu");
                        }
                        this.SplitBean(beanToSplits, requireToExp.Amount, requireToExp.PriorityMaterialId, detachList, lockList);
                    }

                    listToUse = new List<HIS_MATERIAL_BEAN>();

                    //Tach material_bean
                    this.DetachBean(detachList, listToUse, this.recentLockedBeanIds);

                    //Khoa material_bean
                    this.LockBean(lockList, listToUse, this.recentLockedBeanIds);

                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                listToUse = null;
                this.RollBack();
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal bool SplitAndDecreaseByMaterialType(List<ExpMaterialTypeSDO> data, long mediStockId, long? expiredDate, List<long> notUseBeandIds, ref List<HIS_MATERIAL_BEAN> listToUse, ref List<HIS_MATERIAL_PATY> materialPaties)
        {
            bool result = false;
            try
            {
                bool valid = true;

                HisMaterialBeanCheck checker = new HisMaterialBeanCheck(param);
                valid = valid && IsNotNullOrEmpty(data);
                valid = valid && checker.IsValidRequireAmount(data);
                if (valid)
                {
                    List<HIS_MATERIAL_BEAN> useMaterialBeans = this.GetMaterialBeanToSplit(data, mediStockId, notUseBeandIds, expiredDate);

                    Dictionary<long, decimal> dicMaterialBean = new Dictionary<long, decimal>();
                    if (IsNotNullOrEmpty(useMaterialBeans))
                    {
                        var Groups = useMaterialBeans.GroupBy(g => g.TDL_MATERIAL_TYPE_ID);
                        foreach (var group in Groups)
                        {
                            dicMaterialBean[group.Key] = group.Sum(s => s.AMOUNT);
                        }
                    }

                    foreach (ExpMaterialTypeSDO sdo in data)
                    {
                        if (dicMaterialBean.ContainsKey(sdo.MaterialTypeId))
                        {
                            decimal availAmount = dicMaterialBean[sdo.MaterialTypeId];
                            if (availAmount <= 0)
                            {
                                sdo.Amount = 0;
                                continue;
                            }
                            if (availAmount >= sdo.Amount)
                            {
                                dicMaterialBean[sdo.MaterialTypeId] = availAmount - sdo.Amount;
                                continue;
                            }
                            else
                            {
                                sdo.Amount = availAmount;
                                dicMaterialBean[sdo.MaterialTypeId] = 0;
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
                    result = this.SplitByMaterialType(data, mediStockId, expiredDate, useMaterialBeans, null, ref listToUse, ref materialPaties);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                listToUse = null;
                param.HasException = true;
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Xy ly tach material_bean dua theo thong tin material
        /// </summary>
        /// <param name="hisMaterialDTOs"></param>
        internal bool SplitByMaterial(List<ExpMaterialSDO> data, long mediStockId, ref List<HIS_MATERIAL_BEAN> listToUse)
        {
            List<HIS_MATERIAL_PATY> materialPaties = null;
            return this.SplitByMaterial(data, mediStockId, null, ref listToUse, ref materialPaties);
        }

        /// <summary>
        /// Xy ly tach material_bean dua theo thong tin material
        /// </summary>
        /// <param name="hisMaterialDTOs"></param>
        internal bool SplitByMaterial(List<ExpMaterialSDO> data, long mediStockId, List<long> notUseBeandIds, ref List<HIS_MATERIAL_BEAN> listToUse, ref List<HIS_MATERIAL_PATY> materialPaties)
        {
            bool result = false;
            try
            {
                bool valid = true;
                List<HIS_MATERIAL_BEAN> materialBeans = this.GetMaterialBeanToSplit(data, mediStockId, notUseBeandIds);
                HisMaterialBeanCheck checker = new HisMaterialBeanCheck(param);
                valid = valid && IsNotNullOrEmpty(data);
                valid = valid && checker.IsValidRequireAmount(data);
                if (valid)
                {
                    //Neu trong y/c tach ben co truyen vao doi tuong thanh toan va cac bean trong kho ton tai bean 
                    //ko co cau hinh "ban bang gia nhap" thi lay chinh sach gia, phuc vu kiem tra chinh sach gia
                    List<long> materialIds = materialBeans
                        .Where(o => o.TDL_IS_SALE_EQUAL_IMP_PRICE != MOS.UTILITY.Constant.IS_TRUE
                            && data.Exists(t => t.PatientTypeId.HasValue && t.MaterialId == o.MATERIAL_ID))
                        .Select(o => o.MATERIAL_ID).Distinct().ToList();

                    List<HIS_MATERIAL_PATY> paties = null;
                    if (IsNotNullOrEmpty(materialIds))
                    {
                        List<long> patientTypeIds = data
                        .Where(o => o.PatientTypeId.HasValue)
                        .Select(o => o.PatientTypeId.Value).Distinct().ToList();

                        HisMaterialPatyFilterQuery filter = new HisMaterialPatyFilterQuery();
                        filter.MATERIAL_IDs = materialIds;
                        filter.PATIENT_TYPE_IDs = patientTypeIds;
                        paties = new HisMaterialPatyGet().Get(filter);
                    }

                    materialPaties = paties;

                    List<HIS_MATERIAL_BEAN> lockList = new List<HIS_MATERIAL_BEAN>();
                    List<HIS_MATERIAL_BEAN> detachList = new List<HIS_MATERIAL_BEAN>();

                    foreach (ExpMaterialSDO requireToExp in data)
                    {
                        //Lay ra cac bean thuoc loai y/c tach
                        //Trong trường hợp y/c tách có thông tin đối tượng thanh toán thì kiểm tra thông tin chính sách giá:
                        // - Bean phải thuộc lô thuốc được cấu hình bán bằng giá nhập
                        // - Hoặc phải có chính sách giá trong material_paty
                        List<HIS_MATERIAL_BEAN> beanToSplits = materialBeans
                            .Where(o => o.MATERIAL_ID == requireToExp.MaterialId &&
                                (!requireToExp.PatientTypeId.HasValue
                                || o.TDL_IS_SALE_EQUAL_IMP_PRICE == MOS.UTILITY.Constant.IS_TRUE
                                || (paties != null && paties.Exists(t => t.MATERIAL_ID == o.MATERIAL_ID && t.PATIENT_TYPE_ID == requireToExp.PatientTypeId.Value))))
                            .ToList();

                        if (!IsNotNullOrEmpty(beanToSplits))
                        {
                            V_HIS_MATERIAL material = new HisMaterialGet().GetViewById(requireToExp.MaterialId);
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMaterialBean_ThuocKhaDungKhongDuDeThucHien, material.MATERIAL_TYPE_NAME, "0", material.SERVICE_UNIT_NAME, requireToExp.Amount.ToString(), material.SERVICE_UNIT_NAME);
                            throw new Exception("Loai thuoc " + material.MATERIAL_TYPE_NAME + " khong du de xuat. So luong ton: " + 0 + "; So luong yeu cau xuat: " + requireToExp.Amount);
                        }
                        this.SplitBean(beanToSplits, requireToExp.Amount, null, detachList, lockList);
                    }

                    listToUse = new List<HIS_MATERIAL_BEAN>();

                    this.DetachBean(detachList, listToUse, this.recentLockedBeanIds); //detach: de tách bean từ bean cũ
                    this.LockBean(lockList, listToUse, this.recentLockedBeanIds); //update: để khóa bean (nhằm chiếm dụng bean đó)

                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                listToUse = null;
                this.RollBack();
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
        /// <param name="priorityMaterialId">Lo uu tien</param>
        private void SplitBean(List<HIS_MATERIAL_BEAN> beanToSplits, decimal requireAmount, long? priorityMaterialId, List<HIS_MATERIAL_BEAN> detachList, List<HIS_MATERIAL_BEAN> lockList)
        {
            if (!IsNotNullOrEmpty(beanToSplits))
            {
                return;
            }

            decimal existsAmount = beanToSplits.Sum(o => o.AMOUNT);

            //Neu so luong thuoc co trong kho < so luong thuoc yeu cau xuat
            if (existsAmount < requireAmount)
            {
                V_HIS_MATERIAL_TYPE materialType = new HisMaterialTypeGet().GetViewById(beanToSplits[0].TDL_MATERIAL_TYPE_ID);

                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMaterialBean_ThuocKhaDungKhongDuDeThucHien, materialType.MATERIAL_TYPE_NAME, existsAmount.ToString(), materialType.SERVICE_UNIT_NAME, requireAmount.ToString(), materialType.SERVICE_UNIT_NAME);
                throw new Exception("Loai thuoc " + materialType.MATERIAL_TYPE_NAME + " khong du de xuat. So luong ton: " + existsAmount + "; So luong yeu cau xuat: " + requireAmount);
            }

            //Sap xep uu tien theo HSD, thoi gian nhap va so luong ton de tao bean
            //neu cau hinh uu tien theo thoi gian nhap
            if (HisMediStockCFG.EXPORT_OPTION == (int)HisMediStockCFG.ExportOption.IMP_TIME)
            {
                beanToSplits = beanToSplits
                .OrderBy(o => !o.EXP_MEST_MATERIAL_ID.HasValue)//Uu tien cac bean co exp_mest_material_id truoc (xay ra trong truong hop sua phieu xuat)
                .ThenBy(o => !(o.MATERIAL_ID == priorityMaterialId))//Uu tien lo duoc uu tien
                .ThenBy(o => o.TDL_MATERIAL_IMP_TIME)
                .ThenBy(o => o.TDL_MATERIAL_EXPIRED_DATE.HasValue)//co hsd se bi xep sau, uu tien ko co HSD
                .ThenBy(o => o.TDL_MATERIAL_EXPIRED_DATE)
                .ThenBy(o => o.AMOUNT).ToList();
            }
            else
            {
                beanToSplits = beanToSplits
                .OrderBy(o => !o.EXP_MEST_MATERIAL_ID.HasValue)//Uu tien cac bean co exp_mest_material_id truoc (xay ra trong truong hop sua phieu xuat)
                .ThenBy(o => !(o.MATERIAL_ID == priorityMaterialId))//Uu tien lo duoc uu tien
                .ThenBy(o => o.TDL_MATERIAL_EXPIRED_DATE.HasValue) //co hsd se bi xep sau, uu tien ko co HSD
                .ThenBy(o => o.TDL_MATERIAL_EXPIRED_DATE)
                .ThenBy(o => o.TDL_MATERIAL_IMP_TIME)
                .ThenBy(o => o.AMOUNT).ToList();
            }

            decimal leftAmount = requireAmount;
            int i = 0;

            Mapper.CreateMap<HIS_MATERIAL_BEAN, HIS_MATERIAL_BEAN>();
            while (leftAmount > 0)
            {
                HIS_MATERIAL_BEAN toUpdateDto = Mapper.Map<HIS_MATERIAL_BEAN>(beanToSplits[i]);

                /*
                 * Neu leftAmount > beanToExps[0].AMOUNT thi thuc hien tach material_bean.
                 * Material_bean duoc tao moi la material_bean duoc xuat (bi khoa).
                 * Neu leftAmount <= beanToExps[0].AMOUNT thi thuc hien xuat material_bean co san (bi khoa)
                 */
                if (beanToSplits[i].AMOUNT > leftAmount)
                {
                    toUpdateDto.DETACH_KEY = Guid.NewGuid().ToString();//key de lay lai du lieu tu DB
                    toUpdateDto.DETACH_AMOUNT = leftAmount;
                    detachList.Add(toUpdateDto);
                }
                else
                {
                    lockList.Add(toUpdateDto);
                }

                leftAmount = leftAmount - beanToSplits[i].AMOUNT;
                i++;
            }
        }

        /// <summary>
        /// Tao moi bean
        /// </summary>
        /// <param name="insertList"></param>
        private void DetachBean(List<HIS_MATERIAL_BEAN> detachList, List<HIS_MATERIAL_BEAN> listToUse, List<long> rencentLockedBeans)
        {
            if (IsNotNullOrEmpty(detachList))
            {
                if (!this.hisMaterialBeanUpdate.UpdateListNoCheckLock(detachList))
                {
                    throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                }

                HisMaterialBeanFilterQuery filter = new HisMaterialBeanFilterQuery();
                filter.DETACH_KEYs = detachList.Select(o => o.DETACH_KEY).ToList();
                var result = new HisMaterialBeanGet().Get(filter);

                if (IsNotNullOrEmpty(result))
                {
                    if (listToUse == null)
                    {
                        listToUse = new List<HIS_MATERIAL_BEAN>();
                    }
                    listToUse.AddRange(result);

                    rencentLockedBeans.AddRange(result.Select(o => o.ID).ToList());
                }
            }
        }

        /// <summary>
        /// Cap nhat bean co san
        /// </summary>
        /// <param name="updateList"></param>
        private void LockBean(List<HIS_MATERIAL_BEAN> lockList, List<HIS_MATERIAL_BEAN> listToUse, List<long> rencentLockedBeanIds)
        {
            if (IsNotNullOrEmpty(lockList))
            {
                //Ko lay cac bean co exp_mest_material_id, do cac bean nay da thuoc phieu xuat va dang bi khoa
                List<long> toLockIds = lockList.Where(t => !t.EXP_MEST_MATERIAL_ID.HasValue).Select(o => o.ID).ToList();
                if (!this.hisMaterialBeanLock.Run(toLockIds))
                {
                    throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                }
                if (listToUse == null)
                {
                    listToUse = new List<HIS_MATERIAL_BEAN>();
                }
                lockList.ForEach(o => o.IS_ACTIVE = 0);
                listToUse.AddRange(lockList);

                rencentLockedBeanIds.AddRange(toLockIds);
            }
        }

        /// <summary>
        /// Lay danh sach material_bean tuong ung voi danh sach material_type
        /// </summary>
        /// <param name="data"></param>
        /// <param name="mediStockId"></param>
        /// <returns></returns>
        private List<HIS_MATERIAL_BEAN> GetMaterialBeanToSplit(List<ExpMaterialTypeSDO> data, long mediStockId, List<long> notInBeandIds, long? expiredDate)
        {
            List<long> expMestMaterialIds = new List<long>();
            foreach (ExpMaterialTypeSDO t in data)
            {
                if (IsNotNullOrEmpty(t.ExpMestMaterialIds))
                {
                    expMestMaterialIds.AddRange(t.ExpMestMaterialIds);
                }
            }

            if (expiredDate.HasValue)
            {
                try
                {
                    //Lay dau ngay de check HSD (vi vat tu khi nhap kho, HSD ko luu gio, phut, giay
                    expiredDate = Inventec.Common.DateTime.Get.StartDay(expiredDate.Value);
                }
                catch (Exception ex)
                {
                    LogSystem.Error(ex);
                }
            }

            HisMaterialBeanFilterQuery materialBeanFilter = new HisMaterialBeanFilterQuery();
            materialBeanFilter.MATERIAL_TYPE_IDs = data.Select(o => o.MaterialTypeId).ToList();
            materialBeanFilter.MEDI_STOCK_ID = mediStockId;
            materialBeanFilter.ACTIVE__OR__EXP_MEST_MATERIAL_IDs = expMestMaterialIds; //luu y: filter nay ko duoc phep truyen vao null
            materialBeanFilter.MATERIAL_IS_ACTIVE = IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE;
            materialBeanFilter.NOT_IN_IDs = notInBeandIds;
            materialBeanFilter.EXPIRED_DATE_NULl__OR__GREATER_THAN__OR__EQUAL = expiredDate;

            return new HisMaterialBeanGet().Get(materialBeanFilter);
        }

        /// <summary>
        /// Lay danh sach material_bean tuong ung voi danh sach material
        /// </summary>
        /// <param name="data"></param>
        /// <param name="mediStockId"></param>
        /// <returns></returns>
        private List<HIS_MATERIAL_BEAN> GetMaterialBeanToSplit(List<ExpMaterialSDO> data, long mediStockId, List<long> notInBeandIds)
        {
            List<long> expMestMaterialIds = new List<long>();
            foreach (ExpMaterialSDO t in data)
            {
                if (IsNotNullOrEmpty(t.ExpMestMaterialIds))
                {
                    expMestMaterialIds.AddRange(t.ExpMestMaterialIds);
                }
            }

            HisMaterialBeanFilterQuery materialBeanFilter = new HisMaterialBeanFilterQuery();
            materialBeanFilter.MATERIAL_IDs = data.Select(o => o.MaterialId).ToList();
            materialBeanFilter.MEDI_STOCK_ID = mediStockId;
            materialBeanFilter.ACTIVE__OR__EXP_MEST_MATERIAL_IDs = expMestMaterialIds; //luu y: filter nay ko duoc phep truyen vao null
            materialBeanFilter.MATERIAL_IS_ACTIVE = IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE;
            materialBeanFilter.NOT_IN_IDs = notInBeandIds;

            return new HisMaterialBeanGet().Get(materialBeanFilter);
        }

        /// <summary>
        /// Rollback du lieu
        /// </summary>
        internal void RollBack()
        {
            //Chi can rollback bang cach mo khoa cac bean dang bi lock
            if (IsNotNullOrEmpty(this.recentLockedBeanIds))
            {
                if (!new HisMaterialBeanUnlock(param).Run(this.recentLockedBeanIds))
                {
                    LogSystem.Error("Mo khoa thong tin Material_bean that bai. Can kiem tra lai log.");
                }
                this.recentLockedBeanIds = null;
            }
        }
    }
}
