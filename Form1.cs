using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Recognition;

namespace speecrecog
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
           
        //kelime tanımlama
        private string[] words = { "run" };

        private void Form1_Load(object sender, EventArgs e)
        {

            
            SpeechRecognitionEngine speechRecognitionEngine = new SpeechRecognitionEngine();
            new System.Globalization.CultureInfo("en-US");
            speechRecognitionEngine.SetInputToDefaultAudioDevice();

           
            Choices choices = new Choices();

            foreach (string word in this.words)
            {
                choices.Add(word);
            }

            
            GrammarBuilder grammarBuilder = new GrammarBuilder(choices);
            grammarBuilder.Culture = System.Globalization.CultureInfo.GetCultureInfoByIetfLanguageTag("en-US");

            Grammar grammar = new Grammar(grammarBuilder);

        
            speechRecognitionEngine.LoadGrammar(grammar);
            speechRecognitionEngine.SpeechRecognized += this.speechRecognitionEngine_SpeechRecognized;
            speechRecognitionEngine.RecognizeAsync(RecognizeMode.Multiple);
        }



       

       

    

        private void speechRecognitionEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            foreach (RecognizedWordUnit recognizedWord in e.Result.Words)
            {
                // words dizisi algılanan kelimeyi içeriyor mu?
                if (words.Contains(recognizedWord.Text))
                {


                    // Yukarıda belirlenen kelime için.
                    listBox1.Items.Add(recognizedWord.Text);
                    System.Diagnostics.Process.Start(label1.Text);
                }

            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            //dosya seçtirme 
            OpenFileDialog file = new OpenFileDialog();
            file.ShowDialog();
            file.RestoreDirectory = true;
            file.Title = "Dosya Seçiniz..";


            string DosyaYolu = file.FileName;
            // seçilen dosyanın tüm yolunu verir
            string DosyaAdi = file.SafeFileName;
            // seçilen dosyanın adını verir.

            label1.Text = DosyaYolu;
            label2.Text = DosyaAdi;

            if (string.IsNullOrWhiteSpace(DosyaYolu))
            {
                MessageBox.Show("program seç");
            }

            
        }
    }
    
}
