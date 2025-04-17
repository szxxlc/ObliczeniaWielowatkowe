using ObliczeniaWielowatkowe;
using System.Windows.Forms;

namespace ImageProcessingGUI
{
    public partial class Form1 : Form
    {
        private OpenFileDialog openFileDialog1;
        private Bitmap? img;

        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string defaultPath = "duck.jpg";

            if (File.Exists(defaultPath))
            {
                img = new Bitmap(defaultPath);
                pictureBox1.Image = img;
            }
            else
            {
                MessageBox.Show("Default JPG not found.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "JPEG files (*.jpg)|*.jpg";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.Title = "Select a JPG file";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var file = openFileDialog1.FileName;

                if (System.IO.Path.GetExtension(file).ToLower() == ".jpg")
                {
                    img = new Bitmap(file);
                    pictureBox1.Image = img;
                }
                else
                {
                    MessageBox.Show("Files other than *.jpg are forbidden", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (img == null)
            {
                MessageBox.Show("Load an image first >:(", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var watch = System.Diagnostics.Stopwatch.StartNew();

            ImageProcessing processor = new ImageProcessing(img);

            int completed = 0;
            object lockObj = new object();

            void FilterFinished(Bitmap bmp, PictureBox box)
            {
                box.Invoke(() => box.Image = bmp);

                lock (lockObj)
                {
                    completed++;
                    if (completed == 4)
                    {
                        watch.Stop();
                        textBox1.Invoke(() => textBox1.Text = $"{watch.ElapsedMilliseconds} ms");
                    }
                }
            }

            processor.StartAllFilters(
                bmp => FilterFinished(bmp, pictureBox2),
                bmp => FilterFinished(bmp, pictureBox3),
                bmp => FilterFinished(bmp, pictureBox4),
                bmp => FilterFinished(bmp, pictureBox5)
            );
        }
    }

    public static class ControlExtensions
    {
        public static void Invoke(this Control control, Action action)
        {
            if (control.InvokeRequired)
                control.Invoke(new MethodInvoker(action));
            else
                action();
        }
    }

}
