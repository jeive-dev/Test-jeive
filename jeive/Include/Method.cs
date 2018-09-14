using jeive.Models;



namespace jeive.Include
{
    public class Method : IMethod
    {
        private readonly DBConnect _bd;

        public Method(DBConnect x)
        {
            _bd = x;
        }

        public void Add(Variable.Config configs)
        {
            _bd.api_config.Add(configs);
            _bd.SaveChanges();
        }  
    }
}
