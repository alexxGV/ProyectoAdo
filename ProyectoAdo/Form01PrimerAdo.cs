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

namespace ProyectoAdo
{
    public partial class Form01PrimerAdo : Form
    {
        String cadenaconexion;
        SqlConnection cn;
        SqlCommand com;
        SqlDataReader reader;
        public Form01PrimerAdo()
        {
            InitializeComponent();
            this.cadenaconexion = "Data Source=LOCALHOST;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA;Password=MCSD2020";
            this.cn = new SqlConnection(this.cadenaconexion);
            this.com = new SqlCommand();
            this.cn.StateChange += Cn_StateChange;
        }

        private void Cn_StateChange(object sender, StateChangeEventArgs e)
        {
            this.lblEstado.Text = "Connection pasando de " + e.OriginalState + " a " + e.CurrentState;
        }

        private void btConectar_Click(object sender, EventArgs e)
        {
            if (this.cn.State == ConnectionState.Closed)
            {
                this.cn.Open();
                this.lblEstado.BackColor = Color.LightGreen;
            }
        }

        private void btDesconectar_Click(object sender, EventArgs e)
        {
            this.cn.Close();
            this.lblEstado.BackColor = Color.LightCoral;
        }

        private void btLeerDatos_Click(object sender, EventArgs e)
        {
            //INDICAR AL COMANDO LA CONEXION
            this.com.Connection = this.cn;
            //TIPO DE CONSULTA A REALIZAR
            this.com.CommandType = CommandType.Text;
            //LA CONSULTA A BBDD
            this.com.CommandText = "select * from emp";
            //EJECUTAR EL COMANDO CON CONEXION ABIERTA
            this.reader = this.com.ExecuteReader();
            for (int i = 0; i < this.reader.FieldCount; i++)
            {

                String columna = this.reader.GetName(i);
                String tipo = this.reader.GetDataTypeName(i);

                this.lstColumnas.Items.Add(columna);
                this.lstTipos.Items.Add(tipo);
            }

            while (this.reader.Read())
            {

                String apellido = this.reader.GetString(1);
                this.lstApellidos.Items.Add(apellido);
            }
            this.reader.Close();
        }
    }
}
