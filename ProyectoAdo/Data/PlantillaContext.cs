using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using ProyectoAdo.Models;

namespace ProyectoAdo.Data
{
    public class PlantillaContext
    {
        private SqlConnection cn;
        private SqlCommand com;
        private SqlDataReader reader;

        public PlantillaContext()
        {
            String cadena = ConfigurationManager.ConnectionStrings["conexionhospital"].ConnectionString;
            this.cn = new SqlConnection(cadena);
            this.com = new SqlCommand();
            this.com.Connection = this.cn;
        }

        public List<Hospital> GetHospitales()
        {
            this.com.CommandType = System.Data.CommandType.Text;
            this.com.CommandText = "SELECT * FROM HOSPITAL";
            this.cn.Open();
            this.reader = this.com.ExecuteReader();
            List<Hospital> hospitales = new List<Hospital>();
            while (this.reader.Read())
            {
                Hospital hospital = new Hospital();
                hospital.idHospital = Convert.ToInt32(this.reader["HOSPITAL_COD"]);
                hospital.Nombre = this.reader["NOMBRE"].ToString();
                hospitales.Add(hospital);
            }
            this.reader.Close();
            this.cn.Close();
            return hospitales;
        }

        public List<Plantilla> GetPlantillaHospital(String datos)
        {
            List<Plantilla> plantillas = new List<Plantilla>();
            String sql =
            "select * from plantilla where hospital_cod in(" + datos + ")";
            this.com.CommandType = System.Data.CommandType.Text;
            this.com.CommandText = sql;
            this.cn.Open();
            this.reader = this.com.ExecuteReader();
            while (this.reader.Read())
            {
                Plantilla plan = new Plantilla();
                plan.IdPlantilla = Convert.ToInt32(this.reader["EMPLEADO_NO"]);
                plan.Apellido = this.reader["APELLIDO"].ToString();
                plan.Funcion = this.reader["FUNCION"].ToString();
                plan.Hospital = Convert.ToInt32(this.reader["HOSPITAL_COD"]);
                plantillas.Add(plan);
            }
            this.reader.Close();
            this.cn.Close();
            return plantillas;
        }

    }
}
