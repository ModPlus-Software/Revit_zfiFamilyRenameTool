namespace zfiFamilyRenameTool.Revit
{
    using System;
    using Autodesk.Revit.Attributes;
    using Autodesk.Revit.DB;
    using Autodesk.Revit.UI;
    using ModPlusAPI.Windows;
    using Services;
    using View;
    using ViewModel;

    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class RenamerCommand : IExternalCommand
    {
        /// <summary>
        /// Статический экземпляр главного окна плагина
        /// </summary>
        public static RenamerWindow RenamerWindow { get; private set; }

        /// <inheritdoc/>
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
#if !DEBUG
                ModPlusAPI.Statistic.SendCommandStarting(ModPlusConnector.Instance);
#endif

                if (RenamerWindow == null)
                {
                    var mainVm = new MainViewModel(new RevitService(commandData.Application.Application, new RevitEvent()));
                    RenamerWindow = new RenamerWindow { DataContext = mainVm };
                    RenamerWindow.Closed += (sender, args) => RenamerWindow = null;
                    RenamerWindow.Show();
                }
                else
                {
                    RenamerWindow.Activate();
                }
            }
            catch (Exception exception)
            {
                ExceptionBox.Show(exception);
                return Result.Failed;
            }

            return Result.Succeeded;
        }
    }
}