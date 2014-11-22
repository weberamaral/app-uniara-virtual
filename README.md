service-uniara-virtual-service-1.0
==================================

Serviço REST que possibilita o consumo de informações referente á notas, faltas e aulas dos alunos da Uniara.

<h2>Uso básico</h2>

Em cada requisição, deverá ser passado no HEADER: <i>as credencias do aluno</i>

Exemplo:
<code>Authorization: Basic QWxhZGRpbjpvcGVuIHNlc2FtBmU=</code>

O trecho <i>QWxhZGRpbjpvcGVuIHNlc2FtBmU=</i> representa a credencial do aluno no seguinte formato:
<strong>{matricula}:{senha}</strong> em encode Base64.

<h2>Descrição da resposta</h2>

Toda resposta do servidor é em JSON, possuindo as seguintes chaves:

* <strong>MessageResult</strong><br/>
    Representa uma mensagem amigável retornada do servidor. Em caso que a requisição foi bem sucedida todas as mensagens
    de retorno serão "Ok"
* <strong>RequestDate</strong><br/>
    Datetime da requisição realizada no servidor
* <strong>StatusCodeResult</strong><br/>
    Código de Status da resposta no servidor. Os possiveis valores são:<br/>
    200: A requisição foi processado com sucesso no servidor<br/>
    403: A requisição foi processada com sucesso no servidor, porém você não possui autorização para consumir o serviço ou
    a credencial so aluno não é valida.<br/>
    500: Ocorreu um erro na requisição. Erro interno
* <strong>DataResult</strong><br/>
    Informações do aluno.

<h2>Métodos disponíveis</h2>

Listagem de todos os métodos disponíveis para consumo na aplicação

<hr/>

<strong>GET /rest/aluno/perfil</strong><br>
<i>Recupera o perfil do aluno<i>

<div class="highlight">
<pre>
{
    "MessageResult": "Ok",
    "StatusCodeResult": 200,
    "RequestDate": "8/22/2013 1:30:29 PM",
    "DataResult": {
        "Nome": "NOME ALUNO",
        "Curso": ""
    }
}
</pre>
</div>

<hr/>

<strong>GET /rest/aluno/faltas</strong><br>
<i>Recupera a frequência do aluno em todas as matérias cursadas atualmente<i>

<div class="highlight">
<pre>
{
    "MessageResult": "Ok",
    "StatusCodeResult": 200,
    "RequestDate": "8/22/2013 1:48:56 PM",
    "DataResult": [
        {
            "Nome": "NOME DA DISCIPLINA",
            "Atualizacao": "20/08/2013",
            "CargaHoraria": "80h",
            "Frequencia": "95%",
            "Faltas": "4"
        },
        {
            "Nome": "NOME DA DISCIPLINA",
            "Atualizacao": "20/08/2013",
            "CargaHoraria": "80h",
            "Frequencia": "95%",
            "Faltas": "4"
        },
        
        ...
    ]
}
</pre>
</div>

<hr/>

<strong>GET /rest/aluno/aulas</strong><br>
<i>Recupera as aulas semanais do curso do aluno<i>

<div class="highlight">
<pre>
{
    "MessageResult": "Ok",
    "StatusCodeResult": 200,
    "RequestDate": "8/22/2013 2:05:03 PM",
    "DataResult": [
        {
            "Nome": "NOME DA DISCIPLINA",
            "DiaSemana": "Segunda",
            "HoraInicial": "19:00",
            "HoraFinal": "20:30",
            "Sala": "LAB. INFORM.5"
        },
        {
            "Nome": "NOME DA DISCIPLINA",
            "DiaSemana": "Terça",
            "HoraInicial": "20:50",
            "HoraFinal": "22:30",
            "Sala": "SALA 315"
        },
        
        ...
        
    ]
}
</pre>
</div>

<hr/>

<strong>GET /rest/aluno/notas</strong><br>
<i>Recupera as notas das disciplinas cursadas atualmente<i>

<div class="highlight">
<pre>
{
    "MessageResult": "Ok",
    "StatusCodeResult": 200,
    "RequestDate": "8/22/2013 2:07:40 PM",
    "DataResult": [
        {
            "Frequencia": {
                "Nome": null,
                "Atualizacao": "20/08/2013",
                "CargaHoraria": "80",
                "Frequencia": "90%",
                "Faltas": "8"
            },
            "Notas": {
                "Nome": null,
                "Nota1": "N.C.",
                "Nota2": "8,00",
                "Nota3": "--",
                "Nota4": "--",
                "Sub": "--",
                "Exame": "--",
                "Media": "--"
            },
            "Ano": "2013",
            "Nome": "NOME DA DISCIPLINA",
            "Motivo": "--",
            "Situacao": "Cursando",
            "Periodo": "Anual",
            "Turma": "4A"
        },
        
        ...
    ]
}
</pre>
</div>

<hr/>

<strong>GET /rest/aluno/historico</strong><br>
<i>Recupera todas as disciplinas cursadas pelo aluno<i>

<div class="highlight">
<pre>
{
    "MessageResult": "Ok",
    "StatusCodeResult": 200,
    "RequestDate": "8/22/2013 2:07:40 PM",
    "DataResult": [
        {
            "Frequencia": {
                "Nome": null,
                "Atualizacao": "20/08/2013",
                "CargaHoraria": "80",
                "Frequencia": "90%",
                "Faltas": "8"
            },
            "Notas": {
                "Nome": null,
                "Nota1": "N.C.",
                "Nota2": "8,00",
                "Nota3": "--",
                "Nota4": "--",
                "Sub": "--",
                "Exame": "--",
                "Media": "--"
            },
            "Ano": "2013",
            "Nome": "NOME DA DISCIPLINA",
            "Motivo": "--",
            "Situacao": "Cursando",
            "Periodo": "Anual",
            "Turma": "4A"
        },
        
        ...
    ]
}
</pre>
</div>

<hr/>

<strong>GET /rest/aluno/all</strong><br>
<i>Recupera todas as informações do aluno<i>

<div class="highlight">
<pre>
{
    "MessageResult": "Ok",
    "StatusCodeResult": 200,
    "RequestDate": "8/22/2013 2:12:11 PM",
    "DataResult": {
        "Perfil": {
            "Nome": "NOME DO ALUNO",
            "Curso": ""
        },
        "Disciplinas": [
            {
                "Frequencia": {
                    "Nome": null,
                    "Atualizacao": null,
                    "CargaHoraria": "80",
                    "Frequencia": "98",
                    "Faltas": "2"
                },
                "Notas": {
                    "Nome": null,
                    "Nota1": "8,00",
                    "Nota2": "6,50",
                    "Nota3": "N.C.",
                    "Nota4": "6,75",
                    "Sub": "5,00",
                    "Exame": "--",
                    "Media": "6,56"
                },
                "Ano": "2010",
                "Nome": "NOME DA DISCIPLINA",
                "Motivo": "--",
                "Situacao": "Aprovado",
                "Periodo": "Anual",
                "Turma": "1A"
            },
            
            ...
            
        ],
        "Aulas": [
            {
                "Nome": "NOME DA DISCIPLINA",
                "DiaSemana": "Segunda",
                "HoraInicial": "19:00",
                "HoraFinal": "20:30",
                "Sala": "LAB. INFORM.5"
            },
            
            ...
            
        ]
    }
}
</pre>
</div>
