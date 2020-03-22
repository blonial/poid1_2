using poid.Commands;
using poid.Models;
using poid.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace poid.ViewModels
{
    public class MaskCreatorViewModel : NotifyPropertyChangedEvent
    {
        #region Properties

        public string Name { get; }

        private int X { get; }

        private int Y { get; }

        private MaskCreatorView Window { get; }

        private List<TextBox> Values = new List<TextBox>();

        private List<List<int>> Mask { get; set; } = null;

        #endregion

        #region Constructors

        public MaskCreatorViewModel(int x, int y, string name, MaskCreatorView window)
        {
            this.X = x;
            this.Y = y;
            this.Name = name;
            this.Window = window;
            this.InitializeCommands();
        }

        #endregion

        #region Initializers

        public void InitializeGrid()
        {
            Grid mask = this.Window.Mask;
            for (int i = 0; i < this.Y; i++)
            {
                RowDefinition rowDefinition = new RowDefinition();
                rowDefinition.Height = new System.Windows.GridLength(1, System.Windows.GridUnitType.Auto);
                mask.RowDefinitions.Add(rowDefinition);
                for (int j = 0; j < this.X; j++)
                {
                    ColumnDefinition columnDefinition = new ColumnDefinition();
                    columnDefinition.Width = new System.Windows.GridLength(1, System.Windows.GridUnitType.Auto);
                    mask.ColumnDefinitions.Add(columnDefinition);
                    TextBox textBox = new TextBox();
                    Values.Add(textBox);
                    Grid.SetRow(textBox, i);
                    Grid.SetColumn(textBox, j);
                    textBox.Width = 36;
                    textBox.MaxLength = 3;
                    mask.Children.Add(textBox);
                }
            }
        }

        private void InitializeCommands()
        {
            this._CreateMask = new RelayCommand(this.CreateMask);
        }

        #endregion

        #region Commands

        public ICommand _CreateMask { get; private set; }

        #endregion

        #region Methods

        private void CreateMask(object sender)
        {
            try
            {
                this.Mask = new List<List<int>>();
                for (int i = 0; i < this.Y; i++)
                {
                    this.Mask.Add(new List<int>());
                    for (int j = 0; j < this.X; j++)
                    {
                        int index = i * this.X + j;
                        this.Mask[i].Add(int.Parse(this.Values[i * this.X + j].Text));
                    }
                }
                this.Window.Close();
            }
            catch (Exception)
            {
                this.Mask = null;
                Notify.Error("Invalid input values!\nValue must be and int!");
            }
        }

        public List<List<int>> GetMask()
        {
            return this.Mask;
        }

        #endregion
    }
}
