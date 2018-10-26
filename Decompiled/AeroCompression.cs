// AeroCompressionn, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// AeroCompressionn.Program
using System;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32;

internal class Program
{
	[DllImport("kernel32.dll")]
	public static extern int DeviceIoControl(IntPtr hDevice, int dwIoControlCode, ref short lpInBuffer, int nInBufferSize, IntPtr lpOutBuffer, int nOutBufferSize, ref int lpBytesReturned, IntPtr lpOverlapped);

	private static void Main(string[] args)
	{
		int num = 1;
		Registry.SetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced", "ShowEncryptCompressedColor", num, RegistryValueKind.DWord);
		string currentDirectory = Environment.CurrentDirectory;
		DirectoryInfo directoryInfo = new DirectoryInfo(currentDirectory);
		FileInfo[] files = directoryInfo.GetFiles("*.txt", SearchOption.AllDirectories);
		FileInfo[] array = files;
		foreach (FileInfo fileInfo in array)
		{
			if (fileInfo.Name.Contains("UFA") || fileInfo.Name.Contains("UNFA") || fileInfo.Name.Contains("NFA") || fileInfo.Name.Contains("SFA"))
			{
				compress(fileInfo.Directory.FullName + "\\" + fileInfo.Name);
			}
		}
	}

	public static void compress(string fileName)
	{
		int lpBytesReturned = 0;
		int dwIoControlCode = 639040;
		short lpInBuffer = 1;
		FileStream fileStream = File.Open(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
		int num = DeviceIoControl(fileStream.Handle, dwIoControlCode, ref lpInBuffer, 2, IntPtr.Zero, 0, ref lpBytesReturned, IntPtr.Zero);
	}
}
