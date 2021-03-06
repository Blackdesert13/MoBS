﻿namespace MoBaSteuerung.Anlagenkomponenten.Enum
{
	/// <summary>
	/// Log Level
	/// </summary>
	public enum LogLevel
	{
		/// <summary>
		/// 
		/// </summary>
		UnhandledException,
		/// <summary>
		/// 
		/// </summary>
		ThreadException,
		/// <summary>
		/// 
		/// </summary>
		Error,
		/// <summary>
		/// 
		/// </summary>
		Trace,
		/// <summary>
		/// 
		/// </summary>
		Info
	}

	/// <summary>
	/// 
	/// </summary>
	public enum AppTyp
	{
		/// <summary>
		/// 
		/// </summary>
		Undefiniert,
		/// <summary>
		/// 
		/// </summary>
		Slave,
		/// <summary>
		/// 
		/// </summary>
		Master
	}

	/// <summary>
	/// Anzeigetyp der Anzeige.
	/// </summary>
	public enum AnzeigeTyp
	{
		/// <summary>
		/// Bedienen der Anlage.
		/// </summary>
		Bedienen,
		/// <summary>
		/// Bearbeiten der Anlage.
		/// </summary>
		Bearbeiten
	}

	public enum ServoAction
	{
		None,
		LinksClick,
		RechtsClick,
		LinksHold,
		RechtsHold,
		HoldStop
	}

	/// <summary>
	/// Bearbeitungs Modus 
	/// </summary>
	public enum BearbeitungsModus
	{
		Selektieren,
		Knoten,
		Weiche,
		Schalter,
		Entkuppler,
		Signal,
		Platiene,
		MC,
		Gleis,
		Fss,
		InfoElement,
		EingangsSchalter
	}


	/// <summary>
	/// 
	/// </summary>
	public enum GitterTyp
	{
		/// <summary>
		/// 
		/// </summary>
		Linie,
		/// <summary>
		/// 
		/// </summary>
		LinieVersetzt,
		/// <summary>
		/// 
		/// </summary>
		Punkt,
		/// <summary>
		/// 
		/// </summary>
		PunktVersetzt
	}

	/// <summary>
	/// 
	/// </summary>
	public enum FadenkreuzTyp
	{
		/// <summary>
		/// 
		/// </summary>
		Linie,
		/// <summary>
		/// 
		/// </summary>
		Punkte,
	}



	public enum Elementzustand
	{
		An,
		Aus,
		Neg,
		Gleich,
		Selektiert
	}

	public enum FahrstrassenSignalTyp
	{
		StartSignal,
		ZielSignal
	}

	/// <summary>
	/// 
	/// </summary>
	public enum ElementRichtung
	{
		/// <summary>
		/// Ost 0 in X+ Richtung
		/// </summary>
		O,
		/// <summary>
		/// SüdOst 1 in X+ Y+ Richtung
		/// </summary>
		SO,
		/// <summary>
		/// Süd 2 in Y+ Richtung
		/// </summary>
		S,
		/// <summary>
		/// SüdWest 3 in X- Y+ Richtung
		/// </summary>
		SW,
		/// <summary>
		/// West 4 in X- Richtung
		/// </summary>
		W,
		/// <summary>
		/// NordWest 5 in X- Y- Richtung
		/// </summary>
		NW,
		/// <summary>
		/// Nord 6 Y- Richtung
		/// </summary>
		N,
		/// <summary>
		/// NordOst 7 X+ Y- Richtung
		/// </summary>
		NO
	}

}