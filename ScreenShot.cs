using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Drawing;
using System.Threading;
using System.Runtime.InteropServices;

public class ScreenShot 
{
    private IntPtr m_HBitmap;
    private IntPtr deskhWnd;
    private long lastShotTime;
    private int increment;
    private Bitmap lastBMP;

    public ScreenShot(int msbeween)
    {
        increment = msbeween;
        lastShotTime = System.DateTime.Now.Ticks;
    }
    public  Bitmap GetDesktopImage()
    {
        if (System.DateTime.Now.Ticks < lastShotTime + increment)
        {
            return(lastBMP);
        }
        //Debug.Log("Thread: " + Thread.CurrentThread.Name);
        WIN32_API.SIZE size;
        deskhWnd = WIN32_API.GetDesktopWindow();
        IntPtr hDC = WIN32_API.GetDC(deskhWnd);
        IntPtr hMemDC = WIN32_API.CreateCompatibleDC(hDC);

        size.cx = WIN32_API.GetSystemMetrics(WIN32_API.SM_CXSCREEN);
        size.cy = WIN32_API.GetSystemMetrics(WIN32_API.SM_CYSCREEN);



        if (m_HBitmap == IntPtr.Zero)
        {
            m_HBitmap = WIN32_API.CreateCompatibleBitmap(hDC, size.cx, size.cy);
        }

        if (m_HBitmap != IntPtr.Zero)
        {
            IntPtr hOld = (IntPtr)WIN32_API.SelectObject(hMemDC, m_HBitmap);
            WIN32_API.BitBlt(hMemDC, 0, 0, size.cx, size.cy, hDC, 0, 0, WIN32_API.SRCCOPY);
            WIN32_API.SelectObject(hMemDC, hOld);
            var a = WIN32_API.DeleteDC(hMemDC);
            var b = WIN32_API.ReleaseDC(deskhWnd, hDC);
            var o = System.Drawing.Image.FromHbitmap(m_HBitmap);
            //Debug.Log(a.ToString() + ":" + b.ToString());
            lastShotTime = System.DateTime.Now.Ticks;
            lastBMP = o;
            return lastBMP;

        }
        
        return null;
    }
    public class WIN32_API
    {
        public struct SIZE
        {
            public int cx;
            public int cy;
        }
        public const int SRCCOPY = 13369376;
        public const int SM_CXSCREEN = 0;
        public const int SM_CYSCREEN = 1;

        [DllImport("gdi32.dll", EntryPoint = "DeleteDC")]
        public static extern IntPtr DeleteDC(IntPtr hDc);

        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        public static extern IntPtr DeleteObject(IntPtr hDc);

        [DllImport("gdi32.dll", EntryPoint = "BitBlt")]
        public static extern bool BitBlt(IntPtr hdcDest, int xDest, int yDest, int wDest, int hDest, IntPtr hdcSource, int xSrc, int ySrc, int RasterOp);

        [DllImport("gdi32.dll", EntryPoint = "CreateCompatibleBitmap")]
        public static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int nWidth, int nHeight);

        [DllImport("gdi32.dll", EntryPoint = "CreateCompatibleDC")]
        public static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        [DllImport("gdi32.dll", EntryPoint = "SelectObject")]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr bmp);

        [DllImport("user32.dll", EntryPoint = "GetDesktopWindow")]
        public static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll", EntryPoint = "GetDC")]
        public static extern IntPtr GetDC(IntPtr ptr);

        [DllImport("user32.dll", EntryPoint = "GetSystemMetrics")]
        public static extern int GetSystemMetrics(int abc);

        [DllImport("user32.dll", EntryPoint = "GetWindowDC")]
        public static extern IntPtr GetWindowDC(Int32 ptr);

        [DllImport("user32.dll", EntryPoint = "ReleaseDC")]
        public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDc);

    }
}
