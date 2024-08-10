using System.ComponentModel;

namespace RainWorldSaveEditor.Controls;

[DefaultEvent("PipCountChanged")]
public partial class FoodPipControl : UserControl
{
    public FoodPipControl()
    {
        InitializeComponent();
    }

    private byte _pipCount = 7;
    private byte _pipBar = 4;
    private byte _filledPips = 0;

    public event EventHandler? PipCountChanged;
    public byte PipCount
    {
        get => _pipCount;
        set
        {
            _pipCount = value;
            if (_pipBar > PipCount)
                _pipBar = PipCount;
            if (_filledPips > PipCount)
                _filledPips = PipCount;
            AdjustControlSize();
            Invalidate();
        }
    }

    public byte PipBarIndex
    {
        get => _pipBar;
        set
        {
            _pipBar = value;
            if (_pipBar > PipCount)
                _pipBar = PipCount;

            Invalidate();
        }
    }

    public byte FilledPips
    {
        get => _filledPips;
        set
        {
            _filledPips = value;
            if (_filledPips > PipCount)
                _filledPips = PipCount;
            AdjustControlSize();
            Invalidate();
        }
    }
    private void AdjustControlSize()
    {
        var minWidth = ((Properties.Resources.Food_pip_empty.Width + 1) * PipCount) + (Properties.Resources.Food_line.Width + 1);
        if (Width < minWidth)
            Width = minWidth;
    }

    private void FoodPipControl_Paint(object sender, PaintEventArgs e)
    {
        e.Graphics.Clear(BackColor);

        using Bitmap emptyPipBitmap = Properties.Resources.Food_pip_empty;
        using Bitmap fullPipBitmap = Properties.Resources.Food_pip_full;
        using Bitmap fullPipGrayBitmap = Properties.Resources.Food_pip_full_gray;
        using Bitmap pipBarBitmap = Properties.Resources.Food_line;

        var currX = 0;

        for (var i = 0; i < PipCount; i++)
        {
            if (i == _pipBar)
                currX += (pipBarBitmap.Width + 1);

            if (i <= (FilledPips - 1))
            {
                if (_mouseHoverIndex != -1 && i >= _mouseHoverIndex)
                    e.Graphics.DrawImage(fullPipGrayBitmap, currX, (Height / 2) - (emptyPipBitmap.Height / 2));
                else
                    e.Graphics.DrawImage(fullPipBitmap, currX, (Height / 2) - (emptyPipBitmap.Height / 2));
            }
            else
            {
                if (i <= _mouseHoverIndex)
                    e.Graphics.DrawImage(fullPipGrayBitmap, currX, (Height / 2) - (emptyPipBitmap.Height / 2));
                else
                    e.Graphics.DrawImage(emptyPipBitmap, currX, (Height / 2) - (emptyPipBitmap.Height / 2));
            }
            currX += (emptyPipBitmap.Width + 1);

        }

        e.Graphics.DrawImage(pipBarBitmap, (emptyPipBitmap.Width + 1) * PipBarIndex, (Height / 2) - (pipBarBitmap.Height / 2));
    }


    private bool _containsMouse = false;
    private Point _mousePoint = new(0, 0);
    private int _mouseHoverIndex = -1;

    private void UpdateMouseInfo()
    {
        if (!_containsMouse)
        {
            _mouseHoverIndex = -1;
            Invalidate();
        }
        var prev_index = _mouseHoverIndex;
        _mouseHoverIndex = -1;

        var currX = 0;
        for (var i = 0; i < PipCount; i++)
        {
            if (i == _pipBar)
                currX += 8;

            RectangleF bounds = new(currX, (Height / 2) - 13.5f, 27, 27);

            if (bounds.Contains(_mousePoint.X, _mousePoint.Y))
            {
                if (prev_index != i)
                {
                    _mouseHoverIndex = i;
                    Invalidate();
                    break;
                }
            }

            currX += 28;
        }

        if (_mouseHoverIndex == -1)
            _mouseHoverIndex = prev_index;
    }


    private void FoodPipControl_MouseLeave(object sender, EventArgs e)
    {
        _containsMouse = false;
        UpdateMouseInfo();
    }

    private void FoodPipControl_MouseEnter(object sender, EventArgs e)
    {
        _containsMouse = true;
        UpdateMouseInfo();
    }
    private void FoodPipControl_MouseMove(object sender, MouseEventArgs e)
    {
        _mousePoint = e.Location;
        UpdateMouseInfo();
    }

    private void FoodPipControl_MouseClick(object sender, MouseEventArgs e)
    {
        _mousePoint = e.Location;
        UpdateMouseInfo();

        if (e.Button == MouseButtons.Left)
        {
            FilledPips = (byte)(_mouseHoverIndex + 1);
            PipCountChanged?.Invoke(this, EventArgs.Empty);
        }
        else if (e.Button == MouseButtons.Right)
        {
            FilledPips = 0;
            PipCountChanged?.Invoke(this, EventArgs.Empty);
        }
        
    }
}
