using System.Collections.Generic;
namespace MOS.DAO.Base
{
    public abstract class StagingObjectBase
    {
        public bool IsIncludeDeleted; //lay ca cac du lieu is_delete = 1
        public string OrderField;
        public string OrderDirection;

        public string ExtraOrderField1;
        public string ExtraOrderDirection1;

        public string ExtraOrderField2;
        public string ExtraOrderDirection2;

        public string ExtraOrderField3;
        public string ExtraOrderDirection3;

        public string ExtraOrderField4;
        public string ExtraOrderDirection4;

        public List<string> DynamicColumns { get; set; }
    }
}
