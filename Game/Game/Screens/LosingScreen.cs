using Game.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Game.Screens
{
    public class LosingScreen : GameScreen
    {
        List<string> scores;
        List<Button> buttons;
        Texture2D backgroundImage;
        private int score;
        private string text, textBox;
        private bool highScore;

        Button A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z;

        public LosingScreen(int Score)
        {
            scores = new List<string>();
            score = Score;
        }

        public bool CheckScore(int Score)
        {
            if (scores.Count < 10)
                return true;

            if (Score > Int32.Parse(scores[scores.Count - 1].Split(',')[1]))
                return true;
            else
                return false;
        }

        public void LoadContent() 
        {
            ContentManager Content = ScreenManager.Game.Content;
            //backgroundImage = Content.Load<Texture2D>("");

            if(highScore)LoadButtonsContent();

            base.LoadContent();
        }

        public void Initialize()
        {
            scores.AddRange(System.IO.File.ReadAllLines("Text/HighScores.txt"));

            if (CheckScore(score))
            {
                InitializeButtons();

                text = "Congratulations, You made it into top 10! Please enter your name : ";
                highScore = true;
            }
            else
            {
                text = "Sorry, You did not make it into top 10 !";
            }

            base.Initialize();
        }

        public void AddScore(int Score, string Name)
        {
            scores.Add(Name + "," + Score);

            scores.Sort(compareHighScores);

            scores.RemoveAt(scores.Count - 1);

            System.IO.File.WriteAllLines("Text/HighScores.txt", scores);

        }

        public void InitializeButtons()
        {
            #region Letter Buttons
            A = new Button();
            B = new Button();
            C = new Button();
            D = new Button();
            E = new Button();
            F = new Button();
            G = new Button();
            H = new Button();
            I = new Button();
            J = new Button();
            K = new Button();
            L = new Button();
            M = new Button();
            N = new Button();
            O = new Button();
            P = new Button();
            Q = new Button();
            R = new Button();
            S = new Button();
            T = new Button();
            U = new Button();
            V = new Button();
            W = new Button();
            X = new Button();
            Y = new Button();
            Z = new Button();

            buttons.Add(A);
            buttons.Add(B);
            buttons.Add(C);
            buttons.Add(D);
            buttons.Add(E);
            buttons.Add(F);
            buttons.Add(G);
            buttons.Add(H);
            buttons.Add(I);
            buttons.Add(J);
            buttons.Add(K);
            buttons.Add(L);
            buttons.Add(M);
            buttons.Add(N);
            buttons.Add(O);
            buttons.Add(P);
            buttons.Add(Q);
            buttons.Add(R);
            buttons.Add(S);
            buttons.Add(T);
            buttons.Add(U);
            buttons.Add(V);
            buttons.Add(W);
            buttons.Add(X);
            buttons.Add(Y);
            buttons.Add(Z);

            #endregion
        }

        public void LoadButtonsContent()
        {

        }
            
        private static int compareHighScores(string x1, string y1)
        {
            string[] x = x1.Split(',');
            string[] y = y1.Split(',');

            if(Int32.Parse(x[1]) > Int32.Parse(y[1])) return 1;
            else if (Int32.Parse(x[1]) < Int32.Parse(y[1])) return -1;
            else return 0;
        }

        public void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            spriteBatch.Begin();
            spriteBatch.End();
        }
    }
}
