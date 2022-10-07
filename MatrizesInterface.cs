using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculadora_de_Matrizes
{
    /// <summary>
    /// Classe criada para auxiliar com quesitos de interface da calculadora, cuidando da parte "estética", textBoxs, avisos entre outros
    /// </summary>
    public static class MatrizesInterface
    {
        public static TextBox[,] Matriz, Matriz2, matrizResultado;
        public static float determinante;

        public static int linha1, coluna1;
        public static int linha2, coluna2;


        /// <summary>
        /// Método criado para instanciar os textboxs dentro do panel para gerar as matrizes
        /// </summary>
        /// <param name="linhas">Números de linhas, oriundo do numericUpDown</param>
        /// <param name="colunas">Número de colunas, oriundo do numericUpDown</param>
        /// <param name="matrixFinal">Panel no qual serão instanciados os textBoxs</param>
        public static void instanciarTextBox(int linhas, int colunas, Panel matrixFinal)
        {
            int altura = 35;
            int largura = 50;

            TextBox[,] Matriz = new TextBox[linhas, colunas];

            for (int x = 0; x < linhas; x++)
            {
                for (int y = 0; y < colunas; y++)
                {
                    Matriz[x, y] = new TextBox();
                    Matriz[x, y].Font = new Font("Microsoft Sans Serif", 10f);
                    Matriz[x, y].Text = "0";
                    Matriz[x, y].BackColor = Color.Black;
                    Matriz[x, y].ForeColor = Color.White;
                    Matriz[x, y].Location = new Point((40) * y, (30) * x);
                    Matriz[x, y].KeyPress += new KeyPressEventHandler(naoMostrarLetras);
                    Matriz[x, y].Size = new Size(altura, largura);
                    matrixFinal.Controls.Add(Matriz[x, y]);
                }
            }
        }

        /// <summary>
        /// Método para instanciar os textBox na Matriz Resultante, usado com os métodos para cálculos
        /// </summary>
        /// <param name="panel">Recebe o painel resultante</param>
        /// <param name="matriz">Recebe a matriz para passar para o painel resultante</param>
        public static void instanciarTextBoxMatrizResultante(Panel panel, float[,] matriz)
        {
            panel.Controls.Clear();
            int altura = 35;
            int largura = 50;
            matrizResultado = new TextBox[matriz.GetLength(0), matriz.GetLength(1)];


            for (int x = 0; x < matriz.GetLength(0); x++)
            {
                for (int y = 0; y < matriz.GetLength(1); y++)
                {
                    matrizResultado[x, y] = new TextBox();
                    matrizResultado[x, y].Font = new Font("Microsoft Sans Serif", 10f);
                    matrizResultado[x, y].BackColor = Color.Black;
                    matrizResultado[x, y].ForeColor = Color.White;
                    matrizResultado[x, y].Location = new Point((40) * y, (30) * x);
                    matrizResultado[x, y].KeyPress += new KeyPressEventHandler(naoMostrarLetras);
                    matrizResultado[x, y].Size = new Size(altura, largura);
                    matrizResultado[x,y].Text = ((float)matriz[x, y]).ToString();
                    panel.Controls.Add(matrizResultado[x, y]);
                }
            }
        }

        /// <summary>
        /// Método para exibir apenas números nos textBoxs das Matrizes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void naoMostrarLetras(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                 (e.KeyChar != ',' && e.KeyChar != '-'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == ',') && ((sender as TextBox).Text.IndexOf(',') > -1))
            {
                e.Handled = true;
            }
        }


        /// <summary>
        /// Método para renomear os grupBoxs, passando títulos de acordo com o botão apertado
        /// </summary>
        /// <param name="campo">Recebe o groupBox que terá a propriedade Text alterada</param>
        /// <param name="linhas">Recebe o número de linhas para ser exibida</param>
        /// <param name="colunas">Recebe o número de colunas para ser exibida</param>
        /// <param name="letra">Recebe a letra do groupBox</param>
        public static void nomeDosGroupBox(GroupBox campo,String letra, int linhas, int colunas)
        {
            campo.Text = "Matrix " + letra + ": " + linhas.ToString() + " linhas " + colunas.ToString() + " colunas"; 
        }


        /// <summary>
        /// Método que percorre todo o painel indicado pelo parametro e adiciona os valores dos textbox no array
        /// </summary>
        /// <param name="panel">Recebe o painel o qual ira buscar os valores</param>
        /// <param name="colunas">Recebe o número de colunas deste painel</param>
        /// <param name="linhas">Recebe o número de linhas deste painel</param>
        /// <returns>Retorna o array com todos os valores do painel indicado</returns>
        public static float[,] resgatarNumeros(Panel panel, int linhas, int colunas)
        {
            float[,] matriz = new float[linhas, colunas];
            int x = 0;
            int y = 0;

            foreach (TextBox Matriz in panel.Controls)
            {
                matriz[x, y] = float.Parse(Matriz.Text);

                if (y == colunas - 1)
                {
                    y = 0;
                    x++;
                }
                else y++;
            }
            return matriz;
        }

        /// <summary>
        /// Método para limpar as matrizes, limpa titulo do groupBox, valores do numeriUpDown
        /// </summary>
        /// <param name="panel">Recebe o painel para ser limpo</param>
        /// <param name="linhas">Recebe o numericUpDown de linhas</param>
        /// <param name="colunas">Recebe o numericUpDown de colunas</param>
        /// <param name="box">Recebe o groupBox para alterar o titulo</param>
        /// <param name="titulo">Recebe o novo titulo</param>
        public static void limparMatrizes(Panel panel, NumericUpDown linhas, NumericUpDown colunas, GroupBox box, String titulo)
        {
            panel.Controls.Clear();
            //linhas.Value = 0;
           // colunas.Value = 0;
            box.Text = titulo;
        }
    }
}
