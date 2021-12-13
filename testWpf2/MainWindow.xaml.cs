using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace testWpf2
{
    public partial class MainWindow : Window
    {
        bool createNode = true;
        public MainWindow()
        {
            InitializeComponent();
        }

        int id=1;

        private void canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (createNode)
            {
                Button btn = new Button();
                var point = Mouse.GetPosition(this);
                btn.Width = 30;
                btn.Height = 30;
                btn.VerticalAlignment = VerticalAlignment.Top;
                btn.Margin = new Thickness(point.X - 15, point.Y - 110, 0, 0);
                btn.Click += new RoutedEventHandler(ClickNode);
                btn.Name = $"id{id}";
                canvas.Children.Add(btn);
                id++;
            }
        }
        bool FirstCoord = true;
        double y1;
        double x1;
        string nameFirstNode;
        void ClickNode(object sender, RoutedEventArgs e)
        {
            if (!createNode)
            {
                canvas.Children.Remove(sender as Button);

                var images = canvas.Children.OfType<Line>().ToList(); 
                foreach (var image in images)
                {
                    var name = image.Name.Split("_");
                    for (int i = 0; i < 2; i++)
                    {
                        if (name[i] == $"{(sender as Button).Name}") 
                            canvas.Children.Remove(image); 
                    }
                   
                }

            }

            else
            {
                if (FirstCoord)
                {

                    y1 = (sender as Button).DesiredSize.Height;
                    x1 = (sender as Button).DesiredSize.Width;
                    nameFirstNode = $"{(sender as Button).Name}";

                    FirstCoord = false;
                }
                else
                {
                    Line line = new Line();

                    line.Y1 = y1-15;
                    line.X1 = x1-15;
                    line.Y2 = (sender as Button).DesiredSize.Height-15;
                    line.X2 = (sender as Button).DesiredSize.Width-15;
                    line.Stroke = Brushes.Black;

                    line.Name = nameFirstNode + $"_{(sender as Button).Name}";

                    canvas.Children.Add(line);
                    FirstCoord = true;
                }
               
            }
        }

        private void btn_CreateNode_Click(object sender, RoutedEventArgs e)
        {
            if (createNode)
            {
                createNode = false;
                this.btn_CreateNode.Content = "Удалить узел";
            }
            else
            {
                createNode = true;
                this.btn_CreateNode.Content = "Создать узел";
            }
                
        }
    }
}
