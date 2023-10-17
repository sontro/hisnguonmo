using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisSupplier.HisSupplier
{
    [Persistent("Person.Person")]
    public class Person : XPLiteObject
    {
        public Person(Session session) : base(session) { }
        [Key, DevExpress.Xpo.DisplayName("ID")]
        public System.Int32 BusinessEntityID;
        public string Title;
        [DevExpress.Xpo.DisplayName("First Name")]
        public string FirstName;
        [DevExpress.Xpo.DisplayName("Middle Name")]
        public string MiddleName;
        [DevExpress.Xpo.DisplayName("Last Name")]
        public string LastName;
    }
}
