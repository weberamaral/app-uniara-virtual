using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Windows.Navigation;
using UniaraVirtual.Code;
using UniaraVirtual.Code.Model;

namespace UniaraVirtual.View
{
    public partial class DetailsPage : PhoneApplicationPage
    {
        private IsolatedStorageSettings storage = IsolatedStorageSettings.ApplicationSettings;

        public DetailsPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            string disciplina = NavigationContext.QueryString["disciplina"];

            List<Disciplina> disciplinas = (List<Disciplina>)storage["Disciplinas"];

            Disciplina selected = disciplinas.Where(x => x.Nome.ToLower().Equals(disciplina.ToLower())).SingleOrDefault();

            DisciplinaName.Text = selected.Nome.ToLower();
            DisciplinaStatus.Text = selected.Situacao.ToLower();
            DisciplinaFrequencia.Text = selected.Frequencia.Frequencia;
            DisciplinaFaltas.Text = selected.Frequencia.Faltas;
            DisciplinaAno.Text = selected.Ano;
            DisciplinaHoras.Text = selected.Frequencia.CargaHoraria;
            DisciplinaPeriodo.Text = selected.Periodo;

            List<string> notas = new List<string>();
            if (selected.Notas.Nota1 != "--") notas.Add(selected.Notas.Nota1);
            if (selected.Notas.Nota2 != "--") notas.Add(selected.Notas.Nota2);
            if (selected.Notas.Nota3 != "--") notas.Add(selected.Notas.Nota3);
            if (selected.Notas.Nota4 != "--") notas.Add(selected.Notas.Nota4);
            if (selected.Notas.Sub != "--") notas.Add(selected.Notas.Sub);
            if (selected.Notas.Exame != "--") notas.Add(selected.Notas.Exame);

            DisciplinasNotasBimestrais.ItemsSource = notas;

            if (selected.Situacao.ToLower().Equals("cursando"))
            {
                DisciplinaMediaStatus.Text = "MÉDIA PARCIAL";
                DisciplinaMedia.Text = CalcMedia(notas).ToString("F2");
            }
            else
            {
                DisciplinaMediaStatus.Text = "MÉDIA FINAL";
                DisciplinaMedia.Text = selected.Notas.Media;
            }
        }

        private decimal CalcMedia(List<string> notas)
        {
            // total de notas disponiveis
            int quantidadeNotas = notas.Count;
            decimal mediaParcial = 0;

            if (quantidadeNotas == 0)
            {
            }
            else
            {
                decimal somaNotas = 0;
                foreach(string nota in notas)
                {
                    try
                    {
                        decimal notaConvertida = Convert.ToDecimal(nota);
                        somaNotas += notaConvertida;
                    }
                    catch (FormatException)
                    {
                        somaNotas += 0;
                    }
                }

                mediaParcial = somaNotas / quantidadeNotas;
            }

            return mediaParcial;
        }
    }
}