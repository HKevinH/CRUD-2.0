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
    /// Lógica de interacción para UIInsertMisiones.xaml
    /// </summary>
    public partial class UIInsertMisiones : UserControl
    {
        public UIInsertMisiones()
        {
            InitializeComponent();
            Loaded += UIInsertMisiones_Loaded;
        }

        private void UIInsertMisiones_Loaded(object sender, RoutedEventArgs e)
        {
            boxNombre.Text = string.Empty;
            boxObjectivos.Text = string.Empty;
            boxRequisitos.Text = string.Empty;
        }
    }
}
