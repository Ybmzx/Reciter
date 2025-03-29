using Reciter.ViewModels;
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
using Wpf.Ui.Abstractions.Controls;

namespace Reciter.Views.Pages
{
    /// <summary>
    /// SpellPage.xaml 的交互逻辑
    /// </summary>
    public partial class SpellPage : Page, INavigationAware
    {
        public SpellPage()
        {
            InitializeComponent();
        }

        public Task OnNavigatedFromAsync()
        {
            return Task.CompletedTask;
        }

        public Task OnNavigatedToAsync()
        {
            if (this.DataContext is not null)
            {
                InputTextBox.Focus();
                (this.DataContext as SpellPageVM).NavigatedTo();
            }
            return Task.CompletedTask;
        }
    }
}
