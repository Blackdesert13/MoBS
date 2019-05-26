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
			this.zeichnenElemente.AnlagenZustand = (Anlagenzustand)formatter.Deserialize(stream);

			this.zeichnenElemente.FahrstrassenElemente.AktiveFahrstrassenAktualisieren(this.zeichnenElemente.AnlagenZustand);
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
			serializer.Serialize(stream, this.zeichnenElemente.AnlagenZustand);

			return stream.GetBuffer();
		}


		/// <summary>
		/// löscht alle gegenwärtige Elemente und 
		/// lädt eine neue Anlage
		/// </summary>
		public void AnlageLaden(string anlageDateiPfadName) {
			this.zeichnenElemente.ZugDateiPfadName = anlageDateiPfadName;
			this.AnlageLaden(this.AnlageDatenEinlesen(anlageDateiPfadName));
			//this.zeichnenElemente.ZugDateiSpeichern();
			this.ZugDateiLaden(anlageDateiPfadName);
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
			zeichnenElemente = new AnlagenElemente();
			StreamReader anlageDatenStreamReader = new StreamReader(new MemoryStream(anlageDaten), System.Text.Encoding.UTF8);

			string zeile = "";
			while ((zeile = anlageDatenStreamReader.ReadLine()) != null) {
				string[] elem = zeile.Split('\t');

				if (elem[0] == "Anschluss") {
					new Anschluss(zeichnenElemente, Constanten.STANDARDRASTER, this.anzeigeTyp, elem);
				}

				if (elem[0] == "MCSpeicher") {
					new MCSpeicher(zeichnenElemente, Constanten.STANDARDRASTER, this.anzeigeTyp, elem);
				}

				if (elem[0] == "Regler") {
					new Regler(zeichnenElemente, Constanten.STANDARDRASTER, this.anzeigeTyp, elem);
				}

				if (elem[0] == "Knot") {
					new Knoten(zeichnenElemente, Constanten.STANDARDRASTER, this.anzeigeTyp, elem);
				}

				if (elem[0] == "Servo") {
					new Servo(zeichnenElemente, Constanten.STANDARDRASTER, this.anzeigeTyp, elem);
				}

				if (elem[0] == "Gleis") {
					new Gleis(zeichnenElemente, Constanten.STANDARDRASTER, this.anzeigeTyp, elem);
				}

				if (elem[0] == "Weiche") {
					string[] knot = elem[2].Split(' ');
					Knoten kn = zeichnenElemente.KnotenElemente.Element(Convert.ToInt32(knot[0]));
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
					new Signal(zeichnenElemente, Constanten.STANDARDRASTER, this.anzeigeTyp, elem);
				}

				if (elem[0] == "FSS") {
					new FSS(zeichnenElemente, Constanten.STANDARDRASTER, this.anzeigeTyp, elem);
				}
				if (elem[0] == "Schalter") {
					new Schalter(zeichnenElemente, Constanten.STANDARDRASTER, this.anzeigeTyp, elem);
				}

				if (elem[0] == "Entkuppler") {
					new Entkuppler(zeichnenElemente, Constanten.STANDARDRASTER, this.anzeigeTyp, elem);
				}

				if (elem[0] == "Info") {
					new InfoFenster(zeichnenElemente, Constanten.STANDARDRASTER, this.anzeigeTyp, elem);
				}

				if (elem[0] == "Zug") {
					new Zug(zeichnenElemente, Constanten.STANDARDRASTER, this.anzeigeTyp, elem);
				}

				if (elem[0] == "HS") {
					new Haltestelle(zeichnenElemente, Constanten.STANDARDRASTER, this.anzeigeTyp, elem);
				}

				if (elem[0] == "FahrstrasseV") {
					string befehleStart = anlageDatenStreamReader.ReadLine();
					string befehleZiel = anlageDatenStreamReader.ReadLine();
					string knotenListe = anlageDatenStreamReader.ReadLine();
					Fahrstrasse neueFahrstrasse = new Fahrstrasse(zeichnenElemente, Constanten.STANDARDRASTER, this.anzeigeTyp, elem, befehleStart, befehleZiel, knotenListe);
				}
				if (elem[0] == "FahrstrasseN") {
					string befehleStart = anlageDatenStreamReader.ReadLine();
					string befehleZiel = anlageDatenStreamReader.ReadLine();
					string knotenListe = anlageDatenStreamReader.ReadLine();
					new FahrstrasseN(zeichnenElemente, Constanten.STANDARDRASTER, this.anzeigeTyp, elem, befehleStart, befehleZiel, knotenListe);
				}
			}
			anlageDatenStreamReader.Dispose();

			this.zeichnenElemente.FSSLaden();
			zeichnenElemente.FSSAktualisieren();
			zeichnenElemente.KoppelungenAktivieren();
			this.OnAnlageGrößeInRasterChanged(new Size(65, 35));
		}

		public void ZugDateiLaden(string DateiPfadName) {
			try {
				StreamReader ZugDatenStreamReader = new StreamReader(DateiPfadName + ".zug", System.Text.Encoding.UTF8);//(new MemoryStream(anlageDaten), System.Text.Encoding.UTF8);

				string zeile = "";
				while ((zeile = ZugDatenStreamReader.ReadLine()) != null) {
					string[] elem = zeile.Split('\t');
					if (elem[0] == "Zug") {
						new Zug(zeichnenElemente, Constanten.STANDARDRASTER, this.anzeigeTyp, elem);
					}
				}
				ZugDatenStreamReader.Dispose();
			}
			catch(FileNotFoundException e) {

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
																																	 + this.zeichnenElemente.ZugElemente.SpeicherString);
			zugStreamWriter.Flush();
			zugStreamWriter.Dispose();
		}

		/// <summary>
		/// speichert die gegenwärtige Anlage in einer Datei
		/// </summary>
		/// <param name="anlageDateiPfadName">Der Pfad zu der zu speichernden Anlagendatei</param>
		public void AnlageSpeichern(string anlageDateiPfadName) {
			zeichnenElemente.ZugDateiPfadName = anlageDateiPfadName;
			zeichnenElemente.ZugDateiSpeichern();
			//zugDateiSpeichern(anlageDateiPfadName);
			string trennung = "--------------------------------------------------------------------------------------------";
			StreamWriter anlageStreamWriter = new StreamWriter(anlageDateiPfadName, false, System.Text.Encoding.UTF8);
			anlageStreamWriter.WriteLine(trennung + Environment.NewLine + "Anschlüsse\tNr.\tBez.\tStecker"
																																			+ this.zeichnenElemente.AnschlussElemente.SpeicherString);
			anlageStreamWriter.WriteLine(trennung + Environment.NewLine + "Arduinos\tNr.\tLageX\tLageY\tAnl.-Teil"
																																			+ this.zeichnenElemente.ListeMCSpeicher.SpeicherString);
			anlageStreamWriter.WriteLine(trennung + Environment.NewLine + "Servos\tNr.\tLageX\tLageY\tWinkelE\tWinkelA\tSpeed\tManuell\tAusgang\tBeschr.\tBez\tStecker"
																																			+ this.zeichnenElemente.ServoElemente.SpeicherString);
			anlageStreamWriter.WriteLine(trennung + Environment.NewLine + "FahrReg\tNr.\tLageX\tLageY\tFarbe\tFarbeZ\tBez\tStecker"
																																			+ this.zeichnenElemente.ReglerElemente.SpeicherString);
			anlageStreamWriter.WriteLine(trennung + Environment.NewLine + "Knoten\tNr.\tLageX\tLageY"
																																			+ this.zeichnenElemente.KnotenElemente.SpeicherString);
			anlageStreamWriter.WriteLine(trennung + Environment.NewLine + "Gleise\tNr.\tStartKn\tEndKn\tRegler\tAusgang\tEingang\tBez.\tStecker"
																																			+ this.zeichnenElemente.GleisElemente.SpeicherString);
			anlageStreamWriter.WriteLine(trennung + Environment.NewLine + "Weichen\tNr.\tKnoten\tGru-Ste\tAusgang\tBez.\tStecker\tKoppelung"
																																			+ this.zeichnenElemente.WeicheElemente.SpeicherString);
			anlageStreamWriter.WriteLine(trennung + Environment.NewLine + "GleisSchalter\tNr.\tGleis\tBez.\tKoppelung"
																																			+ this.zeichnenElemente.SchalterElemente.SpeicherString);
			anlageStreamWriter.WriteLine(trennung + Environment.NewLine + "FSSer\tNr.\tGleis\tRegler1\tRegler2\tAusgang\tBez.\tStecker\tKoppelung"
																																			+ this.zeichnenElemente.FssElemente.SpeicherString);
			anlageStreamWriter.WriteLine(trennung + Environment.NewLine + "Entkuppler_\tNr.\tGleis\tAusgang\tBez.\tStecker"
																																			+ this.zeichnenElemente.EntkupplerElemente.SpeicherString);
			anlageStreamWriter.WriteLine(trennung + Environment.NewLine + "Infos\tNr.\tGleis\tLage\tBez"
																																			+ this.zeichnenElemente.InfoElemente.SpeicherString);
			anlageStreamWriter.WriteLine(trennung + Environment.NewLine + "Signale\tNr.\tGleis\tRichtu.\tInfoF\tAusgang\tBez.\tStecker\tAutostart\tZugLmax"
																																			+ this.zeichnenElemente.SignalElemente.SpeicherString);
			anlageStreamWriter.WriteLine(trennung + Environment.NewLine + "HStelln\tNr.\tInfFeld\tBez"
																																			+ this.zeichnenElemente.HaltestellenElemente.SpeicherString);

			anlageStreamWriter.WriteLine(trennung + Environment.NewLine + this.zeichnenElemente.FahrstrassenElemente.SpeicherString);

			anlageStreamWriter.WriteLine(trennung + Environment.NewLine + "Züge\tNr.\tSignal\tLok\tTyp\tGeschw\tBez\tZLänge\tDigAdr.\tAnkunft"
																																	 //1=ID, 2=Signal, 3=Lok, 4=Typ, 5=Speed, 6=Bezeichnung, 7=Länge, 8=DigitalAdresse, 9=AnkunftsZeit
																																	 + this.zeichnenElemente.ZugElemente.SpeicherString);
			anlageStreamWriter.Flush();
			anlageStreamWriter.Dispose();
		}

		/// <summary>
		/// 
		/// </summary>
		public void AnlageZurücksetzen() {
			NeuesElementVorschauReset();
			_aktuellerBefehl = null;
			this.zeichnenElemente.AlleLöschen();
		}

	}
}
