using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ThrowingException
{
    public partial class frmAddProduct : Form
    {
        
        public frmAddProduct()
        {
            InitializeComponent();
        
        showProductList = new BindingSource();
    }
        private string _ProductName, _Category, _MfgDate, _ExpDate, _Description;
        private int _Quantity;
        private double _SellPrice;

        private BindingSource showProductList;


        public string Product_Name(string name)
        {
            if (!Regex.IsMatch(name, @"^[a-zA-Z]+$"))
                throw new StringFormatException("Invalid Product Name Format!");
                return name;
        }
        public int Quantity(string qty)
        {
            if (!Regex.IsMatch(qty, @"^[0-9]"))
                throw new NumberFormatException("Invalid Quantity Format!");
                return Convert.ToInt32(qty);
        }
        public double SellingPrice(string price)
        {
            if (!Regex.IsMatch(price.ToString(), @"^(\d*\.)?\d+$"))
                throw new CurrencyFormatException("Invalid Price Format!");
                return Convert.ToDouble(price);

        
    }
        private void frmAddProduct_Load(object sender, EventArgs e)
        {
            string[] ListOfProductCategory = new string[]
            {"Beverages", "Bread/Bakery", "Canned/Jarred Goods",
            "Dairy", "Frozen Goods", "Meat", "Personal Care", "Other"};

            foreach (string Category in ListOfProductCategory)
            {
                cbCategory.Items.Add(Category);
            }
        }
        class StringFormatException : Exception
        {
            public StringFormatException(string name) : base(name) { }
        }
        class NumberFormatException : Exception
        {
            public NumberFormatException(string qty) : base(qty) { }
        }
        class CurrencyFormatException : Exception
        {
            public CurrencyFormatException(string price) : base(price) { }
        }
        
        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
            {
            
            }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                _ProductName = Product_Name(txtProductName.Text);
                _Category = cbCategory.Text;
                _MfgDate = dtPickerMfgDate.Value.ToString("yyyy-MM-dd");
                _ExpDate = dtPickerExpDate.Value.ToString("yyyy-MM-dd");
                _Description = richTxtDescription.Text;
                _Quantity = Quantity(txtQuantity.Text);
                _SellPrice = SellingPrice(txtSellPrice.Text);

                showProductList.Add(new ProductClass(_ProductName, _Category, _MfgDate,
                _ExpDate, _SellPrice, _Quantity, _Description));
                gridViewProductList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                gridViewProductList.DataSource = showProductList;
            }
            catch (StringFormatException sfe)
            {
                MessageBox.Show(sfe.Message, "Error Format", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (NumberFormatException nfe)
            {
                MessageBox.Show(nfe.Message, "Error Format", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            catch (CurrencyFormatException cfe)
            {
                MessageBox.Show(cfe.Message, "Error Format", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                Console.WriteLine("Finally block executed.");
            }

        }
    }
    }

 

