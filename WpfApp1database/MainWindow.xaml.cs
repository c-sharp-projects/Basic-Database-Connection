using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace WpfApp1database
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection myconn;
        public MainWindow()
        {
            InitializeComponent();
            myconn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Harshal\Desktop\WpfApp1database\WpfApp1database\login.mdf;Integrated Security=True");
        }

        private void ActionListener(object sender, RoutedEventArgs e)
        {

            Button btn = (Button)sender;

            try
            {
                myconn.Open();
                switch (btn.Uid)
                {
                    case "submit":
                        SqlCommand cmd = new SqlCommand("Insert", myconn);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@username", SqlDbType.VarChar).Value = Username.Text.Trim();
                        cmd.Parameters.AddWithValue("@password", SqlDbType.VarChar).Value = Password.Text.Trim();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Inserted");
                        break;
                    case "display":
                        SqlCommand cmd2 = new SqlCommand("Display", myconn);
                        cmd2.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd2.ExecuteNonQuery();
                        DataTable dt = new DataTable();
                        dt.Load(cmd2.ExecuteReader());
                        datagridview.ItemsSource = dt.AsEnumerable().Select(row =>
                       new
                       {
                           Username = row["Name"].ToString(),
                           Password = row["Password"].ToString()
                          
                       }


                        );
                        break;

                    case "delete":
                        SqlCommand cmd3 = new SqlCommand("Delete", myconn);
                        cmd3.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd3.Parameters.AddWithValue("@Name", SqlDbType.VarChar).Value = Username.Text.Trim();
                        cmd3.Parameters.AddWithValue("@Passwd", SqlDbType.VarChar).Value = Password.Text.Trim();

                        if (cmd3.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("deleted");
                        }
                        else
                        {
                            MessageBox.Show("item not present");
                        }
                        
                        break;

                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }


            myconn.Close();
        }
    }
}
