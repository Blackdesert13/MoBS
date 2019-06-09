using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using MoBaSteuerung.Anlagenkomponenten;
using MoBaSteuerung.Anlagenkomponenten.Enum;
using System.Diagnostics;

namespace MoBaSteuerung
{
	/// <summary>
	/// View Bereich
	/// </summary>
	public partial class MoBaStForm
	{
		#region Private Felder

		private BearbeitungsModus bearbeitungsModus;

		private bool fadenkreuzAnzeigen;
		private FadenkreuzTyp fadenkreuzTyp;
		private Pen fadenkreuzStift;
		private Color fadenkreuzFarbe;

		private bool gitterAnzeigen;
		private GitterTyp gitterTyp;
		private Color gitterFarbe;

		private bool vScrollBarScroll;
		private bool hScrollBarScroll;

		private bool mouseEnter;
		// MouseEventArgs
		private MouseEventArgs mouseDownEventArgs;
		private MouseEventArgs mouseMoveEventArgs;
		private MouseEventArgs mouseUpEventArgs;
		private MouseEventArgs mouseClickEventArgs;
		private MouseEventArgs auswahlRechtecktMouseDownEventArgs;
		private KeyEventArgs keydownEventArgs;

		private bool auswahlRechteckVerschieben;
		private Pen auswahlRechteckStift;
		private GraphicsPath auswahlRechteck;

		private Point _letzterRasterpunkt = new Point(0, 0);
		private Point _dragDropDeltaGesamt = new Point(0, 0);
		#endregion

		private Model Model
		{
			get {
				if (_model == null)
					InizializeModel();
				return _model;
			}

			set {
				_model = value;
			}
		}

		private Controller Controller
		{
			get {
				if (_controller == null)
					InitializeController();
				return _controller;
			}

			set {
				_controller = value;
			}
		}



		#region View

		#region MoBaSt

		private void MoBaStForm_MouseWheel(object sender, MouseEventArgs e)
		{
			// Nur innerhalb des Panels
			if (!((this.vScrollBarView.Visible && e.X >= this.panelScrollView.Width) && (this.hScrollBarView.Visible && e.Y >= this.panelScrollView.Height))) {
				if ((Control.ModifierKeys & Keys.Control) == Keys.Control) {
					#region Raster Zoom

					if (e.Delta < 0) {
						#region Verkleinern

						if (this.Controller.Zoom > 1) {
							this.Controller.Zoom = this.Controller.Zoom - 1;
						}

						// Mausposition merken und PictureBox verschieben unter die Mausposition
						int iOffSetX = (Convert.ToInt32((((decimal)(-this.pictureBoxView.Location.X + e.X) * this.Controller.ZoomFaktor) / this.Controller.ZoomFaktorVorÄnderung) - (decimal)(-this.pictureBoxView.Location.X + e.X)));

						if (this.hScrollBarView.Visible && ((this.hScrollBarView.Value + iOffSetX) >= 0 && ((this.hScrollBarView.Value + iOffSetX) + hScrollBarView.LargeChange) <= hScrollBarView.Maximum)) {
							this.hScrollBarView.Value = this.hScrollBarView.Value + iOffSetX;
						}

						int iOffSetY = (Convert.ToInt32((((decimal)(-this.pictureBoxView.Location.Y + e.Y) * this.Controller.ZoomFaktor) / this.Controller.ZoomFaktorVorÄnderung) - (decimal)(-this.pictureBoxView.Location.Y + e.Y)));

						if (this.vScrollBarView.Visible && ((this.vScrollBarView.Value + iOffSetY) >= 0 && ((this.vScrollBarView.Value + iOffSetY) + vScrollBarView.LargeChange) <= vScrollBarView.Maximum)) {
							this.vScrollBarView.Value = this.vScrollBarView.Value + iOffSetY;
						}

						#endregion
					}
					else {
						#region Vergrößern

						this.Controller.Zoom = this.Controller.Zoom + 1;

						// Mausposition merken und PictureBox verschieben unter die Mausposition
						int iOffSetX = (Convert.ToInt32((((decimal)(-this.pictureBoxView.Location.X + e.X) * this.Controller.ZoomFaktor) / this.Controller.ZoomFaktorVorÄnderung) - (decimal)(-this.pictureBoxView.Location.X + e.X)));

						if (this.hScrollBarView.Visible && (this.hScrollBarView.Maximum >= this.hScrollBarView.Value + this.hScrollBarView.LargeChange + iOffSetX)) {
							this.hScrollBarView.Value = this.hScrollBarView.Value + iOffSetX;
						}

						int iOffSetY = (Convert.ToInt32((((decimal)(-this.pictureBoxView.Location.Y + e.Y) * this.Controller.ZoomFaktor) / this.Controller.ZoomFaktorVorÄnderung) - (decimal)(-this.pictureBoxView.Location.Y + e.Y)));

						if (this.vScrollBarView.Visible && (this.vScrollBarView.Maximum >= this.vScrollBarView.Value + this.vScrollBarView.LargeChange + iOffSetY)) {
							this.vScrollBarView.Value = this.vScrollBarView.Value + iOffSetY;
						}

						#endregion
					}

					#endregion
				}
				else if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift) {
					#region Scroll Hochizontal

					if (this.hScrollBarView.Visible) {
						if (e.Delta < 0) {
							// Links
							if (this.hScrollBarView.Maximum <= this.hScrollBarView.Value + this.hScrollBarView.LargeChange + this.hScrollBarView.SmallChange) {
								this.hScrollBarView.Value = this.hScrollBarView.Maximum - this.hScrollBarView.LargeChange + 1;
							}
							else {
								this.hScrollBarView.Value = this.hScrollBarView.Value + this.hScrollBarView.SmallChange;
							}
						}
						else {
							// Rechts
							if (0 > this.hScrollBarView.Value - this.hScrollBarView.SmallChange) {
								this.hScrollBarView.Value = 0;
							}
							else {
								this.hScrollBarView.Value = this.hScrollBarView.Value - this.hScrollBarView.SmallChange + 1;
							}
						}
					}

					#endregion
				}
				else {
					#region Scroll Vertikal

					if (this.vScrollBarView.Visible) {
						if (e.Delta < 0) {
							// Hoch
							if (this.vScrollBarView.Maximum <= this.vScrollBarView.Value + this.vScrollBarView.LargeChange + this.vScrollBarView.SmallChange) {
								this.vScrollBarView.Value = this.vScrollBarView.Maximum - this.vScrollBarView.LargeChange + 1;
							}
							else {
								this.vScrollBarView.Value = this.vScrollBarView.Value + this.vScrollBarView.SmallChange;
							}
						}
						else {
							// Runter
							if (0 > this.vScrollBarView.Value - this.vScrollBarView.SmallChange) {
								this.vScrollBarView.Value = 0;
							}
							else {
								this.vScrollBarView.Value = this.vScrollBarView.Value - this.vScrollBarView.SmallChange + 1;
							}
						}
					}

					#endregion
				}
				this.pictureBoxView.Invalidate();
			}
		}

		#endregion

		#region PanelView

		private void panelView_VisibleChanged(object sender, EventArgs e)
		{
			this.Aktualisieren();
		}

		private void panelView_Resize(object sender, EventArgs e)
		{
			this.Aktualisieren();
		}

		private void panelView_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			this.Controller.ZoomStandard();
		}

		#endregion

		#region ScrollBar

		private void vScrollBarView_ValueChanged(object sender, EventArgs e)
		{
			switch (this.Controller.AnzeigeTyp) {
				case AnzeigeTyp.Bedienen:
					if (this.vScrollBarScroll) {
						this.pictureBoxView.Location = new Point(this.pictureBoxView.Location.X, -this.vScrollBarView.Value + 1);
					}
					break;
				case AnzeigeTyp.Bearbeiten:

					break;
			}
		}

		private void hScrollBarView_ValueChanged(object sender, EventArgs e)
		{
			switch (this.Controller.AnzeigeTyp) {
				case AnzeigeTyp.Bedienen:
					if (this.hScrollBarScroll) {
						this.pictureBoxView.Location = new Point(-this.hScrollBarView.Value + 1, this.pictureBoxView.Location.Y);
					}
					break;
				case AnzeigeTyp.Bearbeiten:

					break;
			}
		}

		#endregion

		#region PictureBoxView

		private void pictureBoxView_DragLeave(object sender, System.EventArgs e)
		{

		}

		private void pictureBoxView_DragOver(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(typeof(MoBaStForm))) {
				MoBaStForm sourceForm = (MoBaStForm)e.Data.GetData(typeof(MoBaStForm));
				if (this == sourceForm) {
					Point deltaRaster = this.GetDragRasterDelta(this.pictureBoxView.PointToClient(new Point(e.X, e.Y)));
					if (deltaRaster.X != this._dragDropDeltaGesamt.X || deltaRaster.Y != this._dragDropDeltaGesamt.Y) {
						this.Controller.BearbeitenDragDrop(new Point(deltaRaster.X - this._dragDropDeltaGesamt.X, deltaRaster.Y - this._dragDropDeltaGesamt.Y), e.Effect);
						this._dragDropDeltaGesamt = deltaRaster;
						this.pictureBoxView.Invalidate();
					}
				}
			}
		}

		private Point GetDragRasterDelta(Point mousePosition)
		{
			int deltaX = mousePosition.X - this.mouseDownEventArgs.X;
			int deltaY = mousePosition.Y - this.mouseDownEventArgs.Y;
			int zoom = this.Controller.Zoom;
			Point deltaRaster = new Point(deltaX / zoom, deltaY / zoom);
			return deltaRaster;
		}

		private void pictureBoxView_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(typeof(MoBaStForm))) {
				MoBaStForm sourceForm = (MoBaStForm)e.Data.GetData(typeof(MoBaStForm));
				if (this == sourceForm) {
					if (e.Effect == DragDropEffects.None) {
						e.Effect = DragDropEffects.Move;
						this._dragDropDeltaGesamt = new Point(0, 0);
					}
				}
			}
		}

		private void pictureBoxView_DragDrop(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(typeof(MoBaStForm))) {
				MoBaStForm sourceForm = (MoBaStForm)e.Data.GetData(typeof(MoBaStForm));
				if (this == sourceForm) {
					this.Controller.BearbeitenDragDropAbschließen(e.Effect);

					this.pictureBoxView.Invalidate();
				}
			}
		}

		private void pictureBoxView_MouseEnter(object sender, EventArgs e)
		{
			this.mouseEnter = true;

			bool neuZeichnen = false;
			switch (this.Controller.AnzeigeTyp) {
				case AnzeigeTyp.Bearbeiten:
					neuZeichnen = this.BearbeitenMouseEnter();
					break;
				case AnzeigeTyp.Bedienen:
					neuZeichnen = this.BedienenMouseEnter();
					break;
			}
			if (neuZeichnen) this.pictureBoxView.Invalidate();
		}

		private void pictureBoxView_MouseDown(object sender, MouseEventArgs e)
		{
			this.mouseDownEventArgs = e;

			bool neuZeichnen = false;
			switch (this.Controller.AnzeigeTyp) {
				case AnzeigeTyp.Bearbeiten:
					neuZeichnen = this.BearbeitenMouseDown();
					break;
				case AnzeigeTyp.Bedienen:
					neuZeichnen = this.BedienenMouseDown();
					break;
			}
			if (neuZeichnen) this.pictureBoxView.Invalidate();
		}

		private void pictureBoxView_MouseMove(object sender, MouseEventArgs e)
		{
			this.mouseMoveEventArgs = e;

			bool neuZeichnen = false;
			switch (this.Controller.AnzeigeTyp) {
				case AnzeigeTyp.Bearbeiten:
					neuZeichnen = this.BearbeitenMouseMove();
					break;
				case AnzeigeTyp.Bedienen:
					neuZeichnen = this.BedienenMouseMove();
					break;
			}
			if (neuZeichnen) this.pictureBoxView.Invalidate();
		}

		private void pictureBoxView_MouseUp(object sender, MouseEventArgs e)
		{
			this.mouseUpEventArgs = e;

			bool neuZeichnen = false;
			switch (this.Controller.AnzeigeTyp) {
				case AnzeigeTyp.Bearbeiten:
					neuZeichnen = this.BearbeitenMouseUp();
					break;
				case AnzeigeTyp.Bedienen:
					neuZeichnen = this.BedienenMouseUp();
					break;
			}
			if (neuZeichnen) this.pictureBoxView.Invalidate();
		}

		private void pictureBoxView_MouseLeave(object sender, EventArgs e)
		{
			this.mouseEnter = false;

			bool neuZeichnen = false;
			switch (this.Controller.AnzeigeTyp) {
				case AnzeigeTyp.Bearbeiten:
					neuZeichnen = this.BearbeitenMouseLeave();
					break;
				case AnzeigeTyp.Bedienen:
					neuZeichnen = this.BedienenMouseLeave();
					break;
			}
			if (neuZeichnen) this.pictureBoxView.Invalidate();
		}

		private void pictureBoxView_DoubleClick(object sender, EventArgs e)
		{
			bool neuZeichnen = false;
			MouseEventArgs eArg = (MouseEventArgs)e;
			switch (this.Controller.AnzeigeTyp) {
				case AnzeigeTyp.Bearbeiten:
					this.Controller.BearbeitenenMouseDoubleClick(eArg.Location);
					break;
				case AnzeigeTyp.Bedienen:
					//neuZeichnen = this.BedienenMouseClick();
					int zugNr = this.Controller.BedienenMouseDoubleClick(eArg.Location);
					neuZeichnen = this.BedienenMouseClick();

					break;
			}
			if (neuZeichnen) this.pictureBoxView.Invalidate();
		}

		private void pictureBoxView_MouseClick(object sender, MouseEventArgs e)
		{
			this.mouseClickEventArgs = e;

			bool neuZeichnen = false;
			switch (this.Controller.AnzeigeTyp) {
				case AnzeigeTyp.Bearbeiten:
					neuZeichnen = this.BearbeitenMouseClick(e);

					break;
				case AnzeigeTyp.Bedienen:
					//neuZeichnen = this.BedienenMouseClick();
					neuZeichnen = this.Controller.BedienenMouseClick(e.Location, e.Button, Control.ModifierKeys);
					break;
			}
			if (neuZeichnen) this.pictureBoxView.Invalidate();
		}

		private void pictureBoxView_Paint(object sender, PaintEventArgs e)
		{
			#region  AnlagenElemente zeichnen
			e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
			e.Graphics.SmoothingMode = SmoothingMode.Default;
			#endregion

			switch (this.Controller.AnzeigeTyp) {
				case AnzeigeTyp.Bearbeiten:
					this.BearbeitenPaint(e);
					break;
				case AnzeigeTyp.Bedienen:
					this.BedienenPaint(e);
					break;
			}
		}

		#endregion

		#region Aktualisieren

		private void Aktualisieren()
		{
			// alles Aktualisieren
			// Anzeige anpassen
			switch (this.Controller.AnzeigeTyp) {
				case AnzeigeTyp.Bedienen:
					// Bedienen / Fahren
					this.BedienenAktualisieren();
					break;
				case AnzeigeTyp.Bearbeiten:
					// Bearbeiten
					this.BearbeitenAktualisieren();
					break;
			}
		}

		#endregion

		#endregion

		#region Bearbeiten

		private bool BearbeitenMouseEnter()
		{
			bool result = false;

			return result;
		}

		private bool BearbeitenMouseDown()
		{
			bool result = false;

			#region BearbeitungsModus

			switch (this.bearbeitungsModus) {
				case BearbeitungsModus.Selektieren:
					#region Selektieren

					#region Linke Maustaste

					if (this.mouseDownEventArgs.Button == MouseButtons.Left) {
						// außerhalb des Auswahlrechteckes
						if (!this.AuswahlRechteckMatrix(this.auswahlRechteck).IsVisible(this.mouseDownEventArgs.Location)) {
							// Auswahlrechteck
							this.auswahlRechteck.Reset();
							this.auswahlRechteckVerschieben = false;
							result = true;
						}
						this.auswahlRechtecktMouseDownEventArgs = this.mouseDownEventArgs;
					}

					#endregion

					#region Rechte Maustaste

					else if (this.mouseDownEventArgs.Button == MouseButtons.Right) {
						// Auswahlrechteck Menü
						this.AuswahlRechteckMenü();
					}
					#endregion

					#endregion
					break;
				case BearbeitungsModus.Knoten:
					break;
				case BearbeitungsModus.Weiche:
					break;
				case BearbeitungsModus.Schalter:
					break;
				case BearbeitungsModus.Entkuppler:
					break;
				case BearbeitungsModus.Signal:
					break;
				case BearbeitungsModus.Platiene:
					break;
				case BearbeitungsModus.MC:
					break;
			}

			#endregion

			return result;
		}

		private bool BearbeitenMouseMove()
		{
			bool result = false;
			bool neuerRasterpunkterreicht = false;

			#region BearbeitungsModus

			this.BearbeitenRasterInfo(this.mouseMoveEventArgs.Location.X, this.mouseMoveEventArgs.Location.Y);

			result = this.fadenkreuzAnzeigen;

			Point aktuellerRasterpunkt = this.Controller.BerechneRasterPunkt(mouseMoveEventArgs.Location);
			if (aktuellerRasterpunkt.X != this._letzterRasterpunkt.X || aktuellerRasterpunkt.Y != this._letzterRasterpunkt.Y) {
				neuerRasterpunkterreicht = true;
				_letzterRasterpunkt = aktuellerRasterpunkt;
			}

			#region AnlagenElement Info String
			string infoString = this.Controller.BearbeitenAnlagenElementInfoText(this.mouseMoveEventArgs.Location);
			if (infoString == String.Empty) {
				infoString = "Info";
			}
			this.toolStripStatusLabelInfo.Text = infoString;

			#endregion

			switch (this.bearbeitungsModus) {
				case BearbeitungsModus.Selektieren:

					#region  DragDrop Start

					if (this.mouseMoveEventArgs.Button == MouseButtons.Left) {
						//if (this.mouseDownEventArgs != null && (this.mouseDownEventArgs.Location != this.mouseMoveEventArgs.Location)) {
						//    // AuswahlRechteck zeichnen
						//    // im Verschiebe Modus
						//    if (this.auswahlRechteckVerschieben) {
						//        result = this.AuswahlRechteckVerschieben();
						//    }
						//    else {
						//        result = this.AuswahlRechteckBerechnen();
						//    }
						//}

						if (this.mouseDownEventArgs != null && this.Model.StartOnSelElement(this.mouseDownEventArgs.Location)) {
							Rectangle dragTestRect = new Rectangle(this.mouseDownEventArgs.Location, Size.Empty);
							dragTestRect.Inflate(SystemInformation.DragSize);
							Point pt = this.PointToClient(Control.MousePosition);
							if (!dragTestRect.Contains(pt)) {
								this.DoDragDrop(this, DragDropEffects.Move);
							}
						}
					}


					#endregion

					#region AuswahlRechteck Mauszeiger ändern

					this.AuswahlRechteckMaus();

					#endregion


					break;
				case BearbeitungsModus.Knoten:
					break;
				case BearbeitungsModus.Weiche:
					break;
				case BearbeitungsModus.Schalter:
				case BearbeitungsModus.Entkuppler:
				case BearbeitungsModus.Signal:
				case BearbeitungsModus.Gleis:
				case BearbeitungsModus.Fss:
				case BearbeitungsModus.InfoElement:
					if (neuerRasterpunkterreicht)
						result = this.Controller.NeuesElementVorschau(this.bearbeitungsModus, this._letzterRasterpunkt);
					break;
				case BearbeitungsModus.Platiene:
					break;
				case BearbeitungsModus.MC:
					break;
			}

			#endregion

			return result;
		}

		private bool BearbeitenMouseUp()
		{
			bool result = false;

			#region BearbeitungsModus

			switch (this.bearbeitungsModus) {
				case BearbeitungsModus.Selektieren:

					#region AuswahlRechteck
					// zuweisen, ob das Auswahlrechteck noch vorhanden ist.
					if (this.auswahlRechteck.PointCount > 0) {
						// Abfragen ob Elemente ausgewählt wurden
						if (this.Controller.AuswahlRechteckElemente(this.auswahlRechteck)) {
							this.auswahlRechteckVerschieben = true;
						}
						else {
							this.auswahlRechteckVerschieben = false;
							this.auswahlRechteck.Reset();
							result = true;
						}
					}
					else {
						this.auswahlRechteckVerschieben = false;
					}
					#endregion

					break;
				case BearbeitungsModus.Knoten:
					break;
				case BearbeitungsModus.Weiche:
					break;
				case BearbeitungsModus.Schalter:
					break;
				case BearbeitungsModus.Entkuppler:
					break;
				case BearbeitungsModus.Signal:
					break;
				case BearbeitungsModus.Platiene:
					break;
				case BearbeitungsModus.MC:
					break;
			}

			#endregion

			return result;
		}

		private bool BearbeitenMouseLeave()
		{
			bool result = false;

			result = this.fadenkreuzAnzeigen;

			this.toolStripStatusLabelRasterInfo.Text = "0,0";

			return result;
		}

		private bool BearbeitenMouseClick(MouseEventArgs e)
		{
			bool result = this.Controller.BearbeitenMouseClick(this.bearbeitungsModus, e.Button, (ModifierKeys == Keys.Control || ModifierKeys == Keys.ControlKey), mouseClickEventArgs.Location);
			if (this.bearbeitungsModus == BearbeitungsModus.Selektieren && this._toolBoxElemente.Visible) {
				this._toolBoxElemente.AktualisierenSelektierteElemente(this.Model.AuswahlElemente);
			}
			if (result) {
				this.bearbeitungsModus = BearbeitungsModus.Selektieren;
				this.toolStripButtonElementEntkuppler.Checked = false;
				this.toolStripButtonElementSchalter.Checked = false;
				this.toolStripButtonElementSignal.Checked = false;
				this.toolStripButtonElementGleis.Checked = false;
			}
			return result;
		}

		private void BearbeitenPaint(PaintEventArgs e)
		{
			#region Auswahlrechteck
			this.Controller.ZeichneElementeBearbeiten(e.Graphics);
			if (this.auswahlRechteck.PointCount > 0) {
				e.Graphics.DrawPath(this.auswahlRechteckStift, this.AuswahlRechteckMatrix(this.auswahlRechteck));
			}
			#endregion

			#region Fadenkreuz zeichnen

			if (this.fadenkreuzAnzeigen && this.mouseEnter) {
				this.FadenkreuzZeichnen(e.Graphics);
			}

			#endregion
		}

		#region Auswahlrechteck

		private bool AuswahlRechteckBerechnen()
		{
			Int32 richtungX = this.mouseMoveEventArgs.X - this.mouseDownEventArgs.X;
			Int32 richtungY = this.mouseMoveEventArgs.Y - this.mouseDownEventArgs.Y;
			Int32 downX = 0;
			Int32 downY = 0;
			Int32 width = 0;
			Int32 height = 0;

			this.auswahlRechteck.Reset();

			#region Richtung X+ und Y+

			if (richtungX > 0 && richtungY > 0) {
				// X Anfang Punkt
				downX = this.mouseDownEventArgs.X / this.Controller.Zoom;
				width = (this.mouseMoveEventArgs.X - this.mouseDownEventArgs.X + (this.Controller.Zoom / 4)) / this.Controller.Zoom;

				// Y Anfang Punkt     
				downY = this.mouseDownEventArgs.Y / this.Controller.Zoom;
				height = (this.mouseMoveEventArgs.Y - this.mouseDownEventArgs.Y + (this.Controller.Zoom / 4)) / this.Controller.Zoom;

				this.auswahlRechteck.AddRectangle(new RectangleF(downX, downY, width, height));
			}

			#endregion

			#region Richtung X+ und Y-

			else if (richtungX > 0 && richtungY < 0) {
				// X Anfang Punkt
				downX = this.mouseDownEventArgs.X / this.Controller.Zoom;
				width = (this.mouseMoveEventArgs.X - this.mouseDownEventArgs.X + (this.Controller.Zoom / 4)) / this.Controller.Zoom;

				// Y Anfang Punkt
				if (this.mouseMoveEventArgs.Y < 0) {
					downY = 0;
				}
				else {
					downY = (this.mouseMoveEventArgs.Y + this.Controller.Zoom) / this.Controller.Zoom;
				}
				height = downY - (this.mouseDownEventArgs.Y / this.Controller.Zoom);

				this.auswahlRechteck.AddRectangle(new RectangleF(downX, downY, width, -height));
			}

			#endregion

			#region Richtung X- und Y-

			else if (richtungX < 0 && richtungY < 0) {
				// X Anfang Punkt
				if (this.mouseMoveEventArgs.X < 0) {
					downX = 0;
				}
				else {
					downX = (this.mouseMoveEventArgs.X + this.Controller.Zoom) / this.Controller.Zoom;
				}
				width = downX - (this.mouseDownEventArgs.X / this.Controller.Zoom);


				// Y Anfang Punkt
				if (this.mouseMoveEventArgs.Y < 0) {
					downY = 0;
				}
				else {
					downY = (this.mouseMoveEventArgs.Y + this.Controller.Zoom) / this.Controller.Zoom;
				}
				height = downY - (this.mouseDownEventArgs.Y / this.Controller.Zoom);

				this.auswahlRechteck.AddRectangle(new RectangleF(downX, downY, -width, -height));
			}

			#endregion

			#region Richtung X- und Y+

			else if (richtungX < 0 && richtungY > 0) {
				// X Anfang Punkt
				if (this.mouseMoveEventArgs.X < 0) {
					downX = 0;
				}
				else {
					downX = (this.mouseMoveEventArgs.X + this.Controller.Zoom) / this.Controller.Zoom;
				}
				width = downX - (this.mouseDownEventArgs.X / this.Controller.Zoom);

				// Y Anfang Punkt     
				downY = this.mouseDownEventArgs.Y / this.Controller.Zoom;
				height = (this.mouseMoveEventArgs.Y - this.mouseDownEventArgs.Y + (this.Controller.Zoom / 4)) / this.Controller.Zoom;

				this.auswahlRechteck.AddRectangle(new RectangleF(downX, downY, -width, height));
			}

			#endregion

			return true;
		}

		private bool AuswahlRechteckVerschieben()
		{
			bool result = false;

			// Auswahlrechteck verschieben
			Int32 iVerschubX = (this.mouseMoveEventArgs.X - this.auswahlRechtecktMouseDownEventArgs.X) / this.Controller.Zoom;
			Int32 iVerschubY = (this.mouseMoveEventArgs.Y - this.auswahlRechtecktMouseDownEventArgs.Y) / this.Controller.Zoom;

			// X min und max
			if (this.auswahlRechteck.PathPoints[0].X + iVerschubX < 0) {
				iVerschubX = 0;
			}
			else {
				result = true;
			}

			if (this.auswahlRechteck.PathPoints[1].X + iVerschubX >= (this.Controller.AnzeigeGrößeInPixel.Width / this.Controller.Zoom)) {
				iVerschubX = 0;
			}
			else {
				result = true;
			}

			// Y min und max
			if (this.auswahlRechteck.PathPoints[0].Y + iVerschubY < 0) {
				iVerschubY = 0;
			}
			else {
				result = true;
			}

			if (this.auswahlRechteck.PathPoints[2].Y + iVerschubY >= (this.Controller.AnzeigeGrößeInPixel.Height / this.Controller.Zoom)) {
				iVerschubY = 0;
			}
			else {
				result = true;
			}

			if (result) {
				Matrix mat = new Matrix();
				mat.Translate(iVerschubX, iVerschubY);
				this.auswahlRechteck.Transform(mat);
				// zuweisen der aktuellen Mausdaten
				this.auswahlRechtecktMouseDownEventArgs = new MouseEventArgs(this.auswahlRechtecktMouseDownEventArgs.Button, this.auswahlRechtecktMouseDownEventArgs.Clicks, this.auswahlRechtecktMouseDownEventArgs.X + (iVerschubX * this.Controller.Zoom), this.auswahlRechtecktMouseDownEventArgs.Y + (iVerschubY * this.Controller.Zoom), this.auswahlRechtecktMouseDownEventArgs.Delta);
				// Verschieben der ausgwählten Anlagenelemente
				this.Controller.AuswahlRechteckVerschieben(this.auswahlRechteck);
			}

			return result;
		}

		private void AuswahlRechteckMaus()
		{
			// Wenn im Auswahlrechteck, dann Mauszeiger wechseln.
			if (this.AuswahlRechteckMatrix(this.auswahlRechteck).IsVisible(this.mouseMoveEventArgs.Location)) {
				this.pictureBoxView.Cursor = Cursors.SizeAll;
			}
			else {
				this.pictureBoxView.Cursor = Cursors.Default;
			}
		}

		private void AuswahlRechteckMenü()
		{
			if (this.AuswahlRechteckMatrix(this.auswahlRechteck).IsVisible(this.mouseDownEventArgs.Location)) {
				this.contextMenuStripAuswahlRechteck.Show(this.pictureBoxView.PointToScreen(this.mouseDownEventArgs.Location));
				// ToDo Elemente suchen zum verschieben


			}
		}

		private void AuswahlRechteckLöschen()
		{
			if (MsgBox.Show("löschen", Constanten.ProgrammName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes) {
				// Auswahl löschen
				if (this.auswahlRechteck != null && this.auswahlRechteck.PointCount > 0) {
					this.Controller.AuswahlRechteckElementeLöschen(this.auswahlRechteck);
					this.auswahlRechteck.Reset();
					this.pictureBoxView.Invalidate();
				}
			}
		}

		private GraphicsPath AuswahlRechteckMatrix(GraphicsPath GraphicsPath)
		{
			GraphicsPath oRet = (GraphicsPath)GraphicsPath.Clone();
			Matrix mat = new Matrix();
			//mat.Translate(this.controller.Zoom / 2, this.controller.Zoom / 2);
			mat.Scale(this.Controller.Zoom, this.Controller.Zoom);
			oRet.Transform(mat);
			return oRet;
		}

		#endregion

		#region Fadenkreuz

		private void FadenkreuzZeichnen(Graphics graphics)
		{
			// Stift Farbe zuweisen
			this.fadenkreuzStift.Color = this.fadenkreuzFarbe;
			switch (this.fadenkreuzTyp) {
				case FadenkreuzTyp.Linie:
					this.fadenkreuzStift.DashStyle = DashStyle.Solid;
					break;
				case FadenkreuzTyp.Punkte:
					this.fadenkreuzStift.DashPattern = new float[] { 5, 10 };
					break;
			}
			graphics.DrawLine(this.fadenkreuzStift, this.mouseMoveEventArgs.X, this.mouseMoveEventArgs.Y - 16, this.mouseMoveEventArgs.X, 0);
			graphics.DrawLine(this.fadenkreuzStift, this.mouseMoveEventArgs.X, this.mouseMoveEventArgs.Y + 16, this.mouseMoveEventArgs.X, this.pictureBoxView.Height);

			graphics.DrawLine(this.fadenkreuzStift, this.mouseMoveEventArgs.X - 16, this.mouseMoveEventArgs.Y, 0, this.mouseMoveEventArgs.Y);
			graphics.DrawLine(this.fadenkreuzStift, this.mouseMoveEventArgs.X + 16, this.mouseMoveEventArgs.Y, this.pictureBoxView.Width, this.mouseMoveEventArgs.Y);
		}

		#endregion

		#region Kontext Menüs

		private void toolStripMenuItemAufheben_Click(object sender, EventArgs e)
		{
			this.auswahlRechteck.Reset();
			this.pictureBoxView.Invalidate();
		}

		private void toolStripMenuItemDrehen_Click(object sender, EventArgs e)
		{

		}

		private void toolStripMenuItemSpiegeln_Click(object sender, EventArgs e)
		{

		}

		private void toolStripMenuItemAuswahlRechteckLöschen_Click(object sender, EventArgs e)
		{
			this.AuswahlRechteckLöschen();
		}

		#endregion

		#region Aktualisieren

		private void BearbeitenAktualisieren()
		{
			this.GitterAktualisieren();



			this.pictureBoxView.Invalidate();
		}

		private void BearbeitenRasterInfo(int x, int y)
		{
			if (x < 0) x = 0;
			if (y < 0) y = 0;

			this.toolStripStatusLabelRasterInfo.Text = ((x - this.Controller.Zoom / 2) / this.Controller.Zoom + 1).ToString() + ","
																							 + ((y - this.Controller.Zoom / 2) / this.Controller.Zoom + 1).ToString();
			this.statusStrip.Refresh();
		}

		#endregion

		#region Gitter

		/// <summary>
		/// Gitter erzeugen.
		/// </summary>
		private void GitterAktualisieren()
		{
			if (this.gitterAnzeigen && this.Controller.Zoom > 0) {
				Image rasterImage = new Bitmap(this.Controller.Zoom, this.Controller.Zoom);
				Graphics graphics = Graphics.FromImage(rasterImage);

				switch (this.gitterTyp) {
					case GitterTyp.Linie:
						graphics.DrawLine(new Pen(this.gitterFarbe), 0, 0, this.Controller.Zoom - 1, 0);
						graphics.DrawLine(new Pen(this.gitterFarbe), 0, 0, 0, this.Controller.Zoom - 1);
						break;
					case GitterTyp.LinieVersetzt:
						graphics.DrawLine(new Pen(this.gitterFarbe), (this.Controller.Zoom) / 2, 0, (this.Controller.Zoom) / 2, this.Controller.Zoom - 1);
						graphics.DrawLine(new Pen(this.gitterFarbe), 0, (this.Controller.Zoom) / 2, this.Controller.Zoom - 1, (this.Controller.Zoom) / 2);
						break;
					case GitterTyp.Punkt:
						graphics.FillRectangle(new SolidBrush(this.gitterFarbe), 0, 0, 1, 1);
						break;
					case GitterTyp.PunktVersetzt:
						graphics.FillRectangle(new SolidBrush(this.gitterFarbe), (this.Controller.Zoom) / 2, (this.Controller.Zoom) / 2, 1, 1);
						break;
				}

				this.pictureBoxView.BackgroundImage = rasterImage;
				graphics.Dispose();
			}
			else {
				this.pictureBoxView.BackgroundImage = null;
			}
		}

		#endregion

		#endregion

		#region Bedienen

		private bool BedienenMouseEnter()
		{
			bool result = false;



			return result;
		}

		private bool BedienenMouseDown()
		{
			bool result = false;
			//Model.BedienenMouseDown(this.mouseDownEventArgs.)


			return result;
		}

		private bool BedienenMouseMove()
		{
			bool result = false;



			return result;
		}

		private bool BedienenMouseUp()
		{
			bool result = false;



			return result;
		}

		private bool BedienenMouseLeave()
		{
			bool result = false;



			return result;
		}

		private bool BedienenMouseClick()
		{
			bool result = true;

			//this.controller.ViewBedienenLinksMouseClick(this.mouseClickEventArgs.Location);

			return result;
		}

		private void BedienenPaint(PaintEventArgs e)
		{
			this.Controller.ZeichneElementeBedienen(e.Graphics);
			#region Test

			////e.Graphics.FillRectangle(Brushes.AliceBlue, 0, 0, anzeigeGrößeInPixel.Width, anzeigeGrößeInPixel.Height);
			//e.Graphics.DrawRectangle(Pens.Black, 5, 5, this.controller.AnzeigeGrößeInPixel.Width - 11, this.controller.AnzeigeGrößeInPixel.Height - 11);
			//e.Graphics.DrawRectangle(Pens.Black, 10, 10, this.controller.AnzeigeGrößeInPixel.Width - 21, this.controller.AnzeigeGrößeInPixel.Height - 21);
			//e.Graphics.DrawRectangle(Pens.Black, 15, 15, this.controller.AnzeigeGrößeInPixel.Width - 31, this.controller.AnzeigeGrößeInPixel.Height - 31);

			//if (this.controller.ZoomFaktor > 0)
			//  e.Graphics.ScaleTransform((float)this.controller.ZoomFaktor, (float)this.controller.ZoomFaktor);

			//e.Graphics.DrawLine(Pens.Black, Constanten.STANDARDRASTER, 0, Constanten.STANDARDRASTER, 100);
			//e.Graphics.DrawLine(Pens.Black, Constanten.STANDARDRASTER * 2, 0, Constanten.STANDARDRASTER * 2, 100);
			//e.Graphics.DrawLine(Pens.Black, Constanten.STANDARDRASTER * 3, 0, Constanten.STANDARDRASTER * 3, 100);
			//e.Graphics.DrawLine(Pens.Black, Constanten.STANDARDRASTER * 4, 0, Constanten.STANDARDRASTER * 4, 100);

			//e.Graphics.DrawLine(Pens.Black, 0, Constanten.STANDARDRASTER, 100, Constanten.STANDARDRASTER);
			//e.Graphics.DrawLine(Pens.Black, 0, Constanten.STANDARDRASTER * 2, 100, Constanten.STANDARDRASTER * 2);
			//e.Graphics.DrawLine(Pens.Black, 0, Constanten.STANDARDRASTER * 3, 100, Constanten.STANDARDRASTER * 3);
			//e.Graphics.DrawLine(Pens.Black, 0, Constanten.STANDARDRASTER * 4, 100, Constanten.STANDARDRASTER * 4);

			//e.Graphics.ResetTransform();
			#endregion

		}

		/// <summary>
		/// Kümmert nich um das AutoScroll der Anzeige.
		/// Wenn AnlagengrößeAktuell größer als Anzeigebereich, dann Scrollbar anzeigen.
		/// </summary>
		private void BedienenAktualisieren()
		{
			if (this.pictureBoxView.BackgroundImage != null) {
				this.pictureBoxView.BackgroundImage = null;
			}

			if ((this.Controller.AnzeigeGrößeInPixel.Width > this.panelView.ClientSize.Width && this.Controller.AnzeigeGrößeInPixel.Height > this.panelView.ClientSize.Height)
			 || (this.Controller.AnzeigeGrößeInPixel.Width > this.panelView.ClientSize.Width && this.Controller.AnzeigeGrößeInPixel.Height > (this.panelView.ClientSize.Height - this.hScrollBarView.Height))
			 || (this.Controller.AnzeigeGrößeInPixel.Width > (this.panelView.ClientSize.Width - this.vScrollBarView.Width) && this.Controller.AnzeigeGrößeInPixel.Height > this.panelView.ClientSize.Height)) // Breite (Hochizontal) und Höhe (Vertikal)
			{
				#region AnlagengrößeAktuell-Breite größer als Anzeige-Breite und AnlagengrößeAktuell-Höhe größer als Anzeige-Höhe
				// oder AnlagengrößeAktuell-Breite größer als Anzeige-Breite und AnlagengrößeAktuell-Höhe größer als Anzeige-Höhe minus vScrollBar-Breite
				// oder AnlagengrößeAktuell-Breite größer als Anzeige-Breite minus hScroll-Breite und AnlagengrößeAktuell-Höhe größer als Anzeige-Höhe

				// panelScroll
				// Position: 
				// X=0, Y=0 und Größe: Breite=Anzeige-Breite minus vScollBar-Breite, Höhe=Anzeige-Höhe minus vScollBar-Höhe
				this.panelScrollView.Bounds = new Rectangle(0, 0, this.panelView.ClientSize.Width - this.vScrollBarView.Width, this.panelView.ClientSize.Height - this.hScrollBarView.Height);

				// PictureBox
				// Größe: Größe=AnlagengrößeAktuell 
				this.pictureBoxView.Size = this.Controller.AnzeigeGrößeInPixel;

				// Offset der Breite berechnen
				int offSetBreite = this.pictureBoxView.Width + this.pictureBoxView.Location.X;
				if (offSetBreite <= this.panelScrollView.Width) {
					// Offset kleiner als panelScroll-Breite

					// Position:
					// X=PictureBox-X plus PictureBox-Breite minus Offset ,Y=PictureBox-Y
					this.pictureBoxView.Location = new Point(this.pictureBoxView.Location.X + (this.panelScrollView.Width - offSetBreite), this.pictureBoxView.Location.Y);
				}

				// Offset der Höhe berechnen
				int offSetHöhe = this.pictureBoxView.Height + this.pictureBoxView.Location.Y;
				if (offSetHöhe <= this.panelScrollView.Height) {
					// Offset kleiner als panelScroll-Höhe

					// Position:
					// X=PictureBox-X ,Y=PictureBox-Y plus PictureBox-Breite minus Offset
					this.pictureBoxView.Location = new Point(this.pictureBoxView.Location.X, this.pictureBoxView.Location.Y + (this.panelScrollView.Height - offSetHöhe));
				}

				// hScrollBar
				// Position:
				// X=0, Y=panelScroll-Höhe und Größe: Breite=Panel-Breite, Höhe=hScrollBar-Höhe
				this.hScrollBarView.Bounds = new Rectangle(0, this.panelScrollView.Height, this.panelScrollView.Width, this.hScrollBarView.Height);
				// einblenden
				this.hScrollBarView.Visible = true;

				// vScrollBar
				// Position:
				// X=panelScroll-Breite, Y=0 und Größe: Breite=vScrollBar-Breite, Höhe=panelScroll-Höhe
				this.vScrollBarView.Bounds = new Rectangle(this.panelScrollView.Width, 0, this.vScrollBarView.Width, this.panelScrollView.Height);
				// einblenden
				this.vScrollBarView.Visible = true;

				#endregion
			}
			else if (this.Controller.AnzeigeGrößeInPixel.Width > this.panelView.ClientSize.Width) // Breite
			{
				#region AnlagengrößeAktuell-Breite größer als Anzeige-Breite

				// panelScroll
				// Position: 
				// X=0, Y=0 und Größe: Breite=Anzeige-Breite, Höhe=Anzeige-Höhe minus hScollBar-Höhe
				this.panelScrollView.Bounds = new Rectangle(0, 0, this.panelView.ClientSize.Width, this.panelView.ClientSize.Height - this.hScrollBarView.Height);

				// PictureBox
				// Größe:
				// Breite=AnlagenRasterGröße-Breite, Höhe=panelScroll-Höhe
				this.pictureBoxView.Size = new Size(this.Controller.AnzeigeGrößeInPixel.Width, this.panelScrollView.Height);
				// Position:
				// Offset der Breite berechnen
				int offSetBreite = this.pictureBoxView.Width + this.pictureBoxView.Location.X;
				if (offSetBreite < this.panelScrollView.Width) {
					// Offset kleiner als panelScroll-Breite

					// Position:
					// X=PictureBox-X plus PictureBox-Breite minus Offset ,Y=PictureBox-Y
					this.pictureBoxView.Location = new Point(this.pictureBoxView.Location.X + (this.panelScrollView.Width - offSetBreite), 0);
				}

				// vScrollBar
				// ausblenden
				this.vScrollBarView.Visible = false;

				// hScrollBar
				// Position:
				// X=0, Y=panelScroll-Höhe und Größe: Breite=panelScroll-Breite, Höhe=hScrollBar-Höhe
				this.hScrollBarView.Bounds = new Rectangle(0, this.panelScrollView.Height, this.panelScrollView.Width, this.hScrollBarView.Height);
				// einblenden
				this.hScrollBarView.Visible = true;

				#endregion
			}
			else if (this.Controller.AnzeigeGrößeInPixel.Height > this.panelView.ClientSize.Height) // Höhe
			{
				#region AnlagengrößeAktuell-Höhe größer als UserControl-Höhe

				// panelScroll
				// Position:
				// X=0, Y=0 und Größe: Breite=Anzeige-Breite minus vScollBar-Breite, Höhe=Anzeige-Höhe
				this.panelScrollView.Bounds = new Rectangle(0, 0, this.panelView.ClientSize.Width - this.vScrollBarView.Width, this.panelView.ClientSize.Height);

				// PictureBox
				// Größe:
				// Breite=panelScroll-Breite, Höhe=AnlagenRasterGröße-Höhe
				this.pictureBoxView.Size = new Size(this.panelScrollView.Width, this.Controller.AnzeigeGrößeInPixel.Height);
				// Position:
				int offSetHöhe = this.pictureBoxView.Height + this.pictureBoxView.Location.Y;
				if (offSetHöhe < this.panelScrollView.Height) {
					// Offset kleiner als panelScroll-Höhe

					// Position:
					// X=PictureBox-X ,Y=PictureBox-Y plus PictureBox-Breite minus Offset
					this.pictureBoxView.Location = new Point(0, this.pictureBoxView.Location.Y + (this.panelScrollView.Height - offSetHöhe));
				}

				// vScrollBar
				// Position:
				// X=panelScroll-Breite, Y=0 und Größe: Breite=vScrollBar-Breite, Höhe=panelScroll-Höhe
				this.vScrollBarView.Bounds = new Rectangle(this.panelScrollView.Width, 0, this.vScrollBarView.Width, this.panelScrollView.Height);
				// einblenden
				this.vScrollBarView.Visible = true;

				// hScrollBar
				// ausblenden
				this.hScrollBarView.Visible = false;

				#endregion
			}
			else {
				#region AnlagengrößeAktuell-Breite kleiner als panelScroll-Breite und AnlagengrößeAktuell-Höhe kleiner als panelScroll-Höhe

				// panelScroll
				// Position:
				// X=0, Y=0 und Größe: Breite=Anzeige-Breite, Höhe=Anzeige-Höhe
				this.panelScrollView.Bounds = new Rectangle(0, 0, this.panelView.ClientSize.Width, this.panelView.ClientSize.Height);

				// PictureBox
				// Position:
				// X=0, Y=0 und Größe: Breite=panelScroll-Breite, Höhe=panelScroll-Höhe
				this.pictureBoxView.Bounds = new Rectangle(0, 0, this.panelScrollView.Width, this.panelScrollView.Height);

				// hScrollBar
				// ausblenden
				this.vScrollBarView.Visible = false;

				// hScrollBar
				// ausblenden
				this.hScrollBarView.Visible = false;

				#endregion
			}

			#region ScrollBar

			// vScrollBar
			if (this.vScrollBarView.Visible) {
				// Eingeblendet

				this.vScrollBarScroll = false;

				// Maximum: PictureBox-Höhe
				this.vScrollBarView.Maximum = this.pictureBoxView.Height;
				// LargeChange: panelScroll-Höhe
				this.vScrollBarView.LargeChange = this.vScrollBarView.Height;
				// SmallChange: RasterAktuel
				this.vScrollBarView.SmallChange = this.Controller.Zoom;

				this.vScrollBarScroll = true;
			}
			else {
				// Ausgeblendet

				this.vScrollBarView.Value = 0;
				// LargeChange: 0
				this.vScrollBarView.LargeChange = 0;
				// SmallChange: 0
				this.vScrollBarView.SmallChange = 0;
				// Maximum: 0
				this.vScrollBarView.Maximum = 0;
			}

			// hScrollBar
			if (this.hScrollBarView.Visible) {
				// Eingeblendet

				this.hScrollBarScroll = false;

				// Maximum: PictureBox-Breite
				this.hScrollBarView.Maximum = this.pictureBoxView.Width;
				// LargeChange: panelScroll-Breite
				this.hScrollBarView.LargeChange = this.hScrollBarView.Width;
				// SmallChange: RasterAktuel
				this.hScrollBarView.SmallChange = this.Controller.Zoom;

				this.hScrollBarScroll = true;
			}
			else {
				// Ausgeblendet

				this.hScrollBarView.Value = 0;
				// LargeChange: 0
				this.hScrollBarView.LargeChange = 0;
				// SmallChange: 0
				this.hScrollBarView.SmallChange = 0;
				// Maximum: 0
				this.hScrollBarView.Maximum = 0;
			}

			#endregion

			this.pictureBoxView.Invalidate();
		}



		#endregion
	}
}