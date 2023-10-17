using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMatyReq;
using MOS.MANAGER.HisMaterialBean.Handle;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Chms.CreateList
{
    class MaterialProcessor : BusinessBase
    {
        private List<HisExpMestMatyReqMaker> matyReqMakers;
        private List<HisMaterialBeanSplit> beanSplits;
        private List<HisExpMestMaterialCreate> materialCreates;

        internal MaterialProcessor(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.materialCreates = new List<HisExpMestMaterialCreate>();
            this.matyReqMakers = new List<HisExpMestMatyReqMaker>();
            this.beanSplits = new List<HisMaterialBeanSplit>();
        }

        internal bool Run(Dictionary<HIS_EXP_MEST, ExpMestChmsDetailSDO> dicExpMest, ref List<HIS_EXP_MEST_MATY_REQ> expMestMatyReqs, ref List<HIS_EXP_MEST_MATERIAL> materials, ref List<string> sqls)
        {
            bool result = false;
            try
            {
                materials = new List<HIS_EXP_MEST_MATERIAL>();
                expMestMatyReqs = new List<HIS_EXP_MEST_MATY_REQ>();
                foreach (var dic in dicExpMest)
                {
                    List<HIS_EXP_MEST_MATERIAL> approves = null;
                    List<HIS_EXP_MEST_MATY_REQ> requests = null;

                    ExpMestChmsDetailSDO sdo = dic.Value;
                    HIS_EXP_MEST expMest = dic.Key;

                    if (IsNotNullOrEmpty(sdo.MaterialTypes))
                    {
                        HisExpMestMatyReqMaker maker = new HisExpMestMatyReqMaker(param);
                        matyReqMakers.Add(maker);
                        if (!maker.Run(sdo.MaterialTypes, expMest, ref requests))
                        {
                            throw new Exception("hisExpMestMatyReqMaker 1. Ket thuc nghiep vu");
                        }
                        expMestMatyReqs.AddRange(requests);
                    }
                    else if (IsNotNullOrEmpty(sdo.Materials))
                    {
                        List<HIS_MATERIAL_BEAN> materialBeans = null;
                        HisMaterialBeanSplit spliter = new HisMaterialBeanSplit(param);
                        beanSplits.Add(spliter);
                        if (!spliter.SplitByMaterial(sdo.Materials, expMest.MEDI_STOCK_ID, ref materialBeans))
                        {
                            return false;
                        }

                        List<ExpMaterialTypeSDO> materialTypeSdos = this.MakeMaterialTypeSdoByBean(materialBeans, sdo.Materials);

                        HisExpMestMatyReqMaker maker = new HisExpMestMatyReqMaker(param);
                        matyReqMakers.Add(maker);
                        if (!maker.Run(materialTypeSdos, expMest, ref requests))
                        {
                            throw new Exception("hisExpMestMatyReqMaker 2. Ket thuc nghiep vu");
                        }

                        approves = new List<HIS_EXP_MEST_MATERIAL>();
                        Dictionary<HIS_EXP_MEST_MATERIAL, List<long>> materialDic = new Dictionary<HIS_EXP_MEST_MATERIAL, List<long>>();

                        //Duyet theo y/c cua client de tao ra exp_mest_material tuong ung
                        foreach (ExpMaterialSDO mate in sdo.Materials)
                        {
                            List<HIS_MATERIAL_BEAN> beans = materialBeans.Where(o => o.MATERIAL_ID == mate.MaterialId).ToList();
                            if (!IsNotNullOrEmpty(beans))
                            {
                                throw new Exception("Ko co bean tuong ung voi material_id " + mate.MaterialId);
                            }

                            HIS_EXP_MEST_MATERIAL exp = new HIS_EXP_MEST_MATERIAL();
                            exp.EXP_MEST_ID = expMest.ID;
                            exp.AMOUNT = mate.Amount;
                            exp.MATERIAL_ID = mate.MaterialId;
                            exp.NUM_ORDER = mate.NumOrder;
                            exp.DESCRIPTION = mate.Description;
                            exp.PRICE = mate.Price;
                            exp.TDL_MATERIAL_TYPE_ID = beans[0].TDL_MATERIAL_TYPE_ID;
                            exp.VAT_RATIO = mate.VatRatio;
                            exp.PRICE = mate.Price;
                            exp.TDL_MEDI_STOCK_ID = expMest.MEDI_STOCK_ID;

                            var req = requests.FirstOrDefault(o => o.MATERIAL_TYPE_ID == exp.TDL_MATERIAL_TYPE_ID);
                            if (req == null)
                            {
                                throw new Exception("Khong tao duoc HIS_EXP_MEST_MATY_REQ tuong ung voi loai thuoc: " + exp.TDL_MATERIAL_TYPE_ID);
                            }
                            exp.EXP_MEST_MATY_REQ_ID = req.ID;
                            approves.Add(exp);
                            materialDic.Add(exp, beans.Select(o => o.ID).ToList());
                        }


                        HisExpMestMaterialCreate mateCreate = new HisExpMestMaterialCreate(param);
                        this.materialCreates.Add(mateCreate);
                        if (!mateCreate.CreateList(approves))
                        {
                            throw new Exception("Tao exp_mest_material that bai. Rollback du lieu");
                        }

                        this.SqlUpdateBean(materialDic, ref sqls);
                        materials.AddRange(approves);
                        expMestMatyReqs.AddRange(requests);
                    }

                }
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private List<ExpMaterialTypeSDO> MakeMaterialTypeSdoByBean(List<HIS_MATERIAL_BEAN> materialBeans, List<ExpMaterialSDO> materialSdos)
        {
            List<ExpMaterialTypeSDO> typeSdos = new List<ExpMaterialTypeSDO>();
            var Groups = materialBeans.GroupBy(g => g.TDL_MATERIAL_TYPE_ID).ToList();
            foreach (var group in Groups)
            {
                ExpMaterialTypeSDO sdo = new ExpMaterialTypeSDO();
                sdo.Amount = group.Sum(s => s.AMOUNT);
                sdo.MaterialTypeId = group.Key;
                sdo.NumOrder = materialSdos.FirstOrDefault(o => group.Any(a => a.MATERIAL_ID == o.MaterialId)).NumOrder;
                typeSdos.Add(sdo);
            }
            return typeSdos;
        }

        private void SqlUpdateBean(Dictionary<HIS_EXP_MEST_MATERIAL, List<long>> useBeandIdDic, ref List<string> sqls)
        {
            if (IsNotNullOrEmpty(useBeandIdDic))
            {
                if (sqls == null)
                {
                    sqls = new List<string>();
                }

                foreach (HIS_EXP_MEST_MATERIAL expMestMaterial in useBeandIdDic.Keys)
                {
                    List<long> beanIds = useBeandIdDic[expMestMaterial];
                    //cap nhat danh sach cac bean da dung
                    string query = DAOWorker.SqlDAO.AddInClause(beanIds, "UPDATE HIS_MATERIAL_BEAN SET SESSION_KEY = NULL, SESSION_TIME = NULL, IS_ACTIVE = 0, EXP_MEST_MATERIAL_ID = {0} WHERE %IN_CLAUSE% ", "ID");
                    query = string.Format(query, expMestMaterial.ID);
                    sqls.Add(query);
                }
            }
        }

        internal void Rollback()
        {
            if (IsNotNullOrEmpty(this.beanSplits))
            {
                foreach (var spliter in this.beanSplits.AsEnumerable().Reverse())
                {
                    try
                    {
                        spliter.RollBack();
                    }
                    catch (Exception ex)
                    {
                        Inventec.Common.Logging.LogSystem.Error(ex);
                    }
                }
            }

            if (IsNotNullOrEmpty(this.materialCreates))
            {
                foreach (var creater in this.materialCreates.AsEnumerable().Reverse())
                {
                    try
                    {
                        creater.RollbackData();
                    }
                    catch (Exception ex)
                    {
                        Inventec.Common.Logging.LogSystem.Error(ex);
                    }
                }
            }

            if (IsNotNullOrEmpty(this.matyReqMakers))
            {
                foreach (var maker in this.matyReqMakers.AsEnumerable().Reverse())
                {
                    try
                    {
                        maker.Rollback();
                    }
                    catch (Exception ex)
                    {
                        Inventec.Common.Logging.LogSystem.Error(ex);
                    }
                }
            }
        }
    }
}
