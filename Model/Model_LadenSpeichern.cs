using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Collections.Generic;
using System.Xml.Serialization;

using MoBaSteuerung.Anlagenkomponenten;
using MoBaSteuerung.Anlagenkomponenten.Delegates;
using MoBaSteuerung.Anlagenkomponenten.Enum;
using MoBaSteuerung.Anlagenkomponenten.MCSpeicher;
using MoBaSteuerung;
using MoBaSteuerung.Elemente;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Diagnostics;
using System.ComponentModel;
using System.Threading;
using MoBaSteuerung.ZeichnenElemente;
using MoBa.Elemente;
//using MoBa.Anlagenkomponenten.ZeichnenElemente;

namespace MoBaSteuerung {
	//hier nur Methoden zum Anlage laden und Speichern

	/// <summary>
	/// Anlagenlogik
	/// </summary>
	public partial class Model : Control {
		/// <summary>
		/// Anlagenzustand wird eingelesen und Deserialisiert
		/// </summary>
		/// <param name="anlagenZustandsDaten"></param>
		public void AnlagenZustandsDatenEinlesen(byte[] anlagenZustandsDaten) {
			XmlSerializer formatter = new XmlSerializer(typeof(Anlagenzustand));
			MemoryStream stream = new MemoryStream(anlagenZustandsDaten);
			this._zeichnenElemente.AnlagenZustand = (Anlagenzustand)formatter.Deserialize(stream);

			this._zeichnenElemente.FahrstrassenElemente.AktiveFahrstrassenAktualisieren(this._zeichnenElemente.AnlagenZustand);
		}

		/// <summary>
		/// Der Anlagenzustand wird Serialisiert zurückgegeben
		/// </summary>
		/// <returns></returns>
		public byte[] AnlagenZustandsDatenAuslesen() {
			XmlSerializer serializer = new XmlSerializer(typeof(Anlagenzustand));

			//BinaryFormatter formatter = new BinaryFormatter();
			MemoryStream stream = new MemoryStream();
			//formatter.Serialize(stream, this.zeichnenElemente.AnlagenZustand);
			serializer.Serialize(stream, this._zeichnenElemente.AnlagenZustand);

			return stream.GetBuffer();
		}

		/// <summary>
		/// speichert die gegenwärtigen Anlagenzustand der Anlage in einer Datei
		/// </summary>
		public void AnlagenzustandSpeichern() {
			string dateiPfad = _zeichnenElemente.ZugDateiPfadName;
			if (dateiPfad != "") {
				File.WriteAllBytes(dateiPfad + ".xml", AnlagenZustandsDatenAuslesen());
			}
		}

		/// <summary>
		/// speichert die gegenwärtigen Anlagenzustand der Anlage in einer Datei
		/// </summary>
		public void AnlagenzustandLaden() {
			string dateiPfad = _zeichnenElemente.ZugDateiPfadName;
			if (dateiPfad != "") {
				dateiPfad += ".xml";
				if (File.Exists(dateiPfad)) {
					AnlagenZustandsDatenEinlesen(File.ReadAllBytes(dateiPfad));
				}
				else {
					AnlagenzustandSpeichern();
				}
			}
		}

		/// <summary>
		/// löscht alle gegenwärtige Elemente und 
		/// lädt eine neue Anlage
		/// </summary>
		public void AnlageLaden(string anlageDateiPfadName) {
			this._zeichnenElemente.ZugDateiPfadName = anlageDateiPfadName;
			this.AnlageLaden(this.AnlageDatenEinlesen(anlageDateiPfadName));
			//this.zeichnenElemente.ZugDateiSpeichern();
			this.ZugDateiLaden(anlageDateiPfadName);
			this.AnlagenzustandLaden();
		}

		public byte[] ZugListeAuslesen() {
			string zugDateiName = ZeichnenElemente.ZugDateiPfadName;
			if (!string.IsNullOrEmpty(zugDateiName)) {
				FileStream zugListeFileStream = new FileStream(zugDateiName + ".zug", FileMode.Open, FileAccess.Read, FileShare.Read);

				byte[] anlageBytes = new byte[zugListeFileStream.Length];
				zugListeFileStream.Read(anlageBytes, 0, anlageBytes.Length);

				zugListeFileStream.Close();
				zugListeFileStream.Dispose();

				return anlageBytes;
			}
			return null;
		}

		/// <summary>
		/// Lädt die Anlagedatei ein.
		/// </summary>
		/// <param name="anlageDateiPfadName"></param>
		/// <returns></returns>
		public byte[] AnlageDatenEinlesen(string anlageDateiPfadName) {
			if (!string.IsNullOrEmpty(anlageDateiPfadName)) {
				FileStream anlageFileStream = new FileStream(anlageDateiPfadName, FileMode.Open, FileAccess.Read, FileShare.Read);

				byte[] anlageBytes = new byte[anlageFileStream.Length];
				anlageFileStream.Read(anlageBytes, 0, anlageBytes.Length);

				anlageFileStream.Close();
				anlageFileStream.Dispose();

				return anlageBytes;
			}
			return null;
		}

		/// <summary>
		/// löscht alle gegenwärtige Elemente und 
		/// lädt eine neue Anlage
		/// </summary>
		public void AnlageLaden(byte[] anlageDaten) {
			// this.AnlageZurücksetzen();
			_zeichnenElemente = new AnlagenElemente();
			StreamReader anlageDatenStreamReader = new StreamReader(new MemoryStream(anlageDaten), System.Text.Encoding.UTF8);

			string zeile = "";
			while ((zeile = anlageDatenStreamReader.ReadLine()) != null) {
				string[] elem = zeile.Split('\t');

				if (elem[0] == "Anschluss") {
					new Anschluss(_zeichnenElemente, Constanten.STANDARDRASTER, this._anzeigeTyp, elem);
				}

				if (elem[0] == "MCSpeicher") {
					new MCSpeicher(_zeichnenElemente, Constanten.STANDARDRASTER, this._anzeigeTyp, elem);
				}

				if (elem[0] == "Regler") {
					new Regler(_zeichnenElemente, Constanten.STANDARDRASTER, this._anzeigeTyp, elem);
				}

				if (elem[0] == "Knot") {
					new Knoten(_zeichnenElemente, Constanten.STANDARDRASTER, this._anzeigeTyp, elem);
				}

				if (elem[0] == "Servo") {
					new Servo(_zeichnenElemente, Constanten.STANDARDRASTER, this._anzeigeTyp, elem);
				}
				if (elem[0] == "Gleis") {
					new Gleis(_zeichnenElemente, Constanten.STANDARDRASTER, this._anzeigeTyp, elem);
				}
				if (elem[0] == "Weiche") {
					string[] knot = elem[2].Split(' ');
					Knoten kn = _zeichnenElemente.KnotenElemente.Element(Convert.ToInt32(knot[0]));
					if (kn != null) {
						Weiche we = kn.Weichen[Convert.ToInt32(knot[1])];
						if (we != null) {
							we.Grundstellung = Convert.ToBoolean(elem[3]);
							we.ID = Convert.ToInt32(elem[1]);
							we.Ausgang.SpeicherString = elem[4];
							we.Bezeichnung = elem[5];
							if (elem.Length > 6)
								we.Stecker = elem[6];
							if (elem.Length > 7)
								we.KoppelungsString = elem[7];
						}
					}
				}

				if (elem[0] == "Signal") {
					new Signal(_zeichnenElemente, Constanten.STANDARDRASTER, this._anzeigeTyp, elem);
				}
				if (elem[0] == "SSG") {
					new StartSignalGruppe(_zeichnenElemente, Constanten.STANDARDRASTER, this._anzeigeTyp, elem);
				}
				if (elem[0] == "FSS") {
					new FSS(_zeichnenElemente, Constanten.STANDARDRASTER, this._anzeigeTyp, elem);
				}
				if (elem[0] == "Schalter") {
					new Schalter(_zeichnenElemente, Constanten.STANDARDRASTER, this._anzeigeTyp, elem);
				}
				if (elem[0] == "Entkuppler") {
					new Entkuppler(_zeichnenElemente, Constanten.STANDARDRASTER, this._anzeigeTyp, elem);
				}
				if (elem[0] == "Info") {
					new InfoFenster(_zeichnenElemente, Constanten.STANDARDRASTER, this._anzeigeTyp, elem);
				}
				if (elem[0] == "EingSchalter") {
					new EingangsSchalter(_zeichnenElemente, Constanten.STANDARDRASTER, this._anzeigeTyp, elem);
				}
				if (elem[0] == "Zug") {
					new Zug(_zeichnenElemente, Constanten.STANDARDRASTER, this._anzeigeTyp, elem);
				}
				if (elem[0] == "HS") {
					new Haltestelle(_zeichnenElemente, Constanten.STANDARDRASTER, this._anzeigeTyp, elem);
				}
				if (elem[0] == "FahrstrasseV") {
					string befehleStart = anlageDatenStreamReader.ReadLine();
					string befehleZiel = anlageDatenStreamReader.ReadLine();
					string knotenListe = anlageDatenStreamReader.ReadLine();
					Fahrstrasse neueFahrstrasse = new Fahrstrasse(_zeichnenElemente, Constanten.STANDARDRASTER, this._anzeigeTyp, elem, befehleStart, befehleZiel, knotenListe);
				}
				if (elem[0] == "FahrstrasseN") {
					string befehleStart = anlageDatenStreamReader.ReadLine();
					string befehleZiel = anlageDatenStreamReader.ReadLine();
					string knotenListe = anlageDatenStreamReader.ReadLine();
					new FahrstrasseN(_zeichnenElemente, Constanten.STANDARDRASTER, this._anzeigeTyp, elem, befehleStart, befehleZiel, knotenListe);
				}
				if (elem[0] == "FahrstrasseK") {
					string fsListe = anlageDatenStreamReader.ReadLine();
					string befehle = anlageDatenStreamReader.ReadLine();
					new FahrstrasseK(_zeichnenElemente, Constanten.STANDARDRASTER, this._anzeigeTyp, elem, fsListe, befehle);
				}
			}
			anlageDatenStreamReader.Dispose();

			this._zeichnenElemente.FSSLaden();
			_zeichnenElemente.FSSAktualisieren();
			_zeichnenElemente.KoppelungenAktivieren();
			this.OnAnlageGrößeInRasterChanged(new Size(65, 35));
		}

		public void ZugDateiLaden(string DateiPfadName) {
			try {
				ZeichnenElemente.ZugDateiPfadName = DateiPfadName;
				StreamReader zugDatenStreamReader = new StreamReader(DateiPfadName + ".zug", System.Text.Encoding.UTF8);//(new MemoryStream(anlageDaten), System.Text.Encoding.UTF8);

				ZeichnenElemente.ZugElemente.AlleLöschen();

				string zeile = "";
				while ((zeile = zugDatenStreamReader.ReadLine()) != null) {
					string[] elem = zeile.Split('\t');
					if (elem[0] == "Zug") {
						new Zug(_zeichnenElemente, Constanten.STANDARDRASTER, this._anzeigeTyp, elem);
					}
				}
				zugDatenStreamReader.Dispose();
			}
			catch (FileNotFoundException e) {

			}

		}

		public void ZugDateiLaden(byte[] zugDateiDaten) {
			try {
				StreamReader zugDatenStreamReader = new StreamReader(new MemoryStream(zugDateiDaten), System.Text.Encoding.UTF8);

				ZeichnenElemente.ZugElemente.AlleLöschen();
				string zeile = "";
				while ((zeile = zugDatenStreamReader.ReadLine()) != null) {
					string[] elem = zeile.Split('\t');
					if (elem[0] == "Zug") {
						new Zug(_zeichnenElemente, Constanten.STANDARDRASTER, this._anzeigeTyp, elem);
					}
				}
				zugDatenStreamReader.Dispose();
			}
			catch (FileNotFoundException e) {

			}
		}

		/// <summary>
		/// Neue Anlage
		/// </summary>
		public void AnlageNeu() {
			this.AnlageZurücksetzen();
		}


		/// <summary>
		/// speichert die gegenwärtige Zug-Liste der Anlage in einer Datei
		/// </summary>
		/// <param name="zugDateiPfadName">Der Pfad zu der zu speichernden Zugdatei</param>
		public void zugDateiSpeichern(string zugDateiPfadName) {
			StreamWriter zugStreamWriter = new StreamWriter(zugDateiPfadName + ".zug", false, System.Text.Encoding.UTF8);

			zugStreamWriter.WriteLine(Environment.NewLine + "Züge\tNr.\tSignal\tLok\tTyp\tGeschw\tBez"
																																	 + this._zeichnenElemente.ZugElemente.SpeicherString);
			zugStreamWriter.Flush();
			zugStreamWriter.Dispose();
		}

		/// <summary>
		/// speichert die gegenwärtige Anlage in einer Datei
		/// </summary>
		/// <param name="anlageDateiPfadName">Der Pfad zu der zu speichernden Anlagendatei</param>
		public void AnlageSpeichern(string anlageDateiPfadName) {
			_zeichnenElemente.ZugDateiPfadName = anlageDateiPfadName;
			_zeichnenElemente.ZugDateiSpeichern();
			//zugDateiSpeichern(anlageDateiPfadName);
			string trennung = "--------------------------------------------------------------------------------------------";
			StreamWriter anlageStreamWriter = new StreamWriter(anlageDateiPfadName, false, System.Text.Encoding.UTF8);
			anlageStreamWriter.WriteLine(trennung + Environment.NewLine + "Anschlüsse\tNr.\tBez.\tStecker"
										+ this._zeichnenElemente.AnschlussElemente.SpeicherString);
			anlageStreamWriter.WriteLine(trennung + Environment.NewLine + "Arduinos\tNr.\tLageX\tLageY\tAnl.-Teil"
										+ this._zeichnenElemente.ListeMCSpeicher.SpeicherString);
			anlageStreamWriter.WriteLine(trennung + Environment.NewLine + "Servos\tNr.\tLageX\tLageY\tWinkelE\tWinkelA\tSpeed\tManuell\tAusgang\tBeschr.\tBez\tStecker"
										+ this._zeichnenElemente.ServoElemente.SpeicherString);
			anlageStreamWriter.WriteLine(trennung + Environment.NewLine + "FahrReg\tNr.\tLageX\tLageY\tFarbe\tFarbeZ\tBez\tStecker"
										+ this._zeichnenElemente.ReglerElemente.SpeicherString);
			anlageStreamWriter.WriteLine(trennung + Environment.NewLine + "Knoten\tNr.\tLageX\tLageY"
										+ this._zeichnenElemente.KnotenElemente.SpeicherString);
			anlageStreamWriter.WriteLine(trennung + Environment.NewLine + "Gleise\tNr.\tStartKn\tEndKn\tRegler\tAusgang\tEingang\tBez.\tStecker\tKoppelung"
										+ this._zeichnenElemente.GleisElemente.SpeicherString);
			anlageStreamWriter.WriteLine(trennung + Environment.NewLine + "Weichen\tNr.\tKnoten\tGru-Ste\tAusgang\tBez.\tStecker\tKoppelung"
										+ this._zeichnenElemente.WeicheElemente.SpeicherString);
			anlageStreamWriter.WriteLine(trennung + Environment.NewLine + "GleisSchalter\tNr.\tGleis\tBez.\tKoppelung"
										+ this._zeichnenElemente.SchalterElemente.SpeicherString);
			anlageStreamWriter.WriteLine(trennung + Environment.NewLine + "EingangsSchalter\tNr.\tGleis\tEingang\tBez.\tStecker"
										+ this._zeichnenElemente.EingSchalterElemente.SpeicherString);
			anlageStreamWriter.WriteLine(trennung + Environment.NewLine + "FSSer\tNr.\tGleis\tRegler1\tRegler2\tAusgang\tBez.\tStecker\tKoppelung\tSichtbar"
										+ this._zeichnenElemente.FssElemente.SpeicherString);
			anlageStreamWriter.WriteLine(trennung + Environment.NewLine + "Entkuppler_\tNr.\tGleis\tAusgang\tBez.\tStecker"
										+ this._zeichnenElemente.EntkupplerElemente.SpeicherString);
			anlageStreamWriter.WriteLine(trennung + Environment.NewLine + "Infos\tNr.\tGleis\tLage\tBez"
										+ this._zeichnenElemente.InfoElemente.SpeicherString);
			anlageStreamWriter.WriteLine(trennung + Environment.NewLine + "Signale\tNr.\tGleis\tRichtu.\tInfoF\tAusgang\tBez.\tStecker\tAutoSta\tZugLmax\tZugTyp\tKopplg.\tZielGleis"
										+ this._zeichnenElemente.SignalElemente.SpeicherString);
			anlageStreamWriter.WriteLine(trennung + Environment.NewLine + "StartSG\tNr.\tLageX\tLageY\tTyp\tBez.\tSignale"
										+ this._zeichnenElemente.SsgElemente.SpeicherString);
			anlageStreamWriter.WriteLine(trennung + Environment.NewLine + "HStelln\tNr.\tInfFeld\tBez"
										+ this._zeichnenElemente.HaltestellenElemente.SpeicherString);
			anlageStreamWriter.WriteLine(trennung + Environment.NewLine + this._zeichnenElemente.FahrstrassenElemente.SpeicherString);
			anlageStreamWriter.WriteLine(trennung + Environment.NewLine + "FahrstrassenK\tNr."
										+ this._zeichnenElemente.FahrstrassenKElemente.SpeicherString);
			anlageStreamWriter.WriteLine(trennung + Environment.NewLine + "Züge\tNr.\tSignal\tLok\tTyp\tGeschw\tBez\tZLänge\tDigAdr.\tAnkunft"
																																	 //1=ID, 2=Signal, 3=Lok, 4=Typ, 5=Speed, 6=Bezeichnung, 7=Länge, 8=DigitalAdresse, 9=AnkunftsZeit
																																	 + this._zeichnenElemente.ZugElemente.SpeicherString);
			anlageStreamWriter.Flush();
			anlageStreamWriter.Dispose();
		}

		/// <summary>
		/// 
		/// </summary>
		public void AnlageZurücksetzen() {
			NeuesElementVorschauReset();
			_aktuellerBefehl = null;
			this._zeichnenElemente.AlleLöschen();
		}

	}
}
