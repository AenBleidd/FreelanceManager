﻿using System;
using System.Windows.Forms;

namespace FreelanceManager
{
  static class Program
  {
    /// <summary>
    /// Главная точка входа для приложения.
    /// </summary>
    [STAThread]
    static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new frmMain());
    }

    public static string getProgramName()
    {
      return "FreelanceManager";
    }
  }
}
