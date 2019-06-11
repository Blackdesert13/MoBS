using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using MoBaSteuerung.Anlagenkomponenten.Enum;
using MoBa.Elemente;

namespace MoBaSteuerung.Anlagenkomponenten.Delegates {
    /// <summary>
    /// Event für Zoomänderung
    /// </summary>
    /// <param name="e"></param>
    public delegate void Int32EventHandler(Int32 e);

    /// <summary>
    /// Event für AnzeigeTypänderung
    /// </summary>
    /// <param name="e"></param>
    public delegate void ViewTypEventHandler(AnzeigeTyp e);

    /// <summary>
    /// Event für Größenänderung
    /// </summary>
    /// <param name="e"></param>
    public delegate void SizeEventHandler(Size e);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="e"></param>
    public delegate void StringEventHandler(string e);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="e"></param>
    public delegate void BoolEventHandler(bool e);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="adresse"></param>
    public delegate void ArduinoSendEventHandler(MCSpeicher.Adresse adresse);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="e"></param>
    public delegate void AppTypEventHandler(AppTyp e);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="e"></param>
    /// <returns></returns>
    public delegate void DateTimeEventHandler(DateTime e);

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public delegate void ViewNeuZeichnenEventHandler();

    /// <summary>
    /// 
    /// </summary>
    public delegate void ArduinoRueckmeldungEventHandler();

    /// <summary>
    /// 
    /// </summary>
    public delegate void AnlagenZustandEventHandler();

	/// <summary>
	/// 
	/// </summary>
	public delegate void ZugListeChangedEventHandler();

	/// <summary>
	/// 
	/// </summary>
	/// <param name="AnlageDaten"></param>
	public delegate void AnlageDatenEventHandler(byte[] AnlageDaten);

    public delegate void ServoBewegungEventHandler(int Servo, ServoAction Richtung);
}