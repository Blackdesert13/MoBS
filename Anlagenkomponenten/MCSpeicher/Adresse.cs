using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MoBaSteuerung.Anlagenkomponenten;
using System.ComponentModel;

namespace MoBaSteuerung.Anlagenkomponenten.MCSpeicher {
	/// <summary>
	/// Adresse für Ausgang oder Rückmeldung
	/// </summary>
	public class Adresse {
		#region Member

		private int _ardNr = 0;//bei arduino = 0 keine aktive Adresse
		private int _adresseNr = 0;
		private int _bitNr = 0;
		private bool _stellung = false;
		private bool _gesperrt = false;
		private MCSpeicher _mc;
		private AnlagenElemente _parent;
		#endregion

		#region Properties
		/// <summary>
		/// Arduino
		/// </summary>
		[Browsable(false)]
		public MCSpeicher MC {
			get {
				return _mc;
			}
			set {
				_mc = value;
			}
		}

		/// <summary>
		/// zur Anzeige und speichern in der Anlagendatei
		/// </summary>
		[Browsable(false)]
		public string SpeicherString {
			set {
				string[] elemente = value.Split('-');
				if (elemente.Length < 3) elemente = value.Split(' ');
				MCNr = Convert.ToInt32(elemente[0]);
				_adresseNr = Convert.ToInt32(elemente[1]);
				_bitNr = Convert.ToInt32(elemente[2]);
				MCSpeicherEintragen();
			}
			get {
				return Convert.ToString(_ardNr) + '-' + Convert.ToString(_adresseNr) + '-' + Convert.ToString(_bitNr);
			}
		}

		/// <summary>
		/// Arduino-Nr
		/// </summary>
		[DisplayName("Arduino Nr.")]
		[Description("Nr. des Arduinos")]

		public int MCNr {
			set {
				_mc = _parent.ListeMCSpeicher.Element(value);
				_ardNr = value;
			}
			get {
				return _ardNr;
			}
		}

		/// <summary>
		/// Platinen-Nr.
		/// </summary>
		[DisplayName("Platinen Nr.")]
		[Category("2")]
		public int AdressenNr {
			set {
				if(value <=2 && value >= 0) {
					_adresseNr = value;
				}

			}
			get {
				return _adresseNr;
			}
		}

		/// <summary>
		/// Anschluss auf der Platine
		/// </summary>
		[DisplayName("Bit Nr.")]
		[Category("3")]
		public int BitNr {
			set {
				if (value <= 15 && value >= 0) {
					_bitNr = value;
				}
				
			}
			get {
				return _bitNr;
			}
		}

		[ReadOnly(true)]
		[Category("4")]
		public bool Stellung {
			set {
				_stellung = value;
			}
			get {
				return _stellung;
			}
		}

		/// <summary>
		/// gibt an ob der Ausgang blockiert ist
		/// </summary>
		[ReadOnly(true)]
		[Category("5")]
		public bool IsLocked {
			get {
				Arduino ard = _parent.AnlagenZustand.GetArduino(_ardNr);
				if (ard != null) {
					return ard.LockedAusg[_adresseNr, _bitNr];
				}
				return _gesperrt;
			}
			set {
				Arduino ard = _parent.AnlagenZustand.GetArduino(_ardNr);
				if (ard != null) {
					ard.LockedAusg[_adresseNr, _bitNr] = value;
				}
				_gesperrt = value;
			}
		}

		/// <summary>
		/// Arduino-Nr.
		/// </summary>
		[Browsable(false)]
		public int ArdNr {
			get {
				return _ardNr;
			}

			set {
				_ardNr = value;
			}
		}

		#endregion

		public Adresse(AnlagenElemente parent, int nrArduino, int nrAdresse, int nrBit) {
			_parent = parent;
			MCNr = nrArduino;
			_adresseNr = nrAdresse;
			_bitNr = nrBit;
			MCSpeicherEintragen();
		}

		public Adresse(AnlagenElemente parent) {
			_parent = parent;
		}

		#region Methoden

		private void MCSpeicherEintragen() {
			_mc = _parent.ListeMCSpeicher.Element(MCNr);
		}

		/// <summary>
		/// liefert die Stellung der Ausgangs-Adresse
		/// </summary>
		/// <returns></returns>
		public bool AusgangAbfragen() {
			//if (_ardNr == 0)
			//    return (_bitNr != 0);
			Arduino ard = _parent.AnlagenZustand.GetArduino(_ardNr);
			if (ard != null) {
				return ard.Ausgaenge[_adresseNr, _bitNr];
			}
			return _stellung;
		}

		/// <summary>
		/// liefert die Stellung der Ausgangs-Adresse
		/// </summary>
		/// <returns></returns>
		public bool EingangAbfragen()
		{
			//if (_ardNr == 0)
			//    return (_bitNr != 0);
			Arduino ard = _parent.AnlagenZustand.GetArduino(_ardNr);
			if (ard != null)
			{
				return ard.Rueckmeldung[_adresseNr, _bitNr];
			}
			return _stellung;
		}
		/// <summary>
		/// liefert den Zustand der Rückmelde-Adresse
		/// </summary>
		/// <returns></returns>
		public bool RueckmeldungAbfragen() {
			Arduino ard = _parent.AnlagenZustand.GetArduino(_ardNr);
			if (ard != null) {
				return ard.Rueckmeldung[_adresseNr, _bitNr];
			}
			return false;
			//return _mc.RueckmeldungAbfragen(this);
		}

		/// <summary>
		/// Schaltet den Ausgang auf 'Stellung'(Befehl)
		/// </summary>
		public void AusgangSchalten() {
			//_mc.AusgangSetzen(this, _stellung);
			Arduino ard = _parent.AnlagenZustand.GetArduino(_ardNr);
			if (ard != null) {
				if (!ard.LockedAusg[_adresseNr, _bitNr])
					ard.Ausgaenge[_adresseNr, _bitNr] = _stellung;
			}
		}

		/// <summary>
		/// Schaltet den Ausgang um.
		/// Gibt "true" zurück, wenn der Ausgang geschaltet werden 
		/// konnte, andernfalls "false".
		/// </summary>
		/// <returns></returns>
		public bool AusgangToggeln() {
			//if(_mc != null) { 
			//    _mc.AusgangToggeln(this);
			//    return;
			//}
			if (!IsLocked) {
				Arduino ard = _parent.AnlagenZustand.GetArduino(_ardNr);
				if (ard != null) {
					ard.Ausgaenge.Toggle(_adresseNr, _bitNr);
					_stellung = ard.Ausgaenge[_adresseNr, _bitNr];
				}
				else{
				_stellung = !_stellung;
				}
				return true;
			}
			return false;
		}

		/// <summary>
		/// Schaltet den Ausgang in den angegebenen Schaltzustand.
		/// Gibt "true" zurück, wenn der Ausgang geschaltet werden 
		/// konnte, andernfalls "false".
		/// </summary>
		/// <returns></returns>
		public bool AusgangSchalten(bool schaltzustand) {
			if (!IsLocked) {
				Arduino ard = _parent.AnlagenZustand.GetArduino(_ardNr);
				if (ard != null) {
					ard.Ausgaenge[_adresseNr, _bitNr] = schaltzustand;
				}
				_stellung = schaltzustand;
				return true;
			}
			
			return false;
		}

		public bool GleicheAdresse(Adresse adresse) {
			if (adresse.ArdNr == this.ArdNr && adresse.AdressenNr == this.AdressenNr && adresse.BitNr == this.BitNr)
				return true;
			return false;
		}

		#endregion


	}
}
