using MoBaSteuerung.Elemente;
using ModellBahnSteuerung;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace MoBaSteuerung.Anlagenkomponenten {
	public class MultiLineTextEditor : UITypeEditor {
		private IWindowsFormsEditorService _editorService;

		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context) {
			return UITypeEditorEditStyle.DropDown;
		}

		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value) {
			_editorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

			TextBox textEditorBox = new TextBox();
			textEditorBox.Multiline = true;
			textEditorBox.ScrollBars = ScrollBars.Vertical;
			textEditorBox.Width = 250;
			textEditorBox.Height = 150;
			textEditorBox.BorderStyle = BorderStyle.None;
			textEditorBox.AcceptsReturn = true;
			textEditorBox.Text = value as string;

			_editorService.DropDownControl(textEditorBox);

			return textEditorBox.Text;
		}
	}

	public class BefehlsListeTypeEditor : UITypeEditor {
		private IWindowsFormsEditorService _editorService;

		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context) {
			return UITypeEditorEditStyle.Modal;
		}

		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value) {
			_editorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

			BefehlsListe liste = (BefehlsListe)value;
			Form frm = new FrmBefehlsliste(liste, "Koppelung");

			_editorService.ShowDialog(frm);

			return liste;
		}

		public override string ToString() {
			return base.ToString();
		}
	}
}
