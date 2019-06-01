using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Globalization;
using MoBaSteuerung.Anlagenkomponenten.MCSpeicher;
using MoBaSteuerung.Elemente;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.Windows.Forms;

namespace MoBaSteuerung.Anlagenkomponenten {

	

	class AdresseTypeConverter : ExpandableObjectConverter{
	
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) {
			return true;
			//return base.CanConvertFrom(context, sourceType);
		}
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value) {
			if (value is String) {
				AnlagenElement adr = (AnlagenElement)context.Instance;
				if (context.PropertyDescriptor.DisplayName == "Ausgang") {
					adr.Ausgang.SpeicherString = (string)value;
					context.OnComponentChanged();
					return adr.Ausgang;
				}
				else if((adr is Gleis)&&(context.PropertyDescriptor.DisplayName == "Eingang")) {
					Gleis gl = (Gleis)adr;
					gl.Eingang.SpeicherString = (string)value;
					return gl.Eingang;
				}
			}
			return null;
		}

		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) {
			return (destinationType.Name == "Adresse");
			//return base.CanConvertTo(context, destinationType);
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType) {
			if(value is Adresse) {
				Adresse adr = (Adresse)value;

				return adr.SpeicherString;
			}
			return value.GetType().FullName;
		}
	}

	class KnotenTypeConverter : TypeConverter {

		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) {
			return false;
		}
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value) {
			return null;
		}

		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) {
			return (destinationType.Name == "Knoten");
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType) {
			if (value is Knoten) {
				Knoten kn = (Knoten)value;

				return kn.ID.ToString();
			}
			return value.GetType().FullName;
		}
	}

	class BefehlslisteTypeConverter : TypeConverter {

		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) {
			return false;
		}
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value) {
			return null;
		}

		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) {
			return (destinationType.Name == "BefehlsListe");
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType) {
			if (value is BefehlsListe) {
				AnlagenElement el = (AnlagenElement)context.Instance;
				if(el!= null) {
					return el.Koppelung.ListenString;
				}
			}
			return value.GetType().FullName;
		}
	}
}
