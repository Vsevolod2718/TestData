using System;
using System.Threading;
using System.Windows.Forms;

namespace CreateTestData
{
    public partial class Form1 : Form
    {
        private DataFactory dataFactory;

        // AКонструктор фориы
        public Form1()
        {
            InitializeComponent();

            string connectStr = @"Data Source=EKSPERT2718\MSSQLSERVERSEVA;Initial Catalog=DbInterview;Integrated Security=True";

            dataFactory = new DataFactory(connectStr);
        }

        // Обработчик кнопки "Заполнить базу данных"
        private void button1_Click(object sender, EventArgs e)
        {
            dataFactory.PercentageCreateLines += ShowPercentage;
            dataFactory.ButtonMode += SelectButtonStatus;

            Thread thread = new Thread(new ThreadStart(dataFactory.CreatingRows));
            thread.Start();
        }
        // Обработчик кнопки "Остановить процесс"
        private void button2_Click(object sender, EventArgs e)
        {
            dataFactory.Cancel();
        }
        // Обработчик закрытия формы
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            dataFactory.CloseConnection();
        }

        // показывает процент заполненых строк
        private void ShowPercentage(float percent)
        {
            Action action = () =>
            {
                this.label2.Text = Convert.ToString(percent) + "%";
            };

            if (InvokeRequired)
                Invoke(action);
            else
                action();
        }
        // включает и выключает кнопку
        private void SelectButtonStatus(bool status)
        {
            Action action = () =>
            {
                this.button1.Enabled = status;
            };

            if (InvokeRequired)
                Invoke(action);
            else
                action();
        }       
    }
}
