using System;
using System.Runtime.InteropServices;
using System.Text;

namespace MoBaSteuerung.Anlagenkomponenten
{
  /// <summary>
  /// 
  /// </summary>
  public class Win32
  {
    #region Values & structs

    /// <summary>
    /// 
    /// </summary>
    public const int WH_CBT = 5;
    /// <summary>
    /// 
    /// </summary>
		public const int HCBT_ACTIVATE = 5;

    /// <summary>
    /// 
    /// </summary>
		[StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
      /// <summary>
      /// 
      /// </summary>
			public int left;
      /// <summary>
      /// 
      /// </summary>
			public int top;
      /// <summary>
      /// 
      /// </summary>
			public int right;
      /// <summary>
      /// 
      /// </summary>
			public int bottom;
    }

    #endregion Values & structs

    #region Stock P/Invokes

    /// <summary>
    /// Arg for SetWindowsHookEx() 
    /// </summary>
    /// <param name="nCode"></param>
    /// <param name="wParam"></param>
    /// <param name="lParam"></param>
    /// <returns></returns>
    public delegate int WindowsHookProc(int nCode, IntPtr wParam, IntPtr lParam);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="idHook"></param>
    /// <param name="lpfn"></param>
    /// <param name="hInstance"></param>
    /// <param name="threadId"></param>
    /// <returns></returns>
		[DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
    public static extern int SetWindowsHookEx(int idHook, WindowsHookProc lpfn, IntPtr hInstance, int threadId);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="idHook"></param>
    /// <returns></returns>
		[DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
    public static extern bool UnhookWindowsHookEx(int idHook);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="idHook"></param>
    /// <param name="nCode"></param>
    /// <param name="wParam"></param>
    /// <param name="lParam"></param>
    /// <returns></returns>
		[DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
    public static extern int CallNextHookEx(int idHook, int nCode, IntPtr wParam, IntPtr lParam);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="hWnd"></param>
    /// <returns></returns>
		[DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    public static extern int GetWindowTextLength(IntPtr hWnd);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="hWnd"></param>
    /// <param name="lpClassName"></param>
    /// <param name="nMaxCount"></param>
    /// <returns></returns>
		[DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    public static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="hWnd"></param>
    /// <param name="lpString"></param>
    /// <param name="nMaxCount"></param>
    /// <returns></returns>
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="hDlg"></param>
    /// <param name="nIDDlgItem"></param>
    /// <param name="lpString"></param>
    /// <param name="nMaxCount"></param>
    /// <returns></returns>
		[DllImport("user32.dll")]
    public static extern uint GetDlgItemText(IntPtr hDlg, int nIDDlgItem, [Out] StringBuilder lpString, int nMaxCount);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="hDlg"></param>
    /// <param name="nIDDlgItem"></param>
    /// <returns></returns>
		[DllImport("user32.dll")]
    public static extern IntPtr GetDlgItem(IntPtr hDlg, int nIDDlgItem);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="hWnd"></param>
    /// <returns></returns>
		[DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
    public static extern IntPtr GetParent(IntPtr hWnd);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="handle"></param>
    /// <param name="r"></param>
    /// <returns></returns>
		[DllImport("User32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern bool GetWindowRect(IntPtr handle, ref RECT r);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="hWnd"></param>
    /// <param name="hWndInsertAfter"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="cx"></param>
    /// <param name="cy"></param>
    /// <param name="uFlags"></param>
    /// <returns></returns>
		[DllImport("user32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

    #endregion Stock P/Invokes

    #region Simplified interfaces

    /// <summary>
    /// 
    /// </summary>
    /// <param name="hWnd"></param>
    /// <returns></returns>
    public static string GetClassName(IntPtr hWnd)
    {
      StringBuilder ClassName = new StringBuilder(100);
      //Get the window class name
      int nRet = GetClassName(hWnd, ClassName, ClassName.Capacity);
      return ClassName.ToString();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="hWnd"></param>
    /// <returns></returns>
		public static string GetWindowText(IntPtr hWnd)
    {
      // Allocate correct string length first
      int length = GetWindowTextLength(hWnd);
      StringBuilder sb = new StringBuilder(length + 1);
      GetWindowText(hWnd, sb, sb.Capacity);
      return sb.ToString();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="hDlg"></param>
    /// <param name="nIDDlgItem"></param>
    /// <returns></returns>
		public static string GetDlgItemText(IntPtr hDlg, int nIDDlgItem)
    {
      IntPtr hItem = GetDlgItem(hDlg, nIDDlgItem);
      if (hItem == IntPtr.Zero)
        return null;
      int length = GetWindowTextLength(hItem);
      StringBuilder sb = new StringBuilder(length + 1);
      GetWindowText(hItem, sb, sb.Capacity);
      return sb.ToString();
    }

    #endregion Simplified interfaces
  }
}