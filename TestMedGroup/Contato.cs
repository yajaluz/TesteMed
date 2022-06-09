using System;

namespace TestMedGroup
{
    public class Contato
    {
        public Guid Id { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }
        public char Sexo { get; set; }
    }
}
