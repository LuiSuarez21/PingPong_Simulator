using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
/*
 * Criador: Luis Esteves;
 * Projecto:  Criar um jogo similar com o Pong como forma de treinar desenvolvimento em jogos digitais em C#;
 * Julho de 2021;
 */

// Prova de commit 2;
// Prova commit 3;

namespace Pong
{
    class Program : GameWindow
    {
        //Varíaveis que nos vão ajudar ao longo do projecto;
        #region Varíaveis
        int contador = 0;
        //Bola;
        int xBola = 0;
        int yBola = 0;
        int tamanhoBola = 20;
        int velocidadeBolaX = 3;
        int velocidadeBolaY = 3;
        //Bola2;
        int xBola2 = 0;
        int yBola2 = 0;
        int velocidadeBolaX2 = 3;
        int velocidadeBolaY2 = 3;
        //Jogadores;
        int YJogador2 = 0;
        int YJogador1 = 0;
        //Barra Central;
        int larguraBarra = 10;
        int alturaBarra = 0;
        //Barras de marcaração;
        int XContador = 0;
        int YContador = 0;
        int alturaContador = 3;
        int larguraContador = 2;
        #endregion

        #region Construtores
        int xDoJogador1()
        {
            return -ClientSize.Width / 2 + LarguraJogadores() / 2;
        }

        int xDoJogador2()
        {
            return ClientSize.Width / 2 - LarguraJogadores() / 2;
        }

        int LarguraJogadores()
        {
            return tamanhoBola;
        }

        int AlturaJogadores()
        {
            return tamanhoBola * 3;
        }

        int AlturaBarra()
        {
            return ClientSize.Height;
        }

        int CentroConsola()
        {
            return ClientSize.Width / 2;
        }


        #endregion

        #region Funções

        //Função que faz update da Consola, dos pixeis todos que envolvem o jogo;
        protected override void OnUpdateFrame (FrameEventArgs e)
        {
            xBola = xBola + velocidadeBolaX;
            yBola = yBola + velocidadeBolaY;
            yBola2 = yBola2 + velocidadeBolaY2;
            xBola2 = xBola2 + velocidadeBolaX2;


            if (xBola + tamanhoBola / 2 > xDoJogador2() - LarguraJogadores() / 2
                && yBola - tamanhoBola / 2 < YJogador2 + AlturaJogadores() / 2 
                && yBola + tamanhoBola / 2 > YJogador2 - AlturaJogadores() / 2 && contador < 6)
            {
                velocidadeBolaX = -velocidadeBolaX;
                if (velocidadeBolaX > 0) velocidadeBolaX++;
                if (velocidadeBolaX < 0) velocidadeBolaX--;
                if (velocidadeBolaY > 0) velocidadeBolaY++;
                if (velocidadeBolaY < 0) velocidadeBolaY--;
            }

            if (xBola + tamanhoBola / 2 > xDoJogador2() - LarguraJogadores() / 2
                && yBola - tamanhoBola / 2 < YJogador2 + AlturaJogadores() / 2
                && yBola + tamanhoBola / 2 > YJogador2 - AlturaJogadores() / 2 && contador == 6)
            {
                velocidadeBolaX = -velocidadeBolaX;
                velocidadeBolaX2 = -velocidadeBolaX2;
            }

            if (xBola - tamanhoBola / 2 < xDoJogador1() + LarguraJogadores() / 2
                && yBola - tamanhoBola / 2 < YJogador1 + AlturaJogadores() / 2
                && yBola + tamanhoBola / 2 > YJogador1 - AlturaJogadores() / 2 && contador < 6 )
            {
                velocidadeBolaX = -velocidadeBolaX;
                if (velocidadeBolaX > 0) velocidadeBolaX++;
                if (velocidadeBolaX < 0) velocidadeBolaX--;
                if (velocidadeBolaY > 0) velocidadeBolaY++;
                if (velocidadeBolaY < 0) velocidadeBolaY--;
            }

            if (xBola - tamanhoBola / 2 < xDoJogador1() + LarguraJogadores() / 2
                && yBola - tamanhoBola / 2 < YJogador1 + AlturaJogadores() / 2
                && yBola + tamanhoBola / 2 > YJogador1 - AlturaJogadores() / 2 && contador == 6)
            {
                velocidadeBolaX = -velocidadeBolaX;
                velocidadeBolaX2 = -velocidadeBolaX2;

            }

            if (xBola < -ClientSize.Width / 2 || xBola > ClientSize.Width / 2)
            {
                if (contador < 6)contador++;
                velocidadeBolaX = 3;
                velocidadeBolaY = 3;
                velocidadeBolaY2 = 3;
                velocidadeBolaX2 = 3;
                yBola = 0;
                xBola = 0;
                yBola2 = 0;
                xBola2 = 0;
            }

            if ((yBola - tamanhoBola / 2) > (ClientSize.Height / 2) )
            {
                velocidadeBolaY = -velocidadeBolaY;
                velocidadeBolaY2 = -velocidadeBolaY2;

            }

            if ((yBola - tamanhoBola / 2) < (-ClientSize.Height / 2) )
            {
                velocidadeBolaY = -velocidadeBolaY;
                velocidadeBolaY2 = -velocidadeBolaY2;
            }



            if (Keyboard.GetState().IsKeyDown(Key.W))
            {
                YJogador1 = YJogador1 + 5;
            }

            if (Keyboard.GetState().IsKeyDown(Key.S))
            {
                YJogador1 = YJogador1 - 5;
            }

            if (Keyboard.GetState().IsKeyDown(Key.Up))
            {
                YJogador2 = YJogador2 + 5;
            }

            if (Keyboard.GetState().IsKeyDown(Key.Down))
            {
                YJogador2 = YJogador2 - 5;
            }
        }

        //Função que irá criar a nossa consola e afins;
        protected override void OnRenderFrame (FrameEventArgs e)
        {
            GL.Viewport(0, 0, ClientSize.Width, ClientSize.Height);
            Matrix4 projection = Matrix4.CreateOrthographic(ClientSize.Width, ClientSize.Height, 0.0f, 1.0f);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            DesenharRectangulo(xBola,yBola,tamanhoBola,tamanhoBola, 1.0f, 1.0f, 0.0f);
            if (contador == 6)
            {
                DesenharRectangulo(xBola2, yBola2, tamanhoBola, tamanhoBola, 1.0f, 1.0f, 0.0f);
                velocidadeBolaY2 = -velocidadeBolaY;
                velocidadeBolaX2 = -velocidadeBolaX;
            }
            DesenharRectangulo(xDoJogador1(), YJogador1, LarguraJogadores(), AlturaJogadores(), 1.0f, 0.0f, 0.0f);
            DesenharRectangulo(xDoJogador2(), YJogador2, LarguraJogadores(), AlturaJogadores(), 0.0f, 0.0f, 1.0f);
            alturaBarra = AlturaBarra();
            int x = (CentroConsola() - larguraBarra / 2 - CentroConsola());
            DesenharRectangulo(x, alturaBarra/2, larguraBarra, alturaBarra*2 , 1.0f, 1.0f, 1.0f);
            SwapBuffers();
        }

        //Função que irá criar os rectangulos do jogo;
        void DesenharRectangulo(int x, int y, int largura, int altura, float r, float g, float b)
        {
            GL.Color3(r, g, b);
            GL.Begin(PrimitiveType.Quads);
            GL.Vertex2(-0.5f * largura + x, -0.5f * altura + y);
            GL.Vertex2(0.5f * largura + x, -0.5f * altura + y);
            GL.Vertex2(0.5f * largura + x, 0.5f * altura + y);
            GL.Vertex2(-0.5f * largura + x, 0.5f * altura + y);
            GL.End();
        }
        #endregion

        static void Main(string[] args)
        {
            new Program().Run();

        }
    }
}
