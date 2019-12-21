namespace zfiFamilyRenameTool.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Abstractions;
    using ModPlusAPI.Mvvm;
    using Services;

    public class RenameableViewModel : VmBase, IRenameable
    {
        private readonly List<IRenameable> _renameable;
        private bool _isChecked;

        public RenameableViewModel(List<IRenameable> renameable)
        {
            _renameable = renameable;
        }

        public event EventHandler<bool> Checked;

        public string Title => _renameable.First().Title;

        public string Source => _renameable.First().Source;

        public string Destination
        {
            get => _renameable.First().Destination;
            set
            {
                _renameable.ForEach(x => x.Destination = value);
                OnPropertyChanged();
            }
        }

        public string ParameterName
        {
            get
            {
                var r = _renameable.FirstOrDefault();
                if (r is FamilyParameterValueWrapper v)
                    return v.ParameterName;
                return null;
            }
        }

        public string FamilyTypeName
        {
            get
            {
                var r = _renameable.FirstOrDefault();
                if (r is FamilyParameterValueWrapper v)
                    return v.FamilyTypeName;
                return null;
            }
        }

        public string GroupCondition => _renameable.First().GroupCondition;

        public bool IsChecked
        {
            get => _isChecked;
            set
            {
                _isChecked = value;
                if (!_isChecked)
                {
                    Destination = string.Empty;
                }

                OnChecked(value);
                OnPropertyChanged();
            }
        }

        public void Rename()
        {
            _renameable.ForEach(x => x.Rename());
        }

        public bool CanRename()
        {
            return true;
        }

        protected virtual void OnChecked(bool e)
        {
            Checked?.Invoke(this, e);
        }
    }
}