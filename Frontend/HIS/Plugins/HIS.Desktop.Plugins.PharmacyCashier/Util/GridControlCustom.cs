using DevExpress.Data.Filtering;
using DevExpress.Data.Filtering.Helpers;
using DevExpress.XtraGrid.Columns;
using Inventec.Desktop.CustomControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.PharmacyCashier.Util
{
    public class GridControlCustom : CustomGridViewWithFilterMultiColumn
    {
        public GridControlCustom() : base() { }

        protected override string OnCreateLookupDisplayFilter(string text, string displayMember)
        {
            List<CriteriaOperator> subStringOperators = new List<CriteriaOperator>();
            string sString = text.Trim();
            string exp = LikeData.CreateContainsPattern(sString);
            List<CriteriaOperator> columnsOperators = new List<CriteriaOperator>();
            foreach (GridColumn col in Columns)
            {
                if (col.ColumnType == typeof(string))
                    columnsOperators.Add(new BinaryOperator(col.FieldName, exp, BinaryOperatorType.Like));
            }
            subStringOperators.Add(new GroupOperator(GroupOperatorType.Or, columnsOperators));
            return new GroupOperator(GroupOperatorType.And, subStringOperators).ToString();
        }
    }
}
