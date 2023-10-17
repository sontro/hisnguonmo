using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDhst
{
    public class HisDhstViewFilterQuery : HisDhstViewFilter
    {
        public HisDhstViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_DHST, bool>>> listVHisDhstExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_DHST, bool>>>();



        internal HisDhstSO Query()
        {
            HisDhstSO search = new HisDhstSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisDhstExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listVHisDhstExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisDhstExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisDhstExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisDhstExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisDhstExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisDhstExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisDhstExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisDhstExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisDhstExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.TREATMENT_ID.HasValue)
                {
                    listVHisDhstExpression.Add(o => o.TREATMENT_ID == this.TREATMENT_ID.Value);
                }

                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listVHisDhstExpression.Add(o =>
                        o.EXECUTE_LOGINNAME.ToLower().Contains(this.KEY_WORD) ||
                        o.EXECUTE_USERNAME.ToLower().Contains(this.KEY_WORD) ||
                        o.DEPARTMENT_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.DEPARTMENT_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.ROOM_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.ROOM_NAME.ToLower().Contains(this.KEY_WORD)
                        );
                }

                if (this.TRACKING_ID.HasValue)
                {
                    listVHisDhstExpression.Add(o => o.TRACKING_ID.HasValue && o.TRACKING_ID.Value == this.TRACKING_ID.Value);
                }
                if (this.TRACKING_IDs != null)
                {
                    listVHisDhstExpression.Add(o => o.TRACKING_ID.HasValue && this.TRACKING_IDs.Contains(o.TRACKING_ID.Value));
                }
                if (this.TREATMENT_IDs != null)
                {
                    listVHisDhstExpression.Add(o => this.TREATMENT_IDs.Contains(o.TREATMENT_ID));
                }
                if (this.CARE_ID.HasValue)
                {
                    listVHisDhstExpression.Add(o => o.CARE_ID.HasValue && o.CARE_ID == this.CARE_ID.Value);
                }
                if (this.CARE_IDs != null)
                {
                    listVHisDhstExpression.Add(o => o.CARE_ID.HasValue && this.CARE_IDs.Contains(o.CARE_ID.Value));
                }
                if (this.DHST_SUM_ID.HasValue)
                {
                    listVHisDhstExpression.Add(o => o.DHST_SUM_ID.HasValue && o.DHST_SUM_ID.Value == this.DHST_SUM_ID.Value);
                }
                if (this.DHST_SUM_IDs != null)
                {
                    listVHisDhstExpression.Add(o => o.DHST_SUM_ID.HasValue && this.DHST_SUM_IDs.Contains(o.DHST_SUM_ID.Value));
                }
                if (this.EXECUTE_DEPARTMENT_ID.HasValue)
                {
                    listVHisDhstExpression.Add(o => o.EXECUTE_DEPARTMENT_ID.HasValue && o.EXECUTE_DEPARTMENT_ID.Value == this.EXECUTE_DEPARTMENT_ID.Value);
                }
                if (this.EXECUTE_DEPARTMENT_IDs != null)
                {
                    listVHisDhstExpression.Add(o => o.EXECUTE_DEPARTMENT_ID.HasValue && this.EXECUTE_DEPARTMENT_IDs.Contains(o.EXECUTE_DEPARTMENT_ID.Value));
                }
                if (this.EXECUTE_ROOM_ID.HasValue)
                {
                    listVHisDhstExpression.Add(o => o.EXECUTE_ROOM_ID.HasValue && o.EXECUTE_ROOM_ID.Value == this.EXECUTE_ROOM_ID.Value);
                }
                if (this.EXECUTE_ROOM_IDs != null)
                {
                    listVHisDhstExpression.Add(o => o.EXECUTE_ROOM_ID.HasValue && this.EXECUTE_ROOM_IDs.Contains(o.EXECUTE_ROOM_ID.Value));
                }

                if (this.EXECUTE_TIME_FROM.HasValue)
                {
                    listVHisDhstExpression.Add(o => o.EXECUTE_TIME.HasValue && o.EXECUTE_TIME.Value >= this.EXECUTE_TIME_FROM.Value);
                }
                if (this.EXECUTE_TIME_TO.HasValue)
                {
                    listVHisDhstExpression.Add(o => o.EXECUTE_TIME.HasValue && o.EXECUTE_TIME.Value <= this.EXECUTE_TIME_TO.Value);
                }

                if (!String.IsNullOrWhiteSpace(this.DEPARTMENT_CODE__EXACT))
                {
                    listVHisDhstExpression.Add(o => o.DEPARTMENT_CODE == this.DEPARTMENT_CODE__EXACT);
                }
                if (!String.IsNullOrWhiteSpace(this.EXECUTE_LOGINNAME__EXACT))
                {
                    listVHisDhstExpression.Add(o => o.EXECUTE_LOGINNAME == this.EXECUTE_LOGINNAME__EXACT);
                }
                if (!String.IsNullOrWhiteSpace(this.ROOM_CODE__EXACT))
                {
                    listVHisDhstExpression.Add(o => o.ROOM_CODE == this.ROOM_CODE__EXACT);
                }

                if (this.IS_IN_SERVICE_REQ.HasValue)
                {
                    if (this.IS_IN_SERVICE_REQ.Value)
                    {
                        listVHisDhstExpression.Add(o => o.IS_IN_SERVICE_REQ != null && o.IS_IN_SERVICE_REQ == "1");
                    }
                    else
                    {
                        listVHisDhstExpression.Add(o => o.IS_IN_SERVICE_REQ == null || o.IS_IN_SERVICE_REQ != "1");
                    }
                }
                if (this.HAS_CARE_ID.HasValue)
                {
                    if (this.HAS_CARE_ID.Value)
                    {
                        listVHisDhstExpression.Add(o => o.CARE_ID.HasValue);
                    }
                    else
                    {
                        listVHisDhstExpression.Add(o => !o.CARE_ID.HasValue);
                    }
                }
                if (this.HAS_TRACKING_ID.HasValue)
                {
                    if (this.HAS_TRACKING_ID.Value)
                    {
                        listVHisDhstExpression.Add(o => o.TRACKING_ID.HasValue);
                    }
                    else
                    {
                        listVHisDhstExpression.Add(o => !o.TRACKING_ID.HasValue);
                    }
                }
                if (this.HAS_CARE_OR_TRACKING_ID.HasValue)
                {
                    if (this.HAS_CARE_OR_TRACKING_ID.Value)
                    {
                        listVHisDhstExpression.Add(o => o.CARE_ID.HasValue || o.TRACKING_ID.HasValue);
                    }
                    else
                    {
                        listVHisDhstExpression.Add(o => !o.CARE_ID.HasValue && !o.TRACKING_ID.HasValue);
                    }
                }
                if (this.RECORD_TYPE_BELONGs != null && this.RECORD_TYPE_BELONGs.Count > 0)
                {
                    var searchPredicate = PredicateBuilder.False<V_HIS_DHST>();

                    foreach (RecordTypeBelong t in this.RECORD_TYPE_BELONGs)
                    {
                        if (t == RecordTypeBelong.CARE)
                        {
                            searchPredicate =
                              searchPredicate.Or(o => o.CARE_ID.HasValue);
                        }
                        else if (t == RecordTypeBelong.EXAM)
                        {
                            searchPredicate =
                             searchPredicate.Or(o => o.IS_IN_SERVICE_REQ == "1");
                        }
                        else if (t == RecordTypeBelong.TRACKING)
                        {
                            searchPredicate =
                              searchPredicate.Or(o => o.TRACKING_ID.HasValue);
                        }
                    }

                    search.listVHisDhstExpression.Add(searchPredicate);
                }
                if (this.VACCINATION_EXAM_ID.HasValue)
                {
                    listVHisDhstExpression.Add(o => o.VACCINATION_EXAM_ID.HasValue && o.VACCINATION_EXAM_ID.Value == this.VACCINATION_EXAM_ID.Value);
                }
                if (this.VACCINATION_EXAM_IDs != null)
                {
                    listVHisDhstExpression.Add(o => o.VACCINATION_EXAM_ID.HasValue && this.VACCINATION_EXAM_IDs.Contains(o.VACCINATION_EXAM_ID.Value));
                }

                search.listVHisDhstExpression.AddRange(listVHisDhstExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisDhstExpression.Clear();
                listVHisDhstExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
