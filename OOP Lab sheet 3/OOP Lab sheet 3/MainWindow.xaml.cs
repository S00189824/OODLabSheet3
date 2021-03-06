﻿using System;
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

namespace OOP_Lab_sheet_3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        NORTHWNDEntities db = new NORTHWNDEntities();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnQueryEx1_Click(object sender, RoutedEventArgs e)//Exercise 1
        {
            var query = from c in db.Customers
                        select c.CompanyName;

            lbxCustomer.ItemsSource = query.ToList();
        }

        private void BtnQueryEx2_Click(object sender, RoutedEventArgs e)//Exercise 2
        {
            var query = from c in db.Customers
                        select c;

            DAtaGridCustomerEx2.ItemsSource = query.ToList();
        }

        private void BtnQueryEx3_Click(object sender, RoutedEventArgs e)//Exercise 3 Order Information
        {
            var Query = from o in db.Orders
                        where o.Customer.City.Equals("London")
                        || o.Customer.City.Equals("Paris")
                        || o.Customer.City.Equals("USA")
                        orderby o.Customer.CompanyName
                        select new
                        {
                            CustomerName = o.Customer.CompanyName,
                            city = o.Customer.City,
                            Address = o.Customer.Address
                        };

            lbxCustomerEx3.ItemsSource = Query.ToList();

        }

        private void BtnQueryEx4_Click(object sender, RoutedEventArgs e)//Exercise 4 Product Information
        {
            var Query = from p in db.Products
                        where p.Category.CategoryName.Equals("Beverages")
                        orderby p.ProductID descending
                        select new
                        {
                            p.ProductID,
                            p.ProductName,
                            p.Category.CategoryName,
                            p.UnitPrice
                        };

            lbxCustomerEx4.ItemsSource = Query.ToList();
        }

        private void BtnQueryEx5_Click(object sender, RoutedEventArgs e)//Exercise 5 Insert Information
        {
            Product p = new Product()
            {
                ProductName = "Kickapoo Jungle Joy Juice",
                UnitPrice = 12.55m,
                CategoryID = 1
            };

            db.Products.Add(p);
            db.SaveChanges();


            ShowProducts(lbxCustomerEx5);
            
        }

        private void ShowProducts(DataGrid currentGrid)
        {
            var query = from p in db.Products
                        where p.Category.CategoryName.Equals("Beverages")
                        orderby p.ProductID descending
                        select new
                        {
                            p.ProductID,
                            p.ProductName,
                            p.Category.CategoryName,
                            p.UnitPrice
                        };

            currentGrid.ItemsSource = query.ToList();
        }

        private void BtnQueryEx6_Click(object sender, RoutedEventArgs e)//Exercise 6 Update Product Information
        {
            Product p1 = (db.Products
                .Where(p => p.ProductName.StartsWith("Kick"))
                .Select(p => p)).First();

            p1.UnitPrice = 10m;

            db.SaveChanges();
            ShowProducts(lbxCustomerEx6);

        }

        private void BtnQueryEx7_Click(object sender, RoutedEventArgs e)//Exercise 7 Multiple Update
        {
            var products = from p in db.Products
                           where p.ProductName.StartsWith("Ch")
                           select p;

            foreach (var item in products)
            {
                item.UnitPrice = 100m;
            }

            db.SaveChanges();
            ShowProducts(lbxCustomerEx7); 
        }

        private void BtnQueryEx8_Click(object sender, RoutedEventArgs e)//Exercise 8 Delete
        {
            var products = from p in db.Products
                           where p.ProductName.StartsWith("Kick")
                           select p;


            db.Products.RemoveRange(products);
            db.SaveChanges();
            ShowProducts(lbxCustomerEx8);
        }

        private void BtnQueryEx9_Click(object sender, RoutedEventArgs e)//Exercise 9 Stored Procedure
        {
            var query = db.Customers_By_City("London");

            lbxCustomerEx9.ItemsSource = query.ToList();
        }
    }
}
