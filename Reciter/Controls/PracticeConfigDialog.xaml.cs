using Reciter.Models;
using Reciter.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpf.Ui.Controls;

namespace Reciter.Controls
{
    /// <summary>
    /// PracticeConfig.xaml 的交互逻辑
    /// </summary>
    public partial class PracticeConfigDialog : ContentDialog
    {
        public PracticeConfig Config { get; set; } = new();
        public PracticeConfigDialog(ContentPresenter contentPresenter) : base(contentPresenter)
        {
            InitializeComponent();
            OrderComboBox.ItemsSource = Enum.GetValues(typeof(PracticeConfig.OrderType))
                .Cast<PracticeConfig.OrderType>()
                .Select(e => new { Value = e, Description = e.GetDescription() })
                .ToList();

            OrderComboBox.DisplayMemberPath = "Description";
            OrderComboBox.SelectedValuePath = "Value";
            OrderComboBox.SelectedIndex = 0;

            SelectionComboBox.ItemsSource = Enum.GetValues(typeof(PracticeConfig.SelectionType))
                .Cast<PracticeConfig.SelectionType>()
                .Select(e => new { Value = e, Description = e.GetDescription() })
                .ToList();

            SelectionComboBox.DisplayMemberPath = "Description";
            SelectionComboBox.SelectedValuePath = "Value";
            SelectionComboBox.SelectedIndex = 0;
        }

        private void OrderComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = OrderComboBox.SelectedItem as dynamic;
            if (selectedItem != null)
            {
                Config.Order = selectedItem.Value;
            }
        }

        private void SelectionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = SelectionComboBox.SelectedItem as dynamic;
            if (selectedItem != null)
            {
                Config.Selection = selectedItem.Value;
            }
        }
    }
}
