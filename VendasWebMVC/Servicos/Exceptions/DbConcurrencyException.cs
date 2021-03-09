using System;

namespace VendasWebMVC.Servicos.Exceptions {
    public class DbConcurrencyException : ApplicationException{

        public DbConcurrencyException(string message): base(message) {

        }
    }
}
