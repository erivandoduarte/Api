using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entidade
{
    public class Pessoa
    {
        [Key]
        public int Id { get; set; }

        public string DscCPF { get; set; }

        public string DscNome { get; set; }

        public string DscTelCelular { get; set; }

        public string DscEmail { get; set; }

        public string DscEndereco { get; set; }

        public string DscCEP { get; set; }

        public string DscBairro { get; set; }

        public string DscMunicipio { get; set; }

        public string DscUF { get; set; }
    }
}