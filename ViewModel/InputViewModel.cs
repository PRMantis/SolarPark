using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SolarPark.ViewModel
{
    class InputViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        InputModel _inputModel;

        public InputViewModel()
        {
            _inputModel = new InputModel { PanelWidth = 0, PanelLength = 0, RowSpacing = 0, ColumnSpacing = 0, TiltAngle = 0 };
        }

        public string this[string name]
        {
            get
            {
                string result = null;

                if (name == "TiltAngle")
                {
                    if (this.TiltAngle > 60)
                    {
                        result = "Tilt angle must be less or equal to 60";
                    }
                }
                return result;
            }
        }

        public string Error
        {
            get
            {
                return null;
            }
        }

        public InputModel InputModel
        {
            get
            {
                return _inputModel;
            }
            set
            {
                _inputModel = value;
            }
        }

        public int PanelWidth
        {
            get { return InputModel.PanelWidth; }
            set
            {
                if (InputModel.PanelWidth != value)
                {
                    InputModel.PanelWidth = value;
                    RaisePropertyChanged("PanelWidth");
                }
            }
        }

        public int PanelLength
        {
            get { return InputModel.PanelLength; }
            set
            {
                if (InputModel.PanelLength != value)
                {
                    InputModel.PanelLength = value;
                    RaisePropertyChanged("PanelLength");
                }
            }
        }
        public int RowSpacing
        {
            get { return InputModel.RowSpacing; }
            set
            {
                if (InputModel.RowSpacing != value)
                {
                    InputModel.RowSpacing = value;
                    RaisePropertyChanged("RowSpacing");
                }
            }
        }
        public int ColumnSpacing
        {
            get { return InputModel.ColumnSpacing; }
            set
            {
                if (InputModel.ColumnSpacing != value)
                {
                    InputModel.ColumnSpacing = value;
                    RaisePropertyChanged("ColumnSpacing");
                }
            }
        }

        public int TiltAngle
        {
            get { return InputModel.TiltAngle; }
            set
            {
                if (InputModel.TiltAngle != value)
                {
                    InputModel.TiltAngle = value;
                    RaisePropertyChanged("TiltAngle");
                }
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Methods

        private void RaisePropertyChanged(string propertyName)
        {
            // take a copy to prevent thread issues
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
