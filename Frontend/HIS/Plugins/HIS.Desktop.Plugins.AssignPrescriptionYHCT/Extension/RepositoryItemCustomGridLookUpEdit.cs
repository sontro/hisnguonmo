using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;

namespace HIS.Desktop.Plugins.AssignPrescriptionYHCT.Extension
{
    [UserRepositoryItem("RegisterCustomGridLookUpEdit")]
    public class RepositoryItemCustomGridLookUpEdit : RepositoryItemGridLookUpEdit
    {
        static RepositoryItemCustomGridLookUpEdit() { RegisterCustomGridLookUpEdit(); }
        public RepositoryItemCustomGridLookUpEdit() { }
        public const string CustomGridLookUpEditName = "CustomGridLookUpEdit";
        public override string EditorTypeName { get { return CustomGridLookUpEditName; } }

        public static void RegisterCustomGridLookUpEdit()
        {
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(CustomGridLookUpEditName,
              typeof(CustomGridLookUpEdit), typeof(RepositoryItemCustomGridLookUpEdit),
              typeof(GridLookUpEditBaseViewInfo), new ButtonEditPainter(), true));
        }
    }
}
