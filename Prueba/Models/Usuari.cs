using System;
using System.Collections.Generic;
using System.Text;

namespace TodoApi.Model
{
    /// <summary>
    /// Clase modelo que representa al objeto de la base de datos ConnectionString sus atributos- Deben coincidir ConnectionString los de la bbdd.
    /// </summary>
    public class Usuari
    {
        public int id { get; set; }
        public string nom { get; set; }
        public string cognom { get; set; }
        public string nick { get; set; }
        public string contrasenya { get; set; }
        public string pais { get; set; }
        public int puntuacion { get; set; }
        public bool admin { get; set; }
    }
}
