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
    /// Lógica de interacción para UIHome.xaml
    /// </summary>
    public partial class UIHome : Window
    {

        UINews news = new UINews();
        UIModify modify = new UIModify();
        UISearcher searcher = new UISearcher();
        UserControl activeWindows = null;
        UIUpdateTables updateTables = new UIUpdateTables();
        public UIHome()
        {
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            SizeToContent = SizeToContent.WidthAndHeight;
            Loaded += UIHome_Loaded;

        }
        private void UIHome_Loaded(object sender, RoutedEventArgs e)
    {
        Top = 0;
        Left = 0;
    }

    private void OpenChildren(UserControl children)
        {
            if (activeWindows == null)
            {
                contentModifyView.Content = children;

            }
            else if(activeWindows != null) 
            {
                activeWindows = null;
            }

        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            OpenChildren(news);
        }


        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            OpenChildren(searcher);
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            OpenChildren(modify);
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            OpenChildren(updateTables);
        }
    }
}
