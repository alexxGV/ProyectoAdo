using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

#region PROCEDIMIENTOS
    //CREATE PROCEDURE CARGARDEPARTAMENTOS
    //AS
    //	SELECT * FROM DEPT
    //GO

    //CREATE PROCEDURE INSERTARDEPARTAMENTO (@NUMERO INT, @NOMBRE NVARCHAR(50), @LOCALIDAD NVARCHAR(50))
    //AS
    //    INSERT INTO  DEPT VALUES (@NUMERO, @NOMBRE, @LOCALIDAD)
    //GO
#endregion

namespace ProyectoAdo
{
    public partial class Form06MensajesServidor : Form
    {
        SqlConnection cn;
        SqlCommand com;
        SqlDataReader reader;

        public Form06MensajesServidor()
        {
            InitializeComponent();
            String host = "Data Source=localhost;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA;Password=MCSD2020";
            cn = new SqlConnection(host);
            com = new SqlCommand();
            this.com.Connection = this.cn;
            this.cn.InfoMessage += Cn_InfoMessage;
            this.CargarDepartamentos();
        }

        private void Cn_InfoMessage(object sender, SqlInfoMessageEventArgs e)
        {
            this.lblMensaje.Text = e.Message;
        }

        private void CargarDepartamentos(){
            this.lstDepartamentos.Items.Clear();
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = "CARGARDEPARTAMENTOS";
            this.cn.Open();
            this.reader = this.com.ExecuteReader();

            while (this.reader.Read())
            {
                this.lstDepartamentos.Items.Add(this.reader["DEPT_NO"] + " - " + this.reader["DNOMBRE"] + " - " + this.reader["LOC"]);
            }
            this.reader.Close();
            this.cn.Close();
        }

        private void btInsertar_Click(object sender, EventArgs e)
        {
            this.lblMensaje.Text = "";
            int numero = int.Parse(this.txtNumero.Text);
            String nombre = this.txtNombre.Text;
            String localidad = this.txtLocalidad.Text;

            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = "INSERTARDEPARTAMENTO";
            this.com.Parameters.AddWithValue("@NUMERO", numero);
            this.com.Parameters.AddWithValue("@NOMBRE", nombre);
            this.com.Parameters.AddWithValue("@LOCALIDAD", localidad);
            this.cn.Open();
            this.com.ExecuteNonQuery();
            this.com.Parameters.Clear();
            this.cn.Close();
            this.CargarDepartamentos();

        }
    }
}
