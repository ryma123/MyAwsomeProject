using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Data;
using System.Linq;
using BusinessLayer.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Text;

namespace KPI_Dashboard.BusinessLayer
{
    public class DashboardDataAccess
    {
        private static MySqlConnection connection;
        public IReader Reader;

        public DashboardDataAccess()
        {



           Reader = new ExcelReader();
           DataTable dt = (DataTable)Reader.Read();


              AddProduct(dt);



        }
        public void AddProduct(DataTable dt)
        {
           // dt = (DataTable)Reader.Read();
            var numberofrows = dt.Rows.Count;

            for (int i = 0; i < numberofrows; i++)
            {
                //for each row, get the 3rd column
                var Prodid = dt.Rows[i]["product"];
                var releaseid = dt.Rows[i]["release"];
                var Locationid = dt.Rows[i]["Location"];
                var kpiRawData = dt.Rows[i]["a"];

                //    var Prodid = "äbcOFF";
                //var releaseid = "vcv1222OFF";
                //var Locationid = "end122OFF";
                //var kpiRawData = "3121OFF";


                using (var db = new Relation())
                {

                    var record = new product() { ProductId = Prodid.ToString(), locatioh = Locationid.ToString() };
                    var recordw = new Release() { Product = record, Releaseid = releaseid.ToString() };
                    var recordk = new KPI() { product = record, release = recordw, SomeValue = kpiRawData.ToString() };

                    {
                        db.release.Add(recordw); db.product.Add(record); db.kpi.Add(recordk);
                       // try
                        {
                            db.SaveChanges();
                        }

                       // catch (DbUpdateException dbu)
                        {
                           // var exception = HandleDbUpdateException(dbu);
                           // throw exception;
                        }
                    }

                    // }
                }
            }
        }

        private Exception HandleDbUpdateException(DbUpdateException dbu)
        {
            var builder = new StringBuilder("A DbUpdateException was caught while saving changes. ");

            try
            {
                foreach (var result in dbu.Entries)
                {
                    builder.AppendFormat("Type: {0} was part of the problem. ", result.Entity.GetType().Name);
                }
            }
            catch (Exception e)
            {
                builder.Append("Error parsing DbUpdateException: " + e.ToString());
            }

            string message = builder.ToString();
            return new Exception(message, dbu);
        }

        public List<string> UpdateProductComboBox()
        {
            var productList = new List<string>();
            using(var db = new Relation())
            {  foreach (product p in db.product)
                { productList.Add(p.ProductId); }
               
            }
            return productList;




            //var productsQuery = "Select distinct *  From product";
            //string readItem = "ProductName";
            //return Read(productsQuery, readItem);
            
        }
        public int GetPercent(string version)
        {

            
            using (var db = new Relation())
            {
                foreach (KPI kp in db.kpi)
                { if (kp.release.Releaseid == version)

                    { return  Convert.ToInt32(kp.SomeValue); } }

            }
            return -1;


           

        }
        public List<string> UpdateVersionComboBox(string selectedProduct)
        {
            var versionlist = new List<string>();
            using (var db = new Relation())
            {
                foreach (Release r in db.release)
                {if (r.Product.ProductId== selectedProduct)

                    versionlist.Add(r.Releaseid); }

            }
            return versionlist;




            //var updateVersionQuery = "Select `release` from db_kpi_dashboard.`release` where ProductName = '" + 
            //    selectedProduct + "'";
            //var readItem = "release";

            //return  Read(updateVersionQuery, readItem);
        }

        //private List<string> Read(string query, string fieldName)
        //{
        //    var output = new List<string>();
        //    var command = new MySqlCommand
        //    {
        //        CommandType = CommandType.Text,
        //        CommandText = query,
        //        Connection = connection
        //    };

        //    connection.Open();
        //    command.ExecuteNonQuery();


        //    using (var reader = command.ExecuteReader())
        //    {
        //        while (reader.Read())
        //        {
        //            output.Add(reader[fieldName].ToString());
        //        }
        //        connection.Close();
        //    }

        //    return output;
        //}





    }


}
