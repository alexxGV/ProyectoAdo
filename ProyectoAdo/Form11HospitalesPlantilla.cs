using ProyectoAdo.Data;
using ProyectoAdo.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoAdo
{
    public partial class Form11HospitalesPlantilla : Form
    {
        PlantillaContext context;
        public Form11HospitalesPlantilla()
        {
            InitializeComponent();
            this.context = new PlantillaContext();
            this.CargarHospitales();
        }

        private void CargarHospitales()
        {
            List<Hospital> hospitales = this.context.GetHospitales();
            int cont = 50;
            foreach (Hospital hospital in hospitales)
            {
                CheckBox checkBox = new CheckBox();
                checkBox.Text = hospital.Nombre;
                checkBox.Tag = hospital.idHospital;
                checkBox.Location = new Point(60, cont);
                cont += 50;

                checkBox.CheckedChanged += MostrarPlantilla;
                this.panel1.Controls.Add(checkBox);
            }
        }
        private void MostrarPlantilla(object sender, EventArgs e)
        {
            String datos = "";
            foreach (Control c in this.panel1.Controls)
            {
                if (c is CheckBox)
                {
                    if (((CheckBox)c).Checked == true)
                    {
                        String id = c.Tag.ToString();
                        datos += id + ",";
                    }
                }
            }
            this.lsvplantilla.Items.Clear();
            if (datos != "")
            {
                //TENEMOS HOSPITALES 19,22,
                datos = datos.Trim(',');
                List<Plantilla> plantillas =
                this.context.GetPlantillaHospital(datos);
                foreach (Plantilla plan in plantillas)
                {
                    ListViewItem it = new ListViewItem();
                    it.Text = plan.Apellido;
                    it.SubItems.Add(plan.Funcion);
                    it.SubItems.Add(plan.Hospital.ToString());
                    this.lsvplantilla.Items.Add(it);
                }
            }


        }
    }
}
