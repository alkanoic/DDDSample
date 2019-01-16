using System;
using Xunit;
using FlaUI.Core;
using FlaUI.UIA3;
using System.Threading;
using System.IO;
using FlaUI.Core.AutomationElements;

namespace DDDSample.WinForm.Test
{
    public class MainFormTest
    {

        private readonly string targetApplicationPath = "DDDSample.WinForms.exe";

        private readonly string FolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        private void WindowCapture(Window window, string fileName)
        {
            Thread.Sleep(500);
            Capture.Element(window).ToFile(Path.Combine(FolderPath, fileName));
        }

        [Fact]
        public void MainForm_Test1()
        {

            var app = Application.Launch(targetApplicationPath);
            try
            {
                using (var automation = new UIA3Automation())
                {
                    var window = app.GetMainWindow(automation);
                    var automationElement = window.FindFirstDescendant(factory => factory.ByAutomationId("Btn_SubForm"));
                    var button = automationElement.AsButton();
                    Assert.NotNull(button);
                    WindowCapture(window, "mainWindow.png");

                    button.Click();
                    var subwindow = window.ModalWindows[0];
                    WindowCapture(subwindow, "subWindow.png");
                    var closeButton = window.FindFirstDescendant(factory => factory.ByAutomationId("Btn_Close"));
                    Assert.NotNull(closeButton);
                    closeButton.Click();

                    Thread.Sleep(1000); // すぐ閉じてしまうので、ここで1秒待つ
                }
            }
            finally
            {
                app.Close();
            }

        }
    }
}
