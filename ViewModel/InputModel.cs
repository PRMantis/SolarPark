
namespace SolarPark.ViewModel
{
    public class InputModel
    {
        #region Members
        int _panelWidth;
        int _panelLength;
        int _rowSpacing;
        int _columnSpacing;
        int _tiltAngle;
        #endregion

        #region Properties
        public int PanelWidth
        {
            get { return _panelWidth; }
            set { _panelWidth = value; }
        }

        public int PanelLength
        {
            get { return _panelLength; }
            set { _panelLength = value; }
        }
        public int RowSpacing
        {
            get { return _rowSpacing; }
            set { _rowSpacing = value; }
        }
        public int ColumnSpacing
        {
            get { return _columnSpacing; }
            set { _columnSpacing = value; }
        }
        public int TiltAngle
        {
            get { return _tiltAngle; }
            set { _tiltAngle = value; }
        }
        #endregion
    }
}
