using Game.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Text;

namespace Game.Screens
{
    public class LosingScreen : GameScreen
    {
        List<string> scores;
        List<Button> buttons;
        Texture2D backgroundImage;
        private int score;
        private string text;
        private StringBuilder textBox;
        private bool highScore;
        

        Button A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z, OKButton;

        public LosingScreen(int Score)
        {
            scores = new List<string>();
            score = Score;
            textBox = new StringBuilder();
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
            //backgroundImage = Content.Load<Texture2D>("Textures/losingScreen");

            if(highScore)LoadButtonsContent();

            base.LoadContent();
        }

        public void Initialize()
        {
            scores.AddRange(System.IO.File.ReadAllLines("Text/HighScores.txt"));

            if (CheckScore(score))
            {
                InitializeButtons();

                text = "Congratulations !\n You made it into top 10! Please enter your name : ";
                highScore = true;
            }
            else
            {
                text = "Sorry ! \n You did not make it into the top 10 !";
            }

            OKButton = new Button();
            //OKButton.Initialize();
            OKButton.Clicked += new Button.ClickedEventHandler(OKButton_Clicked);
            base.Initialize();
        }

        void OKButton_Clicked(object sender, EventArgs a)
        {
            if (highScore)
                AddScore(score, textBox.ToString());

            this.Remove();
            ScreenManager.AddScreen(new MainScreen());
        }

        public void Update(GameTime gameTime)
        {
            if (highScore)
            {
                foreach (Button b in buttons)
                {
                    b.Update(gameTime);
                }
            }

            base.Update(gameTime);
        }
        public void AddScore(int Score, string Name)
        {
            scores.Add(Name + "," + Score + ",");

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

            A.Clicked += new Button.ClickedEventHandler(A_Clicked);
            B.Clicked += new Button.ClickedEventHandler(B_Clicked);
            C.Clicked += new Button.ClickedEventHandler(C_Clicked);
            D.Clicked += new Button.ClickedEventHandler(D_Clicked);
            E.Clicked += new Button.ClickedEventHandler(E_Clicked);
            F.Clicked += new Button.ClickedEventHandler(F_Clicked);
            G.Clicked += new Button.ClickedEventHandler(G_Clicked);
            H.Clicked += new Button.ClickedEventHandler(H_Clicked);
            I.Clicked += new Button.ClickedEventHandler(I_Clicked);
            J.Clicked += new Button.ClickedEventHandler(J_Clicked);
            K.Clicked += new Button.ClickedEventHandler(K_Clicked);
            L.Clicked += new Button.ClickedEventHandler(L_Clicked);
            M.Clicked += new Button.ClickedEventHandler(M_Clicked);
            N.Clicked += new Button.ClickedEventHandler(N_Clicked);
            O.Clicked += new Button.ClickedEventHandler(O_Clicked);
            P.Clicked += new Button.ClickedEventHandler(P_Clicked);
            Q.Clicked += new Button.ClickedEventHandler(Q_Clicked);
            R.Clicked += new Button.ClickedEventHandler(R_Clicked);
            S.Clicked += new Button.ClickedEventHandler(S_Clicked);
            T.Clicked += new Button.ClickedEventHandler(T_Clicked);
            U.Clicked += new Button.ClickedEventHandler(U_Clicked);
            V.Clicked += new Button.ClickedEventHandler(V_Clicked);
            W.Clicked += new Button.ClickedEventHandler(W_Clicked);
            X.Clicked += new Button.ClickedEventHandler(X_Clicked);
            Y.Clicked += new Button.ClickedEventHandler(Y_Clicked);
            Z.Clicked += new Button.ClickedEventHandler(Z_Clicked);

            A.Initialize("Buttons/A", this.ScreenManager.Kinect, new Vector2(317, 270));
            #endregion
        }

        #region Letter Button Listeners
        void Z_Clicked(object sender, EventArgs a)
        {
            textBox.Append('Z');
        }

        void Y_Clicked(object sender, EventArgs a)
        {
            textBox.Append('Y');
        }

        void X_Clicked(object sender, EventArgs a)
        {
            textBox.Append('X');
        }

        void W_Clicked(object sender, EventArgs a)
        {
            textBox.Append('W');
        }

        void V_Clicked(object sender, EventArgs a)
        {
            textBox.Append('V');
        }

        void U_Clicked(object sender, EventArgs a)
        {
            textBox.Append('U');
        }

        void T_Clicked(object sender, EventArgs a)
        {
            textBox.Append('T');
        }

        void S_Clicked(object sender, EventArgs a)
        {
            textBox.Append('S');
        }

        void R_Clicked(object sender, EventArgs a)
        {
            textBox.Append('R');
        }

        void Q_Clicked(object sender, EventArgs a)
        {
            textBox.Append('Q');
        }

        void P_Clicked(object sender, EventArgs a)
        {
            textBox.Append('P');
        }

        void O_Clicked(object sender, EventArgs a)
        {
            textBox.Append('O');
        }

        void N_Clicked(object sender, EventArgs a)
        {
            textBox.Append('N');
        }

        void M_Clicked(object sender, EventArgs a)
        {
            textBox.Append('M');
        }

        void L_Clicked(object sender, EventArgs a)
        {
            textBox.Append('L');
        }

        void K_Clicked(object sender, EventArgs a)
        {
            textBox.Append('K');
        }

        void J_Clicked(object sender, EventArgs a)
        {
            textBox.Append('J');
        }

        void I_Clicked(object sender, EventArgs a)
        {
            textBox.Append('I');
        }

        void H_Clicked(object sender, EventArgs a)
        {
            textBox.Append('H');
        }

        void G_Clicked(object sender, EventArgs a)
        {
            textBox.Append('G');
        }

        void F_Clicked(object sender, EventArgs a)
        {
            textBox.Append('F');
        }

        void E_Clicked(object sender, EventArgs a)
        {
            textBox.Append('E');
        }

        void D_Clicked(object sender, EventArgs a)
        {
            textBox.Append('D');
        }

        void C_Clicked(object sender, EventArgs a)
        {
            textBox.Append('C');
        }

        void B_Clicked(object sender, EventArgs a)
        {
            textBox.Append('B');
        }

        void A_Clicked(object sender, EventArgs a)
        {
            textBox.Append('A');
        }
        #endregion

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
