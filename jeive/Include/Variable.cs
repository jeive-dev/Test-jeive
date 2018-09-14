using System;

namespace jeive.Include
{
    public class Variable 
    {
        public class Config
        {
            public int Id { get; set; }
            public string Loja { get; set; }
            public string Visa { get; set; }    
            public string Master { get; set; }
            public bool Safety { get; set; }
        }

        public class RegistroTransactions
        {
            public int Id { get; set; }
            public string Loja { get; set; }
            public DateTime Date { get; set; }
            public string Registro { get; set; }
        } 


    }
}
