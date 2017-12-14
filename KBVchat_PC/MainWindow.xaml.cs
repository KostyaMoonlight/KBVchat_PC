using Autofac;
using BusinessLogic.Service.Base;
using KBVchat_PC.Infrastructure;
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

namespace KBVchat_PC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            var container = DependencyConfig.GetContainer();
            var userService = container.Resolve<IUserService>();
            var frienService = container.Resolve<IFriendService>();
            
            InitializeComponent();
        }
    }
}
