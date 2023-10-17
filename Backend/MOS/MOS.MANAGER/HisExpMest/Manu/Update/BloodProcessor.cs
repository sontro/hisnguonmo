using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisBlood;
using MOS.MANAGER.HisExpMestBlood;
using MOS.SDO;
using MOS.UTILITY;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Manu.Update
{
    class BloodProcessor : BusinessBase
    {
        private HisExpMestBloodCreate hisExpMestBloodCreate;
        private HisExpMestBloodUpdate hisExpMestBloodUpdate;

        internal BloodProcessor(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.hisExpMestBloodCreate = new HisExpMestBloodCreate(param);
            this.hisExpMestBloodUpdate = new HisExpMestBloodUpdate(param);
        }

        internal bool Run(HisExpMestManuSDO sdo, HIS_EXP_MEST expMest, ref List<HIS_EXP_MEST_BLOOD> resultData, ref List<string> sqls)
        {
            try
            {
                if (expMest != null)
                {
                    List<HIS_EXP_MEST_BLOOD> inserts = new List<HIS_EXP_MEST_BLOOD>();
                    List<HIS_EXP_MEST_BLOOD> deletes = new List<HIS_EXP_MEST_BLOOD>();
                    List<HIS_EXP_MEST_BLOOD> updates = new List<HIS_EXP_MEST_BLOOD>();
                    List<HIS_EXP_MEST_BLOOD> beforeUpdates = new List<HIS_EXP_MEST_BLOOD>();

                    //Lay ra danh sach thong tin cu
                    List<HIS_EXP_MEST_BLOOD> olds = new HisExpMestBloodGet().GetByExpMestId(expMest.ID);

                    //Danh sach exp_mest_blood
                    List<HIS_EXP_MEST_BLOOD> news = this.MakeData(sdo, expMest, olds);

                    List<long> expMestBloodIds = IsNotNullOrEmpty(olds) ? olds.Select(o => o.ID).ToList() : null;

                    this.GetDiff(olds, news, ref inserts, ref deletes, ref updates, ref beforeUpdates);

                    if (IsNotNullOrEmpty(inserts) && !this.hisExpMestBloodCreate.CreateList(inserts))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }

                    if (IsNotNullOrEmpty(updates) && !this.hisExpMestBloodUpdate.UpdateList(updates, beforeUpdates))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }

                    if (sqls == null)
                    {
                        sqls = new List<string>();
                    }

                    List<long> deleteExpMestBloodIds = IsNotNullOrEmpty(deletes) ? deletes.Select(o => o.ID).ToList() : null;
                    List<long> bloodOfDeleteExpMestBloodIds = IsNotNullOrEmpty(deletes) ? deletes.Select(o => o.BLOOD_ID).ToList() : null;
                    //Cap nhat thong tin bean
                    this.SqlUpdateBlood(inserts, bloodOfDeleteExpMestBloodIds, ref sqls);

                    //Xoa cac exp_mest_blood ko dung.
                    //Luu y: can thuc hien xoa exp_mest_blood sau khi da cap nhat bean (tranh bi loi fk)
                    this.SqlDeleteExpMestBlood(deleteExpMestBloodIds, ref sqls);

                    this.PassResult(olds, inserts, updates, deletes, ref resultData);
                }
                return true;
            }
            catch (Exception ex)
            {
                this.Rollback();
                LogSystem.Error(ex);
                return false;
            }
        }

        private void PassResult(List<HIS_EXP_MEST_BLOOD> olds, List<HIS_EXP_MEST_BLOOD> inserts, List<HIS_EXP_MEST_BLOOD> updates, List<HIS_EXP_MEST_BLOOD> deletes, ref List<HIS_EXP_MEST_BLOOD> resultData)
        {
            if (IsNotNullOrEmpty(inserts) || IsNotNullOrEmpty(olds))
            {
                resultData = new List<HIS_EXP_MEST_BLOOD>();
                if (IsNotNullOrEmpty(inserts))
                {
                    resultData.AddRange(inserts);
                }
                if (IsNotNullOrEmpty(updates))
                {
                    resultData.AddRange(updates);
                }
                if (IsNotNullOrEmpty(olds))
                {
                    List<HIS_EXP_MEST_BLOOD> remains = olds;
                    remains.RemoveAll(o => deletes != null && deletes.Exists(t => t.ID == o.ID));
                    remains.RemoveAll(o => updates != null && updates.Exists(t => t.ID == o.ID));
                    resultData.AddRange(remains);
                }
            }
        }

        private void SqlUpdateBlood(List<HIS_EXP_MEST_BLOOD> newExpMestBloods, List<long> bloodIds, ref List<string> sqls)
        {
            if (IsNotNullOrEmpty(bloodIds))
            {
                //cap nhat danh sach cac blood ko dung
                string query2 = DAOWorker.SqlDAO.AddInClause(bloodIds, "UPDATE HIS_BLOOD SET IS_ACTIVE = 1 WHERE %IN_CLAUSE% ", "ID");
                sqls.Add(query2);
            }

            if (IsNotNullOrEmpty(newExpMestBloods))
            {
                List<long> useBloodIds = newExpMestBloods.Select(o => o.BLOOD_ID).ToList();
                //cap nhat danh sach cac bean da dung tuong ung voi cac exp_mest_blood
                string query = DAOWorker.SqlDAO.AddInClause(useBloodIds, "UPDATE HIS_BLOOD SET IS_ACTIVE = 0 WHERE %IN_CLAUSE% ", "ID");
                sqls.Add(query);
            }
        }

        private void SqlDeleteExpMestBlood(List<long> deleteIds, ref List<string> sqls)
        {
            if (IsNotNullOrEmpty(deleteIds))
            {
                string sql = DAOWorker.SqlDAO.AddInClause(deleteIds, "DELETE FROM HIS_EXP_MEST_BLOOD WHERE %IN_CLAUSE%", "ID");
                sqls.Add(sql);
            }
        }

        private void GetDiff(List<HIS_EXP_MEST_BLOOD> olds, List<HIS_EXP_MEST_BLOOD> news, ref List<HIS_EXP_MEST_BLOOD> inserts, ref List<HIS_EXP_MEST_BLOOD> deletes, ref List<HIS_EXP_MEST_BLOOD> updates, ref List<HIS_EXP_MEST_BLOOD> oldOfUpdates)
        {

            //Duyet du lieu truyen len de kiem tra thong tin thay doi
            if (!IsNotNullOrEmpty(news))
            {
                deletes = olds;
            }
            else if (!IsNotNullOrEmpty(olds))
            {
                inserts = news;
            }
            else
            {
                Mapper.CreateMap<HIS_EXP_MEST_BLOOD, HIS_EXP_MEST_BLOOD>();

                //Duyet danh sach moi, nhung du lieu co trong moi ma ko co trong cu ==> can them moi
                foreach (HIS_EXP_MEST_BLOOD newBlood in news)
                {
                    HIS_EXP_MEST_BLOOD old = olds
                        .Where(t => t.BLOOD_ID == newBlood.BLOOD_ID
                            && t.PRICE == newBlood.PRICE
                            && t.VAT_RATIO == newBlood.VAT_RATIO
                        ).FirstOrDefault();

                    if (old == null)
                    {
                        inserts.Add(newBlood);
                    }
                    else if (old.NUM_ORDER != newBlood.NUM_ORDER || old.DESCRIPTION != newBlood.DESCRIPTION)
                    {
                        HIS_EXP_MEST_BLOOD oldOfUpdate = Mapper.Map<HIS_EXP_MEST_BLOOD>(old);
                        old.DESCRIPTION = newBlood.DESCRIPTION;
                        old.NUM_ORDER = newBlood.NUM_ORDER;

                        oldOfUpdates.Add(oldOfUpdate);
                        updates.Add(old);
                    }
                }

                //Duyet danh sach cu, nhung du lieu co trong cu ma ko co trong moi ==> can xoa
                foreach (HIS_EXP_MEST_BLOOD old in olds)
                {
                    HIS_EXP_MEST_BLOOD newBlood = news
                        .Where(t => t.BLOOD_ID == old.BLOOD_ID
                            && t.PRICE == old.PRICE
                            && t.VAT_RATIO == old.VAT_RATIO
                        ).FirstOrDefault();

                    if (newBlood == null)
                    {
                        deletes.Add(old);
                    }
                }
            }
        }

        private List<HIS_EXP_MEST_BLOOD> MakeData(HisExpMestManuSDO sdo, HIS_EXP_MEST expMest, List<HIS_EXP_MEST_BLOOD> oldExpMestBloods)
        {
            List<HIS_EXP_MEST_BLOOD> data = new List<HIS_EXP_MEST_BLOOD>();
            if (IsNotNullOrEmpty(sdo.Bloods) && expMest != null)
            {
                List<long> bloodIds = sdo.Bloods.Select(o => o.BloodId).ToList();
                List<HIS_BLOOD> bls = new HisBloodGet().GetByIds(bloodIds);
                if (!IsNotNullOrEmpty(bls))
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("blood_id ko hop le");
                }

                List<string> notInMediStocks = bls.Where(o => o.MEDI_STOCK_ID != expMest.MEDI_STOCK_ID).Select(o => o.BLOOD_CODE).ToList();
                if (IsNotNullOrEmpty(notInMediStocks))
                {
                    string bloodCodes = string.Join(",", notInMediStocks);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisBlood_KhongThuocKho, bloodCodes);
                    throw new Exception("Rollback du lieu");
                }

                //Lay danh sach tui mau bi khoa va ko thuoc phieu xuat hien tai
                List<string> lockBloods = bls
                    .Where(o => o.IS_ACTIVE != IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE 
                        && (oldExpMestBloods == null || !oldExpMestBloods.Exists(t => t.BLOOD_ID == o.ID)))
                        .Select(s => s.BLOOD_CODE).ToList();
                if (IsNotNullOrEmpty(lockBloods))
                {
                    string bloodCodes = string.Join(",", lockBloods);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisBlood_CacTuiMauSauDangTamKhoa, bloodCodes);
                    throw new Exception("Rollback du lieu");
                }

                //Duyet theo y/c cua client de tao ra exp_mest_blood tuong ung
                foreach (ExpBloodSDO s in sdo.Bloods)
                {
                    HIS_BLOOD blood = bls.Where(o => o.ID == s.BloodId).FirstOrDefault();
                    if (blood == null)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_TuiMauDuocChonKhongKhaDung);
                        throw new Exception("Rollback du lieu");
                    }
                    HIS_EXP_MEST_BLOOD exp = new HIS_EXP_MEST_BLOOD();
                    exp.EXP_MEST_ID = expMest.ID;
                    exp.TDL_MEDI_STOCK_ID = expMest.MEDI_STOCK_ID;
                    exp.TDL_BLOOD_TYPE_ID = blood.BLOOD_TYPE_ID;
                    exp.TDL_SERVICE_REQ_ID = expMest.SERVICE_REQ_ID;
                    exp.TDL_TREATMENT_ID = expMest.TDL_TREATMENT_ID;
                    exp.BLOOD_ID = s.BloodId;
                    exp.NUM_ORDER = s.NumOrder;
                    exp.DESCRIPTION = sdo.Description;
                    exp.AC_SELF_ENVIDENCE = s.AcSelfEnvidence;
                    exp.AC_SELF_ENVIDENCE_SECOND = s.AcSelfEnvidenceSecond;
                    data.Add(exp);
                }
            }
            return data;
        }

        internal void Rollback()
        {
            this.hisExpMestBloodCreate.RollbackData();
            this.hisExpMestBloodUpdate.RollbackData();
        }
    }
}
