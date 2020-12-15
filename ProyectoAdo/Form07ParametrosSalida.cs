using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

#region PROCEDIMIENTOS ALMACENADOS
    //CREATE PROCEDURE EMPLEADOSDEPTOUT (@NOMBRE NVARCHAR(30), @SUMA INT OUT)
    //AS
    //    DECLARE @DEPTNO INT
	   // SELECT @DEPTNO = DEPT_NO FROM DEPT WHERE DNOMBRE = @NOMBRE

	   // SELECT * FROM EMP WHERE DEPT_NO = @DEPTNO

	   // SELECT @SUMA = SUM(SALARIO) FROM EMP WHERE DEPT_NO = @DEPTNO
    //GO

#endregion

namespace ProyectoAdo
{
    public partial class Form07ParametrosSalida : Form
    {
        SqlConnection cn;
        SqlCommand com;
        SqlDataReader reader;

        public Form07ParametrosSalida()
        {
            InitializeComponent();
            String color = ConfigurationManager.AppSettings["colorfondo"];
            //this.BackColor = Color.FromName(color);

            String cadena = ConfigurationManager.ConnectionStrings["conexionHospital"].ConnectionString;
            this.cn = new SqlConnection(cadena);
            this.com = new SqlCommand();
            this.com.Connection = this.cn;
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = "CARGARDEPARTAMENTOS";
            this.cn.Open();
            this.reader = this.com.ExecuteReader();

            while (this.reader.Read())
            {
                this.cmbDepartamentos.Items.Add(this.reader["DNOMBRE"]);
            }
            this.reader.Close();
            this.cn.Close();
        }

        private void cmbDepartamentos_SelectedIndexChanged(object sender, EventArgs e)
        {
            String nombre = this.cmbDepartamentos.SelectedItem.ToString();
            this.com.Parameters.AddWithValue("@NOMBRE", nombre);
            SqlParameter pamsuma = new SqlParameter();
            pamsuma.ParameterName = "@SUMA";
            pamsuma.Direction = ParameterDirection.Output;
            pamsuma.Value = 0;
            this.com.Parameters.Add(pamsuma);
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = "EMPLEADOSDEPTOUT";
            this.cn.Open();
            this.reader = this.com.ExecuteReader();
            this.lstEmpleados.Items.Clear();
            while (this.reader.Read())
            {
                this.lstEmpleados.Items.Add(this.reader["APELLIDO"]);
            }
            this.reader.Close();
            this.txtSuma.Text = pamsuma.Value.ToString();
            this.cn.Close();
            this.com.Parameters.Clear();

        }
    }
}
