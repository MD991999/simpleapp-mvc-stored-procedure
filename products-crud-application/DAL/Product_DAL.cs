 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using products_crud_application.Models;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.Runtime.Remoting.Messaging;

namespace products_crud_application.DAL
{
    public class Product_DAL
    {
        //access the v=connection string from web.config()
        string conString = ConfigurationManager.ConnectionStrings["adoconnectionString"].ToString(); //why we need to convert it into string

        //first we are gonna define a function that access the full data from the database using the storedprocedure
        public List<product> GetAllProducts()
        {
            List<product> ProductList = new List<product>();
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand(); //what is the purpose of this step
                command.CommandType = CommandType.StoredProcedure; //why stored procedure
                command.CommandText = "SP_GetAllProducts"; //heree we introduce the name of stored procedure
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();

                connection.Open();
                adapter.Fill(dt);
                connection.Close();

                //now the data is available in the table,what we do next is we will access this data row by row and introduce them as a list of itms in the productList

                foreach (DataRow dr in dt.Rows)
                {
                    ProductList.Add(new product
                    {
                        ProductID = Convert.ToInt32(dr["ProductID"]),
                        ProductName = dr["ProductName"].ToString(),
                        Price = Convert.ToDecimal(dr["Price"]),
                        Qty = Convert.ToInt32(dr["qty"]),
                        Remarks = dr["Remarks"].ToString(),

                    });

                }
            }
            return ProductList;
        }


        //insert a product details

        public bool InsertProducts(product product)
        //second product means the data that we are going to store in the backend,first product shows that the daata that we are gonna store is of the type of the model that we created in the innitial state.

        {
            int i = 0;
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("SPI_InsertAlldetails", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ProductName", product.ProductName);
                command.Parameters.AddWithValue("@Price", product.Price);
                command.Parameters.AddWithValue("@Qty", product.Qty);
                command.Parameters.AddWithValue("@Remarks", product.Remarks);
                connection.Open();
                i = command.ExecuteNonQuery(); //ExecuteNonQuery will have either 1 or 0 as its value.If we insert the data successfully the value that we obtain will be 1 other wise it will be 0.
                connection.Close();

            }
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // to get the userdetails ny using the id

        public List<product> GetAllProductsbyId(int ProductID)
        {
            List<product> ProductList = new List<product>();
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand(); //what is the purpose of this step
                command.CommandType = CommandType.StoredProcedure; //why stored procedure
                command.CommandText = "SPG_getitemByid"; //heree we introduce the name of stored procedure
                command.Parameters.AddWithValue("@ProductID", ProductID);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();

                connection.Open();
                adapter.Fill(dt);
                connection.Close();

                //now the data is available in the table,what we do next is we will access this data row by row and introduce them as a list of itms in the productList

                foreach (DataRow dr in dt.Rows)
                {
                    ProductList.Add(new product
                    {
                        ProductID = Convert.ToInt32(dr["ProductID"]),
                        ProductName = dr["ProductName"].ToString(),
                        Price = Convert.ToDecimal(dr["Price"]),
                        Qty = Convert.ToInt32(dr["qty"]),
                        Remarks = dr["Remarks"].ToString(),

                    });

                }
            }
            return ProductList;
        }

        // to update the userdetails

        public bool UpdateProducts(product product)
        //second product means the data that we are going to store in the backend,first product shows that the daata that we are gonna store is of the type of the model that we created in the innitial state.

        {
            int i = 0;
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("SPU_UpdateAlldetails", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ProductID", product.ProductID);
                command.Parameters.AddWithValue("@ProductName", product.ProductName);
                command.Parameters.AddWithValue("@Price", product.Price);
                command.Parameters.AddWithValue("@Qty", product.Qty);
                command.Parameters.AddWithValue("@Remarks", product.Remarks);
                connection.Open();
                i = command.ExecuteNonQuery(); //ExecuteNonQuery will have either 1 or 0 as its value.If we insert the data successfully the value that we obtain will be 1 other wise it will be 0.
                connection.Close();

            }
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string DeleteProduct(int productID)
        {
            string result = "";
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("SPD_deleteuserdetails", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ProductID", productID);
                command.Parameters.Add("@outputmessage", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                //the above method means adding a new parameter,its called return  message and it is of the type varchar with 50 length and by parameterdirection.Output wat it means is that  it will retrieve a value from the stored procedure.
                connection.Open();
                command.ExecuteNonQuery();
                result = command.Parameters["@outputmessage"].Value.ToString();
                //by the above method we wil retrieve the value of returnmessage from it and store them in a variable called result.
                connection.Close();
            }

            return result;
            //second product means the data that we are going to store in the backend,first product shows that the daata that we are gonna store is of the type of the model that we created in the innitial state.



        }



    }
}