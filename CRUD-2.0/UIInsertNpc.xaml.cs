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

namespace CRUD_2._0
{
    /// <summary>
    /// Lógica de interacción para UIInsertNpc.xaml
    /// </summary>
    public partial class UIInsertNpc : UserControl
    {
        public UIInsertNpc()
        {
            InitializeComponent();
            Loaded += UIInsertNpc_Loaded;
        }

        private void UIInsertNpc_Loaded(object sender, RoutedEventArgs e)
        {
            boxNombre.Text = string.Empty;
            boxTipo.Text = string.Empty;
        }
    }
}
