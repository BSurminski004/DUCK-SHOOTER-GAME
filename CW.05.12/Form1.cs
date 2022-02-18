using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using System.Media;

namespace CW._05._12
{
    public partial class Form1 : Form
    {
        bool t; //zmienna do zatrzymywania timera
        int trafienia = 0;
        int razem = 0;
        SoundPlayer playerDuck; //glos kaczki
        SoundPlayer playerShot; //glos strzalu
        public Form1()
        {
            InitializeComponent();
            playerDuck = new SoundPlayer("Quack.wav");
            playerShot = new SoundPlayer("Shot.wav");
            button4.Enabled = false;

            Image x = Image.FromFile("duck1.png"); //kaczka do ktorej sie strzela
            Bitmap abc = new Bitmap(x, button2.Width, button2.Height); //zamieniam z png na bitmape
            Image cde = (Image)abc; 
            button2.Image = cde; //przydzielam obrazek kaczki do przycisku 2

            button2.FlatStyle = FlatStyle.Flat;
            button2.Visible = false;

            Panel panel1 = new Panel(); //panel na ktorym ma sie pojawiac kaczka
            panel1.Size = new Size(1243, 423);

            timer1.Interval = 3000; //interwal timera poczatkowy          
        }

        private void timer1_Tick(object sender, EventArgs e) //metoda do timera
        {
            playerDuck.Play();
            float p = PunktR();
            celnosc(trafienia, p);
            kaczka(); //kaczka zmienia polozenie
        }


        private void trackBar1_Scroll(object sender, EventArgs e) //trackbar do ustawiania poziomu trudnosci
        {
            if (trackBar1.Value == 1)
            {
                label4.Text = "Łatwy";
                timer1.Stop();
                timer1.Interval = 3000;
                label3.Text = "3s";    
            }
            else if (trackBar1.Value == 2)
            {
                label4.Text = "Średni";
                timer1.Stop();
                timer1.Interval = 2000;
                label3.Text = "2s";
            }
            else if (trackBar1.Value == 3)
            {
                label4.Text = "Trudny";
                timer1.Stop();
                timer1.Interval = 1000;
                label3.Text = "1s";
            }
            else if (trackBar1.Value == 4)
            {
                label4.Text = "Bardzo Trudny";
                timer1.Stop();
                timer1.Interval = 500;
                label3.Text = "0.5s";
            }
            else if (trackBar1.Value == 5)
            {
                label4.Text = "Bardziej Trudny";
                timer1.Stop();
                timer1.Interval = 250;
                label3.Text = "0.25s";
            }
            else if (trackBar1.Value == 6)
            {
                label4.Text = "Arcy Trudny";
                timer1.Stop();
                timer1.Interval = 100;
                label3.Text = "0.1s";
                playerShot.Stop();
                MessageBox.Show("Jesteś tego pewien? Łatwo nie będzie..","ARCYTRUDNY POZIOM", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button3_Click(object sender, EventArgs e) //Przycisk WYJSCIE
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e) //Przycisk START
        {
            StartGame();
            trackBar1.Enabled = false;
            button1.Enabled = false;
            button4.Enabled = true;
        }

        public void StartGame() //metoda do startu gry
        {
            button2.Visible = true;
            timer1.Start();
            t = true; //timer liczy
        }
       
        
        private void button2_Click(object sender, EventArgs e) //przycisk kaczka
        {
            playerShot.Play();
            timer1.Stop();
            kaczka();
            double tr =liczPunktTraf();
            double p =PunktR();
            celnosc(tr,p);
            timer1.Start();
        }
        
        private int liczPunktTraf() //metoda zliczajaca trafienia
        {
            trafienia += 1;
            label6.Text = trafienia.ToString();
            return trafienia;
        }

        private int PunktR() //metoda zlicza punkty w sumie
        {
            razem += 1;
            label8.Text = razem.ToString();
            return razem;
        }

        private void celnosc(double t, double r) //metoda do obliczania celnosci
        {
            double cel;
            cel = (t / r) * 100;
            if (t == 0)
            { label10.Text = "0 %"; }
            else
            {
                label10.Text = String.Format("{0:##}", cel) + "%"; //wynik w % bez miejsc po przecinku
            }
        }

        private void kaczka() //metoda do zmiany polozenia kaczki
        {
            Random rnd1 = new Random();
            int x = rnd1.Next(0, 850);
            int y = rnd1.Next(0, 300); 
            button2.Location = new Point(x, y);
        }

        private void button4_Click(object sender, EventArgs e) //przycisk restart
        {
            t = false;
            timerStopStart();
            label6.Text = "0";
            label8.Text = "0";
            trafienia = 0;
            razem = 0;
            label10.Text = "0 %";
            kaczka();
            timer1.Start();
            t = true;
        }

        private void button5_Click(object sender, EventArgs e)//Przycisk STOP/START
        {
            timerStopStart();
        } 

        private void timerStopStart() //metoda zo zatrzymywania i odpalania timera
        {
            if (t) //t==true
            { 
                timer1.Stop();
                trackBar1.Enabled = true;
                button5.Text = "START |>";
                t = false;
            }
            else //t==false
            {
                timer1.Start();
                button5.Text = "STOP ||";
                trackBar1.Enabled = false;
                t = true;
            }
        }

        private void button6_Click(object sender, EventArgs e)//Przycisk INFO
        {
            MessageBox.Show("Ustrzel  kaczkę przed końcem czasu albo odleci!! \n\nZ każdą ucieczką kaczki Twoja celność " +
                "spada, pamiętaj o tym..\n\nPoziom trudności ustawiasz za pomocą suwaka, po naciśnięciu przycisku START suwak staje się niedostępny." +
                "\n\nJeśli dany poziom trudnosci jest dla Ciebie za łatwy/trudny możesz go zmienić klikając przycisk STOP|| i używając suwaka(suwak poziomu trudności staje się wtedy dostępny)." +
                "\n\nJeśli chcesz zacząć grę od początku  kliknij RESTART.\n\nAby wyjść z gry kliknij WYJSCIE \n\n" +
                "Jeśli podobała Ci się moja gra lub jest coś, co warto w niej poprawić to będzie mi " +
                "niezmiernie miło jeśli wyślesz mi swoją opinię na mój adres email:\n\nbartosz.surminski@student.put.poznan.pl\n\n" +
                "\nMiłej zabawy!!\n;)","WITAJ W DUCKSHOOTER BS!", MessageBoxButtons.OK,MessageBoxIcon.Question);
        } 
    }
}
