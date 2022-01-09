using System.Windows;

namespace Calculator
{
    public partial class History : Window
    {
        public History()
        {
            InitializeComponent();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}
