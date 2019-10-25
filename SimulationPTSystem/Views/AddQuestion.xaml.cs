using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TaskConstructor.EFModels;
using System.Windows.Forms;
using System.IO;

namespace TaskConstructor.Views
{
    /// <summary>
    /// AddQuestion.xaml 的交互逻辑
    /// </summary>
    public partial class AddQuestion : Window
    {
        public AddQuestion()
        {
            InitializeComponent();
        }

        private void ImageButton_Click_1(object sender, RoutedEventArgs e)
        {
            using (DarkFieldContext dfc = new DarkFieldContext())
            {
               
                ItemBank question = new ItemBank();
                if (radioText.IsChecked==true)
                {
                    question.ItemGenre = "String";
                    question.QBody = System.Text.Encoding.Default.GetBytes(this.body.Text.ToString());

                }
                else if (radioPicture.IsChecked==true)
                {
                    question.ItemGenre = "Picture";
                    question.QBody = TaskConstructor.Utility.GlobalHelper.BitmapImageToByteArray(this.demoImage.Source as BitmapImage);
                    question.QPathVideoOrPics = pathVideo;

                }
                else if (radioVideo.IsChecked == true)
                {
                    question.ItemGenre = "Video";
                    question.QBody = System.Text.Encoding.Default.GetBytes(this.body.Text.ToString());

                    question.QPathVideoOrPics = pathVideo ;
                }

                //   byte[] imageBuffer = System.IO.File.ReadAllBytes(@"H:\DarkField\TaskConstructor\TaskConstructor\Resources\images\查询_0.png");
                //   question.QBody = imageBuffer;
                //  question.TaskKind = comboPlan.SelectedValue.ToString();
                question.TaskKind = comboPlan.Text;
                question.QAnswer = this.rightAnswer.Text.ToString();
                question.QHead = this.title.Text.ToString();


              
                

             //   question.QBody = System.Text.Encoding.Default.GetBytes(this.body.Text.ToString());
                question.QOption = this.options.Text.ToString();

                
                dfc.ItemBank.Add(question);
                dfc.SaveChanges();


            }
        }

        private void ImageButton_Click_2(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ImageButton_Click(object sender, RoutedEventArgs e)
        {

        }

        public string pathVideo = "";

        private void openFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            //过滤上传图片的类型  
            ofd.Filter = "jpg图片|*.jpg|png图片|*.png|jpeg图片|*.jpeg|bmp图片|*.bmp";
           // ofd.InitialDirectory = "c:\\";
            ofd.FilterIndex = 2;
            ofd.RestoreDirectory =false;
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string fileName = ofd.FileName;
                pathVideo = ofd.FileName;


                using (BinaryReader loader = new BinaryReader(File.Open(fileName, FileMode.Open)))
               {
                   FileInfo fd = new FileInfo(fileName);
                   int Length = (int)fd.Length;
                   byte[] buf = new byte[Length];
                   buf = loader.ReadBytes((int)fd.Length);
                   loader.Dispose();
                   loader.Close();


                   //开始加载图像  
                   BitmapImage bim = new BitmapImage();
                   bim.BeginInit();
                   bim.StreamSource = new MemoryStream(buf);
                   bim.EndInit();
                  demoImage.Source = bim;
                   GC.Collect(); //强制回收资源  
               } 
            }
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            this.gridPicture.Visibility = Visibility.Hidden;
            this.body.Visibility = Visibility.Visible;
        }

        private void RadioButton_Click_1(object sender, RoutedEventArgs e)
        {
            this.gridPicture.Visibility = Visibility.Visible;
            this.body.Visibility = Visibility.Hidden;
        }

        /*
        private void AboutView_Checked(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            //过滤上传图片的类型  
            //  ofd.Filter = "jpg图片|*.jpg|png图片|*.png";
            ofd.Filter = "mov视频|*.mov|mp4视频|*.mp4";
            // ofd.InitialDirectory = "c:\\";
            ofd.FilterIndex = 2;
            ofd.RestoreDirectory = false;
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string fileName = ofd.FileName;
                this.videoPath.Text = ofd.FileName;
            }
        }
        */

        private void radioVideo_Click(object sender, RoutedEventArgs e)
        {
            this.gridPicture.Visibility = Visibility.Hidden;
            this.body.Visibility = Visibility.Visible;
            OpenFileDialog ofd = new OpenFileDialog();
            //过滤上传图片的类型  
            //  ofd.Filter = "jpg图片|*.jpg|png图片|*.png";
            ofd.Filter = "mov视频|*.mov|mp4视频|*.mp4";
            // ofd.InitialDirectory = "c:\\";
            ofd.FilterIndex = 2;
            ofd.RestoreDirectory = false;
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string fileName = ofd.FileName;
                pathVideo = ofd.FileName;
                //  this.videoPath.Text = ofd.FileName;
            }

        }
    }
}
