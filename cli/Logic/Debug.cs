#if HS_CONSOLE
using System;
// 让命令行版兼容Unity的一些Log代码
public class Debug {
	public static void Log(string msg){
		Console.WriteLine(msg);
	}
	
	public static void DevLog(string msg) {
		Console.WriteLine(msg);
	}
}
#endif