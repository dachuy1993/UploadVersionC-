using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
using System.IO;

namespace UploadVersion
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }     
        string pathFileExe;      
              
        public void OpenFileTHsystem()
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "All files(*.*)| *.*| Exe Files(*.exe) | *.exe*| Text File(*.txt) |*.txt";
                ofd.FilterIndex = 2;
                ofd.Multiselect = true;
                if (ofd.ShowDialog() == true)
                {
                    txt_path.Text = ofd.FileName;
                    pathFileExe = ofd.FileName;
                }
                byte[] buffer = File.ReadAllBytes(pathFileExe);
                string base64Encoded = Convert.ToBase64String(buffer);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error OpenFileDialog");
            }

        }      


        public void UploadFile()
        {
            try
            {    
                string pathSql = @"Data Source= "+txtIpAddres.Text+";Initial Catalog="+txtNameDb.Text+";Persist Security Info=True;User ID="+txtUserDb.Text +";Password="+txtPassDb.Password+"";
                if(pathFileExe!="")
                {
                    SqlConnection conn = new SqlConnection(pathSql);
                    {
                        conn.Open();
                        byte[] buffer = File.ReadAllBytes(pathFileExe);
                        string base64Encoded = Convert.ToBase64String(buffer);
                        string query = ("INSERT INTO FileUpdateV2(NameType,FileType,DateUpload,VerName,VerData,Note) VALUES('" + txt_FileName.Text + "','" + txt_FileType.Text + "','" + DateTime.Now.ToString() + "','" + txt_Version.Text + "','" + base64Encoded + "', '" + txt_Note.Text + "' )");
                        SqlCommand cmd = new SqlCommand(query, conn);
                        int count = (int)cmd.ExecuteNonQuery();
                        conn.Close();
                        MessageBox.Show("Save Data to SQL Finish", "SAVE DATA", MessageBoxButton.OK, MessageBoxImage.Information);

                    }
                }               

            }
            catch (Exception ex)
            {
                
            }

        }

        private void Btn_OpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileTHsystem();
        }

        private void Btn_UploadFile_Click(object sender, RoutedEventArgs e)
        {
            UploadFile();
        }
     
    }
}
