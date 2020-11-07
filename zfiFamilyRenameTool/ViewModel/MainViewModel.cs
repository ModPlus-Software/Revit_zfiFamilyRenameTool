namespace zfiFamilyRenameTool.ViewModel
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows.Input;
    using ModPlusAPI;
    using ModPlusAPI.Abstractions;
    using ModPlusAPI.Mvvm;
    using ModPlusAPI.Windows;
    using Revit;
    using Services;
    using View;

    public class MainViewModel : VmBase
    {
        private readonly RevitService _service;
        private BodyViewModel _body;

        public MainViewModel(RevitService service)
        {
            _service = service;
            Options = new OptionsViewModel();
        }

        public ICommand ApplyCommand => new RelayCommandWithoutParameter(ApplyAndShowLogs);

        public ICommand CloseCommand => new RelayCommand<IClosable>(Close);

        public ICommand CloseAndApplyCommand => new RelayCommand<IClosable>(CloseAndApply);

        public ICommand OpenFamiliesCommand => new RelayCommandWithoutParameter(OpenFamilies);

        public BodyViewModel Body
        {
            get => _body;
            set
            {
                _body = value;
                OnPropertyChanged();
            }
        }

        public OptionsViewModel Options { get; }

        private void OpenFamilies()
        {
            var docs = _service.LoadDocs();
            if (docs.Count == 0)
            {
                // Не выбрано ни одного файла семейства!
                MessageBox.Show(Language.GetItem(ModPlusConnector.Instance.Name, "msg4"), MessageBoxIcon.Alert);
                return;
            }

            Body = new BodyViewModel(_service, docs, Options);
            Body.PropertyChanged += BodyOnPropertyChanged;
            Body.SelectedTabIndex = 0;

            RenamerCommand.RenamerWindow.Activate();
        }

        private void CloseAndApply(IClosable closeable)
        {
            _service.Renamed += (s, e) =>
            {
                LogWindow.ShowLogs(e);
                _service.SaveAllDocs(_body.Docs);
                Close(closeable);
            };
            Apply();
        }

        private void Close(IClosable closeable)
        {
            closeable.Close();
            if (Body != null)
            {
                Body.PropertyChanged -= BodyOnPropertyChanged;
                Body.Dispose();
            }
        }

        private void ApplyAndShowLogs()
        {
            _service.Renamed += SaveAnShowLogs;
            Apply();
        }

        private void Apply()
        {
            var renameables = Body.GetRenameables();
            if (renameables.Count == 0)
            {
                // Не выбрано значений для изменения!
                MessageBox.Show(Language.GetItem(ModPlusConnector.Instance.Name, "msg5"), MessageBoxIcon.Alert);
                return;
            }

            _service.Rename(renameables);
        }

        private void SaveAnShowLogs(object sender, IEnumerable<LogMessage> e)
        {
            LogWindow.ShowLogs(e);
            _service.SaveAllDocs(_body.Docs);
            _service.Renamed -= SaveAnShowLogs;
            Body.ReloadRenameables();
        }

        private void BodyOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedTabIndex")
            {
                Options.IsEnable = Body.SelectedTabIndex != 3;
            }
        }
    }
}