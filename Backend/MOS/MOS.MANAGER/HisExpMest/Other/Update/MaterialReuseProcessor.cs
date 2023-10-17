using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisMaterialBean.Handle;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Other.Update
{
    class MaterialReuseProcessor : BusinessBase
    {
        private HisMaterialBeanTakeBySerial hisMaterialBeanTakeBySerial;
        private HisExpMestMaterialCreate hisExpMestMaterialCreate;

        internal MaterialReuseProcessor(CommonParam param)
            : base(param)
        {
            this.hisExpMestMaterialCreate = new HisExpMestMaterialCreate(param);
            this.hisMaterialBeanTakeBySerial = new HisMaterialBeanTakeBySerial(param);
        }


        internal bool Run(HisExpMestOtherSDO sdo, HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MATERIAL> materialOlds, ref List<HIS_EXP_MEST_MATERIAL> resultData, ref List<string> sqls)
        {
            try
            {
                if (expMest != null)
                {
                    List<HIS_EXP_MEST_MATERIAL> inserts = new List<HIS_EXP_MEST_MATERIAL>();
                    List<HIS_EXP_MEST_MATERIAL> deletes = new List<HIS_EXP_MEST_MATERIAL>();
                    List<HIS_EXP_MEST_MATERIAL> updates = new List<HIS_EXP_MEST_MATERIAL>();

                    Dictionary<HIS_EXP_MEST_MATERIAL, List<long>> dicBean = new Dictionary<HIS_EXP_MEST_MATERIAL, List<long>>();

                    Dictionary<HIS_EXP_MEST_MATERIAL, List<long>> newMaterialDic = new Dictionary<HIS_EXP_MEST_MATERIAL, List<long>>();

                    List<HIS_EXP_MEST_MATERIAL> olds = materialOlds != null ? materialOlds.Where(o => !String.IsNullOrWhiteSpace(o.SERIAL_NUMBER)).ToList() : null;

                    List<PresMaterialBySerialNumberSDO> seriNews = sdo.SerialNumbers != null ? sdo.SerialNumbers.Where(o => olds == null || !olds.Any(a => a.SERIAL_NUMBER == o.SerialNumber)).ToList() : null;
                    updates = olds != null ? olds.Where(o => sdo.SerialNumbers != null && sdo.SerialNumbers.Any(a => a.SerialNumber == o.SERIAL_NUMBER)).ToList() : null;
                    deletes = olds != null ? olds.Where(o => updates == null || !updates.Any(a => a.ID == o.ID)).ToList() : null;

                    if (IsNotNullOrEmpty(seriNews))
                    {
                        inserts = this.TakeBeanAndMakeData(seriNews, expMest, ref dicBean);
                    }



                    if (IsNotNullOrEmpty(inserts) && !this.hisExpMestMaterialCreate.CreateList(inserts))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }

                    if (sqls == null)
                    {
                        sqls = new List<string>();
                    }

                    List<long> deleteIds = IsNotNullOrEmpty(deletes) ? deletes.Select(o => o.ID).ToList() : null;
                    //Cap nhat thong tin bean
                    this.SqlUpdateBean(dicBean, deleteIds, ref sqls);

                    //Xoa cac exp_mest_material ko dung.
                    //Luu y: can thuc hien xoa exp_mest_material sau khi da cap nhat bean (tranh bi loi fk)
                    this.SqlDeleteExpMestMaterial(deleteIds, ref sqls);

                    this.PassResult(inserts, updates, ref resultData);
                }
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return false;
            }

        }

        private List<HIS_EXP_MEST_MATERIAL> TakeBeanAndMakeData(List<PresMaterialBySerialNumberSDO> reqMaterials, HIS_EXP_MEST expMest, ref Dictionary<HIS_EXP_MEST_MATERIAL, List<long>> materialDic)
        {
            List<HIS_MATERIAL_BEAN> materialBeans = null;
            List<string> serialNumbers = reqMaterials != null ? reqMaterials.Select(o => o.SerialNumber).ToList() : null;
            if (!IsNotNullOrEmpty(serialNumbers))
            {
                return null;
            }
            if (!this.hisMaterialBeanTakeBySerial.Run(serialNumbers, expMest.MEDI_STOCK_ID, ref materialBeans))
            {
                throw new Exception("Tach bean that bai. Rollback du lieu");
            }

            List<HIS_EXP_MEST_MATERIAL> data = new List<HIS_EXP_MEST_MATERIAL>();

            //Duyet theo tung yeu cau de tao ra du lieu duyet (exp_mest_material) tuong ung
            foreach (PresMaterialBySerialNumberSDO req in reqMaterials)
            {
                List<HIS_MATERIAL_BEAN> beans = materialBeans.Where(o => o.SERIAL_NUMBER == req.SerialNumber).ToList();

                if (!IsNotNullOrEmpty(beans))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMaterialBean_VatTuKhongCoSan, req.SerialNumber);
                    throw new Exception("Ko lay duoc vat tu tuong ung voi so seri: " + req.SerialNumber);
                }

                if (beans.Count > 1)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMaterialBean_CoNhieuHon1BeanVoiSoSeri, req.SerialNumber);
                    throw new Exception("Co nhieu hon 1 ban ghi bean voi so seri: " + req.SerialNumber);
                }

                HIS_EXP_MEST_MATERIAL exp = new HIS_EXP_MEST_MATERIAL();
                exp.EXP_MEST_ID = expMest.ID;
                exp.AMOUNT = beans.Sum(o => o.AMOUNT);
                exp.TDL_MATERIAL_TYPE_ID = beans[0].TDL_MATERIAL_TYPE_ID;
                exp.MATERIAL_ID = beans[0].MATERIAL_ID;
                exp.TDL_MEDI_STOCK_ID = expMest.MEDI_STOCK_ID;
                exp.NUM_ORDER = req.NumOrder;
                exp.SERIAL_NUMBER = beans[0].SERIAL_NUMBER;
                exp.REMAIN_REUSE_COUNT = beans[0].REMAIN_REUSE_COUNT;
                exp.NUM_ORDER = req.NumOrder;

                materialDic.Add(exp, beans.Select(o => o.ID).ToList());
                data.Add(exp);
            }
            return data;
        }


        private void SqlUpdateBean(Dictionary<HIS_EXP_MEST_MATERIAL, List<long>> useBeandIdDic, List<long> deleteExpMestMaterialIds, ref List<string> sqls)
        {
            //Can cap nhat cac bean ko dung truoc
            //Tranh truong hop bean duoc gan lai vao cac exp_mest_material tao moi
            if (IsNotNullOrEmpty(deleteExpMestMaterialIds))
            {
                //cap nhat danh sach cac bean ko dung
                string query2 = DAOWorker.SqlDAO.AddInClause(deleteExpMestMaterialIds, "UPDATE HIS_MATERIAL_BEAN SET IS_ACTIVE = 1, EXP_MEST_MATERIAL_ID = NULL WHERE %IN_CLAUSE% ", "EXP_MEST_MATERIAL_ID");
                sqls.Add(query2);
            }

            if (IsNotNullOrEmpty(useBeandIdDic))
            {
                foreach (HIS_EXP_MEST_MATERIAL expMestMaterial in useBeandIdDic.Keys)
                {
                    List<long> beanIds = useBeandIdDic[expMestMaterial];
                    //cap nhat danh sach cac bean da dung
                    string query = DAOWorker.SqlDAO.AddInClause(beanIds, "UPDATE HIS_MATERIAL_BEAN SET IS_ACTIVE = 0, EXP_MEST_MATERIAL_ID = {0} WHERE %IN_CLAUSE% ", "ID");
                    query = string.Format(query, expMestMaterial.ID);
                    sqls.Add(query);
                }
            }
        }

        private void SqlDeleteExpMestMaterial(List<long> deleteIds, ref List<string> sqls)
        {
            if (IsNotNullOrEmpty(deleteIds))
            {
                string sql = DAOWorker.SqlDAO.AddInClause(deleteIds, "UPDATE HIS_EXP_MEST_MATERIAL SET IS_DELETE = 1, EXP_MEST_ID = NULL, MATERIAL_ID = NULL, TDL_MEDI_STOCK_ID = NULL, TDL_MATERIAL_TYPE_ID = NULL WHERE %IN_CLAUSE%", "ID");
                sqls.Add(sql);
            }
        }

        private void PassResult(List<HIS_EXP_MEST_MATERIAL> inserts, List<HIS_EXP_MEST_MATERIAL> updates, ref List<HIS_EXP_MEST_MATERIAL> resultData)
        {
            if (IsNotNullOrEmpty(inserts) || IsNotNullOrEmpty(updates))
            {
                if (resultData == null)
                    resultData = new List<HIS_EXP_MEST_MATERIAL>();
                if (IsNotNullOrEmpty(inserts))
                {
                    resultData.AddRange(inserts);
                }
                if (IsNotNullOrEmpty(updates))
                {
                    resultData.AddRange(updates);
                }
            }
        }

        internal void Rollback()
        {
            try
            {
                this.hisMaterialBeanTakeBySerial.Rollback();
                this.hisExpMestMaterialCreate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
