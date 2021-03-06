﻿using System;
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

    public partial class Form04ModificarSalas : Form
    {
        SqlConnection cn;
        SqlCommand com;
        SqlDataReader reader;

        public Form04ModificarSalas()
        {
            InitializeComponent();
            String cadena = "Data Source=LOCALHOST;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA;Password=MCSD2020";
            this.cn = new SqlConnection(cadena);
            this.com = new SqlCommand();
            
        }

        private void CargarSalas()
        {
            this.lstSalas.Items.Clear();
            String consulta = "UPDATE SALA SET NOMBRE = @NEWNAME WHERE NOMBRE = @OLDNAME";
            String newname = this.txtNombre.Text;
            String oldname = this.lstSalas.SelectedItem.ToString();
            this.com.Parameters.AddWithValue("@NEWNAME", newname);
            this.com.Parameters.AddWithValue("@OLDNAME", oldname);
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = consulta;
            this.cn.Open();
            int update = this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
            this.CargarSalas();
        
        }
    }
}
