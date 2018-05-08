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
    /// Interaction logic for FoodUserControl.xaml
    /// </summary>
    public partial class FoodUserControl : UserControl
    {
        public FoodUserControl()
        {
            InitializeComponent();
        }

        #region Properties
        // Item 
        private Item item;
        public Item Item
        {
            get { return item; }
            set
            {
                item = value;

                lblName.Text = item.Name;
                // Set the picture 
                imgCover.Source = new BitmapImage(new Uri(item.ItemImageThump)); 
            }
        }

        // Item type food or offer 
        private ItemTypes itemType; 
        public ItemTypes ItemType
        {
            get
            {
                return itemType; 
            }

            set
            {
                itemType = value;
            }
        }

        private string menuSection;
        public string MenuSection
        {
            get { return menuSection; }
            set
            {
                menuSection = value;
                lblMenuSection.Text = menuSection;
            }
        }

        #endregion

        #region Events 

        #endregion

        private void mainGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void btnRemove_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
    }

    public enum ItemTypes { Food, Offer } 
}
