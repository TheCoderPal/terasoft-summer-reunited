using Game.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace Game.Screens
{
    public class LosingScreen : GameScreen
    {
        private List<string> scores;
        private List<Button> buttons;
        private Texture2D backgroundImage;
        private HandCursor hand;
        private int score;
        private string text;
        private StringBuilder textBox;
        private bool highScore;
        private SpriteFont font;
        private Vector2 textOrigin;

        private Button A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z, OKButton;

        public LosingScreen(int Score)
        {
            scores = new List<string>();
            buttons = new List<Button>();
            hand = new HandCursor();
            score = Score;
            textBox = new StringBuilder("", 6);
        }

        public bool CheckScore(int Score)
        {
            if (Score > Int32.Parse(scores[scores.Count - 1].Split(',')[1]) || scores.Count < 10)
                return true;
            else
                return false;
        }

        public override void LoadContent() 
        {
            ContentManager Content = ScreenManager.Game.Content;
            backgroundImage = Content.Load<Texture2D>("Textures/losingScreen");
            font = Content.Load<SpriteFont>("Fontopo");
            if (CheckScore(score)) LoadButtonsContent(Content);

            hand.LoadContent(Content);
            base.LoadContent();
        }

        public override void Initialize()
        {
            scores.AddRange(System.IO.File.ReadAllLines("Text/HighScores.txt"));

            if (CheckScore(score))
            {
                InitializeButtons();

                text = "                          CONGRATULATIONS ! \n YOU MADE IT INTO TOP 10 ! PLEASE ENTER YOUR NAME :\n\n" +
                    "                         YOUR SCORE IS: " + score;
            }
            else
            {
                text = "                                SORRY ! \n           YOU DID NOT MAKE IT INTO THE TOP 10 \n\n" +
                    "                        YOUR SCORE IS: " + score;
            }

            OKButton = new Button();
            OKButton.Initialize("Buttons/OK", this.ScreenManager.Kinect, new Vector2(1050, 330));
            buttons.Add(OKButton);
            OKButton.Clicked += new Button.ClickedEventHandler(OKButton_Clicked);

            hand.Initialize(ScreenManager.Kinect);
            base.Initialize();
        }

        void OKButton_Clicked(object sender, EventArgs a)
        {
            if (CheckScore(score))
                AddScore(score, textBox.ToString());

            this.Remove();
            ScreenManager.AddScreen(new MainScreen());
        }

        public override void Update(GameTime gameTime)
        {
            hand.Update(gameTime);
                foreach (Button b in buttons)
                {
                    b.Update(gameTime);
                }

            base.Update(gameTime);
        }

        public void AddScore(int Score, string Name)
        {
            string path = "Text/HighScores.txt";

            scores.Add(Name + "," + Score);

            scores.Sort(compareHighScores);

            scores.RemoveAt(scores.Count - 1);

            StreamWriter fileWriter = new StreamWriter(path, false);

            foreach (string s in scores)
            {
                fileWriter.WriteLine(s);
            }

            fileWriter.Close();

            fileWriter.Dispose();
        }

        public void InitializeButtons()
        {
            #region Letter Buttons
            Kinect.Kinect Kinect = this.ScreenManager.Kinect;

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

            A.Initialize("Buttons/A", Kinect, new Vector2(317, 270));
            B.Initialize("Buttons/B", Kinect, new Vector2(A.Position.X + 80, A.Position.Y));
            C.Initialize("Buttons/C", Kinect, new Vector2(B.Position.X + 80, A.Position.Y));
            D.Initialize("Buttons/D", Kinect, new Vector2(C.Position.X + 80, A.Position.Y));
            E.Initialize("Buttons/E", Kinect, new Vector2(D.Position.X + 80, A.Position.Y));
            F.Initialize("Buttons/F", Kinect, new Vector2(E.Position.X + 80, A.Position.Y));
            G.Initialize("Buttons/G", Kinect, new Vector2(F.Position.X + 80, A.Position.Y));
            H.Initialize("Buttons/H", Kinect, new Vector2(G.Position.X + 80, A.Position.Y));
            I.Initialize("Buttons/I", Kinect, new Vector2(H.Position.X + 80, A.Position.Y));

            J.Initialize("Buttons/J", Kinect, new Vector2(A.Position.X, A.Position.Y + 100));
            K.Initialize("Buttons/K", Kinect, new Vector2(A.Position.X + 80, J.Position.Y));
            L.Initialize("Buttons/L", Kinect, new Vector2(B.Position.X + 80, J.Position.Y));
            M.Initialize("Buttons/M", Kinect, new Vector2(C.Position.X + 80, J.Position.Y));
            N.Initialize("Buttons/N", Kinect, new Vector2(D.Position.X + 80, J.Position.Y));
            O.Initialize("Buttons/O", Kinect, new Vector2(E.Position.X + 80, J.Position.Y));
            P.Initialize("Buttons/P", Kinect, new Vector2(F.Position.X + 80, J.Position.Y));
            Q.Initialize("Buttons/Q", Kinect, new Vector2(G.Position.X + 80, J.Position.Y));
            R.Initialize("Buttons/R", Kinect, new Vector2(H.Position.X + 80, J.Position.Y));

            S.Initialize("Buttons/S", Kinect, new Vector2(J.Position.X + 40, J.Position.Y + 100));
            T.Initialize("Buttons/T", Kinect, new Vector2(S.Position.X + 80, S.Position.Y));
            U.Initialize("Buttons/U", Kinect, new Vector2(T.Position.X + 80, S.Position.Y));
            V.Initialize("Buttons/V", Kinect, new Vector2(U.Position.X + 80, S.Position.Y));
            W.Initialize("Buttons/W", Kinect, new Vector2(V.Position.X + 80, S.Position.Y));
            X.Initialize("Buttons/X", Kinect, new Vector2(W.Position.X + 80, S.Position.Y));
            Y.Initialize("Buttons/Y", Kinect, new Vector2(X.Position.X + 80, S.Position.Y));
            Z.Initialize("Buttons/Z", Kinect, new Vector2(Y.Position.X + 80, S.Position.Y));

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

        public void LoadButtonsContent(ContentManager Content)
        {

            foreach (Button b in buttons)
                b.LoadContent(Content);
        }
            
        private static int compareHighScores(string x1, string y1)
        {
            string[] x = x1.Split(',');
            string[] y = y1.Split(',');

            if(Int32.Parse(x[1]) > Int32.Parse(y[1])) return -1;
            else if (Int32.Parse(x[1]) < Int32.Parse(y[1])) return 1;
            else return 0;
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            spriteBatch.Begin();
            spriteBatch.Draw(backgroundImage, Vector2.Zero, Color.White);


            foreach (Button b in buttons)
                b.Draw(spriteBatch);

            spriteBatch.DrawString(font, text, new Vector2(290, 35), Color.White);
            spriteBatch.DrawString(font, textBox, new Vector2(ScreenManager.GraphicsDevice.Viewport.Width / 2 - 200, 220), Color.White);
            hand.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
