/*
This File Created
By: Burak Hamdi TUFAN
On: https://thecodeprogram.com/
At: 18.04.2020
*/

using System;
using System.Drawing;
using System.IO;

namespace TheWatermark
{
    class Program
    {
        public static string imagePath = "";

        static void Main(string[] args)
        {
            Console.Title = "Thecodeprogram Watermark Application";

        head_of_program:
            //Before we start to write a watermark text we have to get image full path
            Console.WriteLine("Please drag and drop the file here : ");
            imagePath = Console.ReadLine();

            //Then we need to create target image location.
            //I set it here beside the old image with endfix of old image name.
            string sourceImagePath = imagePath;
            string targetImagepath = Path.GetDirectoryName(imagePath);
            targetImagepath += "\\";
            targetImagepath += Path.GetFileNameWithoutExtension(imagePath) + "_watermarked";
            targetImagepath += Path.GetExtension(imagePath);

            //Call the method to write watermark.
            write_watermark_text(sourceImagePath, "THECODEPROGRAM.COM", targetImagepath);

            Console.WriteLine("Watermark added successfully...");
            Console.WriteLine("--------------------------------------");

            //Goto head of the program
            goto head_of_program;
        }

        public static void write_watermark_text(string sourceImage, string text, string targetImage)
        {
            try
            {
                //First we have to take the image from the file and here enable Embedded Color management
                //loaded this related image to the Image variable.
                Image img = Image.FromFile(sourceImage, true);

                //Then we need to get the image width and height.
                //We will use this values to set font size according to image size.
                //Also we are going to set the image starting location according to image dimensions.
                int width = img.Width;
                int height = img.Height;
                int font_size = (width > height ? height : width) / 9;

                //We set text starting location according to image size.
                //If we do not be carefull about this, the text can be overflow from image.
                //So we prevent this. 
                Point text_starting_point = new Point(height  / 4, (width / 4 ) );

                // Later we set the font of the text. I set the Comic Sans MS as font family and 
                //I set the font size according to image size dynamically.
                Font text_font = new Font("Comic Sans MS", font_size, FontStyle.Bold, GraphicsUnit.Pixel);

                //Also we set the text color and transparency of the text.
                //And we create the Brush to write the text with specified color and trancperancy.
                Color color = Color.FromArgb(30, 0, 255, 0);
                SolidBrush brush = new SolidBrush(color);

                //To write a text we create a Graphic component and load the above image inside.
                //Then we draw our string with specified font, color and transparency at specified location above.
                //After our string drawing we will dispose the image to reduce the memory usage.
                Graphics graphics = Graphics.FromImage(img);
                graphics.DrawString(text, text_font, brush, text_starting_point);
                graphics.Dispose();
                
                //After all we need to save our image to the target location.
                img.Save(targetImage);
                //And dispose the image to reduce memory usage of this component.
                img.Dispose();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
