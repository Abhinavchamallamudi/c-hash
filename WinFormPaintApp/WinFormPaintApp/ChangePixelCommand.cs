using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WinFormPaintApp
{
    public class ChangePixelCommand : ICommand
    {
        private Color _previousColor;
        private int _x;
        private int _y;

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            throw new NotImplementedException();
        }

        public void Execute(Bitmap image, int x, int y, Color color)
        {
            // record the data needed to undo this operation.
            _previousColor = image.GetPixel(x, y);
            _x = x;
            _y = y;

            // update the image.
            image.SetPixel(x, y, color);
        }

        public void Execute(object parameter)
        {
            throw new NotImplementedException();
        }

        public void Undo(Bitmap image)
        {
            //  Use the data we saved earlier to put the pixel back the way it was.
            image.SetPixel(_x, _y, _previousColor);
        }
    }
}
