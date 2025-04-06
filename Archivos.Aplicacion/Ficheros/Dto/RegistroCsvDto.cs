
namespace Ficheros.Aplicacion.Archivos.Dto
{
    public class RegistroCsvDto
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public Guid IdProveedor { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int IdMedida { get; set; }
        public int IdCategoria { get; set; }
        public int IdMarca { get; set; }
        public int IdColor { get; set; }
        public int IdModelo { get; set; }
        public int IdMaterial { get; set; }
        public string UrlFoto1 { get; set; }
        public string UrlFoto2 { get; set; }
    }
}
