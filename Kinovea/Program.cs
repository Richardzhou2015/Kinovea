/*
Copyright � Joan Charmant 2008.
jcharmant@gmail.com 
 
This file is part of Kinovea.

Kinovea is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License version 2 
as published by the Free Software Foundation.

Kinovea is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with Kinovea. If not, see http://www.gnu.org/licenses/.

 */

using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Reflection;
using Kinovea.Services;
using System.Diagnostics;
using log4net;

namespace Kinovea.Root
{
    internal static class Program
    {
        private static bool IsFirstInstance
        {
            get
            {
                bool gotMutex;
                mutex = new Mutex(false, "Local\\" + appGuid, out gotMutex);
                return gotMutex;
            }
        }
        private static Mutex mutex;
        private static string appGuid = "b049b83e-90f3-4e84-9289-52ee6ea2a9ea";
        private static readonly ILog log = LogManager.GetLogger(typeof(Program));


        [STAThread]
        private static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += AppDomain_UnhandledException;
            
            Thread.CurrentThread.Name = "Main";
            
            Assembly assembly = Assembly.GetExecutingAssembly();
            Software.Initialize(assembly.GetName().Version);

            Software.LogInfo();
            Software.SanityCheckDirectories();
            PreferencesManager.Initialize();
            PreferencesManager.Refresh();

            // Check if this is the first instance of the application.
            // If we are not started on a specific name this will be used to
            // restore the last closed window if we are first or start a new one otherwise.
            bool isFirstInstance = IsFirstInstance;
            
            // Get launch settings from thecommand line, including instance name.
            // This will update the static LanchSettingsManager with the data.
            var args = Environment.GetCommandLineArgs();
            if (args.Length > 1)
                CommandLineArgumentManager.Instance.ParseArguments(args);

            // Read the list of saved instances and determine which one to load in this window.
            WindowManager.Startup(isFirstInstance);

            if (WindowManager.ActiveWindow == null)
            {
                // We found out that the window is already active and brought it to front.
                // Nothing more to do here.
                return;
            }

            // Make sure each instance logs to its own log file.
            // Up to this line the logging went to the default "log.txt" file.
            // After this line it goes to log.<idName>.txt.
            Software.ConfigureInstanceLogging();

            //----------------------------------------------------------------

            log.InfoFormat("-----------------------------------------------------------");
            log.InfoFormat("Window:{0} ({1}). {2:yyyy-MM-dd HH:mm:ss}", 
                WindowManager.ActiveWindow.Id, WindowManager.ActiveWindow.Name, DateTime.Now);

            // General application startup workflow.
            log.Debug("Application level initialisations.");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // ========== [前置登录模块] ==========
            log.Debug("Login phase: showing login window.");
            string machineId = MachineHelper.GetMachineUniqueId();

            using (LoginSplash loginWindow = new LoginSplash())
            {
                DialogResult loginCheck = loginWindow.ShowDialog();

                if (loginCheck != DialogResult.OK)
                {
                    log.Info("Login cancelled or failed. Exiting.");
                    ClientSession.Clear();
                    return;
                }

                ClientSession.MachineId = machineId;
                // AuthToken/UserName/UserId are already set inside LoginSplash.btnLogin_Click
            }

            log.InfoFormat("Login successful. MachineId:{0}", machineId);
            // ========== [/前置登录模块] ==========

            log.Debug("Showing SplashScreen.");
            FormSplashScreen splashForm = new FormSplashScreen(true);  // 轻量化模式：进度条 + 状态文字
            splashForm.Show();
            splashForm.Update();

            RootKernel kernel = new RootKernel(splashForm);  // 传入 splash 用于进度更新
            kernel.Prepare();
            
            log.Debug("Closing splash screen.");
            splashForm.Close();
            splashForm.Dispose();

            log.Debug("Launching.");
            kernel.Launch();

            // 程序退出时清空会话
            ClientSession.Clear();
        }

        /// <summary>
        /// Top level catch-all for unhandled exceptions.
        /// Dump the data to a separate file if possible.
        /// </summary>
        private static void AppDomain_UnhandledException(object sender, UnhandledExceptionEventArgs args)
        {
            Exception ex = (Exception)args.ExceptionObject;
            
            string message = string.Format("Message: {0}", ex.Message);
            string source = string.Format("Source: {0}", ex.Source);
            string target = string.Format("Target site: {0}", ex.TargetSite);
            string inner = string.Format("InnerException: {0}", ex.InnerException);
            string trace = string.Format("Stack: {0}", ex.StackTrace);
            
            string dumpFile = string.Format("Unhandled Crash - {0}.txt", Guid.NewGuid());
            using (StreamWriter sw = File.AppendText(Path.Combine(Software.SettingsDirectory, dumpFile)))
            {
                sw.WriteLine(message);
                sw.WriteLine(source);
                sw.WriteLine(target);
                sw.WriteLine(inner);
                sw.WriteLine(trace);
                sw.Close();
            }
            
            // Dump again in the log.
            log.Error("----------------- Unhandled Crash -------------------------");
            log.Error(message);
            log.Error(source);
            log.Error(target);
            log.Error(inner);
            log.Error(trace);
        }
    }
}