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
    /// Lógica de interacción para UIInsertUsuarios.xaml
    /// </summary>
    public partial class UIInsertUsuarios : UserControl
    {
        public UIInsertUsuarios()
        {
            Loaded += UserControl_Loaded;
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            boxMail.Clear();
            boxName.Clear();
            boxUser.Clear();
            boxPass.Clear();
        }
    }
}
