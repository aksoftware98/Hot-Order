using OneClickDelivery.Models;
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

namespace OneClickDelivery.DeliveryOrderSystem.Windows.UserControls
{
    /// <summary>
    /// Interaction logic for MenuSectionUserControl.xaml
    /// </summary>
    public partial class MenuSectionUserControl : UserControl
    {
        public MenuSectionUserControl()
        {
            InitializeComponent();
        }

        #region Properties
        // Menu Section Proeprty 
        private MenuSections menuSection;

        public MenuSections MenuSection
        {
            get { return menuSection;  }
            set
            {
                menuSection = value;

                lblName.Text = menuSection.ToString();

                // Set the image 
                switch (menuSection)
                {
                    case MenuSections.Sandwiches:
                        imgCover.Source = new BitmapImage(new Uri("/Images/MenuSections/Sandwiches.jpg", UriKind.Relative));
                        break;
                    case MenuSections.Pizza:
                        imgCover.Source = new BitmapImage(new Uri("/Images/MenuSections/Pizza.jpg", UriKind.Relative));
                        break;
                    case MenuSections.Burger:
                        imgCover.Source = new BitmapImage(new Uri("/Images/MenuSections/Burger.jpg", UriKind.Relative));
                        break;
                    case MenuSections.HotDogs:
                        imgCover.Source = new BitmapImage(new Uri("/Images/MenuSections/HotDogs.jpg", UriKind.Relative));
                        break;
                    case MenuSections.Soups:
                        imgCover.Source = new BitmapImage(new Uri("/Images/MenuSections/Soups.jpg", UriKind.Relative));
                        break;
                    case MenuSections.Meals:
                        imgCover.Source = new BitmapImage(new Uri("/Images/MenuSections/Meals.jpg", UriKind.Relative));
                        break;
                    case MenuSections.Appetizers:
                        imgCover.Source = new BitmapImage(new Uri("/Images/MenuSections/Appetizers.jpg", UriKind.Relative));
                        break;
                    case MenuSections.Beverages:
                        imgCover.Source = new BitmapImage(new Uri("/Images/MenuSections/Beverages.jpg", UriKind.Relative));
                        break;
                    case MenuSections.Chickens:
                        imgCover.Source = new BitmapImage(new Uri("/Images/MenuSections/Chickens.jpg", UriKind.Relative));
                        break;
                    case MenuSections.Salad:
                        imgCover.Source = new BitmapImage(new Uri("/Images/MenuSections/Salads.jpg", UriKind.Relative));
                        break;
                    case MenuSections.Add_Ons:
                        imgCover.Source = new BitmapImage(new Uri("/Images/MenuSections/AddOns.jpg", UriKind.Relative));
                        lblName.Text = "Extras";
                        break;
                    default:
                        break;
                }

                this.ToolTip = lblName.Text;
            }

            
    }
        // Menu Section Object Property 
        public MenuSection Section { get; set; }
        #endregion


        #region Events
        public event EventHandler RemoveClick = delegate { };
        #endregion

        private void MainGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            RemoveClick(this, null);
        }
    }

    public enum MenuSections
    {
        Sandwiches, Pizza, Burger, HotDogs,Soups,Meals, Appetizers,Beverages, Chickens, Salad, Add_Ons
    }
}
