using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Calculadora_de_Matrizes
{
    /// <summary>
    /// Classe associada ao design do form
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// Váriaveis iniciadas como false, para não mostrar os textboxs de fórmulas e cálculos
        /// </summary>
        public Form1()
        {
            InitializeComponent();

            textBoxNumeroMatriz1.KeyPress += new KeyPressEventHandler(MatrizesInterface.naoMostrarLetras);
            textBoxNumeroMatriz1.Visible = false;

            textBoxNumeroMatriz2.KeyPress += new KeyPressEventHandler(MatrizesInterface.naoMostrarLetras);
            textBoxNumeroMatriz2.Visible = false;

            textBoxNumeroMatriz3.KeyPress += new KeyPressEventHandler(MatrizesInterface.naoMostrarLetras);
            textBoxNumeroMatriz3.Visible = false;

            formulaMatriz1.Visible = false;
            formulaMatriz2.Visible = false;

            textBoxRotacionar.KeyPress += new KeyPressEventHandler(MatrizesInterface.naoMostrarLetras);
            textBoxEscalarObjeto.KeyPress += new KeyPressEventHandler(MatrizesInterface.naoMostrarLetras);
            textBoxEixoX.KeyPress += new KeyPressEventHandler(MatrizesInterface.naoMostrarLetras);
            textBoxEixoY.KeyPress += new KeyPressEventHandler(MatrizesInterface.naoMostrarLetras);

            textBoxMenuGrafico.Visible = false;
            textBoxMenuGrafico.KeyPress += new KeyPressEventHandler(MatrizesInterface.naoMostrarLetras);
            txbFormulaMatrizGrafico.Visible = false;

        }

        private static int linha1, coluna1;
        private static int linha2, coluna2;
        private int linhaResultante, colunaResultante;
        private int colunasPlano;

        public static float[,] matriz1, matriz2, matrizResultante;

        /// <summary>
        /// Método para gerar determinante da matriz 1
        /// </summary>
        private void gerarDeterminante()
        {
            try
            {
                float[,] matriz1 = MatrizesInterface.resgatarNumeros(panel1, linha1, coluna1);

                if (matriz1.Length == 0)
                {
                    semMatriz();
                }
                else if (linha1 == coluna1)
                {
                    float determinanteResultado = Calculos.gerarDeterminante(matriz1);
                    MessageBox.Show("Determinante: " + determinanteResultado, "Resultado do determinante da Matriz1");
                }
                else
                {
                    quadrada();
                }
            }
            catch (Exception)
            {
                error();
            }
        }


        /// <summary>
        /// Método para achar a identidade de uma matriz e desenhar no painel
        /// </summary>
        /// <param name="linhas">Recebe número de linhas</param>
        /// <param name="colunas">Recebe o número de colunas</param>
        private void gerarIdentidade(int linhas, int colunas)
        {
            try
            {
                float[,] matrizIdentidade = Calculos.gerarIdentidade(linhas, colunas);
                if (matrizIdentidade.Length == 0)
                {
                    MessageBox.Show("Não há matriz para calcular", "Aviso Importante", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    colunaResultante = colunas;
                    linhaResultante = linhas;
                    MatrizesInterface.instanciarTextBoxMatrizResultante(panel3, matrizIdentidade);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ocorreu um erro", "Aviso Importante", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Método chamado ao apertar o botão gerar, e a opção transposta estiver sido escolhida
        /// </summary>
        private void mostrarTranspostaMatriz1()
        {
            try
            {
                float[,] matriz1 = MatrizesInterface.resgatarNumeros(panel1, linha1, coluna1);

                if (matriz1.Length == 0)
                {
                    MessageBox.Show("Não há matriz para calcular", "Aviso Importante", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    linhaResultante = coluna1;
                    colunaResultante = linha1;
                    panel3.Controls.Clear();
                    float[,] resultado = Calculos.GerarTransposta(matriz1);
                    MatrizesInterface.instanciarTextBoxMatrizResultante(panel3, resultado);
                    groupBoxResultado.Text = ("Resultado da transposta da Matriz 1");

                    if (Calculos.comparandoMatrizes(matriz1, resultado))
                    {
                        MessageBox.Show("Esta é uma matriz simétrica", "Aviso Importante", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ocorreu um erro", "Aviso Importante", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Método chamado ao apertar o botão gerar, e a opção oposta estiver sido escolhida
        /// </summary>
        private void mostrarOpostaMatriz1()
        {
            try
            {
                float[,] matriz1 = MatrizesInterface.resgatarNumeros(panel1, linha1, coluna1);

                if (matriz1.Length == 0)
                {
                    MessageBox.Show("Não há matriz para calcular", "Aviso Importante", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    linhaResultante = linha1;
                    colunaResultante = coluna1;
                    panel3.Controls.Clear();
                    float[,] matrizOposta = Calculos.GerarOposta(matriz1);
                    MatrizesInterface.instanciarTextBoxMatrizResultante(panel3, matrizOposta);
                    groupBoxResultado.Text = "Resultado da oposta da matriz 1";
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ocorreu um erro", "Aviso Importante", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Método chamado ao apertar o botão gerar, e a opção multiplicar estiver sido escolhida
        /// </summary>
        private void multiplicarMatriz1()
        {
            try
            {
                float[,] matriz1 = MatrizesInterface.resgatarNumeros(panel1, linha1, coluna1);

                if (matriz1.Length == 0)
                {
                    semMatriz();
                }
                else if (textBoxNumeroMatriz1.Text == "")
                {
                    digiteNumero();
                }
                else
                {
                    colunaResultante = coluna1;
                    linhaResultante = linha1;
                    float valor = float.Parse(textBoxNumeroMatriz1.Text);
                    panel3.Controls.Clear();
                    matrizResultante = Calculos.multiplicarPorNumeroQualquer(matriz1, valor);
                    MatrizesInterface.instanciarTextBoxMatrizResultante(panel3, matrizResultante);
                    groupBoxResultado.Text = "Resultado da multiplicação por um número qualquer";
                }
            }
            catch (Exception)
            {
                error();
            }
        }

        /// <summary>
        /// Método para multiplicar a matriz 3
        /// </summary>
        private void multiplicarMatriz3()
        {
            try
            {
                float[,] matriz = MatrizesInterface.resgatarNumeros(panel3, linhaResultante, colunaResultante);

                if (matriz.Length == 0)
                {
                    semMatriz();
                }
                else if (textBoxNumeroMatriz3.Text == "")
                {
                    digiteNumero();
                }
                else
                {
                    float valor = float.Parse(textBoxNumeroMatriz3.Text);
                    panel3.Controls.Clear();
                    matrizResultante = Calculos.multiplicarPorNumeroQualquer(matriz, valor);
                    MatrizesInterface.instanciarTextBoxMatrizResultante(panel3, matrizResultante);
                    groupBoxResultado.Text = "Resultado da multiplicação por um número qualquer";
                }
            }
            catch (Exception)
            {
                error();
            }
        }

        /// <summary>
        /// Método para elevar a matriz a qualquer numero
        /// </summary>
        private void elevarMatriz1()
        {
            try
            {
                float[,] matriz1 = MatrizesInterface.resgatarNumeros(panel1, linha1, coluna1);
                if (matriz1.Length == 0)
                {
                    semMatriz();
                }
                else if (textBoxNumeroMatriz1.Text == "")
                {
                    digiteNumero();
                }
                else if (coluna1 == linha1)
                {
                    colunaResultante = coluna1;
                    linhaResultante = linha1;
                    float expoente = float.Parse(textBoxNumeroMatriz1.Text);
                    panel3.Controls.Clear();
                    float[,] resultado = Calculos.elevarMatrizNumeroQualquer(matriz1, expoente);
                    MatrizesInterface.instanciarTextBoxMatrizResultante(panel3, resultado);
                    groupBoxResultado.Text = "Resultante da Matriz 1 elevada";
                }
                else
                {
                    quadrada();
                }
            }

            catch (Exception)
            {
                error();
            }

        }

        /// <summary>
        /// Método para elevar a matriz 3
        /// </summary>
        private void elevarMatriz3()
        {
            try
            {
                float[,] matriz = MatrizesInterface.resgatarNumeros(panel3, linhaResultante, colunaResultante);
                if (matriz.Length == 0)
                {
                    semMatriz();
                }
                else if (textBoxNumeroMatriz3.Text == "")
                {
                    digiteNumero();
                }
                else if (linhaResultante == colunaResultante)
                {
                    float expoente = float.Parse(textBoxNumeroMatriz3.Text);
                    panel3.Controls.Clear();
                    float[,] resultado = Calculos.elevarMatrizNumeroQualquer(matriz, expoente);
                    MatrizesInterface.instanciarTextBoxMatrizResultante(panel3, resultado);
                    groupBoxResultado.Text = "Resultante da Matriz 1 elevada";
                }
                else
                {
                    quadrada();
                }
            }

            catch (Exception)
            {
                error();
            }
        }

        /// <summary>
        /// Método para escolher opção no menu drop da Matriz A
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void matriz1Menu_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            switch (matriz1Menu.Text)
            {
                case "ESCALAR":
                case "ELEVAR":
                    textBoxNumeroMatriz1.Visible = true;
                    formulaMatriz1.Visible = false;
                    break;

                case "FÓRMULA":
                    formulaMatriz1.Visible = true;
                    textBoxNumeroMatriz1.Visible = false;
                    break;

                default:
                    formulaMatriz1.Visible = false;
                    textBoxNumeroMatriz1.Visible = false;
                    break;
            }
        }

        /// <summary>
        /// Método para clique do botão criar matriz 1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_criarMatriz1_Click_1(object sender, EventArgs e)
        {
            linha1 =  (int)numericUpDown1.Value;
            coluna1 = (int)numericUpDown2.Value;

            if (coluna1 == 0 || linha1 == 0)
            {
                MessageBox.Show("Digite os valores de linha e colunas para Matriz A", "Aviso Importante");
            }
            else
            {
                panel1.Controls.Clear();
                panel3.Controls.Clear();
                MatrizesInterface.instanciarTextBox(linha1, coluna1, panel1);
                MatrizesInterface.nomeDosGroupBox(groupBox1, "A", linha1, coluna1);
            }
        }

        /// <summary>
        /// Método para clique do botão criar matriz 2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_criarMatriz2_Click_1(object sender, EventArgs e)
        {
            linha2 = (int)numericUpDown3.Value;
            coluna2 = (int)numericUpDown4.Value;
            if (linha2 == 0 || coluna2 == 0)
            {
                MessageBox.Show("Digite os valores de linhas e colunas para Matriz B", "Aviso Importante");
            }
            else
            {
                panel2.Controls.Clear();
                panel3.Controls.Clear();
                MatrizesInterface.instanciarTextBox(linha2, coluna2, panel2);
                MatrizesInterface.nomeDosGroupBox(groupBox2, "B", linha2, coluna2);
            }
        }

        /// <summary>
        /// Método para clique do botão SOMAR MATRIZES
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSomar_Click_1(object sender, EventArgs e)
        {
            float[,] matriz1 = MatrizesInterface.resgatarNumeros(panel1, linha1, coluna1);
            float[,] matriz2 = MatrizesInterface.resgatarNumeros(panel2, linha1, coluna1);

            linhaResultante = linha1;
            colunaResultante = coluna1;

            if (matriz1.Length == 0 || matriz2.Length == 0)
            {
                MessageBox.Show("Não há matrizes para somar", "Aviso Importante", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (linha1 == linha2 && coluna1 == coluna2)
            {
                panel3.Controls.Clear();
                matrizResultante = Calculos.SomandoMatrizes(matriz1, matriz2);
                MatrizesInterface.instanciarTextBoxMatrizResultante(panel3, matrizResultante);
                groupBoxResultado.Text = "Soma das Matrizes A e B";
            }
            else
            {
                MessageBox.Show("As matrizes não tem ordens iguais", "Aviso Importante", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Método para clique do botão SUBTRAIR MATRIZES
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubtrair_Click_1(object sender, EventArgs e)
        {
            float[,] matriz1 = MatrizesInterface.resgatarNumeros(panel1, linha1, coluna1);
            float[,] matriz2 = MatrizesInterface.resgatarNumeros(panel2, linha2, coluna2);

            linhaResultante = linha1;
            colunaResultante = coluna1;

            if (matriz1.Length == 0 || matriz2.Length == 0)
            {
                MessageBox.Show("Não há matrizes para subtrair", "Aviso Importante", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else if (linha1 == linha2 && coluna1 == coluna2)
            {
                float[,] matrizResultante = Calculos.SubtraindoMatrizes(matriz1, matriz2);
                panel3.Controls.Clear();
                MatrizesInterface.instanciarTextBoxMatrizResultante(panel3, matrizResultante);
                groupBoxResultado.Text = "Resultante da subtração";
            }

            else
            {
                DialogResult result = MessageBox.Show("As matrizes não tem ordens iguais", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Método para clique do botão MULTIPLICAR MATRIZES
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMultiplicar_Click_1(object sender, EventArgs e)
        {
            float[,] matriz1 = MatrizesInterface.resgatarNumeros(panel1, linha1, coluna1);
            float[,] matriz2 = MatrizesInterface.resgatarNumeros(panel2, linha2, coluna2);

            linhaResultante = linha1;
            colunaResultante = coluna2;

            if (matriz1.Length == 0 || matriz2.Length == 0)
            {
                MessageBox.Show("Não há matrizes para multiplicar", "Aviso Importante", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (coluna1 == linha2)
            {
                matrizResultante = Calculos.MultiplicandoMatrizes(matriz1, matriz2);
                panel3.Controls.Clear();
                MatrizesInterface.instanciarTextBoxMatrizResultante(panel3, matrizResultante);
                groupBoxResultado.Text = "Resultante da multiplicação";
            }
            else if (coluna1 != linha2)
            {
                DialogResult result = MessageBox.Show("O número de colunas da Matriz A, não é igual ao número de linhas da Matriz B", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        /// <summary>
        /// Método para o botão limpar da matriz 2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLimparMatriz2_Click_1(object sender, EventArgs e)
        {
            MatrizesInterface.limparMatrizes(panel2, numericUpDown3, numericUpDown4, groupBox2, "Matriz B");
        }

        /// <summary>
        /// Método para o botão limpar da matriz 1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLimparMatriz1_Click_1(object sender, EventArgs e)
        {
            MatrizesInterface.limparMatrizes(panel1, numericUpDown1, numericUpDown2, groupBox1, "Matriz A");
        }

        /// <summary>
        /// Método para o botão limpar da matriz resultante
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLimparMatriz3_Click_1(object sender, EventArgs e)
        {
            panel3.Controls.Clear();
            groupBoxResultado.Text = "Matriz resultante";
        }
        /// <summary>
        /// Método de clique para o botão gerar da matriz 1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAction1_Click_1(object sender, EventArgs e)
        {
            switch (matriz1Menu.Text)
            {
                case "":
                    MessageBox.Show("Escolha uma das opções no menu", "Aviso Importante", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;

                case "OPOSTA":
                    mostrarOpostaMatriz1();
                    break;

                case "TRANSPOSTA":
                    mostrarTranspostaMatriz1();
                    break;

                case "ESCALAR":
                    multiplicarMatriz1();
                    break;

                case "ELEVAR":
                    elevarMatriz1();
                    break;

                case "INVERSA":
                    inversaMatriz1();
                    break;

                case "DETERMINANTE":
                    gerarDeterminante();
                    break;

                case "IDENTIDADE":
                    gerarIdentidade(linha1, coluna1);
                    break;

                case "FÓRMULA":
                    preecherMatriz1PorFormula();
                    break;
            }
        }

        /// <summary>
        /// Método para preecher matriz 1 com formula dada por usuario
        /// Método auxilia o evento de clique do botão gerar
        /// </summary>
        private void preecherMatriz1PorFormula()
        {
            try
            {
                float[,] matrizResultante = Calculos.matrizPorFormula((int)numericUpDown1.Value, (int)numericUpDown2.Value, formulaMatriz1.Text);
                panel1.Controls.Clear();
                MatrizesInterface.instanciarTextBoxMatrizResultante(panel1, matrizResultante);
            }
            catch (Exception)
            {
                MessageBox.Show("Fórmula para lei de formação inválida", "Aviso Importante", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        /// <summary>
        /// Método de clique do botão gerar para matriz 2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gerar_matriz2_Click_1(object sender, EventArgs e)
        {
            switch (menu_matriz2.Text)
            {
                case "":
                    MessageBox.Show("Escolha uma das opções no menu", "Aviso Importante", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;

                case "OPOSTA":
                    mostrarOpostaMatriz2();
                    break;

                case "TRANSPOSTA":
                    mostrarTranspostaMatriz2();
                    break;

                case "ESCALAR":
                    multiplicarMatriz2();
                    break;

                case "ELEVAR":
                    elevarMatriz2();
                    break;

                case "IDENTIDADE":
                    gerarIdentidade(linha2, coluna2);
                    break;

                case "FÓRMULA":
                    preencherMatriz2PorFormula();
                    break;

                case "INVERSA":
                    inversaMatriz2();
                    break;

                case "DETERMINANTE":
                    gerarDeterminante2();
                    break;
            }
        }

        /// <summary>
        /// Gerar determinante da matriz 2
        /// </summary>
        private void gerarDeterminante2()
        {
            try
            {
                float[,] matriz1 = MatrizesInterface.resgatarNumeros(panel2, linha2, coluna2);

                if (matriz1.Length == 0)
                {
                    semMatriz();
                }
                else if (linha2 == coluna2)
                {
                    float determinanteResultado = Calculos.gerarDeterminante(matriz1);
                    MessageBox.Show("Determinante: " + determinanteResultado, "Resultado do determinante da Matriz 2");
                }
                else
                {
                    quadrada();
                }
            }
            catch (Exception)
            {
                error();
            }
        }

        /// <summary>
        /// Método para gerar inversa da matriz 2
        /// </summary>
        private void inversaMatriz2()
        {
            try
            {
                float[,] matrizInicial = MatrizesInterface.resgatarNumeros(panel2, linha2, coluna2);
                if (matrizInicial.Length == 0)
                {
                    semMatriz();
                }
                else
                {
                    float determinante = Calculos.gerarDeterminante(matrizInicial);
                    if(determinante == 0)
                    {
                        MessageBox.Show("Não há matriz para calcular",
                            "Aviso Importante", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        colunaResultante = coluna2;
                        linhaResultante = linha2;
                        float[,] matrizResultante = Calculos.gerarInversa(matrizInicial);
                        panel3.Controls.Clear();
                        MatrizesInterface.instanciarTextBoxMatrizResultante(panel3, matrizResultante);
                        groupBoxResultado.Text = "Inversa da Matriz B";
                    }
                }
            }
            catch
            {
                error();
            }

        }

        /// <summary>
        /// Método para gerar inversa da matriz 1
        /// </summary>
        private void inversaMatriz1()
        {
            try
            {
                float[,] matrizInicial = MatrizesInterface.resgatarNumeros(panel1, linha1, coluna1);

                if (matrizInicial.Length == 0)
                {
                    MessageBox.Show("Não há matriz para calcular", "Aviso Importante", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    float determinante = Calculos.gerarDeterminante(matrizInicial);
                    if (determinante == 0)
                    {
                        MessageBox.Show("O determinante desta matriz é 0 (zero), portanto não existe matriz inversa", 
                            "Explicação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        linhaResultante = linha1;
                        colunaResultante = coluna1;
                        float[,] matrizResultante = Calculos.gerarInversa(matrizInicial);
                        panel3.Controls.Clear();
                        MatrizesInterface.instanciarTextBoxMatrizResultante(panel3, matrizResultante);
                        groupBoxResultado.Text = "Inversa da Matriz A";
                    }
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Ocorreu um erro", "Aviso Importante", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Método para preecnher matriz 2 por fórmula
        /// Método que auxilia o clique do botão gerar 
        /// </summary>
        private void preencherMatriz2PorFormula()
        {
            try
            {
                float[,] matrizResultante = Calculos.matrizPorFormula((int)numericUpDown3.Value, (int)numericUpDown4.Value, formulaMatriz2.Text);
                panel2.Controls.Clear();
                MatrizesInterface.instanciarTextBoxMatrizResultante(panel2, matrizResultante);
            }
            catch (Exception)
            {
                MessageBox.Show("Fórmula para lei de formação inválida", "Aviso Importante", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Método para escolher opção do menu drop da Matriz B
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menu_matriz2_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            switch (menu_matriz2.Text)
            {
                case "ESCALAR":
                case "ELEVAR":
                    textBoxNumeroMatriz2.Visible = true;
                    formulaMatriz2.Visible = false;
                    break;

                case "FÓRMULA":
                    formulaMatriz2.Visible = true;
                    textBoxNumeroMatriz2.Visible = false;
                    break;

                default:
                    formulaMatriz2.Visible = false;
                    textBoxNumeroMatriz2.Visible = false;
                    break;
            }
        }


        /// <summary>
        /// Método para escolher opção da Matriz Resultante
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menu_matriz3_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            switch (menu_matriz3.Text)
            {
                case "ESCALAR":
                case "ELEVAR":
                    textBoxNumeroMatriz3.Visible = true;
                    break;

                default:
                    textBoxNumeroMatriz3.Visible = false;
                    break;
            }
        }

        /// <summary>
        /// Método para escolher opção do menu drop, para explicação na aba de sobre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuResumos_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (menuResumos.Text)
            {
                case "MATRIZES":
                    colocarExplicacao(0);
                    break;

                case "DETERMINANTE":
                    colocarExplicacao(2);
                    break;

                case "SOMA":
                    colocarExplicacao(1);
                    break;

                case "MATRIZ LINHA":
                    colocarExplicacao(3);
                    break;

                case "MATRIZ COLUNA":
                    colocarExplicacao(4);
                    break;

                case "MATRIZ QUADRADA":
                    colocarExplicacao(5);
                    break;

                case "MATRIZ NULA":
                    colocarExplicacao(6);
                    break;

                case "MATRIZ IDENTIDADE":
                    colocarExplicacao(7);
                    break;

                case "MATRIZ OPOSTA":
                    colocarExplicacao(8);
                    break;

                case "MATRIZ TRANSPOSTA":
                    colocarExplicacao(9);
                    break;

                case "ELEMENTOS":
                    colocarExplicacao(10);
                    break;

                case "SUBTRAÇÃO":
                    colocarExplicacao(11);
                    break;

                case "MULTIPLICAÇÃO POR ESCALAR":
                    colocarExplicacao(12);
                    break;

                case "MULTIPLICAÇÃO":
                    colocarExplicacao(13);
                    break;

                case "MATRIZ INVERSA":
                    colocarExplicacao(16);
                    break;

                case "SIMÉTRICA":
                    colocarExplicacao(15);
                    break;
            }
        }

        /// <summary>
        /// Método utilizado para pegar explicação de elemento
        /// </summary>
        /// <param name="posicao">Recebe um valor inteiro, que corresponde a posição da explicao no array</param>
        public void colocarExplicacao(int posicao)
        {
            labelResumos.Text = explicacao.explicacoes[posicao];
        }

        /// <summary>
        /// Método para mostrar Oposta da Matriz 2 - associado ao clique 
        /// </summary>
        void mostrarOpostaMatriz2()
        {
            try
            {
                float[,] matriz2 = MatrizesInterface.resgatarNumeros(panel2, linha2, coluna2);

                if (matriz2.Length == 0)
                {
                    semMatriz();
                }
                else
                {
                    linhaResultante = linha2;
                    colunaResultante = coluna2;
                    panel3.Controls.Clear();
                    float[,] matrizOposta = Calculos.GerarOposta(matriz2);
                    MatrizesInterface.instanciarTextBoxMatrizResultante(panel3, matrizOposta);
                    groupBoxResultado.Text = "Resultante da oposta matriz 2";
                }
            }
            catch (Exception)
            {
                error();
            }
        }

        /// <summary>
        /// Método para mostrar transposta da Matriz 2 - associado ao clique
        /// </summary>
        void mostrarTranspostaMatriz2()
        {
            try
            {
                float[,] matriz2 = MatrizesInterface.resgatarNumeros(panel2, linha2, coluna2);

                if (matriz2.Length == 0)
                {
                    semMatriz();
                }
                else
                {
                    linhaResultante = coluna2;
                    colunaResultante = linha2;
                    panel3.Controls.Clear();
                    float[,] resultado = Calculos.GerarTransposta(matriz2);
                    MatrizesInterface.instanciarTextBoxMatrizResultante(panel3, resultado);
                    groupBoxResultado.Text = ("Resultante da transposta matriz 2");
                    if (Calculos.comparandoMatrizes(matriz2, resultado))
                    {
                        MessageBox.Show("Esta é uma matriz simétrica", "Aviso Importante", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception)
            {
                error();
            }
        }

        /// <summary>
        /// Método para multiplicar Matriz 2 - associado ao clique
        /// </summary>
        void multiplicarMatriz2()
        {
            try
            {
                matriz2 = MatrizesInterface.resgatarNumeros(panel2, linha2, coluna2);

                if (matriz2.Length == 0)
                {
                    semMatriz();
                }
                else if (textBoxNumeroMatriz2.Text == "")
                {
                    digiteNumero();
                }
                else
                {
                    float valor = float.Parse(textBoxNumeroMatriz2.Text);
                    colunaResultante = coluna2;
                    linhaResultante = linha2;
                    panel3.Controls.Clear();
                    matrizResultante = Calculos.multiplicarPorNumeroQualquer(matriz2, valor);
                    MatrizesInterface.instanciarTextBoxMatrizResultante(panel3, matrizResultante);
                    groupBoxResultado.Text = "Resultante da multiplicação por um numero matriz 2";
                }
            }

            catch (Exception)
            {
                error();
            }
        }

        /// <summary>
        /// Método para elevar Matriz 2 - associado ao clique
        /// </summary>
        private void elevarMatriz2()
        {
            try
            {
                float[,] matriz2 = MatrizesInterface.resgatarNumeros(panel2, linha2, coluna2);
                if (matriz2.Length == 0)
                {
                    semMatriz();
                }
                else if (textBoxNumeroMatriz2.Text == "")
                {
                    digiteNumero();
                }
                else if (coluna2 == linha2)
                {
                    colunaResultante = coluna2;
                    linhaResultante = linha2;
                    float expoente = float.Parse(textBoxNumeroMatriz2.Text);
                    panel3.Controls.Clear();
                    float[,] resultado = Calculos.elevarMatrizNumeroQualquer(matriz2, expoente);
                    MatrizesInterface.instanciarTextBoxMatrizResultante(panel3, resultado);
                    groupBoxResultado.Text = "Resultante da Matriz 2 elevada";
                }
                else
                {
                    quadrada();
                }
            }
            catch (Exception)
            {
                error();
            }
        }

        /// <summary>
        /// Método de evento do clique do botão criar matriz para o gráfico
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void criarMatrizGrafico_Click(object sender, EventArgs e)
        {
            try
            {
                colunasPlano = (int)numericUpDownColunasGrafico.Value;
                panel4GraficoMatriz.Controls.Clear();
                MatrizesInterface.instanciarTextBox((int)numericUpDownColunasGrafico.Value, 2, panel4GraficoMatriz);
            }
            catch (Exception)
            {
                error();
            }

        }

        /// <summary>
        /// Método para evento de clique do botão limpar do painel da matriz para o gráfico
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLimparMatriz_Click(object sender, EventArgs e)
        {
            panel4GraficoMatriz.Controls.Clear();
        }

        /// <summary>
        /// Método para clique do botão gerar para a matriz 3
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gerar_matriz3_Click(object sender, EventArgs e)
        {
            switch (menu_matriz3.Text)
            {
                case "":
                    MessageBox.Show("Escolha uma opção no menu", "Aviso Importante", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;

                case "DETERMINANTE":
                    determinanteResultante();
                    break;

                case "ESCALAR":
                    multiplicarMatriz3();
                    break;

                case "ELEVAR":
                    elevarMatriz3();
                    break;
            }
        }

        /// <summary>
        /// Método para calcular determinante da matriz 3
        /// </summary>
        private void determinanteResultante()
        {
            try
            {
                float[,] matriz1 = MatrizesInterface.resgatarNumeros(panel3, linhaResultante, colunaResultante);
                if (matriz1.Length == 0)
                {
                    semMatriz();
                }
                else if (linhaResultante == colunaResultante)
                {
                    float determinanteResultado = Calculos.gerarDeterminante(matriz1);
                    MessageBox.Show("Determinante: " + determinanteResultado, "Resultado do determinante da Matriz 3");
                }
                else
                {
                    quadrada();
                }
            }
            catch (Exception)
            {
                error();
            }
        }

        /// <summary>
        /// Método de clique do botão para inserir valores da matriz resultante nas matrizes A e B 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInserirMatrizResultante_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxInserir.Text == "")
                {
                    MessageBox.Show("Escolha um painel para inserir sua matriz", "Aviso Importante", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else if (comboBoxInserir.Text == "A")
                {
                    passarNumerosParaOutraMatriz(panel1);
                }
                else if (comboBoxInserir.Text == "B")
                {
                    passarNumerosParaOutraMatriz(panel2);
                }
            }
            catch (Exception)
            {
                error();
            }
        }

        /// <summary>
        /// Método para passar valores da Matriz Resultante para Matriz A ou Matriz B
        /// </summary>
        /// <param name="panel"></param>
        private void passarNumerosParaOutraMatriz(Panel panel)
        {
            float[,] matrizPara = MatrizesInterface.resgatarNumeros(panel3, linhaResultante, colunaResultante);
            if (matrizPara.Length == 0)
            {
                semMatriz();
            }
            else
            {
                panel.Controls.Clear();
                MatrizesInterface.instanciarTextBoxMatrizResultante(panel, matrizPara);
            }
        }

        /// <summary>
        /// Método para o botão de limpar o gráfico
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void limparGrafico_Click(object sender, EventArgs e)
        {
            chart1.Series["Objeto"].Points.Clear();
        }

        /// <summary>
        /// Método do evento KeyPress, para bloquear outras letras da caixa para inserir fórmula da Matriz A
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void formulaMatriz1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                 (e.KeyChar != ',' && e.KeyChar != '-' && e.KeyChar != '+' && e.KeyChar != '*' && e.KeyChar != '/' && e.KeyChar != 'i' && e.KeyChar != 'j' && e.KeyChar != '^' && e.KeyChar != ' '))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == ',') && ((sender as TextBox).Text.IndexOf(',') > -1))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Método para escolha de opção do menu drop down do gráfico
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxGrafico_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBoxGrafico.Text)
            {
                case "FÓRMULA":
                    textBoxMenuGrafico.Visible = false;
                    txbFormulaMatrizGrafico.Visible = true;
                    break;

                case "ELEVAR":
                case "ESCALAR":
                    textBoxMenuGrafico.Visible = true;
                    txbFormulaMatrizGrafico.Visible = false;
                    break;

                default:
                    textBoxMenuGrafico.Visible = false;
                    txbFormulaMatrizGrafico.Visible = false;
                    break;
            }
        }

        /// <summary>
        /// Clique no botão para gerar calculos com os determinantes das matrizes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGerarCalculosDet_Click(object sender, EventArgs e)
        {
            switch (comboBoxDeterminantes.Text)
            {
                case "DET A + DET B":
                    calculosComDet("+");
                    break;

                case "DET A - DET B":
                    calculosComDet("-");
                    break;

                case "DET A * DET B":
                    calculosComDet("*");
                    break;

                case "DET A / DET B":
                    calculosComDet("/");
                    break;
            }
        }

        /// <summary>
        /// Clique no botão gerar para o plano
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGerarMenuGrafico_Click(object sender, EventArgs e)
        {
            switch (comboBoxGrafico.Text)
            {
                case "":
                    MessageBox.Show("Escolha uma opção", "Aviso Importante", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;

                case "DETERMINANTE":
                    determinanteGrafico();
                    break;

                case "FÓRMULA":
                    gerarMatrizGraficoFormula();
                    break;

                case "ESCALAR":
                    multiplicarMatrizPlano();
                    break;

                case "ELEVAR":
                    elevarMatrizPlano();
                    break;
            }
        }

        /// <summary>
        /// Método para escalar matriz do plano a qualquer numero
        /// </summary>
        private void elevarMatrizPlano()
        {
            try
            {
                float[,] matriz1 = MatrizesInterface.resgatarNumeros(panel4GraficoMatriz, 2, colunasPlano);
                if (matriz1.Length == 0)
                {
                    semMatriz();
                }
                else if (textBoxMenuGrafico.Text == "")
                {
                    digiteNumero();
                }
                else if (colunasPlano == 2)
                {
                    float expoente = float.Parse(textBoxNumeroMatriz1.Text);
                    panel4GraficoMatriz.Controls.Clear();
                    float[,] resultado = Calculos.elevarMatrizNumeroQualquer(matriz1, expoente);
                    MatrizesInterface.instanciarTextBoxMatrizResultante(panel4GraficoMatriz, resultado);
                    groupBoxResultado.Text = "Resultante da Matriz 1 elevada";
                }
                else
                {
                    quadrada();
                }
            }
            catch (Exception)
            {
                error();
            }
        }

        /// <summary>
        /// Método para multiplicar a matriz do plano por número qualquer
        /// </summary>
        private void multiplicarMatrizPlano()
        {
            try
            {
                float[,] matriz = MatrizesInterface.resgatarNumeros(panel4GraficoMatriz, 2, colunasPlano);

                if (matriz.Length == 0)
                {
                    semMatriz();
                }
                else if (textBoxMenuGrafico.Text == "")
                {
                    digiteNumero();
                }
                else
                {
                    float valor = float.Parse(textBoxMenuGrafico.Text);
                    panel4GraficoMatriz.Controls.Clear();
                    matrizResultante = Calculos.multiplicarPorNumeroQualquer(matriz, valor);
                    MatrizesInterface.instanciarTextBoxMatrizResultante(panel4GraficoMatriz, matrizResultante);
                }
            }
            catch (Exception)
            {
                error();
            }
        }
        /// <summary>
        /// Método para calcular determinante da matriz para plano cartesiano
        /// </summary>
        private void determinanteGrafico()
        {
            try
            {
                float[,] matriz = MatrizesInterface.resgatarNumeros(panel4GraficoMatriz, 2, (int)numericUpDownColunasGrafico.Value);
                if (matriz.Length == 0)
                {
                    semMatriz();
                }
                else if((int)numericUpDownColunasGrafico.Value == 2)
                {
                    float determinanteResultado = Calculos.gerarDeterminante(matriz);
                    MessageBox.Show("Determinante: " + determinanteResultado.ToString(),"Resultado do Determinante");
                }
                else
                {
                    quadrada();
                }
            }
            catch (Exception)
            {
                error();
            }
        }

        /// <summary>
        /// Método para fazer calculos com os determinantes das matrizes
        /// </summary>
        /// <param name="operador">Recebe o operadar que será utilizado na operação</param>
        private void calculosComDet(string operador)
        {
            try
            {
                if (linha1 == coluna1 && linha2 == coluna2)
                {
                    float[,] matriz1 = MatrizesInterface.resgatarNumeros(panel1, linha1, coluna1);
                    float[,] matriz2 = MatrizesInterface.resgatarNumeros(panel2, linha2, coluna2);

                    float determinanteResultado1 = Calculos.gerarDeterminante(matriz1);
                    float determinanteResultado2 = Calculos.gerarDeterminante(matriz2);

                    float numeroFinal = 0;

                    switch(operador)
                    {
                        case "+":
                          numeroFinal = (determinanteResultado1 + determinanteResultado2);
                            break;

                        case "-":
                          numeroFinal = (determinanteResultado1 - determinanteResultado2);
                            break;

                        case "*":
                            numeroFinal = (determinanteResultado1 * determinanteResultado2);
                            break;

                        case "/":
                            numeroFinal = (determinanteResultado1 / determinanteResultado2);
                            break;
                    }

                    MessageBox.Show(numeroFinal.ToString());
                }
                else
                {
                    MessageBox.Show("Estas marizes não são quadradas", "Aviso Importante", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ocorreu um erro na soma dos determinantes", "Aviso Importante", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Método do evento KeyPress, para bloquear outras letras da caixa para inserir fórmula da Matriz B
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void formulaMatriz2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                 (e.KeyChar != ',' && e.KeyChar != '-' && e.KeyChar != '+' && e.KeyChar != '*' && e.KeyChar != '/' && e.KeyChar != 'i' && e.KeyChar != 'j' && e.KeyChar != '^' && e.KeyChar != ' '))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == ',') && ((sender as TextBox).Text.IndexOf(',') > -1))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Método do evento KeyPress, para bloquear outras letras da caixa para inserir fórmula para Matriz do Gráfico
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txbFormulaMatrizGrafico_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                 (e.KeyChar != ',' && e.KeyChar != '-' && e.KeyChar != '+' && e.KeyChar != '*' && e.KeyChar != '/' && e.KeyChar != 'i' && e.KeyChar != 'j' && e.KeyChar != '^' && e.KeyChar != ' '))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == ',') && ((sender as TextBox).Text.IndexOf(',') > -1))
            {
                e.Handled = true;
            }
        }
        /// <summary>
        /// Abaixo todos os eventos de clique para levar aos sites
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://lucassoares.tk");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.facebook.com/luca.alves.737?fref=ts");
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://patrickscoralickcosta.github.io");
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.facebook.com/thais.dutra.90?fref=ts");
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://antoanne.com");
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.facebook.com/cristina.neves.1610?fref=ts");
        }

        private void linkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.facebook.com/navecejll/?fref=ts");
        }

        /// <summary>
        /// Evento de clique no logo para abrir mensagem
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Desenvolvemos esta calculadora para ajudar estudantes e alunos no estudo de matrizes, esperamos que todos possam aproveitar este nosso trabalho, e que seja muito útil!","Agradecimentos Especiais", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Método para gerar a matriz do gráfico por formula
        /// </summary>
        private void gerarMatrizGraficoFormula()
        {
            try
            {
                float[,] matrizResultante = Calculos.matrizPorFormula(2, (int)numericUpDownColunasGrafico.Value, txbFormulaMatrizGrafico.Text);
                panel4GraficoMatriz.Controls.Clear();
                MatrizesInterface.instanciarTextBoxMatrizResultante(panel4GraficoMatriz, matrizResultante);
            }
            catch (Exception)
            {
                MessageBox.Show("Fórmula para lei de formação inválida", "Aviso Importante", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Método para mostrar mensagem avisando que não há matriz
        /// </summary>
        private void semMatriz()
        {
            MessageBox.Show("Não há matriz para calcular", "Aviso Importante", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        /// <summary>
        /// Método para mostrar mensagem avisando de algum erro para alguma função
        /// </summary>
        private void error()
        {
            MessageBox.Show("Ocorreu um erro", "Aviso Importante", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Método para mostrar mensagem avisando para usuário digitar um número
        /// </summary>
        private void digiteNumero()
        {
            MessageBox.Show("Digite um número para fazer o cálculo", "Aviso Importante", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void quadrada()
        {
            MessageBox.Show("Esta matriz não é quadrada", "Aviso Importante", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

    }
}

