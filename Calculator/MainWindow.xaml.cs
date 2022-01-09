using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using Microsoft.Data.Sqlite;

namespace Calculator
{
    public partial class MainWindow : Window
    {
        private readonly History history = new();

        public MainWindow()
        {
            InitializeComponent();
            foreach (UIElement c in MainGrid.Children)
            {
                if (c is Button)
                {
                    ((Button)c).Click += Button_Click;
                }
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string s = (string)((Button)e.OriginalSource).Content;
            switch (s)
            {
                case "History":
                    history.Show();
                    break;

                case "=":
                    try
                    {
                        string expr = textBox.Text;
                        textBox.Text = CalculatorLogic.Calculations.Calculate(textBox.Text);
                        expr += "=" + textBox.Text;
                        TextBlock time = new();
                        time.Text = DateTime.Now.ToString();
                        TextBlock expression = new();
                        expression.Text = expr;
                        history.HistPanel.Children.Add(time);
                        history.HistPanel.Children.Add(expression);
                        using (StreamWriter sw = new("log.txt", true, Encoding.Default))
                        {
                            sw.WriteLine("{0}:", DateTime.Now);
                            sw.WriteLine(expr);
                        }
                        using (SqliteConnection connection = new("Data Source=log.db"))
                        {
                            connection.Open();
                            SqliteCommand command = new("CREATE TABLE if not exists Calculations(_id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, Expression TEXT NOT NULL, Time TEXT NOT NULL)", connection);
                            command.ExecuteNonQuery();
                            command.CommandText = $"INSERT INTO Calculations (Expression, Time) VALUES ('{expr}', '{DateTime.Now}')";
                            command.ExecuteNonQuery();
                        }
                    }
                    catch (ArgumentException)
                    { }
                    catch
                    {
                        MessageBox.Show("Impossible to solve!");
                    }
                    break;

                case "AC":
                    textBox.Text = "";
                    break;

                case "⮜":
                    textBox.Text = textBox.Text.Remove(textBox.Text.Length - 1);
                    break;

                default:
                    textBox.Text += s;
                    break;
            }
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        
    }
}
