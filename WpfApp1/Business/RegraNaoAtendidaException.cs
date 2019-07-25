using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Business
{
    public class RegraNaoAtendidaException : Exception
    {
        public RegraNaoAtendidaException() : base()
        {

        }

        public RegraNaoAtendidaException(String mensagem) : base(mensagem)
        {

        }
    }
}
