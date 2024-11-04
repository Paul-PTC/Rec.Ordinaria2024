using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using Refuerzo2024.Model.Conexion;

namespace Refuerzo2024.Model.DTO
{
    internal class DTOEspecialidad : dbContext
    {
        private int idEspecialidad;
        private string nombreEspecialidad;
        private int idFacultad;

        public int IdEspecialidad { get => idEspecialidad; set => idEspecialidad = value; }
        public string NombreEspecialidad { get => nombreEspecialidad; set => nombreEspecialidad = value; }
        public int IdFacultad { get => idFacultad; set => idFacultad = value; }
    }
}
