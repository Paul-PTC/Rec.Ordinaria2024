using Refuerzo2024.Model.DAO;
using Refuerzo2024.View.Especialidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Refuerzo2024.Controller.Estudiantes
{
    internal class ControlerEspecialidad
    {
        Especialidad objvista;

        public ControlerEspecialidad(Especialidad vista)
        {
            objvista = vista;
            vista.Load += new EventHandler(cargainicial);
            objvista.dgvEstudiantes.CellClick += new DataGridViewCellEventHandler(SeleccionarDato);
            objvista.btnAgregar.Click += new EventHandler(RegistrarEstudiante);
            objvista.btnActualizar.Click += new EventHandler(ActualizarEstudiante);
            objvista.btnEliminar.Click += new EventHandler(EliminarEstudiante);
            objvista.btnBuscar.Click += new EventHandler(BuscarEstudiante);
            vista.btnFacultades.Click += new EventHandler(AbrirFormEstudiantes);
            vista.btnLimpiar.Click += new EventHandler(Limpiar);
        }
        public void Limpiar(object sender, EventArgs e)
        {
            objvista.txtNombres.Text = string.Empty;
            objvista.txtID.Text = string.Empty;
            objvista.cmbEspecialidad.SelectedItem = 0;
        }
        public void cargainicial(object sender, EventArgs e)
        {
            cargarDTG();
            LlenarComboEspecialidades();
        }
        private void LlenarComboEspecialidades()
        {
            //Se crea objeto del DAOEstudiantes para accesar a todos los metodos contenidos en la clase.
            DAOEspecialidad obj = new DAOEspecialidad();
            //Se crea un DataSet que almacenará los valores que retorne el metodo.
            DataSet ds = obj.ObtenerEstudiantes();
            //Llenamos el combobox
            objvista.cmbEspecialidad.DataSource = ds.Tables["Facultades"];
            //Se indica que campo se mostrará al usuario
            objvista.cmbEspecialidad.DisplayMember = "nombreFacultad";
            //Se indica que valor será seleccionado dependiendo de lo que elija el usuario
            objvista.cmbEspecialidad.ValueMember = "idFacultad";
        }
        public void SeleccionarDato(object sender, DataGridViewCellEventArgs e)
        {
            //Capturar la fila a la que se le dió click
            int pos = objvista.dgvEstudiantes.CurrentRow.Index;
            //Enviar los datos del DataGridView hacia los controles
            objvista.txtID.Text = objvista.dgvEstudiantes[0, pos].Value.ToString();
            objvista.txtNombres.Text = objvista.dgvEstudiantes[1, pos].Value.ToString();
            objvista.cmbEspecialidad.Text = objvista.dgvEstudiantes[2, pos].Value.ToString();
        }
        public void cargarDTG()
        {
            DAOEspecialidad obj = new DAOEspecialidad();
            //Se crea un DataSet que almacenará los valores que retorne el metodo.
            DataSet ds = obj.ObtenerFacultades();
            //Llenamos el combobox
            objvista.dgvEstudiantes.DataSource = ds.Tables["Especialidades"];
        }
        public void RegistrarEstudiante(object sender, EventArgs e)
        {
            DAOEspecialidad data = new DAOEspecialidad();
            //Guardar en los atributos del DTO todos los valores contenidos en los componentes del formulario
            data.NombreEspecialidad = objvista.txtNombres.Text.Trim();
            data.IdFacultad = (int)objvista.cmbEspecialidad.SelectedValue;
            //Se invoca al metodo RegistrarEstudiante y se verifica si su retorno es TRUE, de ser así significa que los datos pudieron ser registrados correctamente, de lo contrario, no se pudo registrar los valores.
            if (data.RegistrarEstudiante() == true)
            {
                MessageBox.Show("Datos registrados correctamente", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No se pudo guardar los datos", "Proceso incompleto", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            cargarDTG();
        }
        public void ActualizarEstudiante(object sender, EventArgs e)
        {
            DAOEspecialidad data = new DAOEspecialidad();
            //Guardar en los atributos del DTO todos los valores contenidos en los componentes del formulario
            data.IdEspecialidad = int.Parse(objvista.txtID.Text.Trim().ToString());
            data.NombreEspecialidad = objvista.txtNombres.Text.Trim();
            data.IdFacultad = (int)objvista.cmbEspecialidad.SelectedValue;

            if (data.ActualizarEstudiante() == true)
            {
                MessageBox.Show("Los datos fueron actualizados correctamente", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cargarDTG();
            }
            else
            {
                MessageBox.Show("Los datos no pudieron ser actualizados.", "Proceso interrumpido", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void EliminarEstudiante(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(objvista.txtID.Text.Trim()))
            {
                MessageBox.Show("Seleccione un registro", "Seleccione un valor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DAOEspecialidad data = new DAOEspecialidad();
                data.IdFacultad = int.Parse(objvista.txtID.Text);
                if (MessageBox.Show("¿Desea eliminar el registro seleccionado?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (data.EliminarEstudiante() == true)
                    {
                        MessageBox.Show("El dato fue eliminado correctamente", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cargarDTG();
                    }
                    else
                    {
                        MessageBox.Show("El registro no pudo ser eliminado", "Proceso interrumpido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        public void BuscarEstudiante(object sender, EventArgs e)
        {
            DAOFacultades data = new DAOFacultades();
            DataSet ds = data.BuscarEstudiante(objvista.txtBuscar.Text.Trim());
            objvista.dgvEstudiantes.DataSource = ds.Tables["Especialidades"];
        }
        public void AbrirFormEstudiantes(object sender, EventArgs e)
        {
            Facultades form = new Facultades();
            form.ShowDialog();
            cargarDTG();
        }

    }
}
