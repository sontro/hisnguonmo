using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisBlood;
using MOS.MANAGER.HisBloodType;
using MOS.MANAGER.HisImpMestBlood;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMest.Other.Other.Update
{
    class BloodProcessor : BusinessBase
    {
        private HisBloodUpdate hisBloodUpdate;
        private HisBloodCreate hisBloodCreate;
        //private HisBloodTruncate hisBloodTruncate;
        private HisImpMestBloodCreate hisImpMestBloodCreate;
        private HisImpMestBloodUpdate hisImpMestBloodUpdate;
        //private HisImpMestBloodTruncate hisImpMestBloodTruncate;

        List<HIS_BLOOD> output = new List<HIS_BLOOD>();
        List<HIS_BLOOD> hisBloods;
        List<HIS_IMP_MEST_BLOOD> hisImpMestBloods;

        internal BloodProcessor(CommonParam param)
            : base(param)
        {
            this.hisBloodCreate = new HisBloodCreate(param);
            //this.hisBloodTruncate = new HisBloodTruncate(param);
            this.hisBloodUpdate = new HisBloodUpdate(param);
            this.hisImpMestBloodCreate = new HisImpMestBloodCreate(param);
            //this.hisImpMestBloodTruncate = new HisImpMestBloodTruncate(param);
            this.hisImpMestBloodUpdate = new HisImpMestBloodUpdate(param);
        }

        internal bool Run(HIS_IMP_MEST impMest, List<HIS_BLOOD> otherBloods, ref List<HIS_BLOOD> outBloods, ref List<string> sqls)
        {
            bool result = false;
            try
            {
                this.hisImpMestBloods = new HisImpMestBloodGet().GetByImpMestId(impMest.ID);
                if (IsNotNullOrEmpty(this.hisImpMestBloods))
                {
                    this.hisBloods = new HisBloodGet().GetByIds(this.hisImpMestBloods.Select(s => s.BLOOD_ID).ToList());
                }

                this.ProcessBlood(impMest, otherBloods);

                this.ProcessImpMestBlood(impMest, ref sqls);

                this.ProcessTruncateBlood(ref sqls);

                if (IsNotNullOrEmpty(output))
                {
                    outBloods = output;
                }
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        private void ProcessBlood(HIS_IMP_MEST impMest, List<HIS_BLOOD> otherBloods)
        {
            if (IsNotNullOrEmpty(otherBloods))
            {
                HisImpMestCheck checker = new HisImpMestCheck(param);
                bool valid = true;
                valid = valid && checker.IsValidBloodExpiredDates(otherBloods);
                if (!valid)
                {
                    throw new Exception("Du lieu khong hop le");
                }

                //Lay danh sach blood_type de lay thong tin gia noi bo (internal_price)
                List<long> bloodTypeIds = otherBloods.Select(o => o.BLOOD_TYPE_ID).ToList();
                List<HIS_BLOOD_TYPE> hisBloodTypes = new HisBloodTypeGet().GetByIds(bloodTypeIds);

                List<HIS_BLOOD> listUpdate = new List<HIS_BLOOD>();
                List<HIS_BLOOD> listCreate = new List<HIS_BLOOD>();
                foreach (HIS_BLOOD blood in otherBloods)
                {
                    HIS_BLOOD_TYPE bloodType = hisBloodTypes
                        .Where(o => o.ID == blood.BLOOD_TYPE_ID).SingleOrDefault();

                    //tu dong insert cac truong sau dua vao thong tin co trong danh muc
                    blood.INTERNAL_PRICE = bloodType.INTERNAL_PRICE;
                    blood.SUPPLIER_ID = impMest.SUPPLIER_ID;
                    blood.IS_PREGNANT = MOS.UTILITY.Constant.IS_TRUE;

                    if (blood.ID > 0)
                    {
                        HIS_BLOOD oldBlood = this.hisBloods != null ? this.hisBloods.FirstOrDefault(o => o.ID == blood.ID) : null;
                        if (oldBlood == null)
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                            throw new Exception("BloodId invalid: " + blood.ID);
                        }
                        if (HisBloodUtil.CheckIsDiff(blood, oldBlood))
                        {
                            listUpdate.Add(blood);
                        }
                        else
                        {
                            this.output.Add(blood);
                        }
                    }
                    else
                    {
                        listCreate.Add(blood);
                    }
                }

                if (IsNotNullOrEmpty(listUpdate))
                {
                    if (!this.hisBloodUpdate.UpdateList(listUpdate))
                    {
                        throw new Exception("Update HIS_BLOOD that bai");
                    }
                    this.output.AddRange(listUpdate);
                }
                if (IsNotNullOrEmpty(listCreate))
                {
                    if (!this.hisBloodCreate.CreateList(listCreate))
                    {
                        throw new Exception("Tao HIS_BLOOD that bai");
                    }
                    this.output.AddRange(listCreate);
                }
            }
        }

        private void ProcessImpMestBlood(HIS_IMP_MEST impMest, ref List<string> sqls)
        {
            List<HIS_IMP_MEST_BLOOD> listToDelete = new List<HIS_IMP_MEST_BLOOD>();
            List<HIS_IMP_MEST_BLOOD> listToUpdate = new List<HIS_IMP_MEST_BLOOD>();
            List<HIS_IMP_MEST_BLOOD> listToCreate = new List<HIS_IMP_MEST_BLOOD>();

            List<long> updateIds = new List<long>();
            if (IsNotNullOrEmpty(this.output))
            {
                foreach (HIS_BLOOD blood in this.output)
                {
                    HIS_IMP_MEST_BLOOD impMestBlood = null;
                    HIS_IMP_MEST_BLOOD exists = IsNotNullOrEmpty(this.hisImpMestBloods) ? this.hisImpMestBloods.FirstOrDefault(o => o.BLOOD_ID == blood.ID) : null;
                    if (exists != null)
                    {
                        impMestBlood = exists;
                        if (impMestBlood.PRICE != blood.IMP_PRICE || impMestBlood.VAT_RATIO != blood.IMP_VAT_RATIO)
                        {
                            listToUpdate.Add(impMestBlood);
                        }
                        updateIds.Add(impMestBlood.ID);
                    }
                    else
                    {
                        impMestBlood = new HIS_IMP_MEST_BLOOD();
                        impMestBlood.IMP_MEST_ID = impMest.ID;
                        impMestBlood.BLOOD_ID = blood.ID;
                        impMestBlood.PRICE = blood.IMP_PRICE;
                        impMestBlood.VAT_RATIO = blood.IMP_VAT_RATIO;
                        listToCreate.Add(impMestBlood);
                    }
                }
            }

            //lay ra danh sach can xoa la danh sach co trong he thong ma ko co trong danh sach y/c tu client
            listToDelete = IsNotNullOrEmpty(this.hisImpMestBloods) ? this.hisImpMestBloods.Where(o => !updateIds.Contains(o.ID)).ToList() : null;

            if (IsNotNullOrEmpty(listToCreate))
            {
                if (!this.hisImpMestBloodCreate.CreateList(listToCreate))
                {
                    throw new Exception("Tao HIS_IMP_MEST_BLOOD that bai");
                }
            }

            if (IsNotNullOrEmpty(listToUpdate))
            {
                if (!this.hisImpMestBloodUpdate.UpdateList(listToUpdate))
                {
                    throw new Exception("Update HIS_IMP_MEST_BLOOD that bai");
                }
            }

            if (IsNotNullOrEmpty(listToDelete))
            {
                string sql = DAOWorker.SqlDAO.AddInClause(listToDelete.Select(s => s.ID).ToList(), "DELETE HIS_IMP_MEST_BLOOD WHERE %IN_CLAUSE% ", "ID");
                sqls.Add(sql);
                //if (!this.hisImpMestBloodTruncate.TruncateList(listToDelete))
                //{
                //    throw new Exception("Xoa HIS_IMP_MEST_BLOOD that bai");
                //}
            }
        }

        private void ProcessTruncateBlood(ref List<string> sqls)
        {
            List<HIS_BLOOD> needToDeleteBloods = this.hisBloods != null ? this.hisBloods.Where(o => !this.output.Exists(e => e.ID == o.ID)).ToList() : null;

            if (IsNotNullOrEmpty(needToDeleteBloods))
            {
                string sql = DAOWorker.SqlDAO.AddInClause(needToDeleteBloods.Select(s => s.ID).ToList(), "DELETE HIS_BLOOD WHERE %IN_CLAUSE% ", "ID");
                sqls.Add(sql);
                //if (!this.hisBloodTruncate.TruncateList(needToDeleteBloods))
                //{
                //    throw new Exception("Xoa HIS_IMP_MEST_BLOOD that bai");
                //}
            }
        }

        internal void Rollback()
        {
            try
            {
                this.hisImpMestBloodUpdate.RollbackData();
                this.hisImpMestBloodCreate.RollbackData();
                this.hisBloodUpdate.RollbackData();
                this.hisBloodCreate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
