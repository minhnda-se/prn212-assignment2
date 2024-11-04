using BusinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace Assignment2
{
    /// <summary>
    /// Interaction logic for ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IProductRepository productRepository;
        public ProductWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            categoryRepository = new CategoryRepository();
            productRepository = new ProductRepository();
            Window_Loaded();
        }


        private void Window_Loaded()
        {
            dtgProduct.ItemsSource = null;
            dtgProduct.ItemsSource = productRepository.GetAllProducts().Select(p => new
            {
                p.ProductId,
                p.ProductName,
                p.UnitsInStock,
                p.UnitPrice,
                CategoryName = p.Category != null ? p.Category.CategoryName : "No Category"
            }).ToList();

            cbCategory.ItemsSource = categoryRepository.GetCategories();
            cbCategory.SelectedValuePath = "CategoryId";
            cbCategory.DisplayMemberPath = "CategoryName";
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Select_Change(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex);
            if (row != null)
            {
                DataGridCell cell = dataGrid.Columns[0].GetCellContent(row).Parent as DataGridCell;
                if (cell != null)
                {
                    int id = int.Parse(((TextBlock)cell.Content).Text);
                    Product product = productRepository.GetProductById(id);
                    if (product != null)
                    {
                        SaveDataToWindow(product);
                    }
                }
            }
        }
        
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Product product = productRepository.GetProductById(int.Parse(txtProductId.Text));
            if (product == null)
            {
                Product newProduct = SaveDataToDB();
                bool result = productRepository.SaveProducts(newProduct);
                if ( result)
                {
                    Window_Loaded();
                    SaveDataToWindow(newProduct);
                    MessageBox.Show("Save successfully!!");
                }
                else
                {
                    MessageBox.Show("Save failed!");
                }
            }
            else
            {
                MessageBox.Show("The product is exist!!");
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Product product = productRepository.GetProductById(int.Parse(txtProductId.Text));
            if (product != null)
            {
                bool result = productRepository.UpdateProduct(SaveDataToDB());
                if (result)
                {
                    Window_Loaded();
                    SaveDataToWindow(product);
                    MessageBox.Show("Update successfully!!");
                }
                else
                {
                    MessageBox.Show("Update failed!");
                }
            }
            else
            {
                MessageBox.Show("No product available!!");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Product product = productRepository.GetProductById(int.Parse(txtProductId.Text));
            if (product != null)
            {
                bool result = productRepository.DeleteProduct(int.Parse(txtProductId.Text));
                if (result)
                {
                    Window_Loaded();
                    ClearData();
                    MessageBox.Show("Delete successfully!!");
                }
                else
                {
                    MessageBox.Show("Delete failed!");
                }
            }
            else
            {
                MessageBox.Show("No product available!!");
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SaveDataToWindow(Product product)
        {
            txtProductId.Text = product.ProductId.ToString();
            txtName.Text = product.ProductName;
            txtPrice.Text = product.UnitPrice.ToString();
            txtStock.Text = product.UnitsInStock.ToString();
            cbCategory.SelectedValue = product.CategoryId;
        }

        private Product SaveDataToDB()
        {
            return new Product
            (
                int.Parse(txtProductId.Text),
                txtName.Text,
                int.Parse(cbCategory.SelectedValue.ToString()),
                short.Parse(txtStock.Text),
                decimal.Parse(txtPrice.Text)
            );
        }

        private void ClearData()
        {
            txtProductId.Text = string.Empty; 
            txtName.Text = string.Empty; 
            txtPrice.Text = string.Empty; 
            txtStock.Text = string.Empty; 
            cbCategory.SelectedValue = null;
        }

        private void txtProductId_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextNumeric(e.Text);
        }

        private bool IsTextNumeric(string text)
        {
            return Regex.IsMatch(text, @"^\d+$"); 
        }
    }

}
