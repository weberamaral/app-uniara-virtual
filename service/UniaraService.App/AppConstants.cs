using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniaraService.App
{
    public class AppConstants
    {
        /*
         * Urls
         */
        public const string LOGIN_URL = "http://virtual.uniara.com.br/login";
        public const string HISTORICO_URL = "http://virtual.uniara.com.br/alunos/consultas/historico/";
        public const string FALTAS_URL = "http://virtual.uniara.com.br/alunos/consultas/faltas";
        public const string INDEX_URL = "http://virtual.uniara.com.br/alunos/index/";
        public const string NOTICIAS_URL = "http://www.uniara.com.br/noticias/";
        public const string AULAS_URL = "http://virtual.uniara.com.br/alunos/consultas/horario/";

        /*
         * Mensagens
         */

        public const string INVALID_CREDENTIAL = "Credencial Inválida";
        public const string REQUEST_OK = "Ok";
        public const string REQUEST_FAIL = "Ocorreu um erro ao processar sua requisição";
        public const string NOT_AUTHRIZED = "Você não possui autorização para consumir esse serviço";
    }
}
