using poid.Commands;
using poid.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace poid.ViewModels
{
    public class ImageMasksViewModel : NotifyPropertyChangedEvent
    {
        #region Properties

        public List<RegionMask> Masks { get; private set; }

        private RegionMask _SelectedRegionMask;
        public RegionMask SelectedRegionMask
        {
            get
            {
                return _SelectedRegionMask;
            }
            set
            {
                _SelectedRegionMask = value;
                NotifyPropertyChanged("SelectedRegionMask");
            }
        }

        public List<Color> Colors { get; } = new List<Color> { Color.Black, Color.White, Color.Red, Color.Green, Color.Blue };

        private Color _SelectedColor;
        public Color SelectedColor
        {
            get
            {
                return _SelectedColor;
            }
            set
            {
                _SelectedColor = value;
                NotifyPropertyChanged("SelectedColor");
            }
        }

        private Bitmap _Mask;
        public Bitmap Mask
        {
            get
            {
                return _Mask;
            }
            private set
            {
                _Mask = value;
                NotifyPropertyChanged("Mask");
            }
        }

        private Bitmap _Image;
        public Bitmap Image
        {
            get
            {
                return _Image;
            }
            private set
            {
                _Image = value;
                NotifyPropertyChanged("Image");
            }
        }

        private bool _Working = false;
        public bool Working
        {
            get
            {
                return _Working;
            }
            private set
            {
                _Working = value;
                NotifyPropertyChanged("Working");
            }
        }

        #endregion

        #region Constructors

        public ImageMasksViewModel(List<RegionMask> masks)
        {
            this.Masks = masks;
            this.InitializeSelectedColor();
            this.InitializeCommands();
            this.InitializeEventListeners();
        }

        #endregion

        #region Initializers

        private void InitializeSelectedColor()
        {
            this.SelectedColor = this.Colors[0];
        }

        private void InitializeCommands()
        {
            this._GenerateImageWithMask = new RelayCommand(o => this.SelectedRegionMask != null, this.GenerateImageWithMask);
        }

        public void InitializeEventListeners()
        {
            this.PropertyChanged += HandleSelectedColorChange;
            this.PropertyChanged += HandleSelectedRegionMaskChange;
        }

        #endregion

        #region Commands

        public ICommand _GenerateImageWithMask { get; private set; }

        #endregion

        #region Event listeners

        private void HandleSelectedRegionMaskChange(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "SelectedRegionMask")
            {
                this.Image = null;
                if (this.SelectedRegionMask != null)
                {
                    this.Working = true;
                    Task generateMaskTask = new Task(new Action(() => {
                        this.Mask = this.SelectedRegionMask.GenerateMask();
                        this.Working = false;
                    }));
                    generateMaskTask.Start();
                }
            }
        }

        private void HandleSelectedColorChange(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "SelectedColor")
            {
                if (this.Image != null && this.SelectedRegionMask != null)
                {
                    this.Working = true;
                    Task generateImageTask = new Task(new Action(() => {
                        this.Image = this.SelectedRegionMask.GenerateImageWithMask(this.SelectedColor);
                        this.Working = false;
                    }));
                    generateImageTask.Start();
                }
            }
        }

        #endregion

        #region Methods

        private void GenerateImageWithMask(object o)
        {
            this.Image = this.SelectedRegionMask.GenerateImageWithMask(this.SelectedColor);
        }

        #endregion
    }
}
