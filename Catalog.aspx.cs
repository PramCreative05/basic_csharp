using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Text;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;


namespace Lite_TaiMeng
{
    public partial class Catalog : System.Web.UI.Page
    {
        string connString = "Data Source=LAPTOP-TGN70FFC\\SQLEXPRESS;Initial Catalog = TaiwanCarCatalog; Integrated Security = True";

        protected void Page_Load(object sender, EventArgs e)
        {
            {
                if (!this.IsPostBack)
                {
                    SqlConnection conn = new SqlConnection(connString);
                    string sqlSTD = "SELECT * FROM [TblMaker]";
                    SqlDataAdapter DatAdapter = new SqlDataAdapter(sqlSTD, conn);
                    DataSet DatSet = new DataSet();
                    DatAdapter.Fill(DatSet);

                    DropDownList1.DataSource = DatSet.Tables[0];
                    DropDownList1.DataTextField = "BrandName";
                    DropDownList1.DataValueField = "BrandName";
                    DropDownList1.DataBind();
                }
            }

        }

        //Basic Data with Brand
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Literal1.Text = genData();

            /*
             * Check Box dynamically avoid BUG
             * CheckBox CheckLength = new CheckBox();
            CheckLength.ID = "CheckBox1";
            CheckLength.Text = " Length View";
            onche*/
        }

        //Data with length
        
        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox1.Checked)
            {
                StringBuilder length = new StringBuilder();
                length.Append("<div class='col' style='color: transparent; flex-basis: 0.1%'>.</div>");
                length.Append("<div class='col col-1'>Length</div>");
                Literal2.Text = length.ToString();

                Literal1.Text = genLength();


            }
            else
            {
                Response.Redirect(Request.RawUrl);
            }
            
        }

        //================================================================BASIC DATA===================================================================================================//
        //Generate Basic Data
        public string genData()
        {
            DataTable dt = this.GetData();
            StringBuilder html = new StringBuilder();

            foreach (DataRow row in dt.Rows)
            {
                string Car = row["Car Model"].ToString();
                html.Append("<li class='table-row'>");

                //responsive on 767 ==>image max: 500 px car max 5500 mm
                //https://stackoverflow.com/questions/56914377/how-to-hide-and-show-table-columns-with-selected-check-box
                //https://www.codeproject.com/Questions/655887/hide-and-show-table-column-using-CheckBox

                html.Append("<div class='col col-0' data-label='Image'><img src='graph/" + Car + ".PNG' width='210'></div>");
                html.Append("<div class='col col-1' data-label='Brand'>" + row["BrandName"].ToString() + "</div>");
                html.Append("<div class='col col-2' data-label='Car Model'>" + Car + "</div>");
                html.Append("<div class='col col-3' data-label='Style Name'>" + row["StyleName"].ToString() + "</div>");
                html.Append("<div class='col col-4' data-label='Price Range'>" + row["Price Range"].ToString() + "</div>");
                html.Append("<div class='col col-5' data-label='Fuel Efficiency'>" + row["Fuel Type and Efficiency"].ToString() + "</div>");
                html.Append("<div class='col col-6' data-label='Engine'>" + row["Engine"].ToString() + "</div>");
                html.Append("<div class='col col-7' data-label='CC-Power'>" + row["CC and Power"].ToString() + "</div>");
                html.Append("<div class='col col-8' data-label='Transmission'>" + row["Transmission"].ToString() + "</div>");
                html.Append("<div class='col col-9' data-label='Seating'>" + row["Seating Capacity"].ToString() + "</div>");
                html.Append("<div class='col col-10' data-label='More Info'><a href=" + row["Reference Link"].ToString() + " target=_blank>"+ Car +" - Taiwan 2020</a></div>");

                /*string[] columnNames = (from dc in dt.Columns.Cast<DataColumn>() select dc.ColumnName).ToArray();
                foreach (DataColumn column in dt.Columns)
                {
                    int i = dt.Columns.IndexOf(column);
                    int colCount = dt.Columns.Count;

                    for (i = 3; i < colCount; i++)
                    {
                        html.Append("<div class='col col-" + column.Ordinal + "' data-label='" + column.ColumnName + "'>");
                        html.Append(row[column.ColumnName]);
                        html.Append("</div>");
                    }
                }
                I hope that this can be automated, ***the answer is already in ur SOF
                 */

                html.Append("</li>");
            }
            return html.ToString();
        }

        //Get the Basic Data
        public DataTable GetData()
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand join = new SqlCommand("SELECT TblMaker.BrandName, TblCars.[Car Model], TblStyle.StyleName, TblCars.[Price Range], TblCars.[Fuel Type and Efficiency], TblCars.[Engine], TblCars.[CC and Power], TblCars.[Transmission], TblCars.[Seating Capacity], TblCars.[Reference Link]" +
                "FROM TblCars " +
                "JOIN TblMaker ON TblMaker.BrandID = TblCars.Brand JOIN TblStyle ON TblCars.[Body Type] = TblStyle.StyleID", conn);
            SqlDataAdapter joinTable = new SqlDataAdapter(join);
            DataTable InJoin = new DataTable();
            joinTable.Fill(InJoin);

            string BrandName = DropDownList1.SelectedItem.Value;
            if (!string.IsNullOrEmpty(BrandName))
            {
                //https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/creating-a-datatable-from-a-query-linq-to-dataset

                if (BrandName == "All")
                {
                    var result = from row in InJoin.AsEnumerable()
                                 select row;
                    DataTable Fin = new DataTable();
                    Fin = result.CopyToDataTable();
                    return Fin;
                }
                else
                {
                    var result = from row in InJoin.AsEnumerable()
                                 where row.Field<string>("BrandName") == BrandName
                                 select row;
                    DataTable Fin = new DataTable();
                    Fin = result.CopyToDataTable();
                    return Fin;
                }
            }
            else 
            {
                return InJoin;
            }
        }
        //================================================================END BASIC DATA===================================================================================================//

        //================================================================GET DATA WITH LENGTH===================================================================================================//
        public DataTable GetLength()
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand join = new SqlCommand("SELECT TblMaker.BrandName, TblCars.[Length (meter)], TblCars.[Car Model], TblStyle.StyleName, TblCars.[Price Range], TblCars.[Fuel Type and Efficiency], TblCars.[Engine], TblCars.[CC and Power], TblCars.[Transmission], TblCars.[Seating Capacity], TblCars.[Reference Link]" +
                "FROM TblCars " +
                "JOIN TblMaker ON TblMaker.BrandID = TblCars.Brand JOIN TblStyle ON TblCars.[Body Type] = TblStyle.StyleID", conn);
            SqlDataAdapter joinTable = new SqlDataAdapter(join);
            DataTable InJoin = new DataTable();
            joinTable.Fill(InJoin);

            string BrandName = DropDownList1.SelectedItem.Value;
            if (!string.IsNullOrEmpty(BrandName))
            {
                //https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/creating-a-datatable-from-a-query-linq-to-dataset

                if (BrandName == "All")
                {
                    var result = from row in InJoin.AsEnumerable()
                                 select row;
                    DataTable Fin = new DataTable();
                    Fin = result.CopyToDataTable();
                    return Fin;
                }
                else
                {
                    var result = from row in InJoin.AsEnumerable()
                                 where row.Field<string>("BrandName") == BrandName
                                 select row;
                    DataTable Fin = new DataTable();
                    Fin = result.CopyToDataTable();
                    return Fin;
                }
            }
            else
            {
                return InJoin;
            }
        }

        public string genLength()
        {
            DataTable dt = this.GetLength();
            StringBuilder html = new StringBuilder();

            foreach (DataRow row in dt.Rows)
            {
                string Car = row["Car Model"].ToString();
                string Length = row["Length (meter)"].ToString();

                int number;

                bool success = Int32.TryParse(Length,out number);
                if (success)
                {

                }
                else
                {
                    Console.WriteLine("Attempted conversion of '{0}' failed.",
                                       Length ?? "<null>");
                }


                // 5.5 m ~ 210 px
                //1 m ~ 38 px
                double x = number * 0.038 ;
                double L = number * 0.001;


                html.Append("<li class='table-row'>");

                html.Append("<div class='col col-0' data-label='Image'><img src='graph/" + Car + "Cal.PNG' width='"+x.ToString()+"'></div>");

                html.Append("<div class='" +
                    "col' data-label='.' style='color: transparent; flex-basis: 0.1%'><img src='graph/length.PNG' width='210'></div>");
                html.Append("<div class='col col-1' data-label='Length'>" + L.ToString() + " meter</div>");

                html.Append("<div class='col col-1' data-label='Brand'>" + row["BrandName"].ToString() + "</div>");
                html.Append("<div class='col col-2' data-label='Car Model'>" + Car + "</div>");
                html.Append("<div class='col col-3' data-label='Style Name'>" + row["StyleName"].ToString() + "</div>");
                html.Append("<div class='col col-4' data-label='Price Range'>" + row["Price Range"].ToString() + "</div>");
                html.Append("<div class='col col-5' data-label='Fuel Efficiency'>" + row["Fuel Type and Efficiency"].ToString() + "</div>");
                html.Append("<div class='col col-6' data-label='Engine'>" + row["Engine"].ToString() + "</div>");
                html.Append("<div class='col col-7' data-label='CC-Power'>" + row["CC and Power"].ToString() + "</div>");
                html.Append("<div class='col col-8' data-label='Transmission'>" + row["Transmission"].ToString() + "</div>");
                html.Append("<div class='col col-9' data-label='Seating'>" + row["Seating Capacity"].ToString() + "</div>");
                html.Append("<div class='col col-10' data-label='More Info'><a href=" + row["Reference Link"].ToString() + " target=_blank>" + Car + " - Taiwan 2020</a></div>");

                html.Append("</li>");
            }
            return html.ToString();
        }
        //================================================================END GET DATA WITH LENGTH===================================================================================================//
    }
}