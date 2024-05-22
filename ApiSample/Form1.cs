using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using Newtonsoft.Json;
using Hotcakes.CommerceDTO.v1.Client;
using Hotcakes.CommerceDTO.v1.Contacts;
using Hotcakes.CommerceDTO.v1;
using System.Security.Cryptography;
using ApiSample;
using System.Security.Policy;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ApiSample
{
    public partial class Form1 : Form
    {
        List<string> selectedItems = new List<string>();
        Dictionary<string, string> selectedMenuItems = new Dictionary<string, string>();

        private static readonly HttpClient client = new HttpClient();
        static string url = "http://20.234.113.211:8106";
        static string key = "1-2d78ad9c-d5ca-46e7-8cbc-066d8e72b40c";

        Api proxy = new Api(url, key);
        private readonly ApiClient _apiClient;

        // Control elements
        System.Windows.Forms.ComboBox comboBox1;
        System.Windows.Forms.ComboBox comboBox2;
        System.Windows.Forms.ComboBox comboBox3;
        CheckedListBox checkedListBox1;
        CheckedListBox checkedListBox2;
        Label labelMenu;
        Label labelAheti;
        Label labelBheti;
        System.Windows.Forms.Button buttonTovabbA;
        System.Windows.Forms.Button buttonTovabbB;

        // Static variable for MenuProductID
        private static int currentMenuProductID = 100;

        public Form1()
        {
            InitializeComponent();
            _apiClient = new ApiClient();



            this.Text = "Ázsiai heti menü";
            this.Size = new Size(900, 660);
            this.BackColor = Color.Black;

            // Label setup
            labelMenu = new Label
            {
                Text = "Heti menü",
                Location = new Point(400, 5),
                Size = new Size(200, 30),
                Font = new Font("Arial", 18, FontStyle.Bold),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter
            };

            labelAheti = new Label
            {
                Text = "A heti",
                Location = new Point(150, 70),
                Size = new Size(100, 30),
                Font = new Font("Arial", 14, FontStyle.Bold),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter
            };

            labelBheti = new Label
            {
                Text = "B heti",
                Location = new Point(600, 70),
                Size = new Size(100, 30),
                Font = new Font("Arial", 14, FontStyle.Bold),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Panel setup
            RoundedPanel panelA = new RoundedPanel
            {
                Location = new Point(30, 100),
                Size = new Size(400, 450),
                BackColor = Color.FromArgb(51, 25, 25),
                CornerRadius = 20
            };

            RoundedPanel panelB = new RoundedPanel
            {
                Location = new Point(450, 100),
                Size = new Size(400, 450),
                BackColor = Color.FromArgb(51, 25, 25),
                CornerRadius = 20
            };

            RoundedPanel panelM = new RoundedPanel
            {
                Location = new Point(30, 10),
                Size = new Size(820, 40),
                BackColor = Color.FromArgb(51, 25, 25),
                CornerRadius = 20
            };

            //Button gomb
            buttonTovabbA = new System.Windows.Forms.Button
            {
                Text = "Mentés",
                Location = new Point(750, 560),
                Size = new Size(100, 40),
                BackColor = Color.FromArgb(102, 51, 51),
                ForeColor = Color.White,
                Font = new Font("Arial", 12, FontStyle.Bold)

            };
            buttonTovabbA.Click +=  ButtonTovabbA_Click;


            buttonTovabbB = new System.Windows.Forms.Button
            {
                Text = "Mentés",
                Location = new Point(330, 560),
                Size = new Size(100, 40),
                BackColor = Color.FromArgb(102, 51, 51),
                ForeColor = Color.White,
                Font = new Font("Arial", 12, FontStyle.Bold)
            };
            buttonTovabbB.Click += ButtonTovabbB_Click;



            // ComboBox for menu types
            comboBox1 = new System.Windows.Forms.ComboBox
            {
                Location = new Point(280, 5),
                Size = new Size(150, 50),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Arial", 12)
            };
            comboBox1.Items.AddRange(new string[] { "Ázsiai", "Amerikai", "Európai", "Vegyes" });
            comboBox1.SelectedIndex = 0;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;



            // ComboBox for days of the week (A heti)
            comboBox2 = new System.Windows.Forms.ComboBox
            {
                Location = new Point(75, 80),
                Size = new Size(250, 50),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Arial", 12)
            };
            comboBox2.Items.AddRange(new string[] { "Hétfő", "Kedd", "Szerda", "Csütörtök", "Péntek" });
            comboBox2.SelectedIndex = 0;
            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;

            // CheckedListBox for menu items (A heti)
            checkedListBox1 = new CheckedListBox
            {
                Location = new Point(75, 130),
                Size = new Size(250, 300),
                Font = new Font("Arial", 12)
            };
            checkedListBox1.SelectionMode = SelectionMode.One;
            checkedListBox1.ItemCheck += checkedListBox1_ItemCheck;

            // ComboBox for days of the week (B heti)
            comboBox3 = new System.Windows.Forms.ComboBox
            {
                Location = new Point(75, 80),
                Size = new Size(250, 50),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Arial", 12)
            };
            comboBox3.Items.AddRange(new string[] { "Hétfő", "Kedd", "Szerda", "Csütörtök", "Péntek" });
            comboBox3.SelectedIndex = 0;
            comboBox3.SelectedIndexChanged += comboBox3_SelectedIndexChanged;

            // CheckedListBox for menu items (B heti)
            checkedListBox2 = new CheckedListBox
            {
                Location = new Point(75, 130),
                Size = new Size(250, 300),
                Font = new Font("Arial", 12)
            };
            checkedListBox2.SelectionMode = SelectionMode.One;
            checkedListBox2.ItemCheck += checkedListBox2_ItemCheck;



            // Adding controls to panels

            panelA.Controls.Add(comboBox2);
            panelA.Controls.Add(checkedListBox1);

            panelB.Controls.Add(comboBox3);
            panelB.Controls.Add(checkedListBox2);

            panelM.Controls.Add(comboBox1);
            panelM.Controls.Add(labelMenu);

            // Adding panels and labels to the form
            this.Controls.Add(panelM);

            this.Controls.Add(labelAheti);
            this.Controls.Add(labelBheti);
            this.Controls.Add(panelA);
            this.Controls.Add(panelB);
            this.Controls.Add(buttonTovabbA);
            this.Controls.Add(buttonTovabbB);


            // Initializing items
            LoadMenuItems("Ázsiai");


        }

      
        private async void ButtonTovabbB_Click(object sender, EventArgs e)
        {
            await UpdateMenuBAsync();
        }

        private async void ButtonTovabbA_Click(object sender, EventArgs e)
        {
            await UpdateMenuAAsync();
        }

        private async Task UpdateMenuAAsync()
        {
            string selectedDay = comboBox3.SelectedItem.ToString();
            string selectedItem = checkedListBox2.CheckedItems.Count > 0 ? checkedListBox2.CheckedItems[0].ToString() : null;

            if (string.IsNullOrEmpty(selectedItem))
            {
                MessageBox.Show("Please select a menu item.");
                return;
            }

            var productDetails = GetProductDetails(selectedItem);
            if (productDetails == null)
            {
                MessageBox.Show("Failed to get product details.");
                return;
            }

            var menuCategory = new MenuCategory
            {
                MenuProductsID = currentMenuProductID++,
                Category = comboBox1.Text,
                Day = selectedDay,
                OptionsID = 1,
                Price = productDetails.Price,
                ProductName = productDetails.ProductName,
                Quantity = productDetails.Quantity,
                SKU = int.Parse(productDetails.SKU)
            };

            var success = await _apiClient.UpdateMenuAsync(menuCategory.MenuProductsID, menuCategory);

            if (success)
            {
                MessageBox.Show("Menu updated successfully!");
            }
            else
            {
                MessageBox.Show("Failed to update menu.");
            }
        }

        private async Task UpdateMenuBAsync()
        {
            string selectedDay = comboBox2.SelectedItem.ToString();
            string selectedItem = checkedListBox1.CheckedItems.Count > 0 ? checkedListBox1.CheckedItems[0].ToString() : null;

            if (string.IsNullOrEmpty(selectedItem))
            {
                MessageBox.Show("Please select a menu item.");
                return;
            }

            var productDetails = GetProductDetails(selectedItem);
            if (productDetails == null)
            {
                MessageBox.Show("Failed to get product details.");
                return;
            }

            var menuCategory = new MenuCategory
            {
                MenuProductsID = currentMenuProductID++,
                Category = comboBox1.Text,
                Day = selectedDay,
                OptionsID = 2,
                Price = productDetails.Price,
                ProductName = productDetails.ProductName,
                Quantity = productDetails.Quantity,
                SKU = int.Parse(productDetails.SKU)
            };

            var success = await _apiClient.UpdateMenuAsync(menuCategory.MenuProductsID, menuCategory);

            if (success)
            {
                MessageBox.Show("Menu updated successfully!");
            }
            else
            {
                MessageBox.Show("Failed to update menu.");
            }
        }



        private ProductDetails GetProductDetails(string productName)
        {
            var snaps = proxy.ProductsFindAll();
            if (snaps != null && snaps.Content != null)
            {
                foreach (var snap in snaps.Content)
                {
                    if (snap.ProductName == productName)
                    {
                        return new ProductDetails
                        {
                            ProductName = snap.ProductName,
                            SKU = snap.Sku,
                            Price = snap.ListPrice, 
                            Quantity = 100 
                        };
                    }
                }
            }
            return null;
        }

        public class ProductDetails
        {
            public string ProductName { get; set; }
            public string SKU { get; set; }
            public decimal Price { get; set; }
            public int Quantity { get; set; }
        }






        
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            string selectedCategory = comboBox1.SelectedItem.ToString();
            checkedListBox1.Items.Clear();
            LoadMenuItems(selectedCategory);
           
            checkedListBox2.Items.Clear();
            LoadMenuItems(selectedCategory);

        }




        private async void LoadMenuItems(string category)
        {

            var snaps = proxy.ProductsFindAll();
            if (snaps != null && snaps.Content != null)
            {
                foreach (var snap in snaps.Content)
                {
                    if (IsCategoryMatch(snap.Sku, category))
                    {
                        checkedListBox1.Items.Add(snap.ProductName);
                        checkedListBox2.Items.Add(snap.ProductName);
                    }
                }
            }
            else
            {
                MessageBox.Show("Hiba történt a termékek lekérdezése közben.");
            }
        }


            private bool IsCategoryMatch(string sku, string category)
        {
            switch (category)
            {
                case "Ázsiai":
                    return sku.StartsWith("11") || sku.StartsWith("12");
                case "Amerikai":
                    return sku.StartsWith("13");
                case "Európai":
                    return sku.StartsWith("14") || sku.StartsWith("15") || sku.StartsWith("16") || sku.StartsWith("17") || sku.StartsWith("18") || sku.StartsWith("19");
                case "Vegyes":
                    return sku.StartsWith("11") || sku.StartsWith("12") || sku.StartsWith("13") || sku.StartsWith("14") || sku.StartsWith("15") || sku.StartsWith("16") || sku.StartsWith("17") || sku.StartsWith("18") || sku.StartsWith("19");
                default:
                    return false;
            }
        }




        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

            string selectedDay = comboBox2.SelectedItem.ToString();
            if (selectedMenuItems.ContainsKey(selectedDay))
            {
                int index = checkedListBox1.Items.IndexOf(selectedMenuItems[selectedDay]);
                if (index >= 0)
                {
                    checkedListBox1.SetItemChecked(index, true);
                }
            }
            else
            {
                foreach (int index in checkedListBox1.CheckedIndices)
                {
                    checkedListBox1.SetItemChecked(index, false);
                }
            }

            
        }


        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {
                foreach (int index in checkedListBox1.CheckedIndices)
                {
                    checkedListBox1.SetItemChecked(index, false);
                }
                string selectedDay = comboBox2.SelectedItem.ToString();
                selectedMenuItems[selectedDay] = checkedListBox1.Items[e.Index].ToString();
            }

           

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedDay = comboBox3.SelectedItem.ToString();
            if (selectedMenuItems.ContainsKey(selectedDay))
            {
                int index = checkedListBox2.Items.IndexOf(selectedMenuItems[selectedDay]);
                if (index >= 0)
                {
                    checkedListBox2.SetItemChecked(index, true);
                }
            }
            else
            {
                foreach (int index in checkedListBox2.CheckedIndices)
                {
                    checkedListBox2.SetItemChecked(index, false);
                }
            }

          


        }

        

        private void checkedListBox2_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {
                foreach (int index in checkedListBox2.CheckedIndices)
                {
                    checkedListBox2.SetItemChecked(index, false);
                }
                string selectedDay = comboBox3.SelectedItem.ToString();
               selectedMenuItems[selectedDay] = checkedListBox2.Items[e.Index].ToString();
            }

            
        }








        
    }
}




