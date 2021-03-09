using System;

namespace VendasWebMVC.Servicos.Exceptions {
    public class NotFoundException: ApplicationException{

        public NotFoundException(string message) : base(message) {

        }
    }
}
