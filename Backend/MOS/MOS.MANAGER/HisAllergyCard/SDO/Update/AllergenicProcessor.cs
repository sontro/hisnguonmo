using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisAllergenic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisAllergyCard.SDO.Update
{
    class AllergenicProcessor : BusinessBase
    {
        private HisAllergenicCreate hisAllergenicCreate;
        private HisAllergenicUpdate hisAllergenicUpdate;

        internal AllergenicProcessor()
            : base()
        {
            this.Init();
        }

        internal AllergenicProcessor(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisAllergenicCreate = new HisAllergenicCreate(param);
            this.hisAllergenicUpdate = new HisAllergenicUpdate(param);
        }

        internal bool Run(HIS_ALLERGY_CARD allergyCard,List<HIS_ALLERGENIC> allergenicss,List<HIS_ALLERGENIC> olds, ref List<string> sqls)
        {
            bool result = false;
            try
            {
                if (allergyCard != null && IsNotNullOrEmpty(allergenicss))
                {
                    allergenicss.ForEach(o => o.ALLERGY_CARD_ID = allergyCard.ID);
                    List<HIS_ALLERGENIC> updates = new List<HIS_ALLERGENIC>();
                    List<HIS_ALLERGENIC> inserts = new List<HIS_ALLERGENIC>();
                    List<HIS_ALLERGENIC> deletes = new List<HIS_ALLERGENIC>();
                    List<HIS_ALLERGENIC> befores = new List<HIS_ALLERGENIC>();

                    Mapper.CreateMap<HIS_ALLERGENIC, HIS_ALLERGENIC>();

                    foreach (HIS_ALLERGENIC adrMety in allergenicss)
                    {
                        if (adrMety.ID > 0)
                        {
                            HIS_ALLERGENIC exists = olds.FirstOrDefault(o => o.ID == adrMety.ID);
                            if (exists == null)
                            {
                                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                                throw new Exception("Khong tim thay HIS_ALLERGENIC tuong ung voi ID: " + adrMety.ID);
                            }
                            befores.Add(Mapper.Map<HIS_ALLERGENIC>(exists));
                            updates.Add(adrMety);
                        }
                        else
                        {
                            inserts.Add(adrMety);
                        }
                    }

                    deletes = olds != null ? olds.Where(s => !updates.Exists(e => e.ID == s.ID)).ToList() : null;

                    if (IsNotNullOrEmpty(inserts))
                    {
                        if (!this.hisAllergenicCreate.CreateList(inserts))
                        {
                            throw new Exception("Tao Allergenic that bai. Ket thuc nghiep vu");
                        }
                    }

                    if (IsNotNullOrEmpty(updates))
                    {
                        if (!this.hisAllergenicUpdate.UpdateList(updates, befores))
                        {
                            throw new Exception("Cap nhat Allergenic that bai. Ket thuc nghiep vu");
                        }
                    }

                    if (IsNotNullOrEmpty(deletes))
                    {
                        if (sqls == null) sqls = new List<string>();
                        string sqlDelete = DAOWorker.SqlDAO.AddInClause(deletes.Select(s => s.ID).ToList(), "DELETE HIS_ALLERGENIC WHERE %IN_CLAUSE% ", "ID");
                        sqls.Add(sqlDelete);
                    }

                    allergenicss = new List<HIS_ALLERGENIC>();
                    if (IsNotNullOrEmpty(updates))
                    {
                        allergenicss.AddRange(updates);
                    }
                    if (IsNotNullOrEmpty(inserts))
                    {
                        allergenicss.AddRange(inserts);
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        internal void RollbackData()
        {
            try
            {
                this.hisAllergenicUpdate.RollbackData();
                this.hisAllergenicCreate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
