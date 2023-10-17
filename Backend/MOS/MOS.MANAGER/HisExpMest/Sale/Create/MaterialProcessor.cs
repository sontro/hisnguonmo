using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisMaterial;
using MOS.MANAGER.HisMaterialBean;
using MOS.MANAGER.HisMaterialBean.Handle;
using MOS.MANAGER.HisMaterialPaty;
using MOS.SDO;
using MOS.UTILITY;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Sale.Create
{
    class MaterialProcessor : BusinessBase
    {
        private HisExpMestMaterialCreate hisExpMestMaterialCreate;
        private List<long> recentMaterialBeanIds = new List<long>();

        internal MaterialProcessor(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.hisExpMestMaterialCreate = new HisExpMestMaterialCreate(param);
        }

        /// <summary>
        /// Kiem tra cac bean ma client gui len co hop le khong. Bean can dam bao:
        /// + Dang khong bi khoa hoac Neu dang bi khoa thi session-key = session-key cua client truyen len
        /// + Tong so luong phai dung voi so luong client gui len
        /// </summary>
        /// <returns></returns>
        internal bool Run(string clientSessionKey, long? patientTypeId, List<long> materialBeanIds, List<ExpMaterialTypeSDO> materials, HIS_EXP_MEST expMest, ref List<HIS_EXP_MEST_MATERIAL> resultData, ref List<string> sqls, AutoEnum en = AutoEnum.NONE, long? axTime = null, string loginname = null, string username = null)
        {
            try
            {
                if (IsNotNullOrEmpty(materials) && expMest != null)
                {
                    string sessionKey = SessionUtil.SessionKey(clientSessionKey);

                    List<HIS_MATERIAL_BEAN> materialBeans = IsNotNullOrEmpty(materialBeanIds) ? new HisMaterialBeanGet().GetByIds(materialBeanIds) : null;
                    if (!IsNotNullOrEmpty(materialBeans))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        LogSystem.Warn("material_bean_id ko hop le");
                        return false;
                    }

                    List<HIS_MATERIAL_BEAN> unavailables = materialBeans
                        .Where(o => o.IS_ACTIVE == MOS.UTILITY.Constant.IS_TRUE && o.SESSION_KEY != sessionKey)
                        .ToList();

                    if (IsNotNullOrEmpty(unavailables))
                    {
                        LogSystem.Warn("Ton tai meidicine_bean khong cho phep lay: co session_key khac session_key client va dang bi khoa" + LogUtil.TraceData("unavailables", unavailables));
                        return false;
                    }

                    //List<long> materialIds = materialBeans.Select(o => o.MATERIAL_ID).ToList();
                    List<HIS_MATERIAL_PATY> materialPaties = null;
                    if (patientTypeId.HasValue && materialBeans.Any(a => !a.TDL_IS_SALE_EQUAL_IMP_PRICE.HasValue || a.TDL_IS_SALE_EQUAL_IMP_PRICE.Value != Constant.IS_TRUE))
                    {
                        materialPaties = new HisMaterialPatyGet().GetAppliedMaterialPaty(materialBeans.Where(a => !a.TDL_IS_SALE_EQUAL_IMP_PRICE.HasValue || a.TDL_IS_SALE_EQUAL_IMP_PRICE.Value != Constant.IS_TRUE).Select(s => s.MATERIAL_ID).ToList(), patientTypeId.Value);
                    }

                    List<HIS_EXP_MEST_MATERIAL> expMestMaterials = new List<HIS_EXP_MEST_MATERIAL>();
                    Dictionary<HIS_EXP_MEST_MATERIAL, List<long>> materialDic = new Dictionary<HIS_EXP_MEST_MATERIAL, List<long>>();

                    //List<HIS_MATERIAL> hisMaterials = new HisMaterialGet().GetByIds(materialIds);

                    var groupSdos = materials.GroupBy(g => g.MaterialTypeId).ToList();

                    //Duyet theo y/c cua client de tao ra exp_mest_material tuong ung
                    foreach (var group in groupSdos)
                    {
                        List<HIS_MATERIAL_BEAN> beans = materialBeans
                            .Where(o => o.TDL_MATERIAL_TYPE_ID == group.Key && o.MEDI_STOCK_ID == expMest.MEDI_STOCK_ID)
                            .ToList();

                        ExpMaterialTypeSDO firstSdo = group.FirstOrDefault();

                        decimal beanAmount = IsNotNullOrEmpty(beans) ? beans.Sum(o => o.AMOUNT) : 0;
                        decimal sdoAmount = group.ToList().Sum(s => s.Amount);

                        //Neu so luong cua bean ko khop so luong do client y/c ==> reject
                        if (sdoAmount != beanAmount)
                        {
                            MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                            LogSystem.Warn("Ton tai meidicine_bean khong hop le: co so luong y/c khac voi tong so luong cua bean. So luong bean: " + beanAmount + LogUtil.TraceData("sdo", group.ToList()));
                            return false;
                        }

                        var groupMaterials = beans.Select(o => new { o.MATERIAL_ID, o.TDL_MATERIAL_TYPE_ID }).Distinct().ToList();
                        foreach (var material in groupMaterials)
                        {
                            List<HIS_MATERIAL_BEAN> usedBeans = beans.Where(o => o.MATERIAL_ID == material.MATERIAL_ID).ToList();
                            HIS_MATERIAL_BEAN first = usedBeans.FirstOrDefault();

                            decimal amount = usedBeans.Sum(o => o.AMOUNT);
                            decimal? discount = null;
                            decimal price = 0;
                            decimal vatRatio = 0;
                            bool isUseClientPrice = false;

                            //neu co thong tin doi tuong 
                            if (firstSdo.Price.HasValue)
                            {
                                price = firstSdo.Price.Value;
                                vatRatio = firstSdo.VatRatio.HasValue ? firstSdo.VatRatio.Value : 0;
                                discount = !firstSdo.DiscountRatio.HasValue ?
                            0 : firstSdo.DiscountRatio.Value * amount * price * (1 + vatRatio);
                                isUseClientPrice = true;
                            }
                            else
                            {
                                if (first.TDL_IS_SALE_EQUAL_IMP_PRICE.HasValue && first.TDL_IS_SALE_EQUAL_IMP_PRICE.Value == UTILITY.Constant.IS_TRUE)
                                {
                                    price = first.TDL_MATERIAL_IMP_PRICE;
                                    vatRatio = first.TDL_MATERIAL_IMP_VAT_RATIO;
                                }
                                else
                                {
                                    HIS_MATERIAL_PATY mp = IsNotNullOrEmpty(materialPaties) && patientTypeId.HasValue ?
                                        materialPaties.Where(o => o.MATERIAL_ID == material.MATERIAL_ID && o.PATIENT_TYPE_ID == patientTypeId.Value).FirstOrDefault() : null;
                                    if (mp == null)
                                    {
                                        V_HIS_MATERIAL m = new HisMaterialGet().GetViewById(material.MATERIAL_ID);
                                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMaterialPaty_KhongTonTaiTuongUngVoiLoaiVatTu, m.MATERIAL_TYPE_NAME);
                                        throw new Exception("Khong co thong tin gia ban tuong ung voi MATERIAL_ID:" + material.MATERIAL_ID + "; patientTypeId:" + patientTypeId);
                                    }
                                    price = mp.EXP_PRICE;
                                    vatRatio = mp.EXP_VAT_RATIO;
                                }
                            }

                            HIS_EXP_MEST_MATERIAL exp = new HIS_EXP_MEST_MATERIAL();
                            exp.EXP_MEST_ID = expMest.ID;
                            exp.AMOUNT = amount;
                            exp.MATERIAL_ID = material.MATERIAL_ID;
                            exp.TDL_MATERIAL_TYPE_ID = material.TDL_MATERIAL_TYPE_ID;
                            exp.NUM_ORDER = firstSdo.NumOrder;
                            exp.PRICE = price;
                            exp.VAT_RATIO = vatRatio;
                            exp.DISCOUNT = discount;
                            exp.DESCRIPTION = firstSdo.Description;
                            exp.TDL_MEDI_STOCK_ID = expMest.MEDI_STOCK_ID;
                            exp.IS_USE_CLIENT_PRICE = isUseClientPrice ? (short?)Constant.IS_TRUE : null;
                            if (en == AutoEnum.APPROVE)
                            {
                                exp.APPROVAL_TIME = axTime;
                                exp.APPROVAL_LOGINNAME = loginname;
                                exp.APPROVAL_USERNAME = username;
                            }
                            else if (en == AutoEnum.APPROVE_EXPORT && expMest.EXP_MEST_STT_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__DONE)
                            {
                                exp.APPROVAL_TIME = axTime;
                                exp.APPROVAL_LOGINNAME = loginname;
                                exp.APPROVAL_USERNAME = username;

                                exp.IS_EXPORT = Constant.IS_TRUE;
                                exp.EXP_TIME = axTime;
                                exp.EXP_LOGINNAME = loginname;
                                exp.EXP_USERNAME = username;
                            }

                            expMestMaterials.Add(exp);
                            materialDic.Add(exp, usedBeans.Select(o => o.ID).ToList());
                        }
                    }

                    if (!this.hisExpMestMaterialCreate.CreateList(expMestMaterials))
                    {
                        throw new Exception("Tao exp_mest_material that bai. Rollback du lieu");
                    }

                    this.SqlUpdateBean(expMest, sessionKey, materialDic, en, ref sqls);

                    resultData = expMestMaterials;
                }
            }
            catch (Exception ex)
            {
                this.Rollback();
                LogSystem.Error(ex);
                return false;
            }
            return true;
        }

        private void SqlUpdateBean(HIS_EXP_MEST expMest, string sessionKey, Dictionary<HIS_EXP_MEST_MATERIAL, List<long>> useBeandIdDic, AutoEnum en, ref List<string> sqls)
        {
            if (IsNotNullOrEmpty(useBeandIdDic))
            {
                if (sqls == null)
                {
                    sqls = new List<string>();
                }

                List<long> materialBeanIds = new List<long>();
                foreach (HIS_EXP_MEST_MATERIAL expMestMaterial in useBeandIdDic.Keys)
                {
                    List<long> beanIds = useBeandIdDic[expMestMaterial];
                    //cap nhat danh sach cac bean da dung
                    if (en == AutoEnum.APPROVE_EXPORT && expMest.EXP_MEST_STT_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__DONE)
                    {
                        string query = DAOWorker.SqlDAO.AddInClause(beanIds, "UPDATE HIS_MATERIAL_BEAN SET SESSION_KEY = NULL, SESSION_TIME = NULL, IS_ACTIVE = 1, EXP_MEST_MATERIAL_ID = NULL, MEDI_STOCK_ID = NULL WHERE %IN_CLAUSE% ", "ID");
                        query = string.Format(query, expMestMaterial.ID);
                        sqls.Add(query);
                    }
                    else
                    {
                        string query = DAOWorker.SqlDAO.AddInClause(beanIds, "UPDATE HIS_MATERIAL_BEAN SET SESSION_KEY = NULL, SESSION_TIME = NULL, IS_ACTIVE = 0, EXP_MEST_MATERIAL_ID = {0} WHERE %IN_CLAUSE% ", "ID");
                        query = string.Format(query, expMestMaterial.ID);
                        sqls.Add(query);
                    }

                    materialBeanIds.AddRange(beanIds);
                }

                //cap nhat danh sach cac bean ko dung
                string query2 = DAOWorker.SqlDAO.AddNotInClause(materialBeanIds, "UPDATE HIS_MATERIAL_BEAN SET SESSION_KEY = NULL, SESSION_TIME = NULL, EXP_MEST_MATERIAL_ID = NULL, IS_ACTIVE = 1 WHERE SESSION_KEY = '{0}' AND %IN_CLAUSE% ", "ID");
                query2 = string.Format(query2, sessionKey);
                sqls.Add(query2);

                this.recentMaterialBeanIds.AddRange(materialBeanIds);
            }
        }

        internal void Rollback()
        {
            if (IsNotNullOrEmpty(this.recentMaterialBeanIds))
            {
                try
                {
                    string sql = DAOWorker.SqlDAO.AddInClause(this.recentMaterialBeanIds, "UPDATE HIS_MATERIAL_BEAN SET SESSION_KEY = NULL, SESSION_TIME = NULL, IS_ACTIVE = 1, EXP_MEST_MATERIAL_ID = NULL WHERE IS_ACTIVE = 0 AND EXP_MEST_MATERIAL_ID IS NOT NULL AND %IN_CLAUSE% ", "ID");
                    if (!DAOWorker.SqlDAO.Execute(sql))
                    {
                        LogSystem.Warn("Rollback du lieu his_medicine_bean that bai");
                    }
                }
                catch (Exception ex)
                {
                    LogSystem.Error(ex);
                    LogSystem.Warn("Rollback du lieu his_medicine_bean that bai");
                }
            }
            this.hisExpMestMaterialCreate.RollbackData();
        }
    }
}
