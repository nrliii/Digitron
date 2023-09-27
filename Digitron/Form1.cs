using System.Text;
using System.Net.Http;


namespace Digitron
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        double trenutnaVrednost = 0;
        char trenutnaOperacija = '+';
        double trenutniBroj = 0;
        private void BrojDugme_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            textBoxOutput.Text += button.Text;
        }
        private void CEDugme_Click(object sender, EventArgs e)
        {
            textBoxOutput.Text = "";
        }
        private void PlusDugme_Click(object sender, EventArgs e)
        {
            trenutniBroj = double.Parse(textBoxOutput.Text);
            textBoxOutput.Text = "";
            trenutnaOperacija = '+';
        }

        private void MinusDugme_Click(object sender, EventArgs e)
        {
            trenutniBroj = double.Parse(textBoxOutput.Text);
            textBoxOutput.Text = "";
            trenutnaOperacija = '-';
        }

        private void PomnoziDugme_Click(object sender, EventArgs e)
        {
            trenutniBroj = double.Parse(textBoxOutput.Text);
            textBoxOutput.Text = "";
            trenutnaOperacija = '*';
        }

        private void PodeliDugme_Click(object sender, EventArgs e)
        {
            trenutniBroj = double.Parse(textBoxOutput.Text);
            textBoxOutput.Text = "";
            trenutnaOperacija = '/';
        }
        bool PrikazanEasterEgg = false;
        private async void JednakoDugme_Click(object sender, EventArgs e)
        {
            double noviBroj = double.Parse(textBoxOutput.Text);
            trenutnaVrednost = trenutnaOperacija switch
            {
                '+' => trenutniBroj + noviBroj,
                '-' => trenutniBroj - noviBroj,
                '*' => trenutniBroj * noviBroj,
                '/' => trenutniBroj / noviBroj,
                _ => throw new ArgumentException("Invalidna operacija")
            };

            textBoxOutput.Text = trenutnaVrednost.ToString();
            trenutnaVrednost = 0;
            trenutnaOperacija = '+';

            var timer = new System.Windows.Forms.Timer
            {
                Interval = 500,
                Enabled = true
            };

            timer.Tick += (s, ev) =>
            {
                if (textBoxOutput.Text == "69" && !PrikazanEasterEgg)
                {
                    PrikazanEasterEgg = true;
                    MessageBox.Show("Nice :)");
                }

                timer.Stop();
                timer.Dispose();
            };

            await SendToDiscordWebhookAsync(textBoxOutput.Text);

        }
        private async Task SendToDiscordWebhookAsync(string result)
        {
            using (HttpClient client = new HttpClient())
            {
                string webhookUrl = "WEBHOOK";
                string message = result == "69" ? "Nice :)" : $"Rezultat je: {result}";

                var content = new StringContent($"{{ \"content\": \"{message}\" }}", Encoding.UTF8, "application/json");
                await client.PostAsync(webhookUrl, content);
            }
        }

    }
}