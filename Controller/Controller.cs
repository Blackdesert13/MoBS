using System;
using MoBaSteuerung.Anlagenkomponenten;
using MoBaSteuerung.Anlagenkomponenten.Enum;
using System.Drawing;
using MoBaSteuerung.Anlagenkomponenten.Delegates;
using System.IO;
using System.Drawing.Drawing2D;
using System.Diagnostics;
using System.Collections.Generic;
using System.Windows.Forms;
using MoBaSteuerung.Elemente;
using MoBaSteuerung.ZeichnenElemente;
using MoBaSteuerung.Anlagenkomponenten.MCSpeicher;

namespace MoBaSteuerung
{
	/// <summary>
	/// 
	/// </summary>
	public partial class Controller
	{
		#region Private Felder

		/// <summary>
		/// Model
		/// </summary>
		private Model _model;



		/// <summary>
		/// Programmtyp Master oder Slave
		/// </summary>
		private AppTyp appTyp;

		/// <summary>
		/// Anzeigetyp Bedienen oder Bearbeiten
		/// </summary>
		private AnzeigeTyp anzeigeTyp;

		/// <summary>
		/// View Zoom
		/// </summary>
		private Int32 zoom;

		/// <summary>
		/// Zoomfaktor
		/// </summary>
		private decimal zoomFaktor;


		/// <summary>
		/// Zoomfaktor vor Änderung
		/// </summary>
		private decimal zoomFaktorVorÄnderung;

		/// <summary>
		/// Anlagengröße
		/// </summary>
		private Size anlageGrößeInRaster;

		/// <summary>
		/// Standardgröße der Anlage
		/// </summary>
		private Size anzeigeGrößeInPixel;


		private MoBaKommunikation.Master master;


		private MoBaKommunikation.Slave slave;


		#endregion

		#region Öffentliche Eigenschaften (Properties)

		/// <summary>
		/// Programmtyp.
		/// </summary>
		public AppTyp AppTyp
		{
			get {
				return this.appTyp;
			}

			set {
				this.appTyp = value;
			}
		}

		/// <summary>
		/// alle AnlagenElementListen
		/// </summary>
		public AnlagenElemente ZeichnenElemente { get { return _model.ZeichnenElemente; } }


		/// <summary>
		/// Mit Client verbinden.
		/// </summary>
		public bool MitClientsVerbunden
		{
			get {
				// ToDo Clientanzahl
				return false;
			}
		}

		/// <summary>
		/// Mit dem Server verbunden.
		/// </summary>
		public bool MitServerVerbunden
		{
			get {
				// ToDo
				return false;
			}
		}

		/// <summary>
		/// Anzeigetyp.
		/// </summary>
		public AnzeigeTyp AnzeigeTyp
		{
			get {
				return this.anzeigeTyp;
			}

			set {
				this.anzeigeTyp = value;
				this._model.Anzeigetyp = this.anzeigeTyp;
				this.OnViewTypeChanged(this.anzeigeTyp);
			}
		}

		/// <summary>
		/// Zoom
		/// </summary>
		public int Zoom
		{
			get {
				return this.zoom;
			}
			set {
				this.zoom = value;
				if (this.zoom < 5) {
					this.zoom = 5;
				}
				// Faktor berechnen
				this._model.Zoom = this.zoom;
				this.zoomFaktorVorÄnderung = this.zoomFaktor;
				this.zoomFaktor = (decimal)this.zoom / (decimal)Constanten.STANDARDRASTER;
				this.anzeigeGrößeInPixel = new Size(this.AnlageGrößeInRaster.Width * this.zoom, this.AnlageGrößeInRaster.Height * this.zoom);
				this.OnZoomChanged(this.zoom);
			}
		}

		/// <summary>
		/// Zoomfaktor
		/// </summary>
		public decimal ZoomFaktor
		{
			get {
				return this.zoomFaktor;
			}
		}

		/// <summary>
		/// ZoomfaktorVorÄnderung
		/// </summary>
		public decimal ZoomFaktorVorÄnderung
		{
			get {
				return this.zoomFaktorVorÄnderung;
			}
		}

		/// <summary>
		/// Gibt die Anlagengröße zurück.
		/// </summary>
		public Size AnlageGrößeInRaster
		{
			get {
				if (anlageGrößeInRaster.Width != 0 && anlageGrößeInRaster.Height != 0)
					return this.anlageGrößeInRaster;
				else
					return Constanten.STANDARDANLAGENGRÖßEINRASTER;
			}
			set {
				this.anlageGrößeInRaster = value;
				this.anzeigeGrößeInPixel = new Size(this.AnlageGrößeInRaster.Width * this.zoom, this.AnlageGrößeInRaster.Height * this.zoom);
				this.OnAnlageGrößeInRasterChanged(value);
			}
		}

		/// <summary>
		/// Größe der Anzeige in Pixel.
		/// </summary>
		public Size AnzeigeGrößeInPixel
		{
			get {
				return this.anzeigeGrößeInPixel;
			}

			set {
				this.anzeigeGrößeInPixel = value;
			}
		}

		#endregion

		#region Konstruktor(en)

		/// <summary>
		/// 
		/// </summary>
		public Controller(Model model)
		{
			this._model = model;
			InizializeModel();
		}



		#endregion

		#region Öffentliche Methoden


		public int BedienenMouseDoubleClick(Point location)
		{
			if (this.AppTyp == AppTyp.Master) {
				return _model.BedienenMouseDoubleClick(location);
			}
			return -1;
		}

		public int BearbeitenenMouseDoubleClick(Point location)
		{
			_model.BearbeitenMouseDoubleClick(location);
		
			return 0;//_model.BearbeitenMouseDoubleClick(location);
		}

		private void Model_ZubehoerServoAction(int servo, ServoAction action)
		{
			if (this.AppTyp == AppTyp.Master) {
				if (_model.ServoBewegungManuell(servo, action)) {
					//if (master != null) {
					//    master.SendeAnlageZustandsDatenAnAlle(this._model.AnlagenZustandsDatenAuslesen());
					//}
				}
			}
			if (this.AppTyp == AppTyp.Slave) {
				if (slave != null) {
					//slave.SlaveAnMasterMouseClick(el[0].GetType().Name, el[0].ID);
				}
			}
		}

		public bool BedienenMouseClick(Point p, MouseButtons button, Keys modifierKeys)
		{
			List<Elemente.AnlagenElement> el;
			switch (button) {
				case MouseButtons.Left:

					el = _model.BedienenMouseClick(p);
					if (el.Count > 0) {
						if (this.AppTyp == AppTyp.Master) {
							Logging.Log.Schreibe("Master Element Schalten: " + el[0].GetType().Name + " " + el[0].ID, LogLevel.Trace);
							//if (_model.ElementToggeln(el[0].GetType().Name, el[0].ID)) {
							//	if (master != null) {
							//		master.SendeAnlageZustandsDatenAnAlle(this._model.AnlagenZustandsDatenAuslesen());
							//	}
							//}
							_model.ElementToggeln(el[0].GetType().Name, el[0].ID);
						}
						if (this.AppTyp == AppTyp.Slave) {
							if (slave != null) {
								slave.SlaveAnMasterMouseClick(el[0].GetType().Name, el[0].ID);
							}
						}
						return false;
					}
					break;
				case MouseButtons.Right:
					bool shift = false;
					if (modifierKeys == Keys.Shift) {
						shift = true;
					}

					el = _model.BedienenMouseRightClick(p);
					if (el != null) {
						if (el.Count > 0)
							if (el[0].GetType().Name == "Signal") {
								return FahrstrassenSignal(el[0].ID, shift);
							}
						//if (el.Count == 1) {
						//	if (((FahrstrasseN)el[0]).StartSignal.ID == signalNummer)
						//		return FahrstrasseSchalten((FahrstrasseN)el[0], FahrstrassenSignalTyp.StartSignal);
						//	else if (((FahrstrasseN)el[0]).EndSignal.ID == signalNummer)
						//		return FahrstrasseSchalten((FahrstrasseN)el[0], FahrstrassenSignalTyp.ZielSignal);
						//}
						//                  else {
						//                      this.model.FahrstrassenAuswahl(el);
						//	return true;
						//                  }
					}
					break;
			}
			return false;
		}

		public bool FahrstrassenSignal(int signalNummer, bool shift)
		{
			List<Elemente.AnlagenElement> el = _model.FahrstrassenSignal(signalNummer,shift);
			if (el != null) {
				if (el.Count == 1) {
					if (((FahrstrasseN)el[0]).StartSignal.ID == signalNummer)
						return FahrstrasseSchalten((FahrstrasseN)el[0], FahrstrassenSignalTyp.StartSignal,shift);
					else if (((FahrstrasseN)el[0]).EndSignal.ID == signalNummer)
						return FahrstrasseSchalten((FahrstrasseN)el[0], FahrstrassenSignalTyp.ZielSignal, shift);
				}
				else {
					this._model.FahrstrassenAuswahl(el);
					return true;
				}
			}
			return false;
		}

		public bool FahrstrasseSchalten(FahrstrasseN el, FahrstrassenSignalTyp signal, bool verlaengern)
		{
			bool action = false;
			if (this.AppTyp == AppTyp.Master) {
				_model.ElementToggeln(
					signal == FahrstrassenSignalTyp.StartSignal ? "FahrstrasseN_Start" : "FahrstrasseN_Ziel", el.ID
					);
				//action = _model.FahrstrasseSchalten(el, signal);
				//if (master != null) {
				//	master.SendeAnlageZustandsDatenAnAlle(this._model.AnlagenZustandsDatenAuslesen());
				//}
			}
			if (this.AppTyp == AppTyp.Slave) {
				if (signal == FahrstrassenSignalTyp.ZielSignal) {
					if (slave != null) {
						slave.SlaveAnMasterMouseClick(el.GetType().Name + "_Ziel", el.ID);
					}
				}
				else {
					if (slave != null) {
						slave.SlaveAnMasterMouseClick(el.GetType().Name + "_Start", el.ID);
					}
				}
				return true;
			}
			return action;
		}

		public void BearbeitenDragDrop(Point deltaRaster, DragDropEffects effect)
		{
			this._model.BearbeitenDragDrop(deltaRaster, effect);
		}

		public void BearbeitenDragDropAbschließen(DragDropEffects effect)
		{
			this._model.BearbeitenDragDropAbschließen(effect);
		}

		/// <summary>
		/// Anlage zurück setzen.
		/// </summary>
		public void AnlageNeu()
		{
			// setzen auf Master
			this.AnlageAusgangsZustand(AppTyp.Master);
			// neue Anlage, also alles leeren.    
			this._model.AnlageNeu();
			// Pfadname zurücksetzen
			this._model.AnlageDateiPfadName = "";
			// StandardName übergeben
			this.OnFileNew(this._model.AnlageDateiName);
		}

		/// <summary>
		/// Anlage laden.
		/// </summary>
		/// <param name="anlageName"></param>
		/// <returns></returns>
		public void AnlageLaden(string anlageName)
		{
			// setzen auf Master
			this.AnlageAusgangsZustand(AppTyp.Master);
			// Anlage laden.
			this._model.AnlageLaden(anlageName);
			// Name übergeben
			this._model.AnlageDateiPfadName = anlageName;
			this._model.Zoom = this.zoom;
			// geladenen Namen zurückgeben
			this.OnFileLoaded(this._model.AnlageDateiPfadName);

		}

		/// <summary>
		/// Anlage schließen.
		/// </summary>
		/// <returns></returns>
		public void AnlageSchließen()
		{
			// setzen auf Undefiniert
			this.AnlageAusgangsZustand(AppTyp.Undefiniert);
			// Alles leeren.
			this._model.AnlageZurücksetzen();
			// Pfadname zurücksetzen
			this._model.AnlageDateiPfadName = "";
		}

		/// <summary>
		/// Anlage speichern.
		/// </summary>
		public void AnlageSpeichern()
		{
			// setzen auf Slave
			//this.AnlageAusgangsZustand(AppTyp.Slave);
			// Anlage speichern
			this._model.AnlageSpeichern(this._model.AnlageDateiPfadName);
			// als gespeichert markieren: false ist gespeichert, true müsste gespeichert werden
			this.OnFileSave(false);
		}

		/// <summary>
		/// Anlage speichern unter.
		/// </summary>
		public void AnlageSpeichernUnter(string dateiPfadName)
		{
			// Pfad übergabe und speichern
			this._model.AnlageDateiPfadName = dateiPfadName;
			// speichern
			this.AnlageSpeichern();
			// neuen Namen zurückgeben
			this.OnFileSaved(this._model.AnlageDateiPfadName);
		}

		/// <summary>
		/// Mit Master verbinden.
		/// </summary>
		/// <param name="masterName"></param>
		/// <returns></returns>
		public void SlaveMitMasterVerbinden(string masterName)
		{

			// auf Slave setzen
			this.AnlageAusgangsZustand(AppTyp.Slave);
			// ToDo Verbinden mit Master
			this.slave = new MoBaKommunikation.Slave("MoBaSteuerungSlave");
			this.slave.MasterAnlageDatenEventHandler += Slave_MasterAnlageDatenEventHandler;
			this.slave.MasterAnlagenZustandsDatenEventHandler += Slave_MasterAnlagenZustandsDatenEventHandler;
			this.slave.MasterZugListenDatenEventHandler += Slave_MasterZugListenDatenEventHandler;

			this.slave.Start(masterName, 55555, "MoBaSteuerung", "Test");


			// Masternamen zurück geben
			this.OnMasterConnected(masterName);
		}

		private void Slave_MasterZugListenDatenEventHandler(byte[] e) {
			_model.ZugDateiLaden(e);
			OnViewNeuZeichnen();
		}

		private void Slave_MasterAnlagenZustandsDatenEventHandler(byte[] e)
		{
			_model.AnlagenZustandsDatenEinlesen(e);
			OnViewNeuZeichnen();
		}

		private void Slave_MasterAnlageDatenEventHandler(byte[] e)
		{
			this._model.AnlageLaden(e);
			this._model.Zoom = this.zoom;
			OnViewNeuZeichnen();
		}

		/// <summary>
		/// Vom Master trennen.
		/// </summary>
		/// <returns></returns>
		public bool SlaveVomMasterTrennen()
		{
			// vom Master Trennen
			// ToDo

			this.slave.Stop();
			this.slave.Dispose();
			this.slave = null;

			// setzen auf Undefiniert
			this.AnlageAusgangsZustand(AppTyp.Undefiniert);
			return true;
		}

		/// <summary>
		/// View auf Standardzoom setzen.
		/// </summary>
		public void ZoomStandard()
		{
			this.Zoom = Constanten.STANDARDRASTER;
		}

		/// <summary>
		/// Elemente zeichnen Bedienmodus.
		/// </summary>
		/// <param name="graphics"></param>
		public void ZeichneElementeBedienen(Graphics graphics)
		{
			this._model.ZeichneElementeBedienen(graphics);
		}



		/// <summary>
		/// Elemente zeichnen Bedarbeitungsmodus.
		/// </summary>
		/// <param name="graphics"></param>
		public void ZeichneElementeBearbeiten(Graphics graphics)
		{
			this._model.ZeichneElementeBearbeiten(graphics);
		}

		/// <summary>
		/// Abfrage, ob Elemente im Auswahlrechteck vorhanden sind.
		/// </summary>
		/// <param name="graphicsPath"></param>
		/// <returns></returns>
		public bool AuswahlRechteckElemente(GraphicsPath graphicsPath)
		{
			return this._model.AuswahlRechteckElemente(graphicsPath);
		}

		/// <summary>
		/// Alle Elemente im Auswahlrechteck verschieben.
		/// </summary>
		/// <param name="graphicsPath"></param>
		public void AuswahlRechteckVerschieben(GraphicsPath graphicsPath)
		{
			this._model.AuswahlRechteckVerschieben(graphicsPath);
		}

		/// <summary>
		/// Alle Elemente im Auswahlrechteck löschen.
		/// </summary>
		/// <param name="graphicsPath"></param>
		public void AuswahlRechteckElementeLöschen(GraphicsPath graphicsPath)
		{
			this._model.AuswahlRechteckElementeLöschen(graphicsPath);
		}

		/// <summary>
		/// Aktuelle Uhrzeit.
		/// </summary>
		/// <returns></returns>
		public DateTime AktuelleZeit()
		{
			return DateTime.Now;
		}

		#endregion

		#region Private Methoden

		private void InizializeModel()
		{
			this._model.AnlageNeuZeichnen += Model_AnlageNeuZeichnen;
			this._model.AnlageGrößeInRasterChanged += Model_AnlageGrößeInRasterChanged;
			this._model.AnlagenzustandChanged += Model_AnlagenzustandAdresseChanged;
			this._model.ZugListeChanged += Model_ZugListeChanged;
			this._model.ZubehoerServoAction += Model_ZubehoerServoAction;
			this.AnlageAusgangsZustand(AppTyp.Undefiniert);
		}


		public void StartServer(string remoteID, Int32 iPPort)
		{

			try {
				this.master = new MoBaKommunikation.Master("MoBaSteuerungMaster");
				this.master.SlaveAbmeldenClickEventHandler += Master_SlaveAbmeldenClickEventHandler;
				this.master.SlaveAnmeldenClickEventHandler += Master_SlaveAnmeldenClickEventHandler;
				this.master.SlaveMouseClickEventHandler += Master_SlaveMouseClickEventHandler;

				this.master.Start(remoteID, iPPort);
			}
			catch (Exception ex) {

				Debug.Print("master.Start: " + ex.Message);
			}
		}

		public void StopServer()
		{
			try {
				this.master.Stop();

				this.master.SlaveAbmeldenClickEventHandler -= Master_SlaveAbmeldenClickEventHandler;
				this.master.SlaveAnmeldenClickEventHandler -= Master_SlaveAnmeldenClickEventHandler;
				this.master.SlaveMouseClickEventHandler -= Master_SlaveMouseClickEventHandler;
				this.master.Dispose();
				this.master = null;

			}
			catch (Exception ex) {

				Debug.Print("master.Stop: " + ex.Message);
			}



		}

		private void Model_AnlageNeuZeichnen()
		{
			this.OnViewNeuZeichnen();
		}

		private void Model_AnlageGrößeInRasterChanged(Size e)
		{
			this.AnlageGrößeInRaster = e;
		}

		private void Model_AnlagenzustandAdresseChanged(Adresse adresse)
		{
			if (master != null) {
				master.SendeAnlageZustandsDatenAnAlle(this._model.AnlagenZustandsDatenAuslesen());
			}
		}
		
		public void Model_ZugListeChanged() {
			if(master != null) {
				master.SendeZugListeAnAlle(this._model.ZugListeAuslesen());
			}
		}

		private void AnlageAusgangsZustand(AppTyp appTyp)
		{
			// Model zurücksetzen
			this._model.AnlageZurücksetzen();
			// Controller auf Standard zurück
			this.ZoomStandard();
			this.AppTyp = appTyp;
			this.AnzeigeTyp = AnzeigeTyp.Bedienen;
			this._model.IstAnlageSpeichernErforderlich = false;
			this._model.AnlageDateiPfadName = "";
		}

		#endregion

		#region Events

		#region EeventHandler

		/// <summary>
		/// 
		/// </summary>
		public event StringEventHandler FileNew;

		/// <summary>
		/// 
		/// </summary>
		public event StringEventHandler FileLoaded;

		/// <summary>
		/// 
		/// </summary>
		public event BoolEventHandler FileSave;

		/// <summary>
		/// 
		/// </summary>
		public event StringEventHandler FileSaved;

		/// <summary>
		/// 
		/// </summary>
		public event StringEventHandler MasterConnected;

		/// <summary>
		/// Event zum Zoomänderung.
		/// </summary>
		public event Int32EventHandler ZoomChanged;

		/// <summary>
		/// 
		/// </summary>
		public event SizeEventHandler AnlageGrößeInRasterChanged;

		/// <summary>
		/// 
		/// </summary>
		public event AppTypEventHandler AppTypChanged;

		/// <summary>
		/// 
		/// </summary>
		public event ViewTypEventHandler ViewTypChanged;

		/// <summary>
		/// 
		/// </summary>
		public event ViewNeuZeichnenEventHandler ViewNeuZeichnen;

		#endregion

		/// <summary>
		/// Wird ausgelösst, wenn eine Anlage geladen wurde.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnFileLoaded(string e)
		{
			if (this.FileLoaded != null)
				this.FileLoaded(e);
		}

		/// <summary>
		/// Wird ausgelösst, wenn die Anlage gespeichert wird.
		/// </summary>
		/// <param name="e"></param>
		public virtual void OnFileSave(bool e)
		{
			if (this.FileSave != null)
				this.FileSave(e);
		}

		/// <summary>
		/// Wird ausgelösst, wenn die Anlage gespeichert wurde.
		/// </summary>
		/// <param name="e"></param>
		public virtual void OnFileSaved(string e)
		{
			if (this.FileSaved != null)
				this.FileSaved(e);
		}

		/// <summary>
		/// Wird ausgelösst, wenn eine neue Anlage erzeugt wurde.
		/// </summary>
		/// <param name="e"></param>
		public virtual void OnFileNew(string e)
		{
			if (this.FileNew != null)
				this.FileNew(e);
		}

		/// <summary>
		/// Wird ausgelösst, wenn mit einem Server verbunden.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnMasterConnected(string e)
		{
			if (this.MasterConnected != null)
				this.MasterConnected(e);
		}

		/// <summary>
		/// Wird ausgelösst, wenn der Zoom geändert wurde.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnZoomChanged(Int32 e)
		{
			if (this.ZoomChanged != null)
				this.ZoomChanged(e);
		}

		/// <summary>
		/// Wird ausgelösst, wenn die Größe geändert wurde.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnAnlageGrößeInRasterChanged(Size e)
		{
			if (this.AnlageGrößeInRasterChanged != null) {
				this.AnlageGrößeInRasterChanged(e);
			}
		}

		/// <summary>
		/// Wird ausgelösst, wenn der Programmtyp geändert wurde.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnAppTypChanged(AppTyp e)
		{
			if (this.AppTypChanged != null)
				this.AppTypChanged(e);
		}

		/// <summary>
		/// Wird ausgelösst, wenn der Anzeigetyp geändert wurde.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnViewTypeChanged(AnzeigeTyp e)
		{
			if (this.ViewTypChanged != null)
				this.ViewTypChanged(e);
		}

		/// <summary>
		/// Wird ausgelösst, wenn neu gezeichnet werden müßte.
		/// </summary>
		protected virtual void OnViewNeuZeichnen()
		{
			if (this.ViewNeuZeichnen != null) {
				this.ViewNeuZeichnen();
			}
		}

		#endregion

		#region MoBaKommunikation

		#region Empfang vom Slave

		private void Master_SlaveMouseClickEventHandler(string elementType, int id)
		{
			Debug.Print("Master SlaveClick: " + elementType + " " + id);
			this._model.ElementToggeln(elementType, id);
			//if (this._model.ElementToggeln(elementType, id)) {
			//	master.SendeAnlageZustandsDatenAnAlle(_model.AnlagenZustandsDatenAuslesen());
			//	this.OnViewNeuZeichnen();
			//}
		}

		private void Master_SlaveAnmeldenClickEventHandler(MoBaKommunikation.SlaveClient slaveClient)
		{
			Debug.Print("Master SlaveAnmelden " + slaveClient.SlaveDNS + ":" + slaveClient.SlavePort.ToString());
			try {
				slaveClient.SendenZumSlave.AnlageDaten(this._model.AnlageDatenEinlesen(this._model.AnlageDateiPfadName));
				Model_AnlagenzustandAdresseChanged(null);
				Model_ZugListeChanged();
				//this.master.SendeAnlageZuSlave(slaveClient.SlaveDNS, this.model.AnlageDatenEinlesen(this.anlageDateiPfadName));
			}
			catch (Exception ex) {

				Debug.Print("Master Slave Anmeldung: " + ex.Message);
			}
		}

		private void Master_SlaveAbmeldenClickEventHandler(MoBaKommunikation.SlaveClient slaveClient)
		{
			Debug.Print("Master SlaveAbmelden: " + slaveClient.SlaveDNS);
		}

		#endregion

		#region senden zum Slave

		private void TestSenden()
		{
			this.master.SendeAnlageZustandsDatenAnAlle(new byte[] { 0, 1, 2, 3, 4 });
		}

		#endregion

		#endregion

		#region neu

		public string[] GetSerialPortNames()
		{
			return this._model.GetSerialPortNames();
		}

		public bool OpenComPort(string portName)
		{
			if (this._model.OpenComPort(portName)) {
				this._model.ArduinoRueckmeldungReceived += Model_ArduinoRueckmeldungReceived;
				return true;
			}
			return false;
		}


		public bool CloseComPort()
		{
			if (this._model.CloseComPort()) {
				this._model.ArduinoRueckmeldungReceived -= Model_ArduinoRueckmeldungReceived;
				return true;
			}
			return false;
		}

		private void Model_ArduinoRueckmeldungReceived()
		{
			if (this.master != null)
				this.master.SendeAnlageZustandsDatenAnAlle(_model.AnlagenZustandsDatenAuslesen());
			this.OnViewNeuZeichnen();
		}

		public void FahrstrassenSuchen()
		{
			this._model.FahrstrassenSuchen();
		}

		/// <summary>
		/// Löscht die gegenwärtige Auswahl (Selektion) im Bedienmodus, gegenwärtig können dies nur Fahrstraßen sein
		/// </summary>
		/// <returns>Gibt TRUE zurück, wenn eine Auswahl gelöscht wurde</returns>
		public bool BedienenAuswahlLöschen()
		{
			return this._model.BedienenAuswahlLöschen();
		}



		/// <summary>
		/// Gibt an ob Rückmeldung angezeigt werden soll
		/// </summary>
		public bool RückmeldungAnzeigen
		{
			set {
				this._model.RückmeldungAnzeigen = value;
			}
			get {
				return this._model.RückmeldungAnzeigen;
			}
		}

		/// <summary>
		/// Gibt an ob die Rückmeldung vom Arduino gesendet werden
		/// </summary>
		public bool RückmeldungAktiv
		{
			set {
				this._model.RückmeldungAktiv = value;
			}
			get {
				return this._model.RückmeldungAktiv;
			}
		}

		/// <summary>
		/// Gibt an nach wie vielen Millisekunden das Startgleis einer Fahrstraße eingeschaltet wird
		/// </summary>
		public int FahrstraßenStartVerzögerung
		{
			get {
				return this._model.FahrstraßenStartVerzögerung;
			}
			set {
				this._model.FahrstraßenStartVerzögerung = value;
			}
		}

		/// <summary>
		/// aktualisiert die Vorschau für ein neues Anlagenelement
		/// </summary>
		/// <param name="bearbeitungsModus">Angabe des Elementtyps</param>
		/// <param name="_letzterRasterpunkt">Position des neuen Elements</param>
		/// <returns></returns>
		public bool NeuesElementVorschau(BearbeitungsModus bearbeitungsModus, Point _letzterRasterpunkt)
		{
			return this._model.NeuesElementVorschau(bearbeitungsModus, _letzterRasterpunkt, this.Zoom);
		}

		/// <summary>
		/// Setzt das Vorschau Element zurück
		/// </summary>
		public void NeuesElementVorschauReset()
		{
			this._model.NeuesElementVorschauReset();
		}

		public string BearbeitenAnlagenElementInfoText(Point punkt)
		{
			return _model.BearbeitenAnlagenElementInfoText(punkt);
		}

		public bool BearbeitenMouseClick(BearbeitungsModus bearbeitungsModus, MouseButtons button, bool ctrlPressed, Point punkt)
		{
			bool result = false;
			switch (bearbeitungsModus) {
				case BearbeitungsModus.Schalter:
				case BearbeitungsModus.Signal:
				case BearbeitungsModus.Gleis:
				case BearbeitungsModus.Entkuppler:
				case BearbeitungsModus.Fss:
				case BearbeitungsModus.InfoElement:
					result = this._model.BearbeitenNeuZeichnen(bearbeitungsModus, button, this.BerechneRasterPunkt(punkt));
					break;
				case BearbeitungsModus.Selektieren:
					result = this._model.BearbeitenSelektieren(button, ctrlPressed, punkt);
					break;
			}
			return result;
		}

		public bool SignalDrehen()
		{
			return this._model.SignalDrehen();
		}

		/// <summary>
		/// Berechnet aus den übergebenen Pixelkoordinaten den Rasterpunkt´und gibt diesen als neuen Punkt zurück
		/// </summary>
		/// <param name="punkt">Pixelkoordinaten</param>
		/// <returns>Rasterpunkt</returns>
		public Point BerechneRasterPunkt(Point punkt)
		{
			int x = punkt.X;
			int y = punkt.Y;
			if (x < 0) x = 0;
			if (y < 0) y = 0;
			return new Point((x - this.Zoom / 2) / this.Zoom + 1
											, (y - this.Zoom / 2) / this.Zoom + 1);
		}

		public ElementListe<Zug> GetZugListe()
		{
			return this._model.GetZugListe();
		}
		#endregion
	}
}