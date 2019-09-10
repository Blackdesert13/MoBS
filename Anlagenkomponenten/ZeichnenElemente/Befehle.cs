using MoBaSteuerung.Anlagenkomponenten;
using MoBaSteuerung.Anlagenkomponenten.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MoBaSteuerung.Elemente {
	public class BefehlsListe {
		private List<Befehl> _liste;
		private string _listenString;
		private AnlagenElemente _parent;
		private bool _ausgangsStellung;
		//private bool _aktiv = true;

		public BefehlsListe(AnlagenElemente Parent, bool AusgangsStellung, string Listenstring) {
			_parent = Parent;
			//_aktiv = AusgangsStellung;
			_listenString = Listenstring;
			ListeAktivieren();
		}

		//public bool Aktiv
		//{
		//	set {
		//		//_aktiv = value;
		//	}			
		//}

		public string ListenString {
			get { return _listenString; }
			set { _listenString = value; }
		}

		public List<Befehl> BefListe {
			get { return _liste; }
			set { _liste = value; }
		}

		/// <summary>
		/// übernimmt eine neue Befehls-Liste aus einem String und aktiviert diese gleich
		/// </summary>
		/// <param name="ListensString"></param>
		public void ListeNeu(string ListensString) { }


		/// <summary>
		/// 
		/// </summary>
		/// <param name="parent"></param>
		public void ListeAktivieren()//(AnlagenElemente parent)
		{
			_liste = new List<Befehl>();
			//_aktiv = true;
			if (_listenString != "" && _listenString != null) {
				string[] stringArray = _listenString.Split(new char[] { ' ' ,';'});
				for (int i = 0; i < stringArray.Length; i++) {
					string[] befehl = stringArray[i].Split(':');
					string[] elName = Regex.Matches(befehl[0], @"[a-zA-Z]+|\d+").Cast<Match>().Select(m => m.Value).ToArray();
					AnlagenElement el = null;
					//Befehl nBefehl = new Befehl(,);

					if (elName.Length > 0) {
						switch (elName[0]) {
							case "Gl":
								el = this._parent.GleisElemente.Element(Convert.ToInt16(elName[1]));
								break;
							case "Sn":
								el = this._parent.SignalElemente.Element(Convert.ToInt16(elName[1]));
								break;
							case "We":
								el = this._parent.WeicheElemente.Element(Convert.ToInt16(elName[1]));
								break;
							case "Fss":
								el = this._parent.FssElemente.Element(Convert.ToInt16(elName[1]));
								break;
							default:
								break;
						}
					}

					if (el != null) {
						Befehl nBefehl = new Befehl();
						nBefehl.Element = el;
						switch (befehl[1]) {
							case "An":
								nBefehl.Attribut = Elementzustand.An;
								break;
							case "Aus":
								nBefehl.Attribut = Elementzustand.Aus;
								break;
							case "Neg":
								nBefehl.Attribut = Elementzustand.Neg;
								break;
							case "Gleich":
								nBefehl.Attribut = Elementzustand.Gleich;
								break;
						}
						_liste.Add(nBefehl);
					}
				}
			}
		}
		public void KoppelungSchalten(bool AusgangsStellung) {
			//if (_aktiv)
			{
			_ausgangsStellung = AusgangsStellung;
				foreach (Befehl x in _liste)
				{
				x.BefehlAusfuehren(_ausgangsStellung);
			}
		}
		}
		/// <summary>
		/// führt alle Befehle der Befehlsliste aus
		/// </summary>
		public void KoppelungSchalten() {
			//if (_aktiv)
			{
				foreach (Befehl x in _liste)
				{
				x.BefehlAusfuehren();
			}
			}
		}
	}

	public class Befehl {
		private AnlagenElement _element;
		private bool _schaltZustand;
		private Elementzustand _attribut;


		public AnlagenElement Element {
			get {
				return _element;
			}

			set {
				_element = value;
			}
		}

		public bool SchaltZustand {
			get { return _schaltZustand; }
			set { _schaltZustand = value; }
		}

		public Elementzustand Attribut {
			get { return _attribut; }
			set {
				_attribut = value;
				switch (_attribut) {
					case Elementzustand.An:
						_schaltZustand = true;
						break;
					case Elementzustand.Aus:
						_schaltZustand = false;
						break;
					case Elementzustand.Gleich:
						//_schaltZustand = true;
						break;
				}
			}
		}

		public Befehl() { }

		public Befehl(AnlagenElement element, bool schaltZustand) {
			this.Element = element;
			this.SchaltZustand = schaltZustand;
		}

		public void BefehlAusfuehren() {
			this.Element.Ausgang.AusgangSchalten(this.SchaltZustand);
		}

		public void BefehlAusfuehren(bool UrsprungsZustand) {
			switch (_attribut) {
				case Elementzustand.Gleich:
					_schaltZustand = UrsprungsZustand;
					break;
				case Elementzustand.Neg:
					_schaltZustand = !UrsprungsZustand;
					break;
			}
			_element.Ausgang.AusgangSchalten(_schaltZustand);
		}
	}
}
